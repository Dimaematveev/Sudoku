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
        public MainWindow()
        {
            InitializeComponent();
            
            Loaded += MainWindow_Loaded;
            DataGridSudoku.CellEditEnding += DataGridSudoku_CellEditEnding;
            ButtonOpenFile.Click += (sender, e) => { OpenFile_Click(); };
        }

       
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NewTable();
            FillFile(@"D:\Дмитрий\Documents\Настраиваемые шаблоны Office\sud1.csv");

        }

        private void NewTable()
        {
            int numSudoku = 9;
            for (int i = 0; i < numSudoku; i++)
            {
                dataTableSudoku.Columns.Add($"{i}", typeof(object));
            }
            for (int i = 1; i < numSudoku; i++)
            {
                var newRow = dataTableSudoku.NewRow();
                for (int j = 0; j < newRow.ItemArray.Length; j++)
                {

                    newRow[j] = new ItemCellSudoku() { Item = ' ' };
                    //newRow[j] = $"{i}_{j}";
                }
                dataTableSudoku.Rows.Add(newRow);
            }
            DataGridSudoku.ItemsSource = dataTableSudoku.DefaultView;
            DataGridSudoku.CanUserAddRows = false;
            DataGridSudoku.CanUserResizeColumns = false;
            DataGridSudoku.CanUserDeleteRows = false;
            DataGridSudoku.CanUserResizeRows = false;
            DataGridSudoku.CanUserReorderColumns = false;
            DataGridSudoku.CanUserSortColumns = false;
        }

        private void DataGridSudoku_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            TextBox cell = (TextBox)e.EditingElement;
            if (cell.Text.Length > 1)
            {
                throw new ArgumentException("Не символ");
            }
            else
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
                dataTableSudoku.Rows[e.Row.GetIndex()][e.Column.DisplayIndex] = new ItemCellSudoku() { Item = k };
            }
        }


        /* TODO: что надо примерно сделать
         * Первое создать список всех возможных объединений где не должны совпадать элементы
         * Второе после проверить на одинаковое кол-во чисел
         *
         *
         */
        private void FillFile(string nameFile)
        {
            dataTableSudoku.Clear();
            string line;
            StreamReader file = new StreamReader(nameFile);
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
                        dataRow[j] = new ItemCellSudoku() { Item = k };
                    }
                }
                dataTableSudoku.Rows.Add(dataRow);
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

    
}
