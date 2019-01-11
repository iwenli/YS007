using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.Sdk.Demo.Common;
using Ys.Sdk.Demo.Service.Entities;

namespace Ys.Sdk.Demo.Service.Http
{
	public class StoreService : ServiceBase
	{
		public StoreService(ServiceContext context) : base(context)
		{
		}
		public async Task<ResponseBase<List<StoreInfo>>> GetList()
		{
			var _queryDic = new SortedDictionary<string, string>();
			_queryDic.Add("brandtoken", GetBrandToken());
			var _result = await NetClient.GetAsync(GetStoreList, queryDictionary: _queryDic);
			return _result.FromJson<ResponseBase<List<StoreInfo>>>();
		}
	}
}
