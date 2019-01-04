using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Service.Entities.User
{
	/// <summary>
	/// 加盟商信息
	/// </summary>
	public class BrandInfo
	{
		/// <summary>
		/// 加盟商id
		/// </summary>
		public int BrandId { set; get; }
		/// <summary>
		/// 加盟商
		/// </summary>
		public string BrandName { set; get; }
		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName { set; get; }
		/// <summary>
		/// 角色
		/// </summary>
		public string Role { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string Logo { set; get; }
	}
}
