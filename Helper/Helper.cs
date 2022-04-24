using System;
using System.Collections.Generic;
using System.Text;

namespace badlife
{
    public static class Helper
    {
        public static char[][] GenerateWorld(int rows, int cols)
        {
            char[][] world = new char[rows][];

            Random generator = new Random();
            int number;

            for (int i = 0; i < rows; i++)
            {
                world[i] = new char[cols];

                for (int j = 0; j < cols; j++)
                {
                    number = generator.Next(2);
                    world[i][j] = number == 0 ? '_' : '*';
                }
            }

            return world;
        }

        public static void DisplayWord(Output world)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;

            //foreach (var cell in world.liveCells)
            //{
            //    Console.SetCursorPosition(cell.Row, cell.Col);

            //    Console.WriteLine(Cell.Icon);
            //}

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < world.MaxRows; i++)
            {
                sb.Clear();

                for (int j = 0; j < world.MaxCols; j++)
                {
                    if (world.LiveCells.ContainsKey($"{i}#{j}"))
                        sb.Append("*");
                    else
                        sb.Append(" ");
                }

                Console.WriteLine(sb.ToString());
            }
        }

        public static List<string> GetWorld(Output Output)
        {
            var world = new List<string>(Output.MaxRows);

            for (int i = 0; i < Output.MaxCols; i++)
            {
                var sb = new StringBuilder();

                for (int j = 0; j < Output.MaxCols; j++)
                {
                    if (Output.LiveCells.ContainsKey($"{i}#{j}"))
                    {
                        sb.Append("*");
                    }
                    else
                        sb.Append(" ");
                }
             
                world.Add(sb.ToString());
            }

            return world;
        }
    }
}
