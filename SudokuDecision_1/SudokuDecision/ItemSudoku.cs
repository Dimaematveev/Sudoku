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
    }
}
