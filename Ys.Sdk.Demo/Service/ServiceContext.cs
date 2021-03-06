﻿using Ys.Sdk.Demo.Service.Cache;
using Ys.Sdk.Demo.Service.Http;

namespace Ys.Sdk.Demo.Service
{
	/// <summary>
	/// 服务上下文
	/// </summary>
	public class ServiceContext
	{
		public ServiceContext()
		{
			Session = new Session();
			CacheContext = CacheContext.Instance;
			CacheContext.ServiceContext = this;
			PassportService = new PassportService(this);
			DeviceService = new DeviceService(this);
			StoreService = new StoreService(this);
		}

		/// <summary>
		/// 获得当前的会话状态
		/// </summary>
		public Session Session { get; private set; }

		/// <summary>
		/// 缓存上下文
		/// </summary>
		public CacheContext CacheContext { get; private set; }

		/// <summary>
		/// 授权服务
		/// </summary>
		public PassportService PassportService { get; private set; }
		/// <summary>
		/// 门店服务
		/// </summary>
		public StoreService StoreService { get; set; }
		/// <summary>
		/// 设备服务
		/// </summary>
		public DeviceService DeviceService { get; set; }
	}
}