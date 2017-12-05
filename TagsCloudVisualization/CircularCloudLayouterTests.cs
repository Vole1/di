using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	[TestFixture]
	class CircularCloudLayouterTests
	{
		private CircularCloudLayouter ccl;
		private Rectangle[] rectangles;
		private Point center;

		[SetUp]
		public void SetUp()
		{
			var words = new[] { "hi", "heRe", "There", "mE", "in", "other", "TREE", "beautiful", "welL" };
			center = new Point(900, 500);
			ccl = new CircularCloudLayouter(new DefaultPreProcessor(new TestReader{Input = words }, new DefaultImageConfig()), new DefaultImageConfig());

			rectangles = ccl.PutRectangles().ToArray();
		}
		class TestReader : IReader
		{
			public string[] Input { get; set; }

			public void SetFileName(string fileName)
			{
			}

			public string[] ReadFile()
			{
				return Input;
			}
		}

		[TearDown]
		public void TearDown()
		{
			var testResult = TestContext.CurrentContext.Result.Outcome;

			if (Equals(testResult, ResultState.Failure) ||
			    Equals(testResult == ResultState.Error))
			{
				var bmp = new Bitmap(1920, 1080);
				var graphics = Graphics.FromImage(bmp);
				graphics.Clear(Color.Black);
				foreach (var rectangle in rectangles)
					graphics.DrawRectangle(new Pen(Color.Chartreuse), rectangle);

				var path = AppDomain.CurrentDomain.BaseDirectory + "\\Test " + TestContext.CurrentContext.Test.MethodName +
				           " Failure.bmp";
				bmp.Save(path);

				Console.WriteLine("Tag cloud visualization saved to file {0}", path);
				TestContext.Out.NewLine = string.Format("Tag cloud visualization saved to file {0}", path);
			}
		}

		[TestCase(30)]
		public void Rectangles_ShouldBePositionedInAShapeOfACircle(int radiusDelta)
		{
			var averageRadius = GetAverageReadiusFrom4Sides();

			var rectangleSquareSum = rectangles.Select(r => r.Width * r.Height).Sum();

			var mincircleSquare = (int) Math.Round(Math.PI * Math.Pow(averageRadius - radiusDelta, 2));
			var maxcircleSquare = (int) Math.Round(Math.PI * Math.Pow(averageRadius + radiusDelta, 2));

			rectangleSquareSum.Should().BeInRange(mincircleSquare, maxcircleSquare);
		}

		[Test]
		public void Rectangles_ShouldNotCross_WithEachOther()
		{
			for (var i = 0; i < rectangles.Length; i++)
			for (var j = 0; j < rectangles.Length; j++)
			{
				if (i == j)
					continue;
				rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
			}
		}

		[TestCase(10)]
		public void Rectangles_ShouldBePositionedPossiblyClose(int maxDistace)
		{
			for (var i = 0; i < rectangles.Length; i++)
			{
				var interSectionFlag = false;
				var currentRect = rectangles[i];
				var rectangleForCheck = new Rectangle(currentRect.X - maxDistace, currentRect.Y - maxDistace,
					currentRect.Width + maxDistace * 5, currentRect.Height + maxDistace * 2);
				for (int j = 0; j < rectangles.Length; j++)
				{
					if (i == j)
						continue;
					if (rectangleForCheck.IntersectsWith(rectangles[j]))
						interSectionFlag = true;
				}
				interSectionFlag.Should().BeTrue();
			}
		}

		private double GetAverageReadiusFrom4Sides()
		{
			var radiusesFromDifferentParties = new List<int>();
			var bottom = rectangles.Select(r => r.Bottom).Min();
			var top = rectangles.Select(r => r.Top).Max();
			var left = rectangles.Select(r => r.Left).Min();
			var right = rectangles.Select(r => r.Right).Max();

			radiusesFromDifferentParties.Add(center.Y - bottom);
			radiusesFromDifferentParties.Add(top - center.Y);
			radiusesFromDifferentParties.Add(center.X - left);
			radiusesFromDifferentParties.Add(right - center.X);

			return radiusesFromDifferentParties.Average();
		}
	}
}