using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for Groups_Permission
/// </summary>
/// 
namespace ORM
{
    public class Group_Permissions : BaseGrpPrm
    {
        
        public Group_Permissions()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Group_Permissions(String form_id, String group_id, Boolean readable,
             Boolean writable, Boolean updatable, Boolean deletable,
             bool addQuestion, bool ReadOtherAnswers)
        {
            _form_id = form_id;
            _group_id = group_id;
            _readable = readable;
            _writable = writable;
            _updatable = updatable;
            _deleteable = deletable;
            _addQuestion = addQuestion;
            _ReadOtherAnswers = ReadOtherAnswers;
        }

        public Group_Permissions(String form_id, String group_id)
        {
            _form_id = form_id;
            _group_id = group_id;
            String sql_query = "SELECT [Form_ID],[Group_ID],[Readable],[Writeable],[Updateable],[Deleteable] ,[AddQuestion],ReadOtherAnswers " +
                "FROM [Group_Permissions] where [Form_ID]=" + form_id + " and [Group_ID]=" + group_id;
            SQL_Connector connector = new SQL_Connector();
            DataTable dt = connector.Select(sql_query);

            if (dt != null)
                foreach (DataRow dr in dt.Rows)
                {
                    _readable = Convert.ToBoolean(dr["Readable"].ToString());
                    _writable = Convert.ToBoolean(dr["Writeable"].ToString());
                    _updatable = Convert.ToBoolean(dr["Updateable"].ToString());
                    _deleteable = Convert.ToBoolean(dr["Deleteable"].ToString());
                    _addQuestion = Convert.ToBoolean(dr["AddQuestion"].ToString());
                    _ReadOtherAnswers = Convert.ToBoolean(dr["ReadOtherAnswers"].ToString());
                }

        }



        public static Group_Permissions Get_Permission(String formid, String User_Name)
        {
            SQL_Connector connector = new SQL_Connector();
            string sql_query;
            sql_query = "select Forms.Form_ID AS [FormID], Forms.Form_Name as [FormName] , Forms.Form_Description as [Description] ," +
             "t1.readable , t1.Writeable  , t1.Updateable , t1.Deleteable,t1.AddQuestion,t1.ReadOtherAnswers from (" +
                "select group_permissions.form_id " +
           ",convert(bit,1) as readable " +
           ",convert(bit,1) as Writeable " +
           ",convert(bit,1) as Updateable " +
           ",convert(bit,1) as Deleteable " +
             ",convert(bit,1) as AddQuestion " +
             ",convert(bit,1) as ReadOtherAnswers " +
           "from groups , group_permissions , Group_Users where " +
           "groups.group_id = group_permissions.group_id and  groups.group_id = Group_Users.group_id " +
           "and LTRIM(User_Name)  =N'" + User_Name + "' group by (group_permissions.form_id) " + ") as t1 , Forms " +
           "where forms.form_id = t1.form_id and t1.form_id =" + formid;
            DataTable dt = connector.Select(sql_query);
            if (dt != null && dt.Rows.Count > 0)
            {
                return new Group_Permissions(
                    formid, "",
                    Convert.ToBoolean(dt.Rows[0]["readable"].ToString()),
                   Convert.ToBoolean(dt.Rows[0]["Writeable"].ToString()),
                   Convert.ToBoolean(dt.Rows[0]["Updateable"].ToString()),
                   Convert.ToBoolean(dt.Rows[0]["Deleteable"].ToString()),
                   Convert.ToBoolean(dt.Rows[0]["AddQuestion"].ToString()),
                   Convert.ToBoolean(dt.Rows[0]["ReadOtherAnswers"].ToString()));
            }
            else
            {
                return null;
            }
        }
        public static Group_Permissions get_Permission_old(String formid, String User_Name)
        {
            SQL_Connector connector = new SQL_Connector();

            String sql_query = "select Forms.Form_ID AS [FormID], Forms.Form_Name as [FormName] , Forms.Form_Description as [Description] ," +
                 "t1.readable , t1.Writeable  , t1.Updateable , t1.Deleteable,t1.AddQuestion,t1.ReadOtherAnswers from (" +
                 "select group_permissions.form_id " +
                 ",convert(bit,max(convert(smallint,group_permissions.readable))) as readable " +
                 ",convert(bit,max(convert(smallint,group_permissions.Writeable)))as Writeable " +
                 ",convert(bit,max(convert(smallint,group_permissions.Updateable)))as Updateable " +
                 ",convert(bit,max(convert(smallint,group_permissions.Deleteable)))as Deleteable " +
                 ",convert(bit,max(convert(smallint,group_permissions.AddQuestion)))as AddQuestion " +
                 ",convert(bit,max(convert(smallint,group_permissions.ReadOtherAnswers)))as ReadOtherAnswers " +
                 "from groups , group_permissions , Group_Users where " +
                 "groups.group_id = group_permissions.group_id and  groups.group_id = Group_Users.group_id " +
                 "and [User_Name] =N'" + User_Name + "' group by (group_permissions.form_id) " + ") as t1 , Forms " +
                 "where forms.form_id = t1.form_id and t1.form_id =" + formid;

            DataTable dt = connector.Select(sql_query);

            if (dt != null && dt.Rows.Count > 0) return new Group_Permissions(
                formid, "",
                Convert.ToBoolean(dt.Rows[0]["readable"].ToString()),
               Convert.ToBoolean(dt.Rows[0]["Writeable"].ToString()),
               Convert.ToBoolean(dt.Rows[0]["Updateable"].ToString()),
               Convert.ToBoolean(dt.Rows[0]["Deleteable"].ToString()),
               Convert.ToBoolean(dt.Rows[0]["AddQuestion"].ToString()),
               Convert.ToBoolean(dt.Rows[0]["ReadOtherAnswers"].ToString()));
            else
                return null;


        }



    }
}