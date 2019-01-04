using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Service.Entities.User
{
	/// <summary>
	/// 登陆信息
	/// </summary>
	public class LoginInfo
	{ 

		/// <summary>
		/// 用户id
		/// </summary>
		[JsonProperty("user_id")]
		public int UserId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("token")]
		public string Token { get; set; }

		/// <summary>
		/// 加盟商id
		/// </summary>
		[JsonProperty("brand_id")]
		public int BrandId { get; set; }
		/// <summary>
		/// 运营商id
		/// </summary>
		[JsonProperty("comid")]
		public int ComId { get; set; }

		/// <summary>
		/// 登录手机号
		/// </summary>
		[JsonProperty("mobile")]
		public string Mobile { get; set; }

		/// <summary>
		/// 记住密码
		/// </summary>
		public bool RememberPwd { get; set; } = true;

		//public string GetBrandToken { }

		/// <summary>
		/// 最后一次登陆时间
		/// </summary>
		public DateTime LastLoginTime { set; get; } = DateTime.Now;

		/// <summary>
		/// 当前账号下加盟商列表
		/// </summary>
		public List<BrandInfo> BrandList { set; get; } = new List<BrandInfo>();
	}
}
