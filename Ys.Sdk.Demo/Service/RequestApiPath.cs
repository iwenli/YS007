using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.Sdk.Demo.Common;

namespace Ys.Sdk.Demo.Service
{
	/// <summary>
	/// 请求api列表
	/// </summary>
	public class RequestApiPath
	{
		#region 基础设置
		/// <summary>
		/// 请求域
		/// </summary>
		public static string Domain => ".txooo.com";

		/// <summary>
		/// 是否生产环境
		/// </summary>
		public static bool IsProducter => true;
		/// <summary>
		/// 是否Https
		/// </summary>
		public static bool IsHttps => Convert.ToBoolean(AppConfiguration.GetItem("Https") ?? "true");
		/// <summary>
		/// 网关请求域
		/// </summary>
		public static string GateWayHost => $"{(IsHttps ? "https" : "http") }://gateway{Domain}";

		/// <summary>
		/// api请求域
		/// </summary>
		public static string ApiHost => $"{(IsHttps ? "https" : "http") }://api{Domain}";
		/// <summary>
		/// 网关请求全路径
		/// </summary>
		public static string GateWayFullPath => $"{GateWayHost}{(IsProducter ? "" : "/test")}";

		/// <summary>
		/// api请求全路径
		/// </summary>
		public static string ApiFullPath => $"{ApiHost}{(IsProducter ? "" : "/test")}";
		#endregion

		#region 登陆授权
		/// <summary>
		/// 验证手机号能否登录（是否为CBO）
		/// </summary>
		protected static string IfMobileCanLogin_V1 => $"{GateWayFullPath}/gzy/account/ifmobilecanlogin";
		/// <summary>
		/// 发送验证码
		/// </summary>
		protected static string SendMobileCode_V1 => $"{ApiFullPath}/api/market/account/sendmobilecode";
		/// <summary>
		/// 登录
		/// </summary>
		protected static string Login_V1 => $"{GateWayFullPath}/gzy/account/cbooremployeelogin";
		/// <summary>
		/// 企业列表
		/// </summary>
		protected static string GetCompanyList_V1 => $"{GateWayFullPath}/gzy/account/getcompanylist";
		#endregion

		#region 门店
		/// <summary>
		/// 门店列表
		/// </summary>
		protected static string GetStoreList => $"{GateWayFullPath}/v1/store.api/getlist";
		#endregion

		#region 设备
		/// <summary>
		/// 设备列表
		/// </summary>
		protected static string GetDeviceList => $"{GateWayFullPath}/v1/device.api/getlist";
		#endregion
	}
}
