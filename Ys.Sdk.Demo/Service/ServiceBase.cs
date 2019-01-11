using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.Sdk.Demo.Network;

namespace Ys.Sdk.Demo.Service
{
	/// <summary>								
	/// 通用的服务基类，提供一些额外的基础服务
	/// </summary>
	public abstract class ServiceBase : RequestApiPath
	{
		protected ServiceBase(ServiceContext context)
		{
			ServiceContext = context;
		}

		/// <summary>
		/// 获得当前的服务上下文
		/// </summary>
		public ServiceContext ServiceContext { get; private set; }

		/// <summary>
		/// 获得当前关联的会话
		/// </summary>
		public Session Session => ServiceContext.Session;

		/// <summary>
		/// 获得当前关联的网络访问对象
		/// </summary>
		public HttpSignClient NetClient => Session.NetClient;

		/// <summary>
		/// 获得当前是否已经登录
		/// </summary>
		public bool IsLogined => Session.IsLogined;


		/// <summary>
		/// 获取请求token  
		/// brandid|userid|token|storeid|deviceid|memberid
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		protected string GetBrandToken(long storeId = 0, long deviceId = 0, long memberId = 0)
		{
			if (Session.IsLogined)
			{
				var user = Session.LoginInfo;
				return $"{user.BrandId}|{user.UserId}|{user.Token}|{storeId}|{deviceId}|{memberId}";
			}
			return "";
		}
	}
}
