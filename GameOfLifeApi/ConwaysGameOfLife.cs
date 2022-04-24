using System.Collections.Concurrent;

namespace badlife
{
    public class ConwaysGameOfLife : Rules
    {
        public ConwaysGameOfLife(string fileName, int playCount = int.MaxValue, int delayInSeconds = 0) : base(fileName, playCount, delayInSeconds)
        {
        }
        public ConwaysGameOfLife(char[][] world, int playCount = int.MaxValue, int delayInSeconds = 0) : base(world, playCount, delayInSeconds)
        {
        }

        internal override void ApplyRules(ConcurrentDictionary<string, Cell> activeNeighbours)
        {
            Cell.StateChanged = false;

            var keys = activeNeighbours.Keys;

            foreach (var key in keys)
            {
                if (activeNeighbours.TryGetValue(key, out Cell cell))
                {
                    cell.State = GetNewState(cell.State, cell.Neighbours);

                    if (cell.State == 0)
                        activeNeighbours.TryRemove(key, out _); 
                }
            }
        }

        public static int GetNewState(int currentState, int neighbours)
        {
            if (neighbours == 3) //cell continues or born
                return 1;
            else if (neighbours == 2 && currentState == 1) //cell continues
                return currentState;
            else
                return 0; //cell dies
        }
    }
}
