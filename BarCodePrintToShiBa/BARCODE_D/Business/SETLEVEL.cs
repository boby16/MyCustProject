using System;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business
{
	/// <summary>
	/// Summary description for SETLEVEL.
	/// </summary>
	public class SETLEVEL : BizObject
	{
		/// <summary>
		/// ��Ա�ȼ��趨
		/// </summary>
		public SETLEVEL()
		{
		}

		/// <summary>
		/// ȡ��Ա�ȼ�����
		/// </summary>
		/// <param name="sqlWhere"></param>
		/// <returns></returns>
		public SunlikeDataSet GetDataSqlWhere(string sqlWhere)
		{
			DbSETLEVEL _le = new DbSETLEVEL(Comp.Conn_DB);
			SunlikeDataSet _ds = _le.GetDataSqlWhere(sqlWhere);
			return _ds;
		}

		/// <summary>
		/// ȡ��Ա�ȼ�����
		/// </summary>
		/// <param name="CardCls">�ȼ�����</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string CardCls)
		{
			DbSETLEVEL _le = new DbSETLEVEL(Comp.Conn_DB);
			SunlikeDataSet _ds = _le.GetData(CardCls);
			return _ds;
		}

		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="ChangedDS"></param>
		/// <param name="bubbleException">�Ƿ��׳��쳣��trueΪֱ���׳��쳣��false����ErrorTable��</param>
		public DataTable UpdateData(SunlikeDataSet ChangedDS,bool bubbleException)
		{
			System.Collections.Hashtable _ht = new System.Collections.Hashtable();
			_ht["POSCARDTP"] = "CARD_CLS,NAME,DIS_PER,BTH_DIS";
			this.UpdateDataSet(ChangedDS,_ht);
			if ( ChangedDS.HasErrors && bubbleException )
			{
				string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(ChangedDS);
				throw new SunlikeException("RCID=SETLEVEL.UpdateData() Error:"+ _errorMsg);
			}
			DataTable _dtError = BizObject.GetAllErrors(ChangedDS);
			return _dtError;
		}

		/// <summary>
		/// ����֮ǰ
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="statementType"></param>
		/// <param name="dr"></param>
		/// <param name="status"></param>
		protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
		{
			if (tableName == "POSCARDTP" && statementType == StatementType.Delete)
			{
                Query _query = new Query();
                SunlikeDataSet _ds = _query.DoSQLString("select CARD_CLS from POSCARD where CARD_CLS='" + dr["CARD_CLS", DataRowVersion.Original].ToString() + "'");
				if (_ds.Tables[0].Rows.Count > 0)
				{
					dr.SetColumnError("CARD_CLS","RCID=EC.HINT.CARD_CLS_CANNOTDEL,PARAM=" + dr["CARD_CLS",DataRowVersion.Original].ToString());//��Ա�ȼ�[{0}]��ʹ�ã�����ɾ��
					status = UpdateStatus.SkipAllRemainingRows;
				}
			}
            else if (tableName == "POSCARDTP_REQ" && statementType == StatementType.Insert)
            {
                DbSETLEVEL _level = new DbSETLEVEL(Comp.Conn_DB);
                dr["ITM"] = _level.GetReqMaxItm();
            }
        }

        /// <summary>
        /// AfterUpdate
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        /// <param name="recordsAffected"></param>
        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {
            if (tableName == "POSCARDTP")
            {
                string _cardCls;
                if (statementType == StatementType.Delete)
                {
                    _cardCls = dr["CARD_CLS", DataRowVersion.Original].ToString();
                }
                else
                {
                    _cardCls = dr["CARD_CLS"].ToString();
                }
                Query _query = new Query();
                if (statementType == StatementType.Insert)
                {
                    // ����ɾ��������
                    _query.RunSql("DELETE FROM POSCARDTP_DEL WHERE CARD_CLS='" + _cardCls + "'");
                }
                else if (statementType == StatementType.Delete)
                {
                    // ����ɾ��������
                    _query.RunSql("INSERT INTO POSCARDTP_DEL(CARD_CLS) VALUES('" + _cardCls + "')");
                }
            }
			else if (tableName == "POSCARDTP_REQ" && statementType == StatementType.Update)
			{
				string _cardCls = dr.Table.DataSet.ExtendedProperties["CARD_CLS_REQ"].ToString();
				if (dr["STATUS"].ToString() == "1")
				{
					//�����
					Query _query = new Query();
					SunlikeDataSet _dsReq = _query.DoSQLString("select ITM from POSCARDTP_REQ where CARD_NO='"
						+ dr["CARD_NO"].ToString() + "' and REQ_DD>'" + dr["REQ_DD"].ToString() + "'");
					if (_dsReq.Tables[0].Rows.Count > 0)
					{
						throw new SunlikeException("RCID=INV.HINT.HASCLS");//�Ѿ��᰸�����ܷ���ˣ�
					}
					else
					{
						CardMember _card = new CardMember();
						SunlikeDataSet _dsCard = _card.GetData("", dr["CARD_NO"].ToString(), false);
						if (_dsCard.Tables["POSCARD"].Rows.Count > 0)
						{
							_dsCard.Tables["POSCARD"].Rows[0]["CARD_CLS"] = dr["CARD_CLS"];
							_card.UpdateData(_dsCard, true);
						}
						else
						{
							throw new SunlikeException("RCID=EC.HINT.NOTEXIST");//��Ա�ʺŲ����ڣ�
						}
					}
				}
				else if (dr["STATUS"].ToString() == "2")
				{
					//���ͬ��
					if (_cardCls == dr["CARD_CLS"].ToString())
					{
						throw new SunlikeException("RCID=EC.UPLEVEL.CARD_CLS_ERROR");//��Ա�Ѿ��ǵ�ǰ�ȼ���������������
					}
					else
					{
						CardMember _card = new CardMember();
						SunlikeDataSet _dsCard = _card.GetData("", dr["CARD_NO"].ToString(), false);
						if (_dsCard.Tables["POSCARD"].Rows.Count > 0)
						{
							_dsCard.Tables["POSCARD"].Rows[0]["CARD_CLS"] = _cardCls;
							_card.UpdateData(_dsCard, true);
							Query _query = new Query();
							SunlikeDataSet _dsQuery = _query.DoSQLString("select CUS_NO from CUST where CARD_NO='"
								+ dr["CARD_NO"].ToString() + "'");
							if (_dsQuery.Tables[0].Rows.Count > 0)
							{
								Cust _cust = new Cust();
                                DataTable _dtCust = _cust.GetData(_dsQuery.Tables[0].Rows[0]["CUS_NO"].ToString());
								if (_dtCust.Rows.Count > 0)
								{
									Users _users = new Users();
									DataTable _dtUser = _users.GetData(dr["CHK_MAN"].ToString());
									if (_dtUser.Rows[0]["ISSALM"].ToString() == "T" && _dtUser.Rows[0]["COMP_BOSS"].ToString() != "T")
									{
										_dtCust.Rows[0]["SAL"] = dr["CHK_MAN"];
										SunlikeDataSet _dsCust = new SunlikeDataSet();
										_dsCust.Merge(_dtCust);
										_cust.UpdateDate(_dsCust, true);
									}
								}
								else
								{
									throw new SunlikeException("RCID=EC.HINT.CUSNONOTEXIST");//�ͻ����Ų����ڣ�
								}
							}
							else
							{
								throw new SunlikeException("RCID=EC.HINT.CUSNONOTEXIST");//�ͻ����Ų����ڣ�
							}
						}
						else
						{
							throw new SunlikeException("RCID=EC.HINT.NOTEXIST");//��Ա�ʺŲ����ڣ�
						}
					}
				}
			}
        }

        /// <summary>
        /// ȡ��Ա�ȼ�������������
        /// </summary>
        /// <param name="itm">�������</param>
        /// <returns></returns>
        public SunlikeDataSet GetReqData(int itm)
        {
            DbSETLEVEL _level = new DbSETLEVEL(Comp.Conn_DB);
            SunlikeDataSet _ds = _level.GetReqData(itm);
            return _ds;
        }

        /// <summary>
        /// ���»�Ա�ȼ�������������
        /// </summary>
        /// <param name="ds">������������</param>
        /// <returns></returns>
        public DataTable UpdateReqData(SunlikeDataSet ds)
        {
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["POSCARDTP_REQ"] = "ITM,CARD_NO,CARD_CLS,REQ_DD,REM,STATUS,CHK_MAN,CLS_DATE";
            UpdateDataSet(ds, _ht);
            DataTable _dtError = GetAllErrors(ds);
            return _dtError;
        }
	}
}
