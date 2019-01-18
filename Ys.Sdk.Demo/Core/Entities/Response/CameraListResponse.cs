using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core.Entities.Response
{

	public class CameraListResponse : Base
	{
		/// <summary>
		/// 监控列表
		/// </summary>
		public List<CameraInfo> CameraList { get; set; }

		/// <summary>
		/// 返回的消息
		/// </summary>
		[JsonProperty("resultMsg")]
		protected override string Msg { set; get; }

		/// <summary>
		/// 业务码
		/// </summary>
		[JsonProperty("resultCode")]
		protected override uint Code { set; get; }

		public override bool IsSuccess
		{
			get => Code == 200;
		}

		public override string ErrorMsg
		{
			get
			{
				if (string.IsNullOrEmpty(Msg))
				{
					var _msg = "";
					YsError.TokenErrorDictionary.TryGetValue(Code, out _msg);
					Msg = _msg;
				}
				return Msg;
			}
		}

		/// <summary>
		/// 总数量
		/// </summary>
		public int Count { get; set; }
	}
	/// <summary>
	/// 单一设备监控列表请求
	/// </summary>
	public class SingleDeviceCameraListResponse : Base<List<CameraInfo>>
	{
		public override bool IsSuccess
		{
			get => Code == 200;
		}

		public override string ErrorMsg
		{
			get
			{
				if (string.IsNullOrEmpty(Msg))
				{
					var _msg = "";
					YsError.TokenErrorDictionary.TryGetValue(Code, out _msg);
					Msg = _msg;
				}
				return Msg;
			}
		}
	}
}
