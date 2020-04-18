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
using Microsoft.Win32;
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
            FillTable();
            FillFile(@"D:\Дмитрий\Documents\Настраиваемые шаблоны Office\sud1.csv");
            
        }


        

        private void FillTable()
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
           


            Table2.ItemsSource = table.DefaultView;
            Table2.CanUserAddRows = false;
            Table2.CanUserResizeColumns = false;
            Table2.CanUserDeleteRows = false;
            Table2.CanUserResizeRows = false;
            Table2.CanUserReorderColumns = false;
            Table2.CanUserSortColumns = false;


        }

        private void FillFile(string nameFile)
        {
            table.Clear();
            string line;
            StreamReader file = new StreamReader(nameFile);
            while ((line = file.ReadLine()) != null)
            {
                var temp = line.Split(';');

                DataRow dataRow = table.NewRow();

                for (int j = 0; j < table.Columns.Count; j++)
                {

                    dataRow[j] = temp[j];
                }

                table.Rows.Add(dataRow);
            }

            
            Table2.ItemsSource = table.DefaultView;
            Table2.CanUserAddRows = false;
            Table2.CanUserResizeColumns = false;
            Table2.CanUserDeleteRows = false;
            Table2.CanUserResizeRows = false;
            Table2.CanUserReorderColumns = false;
            Table2.CanUserSortColumns = false;
        }

        private void Zap_Click(object sender, RoutedEventArgs e)
        {
            tableSudoku = new TableSudoku(table);
            //Table2.ToolTip = "defef";\
            Style CellStyle_ToolTip = new Style();
            var CellSetter = new Setter(DataGridCell.ToolTipProperty, new Binding() { RelativeSource = new RelativeSource(RelativeSourceMode.Self), Path = new PropertyPath("Content.Text") });

            CellStyle_ToolTip.Setters.Add(CellSetter);

            Table2.CellStyle = CellStyle_ToolTip;
        }


        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files(*.*)|*.*";
            openFileDialog1.ShowDialog();


            FillFile(openFileDialog1.FileName);
        }
    }
}
