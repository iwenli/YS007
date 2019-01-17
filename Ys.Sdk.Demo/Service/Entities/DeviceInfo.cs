using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txooo.Extension.Extension;
using Ys.Sdk.Demo.Service.Entities.Ys;

namespace Ys.Sdk.Demo.Service.Entities
{
	public class DeviceInfo
	{
		#region 属性
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("id")]
		public int Id { get; set; }
		/// <summary>
		/// 门店id
		/// </summary>
		[JsonProperty("store_id")]
		public int StoreId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("brand_id")]
		public long BrandId { get; set; }
		/// <summary>
		/// 设备编号
		/// </summary>
		[JsonProperty("device_info")]
		public string Info { get; set; }
		/// <summary>
		/// 设备类型 1代表监控  2代表智能门禁  3代表身份验证 4代表结算台 5只进门禁 6只出门禁
		/// </summary>
		[JsonProperty("device_type")]
		public int DeviceType { get; set; }
		/// <summary>
		/// 0、关闭，1、正常，2、检修，3、异常
		/// </summary>
		[JsonProperty("device_state")]
		public int State { get; set; }
		/// <summary>
		/// 设备中文名称
		/// </summary>
		[JsonProperty("device_name")]
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("add_time")]
		public DateTime AddTime { get; set; }
		/// <summary>
		/// 设备最后检测在线时间
		/// </summary>
		[JsonProperty("online_time")]
		public DateTime OnlineTime { get; set; }
		/// <summary>
		/// 设备配置信息
		/// </summary>
		[JsonProperty("device_cfg")]
		public string Cfg { get; set; }
		/// <summary>
		/// mqqt配置信息
		/// </summary>
		[JsonProperty("mqqt_cfg")]
		public string MqqtCfg { get; set; }
		/// <summary>
		/// 读写器配置信息
		/// </summary>
		[JsonProperty("reader_cfg")]
		public string ReaderCfg { get; set; }
		/// <summary>
		/// IO控制器配置信息
		/// </summary>
		[JsonProperty("ioc_cfg")]
		public string IocCfg { get; set; }
		/// <summary>
		/// 是否锁定上（1锁，0末锁）
		/// </summary>
		[JsonProperty("is_lock")]
		public int IsLock { get; set; }
		/// <summary>
		/// 是否删除（1删除，0未删除）
		/// </summary>
		[JsonProperty("is_delete")]
		public int IsDelete { get; set; }
		/// <summary>
		/// 对应vmall_device_version v_id
		/// </summary>
		[JsonProperty("version_info_id")]
		public int VersionInfoId { get; set; }
		/// <summary>
		/// 是否链接到Broker
		/// </summary>
		[JsonProperty("online")]
		public bool Online { get; set; }
		/// <summary>
		/// 设备mac地址
		/// </summary>
		[JsonProperty("device_mac")]
		public string DeviceMac { get; set; }
		/// <summary>
		/// 当前设备运行的软件版本
		/// </summary>
		[JsonProperty("software_version")]
		public string SoftwareVersion { get; set; }
		/// <summary>
		/// 设备异常代码 
		/// 0：			表示正常 
		/// 2：			[计算台]读写器断开链接
		/// 100001：		[门禁]外门错误
		/// 100002：		[门禁]门禁内门错误
		/// 100004:		[门禁]13信号错误[地毯]
		/// 100008：		[门禁]13信号错误[红外]
		/// 100016：		[门禁]按下门铃
		/// 100032：		[门禁]交流电错误
		/// 100064：		[门禁]电池错误
		/// 100128：		[门禁]紧急开关开启
		/// 100256：		[门禁]紧急开启流程
		/// </summary>
		[JsonProperty("err_code")]
		public int ErrCode { get; set; }
		/// <summary>
		/// 出入口设备关联监控通道 0-8 为有效值
		/// </summary>
		[JsonProperty("channel_no")]
		public int ChannelNo { get; set; }
		#endregion

		List<string> m_errList;
		/// <summary>
		/// 异常列表
		/// </summary>
		[JsonProperty("err_list")]
		public List<string> ErrList
		{
			get
			{
				if (m_errList.IsNull())
				{
					m_errList = new List<string>();
					if (ErrCode == 0)
					{
						m_errList.Add("正常");
					}
					else if (ErrCode == 2)
					{
						m_errList.Add("读写器断开链接");
					}
					else if (ErrCode > 100000)
					{
						var _errCode = ErrCode - 100000;
						if ((_errCode & (int)DoorErrorType.Error_1) == (int)DoorErrorType.Error_1)
						{
							m_errList.Add("外门错误");
						}
						if ((_errCode & (int)DoorErrorType.Error_2) == (int)DoorErrorType.Error_2)
						{
							m_errList.Add("内门错误");
						}
						if ((_errCode & (int)DoorErrorType.Error_3) == (int)DoorErrorType.Error_3)
						{
							m_errList.Add("13信号错误[地毯]");
						}
						if ((_errCode & (int)DoorErrorType.Error_4) == (int)DoorErrorType.Error_4)
						{
							m_errList.Add("13信号错误[红外]");
						}
						if ((_errCode & (int)DoorErrorType.Error_5) == (int)DoorErrorType.Error_5)
						{
							m_errList.Add("按下门铃");
						}
						if ((_errCode & (int)DoorErrorType.Error_6) == (int)DoorErrorType.Error_6)
						{
							m_errList.Add("交流电错误");
						}
						if ((_errCode & (int)DoorErrorType.Error_7) == (int)DoorErrorType.Error_7)
						{
							m_errList.Add("电池错误");
						}
						if ((_errCode & (int)DoorErrorType.Error_8) == (int)DoorErrorType.Error_8)
						{
							m_errList.Add("紧急开关开启");
						}
						if ((_errCode & (int)DoorErrorType.Error_9) == (int)DoorErrorType.Error_9)
						{
							m_errList.Add("紧急开门");
						}
					}
					else
					{
						m_errList.Add("未知异常");
					}
				}
				return m_errList;
			}
		}

		/// <summary>
		/// 监控设备的监控集合
		/// </summary>
		public List<CameraInfo> CameraList { get; set; } = new List<CameraInfo>();
	}

	/// <summary>
	/// 门禁设备异常类型
	/// </summary>
	enum DoorErrorType
	{
		/// <summary>
		/// 外门错误
		/// </summary>
		Error_1 = 1,
		/// <summary>
		/// 内门错误
		/// </summary>
		Error_2 = 1 << 1,
		/// <summary>
		/// 13信号错误[地毯]
		/// </summary>
		Error_3 = 1 << 2,
		/// <summary>
		/// 13信号错误[红外]
		/// </summary>
		Error_4 = 1 << 3,
		/// <summary>
		/// 按下门铃
		/// </summary>
		Error_5 = 1 << 4,
		/// <summary>
		/// 交流电错误
		/// </summary>
		Error_6 = 1 << 5,
		/// <summary>
		/// 电池错误
		/// </summary>
		Error_7 = 1 << 6,
		/// <summary>
		/// 紧急开关开启
		/// </summary>
		Error_8 = 1 << 7,
		/// <summary>
		/// 紧急开启流程
		/// </summary>
		Error_9 = 1 << 8,
	}
}
