using System.Collections.Generic;
using Kse.Algorithms.Samples;

namespace t3_lab2
{
	class Program
	{
		static void Main(string[] args)
		{
			var globalHeight = 16;
			var globalWidth = 160;
			var generator = new MapGenerator(new MapGeneratorOptions()
			{
				Height = globalHeight,
				Width = globalWidth
			});

			string[,] map = generator.Generate();
			var toStart = new Point(0, 0);
			var finish = new Point(globalWidth - 2, globalHeight - 2);
			List<Point> path = GetShortestPath(map, toStart, finish);
			new MapPrinter().Print(map, path);
			
			List<Point> GetShortestPath(string[,] localMap, Point start, Point goal)
			{
				var localPath = new List<Point> {start};
				var lastPoint = goal;
				var distances = new Dictionary<Point, int>();
				var origins = new Dictionary<Point, Point>();
				var frontier = new Queue<Point>();
				frontier.Enqueue(start);
				distances.Add(start, -1);
				while (frontier.Count != 0)
				{
					var current = frontier.Dequeue();
					var available = FindPointsNearby(localMap, current);
					foreach (var point in available)
					{
						if (!origins.ContainsKey(point))
						{
							frontier.Enqueue(point);
							origins.Add(point, current);
							if (!distances.ContainsKey(point))
							{
								distances.Add(point, distances[current] + 1);
							}
							
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
				localPath.Add(origins[lastPoint]);
				lastPoint = origins[lastPoint];
				}
				localPath.Add(goal);
				return localPath;
			}

		List<Point> FindPointsNearby(string[,] localMap2, Point current)
			{
				List<Point> available = new List<Point>();
				if (current.Column - 1 >= 0 && current.Column - 1 <= globalWidth - 1 && current.Row <= globalWidth - 1)
				{
					if (localMap2[current.Column - 1, current.Row] != "█")
					{
						available.Add(new Point(current.Column - 1, current.Row));
					}
				}

				if (current.Column + 1 >= 0 && current.Column + 1 <= globalWidth - 1 && current.Row <= globalWidth - 1)
				{
					if (localMap2[current.Column + 1, current.Row] != "█")
					{
						available.Add(new Point(current.Column + 1, current.Row));
					}
				}
				if ( current.Row - 1 >= 0 && current.Row - 1 <= globalHeight - 1 && current.Column <= globalWidth - 1) 
				{
					if (localMap2[current.Column, current.Row - 1] != "█")
					{
						available.Add(new Point(current.Column, current.Row - 1));
					}
				}

				if (current.Row + 1 >= 0 && current.Row + 1 <= globalHeight - 1 && current.Column <= globalWidth - 1)
				{
					if (localMap2[current.Column, current.Row + 1] != "█")
					{
						available.Add(new Point(current.Column, current.Row + 1));
					}
				}
				return available;
			}
		}
	}
}