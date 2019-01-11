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
			var _limit = 100;
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
	}
}
