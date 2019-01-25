using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Txooo.Extension;
using Txooo.Extension.Extension;
using Ys.Sdk.Demo.Core.Entities.Response;
using Ys.Sdk.Demo.Core.Entities.V2;
using Ys.Sdk.Demo.Core.V2.Event;
using static Ys.Sdk.Demo.Core.V2.SDK;

namespace Ys.Sdk.Demo.Core.V2
{
	/// <summary>
	/// 萤石云SKD适配器
	/// 根据文档 http://open.ys7.com/doc/zh/book/pc/pc-sdk.html 编写
	/// </summary>
	public class SDKAdapter
	{

		/// <summary>
		/// 版本
		/// </summary>
		public static string Version => "V4.2";

		/// <summary>
		/// 配置文件
		/// </summary>
		static YsCfg _cfg = new YsCfg();

		#region 初始化
		/// <summary>
		/// 开始SDK初始化
		/// </summary>
		/// <returns></returns>
		public static bool Init()
		{
			return SDK.OpenSDK_InitLib(_cfg.AuthAddr, _cfg.PlatformAddr, _cfg.AppKey) == 0;
		}

		public static void Dispose()
		{
			//释放会话
			//反初始化SDK
			SDK.OpenSDK_FiniLib();
		}
		#endregion

		#region 认证流程
		/// <summary>
		/// 获取从缓存中获取access_token
		/// </summary>
		static string AccessToken
		{
			get
			{
				if (string.IsNullOrEmpty(_cfg.AccessToken) || DateTime.Now.ConvertToTimeStamp() >= _cfg.AccessTokenExpireTime)
				{
					return GetAccessTokenFromRemote();
				}
				return _cfg.AccessToken;
			}
		}

		/// <summary>
		/// 远程获取
		/// </summary>
		/// <returns></returns>
		static string GetAccessTokenFromRemote()
		{
			IntPtr response;
			int length;
			if (SDK.OpenSDK_HttpSendWithWait($"{"https:"}//open.ys7.com/api/lapp/token/get?appKey={_cfg.AppKey}&appSecret={_cfg.SecretKey}", ""
				, "", out response, out length) == 0)
			{
				var _rmsg = Marshal.PtrToStringAnsi(response);
				var _result = _rmsg.FromJson<TokenResponse>();
				if (!_result.IsSuccess)
				{
					throw new Exception(_result.ErrorMsg);
				}
				_cfg.AccessToken = _result.Data.AccessToken;
				_cfg.AccessTokenExpireTime = _result.Data.ExpireTime;
			}
			return _cfg.AccessToken;
		}
		#endregion

		#region 事件
		/// <summary>
		/// 数据到达时发生
		/// </summary>
		public static event EventHandler<DataEventArgs> OnData;
		/// <summary>
		/// 消息到达时发生
		/// </summary>
		public static event EventHandler<MessageEventArgs> OnMessage;
		/// <summary>
		/// 推送消息到达时发生
		/// </summary>
		public static event EventHandler<PushMessageEventArgs> OnPushMessage;

		/// <summary>
		/// 消息返回委托
		/// </summary>
		static DataCallBack DataCallBack = (dataType, data, length, user) =>
		{
			OnData?.Invoke(null, new DataEventArgs(dataType, data, length, user));
			return 0;
		};
		/// <summary>
		/// 消息返回委托
		/// </summary>
		static MessageHandler MessageHandler = (sessionId, messageType, errorCode, messageInfo, user) =>
		{
			OnMessage?.Invoke(null, new MessageEventArgs(sessionId, messageType, errorCode, messageInfo, user));
			return 0;
		};
		/// <summary>
		/// 推送消息返回委托
		/// </summary>
		static PushMessageHandler PushMessageHandler = (desc, content, detail, user) =>
		{
			OnPushMessage?.Invoke(null, new PushMessageEventArgs(desc, content, detail, user));
			return 0;
		};
		#endregion

		#region 会话
		static IntPtr sessionId;
		static int sessionIdLength;
		/// <summary>
		/// 分配会话
		/// </summary>
		/// <returns></returns>
		public static string AllocSession()
		{
			IntPtr userID = Marshal.StringToHGlobalAnsi(_cfg.UserId);
			if (SDK.OpenSDK_AllocSessionEx(MessageHandler, userID, out sessionId, out sessionIdLength) == 0)
			{
				return Marshal.PtrToStringAnsi(sessionId, sessionIdLength);
			}
			return "";
		}
		/// <summary>
		/// 销毁会话
		/// </summary>
		/// <returns></returns>
		public static bool FreeSession(string sessionId)
		{
			return SDK.OpenSDK_FreeSession(sessionId) == 0;
		}
		#endregion

		#region 预览流程
		/// <summary>
		/// 开始播放
		/// </summary>
		/// <param name="playHandle"></param>
		/// <param name="camera"></param>
		/// <param name="safekey"></param>
		/// <param name="sessionId"></param>
		/// <returns></returns>
		public static bool Play(IntPtr playHandle, CameraInfo camera, string safekey, out string sessionId)
		{
			OpenSDK_SetAccessToken(AccessToken);
			sessionId = AllocSession();
			return OpenSDK_SetVideoLevel(camera.DeviceSerial, camera.CameraNo, camera.VideoQualityInfos.LastOrDefault().VideoLevel) == 0 
			&& OpenSDK_StartRealPlayEx(sessionId, playHandle, camera.DeviceSerial, camera.CameraNo, safekey) == 0;
		}

		public static bool Stop(string sessionId)
		{
			return OpenSDK_StopRealPlayEx(sessionId) == 1;
		}
		#endregion

		#region 获取数据信息流程
		/// <summary>
		/// 获取账号下所有设备
		/// </summary>
		/// <param name="isShared">是否分享过来的设备 默认否</param>
		/// <returns></returns>
		public static List<DeviceInfo> GetDeviceList(bool isShared = false)
		{
			var _list = new List<DeviceInfo>();
			var _page = 0;
			var _limit = 500;
			try
			{
				while (true)
				{
					IntPtr _hander;
					int _len;
					var _success = isShared ? (SDK.OpenSDK_Data_GetSharedDevList(_page++, _limit, out _hander, out _len) == 0) :
						SDK.OpenSDK_Data_GetDevListEx(_page++, _limit, out _hander, out _len) == 0;

					if (_success)
					{
						var _response = Marshal.PtrToStringAnsi(_hander);
						var _result = _response.FromJson<DeviceResponse>();
						_list.AddRange(_result?.Data);
						if (_list.Count >= _result.Page.Total)
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
		///// <summary>
		///// 获取单个设备信息
		///// </summary>
		///// <param name="deviceSerial"></param>
		///// <returns></returns>
		//public static DeviceInfo GetDevice(string deviceSerial)
		//{
		//	try
		//	{
		//		IntPtr _hander;
		//		int _len;
		//		if (SDK.OpenSDK_Data_GetDeviceInfo(AccessToken, deviceSerial, out _hander, out _len) == 0)
		//		{
		//			var _response = Marshal.PtrToStringAnsi(_hander);
		//			var _result = _response.FromJson<DeviceResponse>();
		//			return _result?.Data?.FirstOrDefault();
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		LogService.AppendErrorLog(typeof(YsAction), "获取设备列表异常", ex);
		//	}
		//	return null;
		//}
		#endregion
	}
}
