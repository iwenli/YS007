using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core.Entities.Response
{
	public class TokenResponse : Base<Token>
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

	public class Token
	{
		/// <summary>
		/// Token
		/// </summary>
		public string AccessToken { get; set; }

		public string UserId { get; set; }
	}
}
