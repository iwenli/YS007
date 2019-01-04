using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Service.Entities.Response
{
	public class VerifyCodeResponse : ResponseBase
	{
		/// <summary>
		/// 待验证信息
		/// </summary>
		[JsonProperty("mobile_verify")]
		public string MobileVerify { set; get; }
	}
}
