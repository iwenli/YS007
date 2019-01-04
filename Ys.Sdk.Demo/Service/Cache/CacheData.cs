using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.Sdk.Demo.Service.Entities.User;

namespace Ys.Sdk.Demo.Service.Cache
{
	/// <summary>
	/// 缓存数据
	/// </summary>
	public class CacheData
	{
		/// <summary>
		/// 最后一次更新缓存时间
		/// </summary>
		public DateTime LastUpdateTime { get; set; }
		/// <summary>
		/// 登陆信息
		/// </summary>
		public LoginInfo LoginInfo { get; set; }

		public CacheData()
		{
		}
	}
}
