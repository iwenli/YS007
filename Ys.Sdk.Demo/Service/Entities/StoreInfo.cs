using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Service.Entities
{
	public class StoreInfo
	{
		#region 属性
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("store_id")]
		public int StoreId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("brand_id")]
		public int BrandId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("f_img")]
		public string FImg { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("area")]
		public int Area { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("area1")]
		public int Area1 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("area2")]
		public int Area2 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("address")]
		public string Address { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("bmapX")]
		public string BmapX { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("bmapY")]
		public string BmapY { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("phone")]
		public string Phone { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("link_json")]
		public string LinkJson { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("summary")]
		public string Summary { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("addtime")]
		public DateTime Addtime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("store_name")]
		public string StoreName { get; set; }
		/// <summary>
		/// 门店类型  1直营店  2加盟店
		/// </summary>
		[JsonProperty("store_type")]
		public int StoreType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("is_top")]
		public int IsTop { get; set; }
		/// <summary>
		/// 状态：0、等待审核，1、通过审核 ，2、未通过
		/// </summary>
		[JsonProperty("is_check")]
		public int IsCheck { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("sort")]
		public int Sort { get; set; }
		/// <summary>
		/// 审核备注
		/// </summary>
		[JsonProperty("check_remark")]
		public string CheckRemark { get; set; }
		/// <summary>
		/// 关联相册图片字段
		/// </summary>
		[JsonProperty("album_imgid")]
		public int AlbumImgid { get; set; }
		/// <summary>
		/// 是否已发布到主站 默认未发布
		/// </summary>
		[JsonProperty("main_publish")]
		public bool MainPublish { get; set; }
		/// <summary>
		/// 发布至主站的时间 默认空
		/// </summary>
		[JsonProperty("main_publish_time")]
		public DateTime MainPublishTime { get; set; }
		/// <summary>
		/// 营业开始时间
		/// </summary>
		[JsonProperty("open_time")]
		public string OpenTime { get; set; }
		/// <summary>
		/// 营业结束时间
		/// </summary>
		[JsonProperty("close_time")]
		public string CloseTime { get; set; }
		/// <summary>
		/// O2O配送开始时间
		/// </summary>
		[JsonProperty("delivery_starttime")]
		public string DeliveryStarttime { get; set; }
		/// <summary>
		/// O2O配送结束时间
		/// </summary>
		[JsonProperty("delivery_endtime")]
		public string DeliveryEndtime { get; set; }
		/// <summary>
		/// 信用积分门槛
		/// </summary>
		[JsonProperty("credit_limit")]
		public int CreditLimit { get; set; }
		/// <summary>
		/// 是否核查过营业时间
		/// </summary>
		[JsonProperty("is_checkopenclosetime")]
		public int IsCheckopenclosetime { get; set; }
		/// <summary>
		/// 店内设备状态 0安装中 1调试中 2正式运行(监控) 3已关闭 4正式运行(不监控)
		/// </summary>
		[JsonProperty("device_runstatus")]
		public int DeviceRunstatus { get; set; }
		/// <summary>
		///营业状态  0装修调试、1打码上货、2开店试营、3正式营业、4关门闭店  
		/// </summary>
		[JsonProperty("manage_status")]
		public int ManageStatus { get; set; }
		#endregion
	}
}
