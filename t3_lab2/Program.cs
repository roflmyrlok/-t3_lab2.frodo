using System;
using System.Collections.Generic;
using Kse.Algorithms.Samples;

namespace t3_lab2
{
	class Program
	{
		static void Main(string[] args)
		{
			var generator = new MapGenerator(new MapGeneratorOptions()
			{
				Height = 35,
				Width = 90,
				Seed = 14
			});

			string[,] map = generator.Generate();
			var start = new Point(0, 0);
			var finish = new Point(88, 34);
			List<Point> path = GetShortestPath(map, start, finish);
			new MapPrinter().Print(map, path);
			Console.Write("pudge");
			
			
			List<Point> GetShortestPath(string[,] map, Point start, Point goal)
			{
				var path = new List<Point>();
				var lastPoint = goal;
				var distances = new Dictionary<Point, int>();
				var origins = new Dictionary<Point, Point>();
				var frontier = new Queue<Point>();
				var previous = new Point(0, 0);
				var available1 = FindPointsNearby(map, previous);
				foreach (var point in available1)
				{
					frontier.Enqueue(point);
				}
				distances.Add(previous, 0);
				origins.Add(previous,previous);
				while (frontier.Count != 0)
				{
					var current = frontier.Dequeue();
					if (current.Row != 35 && current.Column != 91)
					{
						var available = FindPointsNearby(map, current);
						foreach (var point in available)
						{
							if (!origins.ContainsKey(current))
							{
								frontier.Enqueue(point);
							}
						}
						if (!origins.ContainsKey(current))
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

				
				for (var i = 0; i != distances[lastPoint] - 1; i++)
				{
					path.Add(origins[lastPoint]);
					lastPoint = origins[lastPoint];
				}
				
				return path;
			}

			List<Point> FindPointsNearby(string[,] map, Point current)
			{
				List<Point> available = new List<Point>();
				if (current.Column - 1 >= 0 && current.Column -1 <= 89 && map[current.Column - 1, current.Row] != "█" )
				{
					available.Add(new Point(current.Column - 1, current.Row));
				}
				if (current.Column + 1 >= 0 && current.Column + 1 <= 89 && map[current.Column + 1, current.Row] != "█")
				{
					available.Add(new Point(current.Column + 1, current.Row));
				}
				if ( current.Row - 1 >= 0 && current.Row - 1 <= 34 && map[current.Column, current.Row - 1] != "█") 
				{
					available.Add(new Point(current.Column, current.Row + 1));
				}
				if (current.Row + 1 >= 0 && current.Row + 1 <= 34 && map[current.Column, current.Row + 1] != "█")
				{
					available.Add(new Point(current.Column, current.Row + 1));
				}
				return available;
			}
		}
	}
}