using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Txooo.Extension;
using Txooo.Text;
using Ys.Sdk.Demo.Common;
using Ys.Sdk.Demo.Network;

namespace Ys.Sdk.Demo.Service
{
	/// <summary>
	/// http请求客户端
	/// </summary>
	public class HttpSignClient : NetClient
	{
		/// <summary>
		/// 
		/// </summary>
		public string SecretId { get; } = "AKIDns368wrkt3545ybh7e11g3e0mcbcji7o8iwi";
		/// <summary>
		/// 
		/// </summary>
		public string SecretKey { get; } = "9D682kmyWLe9m7z3BJg49D5iLu8s4xk76y1LbW3g";
		/// <summary>
		/// 请求来源标识
		/// </summary>
		public string Source => "WinForm";

		/// <summary>
		/// Api签名校验md5值 和请求方约定的值
		/// </summary>
		public string ApiSignMd5 => @"06D36D63A578ACA4FE63C093ABBD2E1E";

		public HttpSignClient()
		{
			var _signDic = new Dictionary<string, string>();
			_signDic.Add("Date", DateTime.Now.ToString("r"));
			_signDic.Add("Source", $"{Source}|{typeof(HttpSignClient).FullName}|{Assembly.GetExecutingAssembly().GetName().Version}");

			var _sign = Sign(SecretKey, _signDic);
			var _headers = string.Join(" ", _signDic.Keys.Select(m => m.ToLower()));
			var _authorization = $"hmac id=\"{SecretId}\", algorithm=\"hmac-sha1\", headers=\"{_headers}\", signature=\"{_sign}\"";
			DefaultRequestHeaders.TryAddWithoutValidation("Authorization", _authorization);
			DefaultRequestHeaders.TryAddWithoutValidation("Source", _signDic["Source"]);
			DefaultRequestHeaders.Date = Convert.ToDateTime(_signDic["Date"]);
		}
		public async Task<string> PostAsync(string url,
			string contentType = "application/x-www-form-urlencoded",
			int timeOut = 30,
			SortedDictionary<string, string> queryDictionary = null,
			SortedDictionary<string, string> bodyDictionary = null,
			bool isGzip = false)
		{
			return await SendAndReadAsStringAsync(url, HttpMethod.Post, contentType, timeOut, queryDictionary, bodyDictionary, null, isGzip);
		}

		public async Task<string> GetAsync(string url,
			string contentType = "application/x-www-form-urlencoded",
			int timeOut = 30,
			SortedDictionary<string, string> queryDictionary = null,
			bool isGzip = true)
		{
			return await SendAndReadAsStringAsync(url, HttpMethod.Get, contentType, timeOut, queryDictionary, null, null, isGzip);
		}

		/// <summary>
		/// http 请求
		/// </summary>
		/// <param name="url">请求url</param>
		/// <param name="method">请求类型
		/// <param name="contentType">request content_type</param>
		/// <param name="queryDictionary">query集合</param>
		/// <param name="bodyDictionary">body集合</param>
		/// <returns></returns>
		public async Task<string> SendAndReadAsStringAsync(string url,
			HttpMethod method,
			string contentType = "application/x-www-form-urlencoded",
			int timeOut = 30,
			SortedDictionary<string, string> queryDictionary = null,
			SortedDictionary<string, string> bodyDictionary = null,
			SortedDictionary<string, string> headerDictionary = null,
			bool isGzip = false)
		{
			var _response = await GetResponse(url, method, contentType, timeOut, queryDictionary, bodyDictionary, headerDictionary, isGzip);
			if (_response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				var _errMsg = $"请求：{_response.RequestMessage.ToString()}失败，状态码：{(int)_response.StatusCode},内容:{await _response.Content.ReadAsStringAsync()}";
				this.AppendDebugLog(_errMsg);
				return _errMsg;
			}
			if (!isGzip)
			{
				return await _response.Content.ReadAsStringAsync();
			}
			else
			{
				var _result = await _response.Content.ReadAsByteArrayAsync();
				return Encoding.UTF8.GetString(ZipHelper.Decompress(_result));
			}
		}

		/// <summary>
		/// http 请求
		/// </summary>
		/// <param name="url">请求url</param>
		/// <param name="method">请求类型
		/// <param name="contentType">request content_type</param>
		/// <param name="queryDictionary">query集合</param>
		/// <param name="bodyDictionary">body集合</param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetResponse(string url,
			HttpMethod method,
			string contentType = "application/x-www-form-urlencoded",
			int timeOut = 30,
			SortedDictionary<string, string> queryDictionary = null,
			SortedDictionary<string, string> bodyDictionary = null,
			SortedDictionary<string, string> headerDictionary = null,
			bool isGzip = false)
		{
			var _request = new HttpRequestMessage(method, url);

			Timeout = new TimeSpan(0, 0, 1000 * timeOut);
			DefaultRequestHeaders.TryAddWithoutValidation("gzip_type", isGzip ? "1" : "0");
			if (headerDictionary != null)
			{
				foreach (var dic in headerDictionary)
				{
					DefaultRequestHeaders.TryAddWithoutValidation(dic.Key, dic.Value);
				}
			}

			queryDictionary = queryDictionary ?? new SortedDictionary<string, string>();
			queryDictionary.Add("timestamp", DateTime.Now.ConvertToTimeStamp(false).ToString());
			var _waitSignDic = new SortedDictionary<string, string>(queryDictionary);
			if (bodyDictionary != null && bodyDictionary.Count > 0)
			{
				foreach (var dic in bodyDictionary)
				{
					_waitSignDic.Add(dic.Key, dic.Value);
				}
				_request.Content = new FormUrlEncodedContent(bodyDictionary);
			}
			queryDictionary.Add("sign", EncryptHelper.MD5(string.Join("&", _waitSignDic.Select(m => $"{ m.Key}={_waitSignDic[m.Key]}")) + "&" + ApiSignMd5));
			var _queryString = string.Join("&", queryDictionary.Select(m => $"{ m.Key}={queryDictionary[m.Key]}"));

			_request.RequestUri = new Uri($"{url}?{_queryString}");

			return await SendAsync(_request);
		}
		private string Sign(string secret, IDictionary<string, string> dic)
		{
			StringBuilder _signStr = new StringBuilder();
			foreach (var item in dic)
			{
				_signStr.Append($"{item.Key.ToLower()}: {item.Value}\n");
			}
			_signStr.Remove(_signStr.Length - 1, 1);
			return EncryptHelper.HMACSHA1(secret, _signStr.ToString());
		}
	}
}
