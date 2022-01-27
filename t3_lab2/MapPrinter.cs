namespace Kse.Algorithms.Samples
{
    using System;
    using System.Collections.Generic;

    public class MapPrinter
    {
        public void Print(string[,] maze, List<Point> pathedMaze)
        {
            PrintTopLine();
            for (var pathedI = 0; pathedI <= pathedMaze.Count - 1; pathedI++)
            {
                var newX = pathedMaze[pathedI].Column;
                var newY = pathedMaze[pathedI].Row;
                maze[newX, newY] = "*";
            }

            maze[pathedMaze[0].Column, pathedMaze[0].Row] = "A";
            maze[pathedMaze[pathedMaze.Count - 1].Column, pathedMaze[pathedMaze.Count - 1].Row] = "B";
            for (var row = 0; row < maze.GetLength(1); row++)
            {
                Console.Write($"{row}\t");
                for (var column = 0; column < maze.GetLength(0); column++)
                {
                    Console.Write(maze[column, row]);
                }

                Console.WriteLine();
            }
            

            void PrintTopLine()
            {
                Console.Write($" \t");
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    Console.Write(i % 10 == 0? i / 10 : " ");
                }
    
                Console.Write($"\n \t");
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    Console.Write(i % 10);
                }
    
                Console.WriteLine("\n");
                
            }
        }
        
        
    }
}