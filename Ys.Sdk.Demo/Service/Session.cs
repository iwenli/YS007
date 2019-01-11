using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txooo.Extension.Extension;
using Ys.Sdk.Demo.Network;
using Ys.Sdk.Demo.Service.Entities.User;

namespace Ys.Sdk.Demo.Service
{
	/// <summary>
	/// 表示当前的登录会话，以及一些必须的状态信息。
	/// </summary>
	public class Session
	{
		#region 字段
		#endregion

		#region 属性
		/// <summary>
		/// 获得当前使用的网络对象，每个网络对象都是会话关联的。
		/// </summary>
		public HttpSignClient NetClient => new HttpSignClient();
		/// <summary>
		/// 获得当前的登录信息
		/// </summary>
		public LoginInfo LoginInfo { get; private set; }

		bool _isLogined;
		/// <summary>
		/// 获得当前是否已经登录
		/// </summary>
		public bool IsLogined
		{
			get { return _isLogined; }
			private set
			{
				if (_isLogined == value)
					return;

				_isLogined = value;
				OnLoginStateChange();
			}
		}
		#endregion

		#region 事件
		/// <summary>
		/// 登陆状态变更事件
		/// </summary>
		public event EventHandler LoginStateChange;

		/// <summary>
		/// 引发 <see cref="LoginStateChange" /> 事件
		/// </summary>
		protected virtual void OnLoginStateChange()
		{
			LoginStateChange?.Invoke(this, EventArgs.Empty);
		}

		#endregion

		public Session()
		{
			//NetClient = new HttpSignClient();
		}


		/// <summary>
		/// 登陆
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="verify"></param>
		/// <param name="mobileCode"></param>
		/// <returns></returns>
		public async Task<Exception> LoginAsync(ServiceContext serviceContext,string mobile, string verify, string mobileCode)
		{
			IsLogined = false;
			try
			{
				var _result = await serviceContext.PassportService.Login(mobile, verify, mobileCode);
				//登陆失败
				if (!_result.IsSuccess)
				{
					return new Exception(_result.Msg);
				}
				//登陆成功
				LoginInfo = new LoginInfo
				{
					UserId = _result.Data.UserId,
					BrandId = _result.Data.BrandId,
					ComId = _result.Data.ComId,
					Token = _result.Data.Token,
					Mobile = _result.Data.Mobile
				};
				var _companyList = await serviceContext.PassportService.GetCompanyList();
				if (_companyList.IsSuccess)
				{
					LoginInfo.BrandList.AddRange(_companyList.Data);
				}
				IsLogined = true;
			}
			catch (Exception ex)
			{
				return ex;
			}
			return null;
		}

		internal void Logout()
		{
			LoginInfo = null;
			IsLogined = false;
		}
	}
}
