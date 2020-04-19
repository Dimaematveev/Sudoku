using System;
using System.Collections.Generic;
using System.Data;
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
            DataGridSudoku.CellEditEnding += DataGridSudoku_CellEditEnding1;
        }

        private void DataGridSudoku_CellEditEnding1(object sender, DataGridCellEditEndingEventArgs e)
        {
            TextBox cell = (TextBox)e.EditingElement;
            if (cell.Text.Length > 1)
            {
                throw new ArgumentException("Не символ");
            }
            else
            {
                char k = cell.Text[0];
                dataTableSudoku.Rows[e.Row.GetIndex()][e.Column.DisplayIndex] = new ItemCellSudoku() { Item = k };
                //e.EditingElement= new ItemCellSudoku() { Item = k };
                //((DataGrid)sender).SelectedItem= new ItemCellSudoku() { Item = k };
            }
           
            
        }

        private void DataGridSudoku_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            if (e.EditingElement is TextBox)
            {
                TextBox cell = (TextBox)e.EditingElement;
                if (cell.Text.Length > 1) 
                {
                    throw new ArgumentException("Не символ");
                }
                else
                {
                    char k = cell.Text[0];
                    //e.EditingElement= new ItemCellSudoku() { Item = k };
                    //((DataGrid)sender).SelectedItem= new ItemCellSudoku() { Item = k };
                }
            }
            else
            {
                throw new ArgumentException("Не текстбокс");
            }
            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
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

            List<ItemCellSudoku> ListUnion = new List<ItemCellSudoku>();
        }

        /* TODO: что надо примерно сделать
         * Первое создать список всех возможных объединений где не должны совпадать элементы
         * Второе после проверить на одинаковое кол-во чисел
         *
         *
         */
        //private void FillFile(string nameFile)
        //{
        //    table.Clear();
        //    string line;
        //    StreamReader file = new StreamReader(nameFile);
        //    while ((line = file.ReadLine()) != null)
        //    {
        //        var temp = line.Split(';');

        //        DataRow dataRow = table.NewRow();

        //        for (int j = 0; j < table.Columns.Count; j++)
        //        {

        //            dataRow[j] = temp[j];
        //        }

        //        table.Rows.Add(dataRow);
        //    }


        //    Table2.ItemsSource = table.DefaultView;
        //    Table2.CanUserAddRows = false;
        //    Table2.CanUserResizeColumns = false;
        //    Table2.CanUserDeleteRows = false;
        //    Table2.CanUserResizeRows = false;
        //    Table2.CanUserReorderColumns = false;
        //    Table2.CanUserSortColumns = false;
        //}


        //private void OpenFile_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog1 = new OpenFileDialog();
        //    openFileDialog1.Filter = "All files(*.*)|*.*";
        //    openFileDialog1.ShowDialog();


        //    FillFile(openFileDialog1.FileName);
        //}
    }

    public class ForDataGrid
    {
        public string Name;

        public ForDataGrid(string name)
        {
            Name = name;
        }
    }
}
