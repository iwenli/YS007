using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core.Entities
{
	/// <summary>
	/// 监控实体
	/// </summary>
	public class CameraInfo
	{
		/// <summary>
		/// 摄像头唯一标识
		/// </summary>
		public string CameraId { get; set; }
		/// <summary>
		/// 摄像头名称
		/// </summary>
		public string CameraName { get; set; }
		/// <summary>
		/// 通道
		/// </summary>
		public int CameraNo { get; set; }

		/// <summary>
		/// 具有防护能力的设备布撤防状态：0-睡眠，8-在家，16-外出，普通IPC布撤防状态：0-撤防，1-布防
		/// </summary>
		public int Defence { get; set; }
		/// <summary>
		/// 设备唯一标识
		/// </summary>
		public string DeviceId { get; set; }
		/// <summary>
		/// 设备名称
		/// </summary>
		public string DeviceName { get; set; }
		/// <summary>
		/// 设备序列号
		/// </summary>
		public string DeviceSerial { get; set; }
		/// <summary>
		/// 启用加密
		/// </summary>
		public int IsEncrypt { get; set; }

		public int IsIPC { get; set; }
		/// <summary>
		/// 分享状态：1-分享所有者，0-未分享，2-分享接受者（表示此摄像头是别人分享给我的）
		/// </summary>
		public string IsShared { get; set; }
		/// <summary>
		/// 图片地址（大图），若在萤石客户端设置封面则返回封面图片，未设置则返回默认图片
		/// </summary>
		public string PicUrl { get; set; }
		/// <summary>
		/// 在线状态：0-不在线，1-在线
		/// </summary>
		public int Status { get; set; }
		/// <summary>
		/// 视频质量：0-流畅，1-均衡，2-高清，3-超清
		/// </summary>
		public int VideoLevel { get; set; }
	}
}
