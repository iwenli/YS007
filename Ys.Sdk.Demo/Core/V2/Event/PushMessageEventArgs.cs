using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Txooo.Extension.Extension;

namespace Ys.Sdk.Demo.Core.V2.Event
{
	public class PushMessageEventArgs : EventArgs
	{
		public string Desc { set; get; }
		public string Content { set; get; }
		public string Detail { set; get; }
		public string User { set; get; }

		public PushMessageEventArgs(string desc, string content, string detail, IntPtr user)
		{
			Desc = desc;
			Content = content;
			Detail = detail;
			User = Marshal.PtrToStringAnsi(user);
		}

		public override string ToString()
		{
			return this.ToJson();
		}
	}
}
