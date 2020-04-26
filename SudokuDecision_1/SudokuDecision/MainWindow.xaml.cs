using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuDecision
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable dataTableSudoku = new DataTable("Sudoku");
        List<ListItemSudoku> ListItemSudokus = new List<ListItemSudoku>();
        public MainWindow()
        {
            InitializeComponent();
            
            Loaded += MainWindow_Loaded;
            DataGridSudoku.CellEditEnding += (sender, e) => { ResetTableSudoku(DataGridSudoku_CellEditEnding(e)); };
            ButtonOpenFile.Click += (sender, e) => { OpenFile_Click(); };
            ButtonProv.Click += (sender, e) => { Prov_Click(); };
        }

       /// <summary>
       /// Первоначальная загрузка
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NewTable();
            FillFile(@"D:\Дмитрий\Documents\Настраиваемые шаблоны Office\sud1.csv");

        }

        /// <summary>
        /// Первоначальное создание таблицы
        /// </summary>
        private void NewTable()
        {
            int numSudoku = 9;
            

            for (int i = 0; i < numSudoku; i++)
            {
                dataTableSudoku.Columns.Add($"{i}", typeof(object));
            }
            for (int i = 0; i < numSudoku; i++)
            {
                var newRow = dataTableSudoku.NewRow();
                for (int j = 0; j < numSudoku; j++)
                {
                    //TODO: это объединить
                    //var z = ListItemSudokus.Where(x => x.FindRowColumn(i, j)).ToList();
                    newRow[j] = new ItemCellSudoku(' ');
                    //newRow[j] = $"{i}_{j}";
                }
                dataTableSudoku.Rows.Add(newRow);
            }

            for (int i = 0; i < numSudoku; i++)
            {
                ListItemSudoku listItemSudokuRow = new ListItemSudoku();
                ListItemSudoku listItemSudokuColumn = new ListItemSudoku();
                ListItemSudoku listItemTable= new ListItemSudoku();

                for (int j = 0; j < numSudoku; j++)
                {
                    int row = i;
                    int col = j;
                    listItemSudokuRow.ItemSudokus.Add(new ItemSudoku(dataTableSudoku, row, col));
                    row = j;
                    col = i;
                    listItemSudokuColumn.ItemSudokus.Add(new ItemSudoku(dataTableSudoku, row, col));
                    col = (i / 3) * 3 + j / 3;
                    row = (i % 3) * 3 + j % 3;
                    listItemTable.ItemSudokus.Add(new ItemSudoku(dataTableSudoku, col, row));
                }
                ListItemSudokus.Add(listItemSudokuRow);
                ListItemSudokus.Add(listItemSudokuColumn);
                ListItemSudokus.Add(listItemTable);
            }

            for (int i = 0; i < numSudoku; i++)
            {
               
                for (int j = 0; j < numSudoku; j++)
                {
                    var z = ListItemSudokus.Where(x => x.FindRowColumn(i, j)).ToList();
                    ((ItemCellSudoku)dataTableSudoku.Rows[i][j]).ListItemSudokus = z;
                }
               
            }

            



            DataGridSudoku.ItemsSource = dataTableSudoku.DefaultView;
            DataGridSudoku.CanUserAddRows = false;
            DataGridSudoku.CanUserResizeColumns = false;
            DataGridSudoku.CanUserDeleteRows = false;
            DataGridSudoku.CanUserResizeRows = false;
            DataGridSudoku.CanUserReorderColumns = false;
            DataGridSudoku.CanUserSortColumns = false;
        }

        /// <summary>
        /// Изменение ячейки в таблице
        /// </summary>
        /// <param name="e"></param>
        private EditCellSudoku DataGridSudoku_CellEditEnding(DataGridCellEditEndingEventArgs e)
        {
            TextBox cell = (TextBox)e.EditingElement;
            EditCellSudoku editCellSudoku = null;
            if (cell.Text.Length > 1)
            {
                throw new ArgumentException("Не символ");
            }
            else if (!(e.EditAction == DataGridEditAction.Cancel))
            {
                char k;
                if (cell.Text.Length == 0)
                {
                    k = ' ' ;
                }
                else
                {
                    k = cell.Text[0];
                }
                //TODO: это объединить
                editCellSudoku = new EditCellSudoku(e.Row.GetIndex(), e.Column.DisplayIndex, k);
            }
            else
            {
                
            }
            return editCellSudoku;
        }

        /// <summary>
        /// Изменение таблицы судоку по данным
        /// </summary>
        /// <param name="editCellSudoku"></param>
        public void ResetTableSudoku(EditCellSudoku editCellSudoku)
        {
            DataGridSudoku.BeginInit();
            ((ItemCellSudoku)dataTableSudoku.Rows[editCellSudoku.Row][editCellSudoku.Column]).ResetItem(editCellSudoku.Simbol);
            DataGridSudoku.EndInit();
        }

       
         /// <summary>
         /// чтение из файла
         /// </summary>
         /// <param name="sourseFile">путь к файлу</param>
        private void FillFile(string sourseFile)
        {
           
            string line;
            StreamReader file = new StreamReader(sourseFile);
            int lineNumber = 0;
            while ((line = file.ReadLine()) != null)
            {
                var temp = line.Split(';');
                DataRow dataRow = dataTableSudoku.NewRow();
                for (int j = 0; j < dataTableSudoku.Columns.Count; j++)
                {
                    if (temp[j].Length>1)
                    {
                        throw new ArgumentException("Не символ");
                    }
                    else
                    {
                        char k;
                        if (temp[j].Length == 0)
                        {
                            k = ' ';
                        }
                        else
                        {
                            k = temp[j][0];
                        }
                        //TODO: это объединить
                        //var z = ListItemSudokus.Where(x => x.FindRowColumn(lineNumber, j)).ToList();
                        ((ItemCellSudoku)dataTableSudoku.Rows[lineNumber][j]).ResetItem(k);
                    }
                   
                }
                lineNumber++;
            }
        }
        private void Prov_Click()
        {
            foreach (var listItemSudoku in ListItemSudokus)
            {
                Dictionary<char, int> SimbolNumber = new Dictionary<char, int>();
                
                foreach (var itemSudoku in listItemSudoku.ItemSudokus)
                {
                    char simbol = itemSudoku.ItemCellSudoku.Item;
                    if (SimbolNumber.ContainsKey(simbol))
                    {
                        int number = SimbolNumber[simbol];
                        SimbolNumber[simbol] = ++number;
                        MessageBox.Show($"К сожалению проблема в [{itemSudoku.Row},{itemSudoku.Column}] несколько одинаковых значений!");
                    }
                    else
                    {
                        SimbolNumber.Add(simbol, 1);
                    }
                }
                
            }
        }

            private void OpenFile_Click()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files(*.*)|*.*";
            openFileDialog1.ShowDialog();
            FillFile(openFileDialog1.FileName);
        }
    }

    ///TODO:Можно создать список объединений а потом его добавлять в каждый ITEM
    public class EditCellSudoku
    {
        public int Row;
        public int Column;
        public char Simbol;

        public EditCellSudoku(int row, int column, char simbol)
        {
            
            Row = row;
            Column = column;
            Simbol = simbol;
        }
    }
}
