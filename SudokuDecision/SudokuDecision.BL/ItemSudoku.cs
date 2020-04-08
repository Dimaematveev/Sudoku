using System.Data;

namespace SudokuDecision.BL
{
    public class ItemSudoku
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
    }
}
