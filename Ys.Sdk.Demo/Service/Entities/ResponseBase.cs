using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Service.Entities
{
	/// <summary>
	/// 请求响应基类
	/// </summary>
	public class ResponseBase : ResponseBase<string>
	{
	}

	/// <summary>
	/// 请求响应基类
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ResponseBase<T>
	{
		/// <summary>
		/// 请求是否成功
		/// </summary>
		[JsonProperty("success")]
		public bool IsSuccess { set; get; }
		/// <summary>
		/// 错误信息
		/// </summary>

		[JsonProperty("msg")]
		public string Msg { set; get; }

		/// <summary>
		/// 业务码
		/// </summary>
		[JsonProperty("code")]
		public string Code { set; get; }

		/// <summary>
		/// 业务类型
		/// </summary>
		[JsonProperty("type")]
		public string Type { set; get; }

		/// <summary>
		/// 响应数据
		/// </summary>
		[JsonProperty("data")]
		public T Data { set; get; }

		/// <summary>
		/// 服务器响应请求时间
		/// </summary>
		[JsonProperty("ts")]
		public long ServerUnixTimestamp { set; get; }
	}
}
