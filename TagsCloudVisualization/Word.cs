using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	class Word
	{
		public string Value { get; }
		public Font Font { get; }
		public Brush Brush { get; }

		public Word(string value, Font font, Brush brush)
		{
			Value = value;
			Font = font;
			Brush = brush;
		}
	}
}
