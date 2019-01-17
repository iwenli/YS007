using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Service.Entities.Ys.Response
{

	public class CameraListResponse : ResponseBase
	{
		/// <summary>
		/// 监控列表
		/// </summary>
		[JsonProperty("cameraList")]
		public List<CameraInfo> CameraList { get; set; }
	}
	/// <summary>
	/// 单一设备监控列表请求
	/// </summary>
	public class SingleDeviceCameraListResponse
	{
		/// <summary>
		/// 监控列表原始请求数据
		/// </summary>
		[JsonProperty("result")]
		public ResponseBase<List<CameraInfo>> Raw { get; set; }

		/// <summary>
		/// 监控列表
		/// </summary>
		public List<CameraInfo> CameraList => Raw.Data;
	}
}
