using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace t3_lab2
{
	class Program
	{
		static void Main(string[] args)
		{
			var globalHeight = 20;
			var globalWidth = 20;
			var globalTraffic = false;
			var generator = new MapGenerator(new MapGeneratorOptions(.05f)
			{
				Height = globalHeight,
				Width = globalWidth,
				Seed = 14,
				AddTraffic = globalTraffic,
				TrafficSeed = 131
			});
			string[,] testMap = generator.Generate();
			//string[,] testMap ={};

		Map map = new Map();
			map.SetMap(testMap, globalWidth, globalHeight);
			if (globalTraffic)
			{
				List<ModernPoint> path = GetShortestPathByDeycstra(map);
				new MapPrinter().Print(testMap, path,true);
			}
			if (!globalTraffic)
			{
				List<ModernPoint> path = GetShortestPathByAStar(map);
				new MapPrinter().Print(testMap, path);
			}
			
			
			List<ModernPoint> GetShortestPathByDeycstra(Map map)
			{
				var start = map.ListOfList[0][0];
				var goal = map.ListOfList[globalHeight - 2][globalWidth - 2];
				var localPath = new List<ModernPoint>();
				var frontier = new Queue<ModernPoint>();
				frontier.Enqueue(start);
				start.FatherPoint = null;
				start.Cost = 1 / (60 - 6 * (double.Parse(start.GetValue()) - 1));
				start.Visited = true;
				while (frontier.Count != 0)
				{
					var current = frontier.Dequeue();
					var available = map.GetPointsNearby(current);
					foreach (var point in available)
					{
						if (point.FatherPoint == current) continue;
						if (point.Cost > current.Cost + 1 / (60 - 6 * (double.Parse(current.GetValue()) - 1)))
						{
							
							point.Cost = current.Cost + 1 / (60 - 6 * (double.Parse(current.GetValue()) - 1));
							point.FatherPoint = current;
							point.Visited = true;
							if (!point.Visited) continue;
							frontier.Enqueue(point);
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
			List<ModernPoint> GetShortestPathByAStar(Map map)
			{
				var start = map.ListOfList[0][0];
				var goal = map.ListOfList[globalHeight - 2][globalWidth - 2];
				var localPath = new List<ModernPoint>();
				var frontier = new Queue<ModernPoint>();
				var coolerFrontier = new Queue<ModernPoint>();
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