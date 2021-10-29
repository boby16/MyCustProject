using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using System.Data;
using System.Collections;

namespace Sunlike.Business
{
	/// <summary>
	/// ������Ŀ���Ͽ�
	/// </summary>
	public class Me_Role: Sunlike.Business.BizObject
	{
		#region ���캯��
		/// <summary>
		/// ���캯��
		/// </summary>
        public Me_Role()
		{
		}
		#endregion

        #region ����
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="throwException"></param>
        public void UpdateData(SunlikeDataSet dataSet, bool throwException)
        {
            DataTable _Me_Role = dataSet.Tables["ME_ROLE"];
            Hashtable _ht = new Hashtable();
            _ht["ME_ROLE"] = "ITM,IDX1,BDATE,EDATE,PRD_NO,IDX_NO,AREA_NO,CUS_NO,AMTN_ME,PRD_MARK";
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
			base.BeforeUpdate (tableName, statementType, dr, ref status);
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

		}
		#endregion

		#region ��������Ŀ�Ƿ����
		/// <summary>
		/// ��������Ŀ�Ƿ����
		/// </summary>
		/// <param name="idx1">������Ŀ����</param>
		/// <returns></returns>
		public bool IsExists(string idx1)
		{
			DbIndx1 _dbIndx1 = new DbIndx1(Comp.Conn_DB);
            string sqlWhere = "IDX1=" + idx1;
            SunlikeDataSet _ds = _dbIndx1.GetData(sqlWhere);
			if (_ds.Tables["INDX1"].Rows.Count > 0 )
			{
				return true;
			}
			return false;
		}
		#endregion 

        #region �ж��ظ�����
        /// <summary>
        /// �ж��Ƿ����ظ��Ĺ������,�����ظ������ITM��
		/// </summary>
		/// <param name="Ht"></param>
		/// <returns></returns>
        public string GetExistMeRoleItm(Dictionary<String, Object> Ht)
		{
            DbMe_Role _DbMeRole = new DbMe_Role(Comp.Conn_DB);
            return _DbMeRole.GetExistMeRoleItm(Ht);
		}
		#endregion

        #region ȡ��Ӫ�����ù���
        /// <summary>
        /// ȡ��Ӫ�����ù���
		/// </summary>
        /// <param name="sqlWhere"></param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string sqlWhere)
		{
            DbMe_Role _dbMe_Role = new DbMe_Role(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMe_Role.GetData(sqlWhere);
            //�趨����KeyItmΪ�Զ�����
            DataTable _dtBody = _ds.Tables["ME_ROLE"];
            _dtBody.Columns["ITM"].AutoIncrement = true;
            _dtBody.Columns["ITM"].AutoIncrementSeed = _dtBody.Rows.Count > 0 ? Convert.ToInt32(_dtBody.Select("", "ITM desc")[0]["ITM"]) + 1 : 1;
            _dtBody.Columns["ITM"].AutoIncrementStep = 1;
            _dtBody.Columns["ITM"].Unique = true;
            return _ds;
		}
		#endregion

        #region ȡ������
        /// <summary>
        /// ȡ������
        /// </summary>
        /// <returns></returns>
        public int GetMaxItm()
        {
            DbMe_Role _DbMeRole = new DbMe_Role(Comp.Conn_DB);
            return _DbMeRole.GetMaxItm();
        }
        #endregion
    }
}
