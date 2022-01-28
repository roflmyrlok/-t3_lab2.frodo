using System;
using System.Collections.Generic;

namespace t3_lab2
{
    public class MapPrinter
    {
        public void Print(string[,] maze, List<ModernPoints> mazeToPrint)
        {
            PrintTopLine();
            for (var patchedI = 0; patchedI <= mazeToPrint.Count - 1; patchedI++)
            {
                var current = mazeToPrint[patchedI];
                maze[current.GetColumn(),current.GetRow()] = "*";
            }

            maze[mazeToPrint[0].GetColumn(), mazeToPrint[0].GetRow()] = "A";
            maze[mazeToPrint[^1].GetColumn(), mazeToPrint[^1].GetRow()] = "B";
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