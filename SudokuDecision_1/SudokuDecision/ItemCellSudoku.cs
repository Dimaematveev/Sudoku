using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuDecision
{
    public class ItemCellSudoku
    {
        public char Item;
        public List<char> Can;

        public override string ToString()
        {
            return $"_{Item}";
        }
    }

}
