using FSLib.App.SimpleUpdater;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Txooo.Extension;
using Ys.Sdk.Demo.Core;
using Ys.Sdk.Demo.Service;

namespace Ys.Sdk.Demo
{

	static class App
	{
		static System.Threading.Mutex _mutex;

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (CanRun())
			{
				JsonConvertSettings();
				Application.Run(new MainForm());
			}
		}

		/// <summary>
		/// 全局序列化设置
		/// </summary>
		static void JsonConvertSettings()
		{
			Newtonsoft.Json.JsonSerializerSettings setting = new Newtonsoft.Json.JsonSerializerSettings();
			Newtonsoft.Json.JsonConvert.DefaultSettings = new Func<Newtonsoft.Json.JsonSerializerSettings>(() =>
			{
				//日期类型默认格式化处理
				setting.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
				setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";

				//空值处理
				setting.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

				//对 JSON 数据使用混合大小写。驼峰式,但是是javascript 首字母小写形式
				setting.ContractResolver = new CamelCasePropertyNamesContractResolver();
				return setting;
			});
		}
		/// <summary>
		/// 是否可以启动窗体
		/// 暂无运行的窗体  &&  没有需要更新的版本  && 网络
		/// </summary>
		/// <returns></returns>
		static bool CanRun()
		{
			bool canRun;
			Attribute guid_attr = Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(GuidAttribute));
			string guid = ((GuidAttribute)guid_attr).Value;
			_mutex = new System.Threading.Mutex(true, guid, out canRun);
			if (!canRun)
			{
				MessageBox.Show("已经在运行了!", AppInfo.AssemblyTitle,
										MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				canRun = IsConnect();
				if (!canRun)
				{
					MessageBox.Show("网络异常，请检查网络是否连接!", AppInfo.AssemblyTitle,
															MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					canRun = YsAction.InitSdk();
					if (!canRun)
					{
						MessageBox.Show("监控提供商链接失败，请联系软件开发者!", AppInfo.AssemblyTitle,
															MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						canRun = CheckUpdateTask().Result == null;
					}
				}
			}
			return canRun;
		}

		#region 联网校验
		private const int INTERNET_CONNECTION_MODEM = 1;
		private const int INTERNET_CONNECTION_LAN = 2;
		[DllImport("winInet.dll ")]
		private static extern bool InternetGetConnectedState(
		ref int dwFlag,
		int dwReserved
		);
		/// <summary>
		/// 是否联网
		/// </summary>
		static bool IsConnect()
		{
			Int32 dwFlag = new int();
			bool result = true;
			if (!InternetGetConnectedState(ref dwFlag, 0))
			{
				LogService.AppendInfoLog(typeof(App), "网络连接已断开...");
				result = false;
			}
			else if ((dwFlag & INTERNET_CONNECTION_MODEM) != 0)
			{
				LogService.AppendInfoLog(typeof(App), "网络已连接[调治解调器]...");
			}
			else if ((dwFlag & INTERNET_CONNECTION_LAN) != 0)
			{
				LogService.AppendInfoLog(typeof(App), "网络已连接[网卡]...");
			}
			return result;
		}
		#endregion

		#region 检测更新
		/// <summary>
		/// 检测并更新
		/// </summary>
		public static async Task<Version> CheckUpdateTask()
		{
			Version newVersion = null;
			//任务模式检测更新
			var updater = Updater.CreateUpdaterInstance(@"http://iwenli.org/soft/7518/{0}", "update_c.xml");
			try
			{
				newVersion = await updater.CheckUpdateTask();
			}
			catch (Exception ex)
			{
				LogService.AppendErrorLog(typeof(App), "升级程序异常", ex);
			}
			return newVersion;
		}
		#endregion
	}
}
