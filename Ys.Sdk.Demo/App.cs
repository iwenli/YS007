using FSLib.App.SimpleUpdater;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
			try
			{
				//设置应用程序处理异常方式：ThreadException处理
				Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
				Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
				AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				if (CanRun())
				{
					JsonConvertSettings();
					Application.Run(new MainForm());
				}
			}
			catch (Exception ex)
			{
				var _errorMsg = GetExceptionMsg(ex, string.Empty);
				LogService.AppendErrorLog(typeof(App), "应用程序的主入口点异常", ex);
				MessageBox.Show(_errorMsg, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#region 序列化 启动检测
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
		#endregion

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
			var updater = Updater.CreateUpdaterInstance(AppInfo.UpdateUrl, "update_c.xml");
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

		#region 异常处理
		/// <summary>
		/// 处理UI线程异常
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			var _errorMsg = GetExceptionMsg(e.Exception, e.ToString());
			LogService.AppendErrorLog(typeof(App), "处理UI线程异常", e.Exception);
			MessageBox.Show(_errorMsg, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		/// <summary>
		/// 处理非UI线程异常
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var _errorMsg = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
			LogService.AppendErrorLog(typeof(App), "处理非UI线程异常", (Exception)e.ExceptionObject);
			MessageBox.Show(_errorMsg, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		/// <summary>
		/// 生成自定义异常消息
		/// </summary>
		/// <param name="ex">异常对象</param>
		/// <param name="backStr">备用异常消息：当ex为null时有效</param>
		/// <returns>异常字符串文本</returns>
		static string GetExceptionMsg(Exception ex, string backStr)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("****************************异常文本****************************");
			sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
			if (ex != null)
			{
				sb.AppendLine("【异常类型】：" + ex.GetType().Name);
				sb.AppendLine("【异常信息】：" + ex.Message);
				sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
			}
			else
			{
				sb.AppendLine("【未处理异常】：" + backStr);
			}
			sb.AppendLine("***************************************************************");
			return sb.ToString();
		}
		#endregion
	}
}
