using System;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business
{
	/// <summary>
	/// Summary description for CardMember.
	/// </summary>
	public class CardMember : BizObject
	{
		/// <summary>
		/// 会员卡
		/// </summary>
		public CardMember()
		{
		}

		/// <summary>
		/// 取得会员卡资料
		/// </summary>
		/// <param name="usr">操作人</param>
		/// <param name="cardNo">卡号</param>
		/// <param name="onlyFillSchema">是否只读取Schema</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string usr,string cardNo,bool onlyFillSchema)
		{
			DbCardMember _card = new DbCardMember(Comp.Conn_DB);
			SunlikeDataSet _ds = _card.GetData(cardNo,onlyFillSchema);
			//增加权限
			if (!onlyFillSchema)
			{

				if (usr != null && !String.IsNullOrEmpty(usr))
				{
					string _pgm = "CARDMEMBERFIND";
					DataTable _dt = _ds.Tables["POSCARD"];
					if (_dt.Rows.Count > 0 )
					{
						string _bill_Dep = _dt.Rows[0]["DEP"].ToString();
						string _bill_Usr = _dt.Rows[0]["USR"].ToString();
						System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, usr,_bill_Dep,_bill_Usr);
						_ds.ExtendedProperties["UPD"] = _billRight["UPD"];
						_ds.ExtendedProperties["DEL"] = _billRight["DEL"];
						_ds.ExtendedProperties["PRN"] = _billRight["PRN"];
					}
				}
			}

			return _ds;
		}

		/// <summary>
		/// 更新会员卡资料
		/// </summary>
		/// <param name="changedDS">会员卡资料</param>
		/// <param name="bubbleException">是否抛出异常（true为直接抛出异常，false返回ErrorTable）</param>
		/// <returns></returns>
		public DataTable UpdateData(SunlikeDataSet changedDS,bool bubbleException)
        {
			System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["POSCARD"] = "CARD_NO,NAME,TEL,TEL1,ADR,E_MAIL,BTH_DAY,BN_DD,EN_DD,USER_ID,DEP,GX_NO,ZIP,SEX_ID,USR,CELL_NO,CARD_CLS,REM,SYSED_ID,OLD_NO,WANGWANG,QQ,MSN,SHOW_ID,COUN_ID,JOB_REM";
			this.UpdateDataSet(changedDS,_ht);
			if ( changedDS.HasErrors && bubbleException )
			{
				string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(changedDS);
				throw new SunlikeException("RCID=CARDMEMBER.UpdateData() Error:"+ _errorMsg);
			}
			return Sunlike.Business.BizObject.GetAllErrors(changedDS);
		}

		/// <summary>
		/// BeforeUpdate
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="statementType"></param>
		/// <param name="dr"></param>
		/// <param name="status"></param>
		protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
		{
			if (tableName == "POSCARD")
			{
				//检查资料正确否
				if (statementType != StatementType.Delete)
				{
					//检查部门
					if (string.IsNullOrEmpty(dr["USR"].ToString()))
					{
						dr["USR"] = Comp.DRP_Prop["CARD_USR"];
					}
					Dept _dept = new Dept();
					if (!string.IsNullOrEmpty(dr["DEP"].ToString()) && !_dept.IsExist(dr["USR"].ToString(),dr["DEP"].ToString()))
					{
						dr.SetColumnError("DEP","RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());//部门[{0}]不存在或没有对其操作的权限，请检查
						status = UpdateStatus.SkipAllRemainingRows;
					}
					//停用客户
					if (!string.IsNullOrEmpty(dr["CUS_NO"].ToString() ) && dr["EN_DD"] != dr["EN_DD",DataRowVersion.Original])
					{
						Query _query = new Query();
						if (String.IsNullOrEmpty(dr["EN_DD"].ToString()))
						{
							_query.RunSql("update CUST set END_DD=null where CUS_NO='"
								+ dr["CUS_NO"].ToString() + "'");
						}
						else
						{
							_query.RunSql("update CUST set END_DD='" + Convert.ToDateTime(dr["EN_DD"]).ToString(Comp.SQLDateFormat)
								+ "' where CUS_NO='" + dr["CUS_NO"].ToString() + "'");
						}
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(dr["CUS_NO",DataRowVersion.Original].ToString()))
					{
						Cust _cust = new Cust();
                        DataTable _dtCust = _cust.GetData(dr["CUS_NO", DataRowVersion.Original].ToString());
						WH _wh = new WH();
						DataTable _dtCusWh = _wh.GetCusWhData(dr["CUS_NO",DataRowVersion.Original].ToString());
						_dtCust.DataSet.Merge(_dtCusWh,false,MissingSchemaAction.AddWithKey);
						_dtCust.DataSet.Relations.Add(new DataRelation("CUST_CUS_WH",_dtCust.Columns["CUS_NO"],_dtCust.DataSet.Tables["CUS_WH"].Columns["CUS_NO"]));
						if (_dtCust.Rows.Count > 0)
						{
							if (dr.Table.DataSet.ExtendedProperties["UPDATECUST"] == null)
								_dtCust.Rows[0].Delete();
							else if (dr.Table.DataSet.ExtendedProperties["UPDATECUST"].ToString() =="T")
								_dtCust.Rows[0]["CARD_NO"] = "";
							DataTable _dtErr = _cust.UpdateDate(SunlikeDataSet.ConvertTo(_dtCust.DataSet),false);
							if (_dtErr.Rows.Count > 0)
							{
								dr.SetColumnError("CUS_NO","RCID=EC.HINT.UPD_CUST_ERROR");//无法更新客户！
								status = UpdateStatus.SkipAllRemainingRows;
							}
						}
					}
				}
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
			if (tableName == "POSCARD")
			{
				// 更新删除表内容
				Query _query = new Query();
				if (statementType == StatementType.Insert)
				{
					_query.RunSql("DELETE FROM POSCARD_DEL WHERE CARD_NO='" + dr["CARD_NO"].ToString() + "'");
				}
				else if (statementType == StatementType.Delete)
				{
					_query.RunSql("INSERT INTO POSCARD_DEL(CARD_NO) VALUES('" + dr["CARD_NO",DataRowVersion.Original].ToString() + "')");
				}

                if (statementType == StatementType.Insert)
                {
                    if (dr.Table.DataSet.ExtendedProperties["B2C_AUDIT"] != null)
                    {
                        _query.RunSql("delete from B2C_USER where CUS_NO='"+ dr["CARD_NO"].ToString() + "'");
                    }
                }
			}
		}
		/// <summary>
		/// 同时插入会员资料和客户资料
		/// </summary>
		/// <param name="CardDS">会员资料</param>
		/// <param name="CustDS">客户资料</param>
		/// <returns></returns>
		public DataTable InsertB2CUser(SunlikeDataSet CardDS,SunlikeDataSet CustDS)
		{
			this.EnterTransaction();
			DataTable _dtError = this.UpdateData(CardDS,false);
			if (_dtError.Rows.Count == 0)
			{
				UdfNo _udfNo = new UdfNo();
				for (int i=0;i<CustDS.Tables["CUS_WH"].Rows.Count;i++)
				{
					try
					{
						CustDS.Tables["CUS_WH"].Rows[i]["WH"] = _udfNo.Set(CustDS.Tables["CUS_WH"].Rows[i]["CUS_NO"].ToString() + "->",4);
					}
					catch(Exception _ex)
					{
						this.SetAbort();
						DataRow _drError = _dtError.NewRow();
						_drError["TableName"] = "CUS_WH";
						_drError["ITM"] = i + 1;
						_drError["REM"] = _ex.Message;
						_dtError.Rows.Add(_drError);
					}
				}
				Cust _cust = new Cust();
				_dtError =_cust.UpdateDate(CustDS,false);				
				if (_dtError.Rows.Count > 0)
				{
					this.SetAbort();
				}
				else
				{
					try
					{
						Query _query = new Query();
						_query.RunSql("delete from B2C_USER where CUS_NO='"
							+ CustDS.Tables["CUST"].Rows[0]["CARD_NO"].ToString() + "'");
						this.SetComplete();
					}
					catch(Exception _ex)
					{
						this.SetAbort();
						DataRow _drError = _dtError.NewRow();
						_drError["TableName"] = "B2C_USER";
						_drError["ITM"] = 1;
						_drError["REM"] = _ex.Message;
						_dtError.Rows.Add(_drError);
					}
				}
			}
			this.LeaveTransaction();
			return _dtError;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="cardNo"></param>
		/// <returns></returns>
		public string GetCardCust(string cardNo)
		{
			DbCardMember _card = new DbCardMember(Comp.Conn_DB);
			return _card.GetCardCust(cardNo);
		}
		/// <summary>
		/// 取得会员信息，用于批次停用
		/// </summary>
		/// <param name="sqlWhere">销货单日期区间</param>
		/// <param name="isSchema"></param>
		/// <returns></returns>
		public SunlikeDataSet GetCard4End(string sqlWhere,bool isSchema)
		{
			DbCardMember _card = new DbCardMember(Comp.Conn_DB);
			return _card.GetCard4End(sqlWhere,isSchema);
		}
		/// <summary>
		/// 更新停用注记
		/// </summary>
		/// <param name="cardNo"></param>
		public void SetEndId(string cardNo)
		{
			DbCardMember _card = new DbCardMember(Comp.Conn_DB);
			_card.SetEndId(cardNo);
		}
        /// <summary>
        /// 取得会员折扣
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="_date"></param>
        /// <returns></returns>
        public decimal GetCardDiscount(string cardNo, DateTime _date)
        {
            decimal _disCnt = 0;
            SunlikeDataSet _dsCard = this.GetData("", cardNo, false);
            if (_dsCard.Tables[0].Rows.Count > 0)
            {
                SETLEVEL _setLevel = new SETLEVEL();
                SunlikeDataSet _dsLv = _setLevel.GetData(_dsCard.Tables[0].Rows[0]["CARD_CLS"].ToString());
                if (_dsLv.Tables[0].Rows.Count > 0)
                {
                    string _birthday = _dsCard.Tables[0].Rows[0]["BTH_DAY"].ToString();
                    if (!string.IsNullOrEmpty(_birthday)
                        && Convert.ToDateTime(_birthday).Month == _date.Month
                        && Convert.ToDateTime(_birthday).Day == _date.Day
                        && !string.IsNullOrEmpty(_dsLv.Tables[0].Rows[0]["BTH_DIS"].ToString())
                        && _dsLv.Tables[0].Rows[0]["BTH_DIS"].ToString() != "0")
                    {
                        _disCnt = Convert.ToDecimal(_dsLv.Tables[0].Rows[0]["BTH_DIS"]);
                    }
                    else if (!string.IsNullOrEmpty(_dsLv.Tables[0].Rows[0]["DIS_PER"].ToString()))
                    {
                        _disCnt = Convert.ToDecimal(_dsLv.Tables[0].Rows[0]["DIS_PER"]);
                    }
                }
            }
            return _disCnt;
        }
	}
}
