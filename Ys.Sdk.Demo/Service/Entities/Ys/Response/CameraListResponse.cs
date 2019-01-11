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
}
