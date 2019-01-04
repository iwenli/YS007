using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Txooo.Extension.Extension;
using Ys.Sdk.Demo.Service.Entities;
using Ys.Sdk.Demo.Service.Entities.Response;
using Ys.Sdk.Demo.Service.Entities.User;

namespace Ys.Sdk.Demo.Service.Http
{
	/// <summary>
	/// 
	/// </summary>
	public class PassportService : ServiceBase
	{
		public PassportService(ServiceContext context) : base(context)
		{
		}

		/// <summary>
		/// 验证手机号能否登录（是否为CBO）
		/// </summary>
		/// <param name="mobile"></param>
		/// <returns></returns>
		public async Task<ResponseBase> MobileCanLogin(string mobile)
		{
			var _queryDic = new SortedDictionary<string, string>();
			_queryDic.Add("mobile", mobile);
			var _result = await NetClient.GetAsync(IfMobileCanLogin_V1, queryDictionary: _queryDic);
			return _result.FromJson<ResponseBase>();
		}

		/// <summary>
		/// 发送验证码
		/// </summary>
		/// <param name="mobile"></param>
		/// <returns></returns>
		public async Task<VerifyCodeResponse> SendMobileCode(string mobile)
		{
			var _queryDic = new SortedDictionary<string, string>();
			_queryDic.Add("mobile", mobile);
			var _result = await NetClient.GetAsync(SendMobileCode_V1, queryDictionary: _queryDic, isGzip: false);
			return _result.FromJson<VerifyCodeResponse>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="verify"></param>
		/// <param name="mobileCode"></param>
		/// <returns></returns>
		public async Task<ResponseBase<LoginInfo>> Login(string mobile, string verify, string mobileCode)
		{
			var _queryDic = new SortedDictionary<string, string>();
			_queryDic.Add("mobile", mobile);
			_queryDic.Add("verify", verify);
			_queryDic.Add("mobilecode", mobileCode);
			var _headerDic = new SortedDictionary<string, string>();
			_headerDic.Add("mobilecode", mobileCode);

			var _result = await NetClient.SendAndReadAsStringAsync(Login_V1, method: HttpMethod.Get, queryDictionary: _queryDic, headerDictionary: _headerDic);
			return _result.FromJson<ResponseBase<LoginInfo>>();
		}

		/// <summary>
		/// 企业列表
		/// </summary>
		/// <returns></returns>
		public async Task<ResponseBase<List<BrandInfo>>> GetCompanyList()
		{
			var _queryDic = new SortedDictionary<string, string>();
			_queryDic.Add("login_brand", Session.LoginInfo.BrandId.ToString());
			_queryDic.Add("login_cbo", Session.LoginInfo.UserId.ToString());
			var _result = await NetClient.GetAsync(GetCompanyList_V1, queryDictionary: _queryDic);
			return _result.FromJson<ResponseBase<List<BrandInfo>>>();
		}
	}
}
