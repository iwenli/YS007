using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Network
{
	/// <summary>
	/// 基于System.Net.Http.HttpClient的网络层扩展，进一步抽象以便于后期提供功能
	/// </summary>
	public class NetClient : HttpClient
	{
		/// <summary>
		/// 用特定的处理程序初始化 NetClient 类的新实例。
		/// </summary>
		/// <param name="handler">用于发送请求的使用的 HTTP 处理程序堆栈。</param>
		public NetClient(HttpMessageHandler handler) : base(handler)
		{

		}

		/// <summary>
		/// 用特定的处理程序初始化 NetClient 类的新实例。
		/// </summary>
		public NetClient() : base()
		{
		}
	}
}
