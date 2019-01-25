using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core.V2
{
	/// <summary>
	/// 音视频流数据类型
	/// </summary>
	public enum DataType

	{
		/// <summary>
		/// 流头
		/// </summary>
		NET_DVR_SYSHEAD = 1,
		/// <summary>
		/// 流数据
		/// </summary>
		NET_DVR_STREAMDATA = 2,
		/// <summary>
		/// 结束标记
		/// </summary>
		NET_DVR_RECV_END = 3
	}
}
