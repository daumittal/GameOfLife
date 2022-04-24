using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace badlife
{
    public abstract class Rules : IGameOfLife
    {
        #region Private Variables

        protected int _maxRow, _maxCol, _delayInSeconds;
        protected int _playCount = int.MaxValue; //means keep evolving until state is not changed
        ConcurrentDictionary<string, Cell> _liveCells = new ConcurrentDictionary<string, Cell>();

        #endregion

        #region Constructors
        public Rules(string fileName, int playCount, int delayInSeconds)
        {
            _playCount = playCount;
            _delayInSeconds = delayInSeconds;

            ExtractFile(fileName);
        }
        public Rules(char[][] world, int playCount, int delayInSeconds)
        {
            _playCount = playCount;
            _delayInSeconds = delayInSeconds;

            ExtractFile(world);
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Not returning the entire world two dimentional matrix which can consume too much memory
        /// Returning list of live cells with coordinates that client application can use to print at right location on the screen
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Output> Evolve()
        {
            for (int i = 0; i < _playCount; i++)
            {
                NextGeneration();
                yield return GeNextWorld();

                if (!Cell.StateChanged)
                    break;

                if (_delayInSeconds > 0)
                    Thread.Sleep(_delayInSeconds * 1000);
            }
        }

        #endregion

        #region Public Methods
        internal abstract void ApplyRules(ConcurrentDictionary<string, Cell> activeNeighbours);

        #endregion

        #region Private Methods

        /// <summary>
        /// ASSUMPTION: Board is infinite while # of live cells is finite
        /// By reading line by line and considering only live cells, this can support extremly large world with sparse values.
        /// Time Complexity: O(N) where N are no of cells on the board
        /// Space Comlexity: O(# of live cells)
        /// </summary>
        /// <param name="fileName"></param>
        private void ExtractFile(string fileName)
        {
            try
            {
                _maxRow = 0;
                _maxCol = 0;

                if (string.IsNullOrEmpty(fileName))
                    throw new ArgumentNullException("Invalid file name provided");

                Parallel.ForEach(File.ReadLines(fileName), (line, _, lineNumber) =>
                {
                    _maxRow++;
                    ExtractFile(lineNumber, line.ToCharArray());
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void ExtractFile(char[][] world)
        {
            try
            {
                _maxCol = 0;
                for (_maxRow = 0; _maxRow < world.ToList().Count(); _maxRow++)
                {
                    ExtractFile(_maxRow, world[_maxRow]);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ExtractFile(long row, char[] line)
        {
            try
            {
                for (int col = 0; col < line.Length; col++)
                {
                    if (line[col] == '*')
                    {
                        var cell = new Cell(row, col, 1, 0);
                        _liveCells.TryAdd(cell.Key, cell);
                    }
                }

                if (_maxCol == 0)
                    _maxCol = line.Length;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Collect all 8 neighbours only for live cells and apply game of life rules
        /// Time Complexity: 8 * O(# of live cells)
        /// Space Complexity: 9 * O(# of live cells)
        /// </summary>
        void NextGeneration()
        {
            var keys = _liveCells.Keys;

            Parallel.ForEach(keys, key =>
            {
                int neighbours = 0;

                _liveCells.TryGetValue(key, out Cell liveCell);

                liveCell.Neighbours = 0;

                HashSet<string> visitedCells = new HashSet<string>();

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        //int r = liveCell.Row + i < 0 ? _maxRow - 1 : (liveCell.Row + i == _maxRow ? 0 : liveCell.Row + i);
                        //int c = liveCell.Col + j < 0 ? _maxCol - 1 : (liveCell.Col + j == _maxCol ? 0 : liveCell.Col + j);

                        //Handing corner cases
                        long r = (_maxRow + liveCell.Row + i) % _maxRow;
                        long c = (_maxCol + liveCell.Col + j) % _maxCol;

                        string newKey = $"{r}#{c}";

                        if (i == 0 && j == 0)
                            continue;

                        if (visitedCells.Contains(newKey)) //to ignore duplicates on boundry
                            continue;

                        visitedCells.Add(newKey);

                        if (_liveCells.TryGetValue(newKey, out Cell cell) && cell.State == 1)
                            neighbours += 1;
                        else
                        {
                            _liveCells.AddOrUpdate(newKey, new Cell(r, c, 0, 1), (key, value) => value.Update(1));

                        }
                    }
                }

                liveCell.Update(neighbours);
                visitedCells.Clear();
            });

            ApplyRules(_liveCells);
        }

        /// <summary>
        /// This method saves lot of space by not storing the entire world in two dimentional matrics. It is generated at run time
        /// </summary>
        /// <returns></returns>
        private Output GeNextWorld()
        {
            return new Output(_maxRow, _maxCol, _liveCells);
        }

        #endregion
    }
}
