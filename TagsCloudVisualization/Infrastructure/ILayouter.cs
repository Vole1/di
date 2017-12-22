﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Infrastructure
{
	public interface ILayouter
	{
		Result<List<Rectangle>> PutRectangles(IEnumerable<Size> sizes);
	}
}