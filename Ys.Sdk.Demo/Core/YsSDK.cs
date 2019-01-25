using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core
{
	/// <summary>
	/// 萤石云SDK
	/// </summary>
	class YsSDK
	{
		#region 初始化 授权
		/// <summary>
		/// SDK启动
		/// </summary>
		/// <param name="AuthAddr"></param>
		/// <param name="Platform"></param>
		/// <param name="AppId"></param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_InitLib(string AuthAddr, string Platform, string AppId);

		/// <summary>
		/// SDK关闭
		/// </summary>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_FiniLib();

		/// <summary>
		/// SDK第三方登陆
		/// </summary>
		/// <param name="pToken"></param>
		/// <param name="TokenLth"></param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_Mid_Login(ref string pToken, ref int TokenLth);
		/// <summary>
		/// SDK申请会话
		/// </summary>
		/// <param name="CallBack"></param>
		/// <param name="UserID"></param>
		/// <param name="pSID"></param>
		/// <param name="SIDLth"></param>
		/// <param name="bSync"></param>
		/// <param name="timeout"></param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_AllocSession(MsgHandler CallBack, IntPtr UserID, ref IntPtr pSID, ref int SIDLth, bool bSync, uint timeout);/// <summary>
																																					 /// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_HttpSendWithWait(string szUri, string szHeaderParam, string szBody, out IntPtr iMessage, out int iLength);

		/// <summary>
		/// SDK关闭会话
		/// </summary>
		/// <param name="SID"></param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_FreeSession(string SID);
		#endregion

		#region 设备列表
		/// <summary>
		/// SDK获取所有设备摄像机列表
		/// </summary>
		/// <param name="accessToken"></param>
		/// <param name="iPageStart"></param>
		/// <param name="iPageSize"></param>
		/// <param name="iMessage"></param>
		/// <param name="iLength"></param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_Data_GetDevList(string accessToken, int iPageStart, int iPageSize, out IntPtr iMessage, out int iLength);

		/// <summary>
		/// SDK获取指定设备摄像机信息
		/// </summary>
		/// <param name="accessToken"></param>
		/// <param name="szDeviceSerial"></param>
		/// <param name="iMessage"></param>
		/// <param name="iLength"></param>
		/// <returns></returns>
		[DllImport(@"OpenNetStream.dll")]
		public static extern int OpenSDK_Data_GetDeviceInfo(string accessToken, string szDeviceSerial, out IntPtr iMessage, out int iLength);


		#endregion

		#region 预览
		/// <summary>
		/// SDK开始播放
		/// </summary>
		/// <param name="SID"></param>
		/// <param name="PlayWnd"></param>
		/// <param name="CameraId"></param>
		/// <param name="Token"></param>
		/// <param name="VideoLevel"></param>
		/// <param name="SafeKey"></param>
		/// <param name="AppKey"></param>
		/// <param name="pNSCBMsg"></param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_StartRealPlay(IntPtr SID, IntPtr PlayWnd, string CameraId, string Token, int VideoLevel, string SafeKey, string AppKey, uint pNSCBMsg);

		/// <summary>
		/// SDK关闭播放
		/// </summary>
		/// <param name="SID"></param>
		/// <param name="pNSCBMsg"></param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_StopRealPlay(IntPtr SID, uint pNSCBMsg);
		#endregion

		#region 回放 http://open.ys7.com/doc/zh/pc/group__playback.html
		/// <summary>
		/// 搜索录像
		/// </summary>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_StartSearchEx(string sessionId, string deviceSerial, int channelNo, string startTime, string stopTime);

		#endregion

		/// <summary>
		/// 及其回调函数格式
		/// </summary>
		/// <param name="SID"></param>
		/// <param name="MsgType"></param>
		/// <param name="Error"></param>
		/// <param name="Info"></param>
		/// <param name="pUser"></param>
		/// <returns></returns>
		public delegate int MsgHandler(IntPtr SID, uint MsgType, uint Error, string Info, IntPtr pUser);

		///// <summary>
		///// 回调实例
		///// </summary>
		///// <param name="SID"></param>
		///// <param name="MsgType"></param>
		///// <param name="Error"></param>
		///// <param name="Info"></param>
		///// <param name="pUser"></param>
		///// <returns></returns>
		//public static int HandlerWork(IntPtr SID, uint MsgType, uint Error, string Info, IntPtr pUser)
		//{
		//	return 0;
		//}


		/// <summary>
		/// 截屏
		/// </summary>
		/// <param name="SID"></param>
		/// <param name="szFileName"></param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_CapturePicture(IntPtr SID, string szFileName);

		/// <summary>
		/// 设置数据回调窗口
		/// </summary>
		/// <param name="sessionId"></param>
		/// <param name="pDataCallBack"></param>
		/// <param name="pUser"></param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_SetDataCallBack(IntPtr sessionId, OpenSDK_DataCallBack pDataCallBack, string pUser);


		/// <summary>
		/// 回调函数格式
		/// </summary>
		/// <param name="dateType"></param>
		/// <param name="dateContent"></param>
		/// <param name="dataLen"></param>
		/// <param name="pUser"></param>
		public delegate void OpenSDK_DataCallBack(CallBackDateType dateType, IntPtr dateContent, int dataLen, string pUser);
		public static void DataCallBackHandler(CallBackDateType dataType, IntPtr dataContent, int dataLen, string pUser)
		{

		}
		//数据回调设置
		public enum CallBackDateType
		{
			NET_DVR_SYSHEAD = 0,
			NET_DVR_STREAMDATA = 1,
			NET_DVR_RECV_END = 2,
		};

		/*
        //SDK Http请求接口
        [DllImport(@"OpenNetStream.dll")]
        public static extern int OpenSDK_HttpSendWithWait(string szUri, string szHeaderParam, string szBody, out IntPtr iMessage, out int iLength);

        
       
        //SDK播放指定摄像机
        [DllImport(@"OpenNetStream.dll", EntryPoint = "OpenSDK_StartRealPlay", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OpenSDK_StartRealPlay(string szSessionId, IntPtr hPlayWnd, string szCameraId, string szAccessToken, int iVideoLevel, string szSafeKey, string szAppKey);
        */
	}
}
