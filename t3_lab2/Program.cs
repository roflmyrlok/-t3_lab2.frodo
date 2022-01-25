using System;
using System.Collections.Generic;
using System.Linq;
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
				Seed = 15
			});

			string[,] map = generator.Generate();
			var start = new Point(0, 0);
			var finish = new Point(88, 34);
			List<Point> path = GetShortestPath(map, start, finish);
			new MapPrinter().Print(map, path);
			
			
			List<Point> GetShortestPath(string[,] map, Point start, Point goal)
			{
				var path = new List<Point>();
				var distances = new Dictionary<Point, int>();
				var origins = new Dictionary<Point, Point>();
				var frontier = new Queue<Point>();
				distances.Add(new Point(0, 0), 0);
				origins.Add(new Point(0,0) ,new Point(0,0));
				while (frontier.Count != 0)
				{
					var current = frontier.Dequeue();;
					
					if (current.Equals(goal))
					{
						break;
					}
						
				}
				
				return path;
			}

			List<Point> FindPointsNearby(string[,] map, Point current)
			{
				List<Point> available = new List<Point>();
				if (map[current.Column - 1, current.Row] != "█")
				{
					available.Add(new Point(current.Column - 1, current.Row));
				}
				if (map[current.Column + 1, current.Row] != "█")
				{
					available.Add(new Point(current.Column + 1, current.Row));
				}
				if (map[current.Column, current.Row - 1] != "█")
				{
					available.Add(new Point(current.Column, current.Row + 1));
				}
				if (map[current.Column, current.Row + 1] != "█")
				{
					available.Add(new Point(current.Column, current.Row + 1));
				}
				return available;
			}
		}
	}
}