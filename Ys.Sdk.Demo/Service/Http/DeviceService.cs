using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.Sdk.Demo.Common;
using Ys.Sdk.Demo.Service.Entities;

namespace Ys.Sdk.Demo.Service.Http
{
	public class DeviceService : ServiceBase
	{
		public DeviceService(ServiceContext context) : base(context)
		{
		}

		public async Task<ResponseBase<List<DeviceInfo>>> GetList()
		{
			var _queryDic = new SortedDictionary<string, string>();
			_queryDic.Add("brandtoken", GetBrandToken());
			_queryDic.Add("device_type", "");
			var _result = await NetClient.GetAsync(GetDeviceList, queryDictionary: _queryDic);
			return _result.FromJson<ResponseBase<List<DeviceInfo>>>();
		}
	}
}
