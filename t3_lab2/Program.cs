using System;
using System.Collections.Generic;

namespace t3_lab2
{
	class Program
	{
		static void Main(string[] args)
		{
			var globalHeight = 160;
			var globalWidth = 160;
			var globalTraffic = true;
			var generator = new MapGenerator(new MapGeneratorOptions(.01f)
			{
				Height = globalHeight,
				Width = globalWidth,
				Seed = 14,
				AddTraffic = globalTraffic,
				TrafficSeed = 12345
			});
			string[,] txtMap = generator.Generate();
			ClassMap map = new ClassMap();
			map.SetMap(txtMap, globalWidth, globalHeight);
			if (globalTraffic)
			{
				List<ModernPoints> path = GetShortestPathByDeycstra(map);
				new MapPrinter().Print(txtMap, path);
			}
			if (!globalTraffic)
			{
				List<ModernPoints> path = GetShortestPathByAStar(map);
				new MapPrinter().Print(txtMap, path);
			}
			
			
			List<ModernPoints> GetShortestPathByDeycstra(ClassMap map)
			{
				var start = map.ListOfList[0][0];
				var goal = map.ListOfList[globalHeight - 2][globalWidth - 2];
				var localPath = new List<ModernPoints>();
				var frontier = new Queue<ModernPoints>();
				frontier.Enqueue(start);
				start.FatherPoint = start;
				start.Cost = 1 / (60 - 6 * (double.Parse(start.GetValue()) - 1));
				start.Visited = true;
				while (frontier.Count != 0)
				{
					var current = frontier.Dequeue();
					var available = map.GetPointsNearby(current);
					foreach (var point in available)
					{
						if (!point.Visited)
						{
							if (point.Cost > current.Cost + 1 / (60 - 6 * (double.Parse(current.GetValue()) - 1)))
							{
								frontier.Enqueue(point);
								point.Cost = current.Cost + 1 / (60 - 6 * (double.Parse(current.GetValue()) - 1));
								point.FatherPoint = current;
								point.Visited = true;
							}
						}
					}
					if (current.Equals(goal))
					{
						var time = (current.Cost);
						Console.WriteLine("time: ");
						Console.WriteLine(time);
						localPath.Add(goal);
						break;
					}
				}
				while (localPath[^1] != start)
				{
					localPath.Add(localPath[^1].FatherPoint);
				}
				return localPath;
			}
			List<ModernPoints> GetShortestPathByAStar(ClassMap map)
			{
				var start = map.ListOfList[0][0];
				var goal = map.ListOfList[globalHeight - 2][globalWidth - 2];
				var localPath = new List<ModernPoints>();
				var frontier = new Queue<ModernPoints>();
				var coolerFrontier = new Queue<ModernPoints>();
				frontier.Enqueue(start);
				start.FatherPoint = start;
				start.Cost = 0;
				start.Visited = true;
				while (true)
				{
					var current = coolerFrontier.Count != 0 ? coolerFrontier.Dequeue() : frontier.Dequeue();
					var available = map.GetPointsNearby(current, true);
					var nOfTurnsLocal = available.Count;
					for (var i = 0; i != nOfTurnsLocal ;i++)
					{
						var point = available.Dequeue();
						if (point.Visited) continue;
						if (!(point.Cost > current.Cost + 1)) continue;
						if (i == 0 && !point.Visited)
						{
							coolerFrontier.Enqueue(point);
						}
						else
						{
							frontier.Enqueue(point);
						}
						point.Cost = current.Cost + 1;
						point.FatherPoint = current;
						point.Visited = true;
					}
					if (current.Equals(goal))
					{
						var time = (current.Cost / 60);
						Console.WriteLine("time: ");
						Console.WriteLine(time);
						localPath.Add(goal);
						break;
					}
				}
				while (localPath[^1] != start)
				{
					localPath.Add(localPath[^1].FatherPoint);
				}
				return localPath;
			}
		}
	}
}