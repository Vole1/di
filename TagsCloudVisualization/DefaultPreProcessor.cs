using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using NUnit.Framework.Internal.Execution;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	class DefaultPreProcessor : IPreProcessor
	{
		public string PreProcess(string readString)
		{
			return readString.ToLower();
		}
	}
}
