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
using System.Data.SqlClient;
using System.Collections.Generic;

/// <summary>
/// Summary description for ORM_Forms
/// </summary>
/// 
namespace ORM
{
    public class Forms : BaseOrmFrm
    {
        public Forms(String Form_Name, String Form_Description,
            List<Department> dep_related,
            List<State> state_related,
            List<Question> form_question)
        {
            this.Form_ID = "";
            this._form_Name = Form_Name;
            this._form_Description = Form_Description;
            this._dep_related = dep_related;
            this._state_related = state_related;
            this._form_question = form_question;
        }
        public Forms()
        {

        }

        public Forms(String formid)
        {
            SQL_Connector connector = new SQL_Connector();
            String SQL_query = "SELECT Forms.Form_ID AS [FormID], " +
                "Forms.Form_Name as [FormName] , " +
                "Forms.Form_Description as [Description] " +
                " from forms where Forms.Form_id =" + formid;
            DataTable dt = connector.Select(SQL_query);
            foreach (DataRow dr in dt.Rows)
            {
                this._form_ID = (String)dr["FormID"].ToString();
                this._form_Name = (String)dr["FormName"];
                this._form_Description = (String)dr["description"];
                this._dep_related = Department.get_deps(formid);
                this._state_related = State.get_states(formid);
                this._form_question = Question.Get_Form_Questions(formid);
            }

        }
        public String Get_Answer_query()
        {
            String lastID = "";
            String part1 = " Select ";
            String part2 = " from ";
            String part3 = " where  1=1 ";

            foreach (object obj in this.Form_Question)
            {
                Question local_question = (Question)obj;
                if (lastID == "")
                {
                    part1 += "Q" + local_question.Question_ID + ".[Instance_ID] as [Instance_ID] ,";
                }
                if (lastID != "")
                {
                    part1 += " , ";
                    part2 += " , ";
                    part3 += " AND " + lastID + ".[Instance_ID] =" + "Q" + local_question.Question_ID + ".[Instance_ID] ";
                }
                switch (local_question.Question_Type)
                {
                    case ORM_Types.Question_Type.Text:
                        part1 += "Q" + local_question.Question_ID + ".[Text] as C" + local_question.Question_ID + " ";
                        part2 += "(SELECT [Text] , Instance_ID FROM get_answer_by_date(@from_date , @to_date) " +
                            "WHERE form_id =" + local_question.Form_ID +
                            "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        break;
                    case ORM_Types.Question_Type.Number:
                        part1 += "Q" + local_question.Question_ID + ".[Number] as C" + local_question.Question_ID + " ";
                        part2 += "(SELECT [Number] , Instance_ID FROM get_answer_by_date(@from_date , @to_date) " +
                            "WHERE form_id =" + local_question.Form_ID +
                            "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        break;
                    case ORM_Types.Question_Type.Date:
                        part1 += "Q" + local_question.Question_ID + ".[Date] as C" + local_question.Question_ID + " ";
                        part2 += "(SELECT [Date] , Instance_ID FROM get_answer_by_date(@from_date , @to_date) " +
                            "WHERE form_id =" + local_question.Form_ID +
                            "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        break;
                    case ORM_Types.Question_Type.DropDownList:
                        if (local_question.Template_Type == ORM_Types.Template_Type.Branch)
                        {
                            part1 += "Q" + local_question.Question_ID + ".[Branch_item] as C" + local_question.Question_ID + " ";
                            part2 += "(SELECT [Branch_item] , Instance_ID FROM get_answer_by_date(@from_date , @to_date) " +
                                "WHERE form_id =" + local_question.Form_ID +
                                "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        }
                        if (local_question.Template_Type == ORM_Types.Template_Type.Department)
                        {
                            part1 += "Q" + local_question.Question_ID + ".[Dep_item] as C" + local_question.Question_ID + " ";
                            part2 += "(SELECT [Dep_item] , Instance_ID FROM get_answer_by_date(@from_date , @to_date) " +
                                "WHERE form_id =" + local_question.Form_ID +
                                "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        }
                        if (local_question.Template_Type == ORM_Types.Template_Type.State)
                        {
                            part1 += "Q" + local_question.Question_ID + ".[State_item] as C" + local_question.Question_ID + " ";
                            part2 += "(SELECT [State_item] , Instance_ID FROM get_answer_by_date(@from_date , @to_date) " +
                                "WHERE form_id =" + local_question.Form_ID +
                                "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        }
                        if (local_question.Template_Type == ORM_Types.Template_Type.Manual)
                        {
                            part1 += "Q" + local_question.Question_ID + ".[Selected_item] as C" + local_question.Question_ID + " ";
                            part2 += "(SELECT [Selected_item] , Instance_ID FROM get_answer_by_date(@from_date , @to_date) " +
                                "WHERE form_id =" + local_question.Form_ID +
                                "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        }
                        break;
                    case ORM_Types.Question_Type.RadioButtonList:
                        if (local_question.Template_Type == ORM_Types.Template_Type.Branch)
                        {
                            part1 += "Q" + local_question.Question_ID + ".[Branch_item] as C" + local_question.Question_ID + " ";
                            part2 += "(SELECT [Branch_item] , Instance_ID FROM get_answer_by_date(@from_date , @to_date) " +
                                " WHERE form_id =" + local_question.Form_ID +
                                "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        }
                        if (local_question.Template_Type == ORM_Types.Template_Type.Department)
                        {
                            part1 += "Q" + local_question.Question_ID + ".[Dep_item] as C" + local_question.Question_ID + " ";
                            part2 += "(SELECT [Dep_item] , Instance_ID FROM get_answer_by_date(@from_date , @to_date)" +
                                " WHERE form_id =" + local_question.Form_ID +
                                "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        }
                        if (local_question.Template_Type == ORM_Types.Template_Type.State)
                        {
                            part1 += "Q" + local_question.Question_ID + ".[State_item] as C" + local_question.Question_ID + " ";
                            part2 += "(SELECT [State_item] , Instance_ID FROM get_answer_by_date(@from_date , @to_date) " +
                                " WHERE form_id =" + local_question.Form_ID +
                                "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        }
                        if (local_question.Template_Type == ORM_Types.Template_Type.Manual)
                        {
                            part1 += "Q" + local_question.Question_ID + ".[Selected_item] as C" + local_question.Question_ID + " ";
                            part2 += "(SELECT [Selected_item] , Instance_ID FROM get_answer_by_date(@from_date , @to_date) " +
                                " WHERE form_id =" + local_question.Form_ID +
                                "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        }
                        break;
                    case ORM_Types.Question_Type.ComboBoxList:
                        if (local_question.Template_Type == ORM_Types.Template_Type.Branch)
                        {
                            part1 += "Q" + local_question.Question_ID + ".[Branch_item] as C" + local_question.Question_ID + " ";
                            part2 += "(SELECT [Branch_item] , Instance_ID FROM get_answer_by_date(@from_date , @to_date) " +
                                " WHERE form_id =" + local_question.Form_ID +
                                "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        }
                        if (local_question.Template_Type == ORM_Types.Template_Type.Department)
                        {
                            part1 += "Q" + local_question.Question_ID + ".[Dep_item] as C" + local_question.Question_ID + " ";
                            part2 += "(SELECT [Dep_item] , Instance_ID FROM get_answer_by_date(@from_date , @to_date) " +
                                " WHERE form_id =" + local_question.Form_ID +
                                "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        }
                        if (local_question.Template_Type == ORM_Types.Template_Type.State)
                        {
                            part1 += "Q" + local_question.Question_ID + ".[State_item] as C" + local_question.Question_ID + " ";
                            part2 += "(SELECT [State_item] , Instance_ID FROM get_answer_by_date(@from_date , @to_date) " +
                                " WHERE form_id =" + local_question.Form_ID +
                                "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        }

                        if (local_question.Template_Type == ORM_Types.Template_Type.Manual)
                        {
                            part1 += "Q" + local_question.Question_ID + ".[Selected_item] as C" + local_question.Question_ID + " ";
                            part2 += "(SELECT [Selected_item] , Instance_ID FROM get_answer_by_date(@from_date , @to_date) " +
                                " WHERE form_id =" + local_question.Form_ID +
                                "and (Question_ID =" + local_question.Question_ID + ")) as Q" + local_question.Question_ID;
                        }
                        break;
                }
                lastID = "Q" + local_question.Question_ID;

            }

            return part1 + part2 + part3;
        }
        public String Save()
        {
            var connector = new SQL_Connector();
            var Sql_query = "set transaction isolation level SERIALIZABLE " +
                "Begin transaction formtran " +
                "INSERT INTO [Forms] ([Form_Name],[Form_Description]) " +
                "VALUES (N'" + this.Form_Name + "' , N'" + this.Form_Description + "') " +
                "if @@Error<>0 begin rollback transaction return end " +
                "DECLARE @local int " +
                "Set @local = (SELECT IDENT_CURRENT('Forms')) ";
            foreach (object obj_dep in this.Dep_Related)
            {
                Department dep = (Department)obj_dep;
                foreach (object obj_state in this.State_Related)
                {
                    State state = (State)obj_state;
                    Sql_query += "INSERT INTO [Form_Dep_State] ([Form_ID],[Dep_ID],[State_ID]) VALUES (@local," + dep.Dep_ID + "," + state.State_ID + ") " +
                        "if @@Error<>0 begin rollback transaction return end ";
                }
            }
            Sql_query += "select @local ";
            Sql_query += "Commit transaction formtran";

            DataTable dt = connector.Select(Sql_query);
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
            {
                Logger.Info("INSERT [Forms]", "Orm_Forms.cs");
                return "";
            }
        }
        public static bool Isvalid(String formname)
        {
            var connector = new SQL_Connector();
            var Sql_query = "select count(*) from Forms where Form_Name = N'" + formname + "'";
            var dt = connector.Select(Sql_query);
            if (dt != null && dt.Rows[0][0].ToString() != "0")
                return true;
            else
                return false;
        }
        public Question get_question(String Qid)
        {
            foreach (object obj in this._form_question)
            {
                Question que = (Question)obj;
                if (que.Question_ID == Qid)
                    return que;
            }
            return null;
        }

        public void delete_question(String qid)
        {
            Question temp = this.get_question(qid);
            temp.delete_question();

        }

        public static void Swap_Qestion(String Qid_first, String Qid_second, String formid)
        {
            Question first_question = new Question(Qid_first, formid);
            Question second_question = new Question(Qid_second, formid);

            int temp = first_question.Question_Index;
            first_question.Question_Index = second_question.Question_Index;
            second_question.Question_Index = temp;

            first_question.update();
            second_question.update();
        }


        public static DataTable get_Forms_By_Permission(String User_Name)
        {
            SQL_Connector CN = new SQL_Connector();
            var SQL = "";
            var userIsAdmin = false;
            if (userIsAdmin)
            {
                //static Update1 Delete1 ReadOtherAnswer1 Question1
                SQL = @"select Forms.Form_ID AS [FormID], Forms.Form_Name as [FormName] ,
                        Forms.Form_Description as [Description] ,
                        t1.readable , t1.Writeable  , t1.Updateable ,
                        t1.Deleteable , t1.AddQuestion , t1.ReadOtherAnswers
                        from (select group_permissions.form_id ,convert(bit,1) as readable ,
                        convert(bit,1)as Writeable ,convert(bit,1)as Updateable ,
                        convert(bit,1)as Deleteable ,convert(bit,1) as  AddQuestion,
                        convert(bit,1) as ReadOtherAnswers from groups ,
                        group_permissions , Group_Users 
                        where groups.group_id = group_permissions.group_id 
                        and  groups.group_id = Group_Users.group_id 
                        and LTRIM(User_Name) =N'" + User_Name + "'" +
                        @"group by (group_permissions.form_id) ) as t1 , 
                        Forms where forms.form_id = t1.form_id";
            }
            else
            {
                SQL = @"select Forms.Form_ID AS [FormID], Forms.Form_Name as [FormName] ,
                    Forms.Form_Description as [Description] ,
                    t1.readable , t1.Writeable  , t1.Updateable ,
                    t1.Deleteable , t1.AddQuestion , t1.ReadOtherAnswers 
                    from (select group_permissions.form_id ,
                    convert(bit,max(convert(smallint,group_permissions.readable))) as readable ,
                    convert(bit,max(convert(smallint,group_permissions.Writeable)))as Writeable ,
                    convert(bit,max(convert(smallint,group_permissions.Updateable)))as Updateable ,
                    convert(bit,max(convert(smallint,group_permissions.Deleteable)))as Deleteable ,
                    convert(bit,max(convert(smallint,group_permissions.AddQuestion)))as  AddQuestion,
                    convert(bit,max(convert(smallint,group_permissions.ReadOtherAnswers)))as ReadOtherAnswers
                    from groups , group_permissions , Group_Users where groups.group_id = group_permissions.group_id and
                    groups.group_id = Group_Users.group_id and LTRIM(User_Name)  =N'" + User_Name + "'" +
                  @"group by (group_permissions.form_id) ) as t1 , 
                    Forms where forms.form_id = t1.form_id";
            }
            return CN.Select(SQL);
        }
        public static void delete(String fid)
        {
            SQL_Connector connector = new SQL_Connector();
            String Sql_query = "Delete From [Forms] where Form_ID =" + fid;
            connector.Execute(Sql_query);
        }
        public static DataTable Get_Dt_Related_Parameter(String FID)
        {
            String sql_query = "Select [Prameter_Name],[Description],[Is_Time_Sensitive],[Is_Loc_Sensitive], " +
                "[Sql_Query],[Parameter_Type] From [Parameter] where Sql_Query like'%form_id =" + FID + "%' ";
            SQL_Connector connector = new SQL_Connector();
            return connector.Select(sql_query);
        }
        public static DataTable get_forms(String form_name, String dep_id, String state_id)
        {
            SQL_Connector connector = new SQL_Connector();
            String SQL_query = "SELECT Forms.Form_ID AS [FormID], Forms.Form_Name as [FormName] , Forms.Form_Description as [Description]" +
                "FROM " +
                "Forms Left Outer JOIN " +
                "Form_Dep_State ON Forms.Form_ID = Form_Dep_State.Form_ID INNER JOIN " +
                "State ON Form_Dep_State.State_ID = State.locid INNER JOIN " +
                "Department ON Form_Dep_State.Dep_ID = Department.DepID where 1=1 ";


            if (form_name != "") SQL_query += "AND Forms.Form_Name LIKE N'%" + form_name + "%'";

            if (state_id != "") SQL_query += "AND State.locid = " + state_id + "OR State.locid=0";

            if (dep_id != "") SQL_query += "AND Department.DepID = " + dep_id + "OR Department.DepID=0 ";
            //Bug_fix:Iman
            SQL_query += " group by Forms.Form_ID, Forms.Form_Name , Forms.Form_Description";
            return connector.Select(SQL_query);
        }
        public void Update()
        {
            SQL_Connector connector = new SQL_Connector();
            String Sql_query = "Update [Forms] Set [Form_Name]=N'" + this.Form_Name + "',[Form_Description]= N'" + this.Form_Description + "' " +
                "Where [Form_ID]=" + this.Form_ID;
            connector.Execute(Sql_query);
            return;
        }
        public void Save_relations()
        {
            String Sql_query = "Delete From [Form_Dep_State] where Form_ID =" + this.Form_ID;
            foreach (object obj_dep in this.Dep_Related)
            {
                Department dep = (Department)obj_dep;
                foreach (object obj_state in this.State_Related)
                {
                    State state = (State)obj_state;
                    Sql_query += " INSERT INTO [Form_Dep_State] ([Form_ID],[Dep_ID],[State_ID]) VALUES (" + this.Form_ID + " , " +
                        dep.Dep_ID + "," + state.State_ID + ") ";
                }
            }

            SQL_Connector connector = new SQL_Connector();
            connector.Execute(Sql_query);
        }
        
    }
}