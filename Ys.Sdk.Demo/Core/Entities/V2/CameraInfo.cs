using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.Sdk.Demo.Core.V2;

namespace Ys.Sdk.Demo.Core.Entities.V2
{
	/// <summary>
	/// 视频信息
	/// </summary>
	public class CameraInfo
	{

		private string id;
		/// <summary>
		/// 唯一标识
		/// </summary>
		public string Id
		{
			get
			{
				{
					if (string.IsNullOrEmpty(id))
					{
						id = $"{DeviceSerial}_{CameraNo}";
					}
					return id;
				}
			}
		}

		/// <summary>
		/// 通道封面
		/// </summary>
		public string CameraCover { get; set; }
		/// <summary>
		/// 视频名camera
		/// </summary>
		public string CameraName { get; set; }
		/// <summary>
		/// 通道
		/// </summary>
		public int CameraNo { get; set; }
		/// <summary>
		/// 设备序列号
		/// </summary>
		public string DeviceSerial { get; set; }
		/// <summary>
		/// 分享状态，1：分享所有者，0：未分享，2：分享接受者（表示此摄像头是别人分享给我的）
		/// </summary>
		public int IsShared { get; set; }
		/// <summary>
		/// 视频质量，0-流畅，1-均衡，2-高清，3-超清
		/// </summary>
		public VideoLevel VideoLevel { get; set; }
		/// <summary>
		/// 支持的视频质量
		/// </summary>
		public List<VideoQualityInfo> VideoQualityInfos { get; set; }
	}
}
