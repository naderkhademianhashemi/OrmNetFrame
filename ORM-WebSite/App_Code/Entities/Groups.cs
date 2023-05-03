using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Data.SqlClient;
using System.Collections.Generic;

/// <summary>
/// Summary description for Groups
/// </summary>
/// 
namespace ORM
{
    public class Groups : BaseGrp
    {
        public Groups()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Groups(String group_id)
        {
            _group_permissions = new List<Group_Permissions>();
            _group_users = new List<string>();
            _group_id = group_id;

            SQL_Connector connector = new SQL_Connector();

            String sql_query = "SELECT [Group_ID],[Group_Name],[Description] FROM [Groups] " +
                "where [Group_ID]=" + group_id;

            DataTable dt = connector.Select(sql_query);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    _group_name = dr["Group_Name"].ToString();
                    _group_description = dr["group_id"].ToString();
                }
            }

            sql_query = "SELECT [Form_ID] FROM [Group_Permissions] where [Group_ID]=" + group_id;
            dt = connector.Select(sql_query);

            if (dt != null)
                foreach (DataRow dr in dt.Rows)
                {
                    _group_permissions.Add(new ORM.Group_Permissions(dr["Form_ID"].ToString(), group_id));
                }

            sql_query = "SELECT [User_Name] FROM [Group_Users] where [Group_ID]=" + group_id;
            dt = connector.Select(sql_query);
            if (dt != null)
                foreach (DataRow dr in dt.Rows)
                {
                    _group_users.Add(dr["User_Name"].ToString().ToLower());
                }

        }

        public void Save_Users()
        {
            String sql_query = "DELETE FROM [Group_Users] WHERE [Group_ID]=" + _group_id + " ";
            SqlCommand com = new SqlCommand();

            foreach (String str in _group_users)
                sql_query += "INSERT INTO [Group_Users] ([Group_ID],[User_Name]) VALUES (" + _group_id + ",N'" + str + "') ";
            SQL_Connector connector = new SQL_Connector();
            connector.Transaction(sql_query);

        }

        public void Save_Permission_old()
        {
            String sql_query = "DELETE FROM [Group_Permissions] WHERE [Group_ID]=" + _group_id + " ";
            SqlCommand com = new SqlCommand();

            foreach (object obj in Group_Permissions)
            {
                ORM.Group_Permissions permisson = (ORM.Group_Permissions)obj;
                sql_query += "INSERT INTO [Group_Permissions] ([Form_ID],[Group_ID],[Readable],[Writeable],[Updateable],[Deleteable]) VALUES (" +
                    permisson._form_id + "," + permisson._group_id + "," + Convert.ToByte(permisson._readable) +
                    "," + Convert.ToByte(permisson._writable) + "," + Convert.ToByte(permisson._updatable) +
                    "," + Convert.ToByte(permisson._deleteable) + ")";
            }

            SQL_Connector connector = new SQL_Connector();
            connector.Transaction(sql_query);
        }

        public void Save_Permission()
        {
            String sql_query = "DELETE FROM [Group_Permissions] WHERE [Group_ID]=" + _group_id + " ";
            SqlCommand com = new SqlCommand();

            foreach (object obj in Group_Permissions)
            {
                ORM.Group_Permissions permisson = (ORM.Group_Permissions)obj;
                sql_query += "INSERT INTO [Group_Permissions] ([Form_ID],[Group_ID],[Readable],[Writeable],[Updateable],[Deleteable],AddQuestion,ReadOtherAnswers) VALUES (" +
                    permisson._form_id + "," + permisson._group_id + "," + Convert.ToByte(permisson._readable) +
                    "," + Convert.ToByte(permisson._writable) + "," + Convert.ToByte(permisson._updatable) + ","
                    + Convert.ToByte(permisson._deleteable) + ","
                    + Convert.ToByte(permisson._addQuestion) + ","
                     + Convert.ToByte(permisson._ReadOtherAnswers) + ")";
            }

            SQL_Connector connector = new SQL_Connector();
            connector.Transaction(sql_query);
        }
        public Boolean Have_User(String User)
        {
            foreach (String str in Group_Users)
                if (str.Equals(User))
                    return true;
            return false;
        }

        public Group_Permissions Get_Permissions(String form_id)
        {
            foreach (object obj in Group_Permissions)
            {
                ORM.Group_Permissions permisson = (ORM.Group_Permissions)obj;
                if (permisson._form_id.Equals(form_id))
                    return permisson;
            }
            return new Group_Permissions();
        }

    }
}