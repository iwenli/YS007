using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ys.Sdk.Demo.Core.Entities;

namespace Ys.Sdk.Demo.Core
{
	/// <summary>
	/// 通知订阅事件值
	/// </summary>
	public class SubscribeEventArgs : EventArgs
	{
		IntPtr m_sessionId;
		IntPtr m_user;
		public SubscribeEventArgs(IntPtr sessionId, uint msgType, uint error, string info, IntPtr user)
		{
			m_sessionId = sessionId;
			MsgType = msgType;
			Error = error;
			Info = info;
			m_user = user;
			var _err = "";
			YsError.MessageErrorDictionary?.TryGetValue(Info, out _err);
			MessageError = $"{ _err}({Info})";

			if (Info == "UE105")
			{
				_err = "";
				YsError.CodeErrorDictionary?.TryGetValue(Error, out _err);
				CodeError = $"{ _err}({Error})";
			}
		}
		/// <summary>
		/// 消息类型 
		/// 0:操作失败,则根据MessageInfo来判断错误信息,如果错误信息为UE105, 则可以根据ErrorCode确定具体错误码
		/// 3:播放开始 
		/// 4:播放终止 
		/// 5:回放结束
		/// 
		/// 20:录像搜索成功
		/// 22：录像搜索失败
		/// </summary>
		public uint MsgType { get; set; }
		/// <summary>
		/// 错误
		/// </summary>
		public uint Error { get; set; }
		/// <summary>
		/// 通知原始信息
		/// </summary>
		public string Info { get; set; }
		/// <summary>
		/// 会话id
		/// </summary>
		public string SessionId => Marshal.PtrToStringAnsi(m_sessionId);
		/// <summary>
		/// 用户
		/// </summary>
		public string UserId => Marshal.PtrToStringAnsi(m_user);

		public string MessageError { get; private set; }

		public string CodeError { get; private set; }

		public override string ToString()
		{
			var action = MsgType == 3 ? "播放开始" : MsgType == 4 ? "播放终止" : MsgType == 5 ? "回放结束" : $"未知操作";
			return $"用户[{UserId}]-会话[{SessionId}]的视频[{action}[{MsgType}]] {CodeError} {MessageError}";
		}
	}
}
