using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.Sdk.Demo.Core.Entities.Response;

namespace Ys.Sdk.Demo.Core.Entities.V2
{
	public class DeviceResponse : Base<List<DeviceInfo>>
	{
		/// <summary>
		/// 
		/// </summary>
		public PageInfo Page { get; set; }
	}
}
