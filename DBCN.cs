using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN
{
    internal class DBCN
    {
        // 需要屬性 函式 建構子 變數
        public string ssms1 { get; set; } // 屬性 public(不隱藏)
        public string ssms;
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataAdapter adapter;
        // 建構子需與類別同名稱，否則被辨識為函式
        public DBCN(string sqlcn)
        {
            ssms = sqlcn;
            conn = new SqlConnection(ssms);
            cmd = new SqlCommand();
            adapter = new SqlDataAdapter();
            cmd.Connection = conn;
            adapter.SelectCommand = cmd; // 取得 cmd 的 T-SQL 或 Stored Procedures
        }
        public DBCN()
        {
        }
        public DataTable SQL(string sql, Dictionary<string, string>? Parameters = null)
        {
            cmd.CommandText = sql;
            if (Parameters != null)
            {
                foreach (KeyValuePair<string, string> item in Parameters) // Parameters 傳回參數
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }
            }
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            //adapter.Fill(dt);
            dt.Load(dr);
            cmd.Connection.Close();
            return dt;
        }
        // 訂單
        public void Insert(string sql) 
        {
            cmd.CommandText = sql;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        // 取得單號流水號 參數(O || C...)
        public string? ItemNO(string NOTYPE) 
        {
            string Result = "";
            string sqltable = "";
            string ID = "";
            if (NOTYPE == "O") {
                sqltable = "ORDERHEADER";
                ID = "OrderId";
            } else if (NOTYPE == "C") {
                sqltable = "CARTHEATER";
                ID = "CartId";
            } else if (NOTYPE == "P") {
                sqltable = "PRODUCT";
                ID = "ProductID";
            }
            string sql = $"SELECT SUBSTRING(MAX({ID}),2,10) FROM {sqltable}";
            cmd.CommandText= sql;
            cmd.Connection.Open();
            Result = cmd.ExecuteScalar().ToString();
            try { 
                // 如果為當年度
                if (Result.Substring(0, 2) == DateTime.Today.ToString("yyyy").Substring(2, 2))
                {
                    Result = Result.Substring(6,4);
                    Result = ( Convert.ToInt32(Result) + 1).ToString();
                    Result = Result.PadLeft(4,'0'); // 補成4碼字串
                    Result = $"{NOTYPE}" + (DateTime.Today.ToString("yyyy")).Substring(2) + DateTime.Today.ToString("MM") + DateTime.Today.ToString("dd") + Result;
                }
                else
                {
                    Result = $"{NOTYPE}" + (DateTime.Today.ToString("yyyy")).Substring(2) + DateTime.Today.ToString("MM") + DateTime.Today.ToString("dd") + "0001";
                }
            }
            catch 
            {
                Result = $"{NOTYPE}" + (DateTime.Today.ToString("yyyy")).Substring(2) + DateTime.Today.ToString("MM") + DateTime.Today.ToString("dd") + "0001";
            }
            finally { cmd.Connection.Close(); }
            return Result;
        }


    }
}
