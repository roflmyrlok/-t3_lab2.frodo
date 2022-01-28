using System;

namespace t3_lab2
{
	public class ModernPoints
	{
		private int _column;
		private int _row;
		public bool Visited { get; set; }
		private string _value;
		public double Cost { set; get; } = Double.PositiveInfinity;
		public ModernPoints FatherPoint { get; set; }

		public void SetPoint(int column, int row, string value = "")
		{
			_column = column;
			_row = row;
			_value = value;
		}
		public int GetColumn()
		{
			return _column;
		}
		public int GetRow()
		{
			return _row;
		}
		public string GetValue()
		{
			return _value;
		}
	}
}

