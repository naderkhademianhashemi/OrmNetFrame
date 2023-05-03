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
/// Summary description for Department
/// </summary>
/// 
namespace ORM
{
    public class Department : BaseDptmnt
    {
        public Department()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Department(String depid, String depname, String description)
        {
            this._dep_id = depid;
            this._dep_name = depname;
            this._description = description;
        }

        public Department(String depid)
        {
            this._dep_id = depid;
            this._dep_name = "";
            this._description = "";
        }

        public static DataTable get_deps()
        {
            SQL_Connector connector = new SQL_Connector();
            String sql_query = "select depid , Depname , [Description] from department where depid <>0";
            return connector.Select(sql_query);
        }
        public static List<Department> get_deps(String Form_ID)
        {
            SQL_Connector connector = new SQL_Connector();
            String sql_query = "select depID , DepName , [Description] from " +
                "Form_Dep_State , Department where Form_Dep_State.Dep_ID = Department.DepID and form_id ="
                + Form_ID;
            DataTable dt = connector.Select(sql_query);
            var output = new List<Department>();
            foreach (DataRow dr in dt.Rows)
            {
                Department dep = new Department(dr["depID"].ToString(), (String)dr["DepName"], dr["Description"].ToString());
                output.Add(dep);
            }
            return output;
        }
    }
}