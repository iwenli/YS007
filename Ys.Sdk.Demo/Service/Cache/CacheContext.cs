using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.Sdk.Demo.Common;

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
			return await Task.Run<bool>(() =>
			{
				return true;
			});

			//Data.ProductClassList.Clear();
			//Data.AreaList.Clear();
			//Data.ProductClassList.AddRange(await ServiceContext.ClassDataService.GetAllProductClass());
			//Data.AreaList.AddRange(await ServiceContext.AreaDataService.LoadAreaDatasAsync(1));
			//var list = Data.AreaList.ToList();
			////Parallel.For(0, list.Count, async (i) =>
			//for (int i = 0; i < list.Count; i++)
			//{
			//	if (!new int[] { 110000, 120000, 310000, 500000 }.Contains(list[i].region_code))
			//	{
			//		try
			//		{
			//			Data.AreaList.AddRange(await ServiceContext.AreaDataService.LoadAreaDatasAsync(list[i].region_id));
			//		}
			//		catch (Exception ex)
			//		{
			//			throw ex;
			//		}

			//	}
			//}
			////更新商品来源集合缓存
			//var maxId = 0L;
			//if (Data.ProductSourceTxoooList.Count > 0)
			//{
			//	maxId = Data.ProductSourceTxoooList.Last().Id;
			//}
			//try
			//{
			//	var productSourceList = ServiceContext.ProductService.GetProductsSourceList(maxId);
			//	if (productSourceList != null)
			//	{
			//		Data.ProductSourceTxoooList.AddRange(productSourceList);
			//	}
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			//Data.LastUpdateTime = DateTime.Now;
			//Data.IsLine = !ApiList.IsTest;
			//Save();
			//return true;
		}
	}
}
