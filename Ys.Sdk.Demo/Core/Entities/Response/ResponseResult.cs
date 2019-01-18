using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core.Entities.Response
{
	/// <summary>
	/// Result封装
	/// </summary>
	public class ResponseResult<T> where T : class
	{
		/// <summary>
		/// 请求结果
		/// </summary>
		public T Result { get; set; }
	}
}
