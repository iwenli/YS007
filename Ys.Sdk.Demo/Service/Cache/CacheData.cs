using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.Sdk.Demo.Service.Entities;
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
		/// <summary>
		/// 设备信息
		/// </summary>
		public List<DeviceInfo> DeviceList { set; get; }
		/// <summary>
		/// 门店列表
		/// </summary>
		public List<StoreInfo> StoreList { get; set; }

		/// <summary>
		/// 萤石云安全key
		/// </summary>
		public string SafeKye => LoginInfo?.ComId == 549250 ? "SUMAO100" : "MAIKE100";

		///// <summary>
		///// 监控列表
		///// </summary>
		//public List<CameraInfo> CameraList { get; set; }

		public CacheData()
		{ 
		}
	}
}
