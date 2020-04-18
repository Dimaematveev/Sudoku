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

        public List<ListItemsSudoku> AllCrossing;

        public TableSudoku(DataTable dataTable)
        {
            DataTable = dataTable;
           
            FillAll();
            FillAllCrossing();
        }

        private void FillAllCrossing()
        {
            AllCrossing = new List<ListItemsSudoku>();
            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                for (int j = 0; j < DataTable.Columns.Count; j++)
                {
                    AllCrossing.Add( new ListItemsSudoku(new ItemSudoku(i, j,DataTable)));
                }
            }

            foreach (var crossing in AllCrossing)
            {
                crossing.AddSub(Rows);
                crossing.AddSub(Columns);
                crossing.AddSub(Tables);

            }
            AllCrossing = AllCrossing.OrderBy(x => x.CanBeUsed.Count).ToList();

            List<ListItemsSudoku> crossing_1 = new List<ListItemsSudoku>();
            crossing_1 = AllCrossing.Where(x => x.CanBeUsed.Count == 1 && x.MainItem.Item == "").GroupBy(x => x.MainItem).Select(x => x.First()).ToList();
            if (crossing_1.Count > 0)
            {
                
                foreach (var item in crossing_1)
                {
                    item.MainItem.Item = item.CanBeUsed[0];
                }

                FillAll();
                FillAllCrossing();
            }
            

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
