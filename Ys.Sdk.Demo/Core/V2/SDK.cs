using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core.V2
{
	#region 委托
	/// <summary>
	/// 信息回调
	/// http://open.ys7.com/doc/zh/pc/group__alloc.html#ga4ccc07bd64b9e128d00ed641ad5b5592
	/// </summary>
	/// <param name="sessionId">申请的会话ID</param>
	/// <param name="messageType">消息号 MessageType</param>
	/// <param name="errorCode">错误码 OpenNetStreamError.h</param>
	/// <param name="messageInfo">信息</param>
	/// <param name="user">用户自定义数据</param>
	/// <returns></returns>
	public delegate int MessageHandler(string sessionId, MessageType messageType, uint errorCode, IntPtr messageInfo, IntPtr user);

	/// <summary>
	/// 数据回调
	/// http://open.ys7.com/doc/zh/pc/group__play.html#gaf901ce20689553b80800260e4482079c
	/// </summary>
	/// <param name="dataType">数据类型</param>
	/// <param name="data">数据内容</param>
	/// <param name="length">数据长度</param>
	/// <param name="user">用户自定义数据</param>
	/// <returns></returns>
	public delegate int DataCallBack(DataType dataType, IntPtr data, int length, IntPtr user);
	/// <summary>
	/// 告警消息推送
	/// http://open.ys7.com/doc/zh/pc/group__push.html#ga4f2a82ea2749600296542aa0a9a8b651
	/// </summary>
	/// <param name="desc">推送描述信息</param>
	/// <param name="content">推送内容</param>
	/// <param name="detail">完整的推送信息</param>
	/// <param name="user">用户自定义数据</param>
	/// <returns></returns>
	public delegate int PushMessageHandler(string desc, string content, string detail, IntPtr user);


	#endregion
	/// <summary>
	/// 萤石云SDK
	/// V4.2.0
	/// http://open.ys7.com/doc/zh/pc/_open_net_stream_inter_face_8h.html
	/// </summary>
	public class SDK
	{
		#region Interface 说明
		/*
		 * const char *		(sz...)		:	string
		 * unsigned int		(i...)		:	uint
		 * const char *		(p...)		:	IntPtr
		 
		*/
		#endregion

		#region 函数
		#region 开放SDK初始化

		/// <summary>
		/// 初始化库, 支持配置平台地址
		/// http://open.ys7.com/doc/zh/pc/group__init.html#gadbe3225f98810c2268453610da578cab
		/// </summary>
		/// <param name="authAddr">认证域名:开放平台认证中心地址，默认地址为：https://openauth.ys7.com .对于开发者而言，请填写此默认地址即可.</param>
		/// <param name="platForm">平台域名:开放平台后台地址，默认地址为：https://open.ys7.com .对于开发者而言，请填写此默认地址即可.</param>
		/// <param name="appId">向平台申请的AppKey</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_InitLib(string authAddr, string platForm, string appId);

		/// <summary>
		/// 初始化库, 默认国内版本使用的接口
		/// http://open.ys7.com/doc/zh/pc/group__init.html#gac3d67232951e316b98fe1a3a754fac4b
		/// </summary>
		/// <param name="appid">向平台申请的AppKey</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_Init(string appid);
		/// <summary>
		/// 反初始化库
		/// http://open.ys7.com/doc/zh/pc/group__init.html#ga2062cf9fccbc01af88bd543c3e942384
		/// </summary>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_FiniLib();

		/// <summary>
		/// 全局配置信息, 根据ConfigKey进行配置
		/// http://open.ys7.com/doc/zh/pc/group__init.html#ga19b6eb248177efbe964881f65d91da9e
		/// </summary>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern void OpenSDK_SetConfigInfo(ConfigKeyType configKey, int value);

		/// <summary>
		/// 设置平台地址,海外平台地址重定向后, 通过调用此接口重新设置平台地址
		/// http://open.ys7.com/doc/zh/pc/group__init.html#ga19b6eb248177efbe964881f65d91da9e
		/// </summary>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern void OpenSDK_SetPlatformAddr(string platForm);
		/// <summary>
		/// 设置
		/// http://open.ys7.com/doc/zh/pc/group__init.html#ga04655f3ef71ffe951ff2f232640204a0
		/// </summary>
		/// <param name="appid">向平台申请的AppKey</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_SetAppID(string appid);
		/// <summary>
		/// 设置AccessToken
		/// http://open.ys7.com/doc/zh/pc/group__init.html#ga423d451cbb7a1add8fc4d4300702762b
		/// </summary>
		/// <param name="accessToken"></param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_SetAccessToken(string accessToken);
		#endregion

		#region 分配会话
		/// <summary>
		/// 申请一个会话Id
		/// http://open.ys7.com/doc/zh/pc/group__alloc.html#ga397052ecf209c2f2e866803b1e2b1f73
		/// </summary>
		/// <param name="handler">设置回调函数</param>
		/// <param name="user">用户自定义数据，会在pHandle中原样抛出</param>
		/// <param name="sessionId">用于接收分配的会话ID</param>
		/// <param name="sessionIdLength">会话ID的长度</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_AllocSessionEx(MessageHandler handler, IntPtr user, out IntPtr sessionId, out int sessionIdLength);

		/// <summary>
		/// 销毁SDK操作句柄 
		/// http://open.ys7.com/doc/zh/pc/group__alloc.html#gac3d161aba0a3fa19ab37b7f58884901c
		/// </summary>
		/// <param name="sessionId">会话Id，通过OpenSDK_AllocSessionEx创建</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_FreeSession(string sessionId);

		/// <summary>
		/// 销毁SDK操作句柄
		/// </summary>
		/// <param name="sessionId"></param>
		/// <param name="configKey">配置类型</param>
		/// <param name="value">配置数值, 如果key=CONFIG_OPEN_STREAMTRANS, value=1时表示视频码流经过转封装库输出,用于录像</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_SetSessionConfig(string sessionId, ConfigKeyType configKey, int value);
		#endregion

		#region 预览接口
		/// <summary>
		/// 设置数据回调
		/// http://open.ys7.com/doc/zh/pc/group__play.html#ga7192d401f71e66dca7e23c895a31aab2
		/// </summary>
		/// <param name="sessionId">会话ID</param>
		/// <param name="callBack">回调函数</param>
		/// <param name="user">用户自定义数据，会通过pDataCallBack原样抛出</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_SetDataCallBack(string sessionId, DataCallBack callBack, IntPtr user);

		/// <summary>
		/// 设置或者切换清晰度, 根据OpenSDK_Data_GetDevDetailInfo接口返回信息videoQualityInfos来判断是否支持对应的清晰度类型
		/// http://open.ys7.com/doc/zh/pc/group__play.html#gac6a2498179c809aa46921098e8aa41c0
		/// </summary>
		/// <param name="deviceSerial">设备序列号</param>
		/// <param name="channelNo">设备通道号</param>
		/// <param name="videoLevel">视频质量</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_SetVideoLevel(string deviceSerial, int channelNo, VideoLevel videoLevel);

		/// <summary> 
		/// 针对摄像头进行预览 异步接口
		/// 返回值只是表示操作成功，不代表播放成功 
		/// 如果接口返回-1, 结果根据消息回调函数msgid判断, 
		/// 若msgid=INS_PLAY_START, 表示成功, 若msgid=INS_PLAY_EXCEPTION, 表示失败
		/// http://open.ys7.com/doc/zh/pc/group__play.html#ga3d939cc5da94ff5c9f2a75fc0b768299
		/// </summary>
		/// <param name="sessionId">会话ID</param>
		/// <param name="playHandle">播放窗口句柄, 如果窗口句柄为NULL表示纯粹取流，不做播放</param>
		/// <param name="deviceSerial">设备序列号</param>
		/// <param name="channelNo">设备通道号</param>
		/// <param name="safeKey">视频加密密钥,如果视频未加密, 可以设置为NULL</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_StartRealPlayEx(string sessionId, IntPtr playHandle, string deviceSerial, int channelNo, string safeKey);

		/// <summary>
		/// http://open.ys7.com/doc/zh/pc/group__play.html#ga261176973368661a4685debf942eaac0
		/// 针对摄像头进行预览, 指定主\子码流 异步接口，
		/// 返回值只是表示操作成功，不代表播放成功 
		/// 如果接口返回-1, 结果根据消息回调函数msgid判断, 
		/// 若msgid=INS_PLAY_START, 表示成功, 若msgid=INS_PLAY_EXCEPTION, 表示失败
		/// </summary>
		/// <param name="sessionId">会话ID</param>
		/// <param name="playHandle">播放窗口句柄, 如果窗口句柄为NULL表示纯粹取流，不做播放</param>
		/// <param name="deviceSerial">设备序列号</param>
		/// <param name="channelNo">设备通道号</param>
		/// <param name="safeKey">视频加密密钥,如果视频未加密, 可以设置为NULL</param>
		/// <param name="streamType">主子码流 1-主, 2-子, -1-默认码流类型</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_StartPlayWithStreamType(string sessionId, IntPtr playHandle, string deviceSerial, int channelNo, string safeKey, int streamType);

		/// <summary>
		/// http://open.ys7.com/doc/zh/pc/group__play.html#gab57d3e1076a3c52e3ee8afec93339b09
		/// 停止播放播放 异步接口，返回值只是表示操作成功，不代表停止播放成功 
		/// 如果接口返回-1, 结果根据消息回调函数msgid判断, 若msgid=INS_PLAY_STOP, 表示成功
		/// </summary>
		/// <param name="sessionId">会话ID</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_StopRealPlayEx(string sessionId);
		#endregion

		#region 数据接口
		/// <summary>
		/// 获取摄像头列表
		/// </summary>
		/// <param name="page">分页起始页，从0开始</param>
		/// <param name="size">分页大小, 限制最大1000,默认100, 建议不超过100</param>
		/// <param name="deviceList">摄像头列表的JSON字符串</param>
		/// <param name="length">获取到的数据大小</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_Data_GetDevListEx(int page, int size, out IntPtr deviceList, out int length);

		/// <summary>
		/// 获取他人分享的摄像头列表
		/// http://open.ys7.com/doc/zh/pc/group__data.html#ga9ecfe1f46355c3c8aa4ba2b93c22386a
		/// </summary>
		/// <param name="page">分页起始页，从0开始</param>
		/// <param name="size">分页大小, 限制最大1000,默认100, 建议不超过100</param>
		/// <param name="deviceList">摄像头列表的JSON字符串</param>
		/// <param name="length">获取到的数据大小</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_Data_GetSharedDevList(int page, int size, out IntPtr deviceList, out int length);

		/// <summary>
		/// 获取设备详细信息, 包括预览能力级, PTZ, 对讲能力级等.
		/// http://open.ys7.com/doc/zh/pc/group__data.html#ga716c2971f49c6db67e2d811e2951867c
		/// </summary>
		/// <param name="deviceSerial">设备序列号</param>
		/// <param name="channelNo">设备通道号</param>
		/// <param name="update">是否重新获取设备详细信息, true:重新从萤石平台拿数据, 否则拿缓存信息. true的时机为用户重新登录或者需要刷新设备信息</param>
		/// <param name="deviceDetail">监控点信息，需要调用OpenSDK_FreeData接口释放</param>
		/// <param name="length">监控点信息的长度</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_Data_GetDevDetailInfo(string deviceSerial, int channelNo, bool update, out IntPtr deviceDetail, out int length);

		/// <summary>
		/// 获取单个设备信息
		/// http://open.ys7.com/doc/zh/pc/group__data.html#ga5fd97193d3f47d5731f2d3640d9eb33e
		/// </summary>
		/// <param name="accessToken"></param>
		/// <param name="deviceSerial">设备序列号</param>
		/// <param name="device">设备信息的JSON字符串</param>
		/// <param name="length">获取到的数据大小</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_Data_GetDeviceInfo(string accessToken, string deviceSerial, out IntPtr device, out int length);

		/// <summary>
		/// 获取告警列表
		/// http://open.ys7.com/doc/zh/pc/group__data.html#ga42a9703d55f1e5844a1f76419e474abf
		/// </summary>
		/// <param name="deviceSerial">设备序列号</param>
		/// <param name="channelNo">通道号</param>
		/// <param name="startTime">开始时间</param>
		/// <param name="endTime">结束时间</param>
		/// <param name="alarmType">告警类型</param>
		/// <param name="status">告警状态，0表示未读，1表示已读，2表示所有</param>
		/// <param name="page">分页起始页，从0开始</param>
		/// <param name="size">分页大小</param>
		/// <param name="alarmList">告警信息列表</param>
		/// <param name="length">告警信息列表长度</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_Data_GetAlarmListEx(string deviceSerial, int channelNo, string startTime, string endTime, AlarmType alarmType, int status, int page, int size, out IntPtr alarmList, out int length);

		/// <summary>
		/// 解密告警图片(建议加密的图片才调用，非加密图片直接下载，可以通过url里面isEncrypted=1来区分)
		/// http://open.ys7.com/doc/zh/pc/group__data.html#ga6f5c5aaff2b6b1952e10d792ef3cfb07
		/// </summary>
		/// <param name="accessToken">认证Token</param>
		/// <param name="picUrl">图片URL,https://wuhancloudpictest.ys7.com:8083/...?isEncrypted=1&isCloudStored=0</param>
		/// <param name="deviceSerail">告警图片对应的设备序列号</param>
		/// <param name="safeKey">解密密钥，默认是设备验证码</param>
		/// <param name="pic">解密后图片内容（需要调用OpenSDK_Data_Free释放内存）</param>
		/// <param name="length">pPicBuf的长度</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_DecryptPicture(string accessToken, string picUrl, string deviceSerail, string safeKey, out IntPtr pic, out int length);

		/// <summary>
		/// 设置告警已读
		/// http://open.ys7.com/doc/zh/pc/group__data.html#ga53324e5a752d13748fe1cebfe37ce867
		/// </summary>
		/// <param name="accessToken">认证Token</param>
		/// <param name="alarmId">告警ID</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_Data_SetAlarmRead(string accessToken, string alarmId);

		/// <summary>
		/// 删除设备
		/// http://open.ys7.com/doc/zh/pc/group__data.html#gae844677f6655195aaf710380791f66cc
		/// </summary>
		/// <param name="accessToken">认证Token</param>
		/// <param name="deviceId">设备Id</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_Data_DeleteDevice(string accessToken, string deviceId);

		/// <summary>
		/// 销毁SDK分配的内存
		/// http://open.ys7.com/doc/zh/pc/group__data.html#ga8a06867b2aba790a584b7bfb9c2be569
		/// </summary>
		/// <param name="ptr">SDK分配的内存</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_Data_Free(IntPtr ptr);
		#endregion

		#region 通用工具接口
		/// <summary>
		/// Http请求接口
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="header">头部参数</param>
		/// <param name="body">Body数据</param>
		/// <param name="response">返回报文的内容</param>
		/// <param name="length">返回报文的长度</param>
		/// <returns></returns>
		[DllImport("OpenNetStream.dll")]
		public static extern int OpenSDK_HttpSendWithWait(string url, string header, string body, out IntPtr response, out int length);
		#endregion
		#endregion
	}
}
