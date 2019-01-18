using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core.Entities.Response
{
	public abstract class Base : Base<string>
	{
	}

	public abstract class Base<T> where T : class
	{
		/// <summary>
		/// 是否成功
		/// </summary>
		public virtual bool IsSuccess { get; set; } = false;
		/// <summary>
		/// 错误消息
		/// </summary>
		public virtual string ErrorMsg { get; set; }

		/// <summary>
		/// 返回的消息
		/// </summary>
		[JsonProperty]
		protected virtual string Msg { set; get; }

		/// <summary>
		/// 业务码
		/// </summary>
		[JsonProperty]
		protected virtual uint Code { set; get; }

		/// <summary>
		/// data数据
		/// </summary>
		public virtual T Data { set; get; }
	}
}
