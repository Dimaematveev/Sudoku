﻿using System;
using System.Collections;
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
            Rows = new List<ListSudoku>();
            Columns = new List<ListSudoku>();
            Tables = new List<ListSudoku>();
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

    public class ListSudoku:IEnumerable
    {
       
        public List<ItemSudoku> ItemSudokus { get; set; }
        private DataTable DataTable;
        public ListSudoku(DataTable dataTable)
        {
            ItemSudokus = new List<ItemSudoku>();
            DataTable = dataTable;
        }

        public void Add(int row, int col)
        {
            ItemSudokus.Add(new ItemSudoku(row, col, DataTable));
        }

        public IEnumerator GetEnumerator()
        {
            return ItemSudokus.GetEnumerator();
        }
        public override string ToString()
        {
            string ret = "";
            foreach (var item in this)
            {
                ret += $"[{item}], ";
            }

            return ret;
        }
    }
    public class ItemSudoku
    {
        public object Item { get => DataTable.Rows[Row][Col]; set => DataTable.Rows[Row][Col] = value; }
        public int Row { get; private set; }
        public int Col { get; private set; }
        private DataTable DataTable;

        public ItemSudoku(int row, int col,DataTable dataTable)
        {
            Row = row;
            Col = col;
            DataTable = dataTable;
        }

        public override string ToString()
        {
            return $"c:{Col}_r:{Row}-i:{Item}";
        }
    }
}
