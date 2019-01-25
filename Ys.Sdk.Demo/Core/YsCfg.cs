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
	public class YsCfg
	{
		public string AuthAddr => "https://auth.ys7.com";
		public string ApiUrl => "https://open.ys7.com/api/method";
		public string PlatformAddr => "https://open.ys7.com";
		public string PhoneNumber => "13581952323";
		public string AppKey => "d0cae3b600074f0aa1b9cf5f94e5dd3a";
		public string SecretKey => "a52923c6bcdb0789e6761cbbdb13289a";
		/// <summary>
		/// 自定义用户名，用guid防止重复
		/// </summary>
		public string UserId { set; get; } = "1b14cf56-b61d-43f5-a2c8-62b9626cbaaf";

		/// <summary>
		/// AccessToken
		/// </summary>
		public string AccessToken { set; get; }
		/// <summary>
		/// AccessToken 过期时间
		/// </summary>
		public long AccessTokenExpireTime { set; get; }
	}
}
