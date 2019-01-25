using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Txooo.Extension.Extension;

namespace Ys.Sdk.Demo.Core.V2.Event
{
	public class DataEventArgs : EventArgs
	{
		public DataType DataType { set; get; }
		public string Data { set; get; }
		public int Length { set; get; }
		public string User { set; get; }

		public DataEventArgs(DataType dataType, IntPtr data, int length, IntPtr user)
		{
			DataType = dataType;
			Data = Marshal.PtrToStringAnsi(data);
			Length = length;
			User = Marshal.PtrToStringAnsi(user);
		}

		public override string ToString()
		{
			return this.ToJson();
		}
	}
}
