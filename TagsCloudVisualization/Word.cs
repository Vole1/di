using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	public class Word
	{
		public string Value { get; }
		public Font Font { get; }
		public Brush Brush { get; }
		public Size WordDrawingSize { get; }

		public Word(string value, Font font, Brush brush, Size wordDrawingSize)
		{
			Value = value;
			Font = font;
			Brush = brush;
			WordDrawingSize = wordDrawingSize;
		}
	}
}
