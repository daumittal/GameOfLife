using System;
using System.Collections.Generic;
using System.Linq;
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

            if (world.MaxRows > 100 || world.MaxCols > 100)
            {
                DisplayLargeWord(world);
            }
            else
            {
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
        }

        /// <summary>
        /// It is difficult to draw large population in small board. What I am doing is only displaying live cell rows 
        /// which brings cells closer to each other and does not show spread properly however if we ignore the display part then speed is fantastic
        /// </summary>
        /// <param name="world"></param>
        public static void DisplayLargeWord(Output world)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;

            StringBuilder sb = new StringBuilder();

            long previousRowNo = 0;
            foreach (var rowNo in world.LiveCells.Values.OrderBy(x => x.Item1).Select(x => x.Item1).Distinct())
            {
                if (rowNo > previousRowNo + 1)
                    Console.WriteLine();

                sb.Clear();

                for (int j = 0; j < world.MaxCols; j++)
                {
                    if (world.LiveCells.ContainsKey($"{rowNo}#{j}"))
                        sb.Append("*");
                    else
                        sb.Append(" ");
                }

                Console.WriteLine(sb.ToString());
                previousRowNo = rowNo;
            }
        }

        public static List<string> GetWorld(Output Output)
        {
            var world = new List<string>();

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
