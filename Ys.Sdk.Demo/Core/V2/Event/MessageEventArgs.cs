using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Txooo.Extension.Extension;

namespace Ys.Sdk.Demo.Core.V2.Event
{
	public class MessageEventArgs : EventArgs
	{
		public string SessionId { set; get; }
		public MessageType MessageType { set; get; }
		public uint ErrorCode { set; get; }
		public string MessageInfo { set; get; }
		public string User { set; get; }
		public string ErrorMsg { get; set; }

		public MessageEventArgs(string sessionId, MessageType messageType, uint errorCode, IntPtr messageInfo, IntPtr user)
		{
			SessionId = sessionId;
			MessageType = messageType;
			ErrorCode = errorCode;
			MessageInfo = Marshal.PtrToStringAnsi(messageInfo);
			User = Marshal.PtrToStringAnsi(user);

			var _err = "";
			Entities.YsError.MessageErrorDictionary?.TryGetValue(MessageInfo, out _err);
			if (string.IsNullOrWhiteSpace(_err))
			{
				Entities.YsError.CodeErrorDictionary?.TryGetValue(ErrorCode, out _err);
			}
			ErrorMsg = _err;
		}

		public override string ToString()
		{
			return this.ToJson();
		}
	}
}
