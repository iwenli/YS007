using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Common
{
	/// <summary>
	/// 异常扩展
	/// </summary>
	public static class ExceptionExtension
	{
		/// <summary>
		/// 获取内部异常
		/// </summary>
		/// <param name="exception"></param>
		/// <returns></returns>
		public static Exception GetInnerException(this Exception exception)
		{
			if (exception.InnerException != null) {
				return exception.InnerException.GetInnerException();
			}
			return exception;
		}
	}
}
