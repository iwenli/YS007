using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core.V2
{
	public enum AlarmType
	{
		/// <summary>
		/// 全部
		/// </summary>
		ALARM_TYPE_ALL,
		/// <summary>
		/// 人体感应事件
		/// </summary>
		BODY_SENSOR_EVENT,

		/// <summary>
		/// 紧急遥控按钮事件
		/// </summary>
		EMERGENCY_BUTTON_EVENT,

		/// <summary>
		/// 移动侦测告警
		/// </summary>
		MOTION_DETECT_ALARM,

		/// <summary>
		/// 婴儿啼哭告警
		/// </summary>
		BABY_CRY_ALARM,

		/// <summary>
		/// 门磁告警
		/// </summary>
		MAGNETIC_ALARM,

		/// <summary>
		/// 烟感告警
		/// </summary>
		SMOKE_DETECTOR_ALARM,

		/// <summary>
		/// 可燃气体告警
		/// </summary>
		COMBUSTIBLE_GAS_ALARM,

		/// <summary>
		/// 水浸告警
		/// </summary>
		FLOOD_IN_ALARM,

		/// <summary>
		/// 紧急按钮告警
		/// </summary>
		EMERGENCY_BUTTON_ALARM,

		/// <summary>
		/// 人体感应告警
		/// </summary>
		BODY_SENSOR_ALARM,

		/// <summary>
		/// 遮挡告警
		/// </summary>
		SHELTER_ALARM,

		/// <summary>
		/// 视频丢失
		/// </summary>
		VIDEO_LOSS_ALARM,

		/// <summary>
		/// 越界侦测
		/// </summary>
		LINE_DETECTION_ALARM,

		/// <summary>
		/// 区域入侵
		/// </summary>
		FIELD_DETECTION_ALARM,

		/// <summary>
		/// 人脸检测事件
		/// </summary>
		FACE_DETECTION_ALARM,

		/// <summary>
		/// 智能门铃告警
		/// </summary>
		DOOR_BELL_ALARM,

		/// <summary>
		/// 摄像机失去关联告警
		/// </summary>
		DEVOFFLINE_ALARM,

		/// <summary>
		/// 幕帘告警
		/// </summary>
		CURTAIN_ALARM,

		/// <summary>
		/// 单体门磁告警
		/// </summary>
		MOVE_MAGNETOMETER_ALARM,

		/// <summary>
		/// 场景变更侦测
		/// </summary>
		SCENE_CHANGE_DETECTION_ALARM,

		/// <summary>
		/// 虚焦侦测
		/// </summary>
		DEFOCUS_ALARM,

		/// <summary>
		/// 音频异常侦测
		/// </summary>
		AUDIO_EXCEPTION_ALARM,

		/// <summary>
		/// 物品遗留侦测
		/// </summary>
		LEFT_DETECTION_ALARM,

		/// <summary>
		/// 物品拿取侦测
		/// </summary>
		TAKE_DETECTION_ALARM,

		/// <summary>
		/// 非法停车侦测
		/// </summary>
		PARKING_DETECTION_ALARM,

		/// <summary>
		/// 人员聚集侦测
		/// </summary>
		HIGH_DENSITY_DETECTION_ALARM,

		/// <summary>
		/// 徘徊检测侦测
		/// </summary>
		LOITER_DETECTION_ALARM,

		/// <summary>
		/// 快速移动侦测
		/// </summary>
		RUN_DETECTION_ALARM,

		/// <summary>
		/// 进入区域侦测
		/// </summary>
		ENTER_AREA_DETECTION_ALARM,

		/// <summary>
		/// 离开区域侦测
		/// </summary>
		EXIT_AREA_DETECTION_ALARM,

		/// <summary>
		/// 磁干扰告警
		/// </summary>
		MAG_GIM_ALARM,


		/// <summary>
		/// 电池欠压告警
		/// </summary>
		UNDER_VOLTAGE_ALARM,

		/// <summary>
		/// 闯入告警
		/// </summary>
		INTRUSION_ALARM,

		/// <summary>
		/// IO告警
		/// </summary>
		IO_00_ALARM,

		/// <summary>
		/// IO-1告警
		/// </summary>
		IO_01_ALARM,

		/// <summary>
		/// IO-2告警
		/// </summary>
		IO_02_ALARM,

		/// <summary>
		/// IO-3告警
		/// </summary>
		IO_03_ALARM,

		/// <summary>
		/// IO-4告警
		/// </summary>
		IO_04_ALARM,


		/// <summary>
		/// IO-5告警
		/// </summary>
		IO_05_ALARM,


		/// <summary>
		/// IO-6告警
		/// </summary>
		IO_06_ALARM,


		/// <summary>
		/// IO-7告警
		/// </summary>
		IO_07_ALARM,


		/// <summary>
		/// IO-8告警
		/// </summary>
		IO_08_ALARM,


		/// <summary>
		/// IO-9告警
		/// </summary>
		IO_09_ALARM,

		/// <summary>
		/// IO-10告警
		/// </summary>
		IO_10_ALARM,

		/// <summary>
		/// IO-11告警
		/// </summary>
		IO_11_ALARM,


		/// <summary>
		/// IO-12告警
		/// </summary>
		IO_12_ALARM,


		/// <summary>
		/// IO-13告警
		/// </summary>
		IO_13_ALARM,


		/// <summary>
		/// IO-14告警
		/// </summary>
		IO_14_ALARM,

		/// <summary>
		/// IO-15告警
		/// </summary>
		IO_15_ALARM,


		/// <summary>
		/// IO-16告警
		/// </summary>
		IO_16_ALARM,

	}
}
