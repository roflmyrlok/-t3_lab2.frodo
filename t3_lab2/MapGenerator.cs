using System;
using System.Collections.Generic;

namespace t3_lab2
{
    public class MapGenerator
    {
        private const string Wall = "█";

        private const string Space = " ";

        private readonly MapGeneratorOptions _options;

        private readonly Random _random;

        private string[,] _maze;

        public MapGenerator(MapGeneratorOptions options)
        {
            this._options = options;
            _random = new Random((int)(options.Seed == -1 ? DateTime.UtcNow.Ticks : options.Seed));
        }

        public string[,] Generate()
        {
            _maze = new string[_options.Width, _options.Height];
            if (_options.Type == MapType.Maze)
            {
                return GenerateMaze();
            }
            else
            {
                return null;
            }
        }

        private string[,] GenerateMaze()
        {
            for (var x = 0; x < _maze.GetLength(0); x++)
            {
                for (var y = 0; y < _maze.GetLength(1); y++)
                {
                    _maze[x, y] = (y % 2 == 1 || x % 2 == 1) ? Wall : Space;
                }
            }

            ExpandFrom(new Point(0, 0), new List<Point>());
            RemoveWalls(_options.Noise);

            if (_options.AddTraffic)
            {
                AddTraffic(_options.TrafficSeed);
            }

            return _maze;

            void ExpandFrom(Point point, List<Point> visited)
            {
                visited.Add(point);
                var neighbours = GetNeighbours(point.Column, point.Row, _maze).ToArray();
                Shuffle(_random, neighbours);
                foreach (var neighbour in neighbours)
                {
                    if (visited.Contains(neighbour))
                    {
                        continue;
                    }

                    RemoveWallBetween(point, neighbour);
                    ExpandFrom(neighbour, visited);
                }
            }

            void RemoveWallBetween(Point a, Point b)
            {
                _maze[(a.Column + b.Column) / 2, (a.Row + b.Row) / 2] = " ";
            }

            void Shuffle(Random rng, Point[] array)
            {
                var n = array.Length;
                while (n > 1)
                {
                    var k = rng.Next(n--);
                    (array[n], array[k]) = (array[k], array[n]);
                }
            }
        }

        private void RemoveWalls(float chance)
        {
            for (var y = 0; y < _maze.GetLength(1); y++)
            {
                for (var x = 0; x < _maze.GetLength(0); x++)
                {
                    if (_random.NextDouble() < chance && _maze[x, y] == Wall)
                    {
                        _maze[x, y] = " ";
                    }
                }
            }
        }

        private void AddTraffic(int seed)
        {
            var next = GetNextEmpty();
            var trafficRandom = new Random(_options.TrafficSeed);
            while (next.HasValue)
            {
                PaintTrafficDfs(next.Value, trafficRandom.Next(50, 130), trafficRandom.Next(1, 10));
                next = GetNextEmpty();
            }
            

            Point? GetNextEmpty()
            {
                for (var y = 0; y < _maze.GetLength(1); y++)
                {
                    for (var x = 0; x < _maze.GetLength(0); x++)
                    {
                        if (_maze[x, y] == " ")
                        {
                            return new Point(x, y);
                        }
                    }
                }

                return null;
            }
            
            void PaintTrafficDfs(Point point, int depth, int value)
            {
                var visited = new List<Point>();
                var stack = new Stack<Point>();
                stack.Push(point);
                while (stack.Count > 0 && depth > 0)
                {
                    var next = stack.Pop();
                    if (visited.Contains(next))
                    {
                        continue;
                    }

                    Visit(next);
                    var neighbours = GetNeighbours(next.Column, next.Row, _maze, 1, true);
                    foreach (var neighbour in neighbours)
                    {
                        stack.Push(neighbour);
                    }

                    void Visit(Point point)
                    {
                        _maze[point.Column, point.Row] = value.ToString();
                        depth -= 1;
                        visited.Add(point);
                    }
                }
            }
        }
        private List<Point> GetNeighbours(int column, int row, string[,] maze, int offset = 2,
            bool checkWalls = false)
        {
            var result = new List<Point>();
            TryAddWithOffset(offset, 0);
            TryAddWithOffset(-offset, 0);
            TryAddWithOffset(0, offset);
            TryAddWithOffset(0, -offset);
            return result;

            void TryAddWithOffset(int offsetX, int offsetY)
            {
                var newColumn = column + offsetX;
                var newRow = row + offsetY;
                if (newColumn >= 0 && newRow >= 0 && newColumn < maze.GetLength(0) && newRow < maze.GetLength(1))
                {
                    if (!checkWalls || maze[newColumn, newRow] == Space)
                    {
                        result.Add(new Point(newColumn, newRow));
                    }
                }
            }
        }
    }
}