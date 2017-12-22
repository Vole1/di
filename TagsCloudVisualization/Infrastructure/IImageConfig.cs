using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Infrastructure
{
	public interface IImageConfig
	{
		Brush GetWordBrush(Size wordSize);

		Color BackgroundColor { get; }

		Size ImageSize { get; }
		Font WordsFont { get; }

		float MaxFontSize { get; }
		float MinFontSize { get; }

		ImageFormat ImageFormat { get; }

		Point CloudCenter { get; }

	}
}
