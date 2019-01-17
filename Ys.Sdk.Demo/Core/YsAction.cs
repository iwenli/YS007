using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Txooo.Extension;
using Ys.Sdk.Demo.Common;
using Ys.Sdk.Demo.Service.Entities.Ys;
using Ys.Sdk.Demo.Service.Entities.Ys.Response;

namespace Ys.Sdk.Demo.Core
{
	class YsAction
	{
		public static YsCfg Cfg { get; private set; } = new YsCfg();
		#region 事件
		/// <summary>
		/// 订阅
		/// </summary>
		public static event EventHandler<SubscribeEventArgs> OnSubscribe;
		#endregion

		#region Init
		/// <summary>
		/// 版本
		/// </summary>
		public static string Version => "1.0";

		/// <summary>
		/// 获取从缓存中获取access_token
		/// </summary>
		public static string AccessToken
		{
			get
			{
				if (string.IsNullOrEmpty(Cfg.AccessToken))
				{
					Cfg.AccessToken = GetAccessToken();
				}
				return Cfg.AccessToken;
			}
		}

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

		/// <summary>
		/// 获取accesstoken
		/// </summary>
		/// <returns></returns>
		public static string GetAccessToken()
		{
			IntPtr iMessage;
			int iLength;
			string _cmd = GetCommond("token");
			if (YsSDK.OpenSDK_HttpSendWithWait(Cfg.ApiUrl, _cmd, "", out iMessage, out iLength) == 0)
			{
				JObject result = (JObject)JsonConvert.DeserializeObject(Marshal.PtrToStringAnsi(iMessage, iLength));
				if (result["result"]["code"].ToString() == "200")
				{
					Cfg.AccessToken = result["result"]["data"]["accessToken"].ToString();
					Debug.WriteLine(Cfg.AccessToken);
				}
				else
				{
					Debug.WriteLine(result["result"]["code"].ToString());
				}
			}
			return Cfg.AccessToken;
		}
		/// <summary>
		/// 获取监控列表
		/// </summary>
		/// <returns></returns>
		public static List<CameraInfo> GetCameraList()
		{
			var _list = new List<CameraInfo>();
			var _page = 0;
			var _limit = 500;
			try
			{
				while (true)
				{
					IntPtr _hander;
					int _len;
					if (YsSDK.OpenSDK_Data_GetDevList(AccessToken, _page++, _limit, out _hander, out _len) == 0)
					{
						var _response = Marshal.PtrToStringAnsi(_hander);
						var _result = _response.FromJson<CameraListResponse>();
						_list.AddRange(_result?.CameraList);
						if (_result.Count < _limit)
						{
							break;
						}
					}
				}

			}
			catch (Exception ex)
			{
				LogService.AppendErrorLog(typeof(YsAction), "获取设备列表异常", ex);
			}
			return _list;
		}
		/// <summary>
		/// 获取监控列表
		/// </summary>
		/// <param name="deviceSerial"></param>
		/// <returns></returns>
		public static List<CameraInfo> GetCameraList(string deviceSerial)
		{
			var _list = new List<CameraInfo>();
			try
			{
				IntPtr _hander;
				int _len;
				if (YsSDK.OpenSDK_Data_GetDeviceInfo(AccessToken, deviceSerial, out _hander, out _len) == 0)
				{
					var _response = Marshal.PtrToStringAnsi(_hander);
					var _result = _response.FromJson<SingleDeviceCameraListResponse>();
					_list.AddRange(_result?.CameraList);
				}
			}
			catch (Exception ex)
			{
				LogService.AppendErrorLog(typeof(YsAction), $"获取设备[deviceSerial={deviceSerial}]列表异常", ex);
			}
			return _list;
		}

		/// <summary>
		/// 获取请求参数
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		static string GetCommond(string type)
		{
			var _method = string.Empty;
			type = type?.ToLower() ?? "";
			if (string.Equals("token", type, StringComparison.CurrentCultureIgnoreCase))
			{
				_method = "token/getAccessToken";
			}
			var _timestamp = DateTime.Now.ConvertToTimeStamp(false);
			var _result = new
			{
				Id = 100,
				System = new
				{
					Key = Cfg.AppKey,
					Sign = Txooo.Text.EncryptHelper.MD5($"phone:{Cfg.PhoneNumber},method:{_method},time:{_timestamp},secret:{Cfg.SecretKey}"),
					Time = _timestamp,
					Ver = Version
				},
				Method = _method,
				Params = new
				{
					Phone = Cfg.PhoneNumber
				}
			};
			return _result.ToJson();
		}
		#endregion

		#region Stop
		/// <summary>
		/// 停止播放（预览）
		/// </summary>
		/// <param name="SessionId"></param>
		/// <returns></returns>
		public static bool Stop(IntPtr SessionId)
		{
			CloseAllocion(SessionId);//每次播放结束会话
			return YsSDK.OpenSDK_StopRealPlay(SessionId, 0) == 0;
		}
		/// <summary>
		/// 播放视频（预览）
		/// </summary>
		/// <param name="playWnd"></param>
		/// <param name="sessionId"></param>
		/// <param name="cameraId"></param>
		/// <param name="level">清晰度，0流畅，1标清，2高清</param>
		/// <param name="safeKey">安全码</param>
		/// <returns></returns>
		public static bool Play(IntPtr playWnd, IntPtr sessionId, string cameraId, int level = 2, string safeKey = "MAIKE100")
		{
			return (YsSDK.OpenSDK_StartRealPlay(SessionId, playWnd, cameraId, AccessToken, level, safeKey, Cfg.AppKey, 0) == 0);
		}
		#endregion

		#region 会话
		public static IntPtr SessionId;
		public static int SessionIdLth;
		public static string SessionIdstr;
		/// <summary>
		/// 分配会话后，调用方法之后执行的回调函数
		/// </summary>
		static YsSDK.MsgHandler callBack = new YsSDK.MsgHandler(YsAction.HandlerWork);
		/// <summary>
		/// 分配会话
		/// </summary>
		/// <returns></returns>
		public static IntPtr AllocSession()
		{
			IntPtr userID = Marshal.StringToHGlobalAnsi(Cfg.UserId);
			bool flag = YsSDK.OpenSDK_AllocSession(callBack, userID, ref SessionId, ref SessionIdLth, false, uint.MaxValue) == 0;
			SessionIdstr = Marshal.PtrToStringAnsi(SessionId, SessionIdLth);
			return SessionId;
		}
		/// <summary>
		/// 回调函数
		/// </summary>
		/// <param name="sessionId"></param>
		/// <param name="msgType"></param>
		/// <param name="error"></param>
		/// <param name="info"></param>
		/// <param name="pUser"></param>
		/// <returns></returns>
		public static int HandlerWork(IntPtr sessionId, uint msgType, uint error, string info, IntPtr pUser)
		{
			OnSubscribe?.Invoke(null, new SubscribeEventArgs(sessionId, msgType, error, info, pUser));
			return 0;
			//switch (MsgType)
			//{
			//	case 20:
			//		JObject obj = (JObject)JsonConvert.DeserializeObject(Info);
			//		if (Error == 0)
			//		{
			//			//PlayMainWindow.jObjInfo = obj;
			//			//PlayMainWindow.isOpertion = 2;
			//		}
			//		break;
			//	case 3:// 播放开始
			//		break;
			//	case 4:// 播放终止
			//		break;
			//	case 5:// 播放结束，回放结束时会有此消息
			//		   //PlayMainWindow.mType = PlayMainWindow.MessageType.INS_PLAY_ARCHIVE_END;
			//		break;
			//	default:
			//		break;
			//}
			//return 0;
		}

		/// <summary>
		/// 结束会话
		/// </summary>
		/// <param name="sessionId"></param>
		static bool CloseAllocion(IntPtr sessionId)
		{
			return (YsSDK.OpenSDK_FreeSession(sessionId.ToString()) == 0);
		}
		#endregion
	}
}
