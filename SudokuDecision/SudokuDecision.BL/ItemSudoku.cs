using System;
using System.Data;

namespace SudokuDecision.BL
{
    public class ItemSudoku:IComparable
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

        public int CompareTo(object obj)
        {
            if (obj is ItemSudoku)
            {
                var objItemSudoku = (ItemSudoku)obj;
                return GetHashCode().CompareTo(objItemSudoku.GetHashCode());
            }
            throw new Exception("Невозможно сравнить два объекта");
        }

        public override bool Equals(object obj)
        {
            if(CompareTo(obj)==0)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public override int GetHashCode()
        {
            return DataTable.Columns.Count * Row + Col;
        }
    }
}
