using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core.V2
{
	/// <summary>
	/// 配置类型
	/// </summary>
	public enum ConfigKeyType
	{
		/// <summary>
		/// 数据输出使用UTF8
		/// </summary>
		CONFIG_DATA_UTF8,
		/// <summary>
		/// 码流数据输出经过转封装处理, 用于录像存储
		/// </summary>
		CONFIG_OPEN_STREAMTRANS,
		/// <summary>
		/// P2P开关, 用于关闭P2P
		/// </summary>
		CONFIG_CLOSE_P2P,
		/// <summary>
		/// 配置日志等级, 参见
		/// </summary>
		CONFIG_LOG_LEVEL

	}
}
