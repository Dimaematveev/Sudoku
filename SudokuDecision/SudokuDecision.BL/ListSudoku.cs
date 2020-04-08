using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace SudokuDecision.BL
{
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
}
