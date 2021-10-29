using System;
using System.Data;
using System.Configuration;
using Sunlike.Common.CommonVar;
using Sunlike.Business.Data;
using Sunlike.Business;
using Sunlike.Common.Utility;
using System.Data.SqlClient;
using System.Text;
using Microsoft.VisualBasic;

namespace BarPrintService
{
	public class BussBarPrint : BizObject
	{
        public BussBarPrint()
        {
            if (!Comp.DRP_Prop.Contains("BarPrintDB") || Comp.DRP_Prop["BarPrintDB"].ToString() == "")
                Comp.DRP_Prop["BarPrintDB"] = ConfigurationManager.AppSettings["BarPrintDB"];
        }

		#region �����ӡ��Ϣ
		/// <summary>
		/// �����ӡ��Ϣ
        /// ----��BAR_SQNO�ı���˵����
        /// ----1��[PRD_NO]:������룺ֻ������Ʒ�ŵ�1��3��4λ
        ///        [PRD_NO]:��Ʒ�Ͱ��Ʒ���룺�����Ʒ������Ʒ��
        ///     2��[PRD_MARK]:�洢����
		/// </summary>
		/// <param name="usr">����</param>
		/// <param name="printHistroyDS">�����¼DataSet</param>
        /// <param name="_flag">�Ƿ����Ʒ</param>
        /// <param name="_change">�Ƿ񻻻�</param>
        /// <param name="_modify">�Ƿ��޸�:���ΪFalse����ʾҪ���������ˮ��</param>
		/// <returns></returns>
		public DataSet SavePrintData(string usr, DataSet printHistroyDS, bool _flag,bool _change, bool _modify)
		{
            SunlikeDataSet _ds = null;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                //base.EnterTransaction();
                try
                {
                    Bat _bat = new Bat();
                    _ds = _bat.GetData("1<>1");//ȡ���ſ�Schema
                    BarCode _bc = new BarCode();
                    _ds.Merge(_bc.GetBarRecord("1<>1", false));//�������¼
                    _ds.Tables["BAR_REC"].Columns.Add("BAR_NO1", System.Type.GetType("System.String"));
                    _ds.Tables["BAR_REC"].Columns.Add("SN", System.Type.GetType("System.String"));
                    _ds.Tables["BAR_REC"].Columns.Add("DEP", System.Type.GetType("System.String"));
                    _ds.Tables["BAR_REC"].Columns.Add("IDX_NAME", System.Type.GetType("System.String"));
                    _ds.Tables["BAR_REC"].Columns.Add("FORMAT", System.Type.GetType("System.String"));
                    _ds.Tables["BAR_REC"].Columns.Add("LH", System.Type.GetType("System.String"));
                    _ds.Tables["BAR_REC"].Columns.Add("PRT_NAME", System.Type.GetType("System.String"));
                    _ds.Tables["BAR_REC"].Columns.Add("FLAG", System.Type.GetType("System.String"));//�ڳ���������ӡ����ǣ�
                    _ds.Tables["BAR_REC"].Columns.Add("ISCHANGE", System.Type.GetType("System.String"));//���������ӡ����ǣ�
                    _ds.Tables["BAR_REC"].Columns.Add("ISMODIFY", System.Type.GetType("System.String"));//�޸����루��ǣ���---˵�����Ƿ��޸�:���ΪFalse����ʾҪ���������ˮ��
                    _ds.Tables["BAR_REC"].Columns.Add("USR", System.Type.GetType("System.String"));
                    _ds.Tables["BAR_REC"].Columns.Add("OS_BAR_NO", System.Type.GetType("System.String"));

                    if (printHistroyDS.Tables[0].Columns.Contains("PRD_NO_SNM"))
                    {
                        //��������ӡʱ����¼�����1��3��4λ
                        _ds.Tables["BAR_REC"].Columns.Add("PRD_NO_SNM", System.Type.GetType("System.String"));
                    }

                    string _bar_no = "";
                    string _prdNo = "";
                    string _prd_mark = "";
                    string _sn = "";
                    string _dep = "";
                    string _sqlWhere = "";
                    int _maxSn = 0;

                    for (int i = 0; i < printHistroyDS.Tables[0].Rows.Count; i++)
                    {
                        if (printHistroyDS.Tables[0].Rows[i].RowState != DataRowState.Deleted && _maxSn < Convert.ToInt32(printHistroyDS.Tables[0].Rows[i]["SN"]))
                        {
                            _maxSn = Convert.ToInt32(printHistroyDS.Tables[0].Rows[i]["SN"]);
                        }
                    }
                    _ds.ExtendedProperties["MAX_SN"] = _maxSn;

                    for (int i = 0; i < printHistroyDS.Tables[0].Rows.Count; i++)
                    {
                        if (printHistroyDS.Tables[0].Rows[i].RowState != DataRowState.Deleted)
                        {
                            _bar_no = printHistroyDS.Tables[0].Rows[i]["BAR_NO"].ToString();//��������ˮ��
                            _prdNo = printHistroyDS.Tables[0].Rows[i]["PRD_NO1"].ToString();
                            _prd_mark = printHistroyDS.Tables[0].Rows[i]["PRD_MARK1"].ToString();

                            _sn = printHistroyDS.Tables[0].Rows[i]["SN"].ToString();
                            _dep = printHistroyDS.Tables[0].Rows[i]["DEP"].ToString();

                            #region ����BAR_REC
                            _sqlWhere = " BAR_NO='" + _bar_no + _sn + "' ";
                            DataTable _dtBarRec = _bc.GetBarRecord(_sqlWhere, false);
                            if (_dtBarRec.Rows.Count <= 0)
                            {
                                DataRow _dr = _ds.Tables["BAR_REC"].NewRow();
                                _dr["BAR_NO"] = _bar_no + _sn;
                                _dr["PRD_NO"] = _prdNo;
                                _dr["BAT_NO"] = _prd_mark;
                                _dr["BAR_NO1"] = _bar_no;
                                _dr["SN"] = _sn;
                                _dr["DEP"] = _dep;
                                _dr["UPDDATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);

                                _dr["IDX_NAME"] = printHistroyDS.Tables[0].Rows[i]["IDX_NAME"].ToString();
                                if (printHistroyDS.Tables[0].Columns.Contains("FORMAT"))
                                    _dr["FORMAT"] = printHistroyDS.Tables[0].Rows[i]["FORMAT"].ToString();
                                if (printHistroyDS.Tables[0].Columns.Contains("LH"))
                                    _dr["LH"] = printHistroyDS.Tables[0].Rows[i]["LH"].ToString();
                                if (printHistroyDS.Tables[0].Columns.Contains("PRT_NAME"))
                                    _dr["PRT_NAME"] = printHistroyDS.Tables[0].Rows[i]["PRT_NAME"].ToString();
                                if (printHistroyDS.Tables[0].Columns.Contains("USR"))
                                    _dr["USR"] = printHistroyDS.Tables[0].Rows[i]["USR"].ToString();
                                if (printHistroyDS.Tables[0].Columns.Contains("OS_BAR_NO"))
                                    _dr["OS_BAR_NO"] = printHistroyDS.Tables[0].Rows[i]["OS_BAR_NO"].ToString();
                                if (_flag)
                                    _dr["FLAG"] = "T";
                                if (_ds.Tables["BAR_REC"].Columns.Contains("PRD_NO_SNM"))
                                    _dr["PRD_NO_SNM"] = printHistroyDS.Tables[0].Rows[i]["PRD_NO_SNM"].ToString();

                                if (_modify)
                                    _dr["ISMODIFY"] = "T";
                                else
                                    _dr["ISMODIFY"] = "F";
                                if (_change)
                                    _dr["ISCHANGE"] = "T";
                                else
                                    _dr["ISCHANGE"] = "F";

                                _ds.Tables["BAR_REC"].Rows.Add(_dr);
                            }
                            #endregion

                            #region ��������
                            SunlikeDataSet _dsTemp = _bat.GetData(_prd_mark);
                            if (_dsTemp.Tables.Count <= 0 || (_dsTemp.Tables.Count > 0 && _dsTemp.Tables[0].Rows.Count <= 0))
                            {
                                DataRow _drBat = _ds.Tables["BAT_NO"].Rows.Find(_prd_mark);
                                if (_drBat == null)
                                {
                                    DataRow _dr = _ds.Tables["BAT_NO"].NewRow();
                                    _dr["BAT_NO"] = _prd_mark;
                                    _dr["NAME"] = _prd_mark;
                                    _ds.Tables["BAT_NO"].Rows.Add(_dr);
                                }
                            }
                            #endregion
                        }
                    }
                    if (printHistroyDS.ExtendedProperties.Contains("SB_BAR_PRINT") && printHistroyDS.ExtendedProperties["SB_BAR_PRINT"].ToString() == "T")
                    {
                        //������˻������ӡ����������ˮ�ŵ�����¼��Ʒ����
                        _ds.ExtendedProperties["SB_BAR_PRINT"] = "T";
                    }
                    DataTable _dtErr = UpdataData(_ds);
                    if (_dtErr.Rows.Count > 0)
                    {
                        //base.SetAbort();
                        throw new SunlikeException(GetErrorString(_dtErr));
                    }
                }
                catch (Exception _ex)
                {
                    //base.SetAbort();
                    throw new SunlikeException(GetErrorResource(_ex.Message));
                }
                finally
                {
                    //base.LeaveTransaction();
                }
                scope.Complete();
            }
			return _ds;
        }
        /// <summary>
        /// �����������ӡ��Ϣ
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="printHistroyDS"></param>
        /// <returns></returns>
        public DataSet SaveBoxPrintData(string usr, DataSet printHistroyDS)
        {
            string _bar_no = "";
            string _sn = "";

            DataSet _result = new DataSet();
            DataSet _ds = new DataSet();
            //base.EnterTransaction();
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    foreach (DataRow _dr in printHistroyDS.Tables[0].Rows)
                    {
                        if (_dr.RowState != DataRowState.Deleted)
                        {
                            _bar_no = _dr["BAR_NO"].ToString();     //��������ˮ��
                            _sn = _dr["SN"].ToString();             //��ˮ��

                            #region ����BAR_BOX
                            DataSet _dsBarBox = this.GetBoxBar(_bar_no + _sn, "F", " AND ISNULL(A.STOP_ID,'F')<>'T' ");
                            if (_ds == null || _ds.Tables.Count <= 0)
                            {
                                _ds = _dsBarBox.Clone();
                                _ds.Tables["BAR_BOX"].Columns.Add("BOX_NO1", System.Type.GetType("System.String"));
                                _ds.Tables["BAR_BOX"].Columns.Add("PRD_NO1", System.Type.GetType("System.String"));
                                _ds.Tables["BAR_BOX"].Columns.Add("PRD_MARK1", System.Type.GetType("System.String"));
                                _ds.Tables["BAR_BOX"].Columns.Add("SN", System.Type.GetType("System.String"));
                                _ds.Tables["BAR_BOX"].Columns.Add("USR_PRT", System.Type.GetType("System.String"));
                            }

                            if (_dsBarBox.Tables[0].Rows.Count <= 0)
                            {
                                DataRow _drNew = _ds.Tables["BAR_BOX"].NewRow();
                                _drNew["BOX_NO"] = _bar_no + _sn;
                                _drNew["BOX_DD"] = System.DateTime.Today.ToString(Comp.SQLDateFormat);
                                _drNew["PRD_NO"] = _dr["BOX_PRD"].ToString();
                                _drNew["QTY"] = Convert.ToInt32(_dr["BOX_QTY"]);
                                _drNew["DEP"] = _dr["DEP"].ToString();
                                //����beforeupdate�У����浽Bar_sqno,Bar_Print
                                _drNew["BOX_NO1"] = _bar_no;
                                _drNew["SN"] = _sn;
                                _drNew["PRD_NO1"] = _dr["PRD_NO"].ToString();      //�̶�ֵ"WX" �����浽bar_print��bar_sqno��
                                _drNew["PRD_MARK1"] = _dr["PRD_MARK"].ToString();  //20080711001
                                _drNew["BAT_NO"] = _dr["BOX_BAT"].ToString();
                                _drNew["LH"] = _dr["BOX_LH"].ToString();
                                _drNew["IDX_NAME"] = _dr["BOX_IDX_NAME"].ToString();
                                _drNew["FORMAT"] = _dr["BOX_FORMAT"].ToString();
                                _drNew["USR_PRT"] = usr;
                                _ds.Tables["BAR_BOX"].Rows.Add(_drNew);
                            }
                            #endregion
                        }
                    }
                    DataTable _dtErr = UpdataDataBox(_ds);
                    if (_dtErr.Rows.Count > 0)
                        throw new SunlikeException(GetErrorString(_dtErr));
                }
                catch (Exception _ex)
                {
                    //base.SetAbort();
                    throw new SunlikeException(GetErrorResource(_ex.Message));
                }
                finally
                {
                    //base.LeaveTransaction();
                }
                scope.Complete();
            }
            _result = _ds;
            return _result;
        }
		#endregion

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="userId">�û�����</param>
        /// <param name="inPswd">������������</param>
        /// <returns></returns>
        public bool CheckUserPswd(string userId, string inPswd)
        {
            bool _result = false;
            string _userPswd = "";
            Query _query = new Query();
            string _sql = "SELECT USR,COMPNO,NAME,ISNULL(PWD,'') AS PWD,MNG,E_DAT,DEP,COMP_BOSS,ISNULL(ISGROUP,'F') AS ISGROUP,ISNULL(ISSALM,'F') AS ISSALM,ISNULL(ISCUST,'F') AS ISCUST,ISNULL(ISYG,'F') AS ISYG,COMP_BOSS,ISNULL(DEP_UP,'') AS DEP_UP,UPD_DD,PWD_CHG,EXPIRE_ID FROM PSWD WHERE 1=1 AND USR='" + userId + "' AND COMPNO='" + Comp.CompNo + "'";
            DataSet _ds = _query.DoSQLString(Comp.Conn_SunSystem, _sql);
            if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                _userPswd = Sunlike.Common.Utility.Security.DecodingCompData(_ds.Tables[0].Rows[0]["PWD"].ToString(), "4e870172967346a1ac1e651d53be0a56");
                //_userPswd = Sunlike.Common.Utility.Security.DecodingPswd(_ds.Tables[0].Rows[0]["PWD"].ToString());
                if (_userPswd.ToUpper() == inPswd.ToUpper())
                {
                    _result = true;
                }
            }
            return _result;
        }
        #endregion

        #region �жϰ�������Ƿ��Ѵ�ӡ
        /// <summary>
        /// �޸İ������ʱ������������ˮ�ų���BAR_SQNO��MAX_NO,����뵽��������ӡҳ����д�ӡ
        /// </summary>
        /// <param name="_barNo"></param>
        /// <param name="_batNo"></param>
        /// <param name="_sn"></param>
        /// <returns></returns>
        public bool BCIsPrinted(string _barNo, string _batNo, string _sn)
        {
            bool _result = false;
            string _sql = "SELECT * FROM BAR_REC WHERE 1=1 ";
            if (_batNo.Length > 0)
                _sql += " AND replace(SUBSTRING(BAR_NO,13,10),'#','')='" + _batNo + "'";
            if (_sn != "")
                _sql += " AND SUBSTRING(BAR_NO,24,3)='" + _sn + "'";
            string _sql1 = ";SELECT MAX_NO FROM BAR_SQNO WHERE 1=1 ";
            if (_barNo != "")
                _sql1 += " AND PRD_NO='" + _barNo + "'";
            if (_batNo.Length > 0)
                _sql1 += " AND PRD_MARK='" + _batNo + "'";
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql + _sql1);
            if (_ds.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToInt32(_sn) > Convert.ToInt32(_ds.Tables[1].Rows[0][0].ToString()))
                {
                    _result = true;//����������ʱ������������ˮ�ų���BAR_SQNO��MAX_NO,����뵽��������ӡҳ����д�ӡ
                }
            }

            if (_ds.Tables[0].Rows.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                _result = true;//�����Ѵ���
            return _result;
        }
        #endregion

        #region �ж��˻������Ƿ��Ѵ�ӡ
        /// <summary>
        /// �ж��˻������Ƿ��Ѵ�ӡ��ֻ���ж�����+��ˮ����û�д�ӡ����
        /// </summary>
        /// <param name="_batNo"></param>
        /// <param name="_sn"></param>
        /// <returns></returns>
        public bool SBIsPrinted(string _batNo, string _sn)
        {
            bool _result = false;
            string _sql = "SELECT * FROM BAR_REC WHERE 1=1 ";
            if (_batNo.Length > 0)
                _sql += " AND replace(SUBSTRING(BAR_NO,13,10),'#','')='" + _batNo + "'";
            if (_sn != "")
                _sql += " AND SUBSTRING(BAR_NO,24,3)='" + _sn + "'";
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql);
            if (_ds.Tables[0].Rows.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                _result = true;//�����Ѵ���
            return _result;
        }
        #endregion

        #region �жϷ��������Ƿ���ʹ��
        public bool InUse(string _batNo)
        {
            bool _result = false;
            string _sql = "SELECT * FROM BAR_REC WHERE 1=1 ";
            if (_batNo.Length > 0)
                _sql += " AND ISNULL(WH,'')<>'' AND SUBSTRING(BAR_NO,13,10)='" + _batNo + "'";
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql);
            if (_ds.Tables[0].Rows.Count > 0)
                _result = true;
            return _result;
        }
        #endregion

        #region ɾ������
        public void DeleteBarCode(string _where)
        {
            if (_where.Length > 0)
            {
                string _sql = "begin tran;DELETE FROM BAR_REC WHERE 1=1 "
                            + _where
                            + ";DELETE FROM BAR_PRINT WHERE 1=1 "
                            + _where
                            + ";DELETE FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC WHERE 1=1 " + _where+";commit tran;";
                Query _query = new Query();
                _query.RunSql(_sql);
            }
        }
        #endregion

        #region ȡ���������¼
        public DataSet GetBarRecord(string _barNO)
		{
            string _sql = "SELECT A.BAR_NO,A.BOX_NO,A.WH,A.UPDDATE,A.STOP_ID,A.PRD_NO,A.PRD_MARK,A.BAT_NO,A.BIL_NOLIST,A.SPC_NO,A.PH_FLAG,B.SPC,B.IDX1,C.NAME AS IDX_NAME,F.MRP_NO, E.MLID AS FLAG,E.ML_NO,D.MAX_NO"
                        + " FROM BAR_REC A "
                        + " LEFT JOIN PRDT B ON A.PRD_NO=B.PRD_NO"
                        + " LEFT JOIN INDX C ON C.IDX_NO=B.IDX1"
                        + " LEFT JOIN BAR_SQNO D ON D.PRD_NO=A.PRD_NO AND D.PRD_MARK=A.BAT_NO "
                        + " LEFT JOIN TF_ML_B E ON E.BAR_CODE=A.BAR_NO"
                        + " LEFT JOIN MF_ML F ON F.MLID=E.MLID AND F.ML_NO=E.ML_NO"
                        + " WHERE A.BAR_NO='" + _barNO + "' order by F.SYS_DATE DESC ";
			Query _query = new Query();
			DataSet _ds = _query.DoSQLString(_sql);
			_ds.Tables[0].TableName = "BAR_REC";
			return _ds;
        }
        public DataSet GetBarRecord1(string _barNoArray)
        {
            string _sql = "SELECT A.BAR_NO,A.BOX_NO,A.WH,A.UPDDATE,A.STOP_ID,A.PRD_NO,A.PRD_MARK,A.BAT_NO,A.SPC_NO,A.PH_FLAG,B.SPC,B.IDX1,C.NAME AS IDX_NAME FROM BAR_REC A "
                        + " LEFT JOIN PRDT B ON A.PRD_NO=B.PRD_NO LEFT JOIN INDX C ON C.IDX_NO=B.IDX1 WHERE A.BAR_NO in ('" + _barNoArray + "')";
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "BAR_REC";
            return _ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_where"></param>
        /// <returns></returns>
        public DataSet GetBarRecCanDel(string _where)
        {
            string _sql = "SELECT A.BAR_NO,A.BOX_NO,E.BAT_NO,C.NAME AS IDX_NAME,A.PRD_NO,B.SPC,E1.NAME AS USR_NAME,E.SYS_DATE,A.STOP_ID FROM BAR_REC A "
                        + " LEFT JOIN PRDT B ON A.PRD_NO=B.PRD_NO"
                        + " LEFT JOIN INDX C ON C.IDX_NO=B.IDX1"
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC E ON E.BAR_NO=A.BAR_NO AND ISNULL(E.QC,'')='' "
                        + " LEFT JOIN SUNSYSTEM..PSWD E1 ON E1.COMPNO='" + Comp.CompNo + "' AND E1.USR=E.USR "
                        + " WHERE 1=1 ";
            if (_where != "")
                _sql += _where;
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "BAR_REC";
            return _ds;
        }
        /// <summary>
        /// ȡ������Ϣ(����ʱ����)
        /// </summary>
        /// <param name="_barNO">����</param>
        /// <returns></returns>
        public DataSet GetBarChg(string _barNO)
        {
            string _sql = "SELECT A.BAR_NO,A.STOP_ID,F.MRP_NO,B.SPC,B.IDX1,C.NAME AS IDX_NAME,E.ML_NO,D.FORMAT,D.LH,D.PRT_NAME"
                        + " FROM BAR_REC A "
                        + " LEFT JOIN TF_ML_B E ON E.BAR_CODE=A.BAR_NO AND (E.MLID='ML' or E.MLID='M3')"
                        + " LEFT JOIN MF_ML F ON F.MLID=E.MLID AND F.ML_NO=E.ML_NO"
                        + " LEFT JOIN PRDT B ON F.MRP_NO=B.PRD_NO"
                        + " LEFT JOIN INDX C ON C.IDX_NO=B.IDX1"
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC D ON D.BAR_NO=A.BAR_NO"
                        + " WHERE A.BAR_NO='" + _barNO + "'"
                        + " ORDER BY F.ML_DD DESC ";
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "BAR_REC";
            return _ds;
        }
        #endregion

        #region ȡδ�����������Ϣ
        public DataSet GetBarUnQC(string _barNo)
        {
            string _sql = "SELECT A.BAR_NO,REPLACE(A.BAR_NO,'#','') AS BAR_NO_DIS,A.STOP_ID,A.PRD_NO,C.NAME AS PRD_NAME,C.SPC,A.PRD_MARK,A.BAT_NO,A.REM,(CASE WHEN ISNULL(B.BAR_NO,'')<>'' THEN 'T' ELSE 'F' END) AS QC_FLAG FROM BAR_REC A "
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC B ON A.BAR_NO=B.BAR_NO"
                        + " LEFT JOIN PRDT C ON C.PRD_NO=A.PRD_NO"
                        + " WHERE A.BAR_NO='" + _barNo + "'";
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "BAR_QC";
            return _ds;
        }
        public DataSet GetBarUnQCLst(string _barType)
        {
            string _sql = "SELECT A.BAR_NO,REPLACE(A.BAR_NO,'#','') AS BAR_NO_DIS,A.STOP_ID,A.PRD_NO,C.NAME AS PRD_NAME,C.SPC,A.PRD_MARK,A.BAT_NO,A.REM,"
                        + "(CASE WHEN ISNULL(B.BAR_NO,'')<>'' THEN 'T' ELSE 'F' END) AS QC_FLAG,B.SPC_NO,D.NAME AS SPC_NAME,B.PRC_ID"
                        + " FROM BAR_REC A "
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC B ON A.BAR_NO=B.BAR_NO"
                        + " LEFT JOIN SPC_LST D ON B.SPC_NO=D.SPC_NO"
                        + " LEFT JOIN PRDT C ON C.PRD_NO=A.PRD_NO"
                        + " WHERE ISNULL(B.QC,'')='' AND ISNULL(A.STOP_ID,'F')<>'T'";
            //_barType��0����ĺͰ��Ʒ  1����ģ�2�����Ʒ��3����Ʒ��
            if (_barType == "0")
            {
                _sql += " AND (LEN(REPLACE(A.BAR_NO,'#',''))=22 OR LEN(REPLACE(A.BAR_NO,'#',''))=24)";
                //ȡ���Ѽ��� δ�ͽɵİ�Ļ���Ʒ����(��������Ʒ��)
                _sql += "; SELECT A.BAR_NO,REPLACE(A.BAR_NO,'#','') AS BAR_NO_DIS,A.STOP_ID,A.PRD_NO,C.NAME AS PRD_NAME,C.SPC,A.PRD_MARK,A.BAT_NO,A.REM,"
                        + "(CASE WHEN ISNULL(B.BAR_NO,'')<>'' THEN 'T' ELSE 'F' END) AS QC_FLAG,B.SPC_NO,D.NAME AS SPC_NAME"
                        + " FROM BAR_REC A "
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC B ON A.BAR_NO=B.BAR_NO"
                        + " LEFT JOIN SPC_LST D ON B.SPC_NO=D.SPC_NO"
                        + " LEFT JOIN PRDT C ON C.PRD_NO=A.PRD_NO"
                        + " WHERE ISNULL(B.STATUS,'')='QC' AND ISNULL(B.QC,'')<>'' AND (ISNULL(B.QC,'')<>'2' or (ISNULL(B.QC,'')='2' and isnull(B.BIL_NO,'')=''))"
                        + " AND ISNULL(A.STOP_ID,'F')<>'T' AND ISNULL(A.WH,'')='' AND (LEN(REPLACE(A.BAR_NO,'#',''))=22 OR LEN(REPLACE(A.BAR_NO,'#',''))=24)";
            }
            if (_barType == "1")
            {
                _sql += "  AND LEN(REPLACE(A.BAR_NO,'#',''))=22";
                //ȡ���Ѽ��� δ�ͽɵİ������(��������Ʒ��)
                _sql += "; SELECT A.BAR_NO,REPLACE(A.BAR_NO,'#','') AS BAR_NO_DIS,A.STOP_ID,A.PRD_NO,C.NAME AS PRD_NAME,C.SPC,A.PRD_MARK,A.BAT_NO,A.REM,"
                        + "(CASE WHEN ISNULL(B.BAR_NO,'')<>'' THEN 'T' ELSE 'F' END) AS QC_FLAG,B.SPC_NO,D.NAME AS SPC_NAME"
                        + " FROM BAR_REC A "
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC B ON A.BAR_NO=B.BAR_NO"
                        + " LEFT JOIN SPC_LST D ON B.SPC_NO=D.SPC_NO"
                        + " LEFT JOIN PRDT C ON C.PRD_NO=A.PRD_NO"
                        + " WHERE ISNULL(B.STATUS,'')='QC' AND ISNULL(B.QC,'')<>'' AND (ISNULL(B.QC,'')<>'2' or (ISNULL(B.QC,'')='2' and isnull(B.BIL_NO,'')=''))"
                        + " AND ISNULL(A.STOP_ID,'F')<>'T' AND ISNULL(A.WH,'')='' AND LEN(REPLACE(A.BAR_NO,'#',''))=22";
            }
            if (_barType == "2")
            {
                _sql += "  AND LEN(REPLACE(A.BAR_NO,'#',''))=24";
                //ȡ���Ѽ��� δ�ͽɵİ��Ʒ����(��������Ʒ��)
                _sql += "; SELECT A.BAR_NO,REPLACE(A.BAR_NO,'#','') AS BAR_NO_DIS,A.STOP_ID,A.PRD_NO,C.NAME AS PRD_NAME,C.SPC,A.PRD_MARK,A.BAT_NO,A.REM,"
                        + "(CASE WHEN ISNULL(B.BAR_NO,'')<>'' THEN 'T' ELSE 'F' END) AS QC_FLAG,B.SPC_NO,D.NAME AS SPC_NAME"
                        + " FROM BAR_REC A "
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC B ON A.BAR_NO=B.BAR_NO"
                        + " LEFT JOIN SPC_LST D ON B.SPC_NO=D.SPC_NO"
                        + " LEFT JOIN PRDT C ON C.PRD_NO=A.PRD_NO"
                        + " WHERE ISNULL(B.STATUS,'')='QC' AND ISNULL(B.QC,'')<>'' AND (ISNULL(B.QC,'')<>'2' or (ISNULL(B.QC,'')='2' and isnull(B.BIL_NO,'')=''))"//QC=2Ϊ���ϸ�QC=1Ϊ�ϸ�QC=AΪA��Ʒ��QC=BΪB��Ʒ
                        + " AND ISNULL(A.STOP_ID,'F')<>'T' AND ISNULL(A.WH,'')='' AND LEN(REPLACE(A.BAR_NO,'#',''))=24";
            }
            if (_barType == "3")
                _sql += "  AND LEN(REPLACE(A.BAR_NO,'#',''))>24";
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "BAR_QC";
            return _ds;
        }
        #endregion

        #region �ͽ�
        /// <summary>
        /// ȡ���Ѽ���δ�ͽɡ�
        /// �����ͽ�δ�ɿ⡿
        /// �İ��������Ϣ
        /// </summary>
        /// <param name="_where">����</param>
        /// <returns></returns>
        public DataSet GetBCUnToMMLst(string _where)
        {
            string _sql = "SELECT A.BAR_NO,REPLACE(A.BAR_NO,'#','') AS BAR_NO_DIS,A.BIL_ID,A.BIL_NO,A.QC,A.STATUS,A.REM,B.PRD_NO,B.BAT_NO,B.WH"
                        + " FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A "
                        + " INNER JOIN BAR_REC B ON A.BAR_NO=B.BAR_NO"
                        + " WHERE ISNULL(B.STOP_ID,'F')<>'T' AND ISNULL(B.WH,'')='' ";
            if (_where.Length > 0)
                _sql += _where;
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "BAR_QC";
            return _ds;
        }
        /// <summary>
        /// ȡ��װ��δ�ͽɵ������루��Ʒ�ͽ���ҵ���ã�
        /// </summary>
        /// <returns></returns>
        public DataSet GetFTUnToMMLst()
        {
            string _sql = "SELECT A.BOX_NO,SUBSTRING(A.BOX_NO,2,LEN(A.BOX_NO)-1) AS BOX_NO_DIS,A.BOX_DD,A.DEP,A.CONTENT,A.USR,A.WH,A.PH_FLAG,A.STOP_ID,A.VALID_DD,A.QTY,A.PRD_NO,B.SPC,D.IDX_UP,"
                        + " B.IDX1,(case when ISNULL(C.IDX_NAME,'')<>'' THEN C.IDX_NAME ELSE D.NAME END) AS IDX_NAME,C.BAT_NO,"
                        + "(select top 1 QC from " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC inner join BAR_REC on BAR_REC.BAR_NO=" + Comp.DRP_Prop["BarPrintDB"].ToString()
                        + "..BAR_QC.BAR_NO where BAR_REC.BOX_NO=A.BOX_NO) QC,C.STATUS,C.BIL_ID,C.BIL_NO,C.WH_SJ,C.REM"
                        + " FROM BAR_BOX A"
                        + " LEFT JOIN PRDT B ON A.PRD_NO=B.PRD_NO"
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT C ON C.BOX_NO=A.BOX_NO"
                        + " LEFT JOIN INDX D ON B.IDX1=D.IDX_NO"
                        + " WHERE 1=1  AND ISNULL(A.STOP_ID,'F')<>'T'"//δͣ��
                        + " AND ISNULL(A.CONTENT,'')<>''"//�����ݲ�Ϊ��
                        + " AND ISNULL(C.PRD_MARK,'')<>'T'"//�����ڳ���Ʒ���кţ��ڳ���Ʒ���к����̲�ͬ
                        + " AND ISNULL(C.STATUS,'')<>'SW'"//δ�ͽ�
                        + " AND ISNULL(A.WH,'')=''";//���λΪ��,˵��δ�ɿ�
            Query _query = new Query();
            DataSet _ds = _query.FillDataSet(_sql.ToString(), null, new string[] { "BAR_BOX" }, false);
            _ds.Tables["BAR_BOX"].Columns["IDX_NAME"].ReadOnly = false;
            return _ds;
        }
        /// <summary>
        /// �ͽɳ���
        /// </summary>
        /// <param name="_barNoLst">����</param>
        /// <param name="_boxNoList">����</param>
        /// <param name="_usr">��ҵ��Ա</param>
        /// <returns></returns>
        public string UndoToMM(string _barNoLst, string _boxNoList, string _usr)
        {
            string _result = "";
            if (!String.IsNullOrEmpty(_barNoLst))
            {
                string _sql = "";
                if (!String.IsNullOrEmpty(_boxNoList))
                {
                    _sql = "UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC SET BIL_ID=NULL,BIL_NO=NULL,STATUS='QC',WH_SJ=NULL,USR_UNSJ='" + _usr + "',UNSJ_DATE='" + DateTime.Now.ToString(Comp.SQLDateTimeFormat) + "' from " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A inner join BAR_REC B on A.BAR_NO=B.BAR_NO WHERE B.BOX_NO IN (" + _boxNoList + ");"
                         + "UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT SET BIL_ID=NULL,BIL_NO=NULL,STATUS='QC',WH_SJ=NULL WHERE BOX_NO IN (" + _boxNoList + ");";
                }
                else
                {
                    _sql = "UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC SET BIL_ID=NULL,BIL_NO=NULL,STATUS='QC',WH_SJ=NULL,USR_UNSJ='" + _usr + "',UNSJ_DATE='" + DateTime.Now.ToString(Comp.SQLDateTimeFormat) + "' WHERE BAR_NO IN (" + _barNoLst + ");";
                }
                Query _query = new Query();
                try
                {
                    _query.RunSql(_sql);
                }
                catch (Exception _ex)
                {
                    _result = _ex.Message;
                }
            }
            if (_result.Length > 100)
                _result = _result.Substring(0, 100);
            return _result;
        }
        #endregion

        #region ȡ�����ͽɴ��ɿ��������Ϣ
        /// <summary>
        /// ȡ�����ͽɴ��ɿ�İ��������Ϣ
        /// </summary>
        /// <param name="_where">����</param>
        /// <returns></returns>
        public DataSet GetBCUnMMLst(string _where)
        {
            //���BAR_REC�������WH��ֵ��˵���Ѿ����
            string _sql = "SELECT A.BAR_NO,REPLACE(A.BAR_NO,'#','') AS BAR_NO_DIS,A.BOX_NO,A.STOP_ID,A.PRD_NO,A.PRD_MARK,"
                        + "C.NAME AS PRD_NAME,C.SPC,A.BAT_NO,B.QC,B.STATUS,B.WH_SJ,WH.NAME AS WH_SJ_NAME,A.REM,B.BIL_ID,B.BIL_NO"
                        + " FROM BAR_REC A "
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC B ON A.BAR_NO=B.BAR_NO"
                        + " LEFT JOIN PRDT C ON C.PRD_NO=A.PRD_NO"
                        + " LEFT JOIN MY_WH WH ON WH.WH=B.WH_SJ"
                        + " WHERE 1=1 AND ISNULL(A.WH,'')='' AND ISNULL(A.STOP_ID,'F')<>'T' AND ISNULL(B.STATUS,'')='SW'"
                        + " AND (ISNULL(B.QC,'')='1' OR ISNULL(B.QC,'')='A' OR ISNULL(B.QC,'')='B' OR (ISNULL(B.QC,'')='2' AND ISNULL(B.PRC_ID,'')='1'))";
            Query _query = new Query();
            if (_where != "")
            {
                _sql += _where;
            }
            DataSet _ds = _query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "BAR_REC";
            return _ds;
        }
        /// <summary>
        /// ���ͽ�δ�ɿ�������루��������Ʒ��ϸ������Ʒ�ͽɳ�����ҵ���ã�
        /// </summary>
        /// <param name="_where"></param>
        /// <returns></returns>
        public DataSet GetBoxUnMM(string _where)
        {
            //E.PRD_MARK='T'���ڳ����
            string _sql = "SELECT A.BOX_NO AS BAR_NO,A.BOX_NO AS BAR_NO_DIS"
                        + " FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT A "
                        + " LEFT JOIN BAR_BOX B ON B.BOX_NO=A.BOX_NO"
                        + " WHERE ISNULL(B.STOP_ID,'F')<>'T' AND ISNULL(B.CONTENT,'')<>'' AND ISNULL(A.PRD_MARK,'')<>'T' AND ISNULL(A.STATUS, '')='SW' ";
            Query _query = new Query();
            if (_where != "")
            {
                _sql += _where;
            }
            DataSet _ds = _query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "BAR_REC";
            return _ds;
        }
        /// <summary>
        /// ȡ�����ͽ�δ�ɿ�������뼰��ϸ����Ʒ�ɿ���ҵ���ã�
        /// </summary>
        /// <param name="_sqlWhere"></param>
        /// <returns></returns>
        public DataSet GetBoxUnMMLst(string _sqlWhere)
        {
            string _sql = "SELECT A.BAR_NO,A.BOX_NO,SUBSTRING(A.BOX_NO,2,LEN(A.BOX_NO)-1) AS BOX_NO_DIS,A.PRD_NO,A.PRD_MARK,"
                        + "A.BAT_NO,A.REM,B.NAME AS PRD_NAME,E.BIL_ID,E.BIL_NO,E.QC,E.STATUS,E.REM,E.WH_SJ,WH.NAME AS WH_SJ_NAME,D.CONTENT"
                        + " FROM BAR_REC A"
                        + " LEFT JOIN PRDT B ON B.PRD_NO=A.PRD_NO"
                        + " LEFT JOIN BAR_BOX D ON D.BOX_NO=A.BOX_NO"
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT E ON E.BOX_NO=A.BOX_NO"
                        + " LEFT JOIN MY_WH WH ON WH.WH=E.WH_SJ"
                        + " WHERE 1=1 AND ISNULL(A.BOX_NO,'')<>'' AND ISNULL(A.WH,'')='' AND ISNULL(D.STOP_ID,'F')<>'T'"
                        + " AND ISNULL(D.CONTENT,'')<>'' AND ISNULL(E.PRD_MARK,'')<>'T' AND ISNULL(E.STATUS, '')='SW'";//E.PRD_MARK='T'���ڳ���棻A.WH=''��ʾδ�ɿ�
            if (_sqlWhere != "")
                _sql += _sqlWhere;
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "BAR_REC";
            return _ds;
        }
        #endregion

        #region �޸�Ʒ����Ϣ
        /// <summary>
        /// ȡ�ô��޸�Ʒ����Ϣ�����к�
        /// </summary>
        /// <param name="_barNo">���к�</param>
        /// <param name="_barType">��Ʒ���ͣ�1:��ģ�2:���Ʒ��3:��Ʒ��</param>
        /// <returns></returns>
        public DataSet GetBarQC(string _barNo, string _barType)
        {
            string _sql = "SELECT A.BAR_NO,REPLACE(A.BAR_NO,'#','') AS BAR_NO_DIS,A.STOP_ID,A.PRD_NO,C.NAME AS PRD_NAME,C.SPC,A.PRD_MARK,A.BAT_NO,A.REM,"
                        + "(CASE WHEN ISNULL(B.BAR_NO,'')<>'' THEN 'T' ELSE 'F' END) AS QC_FLAG"
                        + " FROM BAR_REC A "
                        + " INNER JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC B ON A.BAR_NO=B.BAR_NO"
                        + " LEFT JOIN PRDT C ON C.PRD_NO=A.PRD_NO"
                        + " WHERE A.BAR_NO='" + _barNo + "' and ISNULL(B.STATUS,'')='QC' AND ISNULL(A.STOP_ID,'')<>'T' AND ISNULL(A.BOX_NO,'')='' AND ISNULL(A.WH,'')=''";

            //_barType��0����ĺͰ��Ʒ  1����ģ�2�����Ʒ��3����Ʒ��
            if (_barType == "0")
                _sql += "  AND (LEN(REPLACE(A.BAR_NO,'#',''))=22 OR LEN(REPLACE(A.BAR_NO,'#',''))=24)";
            else if (_barType == "1")
                _sql += "  AND LEN(REPLACE(A.BAR_NO,'#',''))=22";
            else if (_barType == "2")
                _sql += "  AND LEN(REPLACE(A.BAR_NO,'#',''))=24";
            else if (_barType == "3")
                _sql += "  AND LEN(REPLACE(A.BAR_NO,'#',''))>24";
            else
                _sql += " AND 1<>1";
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "BAR_QC";
            return _ds;
        }
        /// <summary>
        /// ����Ʒ�죬�޸�Ʒ����Ϣʱ����
        /// </summary>
        /// <param name="_barNo"></param>
        public void UndoBarQC(string _barNo)
        {
            Query _query = new Query();
            _query.RunSql("UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC SET QC=NULL,STATUS=NULL,PRC_ID=NULL,SPC_NO=NULL,REM=NULL,USR_QC=NULL,QC_DATE=NULL,USR_SJ=NULL,SJ_DATE=NULL WHERE BAR_NO='" + _barNo + "'");
        }
        #endregion

        #region ȡ���������¼
        public DataSet GetBoxBar(string _boxNO, string _isBox, string _sqlWhere)
        {
            //_isBox:T:ȡ����װ���������; F:ȡ��δװ��������룻 null��գ�����
            string _sql = "SELECT A.BOX_NO,SUBSTRING(A.BOX_NO,2,LEN(A.BOX_NO)-1) AS BOX_NO_DIS,A.BOX_DD,A.DEP,A.CONTENT,A.USR,A.WH,A.PH_FLAG,A.STOP_ID,A.VALID_DD,A.QTY,A.PRD_NO,B.SPC,D.IDX_UP,B.IDX1,"
                        + "C.LH,(case when ISNULL(C.IDX_NAME,'')<>'' THEN C.IDX_NAME ELSE D.NAME END) AS IDX_NAME,C.BAT_NO,C.FORMAT,C.QC,C.STATUS,C.BIL_ID,C.BIL_NO,C.WH_SJ,C.REM FROM BAR_BOX A"
                        + " LEFT JOIN PRDT B ON A.PRD_NO=B.PRD_NO "
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT C ON C.BOX_NO=A.BOX_NO" 
                        + " LEFT JOIN INDX D ON B.IDX1=D.IDX_NO"
                        + " WHERE 1=1 ";
            if (_isBox == "T")//��װ��
                _sql += " AND ISNULL(A.CONTENT,'')<>''";
            else if (_isBox == "F")//δװ��
                _sql += " AND ISNULL(A.CONTENT,'')=''";
            Query _query = new Query();
            SqlParameter[] _para = new SqlParameter[1];
            _para[0] = new SqlParameter("@BOX_NO", SqlDbType.VarChar);
            _para[0].Value = _boxNO;
            //���_boxNOΪ��,�������е�������
            if (!String.IsNullOrEmpty(_boxNO))
            {
                _sql += " AND A.BOX_NO=@BOX_NO";
            }
            if (!String.IsNullOrEmpty(_sqlWhere))
            {
                _sql += _sqlWhere;
            }
            DataSet _ds = _query.FillDataSet(_sql, _para, new string[] { "BAR_BOX" }, false);
            _ds.Tables["BAR_BOX"].Columns["IDX_NAME"].ReadOnly = false;
            return _ds;
        }
        //�����������ظ���ӡ
        public DataSet GetBoxBarPrint(string _boxNo)
        {
            string _sql = "SELECT A.BOX_NO,A.QTY,A.PRD_NO,B.SPC,D.IDX_UP,B.IDX1,C.BAT_NO,C.LH,C.IDX_NAME,E.QTY_PER_GP,E.BC_GP,E.LH_FOXCONN,F.CZ_GP"
                        + " FROM BAR_BOX A"
                        + " LEFT JOIN PRDT B ON A.PRD_NO=B.PRD_NO "
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT C ON C.BOX_NO=A.BOX_NO"
                        + " LEFT JOIN INDX D ON B.IDX1=D.IDX_NO"
                        + " LEFT JOIN PRDT_Z E ON A.PRD_NO=E.PRD_NO"
                        + " LEFT JOIN INDX_Z F ON B.IDX1=F.IDX_NO"
                        + " WHERE 1=1 AND A.BOX_NO=@BOX_NO";
            Query _query = new Query();
            SqlParameter[] _para = new SqlParameter[1];
            _para[0] = new SqlParameter("@BOX_NO", SqlDbType.VarChar,90);
            _para[0].Value = _boxNo;
            DataSet _ds = _query.FillDataSet(_sql, _para, new string[] { "BAR_BOX" }, false);
            _ds.Tables["BAR_BOX"].Columns["IDX_NAME"].ReadOnly = false;
            return _ds;
        }
        #endregion

        #region ȡ�ó�Ʒ����
        public DataSet GetBoxBarRec(string _where)
        {
            //******************�Ѽ���δװ��ĳ�Ʒ����:  ����: ISNULL(B.BAR_NO,'')<>'' AND ISNULL(A.BOX_NO,'')=''**************
            //******************��װ��ĳ�Ʒ����:  ����:  AND A.BOX_NO='" + txtBoxNo.Text + "'**************************
            string _sql = "SELECT A.BAR_NO,REPLACE(A.BAR_NO,'#','') AS BAR_NO_DIS,A.BOX_NO,A.WH,D.NAME AS WH_NAME,A.STOP_ID,A.PRD_NO,C.NAME AS PRD_NAME,C.SPC,A.PRD_MARK,B.BAT_NO,B.QC,B.PRC_ID,A.REM FROM BAR_REC A "
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC B ON A.BAR_NO=B.BAR_NO"
                        + " LEFT JOIN PRDT C ON C.PRD_NO=A.PRD_NO"
                        + " LEFT JOIN MY_WH D ON D.WH=A.WH "
                        + " WHERE LEN(REPLACE(A.BAR_NO,'#',''))>24 ";
            Query _query = new Query();
            if (_where != "")
            {
                _sql += _where;
            }
            DataSet _ds = _query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "BAR_REC";
            return _ds;
        }
        #endregion

        #region ȡ��Ʒ��Ϣ
        /// <summary>
        /// ȡ��Ʒ��Ϣ
        /// </summary>
        /// <param name="_prdNo">Ʒ��</param>
        /// <param name="_idx">��ӡ��ʽ��ֻ�а�ĺ������ӡ�Ŵ��룬��������ռ���</param>
        /// <returns></returns>
        public DataSet GetPrdtFull(string _prdNo, string _idx)
        {
            string _sql = "SELECT P.PRD_NO,P.NAME,P.SPC,P.IDX1,I.NAME AS IDX_NAME,I.IDX_UP,PZ.QTY_PER_GP,PZ.BC_GP,PZ.LH_FOXCONN,IZ.CZ_GP,"
                        + " BCP.NAME AS LH_BC,BCI.NAME AS PRT_NAME_BC,BOXP.NAME AS LH_BOX,BOXI.NAME AS PRT_NAME_BOX "
                        + " FROM PRDT P"
                        + " LEFT JOIN INDX I ON P.IDX1=I.IDX_NO"
                        + " LEFT JOIN PRDT_Z PZ ON P.PRD_NO=PZ.PRD_NO"
                        + " LEFT JOIN INDX_Z IZ ON P.IDX1=IZ.IDX_NO"
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_PRTNAME BCP ON BCP.TYPE_ID='0' AND BCP.IDX='"+_idx+"' AND BCP.IDX_NO='' AND BCP.PRD_NO=P.PRD_NO"//��Ӧ���Ʒ��
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_PRTNAME BCI ON BCI.TYPE_ID='0' AND BCI.IDX='" + _idx + "' AND BCI.PRD_NO='' AND BCI.IDX_NO=P.IDX1"//��Ӧ�������
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_PRTNAME BOXP ON BOXP.TYPE_ID='2' AND BOXP.IDX='" + _idx + "' AND BOXP.IDX_NO='' AND BOXP.PRD_NO=P.PRD_NO"//��Ӧ��Ʒ��
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_PRTNAME BOXI ON BOXI.TYPE_ID='2' AND BOXI.IDX='" + _idx + "' AND BOXI.PRD_NO='' AND BOXI.IDX_NO=P.IDX1"//��Ӧ������
                        + " WHERE P.PRD_NO='" + _prdNo + "' ";
            Query _query = new Query();
            DataSet _ds = (DataSet)_query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "PRDT";
            return _ds;
        }
        #endregion

        #region ȡ���������к�
        /// <summary>
        /// ȡ���������к�
        /// </summary>
        /// <param name="_where"></param>
        /// <returns></returns>
        public DataSet GetRptSABarQClist(string _where)
        {
            //ȡ���������к�
            string _sql = "SET ROWCOUNT 10000;SELECT A.PS_ID,A.PS_ITM,A.ITM,C.PS_DD,C.CLS_DATE,A.PS_NO,E.NAME AS CUS_NAME,D.NAME AS WH_NAME,A.PRD_NO,F.SPC,G.NAME AS IDX_NAME,J.QTY,J.QTY1,A.BAR_CODE,A.BOX_NO,REPLACE(SUBSTRING(A.BAR_CODE,13,10),'#','') AS BAT_NO,H.QC,I.NAME AS SPC_NAME,H.REM AS QC_REM,"
                        + "L.BAR_CODE AS CHG_BAR_CODE,L3.NAME AS CHG_SPC_NAME,L1.NAME AS USR_QC,L.QC_DATE,'' as IS_CHANGED,L2.NAME AS USR_CHG,L.CHG_DATE, "
                        + "SBJ.PS_ID as SB_ID,SBJ.ITM AS SB_ITM,SBC.PS_DD AS SB_DD,SBJ.PS_NO AS SB_NO,SBD.NAME AS SB_WH_NAME,SBE.SNM AS SB_CUS_NAME,SBF.SPC AS SB_SPC,SBG.NAME AS SB_IDX_NAME,SBJ.QTY AS SB_QTY,SBJ.QTY1 AS SB_QTY1,SBC.REM AS SB_REM  "
                        + "FROM TF_PSS3 A "
                        + "INNER JOIN BAR_REC B ON B.BAR_NO=A.BAR_CODE "
                        + "INNER JOIN MF_PSS C ON C.PS_ID=A.PS_ID AND C.PS_NO=A.PS_NO "
                        + "INNER JOIN TF_PSS J ON J.PS_ID=A.PS_ID AND J.PS_NO=A.PS_NO AND J.PRE_ITM=A.PS_ITM "
                        + "LEFT JOIN MY_WH D ON D.WH=B.WH "
                        + "LEFT JOIN CUST E ON E.CUS_NO=C.CUS_NO "
                        + "LEFT JOIN PRDT F ON F.PRD_NO=A.PRD_NO "
                        + "LEFT JOIN INDX G ON G.IDX_NO=F.IDX1 "
                        + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC H ON H.BAR_NO=A.BAR_CODE "
                        + "LEFT JOIN SPC_LST I ON I.SPC_NO=H.SPC_NO "
                        + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..SA_BAR_CHG L ON L.PS_ID=A.PS_ID AND L.PS_NO=A.PS_NO AND L.PS_ITM=A.PS_ITM AND L.ITM=A.ITM "
                        + "LEFT JOIN SUNSYSTEM..PSWD L1 ON L1.COMPNO='" + Comp.CompNo + "' AND L.USR_QC=L1.USR "//������Ա
                        + "LEFT JOIN SUNSYSTEM..PSWD L2 ON L2.COMPNO='" + Comp.CompNo + "' AND L.USR_CHG=L2.USR "//�滻��Ա
                        + "LEFT JOIN SPC_LST L3 ON L3.SPC_NO=L.CHG_SPC_NO "
                        + "LEFT JOIN TF_PSS SBJ ON SBJ.PS_ID=J.PS_ID AND SBJ.PS_NO=J.PS_NO AND SBJ.EST_ITM=J.PRE_ITM AND SBJ.PS_ID='SB'"
                        + "LEFT JOIN MF_PSS SBC ON SBC.OS_ID=SBJ.PS_ID AND SBC.OS_NO=SBJ.PS_NO "
                        + "LEFT JOIN MY_WH SBD ON SBD.WH=SBJ.WH "
                        + "LEFT JOIN CUST SBE ON SBE.CUS_NO=SBC.CUS_NO "
                        + "LEFT JOIN PRDT SBF ON SBF.PRD_NO=SBJ.PRD_NO "
                        + "LEFT JOIN INDX SBG ON SBG.IDX_NO=SBF.IDX1 "
                        + "WHERE 1=1 AND A.PS_ID='SA'";
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql + _where);
            _ds.Tables[0].TableName = "TF_PSS3";
            return _ds;
        }
        /// <summary>
        /// ȡ���������к�
        /// </summary>
        /// <param name="_where"></param>
        /// <returns></returns>
        public DataSet GetSABarRec(string _where)
        {
            //ȡ���������к�
            string _sql = "SET ROWCOUNT 10000;SELECT A.*,B.WH,D.NAME AS WH_NAME,H.BAT_NO,C.CUS_NO,E.NAME AS CUS_NAME,F.SPC,G.NAME AS IDX_NAME,H.QC,H.SPC_NO,I.NAME AS SPC_NAME,H.REM AS QC_REM,L.CHG_SPC_NO,M.NAME AS CHG_SPC_NAME,L.REM,"
                        + "(CASE ISNULL(J.BAR_CODE,'') WHEN '' THEN 'F' ELSE 'T' END) AS CHK_COL,(CASE ISNULL(K.BAR_CODE,'') WHEN '' THEN 'F' ELSE 'T' END) AS IS_CHANGED,C.LOCK_MAN "
                        + "FROM TF_PSS3 A "
                        + "INNER JOIN BAR_REC B ON B.BAR_NO=A.BAR_CODE "
                        + "INNER JOIN MF_PSS C ON C.PS_ID=A.PS_ID AND C.PS_NO=A.PS_NO "
                        + "LEFT JOIN MY_WH D ON D.WH=B.WH "
                        + "LEFT JOIN CUST E ON E.CUS_NO=C.CUS_NO "
                        + "LEFT JOIN PRDT F ON F.PRD_NO=A.PRD_NO "
                        + "LEFT JOIN INDX G ON G.IDX_NO=F.IDX1 "
                        + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC H ON H.BAR_NO=A.BAR_CODE "
                        + "LEFT JOIN SPC_LST I ON I.SPC_NO=H.SPC_NO "
                        + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..SA_BAR_CHG J ON J.PS_ID=A.PS_ID AND J.PS_NO=A.PS_NO AND J.PS_ITM=A.PS_ITM AND J.ITM=A.ITM AND J.BAR_CODE=A.BAR_CODE "
                        + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..SA_BAR_CHG K ON K.PS_ID=A.PS_ID AND K.PS_NO=A.PS_NO AND K.PS_ITM=A.PS_ITM AND K.ITM=A.ITM AND K.BAR_CODE<>A.BAR_CODE "
                        + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..SA_BAR_CHG L ON L.PS_ID=A.PS_ID AND L.PS_NO=A.PS_NO AND L.PS_ITM=A.PS_ITM AND L.ITM=A.ITM "
                        + "LEFT JOIN SPC_LST M ON M.SPC_NO=L.CHG_SPC_NO "
                        + "WHERE 1=1 ";
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql + _where);
            _ds.Tables[0].TableName = "TF_PSS3";
            return _ds;
        }
        /// <summary>
        /// ��������Ʒ��
        /// </summary>
        /// <param name="_dsSA"></param>
        public string SABarQC(DataSet _dsSA)
        {
            string _result = "";
            if (_dsSA != null && _dsSA.Tables.Count > 0 && _dsSA.Tables[0].Rows.Count > 0)
            {
                try
                {
                    DataSet _changeDs = GetSABarChg(" AND A.PS_ID='SA' AND A.PS_NO='" + _dsSA.Tables[0].Rows[0]["PS_NO"].ToString() + "' ");
                    DataRow _drNew;
                    DataRow[] _drArr;
                    foreach (DataRow _dr in _changeDs.Tables[0].Select("isnull(CHG_ID,'')<>'T'"))
                    {
                        _dr.Delete();
                    }
                    foreach (DataRow _dr in _dsSA.Tables[0].Select("CHK_COL='T'"))
                    {
                        _drArr = _changeDs.Tables[0].Select("PS_ID='SA' AND PS_NO='" + _dr["PS_NO"].ToString() + "' and PS_ITM='" + _dr["PS_ITM"].ToString() + "' and ITM='" + _dr["ITM"].ToString() + "' ");
                        foreach (DataRow _drDel in _drArr)
                        {
                            _drDel.Delete();
                        }
                        _drNew = _changeDs.Tables[0].NewRow();
                        _drNew["PS_ID"] = _dr["PS_ID"];
                        _drNew["PS_NO"] = _dr["PS_NO"];
                        _drNew["PS_ITM"] = _dr["PS_ITM"];
                        _drNew["ITM"] = _dr["ITM"];
                        _drNew["BAR_CODE"] = _dr["BAR_CODE"];
                        _drNew["BOX_NO"] = _dr["BOX_NO"];
                        _drNew["USR_QC"] = _dr["USR_QC"];
                        _drNew["QC_DATE"] = _dr["QC_DATE"];
                        _drNew["CHG_SPC_NO"] = _dr["CHG_SPC_NO"];
                        _drNew["REM"] = _dr["REM"];
                        _changeDs.Tables[0].Rows.Add(_drNew);
                    }
                    DataTable _dtErr = UpdataDataSA_BAR_CHG(_changeDs);
                    _result = GetErrorString(_dtErr);
                }
                catch (Exception _ex)
                {
                    _result = _ex.Message;
                }
            }
            if (_result.Length > 500)
                _result = _result.Substring(0, 500);
            return _result;
        }
        /// <summary>
        /// ���±�SA_BAR_CHG
        /// </summary>
        /// <param name="ChangedDS"></param>
        /// <returns></returns>
        public DataTable UpdataDataSA_BAR_CHG(DataSet ChangedDS)
        {
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["SA_BAR_CHG"] = "PS_ID,PS_NO,PS_ITM,ITM,BAR_CODE,BOX_NO,USR_QC,QC_DATE,USR_CHG,CHG_DATE,CHG_ID,CHG_SPC_NO,REM";
            this.UpdateDataSet(Comp.Conn_SunSystem.Replace("sunsystem", Comp.DRP_Prop["BarPrintDB"].ToString()), ChangedDS, _ht);
            DataTable _dt = GetAllErrors(ChangedDS);
            return _dt;
        }
        /// <summary>
        /// ȡ���������滻���к�
        /// </summary>
        /// <param name="_where"></param>
        /// <returns></returns>
        public DataSet GetSABarChg(string _where)
        {
            //ȡ���������滻���к�
            string _sql = "SELECT A.*,B.WH,D.NAME AS WH_NAME,B.BAT_NO,C.CUS_NO,E.NAME AS CUS_NAME,F.SPC,G.NAME AS IDX_NAME,H.QC,H.SPC_NO,I.NAME AS SPC_NAME,H.REM AS QC_REM FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..SA_BAR_CHG A "
                        + "INNER JOIN BAR_REC B ON B.BAR_NO=A.BAR_CODE "
                        + "INNER JOIN MF_PSS C ON C.PS_ID=A.PS_ID AND C.PS_NO=A.PS_NO "
                        + "LEFT JOIN MY_WH D ON D.WH=B.WH "
                        + "LEFT JOIN CUST E ON E.CUS_NO=C.CUS_NO "
                        + "LEFT JOIN PRDT F ON F.PRD_NO=B.PRD_NO "
                        + "LEFT JOIN INDX G ON G.IDX_NO=F.IDX1 "
                        + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC H ON H.BAR_NO=A.BAR_CODE "
                        + "LEFT JOIN SPC_LST I ON I.SPC_NO=H.SPC_NO "
                        + "WHERE 1=1 ";
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql + _where);
            _ds.Tables[0].TableName = "SA_BAR_CHG";
            return _ds;
        }
        #endregion

        #region ���������滻
        /// <summary>
        /// ȡ�����滻����
        /// </summary>
        /// <param name="_where"></param>
        /// <returns></returns>
        public DataSet GetChgBar(string _where)
        {
            string _sql = "SELECT A.*,B.NAME AS WH_NAME,C.SPC,D.NAME AS IDX_NAME,E.QC,E.SPC_NO AS SPC_NO1,F.NAME AS SPC_NAME,E.REM AS QC_REM,G.PRN_DD,SUBSTRING(A.BAR_NO,16,1) AS JT,MM.MM_NO "
                        + "FROM BAR_REC A "
                        + "LEFT JOIN MY_WH B ON B.WH=A.WH "
                        + "LEFT JOIN PRDT C ON C.PRD_NO=A.PRD_NO "
                        + "LEFT JOIN INDX D ON D.IDX_NO=C.IDX1 "
                        + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC E ON E.BAR_NO=A.BAR_NO "
                        + "LEFT JOIN SPC_LST F ON F.SPC_NO=E.SPC_NO "
                        + "LEFT JOIN BAR_PRINT G ON G.BAR_NO=A.BAR_NO "
                        + "left join TF_MM0_B MM on MM.BAR_CODE=A.BAR_NO "
                        + "WHERE 1=1 ";
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql + _where);
            _ds.Tables[0].TableName = "BAR_REC";
            return _ds;
        }
        /// <summary>
        /// ���������滻
        /// </summary>
        /// <param name="changeDs"></param>
        /// <returns></returns>
        public string ChangeSABar(DataSet changeDs)
        {
            string _error = "";
            //DRPSA _sa = new DRPSA();
            //SunlikeDataSet _ds = new SunlikeDataSet();
            if (changeDs != null && changeDs.Tables.Contains("TF_PSS3") && changeDs.Tables["TF_PSS3"].Rows.Count > 0)
            {
                //string ps_id = "";
                //string ps_no = "";
                //if (changeDs.Tables["TF_PSS3"].Rows[0].RowState == DataRowState.Deleted)
                //{
                //    ps_id = changeDs.Tables["TF_PSS3"].Rows[0]["PS_ID", DataRowVersion.Original].ToString();
                //    ps_no = changeDs.Tables["TF_PSS3"].Rows[0]["PS_NO", DataRowVersion.Original].ToString();
                //}
                //else
                //{
                //    ps_id = changeDs.Tables["TF_PSS3"].Rows[0]["PS_ID"].ToString();
                //    ps_no = changeDs.Tables["TF_PSS3"].Rows[0]["PS_NO"].ToString();
                //}
                //_ds = _sa.GetData("", ps_id, ps_no);
                //if (_ds.Tables.Contains("TF_PSS_BAR"))
                //{
                //    changeDs.Tables["TF_PSS3"].TableName = "TF_PSS_BAR";
                //    _ds.Tables["TF_PSS_BAR"].Merge(changeDs.Tables["TF_PSS_BAR"], false, MissingSchemaAction.Ignore);
                //    DataRow[] _drArr;
                //    foreach (DataRow dr in _ds.Tables["TF_PSS_BAR"].Rows)
                //    {
                //        _drArr = _ds.Tables["TF_PSS"].Select("PRE_ITM=" + dr["PS_ITM"].ToString() + "");
                //        if (_drArr.Length>0 && _drArr[0].RowState == DataRowState.Unchanged)
                //            _drArr[0].SetModified();
                //    }
                //    DataTable _dtErr = _sa.UpdateData(_ds);
                //    if (_dtErr.Rows.Count > 0)
                //    {
                //        _error = this.GetErrorString(_dtErr);
                //    }
                //}
                //else
                //{
                //    _error = "�����������¼�����ڣ�";
                //}

                DataTable _dtErr = UpdataDataTF_PSS3(changeDs);
                if (_dtErr.Rows.Count > 0)
                {
                    _error = this.GetErrorString(_dtErr);
                }
            }
            else
            {
                _error = "Ҫ���µļ�¼Ϊ�գ����顣";
            }
            return _error;
        }
        /// <summary>
        /// ȡ��������TF_PSS3������
        /// </summary>
        /// <param name="saNo">��������</param>
        /// <returns></returns>
        public int GetSAMaxItm(string saNo)
        {
            int maxItm = 1;
            Query _query = new Query();
            SunlikeDataSet _ds = _query.DoSQLString("select max(ITM) from TF_PSS3 where PS_NO='" + saNo + "'");
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                maxItm = Convert.ToInt32(_ds.Tables[0].Rows[0][0]);
            }
            return maxItm;
        }
        /// <summary>
        /// ���±�TF_PSS3
        /// </summary>
        /// <param name="ChangedDS"></param>
        /// <returns></returns>
        public DataTable UpdataDataTF_PSS3(DataSet ChangedDS)
        {
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["TF_PSS3"] = "PS_ID,PS_NO,PS_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO";
            this.UpdateDataSet(ChangedDS, _ht);
            DataTable _dt = GetAllErrors(ChangedDS);
            return _dt;
        }
        /// <summary>
        /// ���±�TF_PSS3
        /// </summary>
        /// <param name="ChangedDS"></param>
        /// <returns></returns>
        public string LockSA(string _saNo, string _lockMan)
        {
            string _err = "";
            try
            {
                Query _query = new Query();
                _query.DoSQLString("update MF_PSS set LOCK_MAN='" + _lockMan + "' where PS_ID='SA' and PS_NO='" + _saNo + "'");
            }
            catch (Exception _ex)
            {
                if (_ex.Message.Length > 500)
                    _err = _ex.Message.Substring(0, 500);
                else
                    _err = _ex.Message;
            }
            return _err;
        }
        #endregion

        #region ȡ���к�Ʒ����Ϣ
        /// <summary>
        /// ȡ���к�Ʒ����Ϣ
        /// </summary>
        /// <param name="_where">����</param>
        /// <param name="_type">��Ʒ���1����ģ�2�����Ʒ��3����Ʒ</param>
        /// <returns></returns>
        public DataSet GetBarQCSta(string _where, int _type)
        {
            Query _query = new Query();
            DataSet _ds = new DataSet();
            string _sql ="";
            if (_type == 1)
            {
                //���
                _sql = "SELECT A.BAR_NO AS BC_BAR_NO,A.PRD_NO AS BC_PRD_NO,BCWH.NAME AS BC_WH_NAME,BCI.NAME AS BC_IDX_NAME,BCP.SPC AS BC_SPC,convert(decimal(28,3),BCPZ.QTY_PER_GP) AS BC_QTY,SUBSTRING(A.BAR_NO,13,7) AS BC_BAT_NO,SUBSTRING(A.BAR_NO,24,3) AS BC_SN,"
                     + "BCQC.FORMAT AS BC_FORMAT,BCQC.LH AS BC_LH,BCQC.PRT_NAME AS BC_PRT_NAME,BCQC.QC as BC_QC,BCQC.PRC_ID as BC_PRC_ID,"
                     + "BCS.NAME AS BC_SPC_NAME,BCQC.REM AS BC_QC_REM,BCMO.BIL_TYPE AS BC_BIL_TYPE,BCQC.BIL_NO AS BC_MO_NO,BCMOC.SNM AS BC_MO_CUS_NAME,BCWHSJ.NAME AS BC_SJ_WH_NAME,BCMM.MM_NO AS BC_MM_NO,BCMMWH.NAME AS BC_MM_WH_NAME,"
                     + "BCSA.PS_NO AS BC_SA_NO,BCSAC.NAME AS BC_SA_CUS_NAME,BCSB.PS_NO BC_SB_NO,BCSB.REM AS BC_SB_REM,DFML.ML_NO AS DF_ML_NO "
                     + "FROM BAR_REC A  WITH (NOLOCK) "
                     + "LEFT JOIN MY_WH BCWH ON BCWH.WH=A.WH "
                     + "LEFT JOIN PRDT BCP ON BCP.PRD_NO=A.PRD_NO "
                     + "LEFT JOIN PRDT_Z BCPZ ON BCPZ.PRD_NO=A.PRD_NO "
                     + "LEFT JOIN INDX BCI ON BCI.IDX_NO=BCP.IDX1 "
                     + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC BCQC ON BCQC.BAR_NO=A.BAR_NO "
                     + "LEFT JOIN MF_MO BCMO ON BCMO.MO_NO=BCQC.BIL_NO "
                     + "LEFT JOIN CUST BCMOC ON BCMOC.CUS_NO=BCMO.CUS_NO "
                     + "LEFT JOIN SPC_LST BCS ON BCS.SPC_NO=BCQC.SPC_NO "
                     + "LEFT JOIN MY_WH BCWHSJ ON BCWHSJ.WH=BCQC.WH_SJ "
                     + "LEFT JOIN TF_MM0_B BCMMB on BCMMB.BAR_CODE=A.BAR_NO "
                     + "LEFT JOIN TF_MM0 BCMM on BCMMB.MM_ID=BCMM.MM_ID and BCMMB.MM_NO=BCMM.MM_NO and BCMMB.MM_ITM=BCMM.ITM "
                     + "LEFT JOIN MY_WH BCMMWH ON BCMMWH.WH=BCMM.WH "
                     + "LEFT JOIN TF_PSS3 BCSA3 ON BCSA3.PS_ID='SA' AND BCSA3.BAR_CODE=A.BAR_NO "
                     + "LEFT JOIN MF_PSS BCSA ON BCSA.PS_ID=BCSA3.PS_ID AND BCSA.PS_NO=BCSA3.PS_NO "
                     + "LEFT JOIN CUST BCSAC ON BCSAC.CUS_NO=BCSA.CUS_NO "
                     + "LEFT JOIN TF_PSS3 BCSB3 ON BCSB3.PS_ID='SB' AND BCSB3.BAR_CODE=A.BAR_NO "
                     + "LEFT JOIN MF_PSS BCSB ON BCSB.PS_ID=BCSB3.PS_ID AND BCSB.PS_NO=BCSB3.PS_NO "
                     + "LEFT JOIN TF_ML_B DFML ON DFML.MLID='ML' AND DFML.BAR_CODE=A.BAR_NO "
                     + "WHERE 1=1 " + _where;
                _ds = _query.FillDataSet(_sql.ToString(), null, new string[] { "BARQC_RPT" }, false);
            }
            else if (_type == 2)
            {
                //���Ʒ
                _sql = "SELECT A.BAR_NO AS BC_BAR_NO,SUBSTRING(A.BAR_NO,13,7) AS BC_BAT_NO,DF.BAR_NO AS DF_BAR_NO,DF.PRD_NO AS DF_PRD_NO,DFWH.NAME AS DF_WH_NAME,DFI.NAME AS DF_IDX_NAME,DFP.SPC AS DF_SPC,convert(decimal(28,3),DFPZ.QTY_PER_GP) as DF_QTY,SUBSTRING(DF.BAR_NO,13,10) AS DF_BAT_NO,SUBSTRING(DF.BAR_NO,24,1) AS DF_SN,"
                     + "DFQC.QC as DF_QC,DFQC.PRC_ID as DF_PRC_ID,DFS.NAME AS DF_SPC_NAME,DFQC.REM AS DF_QC_REM,"
                     + "DFMO.BIL_TYPE AS DF_BIL_TYPE,DFQC.BIL_NO AS DF_MO_NO,DFMOC.SNM AS DF_MO_CUS_NAME,DFWHSJ.NAME AS DF_SJ_WH_NAME,DFMM.MM_NO AS DF_MM_NO,DFMMWH.NAME AS DF_MM_WH_NAME,"
                     + "DFSA.PS_NO AS DF_SA_NO,DFSAC.NAME AS DF_SA_CUS_NAME,DFSB.PS_NO DF_SB_NO,DFSB.REM AS DF_SB_REM,FTML.ML_NO AS FT_ML_NO "
                     + "FROM BAR_REC A  WITH (NOLOCK) "
                     + "LEFT JOIN PRDT BCP ON BCP.PRD_NO=A.PRD_NO "
                     + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC BCQC ON BCQC.BAR_NO=A.BAR_NO "
                     + "LEFT JOIN TF_PSS3 BCSA3 ON BCSA3.PS_ID='SA' AND BCSA3.BAR_CODE=A.BAR_NO "
                     + "LEFT JOIN MF_PSS BCSA ON BCSA.PS_ID=BCSA3.PS_ID AND BCSA.PS_NO=BCSA3.PS_NO "
                     + "LEFT JOIN TF_PSS3 BCSB3 ON BCSB3.PS_ID='SB' AND BCSB3.BAR_CODE=A.BAR_NO "
                     + "LEFT JOIN MF_PSS BCSB ON BCSB.PS_ID=BCSB3.PS_ID AND BCSB.PS_NO=BCSB3.PS_NO "
                     + "INNER JOIN BAR_REC DF ON REPLACE(SUBSTRING(A.BAR_NO,13,14),'#','')=SUBSTRING(DF.BAR_NO,13,10) AND LEN(REPLACE(DF.BAR_NO,'#',''))=24 "
                     + "LEFT JOIN MY_WH DFWH ON DFWH.WH=DF.WH "
                     + "LEFT JOIN PRDT DFP ON DFP.PRD_NO=DF.PRD_NO "
                     + "LEFT JOIN PRDT_Z DFPZ ON DFPZ.PRD_NO=DF.PRD_NO "
                     + "LEFT JOIN INDX DFI ON DFI.IDX_NO=DFP.IDX1 "
                     + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC DFQC ON DF.BAR_NO=DFQC.BAR_NO "
                     + "LEFT JOIN MF_MO DFMO ON DFMO.MO_NO=DFQC.BIL_NO "
                     + "LEFT JOIN CUST DFMOC ON DFMOC.CUS_NO=DFMO.CUS_NO "
                     + "LEFT JOIN SPC_LST DFS ON DFS.SPC_NO=DFQC.SPC_NO "
                     + "LEFT JOIN MY_WH DFWHSJ ON DFWHSJ.WH=DFQC.WH_SJ "
                     + "LEFT JOIN TF_MM0_B DFMMB on DFMMB.BAR_CODE=DF.BAR_NO "
                     + "LEFT JOIN TF_MM0 DFMM on DFMMB.MM_ID=DFMM.MM_ID and DFMMB.MM_NO=DFMM.MM_NO and DFMMB.MM_ITM=DFMM.ITM "
                     + "LEFT JOIN MY_WH DFMMWH ON DFMMWH.WH=DFMM.WH "
                     + "LEFT JOIN TF_PSS3 DFSA3 ON DFSA3.PS_ID='SA' AND DFSA3.BAR_CODE=DF.BAR_NO "
                     + "LEFT JOIN MF_PSS DFSA ON DFSA.PS_ID=DFSA3.PS_ID AND DFSA.PS_NO=DFSA3.PS_NO "
                     + "LEFT JOIN CUST DFSAC ON DFSAC.CUS_NO=DFSA.CUS_NO "
                     + "LEFT JOIN TF_PSS3 DFSB3 ON DFSB3.PS_ID='SB' AND DFSB3.BAR_CODE=DF.BAR_NO "
                     + "LEFT JOIN MF_PSS DFSB ON DFSB.PS_ID=DFSB3.PS_ID AND DFSB.PS_NO=DFSB3.PS_NO "
                     + "LEFT JOIN TF_ML_B FTML ON FTML.MLID='ML' AND FTML.BAR_CODE=DF.BAR_NO "
                     + "WHERE 1=1 " + _where;

                using (SqlConnection _sqlConn = new SqlConnection(Comp.Conn_DB))
                {
                    SqlCommand _sqlCmd = new SqlCommand(_sql.ToString(), _sqlConn);
                    _sqlCmd.CommandTimeout = 120;
                    using (SqlDataAdapter _sqlAdp = new SqlDataAdapter(_sqlCmd))
                    {
                        _sqlAdp.Fill(_ds);
                        _ds.Tables[0].TableName = "BARQC_RPT";
                    }
                }
                //_ds = _query.FillDataSet(_sql.ToString(), null, new string[] { "BARQC_RPT" }, false);
            }
            else if (_type == 3)
            {
                //��Ʒ
                _sql = "SELECT A.BAR_NO AS BC_BAR_NO,SUBSTRING(A.BAR_NO,13,7) AS BC_BAT_NO,DF.BAR_NO AS DF_BAR_NO,FT.BAR_NO AS FT_BAR_NO,FT.PRD_NO AS FT_PRD_NO,FTWH.NAME AS FT_WH_NAME,FTI.NAME AS FT_IDX_NAME,FTP.SPC AS FT_SPC,convert(decimal(28,3),FTPZ.QTY_PER_GP) as FT_QTY,SUBSTRING(FT.BAR_NO,13,10) AS FT_BAT_NO,REPLACE(SUBSTRING(FT.BAR_NO,24,4),'#','') AS FT_SN,"
                     + "FTQC.QC as FT_QC,FTQC.PRC_ID as FT_PRC_ID,FTS.NAME AS FT_SPC_NAME,FTQC.REM AS FT_QC_REM,"
                     + "FT.BOX_NO,BOX.PRD_NO AS BOX_PRD_NO,BOXI.NAME AS BOX_IDX_NAME,BOXP.SPC AS BOX_SPC,BOX.QTY AS BOX_QTY,BOX_BAT.IDX_NAME AS BOX_PRT_NAME,BOX_BAT.FORMAT AS BOX_FORMAT,"
                     + "FTMO.BIL_TYPE AS FT_BIL_TYPE,FTQC.BIL_NO AS FT_MO_NO,FTMOC.SNM AS FT_MO_CUS_NAME,FTWHSJ.NAME AS FT_SJ_WH_NAME,FTMM.MM_NO AS FT_MM_NO,FTMMWH.NAME AS FT_MM_WH_NAME,"
                     + "FTSA.PS_NO AS FT_SA_NO,FTSAC.NAME AS FT_SA_CUS_NAME,FTSB.PS_NO FT_SB_NO,FTSB.REM AS FT_SB_REM "
                     + "FROM BAR_REC A  WITH (NOLOCK) "
                     + "LEFT JOIN PRDT BCP ON BCP.PRD_NO=A.PRD_NO "
                     + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC BCQC ON BCQC.BAR_NO=A.BAR_NO "
                     + "LEFT JOIN TF_PSS3 BCSA3 ON BCSA3.PS_ID='SA' AND BCSA3.BAR_CODE=A.BAR_NO "
                     + "LEFT JOIN MF_PSS BCSA ON BCSA.PS_ID=BCSA3.PS_ID AND BCSA.PS_NO=BCSA3.PS_NO "
                     + "LEFT JOIN TF_PSS3 BCSB3 ON BCSB3.PS_ID='SB' AND BCSB3.BAR_CODE=A.BAR_NO "
                     + "LEFT JOIN MF_PSS BCSB ON BCSB.PS_ID=BCSB3.PS_ID AND BCSB.PS_NO=BCSB3.PS_NO "
                     + "left join BAR_REC DF ON REPLACE(SUBSTRING(A.BAR_NO,13,14),'#','')=SUBSTRING(DF.BAR_NO,13,10) AND LEN(REPLACE(DF.BAR_NO,'#',''))=24 "
                     + "INNER JOIN BAR_REC FT ON REPLACE(SUBSTRING(DF.BAR_NO,13,14),'#','')=SUBSTRING(FT.BAR_NO,13,12) AND LEN(REPLACE(FT.BAR_NO,'#',''))=26 "
                     + "LEFT JOIN MY_WH FTWH ON FTWH.WH=FT.WH "
                     + "LEFT JOIN PRDT FTP ON FTP.PRD_NO=FT.PRD_NO "
                     + "LEFT JOIN PRDT_Z FTPZ ON FTPZ.PRD_NO=FT.PRD_NO "
                     + "LEFT JOIN INDX FTI ON FTI.IDX_NO=FTP.IDX1 "
                     + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC FTQC ON FT.BAR_NO=FTQC.BAR_NO "
                     + "LEFT JOIN BAR_BOX BOX ON BOX.BOX_NO=FT.BOX_NO "
                     + "LEFT JOIN PRDT BOXP ON BOXP.PRD_NO=BOX.PRD_NO "
                     + "LEFT JOIN INDX BOXI ON BOXI.IDX_NO=BOXP.IDX1 "
                     + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT BOX_BAT ON BOX_BAT.BOX_NO=FT.BOX_NO "
                     + "LEFT JOIN MF_MO FTMO ON FTMO.MO_NO=FTQC.BIL_NO "
                     + "LEFT JOIN CUST FTMOC ON FTMOC.CUS_NO=FTMO.CUS_NO "
                     + "LEFT JOIN SPC_LST FTS ON FTS.SPC_NO=FTQC.SPC_NO "
                     + "LEFT JOIN MY_WH FTWHSJ ON FTWHSJ.WH=FTQC.WH_SJ "
                     + "LEFT JOIN TF_MM0_B FTMMB on FTMMB.BAR_CODE=FT.BAR_NO "
                     + "LEFT JOIN TF_MM0 FTMM on FTMMB.MM_ID=FTMM.MM_ID and FTMMB.MM_NO=FTMM.MM_NO and FTMMB.MM_ITM=FTMM.ITM "
                     + "LEFT JOIN MY_WH FTMMWH ON FTMMWH.WH=FTMM.WH "
                     + "LEFT JOIN TF_PSS3 FTSA3 ON FTSA3.PS_ID='SA' AND FTSA3.BAR_CODE=FT.BAR_NO "
                     + "LEFT JOIN MF_PSS FTSA ON FTSA.PS_ID=FTSA3.PS_ID AND FTSA.PS_NO=FTSA3.PS_NO "
                     + "LEFT JOIN CUST FTSAC ON FTSAC.CUS_NO=FTSA.CUS_NO "
                     + "LEFT JOIN TF_PSS3 FTSB3 ON FTSB3.PS_ID='SB' AND FTSB3.BAR_CODE=FT.BAR_NO "
                     + "LEFT JOIN MF_PSS FTSB ON FTSB.PS_ID=FTSB3.PS_ID AND FTSB.PS_NO=FTSB3.PS_NO "
                     + "WHERE 1=1 " + _where;

                using (SqlConnection _sqlConn = new SqlConnection(Comp.Conn_DB))
                {
                    SqlCommand _sqlCmd = new SqlCommand(_sql.ToString(), _sqlConn);
                    _sqlCmd.CommandTimeout = 120;
                    using (SqlDataAdapter _sqlAdp = new SqlDataAdapter(_sqlCmd))
                    {
                        _sqlAdp.Fill(_ds);
                        _ds.Tables[0].TableName = "BARQC_RPT";
                    }
                }
                //_ds = _query.FillDataSet(_sql.ToString(), null, new string[] { "BARQC_RPT" }, false);
            }
            else
            {
                //��Ʒ(���ֱ����)
                _sql = "SELECT A.BAR_NO AS BC_BAR_NO,SUBSTRING(A.BAR_NO,13,7) AS BC_BAT_NO,FT.BAR_NO AS FT_BAR_NO,FT.PRD_NO AS FT_PRD_NO,FTWH.NAME AS FT_WH_NAME,FTI.NAME AS FT_IDX_NAME,FTP.SPC AS FT_SPC,convert(decimal(28,3),FTPZ.QTY_PER_GP) as FT_QTY,SUBSTRING(FT.BAR_NO,13,10) AS FT_BAT_NO,REPLACE(SUBSTRING(FT.BAR_NO,24,4),'#','') AS FT_SN,"
                     + "FTQC.QC as FT_QC,FTQC.PRC_ID as FT_PRC_ID,FTS.NAME AS FT_SPC_NAME,FTQC.REM AS FT_QC_REM,"
                     + "FT.BOX_NO,BOX.PRD_NO AS BOX_PRD_NO,BOXI.NAME AS BOX_IDX_NAME,BOXP.SPC AS BOX_SPC,BOX.QTY AS BOX_QTY,BOX_BAT.IDX_NAME AS BOX_PRT_NAME,BOX_BAT.FORMAT AS BOX_FORMAT,"
                     + "FTMO.BIL_TYPE AS FT_BIL_TYPE,FTQC.BIL_NO AS FT_MO_NO,FTMOC.SNM AS FT_MO_CUS_NAME,FTWHSJ.NAME AS FT_SJ_WH_NAME,FTMM.MM_NO AS FT_MM_NO,FTMMWH.NAME AS FT_MM_WH_NAME,"
                     + "FTSA.PS_NO AS FT_SA_NO,FTSAC.NAME AS FT_SA_CUS_NAME,FTSB.PS_NO FT_SB_NO,FTSB.REM AS FT_SB_REM "
                     + "FROM BAR_REC A  WITH (NOLOCK) "
                     + "LEFT JOIN PRDT BCP ON BCP.PRD_NO=A.PRD_NO "
                     + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC BCQC ON BCQC.BAR_NO=A.BAR_NO "
                     + "LEFT JOIN TF_PSS3 BCSA3 ON BCSA3.PS_ID='SA' AND BCSA3.BAR_CODE=A.BAR_NO "
                     + "LEFT JOIN MF_PSS BCSA ON BCSA.PS_ID=BCSA3.PS_ID AND BCSA.PS_NO=BCSA3.PS_NO "
                     + "LEFT JOIN TF_PSS3 BCSB3 ON BCSB3.PS_ID='SB' AND BCSB3.BAR_CODE=A.BAR_NO "
                     + "LEFT JOIN MF_PSS BCSB ON BCSB.PS_ID=BCSB3.PS_ID AND BCSB.PS_NO=BCSB3.PS_NO "
                     + "INNER JOIN BAR_REC FT ON REPLACE(SUBSTRING(A.BAR_NO,13,14),'#','')=SUBSTRING(FT.BAR_NO,13,10) AND LEN(REPLACE(FT.BAR_NO,'#',''))=25 "
                     + "LEFT JOIN MY_WH FTWH ON FTWH.WH=FT.WH "
                     + "LEFT JOIN PRDT FTP ON FTP.PRD_NO=FT.PRD_NO "
                     + "LEFT JOIN PRDT_Z FTPZ ON FTPZ.PRD_NO=FT.PRD_NO "
                     + "LEFT JOIN INDX FTI ON FTI.IDX_NO=FTP.IDX1 "
                     + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC FTQC ON FT.BAR_NO=FTQC.BAR_NO "
                     + "LEFT JOIN BAR_BOX BOX ON BOX.BOX_NO=FT.BOX_NO "
                     + "LEFT JOIN PRDT BOXP ON BOXP.PRD_NO=BOX.PRD_NO "
                     + "LEFT JOIN INDX BOXI ON BOXI.IDX_NO=BOXP.IDX1 "
                     + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT BOX_BAT ON BOX_BAT.BOX_NO=FT.BOX_NO "
                     + "LEFT JOIN MF_MO FTMO ON FTMO.MO_NO=FTQC.BIL_NO "
                     + "LEFT JOIN CUST FTMOC ON FTMOC.CUS_NO=FTMO.CUS_NO "
                     + "LEFT JOIN SPC_LST FTS ON FTS.SPC_NO=FTQC.SPC_NO "
                     + "LEFT JOIN MY_WH FTWHSJ ON FTWHSJ.WH=FTQC.WH_SJ "
                     + "LEFT JOIN TF_MM0_B FTMMB on FTMMB.BAR_CODE=FT.BAR_NO "
                     + "LEFT JOIN TF_MM0 FTMM on FTMMB.MM_ID=FTMM.MM_ID and FTMMB.MM_NO=FTMM.MM_NO and FTMMB.MM_ITM=FTMM.ITM "
                     + "LEFT JOIN MY_WH FTMMWH ON FTMMWH.WH=FTMM.WH "
                     + "LEFT JOIN TF_PSS3 FTSA3 ON FTSA3.PS_ID='SA' AND FTSA3.BAR_CODE=FT.BAR_NO "
                     + "LEFT JOIN MF_PSS FTSA ON FTSA.PS_ID=FTSA3.PS_ID AND FTSA.PS_NO=FTSA3.PS_NO "
                     + "LEFT JOIN CUST FTSAC ON FTSAC.CUS_NO=FTSA.CUS_NO "
                     + "LEFT JOIN TF_PSS3 FTSB3 ON FTSB3.PS_ID='SB' AND FTSB3.BAR_CODE=FT.BAR_NO "
                     + "LEFT JOIN MF_PSS FTSB ON FTSB.PS_ID=FTSB3.PS_ID AND FTSB.PS_NO=FTSB3.PS_NO "
                     + "WHERE 1=1 " + _where;

                using (SqlConnection _sqlConn = new SqlConnection(Comp.Conn_DB))
                {
                    SqlCommand _sqlCmd = new SqlCommand(_sql.ToString(), _sqlConn);
                    _sqlCmd.CommandTimeout = 120;
                    using (SqlDataAdapter _sqlAdp = new SqlDataAdapter(_sqlCmd))
                    {
                        _sqlAdp.Fill(_ds);
                        _ds.Tables[0].TableName = "BARQC_RPT";
                    }
                }
                //_ds = _query.FillDataSet(_sql.ToString(), null, new string[] { "BARQC_RPT" }, false);
            }
            return _ds;
        }
        public DataSet GetRptBarQCList(string _where)
        {
            string _sql = "";
            _sql = "SELECT A.BAR_NO,A.BOX_NO,D.NAME AS IDX_NAME,C.SPC,REPLACE(SUBSTRING(A.BAR_NO,13,10),'#','') as BAT_NO,E.LH,E.PRT_NAME,"
                 + "convert(decimal(28,3),CZ.QTY_PER_GP) as QTY_PER_GP,B.NAME AS WH_NAME,SUBSTRING(A.BAR_NO,16,1) AS JT,"
                 + "E.QC,E.PRC_ID,F.NAME AS SPC_NAME,E.REM AS QC_REM,E1.NAME AS PRN_USR,E.SYS_DATE AS PRN_DD,E2.NAME AS QC_USR,E.QC_DATE,"
                 + "E.TO_CUST,CU.NAME AS TO_CUST_NAME,E3.NAME AS SJ_USR,E.SJ_DATE,E.BIL_NO,G.NAME AS WH_NAME_SJ,MM.MM_NO,A.STOP_ID,A.PRD_NO,E.WH_SJ,A.WH,A.REM "
                 + "FROM BAR_REC A  WITH (NOLOCK) "
                 + "LEFT JOIN MY_WH B ON B.WH=A.WH "
                 + "LEFT JOIN PRDT C ON C.PRD_NO=A.PRD_NO "
                 + "LEFT JOIN PRDT_Z CZ ON CZ.PRD_NO=A.PRD_NO "
                 + "LEFT JOIN INDX D ON D.IDX_NO=C.IDX1 "
                 + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC E ON E.BAR_NO=A.BAR_NO "
                 + "LEFT JOIN SUNSYSTEM..PSWD E1 ON E1.COMPNO='" + Comp.CompNo + "' AND E1.USR=E.USR "
                 + "LEFT JOIN SUNSYSTEM..PSWD E2 ON E2.COMPNO='" + Comp.CompNo + "' AND E2.USR=E.USR_QC "
                 + "LEFT JOIN SUNSYSTEM..PSWD E3 ON E3.COMPNO='" + Comp.CompNo + "' AND E3.USR=E.USR_SJ "
                 + "LEFT JOIN CUST CU ON CU.CUS_NO=E.TO_CUST "
                 + "LEFT JOIN SPC_LST F ON F.SPC_NO=E.SPC_NO "
                 + "LEFT JOIN TF_MM0_B MM on MM.BAR_CODE=A.BAR_NO "
                 + "LEFT JOIN MY_WH G ON G.WH=E.WH_SJ "
                 + "WHERE 1=1 ";
            Query _query = new Query();
            DataSet _ds = _query.FillDataSet(_sql.ToString() + _where, null, new string[] { "BARQC_RPT" }, false);
            return _ds;
        }
        public DataSet GetBoxBarRpt(string _where)
        {
            string _sql = "SET ROWCOUNT 10000;SELECT A.BOX_NO,A.QTY,A.CONTENT,A.WH,B.NAME AS WH_NAME,A.PRD_NO,C.SPC,D.NAME AS IDX_NAME,"
                        + "E.BAT_NO,E.IDX_NAME AS PRT_NAME,E.FORMAT,G.PRN_DD,E.USR_PRT,I.NAME AS USR_PRT_NAME,A.STOP_ID "
                        + "FROM BAR_BOX A WITH (NOLOCK) "
                        + "LEFT JOIN MY_WH B ON B.WH=A.WH "
                        + "LEFT JOIN PRDT C ON C.PRD_NO=A.PRD_NO "
                        + "LEFT JOIN INDX D ON D.IDX_NO=C.IDX1 "
                        + "LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT E ON E.BOX_NO=A.BOX_NO "
                        + "LEFT JOIN BAR_PRINT G ON G.BAR_NO=A.BOX_NO "
                        + "LEFT JOIN MY_WH E1 ON E1.WH=E.WH_SJ "
                        + "LEFT JOIN SUNSYSTEM..PSWD I ON I.COMPNO='" + Comp.CompNo + "' AND I.USR=E.USR_PRT "
                        + "WHERE 1=1 ";
            Query _query = new Query();
            DataSet _ds = _query.FillDataSet(_sql + _where, null, new string[] { "BOXBAR_RPT" }, false);
            return _ds;
        }
        #endregion

        #region ��Ʒ��ʷ��ѯ
        /// <summary>
        /// ��Ʒ��ʷ��ѯ
        /// </summary>
        /// <param name="_where"></param>
        /// <returns></returns>
        public DataSet GetPrdtHisList(string _where)
        {
            StringBuilder _sql = new StringBuilder();
            _sql.Append("SET ROWCOUNT 30000;SELECT DISTINCT BR.BAR_NO,BR.PRD_NO,CONVERT(VARCHAR(500),B.SPC) SPC,C.NAME AS IDX_NAME,")
                .Append("REPLACE(substring(A.BAR_NO,13,10),'#','') BAT_NO,D.NAME AS USR_NAME,A.SYS_DATE,E.NAME AS USR_QC_NAME,A.QC_DATE, ")
                .Append("F.NAME AS USR_BOX_NAME,A.BOX_DATE,G.NAME AS USR_UNBOX_NAME,A.UNBOX_DATE,H.NAME AS USR_SJ_NAME,A.SJ_DATE,I.NAME AS USR_UNSJ_NAME,A.UNSJ_DATE, ")
                .Append("J.NAME AS USR_WHBOX_NAME,A.WHBOX_DATE,K.NAME AS USR_WHUNBOX_NAME,A.WHUNBOX_DATE,MMB.MM_NO,MM1.NAME AS MM_NAME,MM.SYS_DATE as MM_DD,")
                .Append("SA3.PS_NO AS SA_NO,SA1.NAME AS SA_NAME,SA.CLS_DATE AS SA_DD,SB3.PS_NO AS SB_NO,SB1.NAME AS SB_NAME,SB.SYS_DATE AS SB_DD,")
                .Append("MLB.ML_NO,ML1.NAME AS ML_NAME,ML.SYS_DATE as ML_DD,M2B.ML_NO AS M2_NO,M21.NAME AS M2_NAME,M2.SYS_DATE as M2_DD,")
                .Append("M3B.ML_NO AS M3_NO,M31.NAME AS M3_NAME,M3.SYS_DATE as M3_DD,XBB.IJ_NO AS XB_NO,XB1.NAME AS XB_NAME,XB.SYS_DATE as XB_DD,")
                .Append("M4B.ML_NO AS M4_NO,M41.NAME AS M4_NAME,M4.SYS_DATE as M4_DD,M42.NAME AS M4_CUS_NAME ")
                .Append("FROM BAR_REC BR WITH (NOLOCK) ")
                .Append("LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A ON BR.BAR_NO=A.BAR_NO ")
                .Append("LEFT JOIN PRDT B ON B.PRD_NO=BR.PRD_NO ")
                .Append("LEFT JOIN INDX C ON C.IDX_NO=B.IDX1 ")
                .Append("LEFT JOIN SUNSYSTEM..PSWD D ON D.COMPNO='" + Comp.CompNo + "' AND A.USR=D.USR ")//��ӡ��Ա
                .Append("LEFT JOIN SUNSYSTEM..PSWD E ON E.COMPNO='" + Comp.CompNo + "' AND A.USR_QC=E.USR ")//������Ա
                .Append("LEFT JOIN SUNSYSTEM..PSWD F ON F.COMPNO='" + Comp.CompNo + "' AND A.USR_BOX=F.USR ")//װ����Ա
                .Append("LEFT JOIN SUNSYSTEM..PSWD G ON G.COMPNO='" + Comp.CompNo + "' AND A.USR_UNBOX=G.USR ")//������Ա
                .Append("LEFT JOIN SUNSYSTEM..PSWD H ON H.COMPNO='" + Comp.CompNo + "' AND A.USR_SJ=H.USR ")//�ͽ���Ա
                .Append("LEFT JOIN SUNSYSTEM..PSWD I ON I.COMPNO='" + Comp.CompNo + "' AND A.USR_UNSJ=I.USR ")//�ͽɳ�����Ա
                .Append("LEFT JOIN SUNSYSTEM..PSWD J ON J.COMPNO='" + Comp.CompNo + "' AND A.USR_WHBOX=J.USR ")//�ֿ�װ����Ա
                .Append("LEFT JOIN SUNSYSTEM..PSWD K ON K.COMPNO='" + Comp.CompNo + "' AND A.USR_WHUNBOX=K.USR ")//�ֿ������Ա

                .Append("LEFT JOIN TF_MM0_B MMB ON MMB.BAR_CODE=BR.BAR_NO ")//�ɿ�
                .Append("LEFT JOIN MF_MM0 MM ON MMB.MM_ID=MM.MM_ID and MM.MM_NO=MMB.MM_NO ")//�ɿ�����
                .Append("LEFT JOIN SUNSYSTEM..PSWD MM1 ON MM1.COMPNO='" + Comp.CompNo + "' AND MM.USR=MM1.USR ")//�ɿ���Ա

                .Append("LEFT JOIN TF_PSS3 SA3 ON SA3.BAR_CODE=BR.BAR_NO AND SA3.PS_ID='SA' ")//����
                .Append("LEFT JOIN MF_PSS SA ON SA.PS_ID=SA3.PS_ID AND SA.PS_NO=SA3.PS_NO ")
                .Append("LEFT JOIN SUNSYSTEM..PSWD SA1 ON SA1.COMPNO='" + Comp.CompNo + "' AND SA.USR=SA1.USR ")
                .Append("LEFT JOIN SUNSYSTEM..PSWD SA2 ON SA2.COMPNO='" + Comp.CompNo + "' AND SA.CHK_MAN=SA2.USR ")

                .Append("LEFT JOIN TF_PSS3 SB3 ON SB3.BAR_CODE=BR.BAR_NO AND SB3.PS_ID='SB' ")//�˻�
                .Append("LEFT JOIN MF_PSS SB ON SB.PS_ID=SB3.PS_ID AND SB.PS_NO=SB3.PS_NO ")
                .Append("LEFT JOIN SUNSYSTEM..PSWD SB1 ON SB1.COMPNO='" + Comp.CompNo + "' AND SB.USR=SB1.USR ")

                .Append("LEFT JOIN TF_ML_B MLB ON MLB.BAR_CODE=BR.BAR_NO AND MLB.MLID='ML' ")//����
                .Append("LEFT JOIN MF_ML ML ON ML.MLID=MLB.MLID AND ML.ML_NO=MLB.ML_NO ")
                .Append("LEFT JOIN SUNSYSTEM..PSWD ML1 ON ML1.COMPNO='" + Comp.CompNo + "' AND ML.USR=ML1.USR ")

                .Append("LEFT JOIN TF_ML_B M2B ON M2B.BAR_CODE=BR.BAR_NO AND M2B.MLID='M2' ")//����
                .Append("LEFT JOIN MF_ML M2 ON M2.MLID=M2B.MLID AND M2.ML_NO=M2B.ML_NO ")
                .Append("LEFT JOIN SUNSYSTEM..PSWD M21 ON M21.COMPNO='" + Comp.CompNo + "' AND M2.USR=M21.USR ")

                .Append("LEFT JOIN TF_ML_B M3B ON M3B.BAR_CODE=BR.BAR_NO AND M3B.MLID='M3' ")//����
                .Append("LEFT JOIN MF_ML M3 ON M3.MLID=M3B.MLID AND M3.ML_NO=M3B.ML_NO ")
                .Append("LEFT JOIN SUNSYSTEM..PSWD M31 ON M31.COMPNO='" + Comp.CompNo + "' AND M3.USR=M31.USR ")

                .Append("LEFT JOIN TF_IJ1 XBB ON XBB.BAR_CODE=BR.BAR_NO AND XBB.IJ_ID='XB' ")//����������
                .Append("LEFT JOIN MF_IJ XB ON XB.IJ_ID=XBB.IJ_ID AND XB.IJ_NO=XBB.IJ_NO ")
                .Append("LEFT JOIN SUNSYSTEM..PSWD XB1 ON XB1.COMPNO='" + Comp.CompNo + "' AND XB.USR=XB1.USR ")

                .Append("LEFT JOIN TF_ML_B M4B ON M4B.BAR_CODE=BR.BAR_NO AND M4B.MLID='M4' ")//�й�����
                .Append("LEFT JOIN MF_ML M4 ON M4.MLID=M4B.MLID AND M4.ML_NO=M4B.ML_NO ")
                .Append("LEFT JOIN SUNSYSTEM..PSWD M41 ON M41.COMPNO='" + Comp.CompNo + "' AND M4.USR=M41.USR ")

                .Append("LEFT JOIN CUST M42 ON M42.CUS_NO=M4.CUS_NO ")
                .Append("WHERE 1=1 ");
            Query _query = new Query();
            DataSet _ds = _query.FillDataSet(_sql.ToString() + _where, null, new string[] { "BAR_QC" }, false);
            return _ds;
        }
        #endregion

        #region ��Ʒ����ͳ�Ʊ�
        /// <summary>
        /// ��Ʒ����ͳ�Ʊ�
        /// </summary>
        /// <param name="_where"></param>
        /// <param name="_whereb"></param>
        /// <returns></returns>
        public DataSet GetPrdtStat(string _where, string _whereb)
        {
                          //����֧��
            //string _sql = " SELECT A.*,B.QTY AS QTY_OK,C.QTY AS QTY_NG FROM (SELECT SUBSTRING(A.BAR_NO,1,2) AS PRD_NO,SUBSTRING(A.BAR_NO,16,1) AS DEP,COUNT(A.BAR_NO) AS QTY FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A LEFT JOIN BAR_PRINT B ON A.BAR_NO=B.BAR_NO WHERE CHARINDEX('*',A.BAR_NO)<1 "
            //            + _where + "GROUP BY SUBSTRING(A.BAR_NO,1,2),SUBSTRING(A.BAR_NO,16,1)) A"
            //            //ok֧��
            //            + " LEFT JOIN (SELECT SUBSTRING(A.BAR_NO,1,2) AS PRD_NO,SUBSTRING(A.BAR_NO,16,1) AS DEP,COUNT(A.BAR_NO) AS QTY FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A  LEFT JOIN BAR_PRINT B ON A.BAR_NO=B.BAR_NO "
            //            + "WHERE A.QC='1' OR A.QC='A' OR A.QC='B' OR (A.QC='2' AND A.PRC_ID='1') AND CHARINDEX('*',A.BAR_NO)<1 "
            //            + _where + "GROUP BY SUBSTRING(A.BAR_NO,1,2),SUBSTRING(A.BAR_NO,16,1)) B ON A.PRD_NO=B.PRD_NO AND A.DEP=B.DEP"
            //            //ng֧��
            //            + " LEFT JOIN (SELECT SUBSTRING(A.BAR_NO,1,2) AS PRD_NO,SUBSTRING(A.BAR_NO,16,1) AS DEP,COUNT(A.BAR_NO) AS QTY FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A LEFT JOIN BAR_PRINT B ON A.BAR_NO=B.BAR_NO "
            //            + "WHERE A.QC='2' AND ISNULL(A.PRC_ID,'')<>'1' AND CHARINDEX('*',A.BAR_NO)<1 "
            //            + _where + "GROUP BY SUBSTRING(A.BAR_NO,1,2),SUBSTRING(A.BAR_NO,16,1)) C ON C.PRD_NO=A.PRD_NO AND C.DEP=A.DEP;"

            StringBuilder _sql = new StringBuilder();
                //��������λ����
            _sql.Append("SELECT SUBSTRING(A.BAR_NO,1,2) AS PRD_NO,SUBSTRING(A.BAR_NO,16,1) AS DEP,ISNULL(A.SPC_NO,'') AS SPC_NO,ISNULL(SPC.NAME,'') AS SPC_NAME,REPLACE(SUBSTRING(A.BAR_NO,13,10),'#','') AS BAT_NO,ISNULL(CONVERT(VARCHAR(100),PRDT.SPC),'') SPC, \n")
                .Append("COUNT(A.BAR_NO) AS QTY_SPC,C.QTY,C.QTY1,C.QTY_OK,C.QTY1_OK,C.QTY_B,C.QTY1_B,C.QTY_NG,C.QTY1_NG \n")
                .Append("FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A WITH (NOLOCK) \n")
                .Append("LEFT JOIN SPC_LST SPC ON SPC.SPC_NO=A.SPC_NO \n")
                .Append("LEFT JOIN PRDT ON PRDT.PRD_NO=SUBSTRING(A.BAR_NO,1,12) \n")
                //����֧��
                .Append("LEFT JOIN ( \n")
                .Append("   SELECT A.PRD_NO,A.DEP,A.BAT_NO,A.SPC,A.QTY,A.QTY1,B.QTY AS QTY_OK,B.QTY1 AS QTY1_OK,C.QTY AS QTY_B,C.QTY1 AS QTY1_B,D.QTY AS QTY_NG,D.QTY1 AS QTY1_NG \n")
                .Append("   FROM ( \n")
                .Append("       SELECT SUBSTRING(A.BAR_NO,1,2) AS PRD_NO,SUBSTRING(A.BAR_NO,16,1) AS DEP,REPLACE(SUBSTRING(A.BAR_NO,13,10),'#','') AS BAT_NO,CONVERT(VARCHAR(100),PRDT.SPC) SPC,COUNT(A.BAR_NO) AS QTY,SUM(PRDT_Z.QTY_PER_GP) AS QTY1 \n")
                .Append("       FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A \n")
                .Append("       LEFT JOIN PRDT ON PRDT.PRD_NO=SUBSTRING(A.BAR_NO,1,12) \n")
                .Append("       LEFT JOIN PRDT_Z ON PRDT_Z.PRD_NO=PRDT.PRD_NO \n")
                .Append("       WHERE CHARINDEX('*',A.BAR_NO)<1 \n")
                .Append(_where).Append(" \n GROUP BY SUBSTRING(A.BAR_NO,1,2),SUBSTRING(A.BAR_NO,16,1),CONVERT(VARCHAR(100),PRDT.SPC),REPLACE(SUBSTRING(A.BAR_NO,13,10),'#',''),PRDT_Z.QTY_PER_GP \n")
                .Append("       ) A \n")
                //ok֧��
                .Append("   LEFT JOIN ( \n")
                .Append("       SELECT SUBSTRING(A.BAR_NO,1,2) AS PRD_NO,SUBSTRING(A.BAR_NO,16,1) AS DEP,REPLACE(SUBSTRING(A.BAR_NO,13,10),'#','') AS BAT_NO,CONVERT(VARCHAR(100),PRDT.SPC) SPC,COUNT(A.BAR_NO) AS QTY,SUM(PRDT_Z.QTY_PER_GP) AS QTY1 \n")
                .Append("       FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A \n")
                .Append("       LEFT JOIN PRDT ON PRDT.PRD_NO=SUBSTRING(A.BAR_NO,1,12) \n")
                .Append("       LEFT JOIN PRDT_Z ON PRDT_Z.PRD_NO=PRDT.PRD_NO \n")
                .Append("       WHERE (A.QC='1' OR A.QC='A') AND CHARINDEX('*',A.BAR_NO)<1 \n")
                .Append(_where).Append(" \n GROUP BY SUBSTRING(A.BAR_NO,1,2),SUBSTRING(A.BAR_NO,16,1),CONVERT(VARCHAR(100),PRDT.SPC),REPLACE(SUBSTRING(A.BAR_NO,13,10),'#',''),PRDT_Z.QTY_PER_GP \n")
                .Append("       ) B ON A.PRD_NO=B.PRD_NO AND A.DEP=B.DEP  AND A.SPC=B.SPC AND A.BAT_NO=B.BAT_NO \n")
                //B��Ʒ֧��
                .Append("   LEFT JOIN ( \n")
                .Append("       SELECT SUBSTRING(A.BAR_NO,1,2) AS PRD_NO,SUBSTRING(A.BAR_NO,16,1) AS DEP,REPLACE(SUBSTRING(A.BAR_NO,13,10),'#','') AS BAT_NO,CONVERT(VARCHAR(100),PRDT.SPC) SPC,COUNT(A.BAR_NO) AS QTY,SUM(PRDT_Z.QTY_PER_GP) AS QTY1 \n")
                .Append("       FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A \n")
                .Append("       LEFT JOIN PRDT ON PRDT.PRD_NO=SUBSTRING(A.BAR_NO,1,12) \n")
                .Append("       LEFT JOIN PRDT_Z ON PRDT_Z.PRD_NO=PRDT.PRD_NO \n")
                .Append("       WHERE A.QC='B' AND CHARINDEX('*',A.BAR_NO)<1 \n")
                .Append(_where).Append(" \n GROUP BY SUBSTRING(A.BAR_NO,1,2),SUBSTRING(A.BAR_NO,16,1),CONVERT(VARCHAR(100),PRDT.SPC),REPLACE(SUBSTRING(A.BAR_NO,13,10),'#','') \n")
                .Append("       ) C ON C.PRD_NO=A.PRD_NO AND C.DEP=A.DEP AND A.SPC=C.SPC AND A.BAT_NO=C.BAT_NO \n")
                //ng֧��
                .Append("   LEFT JOIN ( \n")
                .Append("       SELECT SUBSTRING(A.BAR_NO,1,2) AS PRD_NO,SUBSTRING(A.BAR_NO,16,1) AS DEP,REPLACE(SUBSTRING(A.BAR_NO,13,10),'#','') AS BAT_NO,CONVERT(VARCHAR(100),PRDT.SPC) SPC,COUNT(A.BAR_NO) AS QTY,SUM(PRDT_Z.QTY_PER_GP) AS QTY1 \n")
                .Append("       FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A \n")
                .Append("       LEFT JOIN PRDT ON PRDT.PRD_NO=SUBSTRING(A.BAR_NO,1,12) \n")
                .Append("       LEFT JOIN PRDT_Z ON PRDT_Z.PRD_NO=PRDT.PRD_NO \n")
                .Append("       WHERE A.QC='2' AND CHARINDEX('*',A.BAR_NO)<1 \n")
                .Append(_where).Append(" \n GROUP BY SUBSTRING(A.BAR_NO,1,2),SUBSTRING(A.BAR_NO,16,1),CONVERT(VARCHAR(100),PRDT.SPC),REPLACE(SUBSTRING(A.BAR_NO,13,10),'#','') \n")
                .Append("       ) D ON D.PRD_NO=A.PRD_NO AND D.DEP=A.DEP AND A.SPC=D.SPC AND A.BAT_NO=D.BAT_NO \n")

                .Append(" ) C ON C.PRD_NO=SUBSTRING(A.BAR_NO,1,2) AND C.DEP=SUBSTRING(A.BAR_NO,16,1) AND C.SPC=CONVERT(VARCHAR(100),PRDT.SPC) AND C.BAT_NO=REPLACE(SUBSTRING(A.BAR_NO,13,10),'#','') \n")
                .Append(" WHERE CHARINDEX('*',A.BAR_NO)<1 \n")
                .Append(_where)
                .Append(" \n GROUP BY SUBSTRING(A.BAR_NO,1,2),SUBSTRING(A.BAR_NO,16,1),ISNULL(A.SPC_NO,''),ISNULL(SPC.NAME,''),REPLACE(SUBSTRING(A.BAR_NO,13,10),'#',''),ISNULL(CONVERT(VARCHAR(100),PRDT.SPC),''),C.QTY,C.QTY1,C.QTY_OK,C.QTY1_OK,C.QTY_B,C.QTY1_B,C.QTY_NG,C.QTY1_NG \n")
                .Append(" ORDER BY SUBSTRING(A.BAR_NO,1,2),SUBSTRING(A.BAR_NO,16,1),REPLACE(SUBSTRING(A.BAR_NO,13,10),'#',''),ISNULL(CONVERT(VARCHAR(100),PRDT.SPC),''); \n")
                //�쳣ԭ���
                .Append("SELECT SPC_NO,NAME FROM SPC_LST;");
            Query _query = new Query();
            //DataSet _ds = _query.FillDataSet(_sql, null, new string[] { "BAR_ALL","BAR_SPC","SPC_LST"}, false);
            DataSet _ds = _query.FillDataSet(_sql.ToString(), null, new string[] { "BAR_SPC", "SPC_LST" }, false);
            return _ds;
        }
        #endregion

        #region ���������¼
        /// <summary>
        /// ���������¼(BAR_REC,BAT_NO)
        /// </summary>
        /// <param name="ChangedDS"></param>
        /// <returns></returns>
        public DataTable UpdataData(DataSet ChangedDS)
		{
			System.Collections.Hashtable _ht = new System.Collections.Hashtable();
			_ht["BAR_REC"] = "BAR_NO,BOX_NO,WH,UPDDATE,STOP_ID,PRD_NO,PRD_MARK,SPC_NO,PH_FLAG";
			_ht["BAT_NO"] = "BAT_NO,NAME";
			this.UpdateDataSet(ChangedDS, _ht);
			DataTable _dt = GetAllErrors(ChangedDS);
			return _dt;
        }
        #endregion

        #region ��������Ʒ����Ϣ
        public DataTable UpdataDataQC(DataSet ChangedDS)
        {
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["BAR_QC"] = "BAR_NO,OS_BAR_NO,QC,STATUS,SPC_NO,BAT_NO,BIL_ID,BIL_NO,WH_SJ,REM,PRC_ID,PRD_TYPE,USR,SYS_DATE,FORMAT,LH,PRT_NAME,USR_QC,QC_DATE,USR_BOX,BOX_DATE,USR_UNBOX,UNBOX_DATE,USR_SJ,SJ_DATE,USR_UNSJ,UNSJ_DATE,USR_WHBOX,WHBOX_DATE,USR_WHUNBOX,WHUNBOX_DATE,TO_CUST";
            this.UpdateDataSet(Comp.Conn_SunSystem.Replace("sunsystem", Comp.DRP_Prop["BarPrintDB"].ToString()), ChangedDS, _ht);
            DataTable _dt = GetAllErrors(ChangedDS);
            return _dt;
        }
        #endregion

        #region ����������
        public DataTable UpdataDataBoxBat(DataSet ChangedDS)
        {
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["BAR_BOX_BAT"] = "BOX_NO,PRD_NO,PRD_MARK,BAT_NO,LH,IDX_NAME,FORMAT,QC,STATUS,SPC_NO,BIL_ID,BIL_NO,WH_SJ,USR_PRT,REM";
            this.UpdateDataSet(Comp.Conn_SunSystem.Replace("sunsystem", Comp.DRP_Prop["BarPrintDB"].ToString()), ChangedDS, _ht);
            DataTable _dt = GetAllErrors(ChangedDS);
            return _dt;
        }

        public DataSet GetDataBoxBat(string _boxNo)
        {
            Query _query = new Query();
            DataSet _ds = new DataSet();
            string _sql = "SELECT BOX_NO,PRD_NO,PRD_MARK,BAT_NO,LH,IDX_NAME,FORMAT,QC,STATUS,SPC_NO,BIL_ID,BIL_NO,WH_SJ,USR_PRT,REM FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT WHERE BOX_NO=@BOX_NO";
            SqlParameter[] _para = new SqlParameter[1];
            _para[0] = new SqlParameter("@BOX_NO", SqlDbType.VarChar);
            _para[0].Value = _boxNo;
            _ds = _query.FillDataSet(_sql, _para, new string[] { "BAR_BOX_BAT" }, false);
            return _ds;
        }
        public DataSet GetDataBox(string _sqlWhere)
        {
            Query _query = new Query();
            DataSet _ds = new DataSet();
            string _sql = "SELECT BOX_NO,PRD_NO,PRD_MARK,BAT_NO,IDX_NAME,FORMAT,QC,STATUS,SPC_NO,BIL_ID,BIL_NO,WH_SJ,USR_PRT,REM FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT WHERE 1=1 ";
            _ds = _query.FillDataSet(_sql + _sqlWhere, null, new string[] { "BAR_BOX_BAT"}, false);
            return _ds;
        }
        #endregion

        #region ����������
        public DataTable UpdataDataBox(DataSet ChangedDS)
        {
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["BAR_BOX"] = "BOX_NO,BOX_DD,DEP,QTY,CONTENT,USR,WH,PRD_NO,PH_FLAG,STOP_ID,VALID_DD";
            this.UpdateDataSet(ChangedDS, _ht);
            DataTable _dt = GetAllErrors(ChangedDS);
            return _dt;
        }
        #endregion

        #region ���²��ϸ�ԭ��
        public DataTable UpdataDataSPC(DataSet ChangedDS)
        {
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["SPC_LST"] = "SPC_NO,NAME";
            this.UpdateDataSet(ChangedDS, _ht);
            DataTable _dt = GetAllErrors(ChangedDS);
            return _dt;
        }
        #endregion

        #region ȡ����Ʒ����Ϣ
        public DataSet GetDataQC(bool _getSchema)
        {
            Query _query = new Query();
            DataSet _ds = new DataSet();
            string _sql = "SELECT BAR_NO,OS_BAR_NO,QC,STATUS,SPC_NO,BAT_NO,BIL_ID,BIL_NO,WH_SJ,REM,PRC_ID,PRD_TYPE,USR,SYS_DATE,FORMAT,LH,PRT_NAME,USR_QC,QC_DATE,USR_BOX,BOX_DATE,USR_UNBOX,UNBOX_DATE,USR_SJ,SJ_DATE,USR_UNSJ,UNSJ_DATE,USR_WHBOX,WHBOX_DATE,USR_WHUNBOX,WHUNBOX_DATE,TO_CUST FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC";
            _ds = _query.FillDataSet(_sql, null, new string[] { "BAR_QC" }, _getSchema);
            return _ds;
        }
        public DataSet GetDataQC1(string _sqlWhere)
        {
            Query _query = new Query();
            DataSet _ds = new DataSet();
            string _sql = "SELECT BAR_NO,OS_BAR_NO,QC,STATUS,SPC_NO,BAT_NO,BIL_ID,BIL_NO,WH_SJ,REM,PRC_ID,PRD_TYPE,USR,SYS_DATE,FORMAT,LH,PRT_NAME,USR_QC,QC_DATE,USR_BOX,BOX_DATE,USR_UNBOX,UNBOX_DATE,USR_SJ,SJ_DATE,USR_UNSJ,UNSJ_DATE,USR_WHBOX,WHBOX_DATE,USR_WHUNBOX,WHUNBOX_DATE,TO_CUST FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC WHERE 1=1 ";
            _ds = _query.FillDataSet(_sql + _sqlWhere, null, new string[] { "BAR_QC" }, false);
            return _ds;
        }
        #endregion

        #region ȡ���Ѵ�ӡ����
        private DataSet GetPrtSqData(string _prdNo, string _prdMark, string _barNo)
        {
            string _sql = "SELECT PRD_NO,PRD_MARK,MAX_NO FROM BAR_SQNO WHERE PRD_NO='" + _prdNo + "' AND PRD_MARK='" + _prdMark + "'; "
                        + "SELECT BAR_NO,SPC_NO,PRN_DD,DEP FROM BAR_PRINT WHERE BAR_NO='" + _barNo + "'";
            Query _query = new Query();
            DataSet _ds = (DataSet)_query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "BAR_SQNO";
            _ds.Tables[1].TableName = "BAR_PRINT";
            return _ds;
        }
        #endregion

        #region �����Ѵ�ӡ������Ϣ
        public DataTable UpdataPrtSqData(DataSet ChangedDS)
        {
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["BAR_PRINT"] = "BAR_NO,SPC_NO,PRN_DD,DEP";
            _ht["BAR_SQNO"] = "PRD_NO,PRD_MARK,MAX_NO";
            this.UpdateDataSet(ChangedDS, _ht);
            DataTable _dt = GetAllErrors(ChangedDS);
            return _dt;
        }
        #endregion

        #region ת�ɿⵥ
        /// <summary>
        /// ת�ɿⵥ
        /// </summary>
        /// <param name="dataSet"></param>
        public void TOMM(DataSet dataSet)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                //base.EnterTransaction();
                try
                {
                    DataTable _dtErr = new DataTable(); 
                    string _barNo = "";//��¼��Ʒ������Ϣ��A','B','C','D','E   ...�ȣ�������������
                    string _boxNo = "";//��¼��������Ϣ��A','B','C','D','E   ...�ȣ�������������
                    if (dataSet.Tables["TOMM_BAR"].Columns.Contains("BOX_NO"))
                    {
                        foreach (DataRow _dr in dataSet.Tables["TOMM_BAR"].Rows)
                        {
                            if (_barNo != "")
                                _barNo += "','";
                            _barNo += _dr["BAR_CODE"].ToString();
                            if (_boxNo != "")
                                _boxNo += ",";
                            //if (_dr["BOX_NO"].ToString() != "")
                            _boxNo += "'" + _dr["BOX_NO"].ToString() + "'";
                        }
                    }
                    else
                    {
                        foreach (DataRow _dr in dataSet.Tables["TOMM_BAR"].Rows)
                        {
                            if (_barNo != "")
                                _barNo += "','";
                            _barNo += _dr["BAR_CODE"].ToString();
                        }
                    }

                    if (_barNo != "")
                    {
                        if (_boxNo != "")
                        {
                            //����BAR_BOX������Ŀ�λ
                            DataSet _dsBarBox = GetBoxBar("", "T", " AND ISNULL(A.STOP_ID,'F')<>'T' AND A.BOX_NO IN (" + _boxNo + ")");
                            foreach (DataRow _dr in _dsBarBox.Tables[0].Rows)
                            {
                                _dr["WH"] = dataSet.Tables["TF_TY_EXPORT"].Rows[0]["WH"];
                            }
                            _dtErr = UpdataDataBox(_dsBarBox);//д��BAR_BOX������Ŀ�λ��˵�����������
                            if (_dtErr.Rows.Count > 0)
                            {
                                //base.SetAbort();
                                throw new SunlikeException(GetErrorString(_dtErr));
                            }
                        }

                        //����BAR_REC�Ŀ�λ
                        DataSet _ds = GetBarRecord1(_barNo);
                        foreach (DataRow _dr in _ds.Tables[0].Rows)
                        {
                            _dr["WH"] = dataSet.Tables["TF_TY_EXPORT"].Rows[0]["WH"];
                            _dr["STOP_ID"] = "F";
                            _dr["UPDDATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                        }
                        _dtErr = UpdataData(_ds);//д��BAR_REC�����¼�Ŀ�λ��˵�������������
                        if (_dtErr.Rows.Count > 0)
                        {
                            //base.SetAbort();
                            throw new SunlikeException(GetErrorString(_dtErr));
                        }
                        else
                        {
                            //ת���깤�ɿ�
                            MRPTY _ty = new MRPTY();
                            _ty.UpdatePassData(SunlikeDataSet.ConvertTo(dataSet), false);
                        }
                    }
                    //base.SetComplete();
                }
                catch (Exception _ex)
                {
                    //base.SetAbort();
                    throw new SunlikeException(this.GetErrorResource(_ex.Message));
                }
                finally
                {
                    //base.LeaveTransaction();
                }
                scope.Complete();//�����ύ
            }
        }
        /// <summary>
        /// ȡ�ѽɿ�����루�ɿ⳷����ҵʱ���ã�
        /// </summary>
        /// <param name="_prdType">��Ʒ���1����ģ�2�����Ʒ��3����Ʒ</param>
        /// <param name="_where">����</param>
        /// <returns></returns>
        public DataSet GetUndoMMBar(string _prdType, string _where)
        {
            DataSet _ds = new DataSet();
            Query _query = new Query();
            if (_prdType == "1")
            {
                string _sql = "SELECT A.BAR_NO,REPLACE(A.BAR_NO,'#','') AS BAR_NO_DIS FROM BAR_REC A "
                            + " INNER JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC B ON A.BAR_NO=B.BAR_NO"
                            + " WHERE ISNULL(B.STATUS,'')='MM' AND LEN(REPLACE(A.BAR_NO,'#',''))=22 AND ISNULL(A.STOP_ID,'F')='T' AND ISNULL(A.WH,'')='' ";//����ɾ���ɿⵥ��BAR_REC�е�STOP_ID���Ϊ��T��
                if (_where.Length > 0)
                    _sql += _where;
                _ds = _query.DoSQLString(_sql);
                _ds.Tables[0].TableName = "BAR_UNDOMM";
            }
            else if (_prdType == "2")
            {
                string _sql = "SELECT A.BAR_NO,REPLACE(A.BAR_NO,'#','') AS BAR_NO_DIS FROM BAR_REC A "
                            + " INNER JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC B ON A.BAR_NO=B.BAR_NO"
                            + " WHERE ISNULL(B.STATUS,'')='MM' AND LEN(REPLACE(A.BAR_NO,'#',''))=24 AND ISNULL(A.STOP_ID,'F')='T' AND ISNULL(A.WH,'')='' ";//����ɾ���ɿⵥ��BAR_REC�е�STOP_ID���Ϊ��T��
                if (_where.Length > 0)
                    _sql += _where;
                _ds = _query.DoSQLString(_sql);
                _ds.Tables[0].TableName = "BAR_UNDOMM";
            }
            else if (_prdType == "3")
            {
                string _sql = "SELECT A.BOX_NO AS BAR_NO,A.BOX_NO AS BAR_NO_DIS FROM BAR_BOX A "
                            + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT B ON B.BOX_NO=A.BOX_NO"
                            + " WHERE ISNULL(B.STATUS, '')='MM' AND ISNULL(A.CONTENT,'')<>'' AND ISNULL(A.WH,'')='' AND ISNULL(B.PRD_MARK,'')<>'T' ";
                if (_where != "")
                    _sql += _where;
                _ds = _query.DoSQLString(_sql);
                _ds.Tables[0].TableName = "BAR_UNDOMM"; 
            }
            return _ds;
        }

        /// <summary>
        /// �ɿ⳷��
        /// �޸����к�BAR_QC������STATUS�ֶ�ֵ
        /// �ɿ�ʱ����BAR_QC���е�STATUS�ֶ�ֵ��ΪMM��˵���������ѽɿ�
        /// �����ɿ�ʱ����BAR_QC���е�STATUS�ֶ�ֵ��ΪSW��˵�����������ͽɣ����ڴ��ɿ�״̬
        /// </summary>
        /// <param name="_barNoLst">���к�</param>
        /// <param name="_boxNoList">������</param>
        public string UndoMM(string _barNoLst, string _boxNoList)
        {
            //�����ɿ�ʱ����BAR_QC���е�STATUS�ֶ�ֵ��ΪSW��˵�����������ͽɣ����ڴ��ɿ�״̬
            //�����ɿ�ʱ�����кű�����������3����ܳ����ɿ⣺
            //  1����BAR_QC����STATUSΪMM
            //  2���ڽɿⵥ���кű�TF_MM0_B�в����ڣ�������ϵͳ�г����ɿ�֮ǰ�ȵ�sunlike��ɾ���ɿⵥ��
            //  3����BAR_REC���п�λWHҪΪ��
            string _err = "";
            try
            {
                string _sql = "begin tran \n"
                            + " UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC SET STATUS='SW' WHERE BAR_NO in (" + _barNoLst + ") \n"
                            + " UPDATE BAR_REC SET STOP_ID='',UPDDATE='" + DateTime.Now.ToString(Comp.SQLDateTimeFormat) + "' WHERE BAR_NO in (" + _barNoLst + ") \n";
                if (_boxNoList != "")
                {
                    _sql += " UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT SET STATUS='SW' WHERE BOX_NO in (" + _boxNoList + ") \n"
                          + " UPDATE BAR_BOX SET STOP_ID='' WHERE BOX_NO in (" + _boxNoList + ") \n"
                          + " UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC SET STATUS='SW' WHERE BAR_NO in (SELECT BAR_NO FROM BAR_REC WHERE BOX_NO IN (" + _boxNoList + ")) \n"
                          + " UPDATE BAR_REC SET STOP_ID='',UPDDATE='" + DateTime.Now.ToString(Comp.SQLDateTimeFormat) + "' WHERE BOX_NO in (" + _boxNoList + ") \n";
                }
                _sql += " if @@error<>0 \n"
                     + "     rollback tran \n"
                     + " else \n"
                     + "     commit tran ";
                Query _query = new Query();
                _query.RunSql(_sql);
            }
            catch (Exception _ex)
            {
                if (_ex.Message.Length > 100)
                    _err = _ex.Message.Substring(0, 100);
                else
                    _err = _ex.Message;
            }
            return _err;
        }
        #endregion

        #region ת�쳣֪ͨ��
        /// <summary>
        /// ת�쳣֪ͨ��
        /// </summary>
        /// <param name="dataSet"></param>
        public void TOTY(DataSet dataSet)
        {
            try
            {
                MRPTY _ty = new MRPTY();
                _ty.UpdateFaultData(SunlikeDataSet.ConvertTo(dataSet));
            }
            catch (Exception _ex)
            {
                throw new SunlikeException(this.GetErrorResource(_ex.Message));
            }
        }
        /// <summary>
        /// ȡ��ת�쳣֪ͨ�������к�
        /// </summary>
        /// <param name="_sqlWhere"></param>
        /// <returns></returns>
        public DataSet GetDataToTY(string _sqlWhere)
        {
            string _sql = "SELECT A.BAR_NO,A.SPC_NO,A.BIL_ID,A.BIL_NO,A.QC,A.STATUS,A.REM,A.PRC_ID,B.PRD_NO,B.PRD_MARK,B.BAT_NO,B.WH,B.BOX_NO,C.NAME AS PRD_NAME,C.SPC,D.NAME AS IDX_NAME,E.NAME AS SPC_NAME,"
                        + " (CASE WHEN ISNULL(A.PRC_ID,'')='2' THEN '" + ConvertToDBLanguage("����") + "' when ISNULL(A.PRC_ID,'')='3' THEN '" + ConvertToDBLanguage("�ؿ����") + "' else A.PRC_ID end) as PRC_NAME"
                        + " FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A "
                        + " INNER JOIN BAR_REC B ON A.BAR_NO=B.BAR_NO"
                        + " LEFT JOIN PRDT C ON C.PRD_NO=B.PRD_NO"
                        + " LEFT JOIN INDX D ON C.IDX1=D.IDX_NO"
                        + " LEFT JOIN SPC_LST E ON E.SPC_NO=A.SPC_NO"
                        + " WHERE ISNULL(B.STOP_ID,'F')<>'T' AND ISNULL(A.BIL_ID,'')='' ";
            if (_sqlWhere.Length > 0)
                _sql += _sqlWhere;
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "BAR_QC";
            return _ds;
        }
        /// <summary>
        /// ȡ��ת�쳣�������루ת�쳣��������ҵʱ���ã�
        /// </summary>
        /// <param name="_prdType">��Ʒ���1����ģ�2�����Ʒ��3����Ʒ</param>
        /// <param name="_where">����</param>
        /// <returns></returns>
        public DataSet GetUndoTRBar(string _prdType, string _where)
        {
            DataSet _ds = new DataSet();
            Query _query = new Query();
            if (_prdType == "1")
            {
                string _sql = "SELECT A.BAR_NO,REPLACE(A.BAR_NO,'#','') AS BAR_NO_DIS FROM BAR_REC A "
                            + " INNER JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC B ON A.BAR_NO=B.BAR_NO"
                            + " LEFT JOIN TZERR C ON B.BIL_NO=C.TR_NO"
                            + " WHERE ISNULL(B.STATUS,'')='TR' AND LEN(REPLACE(A.BAR_NO,'#',''))=22 AND C.TR_NO IS NULL ";
                if (_where.Length > 0)
                    _sql += _where;
                _ds = _query.DoSQLString(_sql);
                _ds.Tables[0].TableName = "BAR_UNDOTR";
            }
            else if (_prdType == "2")
            {
                string _sql = "SELECT A.BAR_NO,REPLACE(A.BAR_NO,'#','') AS BAR_NO_DIS FROM BAR_REC A "
                            + " INNER JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC B ON A.BAR_NO=B.BAR_NO"
                            + " LEFT JOIN TZERR C ON B.BIL_NO=C.TR_NO"
                            + " WHERE ISNULL(B.STATUS,'')='TR' AND LEN(REPLACE(A.BAR_NO,'#',''))=24 AND C.TR_NO IS NULL ";
                if (_where.Length > 0)
                    _sql += _where;
                _ds = _query.DoSQLString(_sql);
                _ds.Tables[0].TableName = "BAR_UNDOTR";
            }
            else if (_prdType == "3")
            {
                string _sql = "SELECT A.BAR_NO,REPLACE(A.BAR_NO,'#','') AS BAR_NO_DIS FROM BAR_REC A "
                            + " INNER JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC B ON A.BAR_NO=B.BAR_NO"
                            + " LEFT JOIN TZERR C ON B.BIL_NO=C.TR_NO"
                            + " WHERE ISNULL(B.STATUS,'')='TR' AND LEN(REPLACE(A.BAR_NO,'#',''))>24 AND C.TR_NO IS NULL ";
                if (_where.Length > 0)
                    _sql += _where;
                _ds = _query.DoSQLString(_sql);
                _ds.Tables[0].TableName = "BAR_UNDOTR";
            }
            return _ds;
        }
        /// <summary>
        /// ת�쳣������
        /// �޸����к�BAR_QC������STATUS�ֶ�ֵ
        /// ת�쳣��ʱ����BAR_QC���е�STATUS�ֶ�ֵ��ΪTR��˵����������ת�쳣��
        /// ����ת�쳣��ʱ����BAR_QC���е�STATUS�ֶ�ֵ��ΪQC��˵�������봦�ڴ�ת�쳣��״̬
        /// </summary>
        /// <param name="_barNoLst">���к�</param>
        public string UndoTR(string _barNoLst)
        {
            //����ת�쳣��ʱ����BAR_QC���е�STATUS�ֶ�ֵ��ΪQC��˵�������봦�ڴ�ת�쳣��״̬
            //����ת�쳣��ʱ�����кű�����������2����ܳ���ת�쳣����
            //  1����BAR_QC����STATUSΪTR
            //  2����Ӧsunlike�쳣��ɾ��
            string _err = "";
            try
            {
                string _sql = " UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC SET STATUS='QC',BIL_ID='',BIL_NO='' WHERE BAR_NO in (" + _barNoLst + ") AND ISNULL(STATUS,'')='TR' \n";
                Query _query = new Query();
                _query.RunSql(_sql);
            }
            catch (Exception _ex)
            {
                if (_ex.Message.Length > 100)
                    _err = _ex.Message.Substring(0,100);
                else
                    _err = _ex.Message;
            }
            return _err;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="_boxNo">����</param>
        /// <param name="_usr">��ǰ�����û�</param>
        /// <param name="_type">1��һ����䣻2���ֿ���䣻3���˻�����</param>
        public void UnDoBox(string _boxNo, string _usr, string _type)
        {
            string _sql = "";
            if (_type == "1")
                _sql += "UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC SET USR_UNBOX=@USR_UNBOX,UNBOX_DATE=@UNBOX_DATE from " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A inner join BAR_REC B on A.BAR_NO=B.BAR_NO WHERE B.BOX_NO=@BOX_NO;";
            if (_type == "2")
                _sql += "UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC SET USR_WHUNBOX=@USR_UNBOX,WHUNBOX_DATE=@UNBOX_DATE from " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A inner join BAR_REC B on A.BAR_NO=B.BAR_NO WHERE B.BOX_NO=@BOX_NO;";
            if (_type == "3")
                _sql += "UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC SET USR_UNBOX=@USR_UNBOX,UNBOX_DATE=@UNBOX_DATE from " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A inner join BAR_REC B on A.BAR_NO=B.BAR_NO WHERE B.BOX_NO=@BOX_NO;";
            _sql += "UPDATE BAR_BOX SET STOP_ID='T' WHERE BOX_NO=@BOX_NO;"
                 + "UPDATE BAR_REC SET BOX_NO=NULL WHERE BOX_NO=@BOX_NO;";
            Query _query = new Query();
            SqlParameter[] _para = new SqlParameter[3];
            _para[0] = new SqlParameter("@BOX_NO", SqlDbType.VarChar);
            _para[0].Value = _boxNo;
            _para[1] = new SqlParameter("@USR_UNBOX", SqlDbType.VarChar);
            _para[1].Value = _usr;
            _para[2] = new SqlParameter("@UNBOX_DATE", SqlDbType.DateTime);
            _para[2].Value = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
            _query.RunSql(_sql, _para);
        }
        #endregion

        #region װ��
        /// <summary>
        /// װ��
        /// </summary>
        /// <param name="changedDS"></param>
        /// <returns></returns>
        public DataTable BarBoxing(DataSet changedDS)
        {
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["BAR_REC"] = "BAR_NO,BOX_NO,CUS_NO,WH,UPDDATE,PH_FLAG,STOP_ID,SPC_NO,PRD_NO,PRD_MARK,BIL_NOLIST,REM";
            _ht["BAR_BOX"] = "BOX_NO,BOX_DD,DEP,QTY,CONTENT,USR,WH,PRD_NO,PH_FLAG,STOP_ID,VALID_DD";
            this.UpdateDataSet(changedDS, _ht);
            DataTable _dt = GetAllErrors(changedDS);
            return _dt;
        }
        /// <summary>
        /// ȡ��δװ������루װ��ʱ���ã�
        /// </summary>
        /// <param name="_boxNo">������</param>
        /// <param name="_barCode">���кţ�ex: ('A','B','C')</param>
        /// <returns></returns>
        public DataSet GetBarBoxRec(string _boxNo, string _barCode)
        {
            string _sql = "SELECT A.BOX_NO,SUBSTRING(A.BOX_NO,2,LEN(A.BOX_NO)-1) AS BOX_NO_DIS,A.BOX_DD,A.DEP,A.QTY,A.CONTENT,A.USR,A.WH,A.PRD_NO,A.PH_FLAG,A.STOP_ID,A.VALID_DD,"
                        + " B.BAT_NO,B.IDX_NAME,B.FORMAT,B.QC,B.STATUS,B.SPC_NO,B.BIL_ID,B.BIL_NO,B.WH_SJ,B.REM"
                        + " FROM BAR_BOX A"
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT B ON B.BOX_NO=A.BOX_NO WHERE A.BOX_NO=@BOX_NO;"
                        + " SELECT A.BAR_NO,A.BOX_NO,A.CUS_NO,A.WH,A.UPDDATE,A.PH_FLAG,A.STOP_ID,A.SPC_NO,A.PRD_NO,A.PRD_MARK,A.BIL_NOLIST,A.BAT_NO,A.REM,B.QC"
                        + " FROM BAR_REC A"
                        + " LEFT JOIN " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC B ON A.BAR_NO=B.BAR_NO"
                        + " WHERE A.BAR_NO IN " + _barCode;
            SqlParameter[] _para = new SqlParameter[1];
            _para[0] = new SqlParameter("@BOX_NO", SqlDbType.VarChar);
            _para[0].Value = _boxNo;
            Query _query = new Query();
            DataSet _ds = _query.FillDataSet(_sql, _para, new string[] { "BAR_BOX","BAR_REC" }, false);
            return _ds;
        }
        #endregion

        #region ȡ�쳣ԭ��
        public DataSet GetSpcLst(string _spcNo)
        {
            string _sql = "SELECT SPC_NO,NAME FROM SPC_LST WHERE SPC_NO=@SPC_NO";
            SqlParameter[] _para = new SqlParameter[1];
            _para[0] = new SqlParameter("@SPC_NO", SqlDbType.VarChar);
            _para[0].Value = _spcNo;
            Query _query = new Query();
            DataSet _ds = _query.FillDataSet(_sql, _para, new string[] { "SPC_LST" }, false);
            return _ds;
        }
        #endregion


        #region ȡ�ͻ����ϵ�
        public DataSet GetCust(string _cusNo)
        {
            string _sql = "SELECT CUS_NO,NAME FROM CUST WHERE CUS_NO=@CUS_NO AND ISNULL(OBJ_ID,'')<>'2'";
            SqlParameter[] _para = new SqlParameter[1];
            _para[0] = new SqlParameter("@CUS_NO", SqlDbType.VarChar);
            _para[0].Value = _cusNo;
            Query _query = new Query();
            DataSet _ds = _query.FillDataSet(_sql, _para, new string[] { "CUST" }, false);
            return _ds;
        }
        #endregion


        #region ȡ����
        public DataSet GetBatNo(string _sqlWhere)
        {
            string _sql = "SELECT LEFT(A.BAT_NO,7) AS BAT_NO_DIS,A.BAT_NO,A.NAME,B.PRD_NO,C.SPC,C.KND,C.IDX1,D.NAME AS IDX_NAME FROM BAT_NO A INNER JOIN BAR_SQNO B ON B.PRD_MARK=A.BAT_NO LEFT JOIN PRDT C ON C.PRD_NO=B.PRD_NO LEFT JOIN INDX D ON D.IDX_NO=C.IDX1 WHERE 1=1 ";
            if (_sqlWhere != "")
                _sql += _sqlWhere;
            Query _query = new Query();
            DataSet _ds = _query.FillDataSet(_sql,null, new string[] { "BAT_NO" }, false);
            return _ds;
        }
        #endregion

        #region �Ϻ�/��ӡƷ������
        public DataSet GetBarPrtName(string typeId,string idx, string _where)
        {
            string _sql = "SELECT A.TYPE_ID,A.IDX,A.IDX_NO,A.PRD_NO,A.NAME,P.NAME AS PRD_NAME,P.SPC,I.NAME AS IDX_NAME FROM "
                + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_PRTNAME A"
                + " LEFT JOIN PRDT P ON P.PRD_NO=A.PRD_NO "
                + " LEFT JOIN INDX I ON I.IDX_NO=A.IDX_NO "
                + " WHERE 1=1 " + _where;
            Query _query = new Query();
            DataSet _ds = (DataSet)_query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "BAR_PRTNAME";

            _ds.Tables[0].Columns["TYPE_ID"].DefaultValue = typeId;
            _ds.Tables[0].Columns["IDX"].DefaultValue = idx;
            _ds.Tables[0].Columns["IDX_NO"].DefaultValue = "";
            _ds.Tables[0].Columns["PRD_NO"].DefaultValue = "";
            return _ds;
        }
        public string UpdateBarPrtName(DataSet _changeDs)
        {
			System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["BAR_PRTNAME"] = "TYPE_ID,IDX,IDX_NO,PRD_NO,NAME";
            this.UpdateDataSet(Comp.Conn_SunSystem.Replace("sunsystem", Comp.DRP_Prop["BarPrintDB"].ToString()), _changeDs, _ht);
            DataTable _dtErr = GetAllErrors(_changeDs);
            return GetErrorString(_dtErr);
        }
        #endregion

        #region  Ȩ���趨
        public DataSet GetUserRightAll(string _usr)
        {
            string _sql = "SELECT A.COMP_BOSS,A.USR,B.COMPNO,B.ROLENO,B.PGM,B.INS FROM PSWD A LEFT JOIN FX_PSWD B ON B.COMPNO=@COMPNO AND A.USR=B.ROLENO WHERE A.COMPNO=@COMPNO AND A.USR=@ROLENO";
            SqlParameter[] _para = new SqlParameter[2];
            _para[0] = new SqlParameter("@COMPNO", SqlDbType.VarChar);
            _para[0].Value = Comp.CompNo;
            _para[1] = new SqlParameter("@ROLENO", SqlDbType.VarChar);
            _para[1].Value = _usr;
            Query _query = new Query();
            DataSet _ds = _query.FillDataSet(Comp.Conn_SunSystem,_sql,_para, new string[] { "FX_PSWD" }, false);
            return _ds;
        }
        public DataSet GetUserRight(string _usr, string _pgm)
        {
            string _sql = "SELECT A.COMP_BOSS,A.USR,B.COMPNO,B.ROLENO,B.PGM,B.INS FROM PSWD A LEFT JOIN FX_PSWD B ON B.COMPNO=@COMPNO AND A.USR=B.ROLENO WHERE A.COMPNO=@COMPNO AND A.USR=@ROLENO AND B.PGM=@PGM";
            SqlParameter[] _para = new SqlParameter[3];
            _para[0] = new SqlParameter("@COMPNO", SqlDbType.VarChar);
            _para[0].Value = Comp.CompNo;
            _para[1] = new SqlParameter("@ROLENO", SqlDbType.VarChar);
            _para[1].Value = _usr;
            _para[2] = new SqlParameter("@PGM", SqlDbType.VarChar);
            _para[2].Value = _pgm;
            Query _query = new Query();
            DataSet _ds = _query.FillDataSet(Comp.Conn_SunSystem, _sql, _para, new string[] { "FX_PSWD" }, false);
            return _ds;
        }
        public DataTable UpdateUserRight(DataSet ChangedDS, string _usr)
        {
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["FX_PSWD"] = "COMPNO,ROLENO,PGM,INS";
            this.UpdateDataSet(Comp.Conn_SunSystem, ChangedDS, _ht);
            DataTable _dt = GetAllErrors(ChangedDS);
            return _dt;
        }
        #endregion

        #region BeforeUpdate
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
		{
			if (tableName == "BAR_REC")
            {
                #region BAR_REC
                if (statementType == StatementType.Insert)
                {
                    //����BAR_PRINT��BAR_SQNO
                    string _bar_no = dr["BAR_NO1"].ToString();
                    string _prdNo = dr["PRD_NO"].ToString();
                    if (dr.Table.Columns.Contains("PRD_NO_SNM") && dr["PRD_NO_SNM"].ToString() != "")//��������ӡʱ����¼�����1��3��4λ
                        _prdNo = dr["PRD_NO_SNM"].ToString();
                    string _prd_mark = dr["BAT_NO"].ToString();//
                    string _sn = dr["SN"].ToString();
                    string _dep = dr["DEP"].ToString();
                    DataSet _dsPrtSq = GetPrtSqData(_prdNo, _prd_mark, _bar_no + _sn);
                    //������˻������ӡ����ֻ�������ź������ˮ�ţ�Ʒ��Ϊ��
                    if (dr.Table.DataSet.ExtendedProperties.Contains("SB_BAR_PRINT") && dr.Table.DataSet.ExtendedProperties["SB_BAR_PRINT"].ToString() == "T")
                        _dsPrtSq = GetPrtSqData("", _prd_mark, _bar_no + _sn);//
                    DataTable _dtErr = new DataTable();
                    if (_dsPrtSq != null && _dsPrtSq.Tables.Count > 1)
                    {
                        DataRow _dr;
                        //������ˮ�ŵ�
                        if (dr["ISMODIFY"].ToString() != "T")
                        {
                            int _maxSn = Convert.ToInt32(_sn);
                            if (dr.Table.DataSet.ExtendedProperties.Contains("MAX_SN"))//���δ�ӡ�������ˮ��
                                _maxSn = Convert.ToInt32(dr.Table.DataSet.ExtendedProperties["MAX_SN"]);
                            if (_dsPrtSq.Tables[0].Rows.Count > 0)
                            {
                                _dr = _dsPrtSq.Tables[0].Rows[0];
                                if (_dr["MAX_NO"].ToString() == "")
                                    _dr["MAX_NO"] = _maxSn;
                                else if (Convert.ToInt32(_dr["MAX_NO"]) < _maxSn)
                                    _dr["MAX_NO"] = _maxSn;
                            }
                            else
                            {
                                _dr = _dsPrtSq.Tables[0].NewRow();
                                if (dr.Table.Columns.Contains("PRD_NO_SNM") && dr["PRD_NO_SNM"].ToString() != "")
                                    _dr["PRD_NO"] = dr["PRD_NO_SNM"].ToString();
                                else
                                    _dr["PRD_NO"] = _prdNo;
                                _dr["PRD_MARK"] = _prd_mark;
                                _dr["MAX_NO"] = _maxSn;
                                _dsPrtSq.Tables[0].Rows.Add(_dr);
                            }
                            if (dr.Table.DataSet.ExtendedProperties.Contains("SB_BAR_PRINT") && dr.Table.DataSet.ExtendedProperties["SB_BAR_PRINT"].ToString() == "T")
                            {
                                //������˻������ӡ����������ˮ�ŵ�����¼��Ʒ����
                                _dr["PRD_NO"] = "";
                            }
                        }
                        //�Ѵ�ӡ���뵵��
                        if (_dsPrtSq.Tables[1].Rows.Count > 0)
                        {
                        }
                        else
                        {
                            _dr = _dsPrtSq.Tables[1].NewRow();
                            _dr["BAR_NO"] = _bar_no + _sn;
                            _dr["SPC_NO"] = "";
                            _dr["PRN_DD"] = System.DateTime.Now;
                            _dr["DEP"] = _dep;
                            _dsPrtSq.Tables[1].Rows.Add(_dr);
                        }
                        _dtErr = UpdataPrtSqData(_dsPrtSq);
                        if (_dtErr.Rows.Count > 0)
                            throw new SunlikeException(GetErrorString(_dtErr));
                    }

                    DataSet _ds = GetDataQC1(" AND BAR_NO='" + _bar_no + _sn + "'");
                    DataRow _drNew;
                    if (dr.Table.Columns.Contains("FLAG") && dr["FLAG"].ToString() == "T")
                    {
                        #region ���������Ϣ
                        //��������Ʒ����Ϣ
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            _drNew = _ds.Tables[0].Rows[0];
                            _drNew["QC"] = "N";
                            _drNew["BAT_NO"] = dr["BAT_NO"];
                            _drNew["STATUS"] = "ST";//״̬��ST��ʾ����Ʒ�����������������
                        }
                        else
                        {
                            _drNew = _ds.Tables[0].NewRow();
                            _drNew["BAR_NO"] = _bar_no + _sn;
                            _drNew["QC"] = "N";
                            _drNew["BAT_NO"] = dr["BAT_NO"];
                            _drNew["STATUS"] = "ST";//״̬��ST��ʾ����Ʒ�����������������
                            _ds.Tables[0].Rows.Add(_drNew);
                        }
                        #endregion
                    }
                    else if (dr.Table.Columns.Contains("ISCHANGE") && dr["ISCHANGE"].ToString() == "T")
                    {
                        #region ���������ӡʱ
                        //��������Ʒ����Ϣ
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            _drNew = _ds.Tables[0].Rows[0];
                            _drNew["QC"] = DBNull.Value;
                            _drNew["BAT_NO"] = dr["BAT_NO"];
                            if (dr.Table.Columns.Contains("OS_BAR_NO"))
                                _drNew["OS_BAR_NO"] = dr["OS_BAR_NO"];
                            _drNew["STATUS"] = "HH";//״̬������
                            _drNew["FORMAT"] = dr["FORMAT"];
                            _drNew["LH"] = dr["LH"];
                            _drNew["PRT_NAME"] = dr["PRT_NAME"];
                            _drNew["USR"] = dr["USR"];
                            _drNew["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                            int _barLen = dr["BAR_NO"].ToString().Replace("#", "").Length;
                            if (_barLen == 22)
                                _drNew["PRD_TYPE"] = 1;//���
                            else if (_barLen == 24)
                                _drNew["PRD_TYPE"] = 2;//���Ʒ
                            else if (_barLen > 24)
                                _drNew["PRD_TYPE"] = 3;//���
                            else
                                _drNew["PRD_TYPE"] = 0;
                        }
                        else
                        {
                            _drNew = _ds.Tables[0].NewRow();
                            _drNew["BAR_NO"] = _bar_no + _sn;
                            _drNew["QC"] = DBNull.Value;
                            _drNew["BAT_NO"] = dr["BAT_NO"];
                            if (dr.Table.Columns.Contains("OS_BAR_NO"))
                                _drNew["OS_BAR_NO"] = dr["OS_BAR_NO"];
                            _drNew["STATUS"] = "HH";//״̬������
                            _drNew["FORMAT"] = dr["FORMAT"];
                            _drNew["LH"] = dr["LH"];
                            _drNew["PRT_NAME"] = dr["PRT_NAME"];
                            _drNew["USR"] = dr["USR"];
                            _drNew["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                            int _barLen = dr["BAR_NO"].ToString().Replace("#", "").Length;
                            if (_barLen == 22)
                                _drNew["PRD_TYPE"] = 1;//���
                            else if (_barLen == 24)
                                _drNew["PRD_TYPE"] = 2;//���Ʒ
                            else if (_barLen > 24)
                                _drNew["PRD_TYPE"] = 3;//���
                            else
                                _drNew["PRD_TYPE"] = 0;
                            _ds.Tables[0].Rows.Add(_drNew);
                        }
                        #endregion
                    }
                    else
                    {
                        #region һ������
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            _drNew = _ds.Tables[0].Rows[0];
                            _drNew["BAT_NO"] = dr["BAT_NO"];
                            _drNew["FORMAT"] = dr["FORMAT"];
                            _drNew["LH"] = dr["LH"];
                            _drNew["PRT_NAME"] = dr["PRT_NAME"];
                            _drNew["USR"] = dr["USR"];
                            _drNew["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                            int _barLen = dr["BAR_NO"].ToString().Replace("#","").Length;
                            if (_barLen == 22)
                                _drNew["PRD_TYPE"] = 1;//���
                            else if (_barLen == 24)
                                _drNew["PRD_TYPE"] = 2;//���Ʒ
                            else if (_barLen >24)
                                _drNew["PRD_TYPE"] = 3;//���
                            else
                                _drNew["PRD_TYPE"] = 0;
                        }
                        else
                        {
                            string _bar_noTemp=_bar_no + _sn;
                            _drNew = _ds.Tables[0].NewRow();
                            _drNew["BAR_NO"] = _bar_noTemp;
                            _drNew["BAT_NO"] = dr["BAT_NO"];
                            _drNew["FORMAT"] = dr["FORMAT"];
                            _drNew["LH"] = dr["LH"];
                            _drNew["PRT_NAME"] = dr["PRT_NAME"];
                            _drNew["USR"] = dr["USR"];
                            _drNew["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                            int _barLen = _bar_noTemp.Replace("#", "").Length;
                            if (_barLen == 22)
                                _drNew["PRD_TYPE"] = 1;//���
                            else if (_barLen == 24)
                                _drNew["PRD_TYPE"] = 2;//���Ʒ
                            else if (_barLen > 24)
                                _drNew["PRD_TYPE"] = 3;//���
                            else
                                _drNew["PRD_TYPE"] = 0;
                            _ds.Tables[0].Rows.Add(_drNew);
                        }
                        #endregion
                    }
                    _dtErr = this.UpdataDataQC(_ds);
                    if (_dtErr.Rows.Count > 0)
                        throw new SunlikeException(GetErrorString(_dtErr));
                }
                if (statementType != StatementType.Delete)
                {
                    if (statementType == StatementType.Update)
                    {
                        //װ��ʱ
                        DataSet _ds = GetDataQC1(" AND BAR_NO='" + dr["BAR_NO"].ToString() + "'");
                        DataRow _drNew;
                        DataTable _dtErr = new DataTable();
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            _drNew = _ds.Tables[0].Rows[0];
                            if (dr.Table.Columns.Contains("USR_BOX"))
                            {
                                _drNew["USR_BOX"] = dr["USR_BOX"];
                            }
                            if (dr.Table.Columns.Contains("BOX_DATE"))
                            {
                                _drNew["BOX_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                            }
                            if (dr.Table.Columns.Contains("USR_WHBOX"))
                            {
                                _drNew["USR_WHBOX"] = dr["USR_WHBOX"];
                            }
                            if (dr.Table.Columns.Contains("WHBOX_DATE"))
                            {
                                _drNew["WHBOX_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                            }
                        }
                        else
                        {
                            _drNew = _ds.Tables[0].NewRow();
                            _drNew["BAR_NO"] = dr["BAR_NO"].ToString();
                            if (dr.Table.Columns.Contains("USR_BOX"))
                            {
                                _drNew["USR_BOX"] = dr["USR_BOX"];
                            }
                            if (dr.Table.Columns.Contains("BOX_DATE"))
                            {
                                _drNew["BOX_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                            }
                            if (dr.Table.Columns.Contains("USR_WHBOX"))
                            {
                                _drNew["USR_WHBOX"] = dr["USR_WHBOX"];
                            }
                            if (dr.Table.Columns.Contains("WHBOX_DATE"))
                            {
                                _drNew["WHBOX_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                            }
                            _ds.Tables[0].Rows.Add(_drNew);
                        }
                        _dtErr = this.UpdataDataQC(_ds);
                        if (_dtErr.Rows.Count > 0)
                            throw new SunlikeException(GetErrorString(_dtErr));
                    }
                }
                #endregion
            }
            if (tableName == "BAR_BOX")
            {
                #region BAR_BOX
                if (statementType == StatementType.Insert)
                {
                    //����BAR_PRINT��BAR_SQNO
                    string _bar_no = dr["BOX_NO1"].ToString();
                    string _sn = dr["SN"].ToString();
                    DataSet _dsPrtSq = GetPrtSqData(dr["PRD_NO1"].ToString(), dr["PRD_MARK1"].ToString(), _bar_no);
                    if (_dsPrtSq != null && _dsPrtSq.Tables.Count > 1)
                    {
                        DataRow _dr;
                        //������ˮ�ŵ�
                        if (_dsPrtSq.Tables[0].Rows.Count > 0)
                        {
                            _dr = _dsPrtSq.Tables[0].Rows[0];
                            _dr["MAX_NO"] = Convert.ToInt32(_sn);
                        }
                        else
                        {
                            _dr = _dsPrtSq.Tables[0].NewRow();
                            _dr["PRD_NO"] = dr["PRD_NO1"];
                            _dr["PRD_MARK"] = dr["PRD_MARK1"];
                            _dr["MAX_NO"] = Convert.ToInt32(_sn);
                            _dsPrtSq.Tables[0].Rows.Add(_dr);
                        }
                        //�Ѵ�ӡ���뵵��
                        if (_dsPrtSq.Tables[1].Rows.Count > 0)
                        {
                        }
                        else
                        {
                            _dr = _dsPrtSq.Tables[1].NewRow();
                            _dr["BAR_NO"] = _bar_no + _sn;
                            _dr["SPC_NO"] = "";
                            _dr["PRN_DD"] = System.DateTime.Now;
                            _dr["DEP"] = dr["DEP"];
                            _dsPrtSq.Tables[1].Rows.Add(_dr);
                        }
                        DataTable _dtErr = UpdataPrtSqData(_dsPrtSq);
                        if (_dtErr.Rows.Count > 0)
                            throw new SunlikeException(GetErrorString(_dtErr));
                    }
                    DataSet _dsBarBox = GetDataBoxBat(_bar_no + _sn);
                    //�����������Ӧ����
                    if (_dsBarBox.Tables[0].Rows.Count > 0)
                    {
                    }
                    else
                    {
                        DataRow _dr = _dsBarBox.Tables[0].NewRow();
                        _dr["BOX_NO"] = _bar_no + _sn;
                        _dr["BAT_NO"] = dr["BAT_NO"];
                        _dr["LH"] = dr["LH"];
                        _dr["IDX_NAME"] = dr["IDX_NAME"];
                        if (dr.Table.Columns.Contains("FORMAT"))
                            _dr["FORMAT"] = dr["FORMAT"];
                        if (dr.Table.Columns.Contains("USR_PRT"))
                            _dr["USR_PRT"] = dr["USR_PRT"];
                        _dsBarBox.Tables[0].Rows.Add(_dr);
                    }
                    DataTable _dtErr1 = UpdataDataBoxBat(_dsBarBox);
                    if (_dtErr1.Rows.Count > 0)
                        throw new SunlikeException(GetErrorString(_dtErr1));
                }
                if (statementType == StatementType.Update)
                {
                    DataSet _dsBarBox = GetDataBoxBat(dr["BOX_NO"].ToString());
                    //�����������Ӧ����
                    if (_dsBarBox.Tables[0].Rows.Count > 0)
                    {
                        DataRow _dr= _dsBarBox.Tables[0].Rows[0];
                        _dr["BOX_NO"] = dr["BOX_NO"];
                        _dr["BAT_NO"] = dr["BAT_NO"];
                        _dr["LH"] = dr["LH"];
                        _dr["IDX_NAME"] = dr["IDX_NAME"];
                        if (dr.Table.Columns.Contains("FORMAT"))
                            _dr["FORMAT"] = dr["FORMAT"];
                        if (dr.Table.Columns.Contains("FLAG") && dr["FLAG"].ToString() == "T")
                            _dr["PRD_MARK"] = "T";//����������������
                        if (dr.Table.Columns.Contains("REM"))
                            _dr["REM"] = dr["REM"];
                    }
                    DataTable _dtErr1 = UpdataDataBoxBat(_dsBarBox);
                    if (_dtErr1.Rows.Count > 0)
                        throw new SunlikeException(GetErrorString(_dtErr1));
                }
                #endregion
            }
            if (tableName == "TF_PSS3")
            {
                if (statementType == StatementType.Update)
                {
                    //�����������滻
                    DateTime _dt = DateTime.Now;
                    if (dr["CHG_DATE"].ToString() != "")
                        _dt = Convert.ToDateTime(dr["CHG_DATE"]);
                    string _sql = "update BAR_REC set STOP_ID='T' where BAR_NO='" + dr["BAR_CODE"].ToString() + "';"
                                + "update BAR_REC set STOP_ID='F' where BAR_NO='" + dr["BAR_CODE", DataRowVersion.Original].ToString() + "'"
                                + "update " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..SA_BAR_CHG set REM='" + dr["REM"].ToString() + "',CHG_ID='T',USR_CHG='" + dr["USR_CHG"].ToString() + "',CHG_DATE='" + _dt.ToString(Comp.SQLDateTimeFormat) + "' where PS_ID='" + dr["PS_ID", DataRowVersion.Original].ToString() + "' AND PS_NO='" + dr["PS_NO", DataRowVersion.Original].ToString() + "' AND BAR_CODE='" + dr["BAR_CODE", DataRowVersion.Original].ToString() + "'";
                    Query _query = new Query();
                    _query.RunSql(_sql);
                }
            }
            if (tableName == "BAR_BOX_BAT")
            {
                if (statementType != StatementType.Delete)
                {
                    if (dr.Table.Columns.Contains("USR_SJ"))
                    {
                        //��Ʒ�ͽ�

                        DateTime _dt = DateTime.Now;
                        if (dr["SJ_DATE"].ToString() != "")
                            _dt = Convert.ToDateTime(dr["SJ_DATE"]);
                        string _sql = "update " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC set USR_SJ='" + dr["USR_SJ"].ToString() + "',SJ_DATE='" + _dt.ToString(Comp.SQLDateTimeFormat) + "' FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC A INNER JOIN BAR_REC B ON A.BAR_NO=B.BAR_NO where B.BOX_NO='" + dr["BOX_NO"].ToString() + "';";
                        Query _query = new Query();
                        _query.RunSql(_sql);
                    }
                }
            }
			base.BeforeUpdate(tableName, statementType, dr, ref status);
        }
        #endregion

        #region ���÷���
        /// <summary>
        /// ��ת��:ת��Ϊ���ݿ�����
        /// </summary>
        /// <param name="_txt"></param>
        /// <returns></returns>
        public string ConvertToDBLanguage(string _txt)
        {
            if (Comp.DataBaseLanguage.ToLower() == "zh-tw")
            {
                return Strings.StrConv(_txt, VbStrConv.TraditionalChinese, 2);
            }
            else
            {
                return Strings.StrConv(_txt, VbStrConv.SimplifiedChinese, 0);
            }
        }
        /// <summary>
        /// ȡ������Ϣ
        /// </summary>
        /// <param name="dtError"></param>
        /// <returns></returns>
        private string GetErrorString(DataTable dtError)
        {
            string _result = "";
            for (int i = 0; i < dtError.Rows.Count; i++)
            {
                string _rem = dtError.Rows[i]["REM"].ToString();
                if (_rem.IndexOf("RCID=") >= 0)
                {
                    _rem = this.GetErrorResource(_rem);
                }
                _result += " " + _rem;
            }
            return _result;
        }

        /// <summary>
        ///  ������Դ����ȡ��˵����ErrorMessage�����ʽ��"RCID=FORMNAME.CAPTIONID,PARAM=001,TEXT=����˵��;"
        ///  PARAM��ʾ��Դ��������õ�FormateString����������PARAM����ʽ�����.
        ///  ex. ��Դ [FORMNAME.CAPTIONID]= "���Ŵ���{0}������" ��"RCID=FORMNAME.CAPTIONID,PARAM=001,TEXT=����˵��;"
        ///  ��������Ϊ"���Ŵ���001������"
        ///  
        ///  P.S. ����ж��RCID���÷ֺ�[��]���ָ�.
        /// </summary>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        private string GetErrorResource(string ErrorMessage)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-cn");
            string _path = AppDomain.CurrentDomain.BaseDirectory + @"Resources\";
            ResourceCenter.LoadXML(_path, _path + "ResourceData.xml", "",  System.Threading.Thread.CurrentThread.CurrentUICulture);
            ResourceCenter _res = Sunlike.Common.CommonVar.ResourceCenter.ResourceManager;

            int _tmpInt1 = -1;
            int _tmpInt2 = -1;
            //��¼_tmpRes��aErrRes��ʵ��λ��
            int _tmpInt3 = 0;
            string _tmpRes = ErrorMessage;
            string _tmpStr = "";
            string _returnStr = "";
            while (_tmpRes != "")
            {
                _tmpInt1 = _tmpRes.IndexOf("RCID=");
                //����д���
                if (_tmpInt1 != -1)
                {
                    _tmpInt2 = _tmpRes.IndexOf("RCID=", _tmpInt1 + 5);
                    //����Ƿ��еڶ�������
                    if (_tmpInt2 != -1)
                    {
                        _tmpInt3 += _tmpInt2;
                        _tmpRes = _tmpRes.Substring(_tmpInt1 + 5, _tmpInt2 - _tmpInt1 - 5);
                    }
                    else
                        _tmpRes = _tmpRes.Substring(_tmpInt1 + 5);
                    if (_tmpRes.IndexOf("PARAM=") != -1)
                    {
                        _tmpStr = _tmpRes.Substring(0, _tmpRes.IndexOf("PARAM=") - 1);
                    }
                    else if (_tmpRes.IndexOf("TEXT=") != -1)
                    {
                        _tmpStr = _tmpRes.Substring(0, _tmpRes.IndexOf("TEXT=") - 1);
                    }
                    else
                    {
                        _tmpStr = _tmpRes;
                    }
                    //����Ҳ������ϣ��ʹ���ԭ�ִ�
                    if (_res.GetResource(_tmpStr) == _tmpStr)
                    {
                        _returnStr += "RCID=" + _tmpRes + "\n";
                    }
                    else
                    {
                        //û�д������Ļ��������������
                        if (_tmpRes.IndexOf("PARAM=") == -1)
                        {
                            _returnStr += _res.GetResource(_tmpStr) + "\n";
                        }
                        else
                        {
                            string[] _ary1 = _tmpRes.Split(',');
                            if (_ary1.Length > 1)
                            {
                                int _aryLength;
                                if (_tmpRes.IndexOf("TEXT=") == -1)
                                    _aryLength = _ary1.Length - 1;
                                else
                                    _aryLength = _ary1.Length - 2;
                                if (_aryLength > 0)
                                {
                                    string[] _ary2 = new string[_aryLength];
                                    for (int i = 0; i < _aryLength; i++)
                                        _ary2[i] = _ary1[i + 1].Replace("PARAM=", "");
                                    _returnStr += String.Format(_res.GetResource(_tmpStr), _ary2) + "\n";
                                }
                                else
                                {
                                    _returnStr += _res.GetResource(_tmpStr) + "\n";
                                }
                            }
                            else
                            {
                                _returnStr += _res.GetResource(_tmpStr) + "\n";
                            }
                        }
                    }
                    //������д�����Ϣ�����ҳ������Ϣ
                    if (_tmpInt2 != -1)
                        _tmpRes = ErrorMessage.Substring(_tmpInt3);
                    else
                        _tmpRes = "";
                    _tmpInt1 = -1;
                    _tmpInt2 = -1;
                }
                else
                {
                    _returnStr += _tmpRes;
                    break;
                }
            }
            if (_returnStr.Substring(_returnStr.Length - 1, 1) == "\n")
            {
                _returnStr = _returnStr.Substring(0, _returnStr.Length - 1);
            }
            return _returnStr;
        }
        #endregion
    }
}
