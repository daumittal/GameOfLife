using System.Collections.Concurrent;
using System.Collections.Generic;

namespace badlife
{
    public class Output
    {
        internal Output(long maxRows, long maxCols, ConcurrentDictionary<string, Cell> activeNeighbours)
        {
            MaxRows=maxRows;
            MaxCols=maxCols;

            LiveCells = new Dictionary<string, (long, long)>();
            foreach (var cell in activeNeighbours)
            {
                LiveCells.Add(cell.Key, (cell.Value.Row, cell.Value.Col));
            }
        }
        public long MaxRows { get; private set; }
        public long MaxCols { get; private set; }
        public Dictionary<string,(long, long)> LiveCells { get; private set; }

    }
}
