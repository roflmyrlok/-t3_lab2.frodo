using System;
using System.Collections.Generic;
using Kse.Algorithms.Samples;

namespace t3_lab2
{
	class Program
	{
		static void Main(string[] args)
		{
			var globalHeigh = 16;
			var globalWidth = 160;
			var generator = new MapGenerator(new MapGeneratorOptions()
			{
				Height = globalHeigh,
				Width = globalWidth,
				Seed = 14
			});

			string[,] map = generator.Generate();
			var start = new Point(0, 0);
			var finish = new Point(globalWidth - 2, globalHeigh - 2);
			List<Point> path = GetShortestPath(map, start, finish);
			new MapPrinter().Print(map, path);
			Console.Write("pudge");


			List<Point> GetShortestPath(string[,] map, Point start, Point goal)
			{
				var path = new List<Point>();
				path.Add(start);
				var lastPoint = goal;
				var distances = new Dictionary<Point, int>();
				var origins = new Dictionary<Point, Point>();
				var frontier = new Queue<Point>();
				var previous = new Point(0, 0);
				frontier.Enqueue(start);
				distances.Add(start, -1);
				while (frontier.Count != 0)
				{
					var current = frontier.Dequeue();
					var available = FindPointsNearby(map, current);
					foreach (var point in available)
					{
						if (!origins.ContainsValue(current))
						{
							frontier.Enqueue(point);
						}
					}
					if (!origins.ContainsKey(current))
					{
						if (distances.ContainsKey(current))
						{
							if (distances[current] < distances[previous] + 1)
							{
								distances[current] = distances[previous] + 1;
								origins.Add(current, previous);
								lastPoint = current;
								previous = current;
							}
							else
							{
								origins.Add(current, previous);
								lastPoint = current;
								previous = current;
							}
						}
						else
						{
							distances.Add(current, distances[previous] + 1);
							origins.Add(current, previous);
							lastPoint = current;
							previous = current;
						}
						
					}
					if (current.Equals(goal))
					{
						lastPoint = goal;
						break;
					}
				}

				var lenOf = distances[lastPoint];
				for (var i = 0; i != lenOf  - 1; i++)
				{
				path.Add(origins[lastPoint]);
				lastPoint = origins[lastPoint];
				}
				path.Add(goal);
				return path;
			}

		List<Point> FindPointsNearby(string[,] map, Point current)
			{
				List<Point> available = new List<Point>();
				if (current.Column - 1 >= 0 && current.Column -1 <= globalWidth - 1 && map[current.Column - 1, current.Row] != "█" && current.Row <= globalWidth - 1)
				{
					available.Add(new Point(current.Column - 1, current.Row));
				}

				if (current.Column + 1 >= 0 && current.Column + 1 <= globalWidth - 1 && current.Row <= globalWidth - 1)
				{
					if (map[current.Column + 1, current.Row] != "█")
					{
						available.Add(new Point(current.Column + 1, current.Row));
					}
				}
				if ( current.Row - 1 >= 0 && current.Row - 1 <= globalHeigh - 1 && current.Column <= globalWidth - 1 && map[current.Column, current.Row - 1] != "█" ) 
				{
					available.Add(new Point(current.Column, current.Row - 1));
				}
				if (current.Row + 1 >= 0 && current.Row + 1 <= globalHeigh - 1 && current.Column <= globalWidth - 1 && map[current.Column, current.Row + 1] != "█")
				{
					available.Add(new Point(current.Column, current.Row + 1));
				}
				return available;
			}
		}
	}
}