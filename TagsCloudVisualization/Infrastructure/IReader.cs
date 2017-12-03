﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Infrastructure
{
	public interface IReader
	{
		string[] ReadFile();

		void SetFileName(string fileName);
	}
}
