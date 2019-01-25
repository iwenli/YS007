using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.Sdk.Demo.Core.V2;

namespace Ys.Sdk.Demo.Core.Entities.V2
{
	/// <summary>
	/// 视频质量信息
	/// </summary>
	public class VideoQualityInfo
	{
		/// <summary>
		/// 流类型
		/// </summary>
		public int StreamType { get; set; }
		/// <summary>
		/// 等级
		/// </summary>
		public VideoLevel VideoLevel { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public string  VideoQualityName { get; set; }
	}
}
