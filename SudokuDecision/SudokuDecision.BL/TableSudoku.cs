using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuDecision.BL
{
    public class TableSudoku
    {
        private DataTable DataTable;
        public List<ListSudoku> Rows;
        public List<ListSudoku> Columns;
        public List<ListSudoku> Tables;

        

        public TableSudoku(DataTable dataTable)
        {
            DataTable = dataTable;
            FillAll();
        }




        private void FillAll()
        {
            FillColumns();
            FillRows();
            FillTables();
        }
        private void FillColumns()
        {
            Columns = new List<ListSudoku>();
            for (int i = 0; i < DataTable.Columns.Count; i++)
            {
                var temp = new ListSudoku(DataTable);
                for (int j = 0; j < DataTable.Rows.Count; j++)
                {
                    temp.Add(j, i);
                }
                Columns.Add(temp);
            }
        }
        private void FillRows()
        {
            Rows = new List<ListSudoku>();
            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                var temp = new ListSudoku(DataTable);
                for (int j = 0; j < DataTable.Columns.Count; j++)
                {
                    temp.Add(i, j);

                }
                Rows.Add(temp);
            }
        }
        private void FillTables()
        {
            Tables = new List<ListSudoku>();
            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                var temp = new ListSudoku(DataTable);
                for (int j = 0; j < DataTable.Columns.Count; j++)
                {
                    int col = (i / 3) * 3 + j / 3;
                    int row = (i % 3) * 3 + j % 3;
                    temp.Add(col, row);
                }
                Tables.Add(temp);
            }
        }

        public override string ToString()
        {
            string ret = "";
            ret += "Строки: \n";
            foreach (var item in Rows)
            {
                ret += $"\t{{{item}}} \n";
            }
            ret += "Столбцы: \n";
            foreach (var item in Columns)
            {
                ret += $"\t{{{item}}} \n";
            }
            ret += "Таблицы 3х3: \n";
            foreach (var item in Tables)
            {
                ret += $"\t{{{item}}}\n";
            }
            return ret;

        }
    }
}
