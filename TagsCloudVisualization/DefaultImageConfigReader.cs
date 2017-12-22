using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Castle.Core.Internal;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	class DefaultImageConfigReader : IConfigReader
	{

		private string[] ValidImageFormats { get; }

		public DefaultImageConfigReader()
		{
			ValidImageFormats = new[] {"bmp", "jpeg", "png"};
		}

		public Result<IImageConfig> GetConfigFile(string fileName)
		{
			var b = File.Exists("config.txt");
			if (!File.Exists(fileName))
				return new Result<IImageConfig>("Configureation file hasn't been found. Please, validate it's path.");
			var readStrings = new List<string>();
			using (var strReader = new StreamReader(fileName))
			{
				while (!strReader.EndOfStream)
					readStrings.Add(strReader.ReadLine());
			}
			return ParseConfig(readStrings);
		}

		private Result<IImageConfig> ParseConfig(List<string> readStrings)
		{
			var brushColorResult = GetBrushColorByName(readStrings);
			if (!brushColorResult.IsSuccess)
				return new Result<IImageConfig>(brushColorResult.Error);
			Brush GetBrushFunc(Size wordSize) => new SolidBrush(brushColorResult.GetValue());

			var backgroundColorResult = GetBackgroundColorByName(readStrings);
			if (!backgroundColorResult.IsSuccess)
				return new Result<IImageConfig>(backgroundColorResult.Error);

			var imageSizeResult = GetImageSize(readStrings);
			if (!imageSizeResult.IsSuccess)
				return new Result<IImageConfig>(imageSizeResult.Error);

			var wordsFontResult = GetWordsFont(readStrings);
			if (!wordsFontResult.IsSuccess)
				return new Result<IImageConfig>(wordsFontResult.Error);

			var fontSizesResult = GetFontSizes(readStrings);
			if (!fontSizesResult.IsSuccess)
				return new Result<IImageConfig>(fontSizesResult.Error);

			var imageFormatResult = GetImageFormat(readStrings);
			if (!imageFormatResult.IsSuccess)
				return new Result<IImageConfig>(imageFormatResult.Error);

			var defaultImageConfig = new DefaultImageConfig(GetBrushFunc, backgroundColorResult.GetValue(), 
				imageSizeResult.GetValue(), wordsFontResult.GetValue(), fontSizesResult.GetValue()[0], fontSizesResult.GetValue()[1],
				imageFormatResult.GetValue());

			return new Result<IImageConfig>(null, defaultImageConfig);
		}

		private Result<Color> GetBrushColorByName(IEnumerable<string> readStrings)
		{
			return GetSpecifiedColorByName(readStrings, "Brush");
		}

		private Result<Color> GetBackgroundColorByName(IEnumerable<string> readStrings)
		{
			return GetSpecifiedColorByName(readStrings, "Background");
		}

		private Result<Size> GetImageSize(IEnumerable<string> readStrings)
		{
			string strToWorkWith = GetValidString(readStrings,
				readString => Regex.IsMatch(readString, "Size", RegexOptions.IgnoreCase) && !Regex.IsMatch(readString, "Font", RegexOptions.IgnoreCase));

			if (strToWorkWith == null)
				return new Result<Size>("Couldn't find required parameters in configuration file: image size");
			var matchGroups = Regex.Match(strToWorkWith, @"(\d+) ?[,;] ?(\d+)").Groups;
			var width = int.Parse(matchGroups[1].Value);
			var height = int.Parse(matchGroups[2].Value);
			return new Result<Size>(null, new Size(width, height));
		}

		private Result<Font> GetWordsFont(IEnumerable<string> readStrings)
		{
			string strToWorkWith = GetValidString(readStrings, readString => Regex.IsMatch(readString, "Font", RegexOptions.IgnoreCase));
			
			if (strToWorkWith == null)
				return new Result<Font>("Couldn't find required parameters in configuration file: font desctirption");
			var fontFamiliesNames = FontFamily.Families.Select(x => x.Name).ToArray();

			var searchResult = strToWorkWith.Split(' ').Where(x => fontFamiliesNames.Contains(Regex.Replace(x, @"\W", "")));
			if (!searchResult.Any())
				return new Result<Font>("Couldn't find required parameters in configuration file: font desctirption");
			return new Result<Font>(null, new Font(searchResult.First(), 1));
		}

		private Result<float[]> GetFontSizes(IEnumerable<string> readStrings)
		{
			string strToWorkWith = GetValidString(readStrings,
				readString => Regex.IsMatch(readString, "Size", RegexOptions.IgnoreCase) && Regex.IsMatch(readString, "Font", RegexOptions.IgnoreCase));
			
			if (strToWorkWith == null)
				return new Result<float[]>("Couldn't find required parameters in configuration file: font min and max sizes");
			var matchGroups = Regex.Match(strToWorkWith, @"(\d+) ?[,;:-] ?(\d+)").Groups;
			var first = float.Parse(matchGroups[1].Value);
			var second = float.Parse(matchGroups[2].Value);
			return new Result<float[]>(null, new []{Math.Min(first, second), Math.Max(first, second)});
		}

		private Result<ImageFormat> GetImageFormat(IEnumerable<string> readStrings)
		{
			string strToWorkWith = GetValidString(readStrings, readSting => Regex.IsMatch(readSting, "Format", RegexOptions.IgnoreCase));
			if (strToWorkWith == null)
				return new Result<ImageFormat>("Couldn't find required parameters in configuration file: font min and max sizes");
			var stringImageFormats = strToWorkWith.Split().Where(x => ValidImageFormats.Contains(Regex.Replace(x.ToLower(), @"\W", "")));
			if (!stringImageFormats.Any())
				return new Result<ImageFormat>("Couldn't find required parameters in configuration file: font min and max sizes");
			var imageFormatConverter = new ImageFormatConverter();

			return new Result<ImageFormat>(null, (ImageFormat)imageFormatConverter.ConvertFromString(stringImageFormats.First()));
		}

		private Result<Color> GetSpecifiedColorByName(IEnumerable<string> readStrings, string keyWord)
		{
			var strToWorkWith = GetValidString(readStrings, readString => Regex.IsMatch(readString, keyWord, RegexOptions.IgnoreCase));
			
			if (strToWorkWith == null)
				return new Result<Color>($"Couldn't find required parameters in configuration file: {keyWord} color");
			var name = strToWorkWith.Split(' ').First(x => Color.FromName(x).ToArgb() != 0);
			return new Result<Color>(null, Color.FromName(name));
		}

		private string GetValidString(IEnumerable<string> readStrings, Func<string, bool> validationRule)
		{
			foreach (var readString in readStrings)
				if (validationRule(readString))
					return readString;
			return null;
		}
	}
}
