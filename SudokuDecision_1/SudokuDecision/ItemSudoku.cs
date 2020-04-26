using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuDecision
{
    public class ItemSudoku
    {
        private DataTable DataTableSudoku;
        public int Row;
        public int Column;
        public ItemCellSudoku ItemCellSudoku { get => (ItemCellSudoku)DataTableSudoku.Rows[Row][Column]; }

        public ItemSudoku(DataTable dataTableSudoku, int row, int column)
        {
            DataTableSudoku = dataTableSudoku;
            Row = row;
            Column = column;
        }

        public override bool Equals(object obj)
        {
            if (obj is ItemSudoku)
            {
                return ((ItemSudoku)obj).Row.Equals(Row) && ((ItemSudoku)obj).Column.Equals(Column);
            }
            return false;
        }
    }
}
