using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	class CircularCloudLayouter : ILayouter
	{
		private List<Rectangle> InsertedRectangles { get; }
		private Spiral Spiral { get; }

		private Point Center { get; }


		public CircularCloudLayouter(IImageConfig imageConfig)
		{
			Center = imageConfig.CloudCenter;
			Spiral = new Spiral();
			InsertedRectangles = new List<Rectangle>();
		}

		public IEnumerable<Rectangle> PutRectangles(IEnumerable<Size> sizes)
		{
			foreach (var currentRectangle in sizes)
				yield return PutNextRectangle(currentRectangle);
		}

		private Rectangle PutNextRectangle(Size rectangleSize)
		{
			while (true)
			{
				var position = GetRectangleCoordinates(rectangleSize);
				var result = new Rectangle(position, rectangleSize);
				if (CheckForIntersectionWithPreviousRectangles(result))
					continue;

				RegisterRectangle(result);
				return result;
			}
		}

		private Point GetRectangleCoordinates(Size rectangleSize)
		{
			if (InsertedRectangles.Count == 0)
				return GetCoordinatesForRectangleInTheCenter(rectangleSize);
			return GetCoordinatesForRectangle();
		}

		private Point GetCoordinatesForRectangleInTheCenter(Size rectangleSize)
		{
			return new Point((int)Math.Round(Center.X - rectangleSize.Width / 2.0),
				(int)Math.Round(Center.Y - rectangleSize.Height / 2.0));
		}

		private Point GetCoordinatesForRectangle()
		{
			var newPolarCoordinates = Spiral.GetNextCoordinates();
			return PolarToCortesianCoordinates(newPolarCoordinates.Item1, newPolarCoordinates.Item2);
		}

		private bool CheckForIntersectionWithPreviousRectangles(Rectangle rectangle)
		{
			foreach (var previousRectangle in InsertedRectangles)
				if (rectangle.IntersectsWith(previousRectangle))
					return true;
			return false;
		}

		private void RegisterRectangle(Rectangle rectangle)
		{
			InsertedRectangles.Add(rectangle);
		}

		private Point PolarToCortesianCoordinates(int radius, int angle)
		{
			var x = Center.X + Math.Round(radius * Math.Cos(CommonUsefulMethods.DegreeesToRadians(angle)));
			var y = Center.Y - Math.Round(radius * Math.Sin(CommonUsefulMethods.DegreeesToRadians(angle)));
			return new Point((int)x, (int)y);
		}
	}
}