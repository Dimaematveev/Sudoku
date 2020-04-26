using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuDecision
{
    public class ItemCellSudoku
    {
        /// <summary> кол-во различных знаков </summary>
        static public int kolNum = 9;
        public char Item;
        public List<char> Can;
        public List<ListItemSudoku> ListItemSudokus;
        public ItemCellSudoku(char item)
        {
            Item = item;
            if (Item == ' ')
            {
                Can = new List<char>();
                for (char ch = '1'; ch < '1'+ kolNum; ch++)
                {
                    Can.Add(ch);
                }
            }
        }

        public ItemCellSudoku(char item, List<char> can) : this(item)
        {
            if (Item == ' ')
            {
                Can = can;
            }

        }

        /// <summary>
        /// Создает новые возможные вставки из ListItemSudokus
        /// </summary>
        public void NewCan()
        {
            if (Item != ' ') 
            {
                return;
            }

            foreach (var item1 in ListItemSudokus)
            {
                foreach (var item2 in item1.ItemSudokus)
                {
                    char temp=item2.ItemCellSudoku.Item;
                    Can.RemoveAll(x => x == temp);
                }
            }
        }
        public override string ToString()
        {
            string res = "";
            if (Can!=null )
            {
                foreach (var item in Can)
                {
                    res += $"{item},";
                }
            }
            
            return $"_{Item} [{res}]";
        }
    }

}
