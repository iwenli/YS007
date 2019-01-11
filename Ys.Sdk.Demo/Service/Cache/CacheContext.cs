using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.Sdk.Demo.Common;
using Ys.Sdk.Demo.Core;

namespace Ys.Sdk.Demo.Service.Cache
{
	/// <summary>
	/// 缓存上下文
	/// </summary>
	public class CacheContext : ContextBase
	{
		/// <summary>
		/// 服务上下文
		/// </summary>
		public ServiceContext ServiceContext { get; set; }

		/// <summary>
		/// 缓存数据
		/// </summary>
		public CacheData Data { get; private set; }

		#region 单例模式

		protected static CacheContext _instance;
		static readonly object _lockObject = new object();

		public static CacheContext Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new CacheContext();
							_instance.Init();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		private CacheContext()
		{

		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init()
		{
			CacheName = $"{nameof(CacheContext)}.dat";
			DataRoot = "Data";
			var root = Environment.CurrentDirectory;
			DataRoot = Path.Combine(root, DataRoot);
			Directory.CreateDirectory(DataRoot);
			Data = LoadData<CacheData>(CacheName);
		}

		public async Task InitAsync()
		{
			await Task.Run(() =>
			{
				Init();
			});
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public void Save()
		{
			SaveData(Data, CacheName);
		}

		/// <summary>
		/// 更新缓存
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync()
		{
			return await Task.Run(async () =>
			{
				//更新缓存
				Data.LoginInfo = ServiceContext.Session.LoginInfo;

				var storeResult = await ServiceContext.StoreService.GetList();
				if (storeResult.IsSuccess)
				{
					Data.StoreList = storeResult.Data;
				}
				if (Data.StoreList?.Count > 0)
				{
					var deviceResult = await ServiceContext.DeviceService.GetList();
					Data.DeviceList = deviceResult.Data;
				}
				//更新监控
				Data.CameraList = YsAction.GetCameraList();
				Data.LastUpdateTime = DateTime.Now;
				Save();
				return true;
			});
		}
	}
}
