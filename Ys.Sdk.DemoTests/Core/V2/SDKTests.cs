using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ys.Sdk.Demo.Core.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core.V2.Tests
{
	[TestClass()]
	public class SDKTests
	{
		public static YsCfg Cfg { get; private set; } = new YsCfg();


		[TestMethod()]
		public void InitLibTest()
		{
			var _result = SDK.OpenSDK_InitLib(Cfg.AuthAddr, Cfg.PlatformAddr, Cfg.AppKey);
			Assert.IsTrue(_result == 0);
		}

		[TestMethod()]
		public void InitTest()
		{
			var _result = SDK.OpenSDK_Init(Cfg.AppKey);
			Assert.IsTrue(_result == 0);
		}

		[TestMethod()]
		public void FiniLibTest()
		{
			SDK.OpenSDK_InitLib(Cfg.AuthAddr, Cfg.PlatformAddr, Cfg.AppKey);
			var _result = SDK.OpenSDK_FiniLib();
			Assert.IsTrue(_result == 0);
		}

		[TestMethod()]
		public void OpenSDK_SetConfigInfoTest()
		{
			SDK.OpenSDK_SetConfigInfo(ConfigKeyType.CONFIG_DATA_UTF8, 1);
			Assert.IsTrue(true);
		}

		[TestMethod()]
		public void OpenSDK_SetPlatformAddrTest()
		{
			SDK.OpenSDK_SetPlatformAddr(Cfg.PlatformAddr + "/test");
			Assert.IsTrue(true);
		}

		[TestMethod()]
		public void OpenSDK_SetAppIDTest()
		{
			var _result = SDK.OpenSDK_SetAppID(Cfg.AppKey);
			Assert.IsTrue(_result == 0);
		}

		[TestMethod()]
		public void OpenSDK_SetAccessTokenTest()
		{
			var _result = SDK.OpenSDK_SetAccessToken("");
			Assert.IsTrue(_result == 0);
		}
	}
}