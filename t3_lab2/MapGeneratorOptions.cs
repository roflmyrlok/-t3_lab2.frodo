namespace t3_lab2
{
	public class MapGeneratorOptions
	{
		public MapGeneratorOptions(float noise)
		{
			Noise = noise;
		}

		public int Width { get; init; }

		public int Height { get; init; }

		public MapType Type { get; } = MapType.Maze; 

		public float Noise { get; }

		public int Seed { get; set; } = -1;
        
		public bool AddTraffic { get; set; }
        
		public int TrafficSeed { get; set; }
	}
    
    
	public enum MapType
	{
		Maze,
		Grid
	}
}