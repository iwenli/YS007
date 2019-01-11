﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Service.Entities.Ys.Response
{
	/// <summary>
	/// 请求响应基类
	/// </summary>
	public class ResponseBase
	{
		/// <summary>
		/// 错误信息
		/// </summary>

		[JsonProperty("resultMsg")]
		public string Msg { set; get; }

		/// <summary>
		/// 业务码
		/// </summary>
		[JsonProperty("resultCode")]
		public string Code { set; get; }

		/// <summary>
		/// 数量
		/// </summary>
		[JsonProperty("count")]
		public int Count { set; get; }
	}
}