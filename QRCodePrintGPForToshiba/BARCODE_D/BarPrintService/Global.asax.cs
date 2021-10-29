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
			//SpcPgm��Ĭ������
            //ConnInfo.SetScCompNo(ConfigurationManager.AppSettings["DatabaseName"]);
            //ȡ��Database�����Ա�
            CodeConv.GetDatabaseLanguage();
            Comp.DRP_Prop["BarPrintDB"] = ConfigurationManager.AppSettings["BarPrintDB"];
            try
            {
                Comp.Conn_DB = Comp.GetDBConnectionString(Comp.CompNo);
            }
            catch { }
            Comp.Conn_DB = Comp.Conn_SunSystem.Replace("sunsystem", "DB_" + Comp.CompNo);
            //SpcPgm��Ĭ������
            ConnInfo.Conn_DB = Comp.Conn_DB;

            #region DRP����
            Comp.DRP_Prop["AUTO_UNBOX"] = "T";//�Զ�����
            Comp.DRP_Prop["BOX_SET_SHOWALL"] = "T";//��װ������Ϊ��ʱ�Ƿ���ʾ
            Comp.DRP_Prop["CUST_BILL_DATE"] = "1";//�����׼��
            Comp.DRP_Prop["DRPYH_BOX"] = "F";//����Ҫ��[Ҫ������]
            Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"] = "0";//Ҫ��/�ܶ������Ʒ�ʽ��0�������ÿ��Ҫ��/�ܶ���1����ʵ�ʿ��Ҫ��/�ܶ���
            Comp.DRP_Prop["DRPYH_CHK_UPNULL"] = "F";//Ҫ���۸�����Ϊ0
            Comp.DRP_Prop["DRPYH_ORDER_LOWQTY"] = "F";//���ÿ�治�����[Ҫ������]
            Comp.DRP_Prop["DRPYH_PRDSETTING"] = "0";//Ҫ����Ʒ���ݣ�0�����п��ò�Ʒ��1����Ʒ���ࣩ
            Comp.DRP_Prop["DRPYH_PRDSETTING_IDX"] = "";//��Ʒ����ֵ
            Comp.DRP_Prop["DRPYI_CHK_QTY"] = "F";//���ÿ�治�����[�˻�����]
            Comp.DRP_Prop["DRPYI_PRDSETTING"] = "0";//���˻���Ʒ���ݣ�0�����п��ò�Ʒ��1���ͻ����в�Ʒ��2���ϸ����кŹ���
            Comp.DRP_Prop["DRPYI_WH"] = "0000";//�˻���λ�趨
            Comp.DRP_Prop["DATA_DYNAMICS_ARLIC"] = "Sunlike,Sunlike,DD-APN-10-E000802,TYB2A2PB5YM7Q53AJ72J";
            Comp.DRP_Prop["DATE_PATTERN"] = "yyyy-MM-dd";//���ڸ�ʽ
            Comp.DRP_Prop["DEF_BACC"] = "";//���ŷ���Ԥ���ʻ�
            Comp.DRP_Prop["DEF_ERACC"] = "";//�ͻ�����Ԥ���ƿ�Ŀ
            Comp.DRP_Prop["NEWS_TIME"] = "60000";//��Ϣˢ��ʱ�䣨��λ�����룩
            Comp.DRP_Prop["PSWD_LIFE"] = "";//������Ч��
            Comp.DRP_Prop["PSWD_LIMITING"] = "1";//�����������ƣ�1�������ƣ�2�����֡�Ӣ�Ļ�ϣ�
            Comp.DRP_Prop["PSWD_MIN_LENGTH"] = "";//������С����
            Comp.DRP_Prop["REP_PMARK_WIDTH"] = "1";//������λ���
            Comp.DRP_Prop["RETURN_ACCN"] = "";//������Ԥ���ʻ���Ŀ				
            Comp.DRP_Prop["SMTP_TYPE"] = "BASE64";//SMTP�������Ƿ���Ҫ��֤��BASE64��base64;PLAIN������;NONE������Ҫ��֤
            Comp.DRP_Prop["SMTP_SERVER"] = "";//SMTP��������ַ
            Comp.DRP_Prop["SMTP_PORT"] = "";//SMTP�������˿ں�
            Comp.DRP_Prop["SMTP_USER"] = "";//SMTP�������û���
            Comp.DRP_Prop["SMTP_PSWD"] = "";//SMTP�������û�����
            Comp.DRP_Prop["SMTP_SENDADDRESS"] = "";//�ʼ����͵�ַ
            Comp.DRP_Prop["SMTP_RECADDRESS"] = "";//�ʼ����յ�ַ
            Comp.DRP_Prop["SMTP_HTML"] = "F";//���ͷ�ʽ
            Comp.DRP_Prop["SMTP_SENDADDRESS"] = "";//�ʼ����͵�ַ
            Comp.DRP_Prop["SMTP_RECADDRESS"] = "";//�ʼ����յ�ַ
            Comp.DRP_Prop["SMTP_INSERVER"] = "";//�ʼ������ڲ���ַ
            Comp.DRP_Prop["SMTP_OUTSERVER"] = "";//�ʼ������ⲿ��ַ
            Comp.DRP_Prop["TIME_PATTERN"] = "HH:mm:ss";//ʱ���ʽ
            Comp.DRP_Prop["PRD_TYPE"] = "DRP";//��Ʒ��
            Comp.DRP_Prop["RPTDATALIMIT"] = 10000;//���������޶�
            Comp.DRP_Prop["TI_FROM_INPUT"] = "T";//��ⵥ�����ֶ��Ǵ�
            Comp.DRP_Prop["TI_FROM_PO"] = "1";//��ⵥ���ԴӲɹ���ת��
            Comp.DRP_Prop["TI_FROM_TW"] = "1";//��ⵥ���Դ����ϼƻ���ת��
            Comp.DRP_Prop["TI_OUT_QTY"] = "F";//��ⵥ�����Ʒ����
            Comp.DRP_Prop["TI_OUT_QTY_CNT"] = "";//��ⵥ�������ްٷֱ�
            Comp.DRP_Prop["AUTO_ALERT"] = "F";//�Ƿ�ʱԤ��
            Comp.DRP_Prop["ALERT_DATESPAN"] = "Day";//���ڹ��˷�Χ--����/����/����/����
            Comp.DRP_Prop["AUTO_RUNAUDIT"] = "T";//������������Ƿ����̹�������
            Comp.DRP_Prop["CONTROL_BOX_QTY"] = "T";//�����عܷ�
            Comp.DRP_Prop["FIRST_WINFORM"] = "T";//����׷������ʹ��WINFORM
            Comp.DRP_Prop["BAT_USE"] = "F";//��������
            Comp.DRP_Prop["DATA_VERSION"] = 0;//���ݿ�ά���汾��
            Comp.DRP_Prop["DRPYH_WH_SHOW"] = "T";//�Ƿ���ʾ���
            Comp.DRP_Prop["DRPYH_WH_QTY"] = 100;//������ع�
            Comp.DRP_Prop["DRPYH_WH_VIEWUNDER"] = "T";//�Ƿ�������λ
            Comp.DRP_Prop["DRPYC_GETPRICE"] = "";//һ��Ҫ��ȡ�۸�ģʽ
            Comp.DRP_Prop["AUTO_INSERTSOYH"] = "T";//�Ƿ�ֱ��ת�ܶ�������Ҫ����
            Comp.DRP_Prop["AUTO_INSERTSO"] = "F";//�Ƿ�ֱ��ת�ܶ�����ƷҪ��/һ��Ҫ����
            Comp.DRP_Prop["DAYPRDT1_TIMESPAN"] = "";//��ʷ���ͳ�Ʊ������������䣩
            Comp.DRP_Prop["MON_RT"] = "SO";
            Comp.DRP_Prop["MON_SORT"] = "0";//���ʵ��������
            Comp.DRP_Prop["MON_JH"] = "F";
            Comp.DRP_Prop["MON_POWER"] = "F";//���ʵ�������ʾ��λ�Ƿ���Ȩ�޿ع�
            Comp.DRP_Prop["CONTINUEADD"] = "F";//Ҫ��/�����������Ƿ���ջ�Ʒ����
            Comp.DRP_Prop["JH_OVERDATE"] = "";//�����˽�ֹ��
            Comp.DRP_Prop["DRPYH_GETPRICE"] = "7";
            Comp.DRP_Prop["CHINESECONVERT_SWITCH"] = "F";//������ת������
            Comp.DRP_Prop["DRPPS_DD_CANMODIFY"] = "F";//������(web)�޸�����
            Comp.DRP_Prop["DRPPS_UP_CANMODIFY"] = "F";//������(web)�޸ĵ���
            Comp.DRP_Prop["CFM_SW_CONTROL"] = "3";//�û�ѡ��
            Comp.DRP_Prop["DRPEA_AMTN_CONTROL"] = "3";//����������Դ�������
            Comp.DRP_Prop["UPDATE_NEWS"] = "F";//������������
            Comp.DRP_Prop["DRPPS_DRPME_SA"] = "F";//�����Զ�תӪ�����ü���
            Comp.DRP_Prop["DRPPS_DRPME_SB"] = "F";//�����Զ�תӪ�����ü���
            Comp.DRP_Prop["DRPPS_DRPME_SD"] = "F";//�����Զ�תӪ�����ü���
            Comp.DRP_Prop["DRPEA_IDX_SINGLE"] = "F";//Ӫ���������뵥һ������Ŀ
            Comp.DRP_Prop["DRPEA_CUST_ISNULL"] = "T";//Ӫ���������뵥�ͻ�����Ϊ�շ�
            Comp.DRP_Prop["DRPSA_GETPRICE"] = "7";//������(Web)ȡ�۷�ʽ
            Comp.DRP_Prop["DRPSA_VOH_ID"] = "";//������(Web)ƾ֤ģ��
            Comp.DRP_Prop["DFTYF_PRDT"] = "";//Ĭ���˷ѻ�Ʒ����
            Comp.DRP_Prop["DRPSB_GETPRICE"] = "7";//�����˻ص�(Web)ȡ�۷�ʽ
            Comp.DRP_Prop["DRPSB_VOH_ID"] = "";//�����˻ص�(Web)ƾ֤ģ��
            Comp.DRP_Prop["DRPPS_SHOWPRICE"] = "T";//�Ƿ�ʹ��[���۵���]��[�����ܼ�]��λ

            #region ������ʽ��Դ�趨
            //��������
            Comp.DRP_Prop["SEND_MTH1"] = "";
            Comp.DRP_Prop["SEND_MTH2"] = "";
            Comp.DRP_Prop["SEND_MTH3"] = "";
            Comp.DRP_Prop["SEND_MTH4"] = "";
            Comp.DRP_Prop["SEND_MTH5"] = "";
            Comp.DRP_Prop["SEND_MTH6"] = "";
            //Ӣ������
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