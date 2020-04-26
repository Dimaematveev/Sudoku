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

        public override bool Equals(object obj)
        {

            if (obj is ListItemSudoku)
            {
                ListItemSudoku listItemSudoku = (ListItemSudoku)obj;
                if (ItemSudokus.Count!= listItemSudoku.ItemSudokus.Count)
                {
                    return false;
                }
                foreach (var itemSudoku in ItemSudokus)
                {
                    if(!listItemSudoku.ItemSudokus.Contains(itemSudoku))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public bool Contains(object obj)
        {

            if (obj is ListItemSudoku)
            {
                ListItemSudoku listItemSudoku = (ListItemSudoku)obj;
                if (ItemSudokus.Count < listItemSudoku.ItemSudokus.Count)
                {
                    return false;
                }
                foreach (var itemSudoku in listItemSudoku.ItemSudokus)
                {
                    if (!ItemSudokus.Contains(itemSudoku))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public bool FindRowColumn(int row,int column)
        {
            int ind = ItemSudokus.FindIndex(x => x.Row == row && x.Column == column);
            return ind >= 0;
        }
    }
}
