using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;

namespace TagsCloudVisualization
{
	public struct Result<T>
	{
		public string Error { get; }
		private T Value { get; }

		public Result(string error, T value = default(T))
		{
			Error = error;
			Value = value;
		}

		public T GetValue()
		{
			if (!Error.IsNullOrEmpty())
				throw new ArgumentException("Invalid value. Check for operation success first.");
			return Value;
		}

		public bool IsSuccess => Error.IsNullOrEmpty();
	}
}
