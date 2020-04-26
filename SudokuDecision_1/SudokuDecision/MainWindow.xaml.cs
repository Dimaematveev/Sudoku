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
            DataGridSudoku.CellEditEnding += DataGridSudoku_CellEditEnding;
            ButtonOpenFile.Click += (sender, e) => { OpenFile_Click(); };
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
                ListItemSudoku listItemSudokuRowColumn = new ListItemSudoku();
                ListItemSudoku listItemSudokuColumnRow = new ListItemSudoku();

                for (int j = 0; j < numSudoku; j++)
                {
                    listItemSudokuRowColumn.ItemSudokus.Add(new ItemSudoku(dataTableSudoku, i, j));
                    listItemSudokuColumnRow.ItemSudokus.Add(new ItemSudoku(dataTableSudoku, j, i));
                    
                }
                ListItemSudokus.Add(listItemSudokuRowColumn);
                ListItemSudokus.Add(listItemSudokuColumnRow);
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridSudoku_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            TextBox cell = (TextBox)e.EditingElement;
            
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
                ((ItemCellSudoku)dataTableSudoku.Rows[e.Row.GetIndex()][e.Column.DisplayIndex]).ResetItem(k);
                DataGridSudoku.UpdateLayout();
                DataGridSudoku.CancelEdit();
            }
            else
            {
                
            }
        }


        /* TODO: что надо примерно сделать
         * Первое создать список всех возможных объединений где не должны совпадать элементы
         * Второе после проверить на одинаковое кол-во чисел
         *
         *
         */
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


        private void OpenFile_Click()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files(*.*)|*.*";
            openFileDialog1.ShowDialog();
            FillFile(openFileDialog1.FileName);
        }
    }

    ///TODO:Можно создать список объединений а потом его добавлять в каждый ITEM
}
