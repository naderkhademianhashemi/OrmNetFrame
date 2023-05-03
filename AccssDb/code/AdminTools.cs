using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccssDb.code
{
    public static class AdminTools
    {
        public static string GetCnStr()
        {
            return RSRC.Setting.PrvdrAce + RSRC.Setting.DataSource;
        }
        public static string GetInfoByTable(string fieldName, string tbl)
        {
            string strRet = "";
            string Q = "Select * From " + tbl;
            using (var CN = new OleDbConnection())
            {
                CN.ConnectionString = GetCnStr();
                CN.Open();
                var cmd = new OleDbCommand()
                {
                    CommandText = Q,
                    Connection = CN
                };
                var RD = cmd.ExecuteReader();
                if (RD.Read() && !DBNull.Value.Equals(fieldName))
                    strRet = RD[fieldName].ToString();
                RD.Close();
                cmd.Dispose();
                CN.Close();
                return strRet;
            }
        }
        public static string GetSettingByField(string FieldName)
        {
            string query = "Select * From tblSetting Where sID = 1";
            string strRet = "";
            using (var CN = new OleDbConnection())
            {
                CN.ConnectionString = GetCnStr();
                CN.Open();
                var cmd = new OleDbCommand()
                {
                    CommandText = query,
                    Connection = CN
                };
                var RD = cmd.ExecuteReader();
                if (RD.Read() && !DBNull.Value.Equals(FieldName))
                    strRet = RD[FieldName].ToString();
                RD.Close();
                cmd.Dispose();
                CN.Close();
                return strRet;
            }
        }
    }
}


