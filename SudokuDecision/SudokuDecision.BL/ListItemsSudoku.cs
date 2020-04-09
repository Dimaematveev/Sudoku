using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuDecision.BL
{
    public class ListItemsSudoku
    {
        
        ItemSudoku MainItem { get; set; }
        List<ItemSudoku> SubItems { get; set; }

        public List<object> NotCanBeUsed
        {
            get => SubItems.GroupBy(x => x.Item).Select(x => x.First().Item).ToList();
            
        }
        public ListItemsSudoku(ItemSudoku mainItem)
        {
            MainItem = mainItem;
            SubItems = new List<ItemSudoku>();
        }

        public void AddSub(List<ListSudoku> listSudokus)
        {
            foreach (var listSudoku in listSudokus)
            {
                int ind = listSudoku.ItemSudokus.FindIndex(x => x.Col == MainItem.Col && x.Row == MainItem.Row);
                if (ind != -1)
                {
                    SubItems.AddRange(listSudoku.ItemSudokus);
                    SubItems.Sort();
                    SubItems = SubItems.GroupBy(x => x.GetHashCode()).Select(x=>x.First()).ToList();
                    SubItems.RemoveAll(x => x.Col == MainItem.Col && x.Row == MainItem.Row);
                }
            }


        }

    }
}
