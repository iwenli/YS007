using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core.Entities.V2
{
	/// <summary>
	/// 设备信息
	/// </summary>
	public class DeviceInfo
	{
		/// <summary>
		/// 设备名称
		/// </summary>
		public string DeviceName { get; set; }
		/// <summary>
		/// 设备序列号
		/// </summary>
		public string DeviceSerial { get; set; }
		/// <summary>
		/// 设备被用户添加时间，精确到毫秒
		/// </summary>
		public long AddTime { get; set; }
		/// <summary>
		/// 告警声音模式
		/// </summary>
		public int AlarmSoundMode { get; set; }
		/// <summary>
		/// 视频数
		/// </summary>
		public int CameraNum { get; set; }
		/// <summary>
		/// 设备大类
		/// </summary>
		public string Category { get; set; }
		/// <summary>
		/// 布A1设备布撤防状态，0:睡眠 8:在家 16:外出, 非A1设备，0-撤防 1-布防
		/// </summary>
		public int Defence { get; set; }
		/// <summary>
		/// 探测器数
		/// </summary>
		public int DetectorNum { get; set; }
		/// <summary>
		/// 探测器信息
		/// </summary>
		public string DetectorInfo { get; set; }
		/// <summary>
		/// 设备封面
		/// </summary>
		public string DeviceCover { get; set; }
		/// <summary>
		/// 设备类型
		/// </summary>
		public string DeviceType { get; set; }
		/// <summary>
		/// 设备版本号
		/// </summary>
		public string DeviceVersion { get; set; }
		/// <summary>
		/// 是否加密，0：不加密，1：加密
		/// </summary>
		public bool IsEncrypt { get; set; }
		/// <summary>
		/// 在线状态，1-在线，2-不在线
		/// </summary>
		public int Status { get; set; }
		/// <summary>
		/// 设备能力集
		/// </summary>
		public string SupportExtShort { get; set; }
		/// <summary>
		/// 视频信息
		/// </summary>
		public List<CameraInfo> CameraInfo { get; set; }
	}
}
