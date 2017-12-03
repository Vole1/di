using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Infrastructure
{
	interface IImageConfig
	{
		Brush GetWordBrush(string word);

		Color BackGroundColor { get; }

		Size ImageSize { get; }
		Font WordsFont { get; }

		float[] MinAndMaxFonts { get; }

		ImageFormat ImageFormat { get; }

		Point CloudCenter { get; }

	}
}
