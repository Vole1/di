using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
	public class Spiral
	{
		private int Radius { get; set; }
		private const int DeltaRadius = 3;
		private int Angle { get; set; }
		private const int DeltaAngle = 5;
		public Spiral()
		{
			Radius = 0;
			Angle = 0;
		}

		private void UpdateCoordinates()
		{
			Angle += DeltaAngle;
			if (Angle >= 360)
			{
				Angle %= 360;
				Radius += DeltaRadius;
			}
		}

		public Tuple<int, int> GetNextCoordinates()
		{
			UpdateCoordinates();
			return new Tuple<int, int>(Radius, Angle);
		}
	}
}
