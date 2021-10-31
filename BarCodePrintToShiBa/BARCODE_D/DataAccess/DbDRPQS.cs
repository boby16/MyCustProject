using System;
using System.Data;
using System.Data.SqlClient;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// 询价单
	/// </summary>
	public class DbDRPQS : DbObject
	{
		#region 构造函数
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="connectionString"></param>
		public DbDRPQS(string connectionString) : base(connectionString)
		{
		}
		#endregion

		#region 修改询价单的采购单号
		/// <summary>
		///	 修改询价单的采购单号
		/// </summary>
		/// <param name="qtId">单据别</param>
		/// <param name="qtNo">询价单号</param>
		/// <param name="osNo">转入单号</param>
		public void UpdateOsNo(string qtId,string qtNo,string osNo)
		{
			string _sqlStr = "";
			if (osNo.Length > 0 )
			{
				_sqlStr = "UPDATE MF_QTS SET OS_NO=@OS_NO WHERE QT_ID=@QT_ID AND QT_NO=@QT_NO AND ISNULL(OS_NO,'')=''";
			}
			else
			{
				_sqlStr = "UPDATE MF_QTS SET OS_NO=@OS_NO WHERE QT_ID=@QT_ID AND QT_NO=@QT_NO";
			}
			SqlParameter[] _sqlPara = new SqlParameter[3];
			_sqlPara[0] = new SqlParameter("@QT_ID",SqlDbType.VarChar,2);
			_sqlPara[0].Value = qtId;
			_sqlPara[1] = new SqlParameter("@QT_NO",SqlDbType.VarChar,20);
			_sqlPara[1].Value = qtNo;
			_sqlPara[2] = new SqlParameter("@OS_NO",SqlDbType.VarChar,20);
			_sqlPara[2].Value = osNo;
			this.ExecuteNonQuery(_sqlStr,_sqlPara);

		}

		#endregion

		#region 修改询价单的已交请购量

		/// <summary>
		///	 修改询价单的已交请购量
		/// </summary>
		/// <param name="qtId">单据别</param>
		/// <param name="qtNo">询价单号</param>
		/// <param name="itm">项次</param>
		/// <param name="qtyPo">已交请购量</param>
		public void UpdateQtyPo(string qtId,string qtNo,int itm,decimal qtyPo)
		{
			string _sql = "	UPDATE TF_QTS SET QTY_PO=ISNULL(QTY_PO,0)+@QTY WHERE QT_ID=@QT_ID AND QT_NO=@QT_NO AND OTH_ITM=@ITM ";
			SqlParameter[] _sqlPara = new SqlParameter[4];
			_sqlPara[0] = new SqlParameter("@QT_ID",SqlDbType.VarChar,2);
			_sqlPara[0].Value = qtId;
			_sqlPara[1] = new SqlParameter("@QT_NO",SqlDbType.VarChar,20);
			_sqlPara[1].Value = qtNo;
			_sqlPara[2] = new SqlParameter("@ITM",SqlDbType.Int);
			_sqlPara[2].Value = itm;
			_sqlPara[3] = new SqlParameter("@QTY",SqlDbType.Decimal);
			_sqlPara[3].Precision = 38;
			_sqlPara[3].Scale = 10;
			_sqlPara[3].Value = qtyPo;
            this.ExecuteNonQuery(_sql, _sqlPara);
		}
		#endregion
	}
}
