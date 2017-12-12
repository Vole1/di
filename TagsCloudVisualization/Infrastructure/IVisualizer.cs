using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Infrastructure
{
	public interface IVisualizer
	{
		void Visualize(IEnumerable<Word> words, IEnumerable<Rectangle> rectangles);

		//Bitmap GetImage(IEnumerable<Word> words, IEnumerable<Rectangle> rectangles);
	}
}
