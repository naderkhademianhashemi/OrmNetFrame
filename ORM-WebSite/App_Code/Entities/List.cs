using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

/// <summary>
/// Summary description for List
/// </summary>
/// 
namespace ORM
{
    public class List : BaseLst
    {
        public List()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List(String qid, String des, List<ORM.List_Items> listitems)
        {
            _question_id = qid;
            _description = des;
            _list_items = listitems;
        }

        public List(String qid)
        {
            SQL_Connector connector = new SQL_Connector();
            String sql_query = "SELECT [List_ID],[Question_ID],[Description] " +
                "FROM [List] where [Question_ID]=" + qid;
            DataTable dt = connector.Select(sql_query);
            _list_items = new List<ORM.List_Items>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    this._list_id = dr["List_ID"].ToString();
                    this._description = dr["Description"].ToString();
                    this._question_id = dr["Question_ID"].ToString();
                    DataTable dt_items = ORM.List_Items.get_Dt_Items(List_ID);
                    foreach (DataRow dr_item in dt_items.Rows)
                        List_Items.Add(new ORM.List_Items(dr_item["Item_ID"].ToString(),
                            dr_item["Item_Text"].ToString(), dr_item["List_ID"].ToString()));
                }
            }
        }

        public void save(String itemtext, Boolean optional, Int16 type, Boolean filled, Int32 tableid)
        {
            SQL_Connector connector = new SQL_Connector();
            string stableid = "null";
            if (filled) stableid = tableid.ToString();
            String sql_query = "INSERT INTO [List] ([Question_ID],[Description]) VALUES (" + this.Question_ID + " , N'" + this.Description + "')" +
                "DECLARE @local bigint Set @local = (SELECT IDENT_CURRENT('List'))" +
                "INSERT INTO [List_Items] ([Item_Text],[List_ID],[Optional],[Item_Type],[Filled],[Table_ID]) VALUES(N'" + itemtext + "', @local" + ",'" +
                optional + "'," + type + ",'" + filled + "'," + stableid + ")";
            connector.Execute(sql_query);
        }
        public void save(String itemtext)
        {
            SQL_Connector connector = new SQL_Connector();

            String sql_query = "INSERT INTO [List] ([Question_ID],[Description]) VALUES (" +
                this.Question_ID + " , N'" + this.Description + "')" +
                "DECLARE @local bigint Set @local = (SELECT IDENT_CURRENT('List'))" +
                "INSERT INTO [List_Items] ([Item_Text],[List_ID]) VALUES(N'" + itemtext + "', @local )";
            connector.Execute(sql_query);
        }
        public void Add(List_Items item)
        {
            List_Items.Add(item);
            item.Add();

        }

        public DataTable get_dt_items()
        {
            if (List_ID.Length == 0)
                return null;
            SQL_Connector connector = new SQL_Connector();
            String sql_query = "SELECT [Item_ID],[Item_Text],[List_ID],Item_ID,(ROW_NUMBER() OVER (ORDER BY [Item_ID])) as [ItemNum] " +
                " FROM [List_Items] where [List_ID]=" + List_ID + " order by [ItemNum]";
            return connector.Select(sql_query);
        }

        public static DataTable get_dt_items(String Qid)
        {
            var CN = new SQL_Connector();
            var Q = "SELECT A.[Item_ID],A.[Item_Text],A.[List_ID]," +
                "1 as [ItemNum] " +
                " FROM [Questions] C, [List] B, [List_Items] A " +
                "where C.question_id = B.question_id and B.List_id = A.list_id " +
                "and C.[question_id]=" + Qid;
            return CN.Select(Q);
        }


    }
}