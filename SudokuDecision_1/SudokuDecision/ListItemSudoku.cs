using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuDecision
{
    public class ListItemSudoku
    {
        public List<ItemSudoku> ItemSudokus;
        
        public ListItemSudoku()
        {
            ItemSudokus = new List<ItemSudoku>();
        }

        public bool FindRowColumn(int row,int column)
        {
            int ind = ItemSudokus.FindIndex(x => x.Row == row && x.Column == column);
            return ind >= 0;
        }
    }
}
