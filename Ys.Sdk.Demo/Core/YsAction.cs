using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core
{
	class YsAction
	{
		public static YsCfg Cfg => new YsCfg();

		/// <summary>
		/// 初始化SDK
		/// </summary>
		/// <returns></returns>
		public static bool InitSdk()
		{
			return (YsSDK.OpenSDK_InitLib(Cfg.AuthAddr, Cfg.PlatformAddr, Cfg.AppKey) == 0);
		}
		/// <summary>
		/// 释放Skd
		/// </summary>
		/// <returns></returns>
		public static bool DisposeSdk()
		{
			return (YsSDK.OpenSDK_FiniLib() == 0);
		}
	}
}
