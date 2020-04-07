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
using SudokuDecision.BL;

namespace SudokuDecision.WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable table = new DataTable();
        TableSudoku tableSudoku;

        public MainWindow() 
        {
            InitializeComponent();


            Loaded += MainWindow_Loaded;
            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Zapoln();

            ReturnAllRows();
            ReturnAllColumns();
            ReturnAllTable();

        }


        private void ReturnAllRows()
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {
                var temp = new ListSudoku(table);
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    temp.Add(j,i);
                }
                tableSudoku.Columns.Add(temp);
            }
        }

        private void ReturnAllColumns()
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var temp = new ListSudoku(table);
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    temp.Add(i,j);
                   
                }
                tableSudoku.Rows.Add(temp);
            }
        }

        private void ReturnAllTable()
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var temp = new ListSudoku(table);
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    int col = (i / 3) * 3 + j / 3;
                    int row = (i % 3) * 3 + j % 3;
                    temp.Add(col,row);
                }
                tableSudoku.Tables.Add(temp);
            }
        }

        private void Zapoln()
        {
            for (int i = 0; i < 9; i++)
            {
                table.Columns.Add(new DataColumn() { ColumnName = $"c{i}" });
            }


            for (int i = 0; i < 9; i++)
            {
                DataRow dataRow = table.NewRow();

                for (int j = 0; j < table.Columns.Count; j++)
                {
                  
                    dataRow[j] = $"r{i}_{table.Columns[j].ColumnName}";
                }
               
                table.Rows.Add(dataRow);
            }
           

            Table2.Items.Clear();
            Table2.ItemsSource = table.DefaultView;
            Table2.CanUserAddRows = false;
            Table2.CanUserResizeColumns = false;

            tableSudoku = new TableSudoku(table);

        }



        ///// <summary>
        ///// заполнение случайными числами
        ///// </summary>
        //private void Zapoln()
        //{
        //    foreach (var item1 in Table1.Children)
        //    {
        //        if (item1 is Grid)
        //        {

        //            Grid itemDataGrid = (Grid)item1;

        //            foreach (var item2 in itemDataGrid.Children)
        //            {
        //                if (item2 is TextBox)
        //                {
        //                    TextBox itemTextBox = (TextBox)item2;
        //                    itemTextBox.Text = itemTextBox.Name;
        //                }
        //            }
        //        }
        //    }
        //}
        ///// <summary>
        ///// Пробую вывести все столбцы
        ///// </summary>
        //private void ReturnAllStold()
        //{
        //    string str = "";

        //    foreach (var item1 in Table1.ColumnDefinitions)
        //    {
        //        var d = item1;
        //        var z = d.BindingGroup;
        //        //if (item1 is Grid)
        //        //{
        //        //    Grid itemDataGrid = (Grid)item1;
        //        //    foreach (var item2 in itemDataGrid.Children)
        //        //    {
        //        //        if (item2 is TextBox)
        //        //        {
        //        //            TextBox itemTextBox = (TextBox)item2;
        //        //            itemTextBox.Text = itemTextBox.Name;
        //        //        }
        //        //    }
        //        //}
        //    }

        //    MessageBox.Show(str);
        //}
    }
}
