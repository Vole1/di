using System;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	class DefaultImageConfig : IImageConfig
	{
		private Func<Size, Brush> GetWordBrushFunc { get; }
		public Brush GetWordBrush(Size wordSize) => GetWordBrushFunc(wordSize);
		public Color BackgroundColor { get; }
		public Size ImageSize { get; }
		public Font WordsFont { get; }
		public float MaxFontSize { get; }
		public float MinFontSize { get; }
		public ImageFormat ImageFormat { get; }
		public Point CloudCenter => new Point(ImageSize.Width / 2, ImageSize.Height / 2);

		public DefaultImageConfig()
		{
			GetWordBrushFunc = word => new SolidBrush(Color.Chartreuse);
			BackgroundColor = Color.Black;
			ImageSize = new Size(1000, 800);
			WordsFont = new Font(FontFamily.GenericMonospace, 30);
			MaxFontSize = 10f;
			MinFontSize = 20f;
			ImageFormat = ImageFormat.Bmp;
		}

		public DefaultImageConfig(Func<Size, Brush> getWordBrushFunc, Color backgroundColor,
			Size imageSize, Font wordsFont, float maxFontSize, float minFontSize, ImageFormat imageFormat)
		{
			GetWordBrushFunc = getWordBrushFunc;
			BackgroundColor = backgroundColor;
			ImageSize = imageSize;
			WordsFont = wordsFont;
			MaxFontSize = maxFontSize;
			MinFontSize = minFontSize;
			ImageFormat = imageFormat;
		}
	}
}