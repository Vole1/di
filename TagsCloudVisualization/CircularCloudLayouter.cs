using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	class CircularCloudLayouter : ICircularCloudLayouter
	{
		private List<Rectangle> InsertedRectangles { get; }
		private Spiral Spiral { get; }

		private string[] wordsIn;
		private IPreProcessor PreProcessor { get; }
		private IImageConfig ImageConfig { get; }

		private Point Center { get; }

		public string[] GetWords() => wordsIn;
		public CircularCloudLayouter(IPreProcessor preProcessor, IImageConfig imageConfig)
		{
			PreProcessor = preProcessor;
			Center = imageConfig.CloudCenter;
			ImageConfig = imageConfig;
			Spiral = new Spiral();
			InsertedRectangles = new List<Rectangle>();
			wordsIn = PreProcessor.PreProcess();
		}

		public IEnumerable<Rectangle> PutRectangles()
		{
			using (var graphics = Graphics.FromImage(new Bitmap(1, 1)))
			{
				for (int i = 0; i < wordsIn.Length; i++)
				{
					float fontSize = ImageConfig.MinAndMaxFonts[1] -
								   (ImageConfig.MinAndMaxFonts[1] - ImageConfig.MinAndMaxFonts[0]) / (wordsIn.Length) * i;
					var strSize = GetReactangleSize(wordsIn[i], fontSize, ImageConfig.WordsFont, graphics);
					var rectSize = new Size(strSize.Width+1, strSize.Height+1);
					yield return PutNextRectangle(rectSize);
				}
			}
		}

		private Size GetReactangleSize(string word, float fontSize, Font wordFont, Graphics graphics)
		{
			var currentFont = new Font(wordFont.FontFamily, fontSize);
			var size = graphics.MeasureString(word, currentFont);
			return new Size((int)Math.Round(size.Width), (int)Math.Round(size.Height));
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
			return new Point((int)Math.Round(Center.X - rectangleSize.Width / 2.0), (int)Math.Round(Center.Y - rectangleSize.Height / 2.0));
		}

		private Point GetCoordinatesForRectangle()
		{
			Spiral.UpdateCoordinates();
			return PolarToCortesianCoordinates(Spiral.Radius, Spiral.Angle);

		}

		private bool CheckForIntersectionWithPreviousRectangles(Rectangle rectangle)
		{
			foreach (var previousRectangle in InsertedRectangles)
			{
				if (rectangle.IntersectsWith(previousRectangle))
					return true;
			}
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
