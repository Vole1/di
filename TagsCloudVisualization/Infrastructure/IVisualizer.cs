using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Infrastructure
{
	interface IVisualizer
	{
		void Visualize(string imageName);

		Bitmap GetImage();
	}
}
