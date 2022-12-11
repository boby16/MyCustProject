using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
using System.Text;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbDRPTW.
	/// </summary>
	public class DbDRPTW : DbObject
	{
		#region ���캯��
		/// <summary>
		/// ����ӹ���
		/// </summary>
		/// <param name="connectionString">SQL�����ִ�</param>
		public DbDRPTW(string connectionString) : base(connectionString)
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region GetData
		/// <summary>
		/// GetData
		/// </summary>		
		/// <param name="twNo">����</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string twNo)
		{
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@TW_NO",SqlDbType.VarChar,20);
			_spc[0].Value = twNo;

			string _sql = "SELECT A.*,C.NAME SAL_NAME,D.NAME DEP_NAME,E.NAME CUS_NAME,F.NAME PRD_NAME,F.UT,G.NAME BIL_NAME FROM MF_TW A "
						+" LEFT JOIN SALM C ON A.SAL_NO=C.SAL_NO "
						+" LEFT JOIN DEPT D ON A.DEP=D.DEP "
						+" INNER JOIN CUST E ON A.CUS_NO=E.CUS_NO "
						+" INNER JOIN PRDT F ON A.MRP_NO=F.PRD_NO "
						+" LEFT JOIN BIL_SPC G ON G.BIL_ID='MO' AND G.SPC_ID='IB' AND A.BIL_TYPE=G.SPC_NO "
						+" WHERE A.TW_NO=@TW_NO; "

						+" SELECT A.*,B.NAME WH_NAME,C.SPC FROM TF_TW A "
						+" LEFT JOIN MY_WH B ON A.WH=B.WH "
						+" INNER JOIN PRDT C ON A.PRD_NO=C.PRD_NO "
						+" WHERE TW_NO=@TW_NO;";

			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[]{"MF_TW","TF_TW"},_spc);

			//�趨PK����Ϊ����left join�Ժ�PK��ȡ����
			DataColumn[] _dca = new DataColumn[1];			
			_dca[0] = _ds.Tables["MF_TW"].Columns["TW_NO"];
			_ds.Tables["MF_TW"].PrimaryKey = _dca;
			_dca = new DataColumn[2];			
			_dca[0] = _ds.Tables["TF_TW"].Columns["TW_NO"];
			_dca[1] = _ds.Tables["TF_TW"].Columns["ITM"];
			_ds.Tables["TF_TW"].PrimaryKey = _dca;
			//��ͷ�ͱ������
			DataColumn[] _dca1 = new DataColumn[1];			
			_dca1[0] = _ds.Tables["MF_TW"].Columns["TW_NO"];
			DataColumn[] _dca2 = new DataColumn[1];
			_dca2[0] = _ds.Tables["TF_TW"].Columns["TW_NO"];
			_ds.Relations.Add("MF_TWTF_TW",_dca1,_dca2);

			return _ds;
		}
		#endregion

		#region �õ��й������䷽
		/// <summary>
		/// 
		/// </summary>
		/// <param name="twNo">����</param>
		/// <returns></returns>
		public string GetIdNo(string twNo)
		{
			string _sql = "SELECT ID_NO FROM MF_TW WHERE TW_NO = @TW_NO";
			SqlParameter[] _aryPt = new SqlParameter[1];
			_aryPt[0] = new SqlParameter("@TW_NO", SqlDbType.VarChar, 20);
			_aryPt[0].Value = twNo;
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql, _ds, new string[1]{"MF_TW"}, _aryPt);
			if (_ds.Tables["MF_TW"].Rows.Count > 0)
			{
				return _ds.Tables["MF_TW"].Rows[0]["ID_NO"].ToString();
			}
			return "";
		}
		#endregion

		#region ��д�й���
		/// <summary>
		/// ��д�й���
		/// </summary>
		/// <param name="twNo">����</param>
		/// <param name="qtyRk">�������</param>
		public void SetFromTi(string twNo, decimal qtyRk)
		{
			string _sql = "UPDATE MF_TW SET QTY_RK = ISNULL(QTY_RK,0) + @QTY_RK WHERE TW_NO = @TW_NO";
			SqlParameter[] _aryPt = new SqlParameter[2];
			_aryPt[0] = new SqlParameter("@TW_NO", SqlDbType.VarChar, 20);
			_aryPt[0].Value = twNo;
			_aryPt[1] = new SqlParameter("@QTY_RK", SqlDbType.Decimal);
			_aryPt[1].Precision = 28;
			_aryPt[1].Scale = 8;
			_aryPt[1].Value = qtyRk;
			this.ExecuteNonQuery(_sql, _aryPt);
		}
		#endregion

		#region �õ���ȷ���й���
		/// <summary>
		/// �õ���ȷ���й���
		/// </summary>
		/// <param name="usr">ȷ����</param>
		/// <returns></returns>
		public int GetScmNum(string usr)
		{
			int _scm = 0;
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@CUS_NO",SqlDbType.VarChar,12);
			_spc[0].Value = usr;

			string _sql = " SELECT COUNT(*) FROM MF_TW A WITH (NOLOCK)"
						+" INNER JOIN TF_TW B ON A.TW_NO=B.TW_NO "
						+" WHERE A.CUS_NO=@CUS_NO AND ISNULL(A.SCM_USR,'')='' AND ISNULL(B.QTY_TS,0)=0 AND ISNULL(B.QTY_RTN,0)=0 "
						+" AND ISNULL(A.QTY_RTN,0)=0";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[]{"TF_TW"},_spc);
			if(_ds.Tables.Contains("TF_TW"))
			{
				if(_ds.Tables["TF_TW"].Rows.Count > 0)
				{
					_scm = Convert.ToInt32(_ds.Tables["TF_TW"].Rows[0][0]);
				}
			}
			return _scm;
		}
		#endregion

		#region  ȷ��/��ȷ���й���
		/// <summary>
		/// ȷ��/��ȷ���й���
		/// </summary>		
		/// <param name="twNoAry">�й�����</param>
		/// <param name="usr">ȷ����</param>
		/// <param name="scm">ȷ�Ϸ�</param>
		/// <returns></returns>
		public int UpdateTwScm(string[] twNoAry,string usr,bool scm)
		{
			string _sql = "UPDATE MF_TW SET SCM_USR=@SCM_USR,SCM_DD=@SCM_DD ";			
			if(!scm)
			{			
				_sql = "UPDATE MF_TW SET SCM_USR=null,SCM_DD=null ";
			}
			SqlParameter[] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@SCM_USR",SqlDbType.VarChar,12);
			_spc[0].Value = usr;
			_spc[1] = new SqlParameter("@SCM_DD",SqlDbType.DateTime);
			_spc[1].Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			
			StringBuilder _sqlWhere = new StringBuilder(" WHERE (1<>1");
			
			if(twNoAry.Length > 0)
			{				
				for(int i = 0;i < twNoAry.Length;i++)
				{					
					_sqlWhere.Append(" OR (TW_NO='"+twNoAry[i]+"')");
				}				
			}
			_sqlWhere.Append(")");
			_sql += _sqlWhere.ToString();
			return this.ExecuteNonQuery(_sql,_spc);
		}
		#endregion
	}
}