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
        private List<char> Can;
        public List<ListItemSudoku> ListItemSudokus;
        public ItemCellSudoku(char item)
        {
            Item = item;
            ListItemSudokus = new List<ListItemSudoku>();
            
            AllChar();
            NewCan();
        }

        private void AllChar()
        {
            Can = new List<char>();
            if (Item == ' ')
            {
                for (char ch = '1'; ch < '1' + kolNum; ch++)
                {
                    Can.Add(ch);
                }
            }
        }

        public void ResetItem(char ch)
        {
            Item = ch;
            NewCan();

            foreach (var item1 in ListItemSudokus)
            {
                foreach (var item2 in item1.ItemSudokus)
                {
                    item2.ItemCellSudoku.NewCan();
                }
            }
        }

        /// <summary>
        /// Создает новые возможные вставки из ListItemSudokus
        /// </summary>
        public void NewCan()
        {
            if (Item != ' ') 
            {
                Can = new List<char>();
                return;
            }
            AllChar();
            //Если в 9 клетках только 1 возможное заполнение конкретного числа
            foreach (var item1 in ListItemSudokus)
            {
                foreach (var item2 in item1.ItemSudokus)
                {
                    char temp=item2.ItemCellSudoku.Item;
                    Can.RemoveAll(x => x == temp);
                }
            }
            if (Can.Count==1)
            {
                ResetItem(Can[0]);
            }

            //Если в 9 клетках только 1 раз встречается число

            foreach (var item1 in ListItemSudokus)
            {
                Dictionary<char, int> CharNumber = new Dictionary<char, int>();
                foreach (var ch in Can)
                {
                    CharNumber.Add(ch, 0);
                }
                foreach (var item2 in item1.ItemSudokus)
                {
                    foreach (var item3 in item2.ItemCellSudoku.Can)
                    {
                        if (CharNumber.ContainsKey(item3))
                        {
                            int k=CharNumber[item3];
                            CharNumber[item3] = ++k;
                        }
                           
                    }
                }
                foreach (var item in CharNumber)
                {
                    if (item.Value==1)
                    {
                        ResetItem(item.Key);
                    }
                }
            }

            // TODO:что нужно еще  что если n с n-мя одинаковыми наборами то удалить эти наборы из других
           
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
