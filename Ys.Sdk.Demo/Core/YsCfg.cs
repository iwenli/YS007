using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core
{
	/// <summary>
	/// 配置信息
	/// </summary>
	class YsCfg
	{
		public string AuthAddr => "https://auth.ys7.com";
		public string ApiUrl => "https://open.ys7.com/api/method";
		public string PlatformAddr => "https://open.ys7.com";
		public string PhoneNumber => "13581952323";
		public string AppKey => "d0cae3b600074f0aa1b9cf5f94e5dd3a";
		public string SecretKey => "a52923c6bcdb0789e6761cbbdb13289a";
		public string AccessToken { set; get; }
	}
}
