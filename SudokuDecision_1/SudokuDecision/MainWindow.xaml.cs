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
                    newRow[j] = new ItemCellSudoku(' ');
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
           
            if (!(e.EditAction == DataGridEditAction.Cancel))
            {
                if (cell.Text.Length > 1)
                {
                    MessageBox.Show("Не символ");
                    return editCellSudoku;
                }
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
            return editCellSudoku;
        }

        /// <summary>
        /// Изменение таблицы судоку по данным
        /// </summary>
        /// <param name="editCellSudoku"></param>
        public void ResetTableSudoku(EditCellSudoku editCellSudoku)
        {
            DataGridSudoku.BeginInit();
            if (editCellSudoku != null) 
            {
                ((ItemCellSudoku)dataTableSudoku.Rows[editCellSudoku.Row][editCellSudoku.Column]).ResetItem(editCellSudoku.Simbol);

                for (int row=0; row < dataTableSudoku.Rows.Count; row++)
                {
                    for (int column = 0; column < dataTableSudoku.Columns.Count; column++)
                    {
                        var item = ((ItemCellSudoku)dataTableSudoku.Rows[row][column]);
                    }
                }

                List<char> AllSimbols = new List<char>();
                for (char ch = '1'; ch < '1' + 9; ch++)
                {
                    AllSimbols.Add(ch);
                }

                foreach (var listItemSudoku in ListItemSudokus)
                {
                    foreach (var simbol in AllSimbols)
                    {
                        ListItemSudoku listItemSudokus = new ListItemSudoku();
                        List<ItemSudoku> itemSudokus = new List<ItemSudoku>();
                        foreach (var itemSudoku in listItemSudoku.ItemSudokus)
                        {
                            var item = itemSudoku;
                            if (item.ItemCellSudoku.Can.Contains(simbol))
                            {
                                itemSudokus.Add(item);
                            }
                           
                        }
                       
                        listItemSudokus.ItemSudokus = itemSudokus;
                        var k = ListItemSudokus.Where(x => x.Contains(listItemSudokus)).ToList();
                        
                        if (itemSudokus != null && k.Count > 1 && itemSudokus.Count > 0 ) 
                        {
                            //Которые надо изменить
                            var kk = k.Select(x => x.ItemSudokus.Where(y => !itemSudokus.Contains(y)).ToList()).ToList();
                            foreach (var item1 in kk)
                            {
                                foreach (var item2 in item1)
                                {
                                    item2.ItemCellSudoku.Can.Remove(simbol);
                                }
                            }
                        }
                    }
                }

            }


           
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
                        var editCellSudoku = new EditCellSudoku(lineNumber, j, k);
                        ResetTableSudoku(editCellSudoku);
                        //((ItemCellSudoku)dataTableSudoku.Rows[lineNumber][j]).ResetItem(k);
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
                        return;
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
