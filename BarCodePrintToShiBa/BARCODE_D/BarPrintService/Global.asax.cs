using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Sunlike.Business;

namespace BarPrintService
{
	public class Global : System.Web.HttpApplication
	{

		protected void Application_Start(object sender, EventArgs e)
		{
            Comp.CompNo = ConfigurationManager.AppSettings["DatabaseName"];
            //Comp.SetScCompNo("4e870172967346a1ac1e651d53be0a56");
			Comp.Conn_SunSystem = Comp.GetSunSystemConnectionString();
            //Comp.Conn_DB = Comp.GetDBConnectionString(Comp.CompNo);
			//SpcPgm的默认连接
            //ConnInfo.SetScCompNo(ConfigurationManager.AppSettings["DatabaseName"]);
            //取得Database的语言别
            CodeConv.GetDatabaseLanguage();
            Comp.DRP_Prop["BarPrintDB"] = ConfigurationManager.AppSettings["BarPrintDB"];
            try
            {
                Comp.Conn_DB = Comp.GetDBConnectionString(Comp.CompNo);
            }
            catch { }
            Comp.Conn_DB = Comp.Conn_SunSystem.Replace("sunsystem", "DB_" + Comp.CompNo);
            //SpcPgm的默认连接
            ConnInfo.Conn_DB = Comp.Conn_DB;

            #region DRP参数
            Comp.DRP_Prop["AUTO_UNBOX"] = "T";//自动拆箱
            Comp.DRP_Prop["BOX_SET_SHOWALL"] = "T";//当装箱内容为零时是否显示
            Comp.DRP_Prop["CUST_BILL_DATE"] = "1";//帐龄基准日
            Comp.DRP_Prop["DRPYH_BOX"] = "F";//按箱要货[要货申请]
            Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"] = "0";//要货/受订库存控制方式（0：按可用库存要货/受订，1：按实际库存要货/受订）
            Comp.DRP_Prop["DRPYH_CHK_UPNULL"] = "F";//要货价格不允许为0
            Comp.DRP_Prop["DRPYH_ORDER_LOWQTY"] = "F";//可用库存不足接受[要货申请]
            Comp.DRP_Prop["DRPYH_PRDSETTING"] = "0";//要货产品依据（0：所有可用产品，1：产品中类）
            Comp.DRP_Prop["DRPYH_PRDSETTING_IDX"] = "";//产品中类值
            Comp.DRP_Prop["DRPYI_CHK_QTY"] = "F";//可用库存不足接受[退货申请]
            Comp.DRP_Prop["DRPYI_PRDSETTING"] = "0";//可退货产品依据（0：所有可用产品，1：客户现有产品，2：严格序列号管理）
            Comp.DRP_Prop["DRPYI_WH"] = "0000";//退货库位设定
            Comp.DRP_Prop["DATA_DYNAMICS_ARLIC"] = "Sunlike,Sunlike,DD-APN-10-E000802,TYB2A2PB5YM7Q53AJ72J";
            Comp.DRP_Prop["DATE_PATTERN"] = "yyyy-MM-dd";//日期格式
            Comp.DRP_Prop["DEF_BACC"] = "";//集团费用预设帐户
            Comp.DRP_Prop["DEF_ERACC"] = "";//客户费用预设会计科目
            Comp.DRP_Prop["NEWS_TIME"] = "60000";//信息刷新时间（单位：毫秒）
            Comp.DRP_Prop["PSWD_LIFE"] = "";//密码有效期
            Comp.DRP_Prop["PSWD_LIMITING"] = "1";//密码内容限制（1：无限制，2：数字、英文混合）
            Comp.DRP_Prop["PSWD_MIN_LENGTH"] = "";//密码最小长度
            Comp.DRP_Prop["REP_PMARK_WIDTH"] = "1";//特征栏位宽度
            Comp.DRP_Prop["RETURN_ACCN"] = "";//返让利预设帐户科目				
            Comp.DRP_Prop["SMTP_TYPE"] = "BASE64";//SMTP服务器是否需要验证。BASE64：base64;PLAIN：明码;NONE：不需要验证
            Comp.DRP_Prop["SMTP_SERVER"] = "";//SMTP服务器地址
            Comp.DRP_Prop["SMTP_PORT"] = "";//SMTP服务器端口号
            Comp.DRP_Prop["SMTP_USER"] = "";//SMTP服务器用户名
            Comp.DRP_Prop["SMTP_PSWD"] = "";//SMTP服务器用户密码
            Comp.DRP_Prop["SMTP_SENDADDRESS"] = "";//邮件发送地址
            Comp.DRP_Prop["SMTP_RECADDRESS"] = "";//邮件接收地址
            Comp.DRP_Prop["SMTP_HTML"] = "F";//发送方式
            Comp.DRP_Prop["SMTP_SENDADDRESS"] = "";//邮件发送地址
            Comp.DRP_Prop["SMTP_RECADDRESS"] = "";//邮件接收地址
            Comp.DRP_Prop["SMTP_INSERVER"] = "";//邮件发送内部地址
            Comp.DRP_Prop["SMTP_OUTSERVER"] = "";//邮件发送外部地址
            Comp.DRP_Prop["TIME_PATTERN"] = "HH:mm:ss";//时间格式
            Comp.DRP_Prop["PRD_TYPE"] = "DRP";//产品别
            Comp.DRP_Prop["RPTDATALIMIT"] = 10000;//报表行数限定
            Comp.DRP_Prop["TI_FROM_INPUT"] = "T";//入库单可以手动登打
            Comp.DRP_Prop["TI_FROM_PO"] = "1";//入库单可以从采购单转入
            Comp.DRP_Prop["TI_FROM_TW"] = "1";//入库单可以从收料计划单转入
            Comp.DRP_Prop["TI_OUT_QTY"] = "F";//入库单允许货品超交
            Comp.DRP_Prop["TI_OUT_QTY_CNT"] = "";//入库单超交上限百分比
            Comp.DRP_Prop["AUTO_ALERT"] = "F";//是否即时预警
            Comp.DRP_Prop["ALERT_DATESPAN"] = "Day";//日期过滤范围--当天/当周/当月/当年
            Comp.DRP_Prop["AUTO_RUNAUDIT"] = "T";//进入审核中心是否立刻过滤数据
            Comp.DRP_Prop["CONTROL_BOX_QTY"] = "T";//箱量控管否
            Comp.DRP_Prop["FIRST_WINFORM"] = "T";//单据追踪优先使用WINFORM
            Comp.DRP_Prop["BAT_USE"] = "F";//启用批号
            Comp.DRP_Prop["DATA_VERSION"] = 0;//数据库维护版本号
            Comp.DRP_Prop["DRPYH_WH_SHOW"] = "T";//是否显示库存
            Comp.DRP_Prop["DRPYH_WH_QTY"] = 100;//库存量控管
            Comp.DRP_Prop["DRPYH_WH_VIEWUNDER"] = "T";//是否含下属库位
            Comp.DRP_Prop["DRPYC_GETPRICE"] = "";//一般要货取价格模式
            Comp.DRP_Prop["AUTO_INSERTSOYH"] = "T";//是否直接转受订（分销要货）
            Comp.DRP_Prop["AUTO_INSERTSO"] = "F";//是否直接转受订（新品要货/一般要货）
            Comp.DRP_Prop["DAYPRDT1_TIMESPAN"] = "";//历史库存统计表（日余额档日期区间）
            Comp.DRP_Prop["MON_RT"] = "SO";
            Comp.DRP_Prop["MON_SORT"] = "0";//对帐单排序规则
            Comp.DRP_Prop["MON_JH"] = "F";
            Comp.DRP_Prop["MON_POWER"] = "F";//对帐单金额等显示栏位是否受权限控管
            Comp.DRP_Prop["CONTINUEADD"] = "F";//要货/销货新增后是否清空货品代号
            Comp.DRP_Prop["JH_OVERDATE"] = "";//反稽核截止日
            Comp.DRP_Prop["DRPYH_GETPRICE"] = "7";
            Comp.DRP_Prop["CHINESECONVERT_SWITCH"] = "F";//简繁资料转换开关
            Comp.DRP_Prop["DRPPS_DD_CANMODIFY"] = "F";//销货单(web)修改日期
            Comp.DRP_Prop["DRPPS_UP_CANMODIFY"] = "F";//销货单(web)修改单价
            Comp.DRP_Prop["CFM_SW_CONTROL"] = "3";//用户选择
            Comp.DRP_Prop["DRPEA_AMTN_CONTROL"] = "3";//费用申请资源余额不足管制
            Comp.DRP_Prop["UPDATE_NEWS"] = "F";//更新新闻资料
            Comp.DRP_Prop["DRPPS_DRPME_SA"] = "F";//销货自动转营销费用计算
            Comp.DRP_Prop["DRPPS_DRPME_SB"] = "F";//销退自动转营销费用计算
            Comp.DRP_Prop["DRPPS_DRPME_SD"] = "F";//销折自动转营销费用计算
            Comp.DRP_Prop["DRPEA_IDX_SINGLE"] = "F";//营销费用申请单一费用项目
            Comp.DRP_Prop["DRPEA_CUST_ISNULL"] = "T";//营销费用申请单客户允许为空否
            Comp.DRP_Prop["DRPSA_GETPRICE"] = "7";//销货单(Web)取价方式
            Comp.DRP_Prop["DRPSA_VOH_ID"] = "";//销货单(Web)凭证模版
            Comp.DRP_Prop["DFTYF_PRDT"] = "";//默认运费货品代号
            Comp.DRP_Prop["DRPSB_GETPRICE"] = "7";//销货退回单(Web)取价方式
            Comp.DRP_Prop["DRPSB_VOH_ID"] = "";//销货退回单(Web)凭证模版
            Comp.DRP_Prop["DRPPS_SHOWPRICE"] = "T";//是否使用[销售单价]和[销售总价]栏位

            #region 交货方式资源设定
            //中文名称
            Comp.DRP_Prop["SEND_MTH1"] = "";
            Comp.DRP_Prop["SEND_MTH2"] = "";
            Comp.DRP_Prop["SEND_MTH3"] = "";
            Comp.DRP_Prop["SEND_MTH4"] = "";
            Comp.DRP_Prop["SEND_MTH5"] = "";
            Comp.DRP_Prop["SEND_MTH6"] = "";
            //英文名称
            Comp.DRP_Prop["SEND_MTHEN1"] = "";
            Comp.DRP_Prop["SEND_MTHEN2"] = "";
            Comp.DRP_Prop["SEND_MTHEN3"] = "";
            Comp.DRP_Prop["SEND_MTHEN4"] = "";
            Comp.DRP_Prop["SEND_MTHEN5"] = "";
            Comp.DRP_Prop["SEND_MTHEN6"] = "";
            #endregion
            #endregion
		}

		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}