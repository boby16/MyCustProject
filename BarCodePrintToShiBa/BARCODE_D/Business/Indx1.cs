using System;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using System.Collections;
using Sunlike.Common.CommonVar;
namespace Sunlike.Business
{
	/// <summary>
	/// 费用项目资料库
	/// </summary>
	public class Indx1: Sunlike.Business.BizObject
	{
		#region 构造函数
		/// <summary>
		/// 构造函数
		/// </summary>
		public Indx1()
		{
		}
		#endregion

        #region 保存
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="throwException"></param>
        public void UpdateData(SunlikeDataSet dataSet, bool throwException)
        {
            DataTable _dtINDX1 = dataSet.Tables["INDX1"];
            Hashtable _ht = new Hashtable();
            _ht["INDX1"] = "IDX1,NAME,CHK_COMP,CHK_SUM";
            this.UpdateDataSet(dataSet, _ht);
            if (dataSet.HasErrors)
            {
                if (dataSet.ExtendedProperties.ContainsKey("BATCH_ERROR"))
                {
                    dataSet.ExtendedProperties["BATCH_ERROR"] = GetErrorsString(dataSet);
                }
                if (throwException)
                {
                    throw new SunlikeException(GetErrorsString(dataSet));
                }
            }
            else
            {
                dataSet.AcceptChanges();
            }
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
            if (tableName == "INDX1" && statementType==StatementType.Delete)
            {
                int _iErrorFlag = this.CanDelete(dr["IDX1", DataRowVersion.Original].ToString());
                if (_iErrorFlag == 1)
                {
                    throw new SunlikeException("RCID=MON.HINT.INDX1_IDX_NOISUSED,TEXT=该费用项目已经使用，不能删除！");
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
            if (tableName == "INDX1")
            {
                Query _query = new Query();
                if (statementType == StatementType.Insert)
                {
                    // 更新删除表内容
                    _query.RunSql("DELETE FROM INDX1_DEL WHERE IDX1 = '" + dr["IDX1"].ToString() + "'");
                }
                else if (statementType == StatementType.Delete)
                {
                    // 更新删除表内容
                    _query.RunSql("DELETE FROM INDX1_DEL WHERE IDX1 = '" + dr["IDX1", DataRowVersion.Original].ToString() + "';INSERT INTO INDX1_DEL(IDX1) VALUES('" + dr["IDX1", DataRowVersion.Original].ToString() + "')");
                }
            }
		}
		#endregion

        #region 检查是否可以删除
        /// <summary>
        /// 检查是否可以删除
        /// </summary>
        /// <param name="idx1">费用代号</param>
        /// <returns>0:可以删除;1:不可以删除;</returns>
        public int CanDelete(string idx1)
        {
            DbIndx1 _dbIndx1 = new DbIndx1(Comp.Conn_DB);
            return _dbIndx1.CanDelete(idx1);
        }
        #endregion

		#region 检查费用项目是否存在
		/// <summary>
		/// 检查费用项目是否存在
		/// </summary>
		/// <param name="idx1">费用项目代号</param>
		/// <returns></returns>
		public bool IsExists(string idx1)
		{
			DbIndx1 _dbIndx1 = new DbIndx1(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbIndx1.GetData(idx1);
			if (_ds.Tables["INDX1"].Rows.Count > 0 )
			{
				return true;
			}
			return false;
		}
		#endregion 

		#region 取得费用项目
		/// <summary>
        /// GetData_Indx1
		/// </summary>
        /// <param name="sqlWhere"></param>
		/// <returns></returns>
        public SunlikeDataSet GetData_Indx1(string sqlWhere)
		{
			DbIndx1 _dbIndx1 = new DbIndx1(Comp.Conn_DB);
            return _dbIndx1.GetData_Indx1(sqlWhere);
		}
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="indx1"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string indx1)
        {
            DbIndx1 _dbIndx1 = new DbIndx1(Comp.Conn_DB);
            return _dbIndx1.GetData(indx1);
        }
		#endregion
	}
}
