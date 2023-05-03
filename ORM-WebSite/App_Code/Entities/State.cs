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
/// Summary description for State
/// </summary>
/// 
namespace ORM
{
    public class State : BaseState
    {
        public State()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public State(String stateid, String Statename, String flashcode, String description)
        {
            this._state_id = stateid;
            this._state_name = Statename;
            this._flashcode = flashcode;
            this._description = description;
        }

        public State(String stateid)
        {
            this._state_id = stateid;
            this._state_name = "";
            this._flashcode = "";
            this._description = "";
        }

        public static List<State> get_states(String Form_ID)
        {
            SQL_Connector connector = new SQL_Connector();
            String sql_query = "select locid , locname ,flashcode , [Description] from " +
                "Form_Dep_State , [State] where Form_Dep_State.state_ID = State.locid and form_id =" + Form_ID;
            DataTable dt = connector.Select(sql_query);
            var output = new List<State>();
            foreach (DataRow dr in dt.Rows)
            {
                State state = new State(dr["locid"].ToString(), dr["locname"].ToString(), dr["flashcode"].ToString(), dr["Description"].ToString());
                output.Add(state);
            }
            return output;
        }

        public static DataTable get_states()
        {
            SQL_Connector connector = new SQL_Connector();
            String sql_query = "select locid , locname ,flashcode , [description] from [state] where locid <>0";
            return connector.Select(sql_query);
        }


    }
}