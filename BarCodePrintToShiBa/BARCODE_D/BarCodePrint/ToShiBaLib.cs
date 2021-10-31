/*BAR_PRTNAME��TYPE_ID���IDX��ӡ��ʽ
    * ��ӡ��ʽ��LH\TS\BASEC,BASIC\SAMPLE\SAMLL\FOXCONN\OTHER\HQ\SBB��
    *      �����Ϊ��0����ģ�ʱ��
    *          LH����ӡ�Ϻţ������������������ӡ�ġ��Ϻš�������Դ�ڱ�BAR_PRTNAME��IDX_NO=�����NAME�ֶΣ�
    *          TS������棨��Ʒ�Ź�����������ӡ�ġ�Ʒ����������Դ�ڱ�BAR_PRTNAME��PRD_NO=Ʒ�ŵ�NAME�ֶΣ�
    *          BASIC����׼�棬����ӡ�ġ�Ʒ����������Դ�ڻ�Ʒ������ϲ�������ţ��˴�����ά������Ϊ��׼���ӡ����Ϣȫ������ϵͳ��
    *      �����Ϊ��2�����룩ʱ��
    *          BASIC����׼�棬����ӡ�ġ�Ʒ����������Դ�ڻ�Ʒ������ϲ�������ţ��˴�����ά������Ϊ��׼���ӡ����Ϣȫ������ϵͳ��
    *          SAMPLE����Լ�棨���Ʊ�׼�棩
    *          SMALL����ӡ��ţ����Ʊ�׼�棩
    *          FOXCONN����ʿ���棨��Ӧ�̣��̶�Ϊ����Ʒ�����ֹ��޸ģ�Ʒ���̶�Ϊ��Ƭ�ġ����ֹ��޸ģ���Ρ���ʿ���Ϻš���Ʒ�������ֱ���Դ�ڻ�Ʒ���������Զ����ֶΡ�BC_GP������LH_FOXCONN������QTY_PER_GP�������ʣ���Դ��������������Զ����ֶΡ�CZ_GP������
    *          OTHER������棨��Ʒ�Ź�����������ӡ�ġ�Ʒ����������Դ�ڱ�BAR_PRTNAME��PRD_NO=Ʒ�ŵ�NAME�ֶΣ�
    *          SBB���ܰر̰棨�����������������ӡ�ġ���Ʒ��š�������Դ�ڱ�BAR_PRTNAME��IDX_NO=Ʒ�ŵ�NAME�ֶΣ�
    *          HQ������棨ͬ����棩��
    */
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;
using System.Net.Sockets;
using System.Net;

namespace BarCodePrintTSB
{
    public class ToShiBaLib
    {
        [DllImport("TSCLIB.DLL")]
        private static extern void openport(string PrinterName);
        [DllImport("TSCLIB.DLL")]
        private static extern void closeport();
        [DllImport("TSCLIB.DLL")]
        private static extern void sendcommand(string command);
        [DllImport("TSCLIB.DLL")]
        private static extern void setup(string LabelWidth, string LabelHeight, string Speed, string Density, string Sensor, string Vertical, string Offset);
        [DllImport("TSCLIB.DLL")]
        private static extern void downloadpcx(string Filename, string ImageName);
        [DllImport("TSCLIB.DLL")]
        private static extern void barcode(string X, string Y, string CodeType, string Height, string Readable, string rotation, string Narrow, string Wide, string Code);
        [DllImport("TSCLIB.DLL")]
        private static extern void printerfont(string X, string Y, string FontName, string rotation, string Xmul, string Ymul, string Content);
        [DllImport("TSCLIB.DLL")]
        private static extern void clearbuffer();
        [DllImport("TSCLIB.DLL")]
        private static extern void printlabel(string NumberOfSet, string NumberOfCopy);
        [DllImport("TSCLIB.DLL")]
        private static extern void formfeed();
        [DllImport("TSCLIB.DLL")]
        private static extern void nobackfeed();
        [DllImport("TSCLIB.DLL")]
        private static extern void windowsfont(Int32 X, Int32 Y, Int32 fontheight, Int32 rotation, Int32 fontstyle, Int32 fontunderline, string FaceName, string TextContent);

		/// <summary>
		/// ��ӡ����
		/// </summary>
		/// <param name="_dr">��ӡ����</param>
		/// <param name="_prdtType">�������(EX:0����ģ�1����Ʒ��2�����䣻3���Է�)</param>
        /// <param name="_format">��ӡģ��(EX:BASIC����׼�棻FOXCONN����ʿ��)</param>
		public void barCodePrint(DataRow _dr, string _prdtType, string _format)
        {
            BarSet _bs = new BarSet();
            string _port = _bs.GetPrintSet(System.Windows.Forms.Application.ExecutablePath + ".config");
			if (_prdtType == "0")
            {
                #region ���
                if (_format == "LH")
                {
                    //��ģ���ӡ�Ϻţ�
                    string _bar_no = _dr["BARCODE"].ToString();
                    string _idx = _dr["LH"].ToString();
                    string _prdt = "�Ϻţ� " + _idx;
                    //string _spc = "��� " + _dr["PRD_SPC"].ToString();
                    string _bat_no = "���ţ� " + _dr["PRD_MARK"].ToString() + "-";
                    string _date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    openport(_port);//�򿪴�ӡ��
                    setup("100", "50", "3", "12", "0", "2.5", "0");//�����ǩ�Ĳ����ͻ�������
                    clearbuffer();
                    windowsfont(85, 30, 60, 0, 0, 0, "�꿬��", _prdt);//Ʒ��
                    //windowsfont(85, 100, 60, 0, 0, 0, "�꿬��", _spc);//���
                    windowsfont(85, 170, 60, 0, 0, 0, "�꿬��", _bat_no + _dr["INIT_NO"].ToString());//����
                    barcode("180", "240", "128", "50", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //����ָ��
                    printerfont("180", "290", "3", "0", "1", "1", _bar_no.Replace(BarRole.TrimChar, "") + _dr["INIT_NO"].ToString());//��������
                    windowsfont(250, 330, 40, 0, 0, 0, "�꿬��", _date);//����
                    printlabel("1", "1");//�趨��ӡ���ʽ��(set)���趨��ӡ������(copy)
                    closeport();//�رմ�ӡ��
                }
                else if (_format == "TS")
                {
                    //��ģ�����棩
                    string _bar_no = _dr["BARCODE"].ToString();
                    string _idx = _dr["IDX_NAME"].ToString();
                    string _prdt = "Ʒ���� " + _idx;
                    string _spc = "��� " + _dr["PRD_SPC"].ToString();
                    string _bat_no = "���ţ� " + _dr["PRD_MARK"].ToString() + "-";
                    string _date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    openport(_port);//�򿪴�ӡ��
                    setup("100", "50", "3", "12", "0", "2.5", "0");//�����ǩ�Ĳ����ͻ�������
                    clearbuffer();
                    windowsfont(85, 30, 60, 0, 0, 0, "�꿬��", _prdt);//Ʒ��
                    windowsfont(85, 100, 60, 0, 0, 0, "�꿬��", _spc);//���
                    windowsfont(85, 170, 60, 0, 0, 0, "�꿬��", _bat_no + _dr["INIT_NO"].ToString());//����
                    barcode("180", "240", "128", "50", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //����ָ��
                    printerfont("180", "290", "3", "0", "1", "1", _bar_no.Replace(BarRole.TrimChar, "") + _dr["INIT_NO"].ToString());//��������
                    windowsfont(250, 330, 40, 0, 0, 0, "�꿬��", _date);//����
                    printlabel("1", "1");//�趨��ӡ���ʽ��(set)���趨��ӡ������(copy)
                    closeport();//�رմ�ӡ��
                }
                else
                {
                    //��ģ���׼�棩
                    string _bar_no = _dr["BARCODE"].ToString();
                    //int _pageCount = 1;//�������
                    //if (_dr["PAGE_COUNT"].ToString() != "")
                    //    _pageCount = Convert.ToInt32(_dr["PAGE_COUNT"]);
                    string _idx = _dr["IDX_NAME"].ToString();
                    string _prdt = "Ʒ���� " + _idx;
                    string _spc = "��� " + _dr["PRD_SPC"].ToString();
                    string _bat_no = "���ţ� " + _dr["PRD_MARK"].ToString() + "-";
                    string _date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    openport(_port);//�򿪴�ӡ��
                    setup("100", "50", "3", "12", "0", "2.5", "0");//�����ǩ�Ĳ����ͻ�������
                    clearbuffer();
                    windowsfont(85, 30, 60, 0, 0, 0, "�꿬��", _prdt);//Ʒ��
                    windowsfont(85, 100, 60, 0, 0, 0, "�꿬��", _spc);//���
                    windowsfont(85, 170, 60, 0, 0, 0, "�꿬��", _bat_no + _dr["INIT_NO"].ToString());//����
                    barcode("180", "240", "128", "50", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //����ָ��
                    printerfont("180", "290", "3", "0", "1", "1", _bar_no.Replace(BarRole.TrimChar, "") + _dr["INIT_NO"].ToString());//��������
                    windowsfont(250, 330, 40, 0, 0, 0, "�꿬��", _date);//����
                    printlabel("1", "1");//�趨��ӡ���ʽ��(set)���趨��ӡ������(copy)
                    closeport();//�رմ�ӡ��
                }
                #endregion
            }
			if (_prdtType == "1")
			{
                #region �������
                string _curPos = _dr["INIT_NO"].ToString();//��ˮ��
                string _bar_no = _dr["BARCODE"].ToString().Replace(BarRole.TrimChar, "") + _curPos;
                string _barCode = _dr["BARCODE"].ToString() + _curPos;
                string _prdt = _dr["IDX_NAME"].ToString();
                string _spc = _dr["PRD_SPC"].ToString();

				openport(_port);
				setup("70", "24", "3", "12", "0", "2.5", "0");//�����ǩ�Ĳ����ͻ�������
				clearbuffer();
				windowsfont(5, 10, 35, 0, 0, 0, "�꿬��", _bar_no);
				windowsfont(5, 45, 35, 0, 0, 0, "�꿬��", _spc);
				barcode("5", "105", "128", "40", "0", "0", "2", "4", _barCode); //����ָ��
                printerfont("5", "155", "3", "0", "1", "1", _bar_no);
				printlabel("1", "1");
				closeport();
                #endregion
            }
            if (_prdtType == "2")
            {
                #region ������
                if (_format == "BASIC")
                {
                    //�����루��׼�棩
                    //int _pageCount = 1;//����
                    //if (_dr["PAGE_COUNT"].ToString() != "")
                    //    _pageCount = Convert.ToInt32(_dr["PAGE_COUNT"]);
                    string _idx = _dr["BOX_IDX_NAME"].ToString();
                    string _bar_no = _dr["BARCODE"].ToString();//���к�
                    string _prdt = "Ʒ���� " + _idx;
                    string _qty = _dr["BOX_QTY"].ToString() + "��";
                    string _spc = "��� " + _dr["BOX_SPC"].ToString();
                    string _bat_no = "���ţ� " + _dr["BOX_BAT"].ToString();

                    openport(_port);
                    setup("100", "50", "3", "12", "0", "2.5", "0");//�����ǩ�Ĳ����ͻ�������
                    clearbuffer();
                    windowsfont(200, 20, 45, 0, 0, 0, "�꿬��", _dr["COMP_NAME"].ToString());
                    windowsfont(100, 70, 30, 0, 0, 0, "�꿬��", _dr["COMP_ENAME"].ToString());
                    windowsfont(50, 110, 40, 0, 0, 0, "�꿬��", _prdt);
                    windowsfont(600, 110, 40, 0, 0, 0, "�꿬��", _qty);
                    windowsfont(50, 160, 40, 0, 0, 0, "�꿬��", _spc);
                    windowsfont(50, 210, 40, 0, 0, 0, "�꿬��", _bat_no);
                    barcode("250", "250", "128", "40", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //����ָ��
                    printerfont("250", "290", "3", "0", "1", "1", _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString());
                    windowsfont(30, 330, 30, 0, 0, 0, "�꿬��", _dr["COMP_ADR"].ToString());
                    windowsfont(500, 315, 30, 0, 0, 0, "�꿬��", "TEL�� " + _dr["COMP_TEL"].ToString());
                    windowsfont(500, 345, 30, 0, 0, 0, "�꿬��", "FAX�� " + _dr["COMP_FAX"].ToString());
                    printlabel("1", "1");
                    closeport();
                }
                if (_format == "SAMPLE")
                {
                    //������(��Լ��)
                    string _idx = _dr["BOX_IDX_NAME"].ToString();
                    string _bar_no = _dr["BARCODE"].ToString();//���к�
                    string _prdt = "Ʒ���� " + _idx;
                    string _qty = _dr["BOX_QTY"].ToString() + "��";
                    string _spc = "��� " + _dr["BOX_SPC"].ToString();
                    string _bat_no = "���ţ� " + _dr["BOX_BAT"].ToString();

                    openport(_port);
                    setup("100", "50", "3", "12", "0", "2.5", "0");//�����ǩ�Ĳ����ͻ�������
                    clearbuffer();
                    windowsfont(100, 50, 60, 0, 0, 0, "�꿬��", _prdt);
                    windowsfont(700, 50, 60, 0, 0, 0, "�꿬��", _qty);
                    windowsfont(100, 120, 60, 0, 0, 0, "�꿬��", _spc);
                    windowsfont(100, 190, 60, 0, 0, 0, "�꿬��", _bat_no);
                    barcode("250", "270", "128", "80", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //����ָ��
                    printerfont("250", "360", "3", "0", "1", "1", _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString());
                    printlabel("1", "1");
                    closeport();
                }
                if (_format == "SMALL")
                {
                    //�����루��ӡ��ţ�
                    string _bar_no = _dr["BARCODE"].ToString();//���к�
                    openport(_port);
                    setup("70", "24", "3", "12", "0", "2.5", "0");//�����ǩ�Ĳ����ͻ�������
                    clearbuffer();
                    barcode("40", "105", "128", "40", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //����ָ��
                    printerfont("40", "155", "3", "0", "1", "1", _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString());
                    printlabel("1", "1");
                    closeport();
                }
                if (_format == "OTHER" || _format == "HQ")
                {
                    //�����루�����棩
                    //int _pageCount = 1;//����
                    //if (_dr["PAGE_COUNT"].ToString() != "")
                    //    _pageCount = Convert.ToInt32(_dr["PAGE_COUNT"]);
                    string _idx = _dr["BOX_IDX_NAME"].ToString();
                    string _bar_no = _dr["BARCODE"].ToString();//���к�
                    string _prdt = "Ʒ���� " + _idx;
                    string _qty = _dr["BOX_QTY"].ToString() + "��";
                    string _spc = "��� " + _dr["BOX_SPC"].ToString();
                    string _bat_no = "���ţ� " + _dr["BOX_BAT"].ToString();

                    openport(_port);
                    setup("100", "50", "3", "12", "0", "2.5", "0");//�����ǩ�Ĳ����ͻ�������
                    clearbuffer();
                    windowsfont(200, 20, 45, 0, 0, 0, "�꿬��", _dr["COMP_NAME"].ToString());
                    windowsfont(100, 70, 30, 0, 0, 0, "�꿬��", _dr["COMP_ENAME"].ToString());
                    windowsfont(50, 110, 40, 0, 0, 0, "�꿬��", _prdt);
                    windowsfont(600, 110, 40, 0, 0, 0, "�꿬��", _qty);
                    windowsfont(50, 160, 40, 0, 0, 0, "�꿬��", _spc);
                    windowsfont(50, 210, 40, 0, 0, 0, "�꿬��", _bat_no);
                    barcode("250", "250", "128", "40", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //����ָ��
                    printerfont("250", "290", "3", "0", "1", "1", _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString());
                    windowsfont(30, 330, 30, 0, 0, 0, "�꿬��", _dr["COMP_ADR"].ToString());
                    windowsfont(500, 315, 30, 0, 0, 0, "�꿬��", "TEL�� " + _dr["COMP_TEL"].ToString());
                    windowsfont(500, 345, 30, 0, 0, 0, "�꿬��", "FAX�� " + _dr["COMP_FAX"].ToString());
                    printlabel("1", "1");
                    closeport();
                }
                if (_format == "FOXCONN")
                {
                    //�����루��ʿ���棩
                    barCodePrintNew(_dr);
                    //string _bar_no = _dr["BARCODE"].ToString();//���к�
                    //string _line11 = "��  Ӧ  ��    " + _dr["FOX_SUP"].ToString(); string _line12 = "�ġ����� ��    " + _dr["FOX_CZ"].ToString();
                    //string _line21 = "Ʒ������    " + _dr["FOX_PRD_NAME"].ToString(); string _line22 = "�桡���� ��    " + _dr["BOX_SPC"].ToString();
                    //string _line31 = "�桡����    " + _dr["FOX_BC"].ToString(); string _line32 = "Foxconn�Ϻ�    " + _dr["FOX_LH"].ToString();
                    //string _line41 = "��������    " + _dr["FOX_BAT_MANU"].ToString(); string _line42 = "ԭ  �� ��  ��    " + _dr["FOX_BAT_MAT"].ToString();
                    //string _qty = "����(��)      " + _dr["BOX_QTY"].ToString();
                    //string _weight = "����(KG)    " + _dr["FOX_WEIGHT"].ToString();
                    //string _bat_no = "׷������    " + _dr["BOX_BAT"].ToString();

                    //openport(_port);
                    //setup("105", "95", "3", "12", "0", "2.5", "0");//�����ǩ�Ĳ����ͻ�������
                    //clearbuffer();
                    //windowsfont(50, 60, 40, 0, 0, 0, "�꿬��", _line11);
                    //windowsfont(380, 60, 40, 0, 0, 0, "�꿬��", _line12);

                    ////printerfont("50", "50", "1", "0", "1", "1", "-");
                    ////printerfont("50", "50", "1", "90", "1", "1", "-");
                    //windowsfont(50, 130, 40, 0, 0, 0, "�꿬��", _line21);
                    //windowsfont(380, 130, 40, 0, 0, 0, "�꿬��", _line22);
                    //windowsfont(50, 200, 40, 0, 0, 0, "�꿬��", _line31);
                    //windowsfont(380, 200, 40, 0, 0, 0, "�꿬��", _line32);
                    //windowsfont(50, 270, 40, 0, 0, 0, "�꿬��", _line41);
                    //windowsfont(380, 270, 40, 0, 0, 0, "�꿬��", _line42);
                    //windowsfont(50, 340, 40, 0, 0, 0, "�꿬��", _qty);
                    //windowsfont(50, 410, 40, 0, 0, 0, "�꿬��", _weight);
                    //windowsfont(50, 480, 40, 0, 0, 0, "�꿬��", _bat_no);
                    //windowsfont(50, 550, 40, 0, 0, 0, "�꿬��", "��������");
                    //barcode("250", "640", "128", "40", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //����ָ��
                    //printerfont("250", "680", "3", "0", "1", "1", _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString());
                    //printlabel("1", "1");
                    //closeport();
                }
                if (_format == "SBB")
                {
                    //�����루������-��Ʒ��ţ�
                    //int _pageCount = 1;//����
                    //if (_dr["PAGE_COUNT"].ToString() != "")
                    //    _pageCount = Convert.ToInt32(_dr["PAGE_COUNT"]);
                    string _idx = _dr["BOX_IDX_NAME"].ToString();
                    string _bar_no = _dr["BARCODE"].ToString();//���к�
                    string _prdt = "Ʒ�������� " + _idx;
                    string _qty = _dr["BOX_QTY"].ToString() + "��";
                    string _spc = "��Ʒ��ţ� " + _dr["BOX_LH"].ToString();
                    string _bat_no = "�������ţ� " + _dr["BOX_BAT"].ToString();
                    string _spcno = "P/O�� " + _dr["SPC_NAME"].ToString();

                    openport(_port);
                    setup("100", "50", "3", "12", "0", "2.5", "0");//�����ǩ�Ĳ����ͻ�������
                    clearbuffer();
                    windowsfont(200, 20, 45, 0, 0, 0, "�꿬��", _dr["COMP_NAME"].ToString());
                    windowsfont(100, 70, 30, 0, 0, 0, "�꿬��", _dr["COMP_ENAME"].ToString());
                    windowsfont(50, 110, 40, 0, 0, 0, "�꿬��", _prdt);
                    windowsfont(600, 110, 40, 0, 0, 0, "�꿬��", _qty);
                    windowsfont(50, 160, 40, 0, 0, 0, "�꿬��", _spc);
                    windowsfont(50, 210, 40, 0, 0, 0, "�꿬��", _bat_no);
                    windowsfont(350, 210, 40, 0, 0, 0, "�꿬��", _spcno);
                    barcode("250", "250", "128", "40", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //����ָ��
                    printerfont("250", "290", "3", "0", "1", "1", _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString());
                    windowsfont(30, 330, 30, 0, 0, 0, "�꿬��", _dr["COMP_ADR"].ToString());
                    windowsfont(500, 315, 30, 0, 0, 0, "�꿬��", "TEL�� " + _dr["COMP_TEL"].ToString());
                    windowsfont(500, 345, 30, 0, 0, 0, "�꿬��", "FAX�� " + _dr["COMP_FAX"].ToString());
                    printlabel("1", "1");
                    closeport();
                }
                #endregion
            }
            if (_prdtType == "3")
            {
                #region ���Ʒ
                string _bar_no = _dr["BARCODE"].ToString();
                string _idx = _dr["IDX_NAME"].ToString();
                string _prdt = "Ʒ���� " + _idx;
                string _spc = "��� " + _dr["PRD_SPC"].ToString();
                string _bat_no = "���ţ� " + _dr["PRD_MARK"].ToString() + "-";
                string _date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                openport(_port);//�򿪴�ӡ��
                setup("100", "50", "3", "12", "0", "2.5", "0");//�����ǩ�Ĳ����ͻ�������
                clearbuffer();
                windowsfont(85, 30, 60, 0, 0, 0, "�꿬��", _prdt);//Ʒ��
                windowsfont(85, 100, 60, 0, 0, 0, "�꿬��", _spc);//���
                windowsfont(85, 170, 60, 0, 0, 0, "�꿬��", _bat_no + _dr["INIT_NO"].ToString().Substring(1));//����
                barcode("180", "240", "128", "50", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //����ָ��
                printerfont("180", "290", "3", "0", "1", "1", _bar_no.Replace(BarRole.TrimChar, "") + _dr["INIT_NO"].ToString());//��������
                windowsfont(250, 330, 40, 0, 0, 0, "�꿬��", _date);//����
                printlabel("1", "1");//�趨��ӡ���ʽ��(set)���趨��ӡ������(copy)
                closeport();//�رմ�ӡ��
                #endregion
            }
        }

        public void barCodePrintNew(DataRow _dr)
        {

            BarSet _bs = new BarSet();
            string fileName = System.Windows.Forms.Application.ExecutablePath + ".config";
            string _ip = _bs.GetPrintSet(fileName, "PrintSetIP");
            string _port = _bs.GetPrintSet(fileName, "PrintSetPort");

            String ipPort = "";
            //��������
            IPAddress ipa = IPAddress.Parse(_ip);
            IPEndPoint ipe = new IPEndPoint(ipa, int.Parse(_port));
            Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soc.Connect(ipe);


            //string str= "hello,123456789,��Һ�! ";



            //�����루��ʿ���棩
            string _bar_no = _dr["BARCODE"].ToString();//���к�
            string _line11 = "�� Ӧ ��   " + _dr["FOX_SUP"].ToString(); string _line12 = "�ġ����� ��   " + _dr["FOX_CZ"].ToString();
            string _line21 = "Ʒ������   " + _dr["FOX_PRD_NAME"].ToString(); string _line22 = "�桡���� ��   " + _dr["BOX_SPC"].ToString();
            string _line31 = "�桡����   " + _dr["FOX_BC"].ToString(); string _line32 = "Foxconn�Ϻ� " + _dr["FOX_LH"].ToString();
            string _line41 = "��������   " + _dr["FOX_BAT_MANU"].ToString(); string _line42 = "ԭ �� �� ��   " + _dr["FOX_BAT_MAT"].ToString();
            string _qty = "����(��)   " + _dr["BOX_QTY"].ToString();
            string _weight = "����(KG)   " + _dr["FOX_WEIGHT"].ToString();
            string _bat_no = "׷������   " + _dr["BOX_BAT"].ToString();

            StringBuilder str = new StringBuilder();
            str.Append("{D1150,0950,1050|}").Append('\n');
            str.Append("{C|}").Append('\n');

            str.Append("{PC000;0950,0780,20,20,e,22,B,J0101=" + _line11 + "|}").Append('\n');
            str.Append("{PC000;0550,0780,20,20,e,22,B,J0101=" + _line12 + "|}").Append('\n');

            str.Append("{PC000;0950,0700,20,20,e,22,B,J0101=" + _line21 + "|}").Append('\n');
            str.Append("{PC000;0550,0700,20,20,e,22,B,J0101=" + _line22 + "|}").Append('\n');

            str.Append("{PC000;0950,0620,20,20,e,22,B,J0101=" + _line31 + "|}").Append('\n');
            str.Append("{PC000;0550,0620,20,20,e,22,B,J0101=" + _line32 + "|}").Append('\n');

            str.Append("{PC000;0950,0540,20,20,e,22,B,J0101=" + _line41 + "|}").Append('\n');
            str.Append("{PC000;0550,0540,20,20,e,22,B,J0101=" + _line42 + "|}").Append('\n');

            str.Append("{PC000;0950,0460,20,20,e,22,B,J0101=" + _qty + "|}").Append('\n');
            str.Append("{PC000;0950,0380,20,20,e,22,B,J0101=" + _weight + "|}").Append('\n');

            str.Append("{PC000;0950,0300,20,20,e,22,B,J0101=" + _bat_no + "|}").Append('\n');
            str.Append("{PC000;0950,0220,20,20,e,22,B,J0101=��������|}").Append('\n');

            str.Append("{XB00;0300,0100,9,1,04,0,0080,+0000000000,000,0,00=" + _bar_no + _dr["INIT_NO"].ToString() + "|}").Append('\n');
            
            str.Append("{PC000;0600,0060,15,15,e,22,B=" +  _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString() + "|}").Append('\n');

            str.Append("{XS;I,0001,0002C6111|}").Append('\n');

            byte[] b = System.Text.Encoding.GetEncoding("GB2312").GetBytes(str.ToString());
            soc.Send(b);
            soc.Close();
        }
    }

}
