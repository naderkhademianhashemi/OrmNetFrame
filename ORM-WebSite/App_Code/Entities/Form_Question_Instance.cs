using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using PersianDateControls;
using System.Collections.Generic;

/// <summary>
/// Summary description for ORM_Form_Question_Instance
/// </summary>
/// 

namespace ORM
{
    public class Form_Question_Instance :BaseFrmQueInstnc
    {
        public Form_Question_Instance(Form_Question_Instance input)
        {
            this._question_id = input.Question_ID;
            this._text_field = input.Text_Field;
            this._date_field = input.Date_Field;
            this._number_field = input.Number_filed;
            this._list_choice = new List<string>();
            this._dep_choice = new List<string>();
            this._state_choice = new List<string>();
            this._branch_choice = new List<string>();
            this._list_choice = input.List_Choice;
            this._state_choice = input.State_Choice;
            this._branch_choice = input.Branch_Choice;
            this._dep_choice = input.Dep_Choice;
            _answer_id = input.Answer_ID;
            _item_id = input.Item_ID;
            //
            // TODO: Add constructor logic here
            //
        }

        public Form_Question_Instance()
        {
            _question_id = "";
            _text_field = "";
            _date_field = "";
            _number_field = "";
            _answer_id = "";
            _item_id = "";

            _list_choice = new List<string>();
            _state_choice = new List<string>();
            _branch_choice = new List<string>();
            _dep_choice = new List<string>();
            //
            // TODO: Add constructor logic here
            //
        }

        public List<Form_Question_Instance> get_answers(String instance_id , String form_id)
        {
            var connector = new SQL_Connector();
            var Sql_query_answer = "Select Form_Instance_ID ," +
                " Question_ID ,[Question_Type] , " +
                "[Template_Type] , [Text] , [date] ,[Number],"+
                " [Selected_Item] , [State_Item] , [dep_Item] , " +
                "[Branch_Item],[item_id],[answer_id] " +
                "From V_Answers where Form_ID = " +
                form_id + " AND Form_Instance_ID = " + instance_id; 
            var dt_answers = connector.Select(Sql_query_answer);
            var Sql_query_questions = "Select [Question_ID],[Question_Type] ," +
                " [Template_Type] " +
                "from Questions where [Form_ID]=" + form_id;
            var dt_questions = connector.Select(Sql_query_questions);
            var output = new List<Form_Question_Instance>();

            foreach (DataRow dr in dt_questions.Rows)
            {
                decimal convertOutput;
                String QID = dr["Question_ID"].ToString();
                Question_ID = QID;
                var qtype = ORM_Types.Get_Question_Type(int.Parse(dr["Question_Type"].ToString()));
                var ttype = ORM_Types.Get_Template_Type(int.Parse(dr["Template_Type"].ToString()));
                DataRow[] dr_answer = dt_answers.Select("Question_ID =" + QID);
                if (dr_answer.Length > 0 && dr_answer[0] != null)
                {
                    switch (qtype)
                    {
                        case ORM_Types.Question_Type.Text:
                            _question_id = QID;
                            _text_field = dr_answer[0]["Text"].ToString();
                            break;
                        case ORM_Types.Question_Type.Number:
                            _question_id = QID;
                            if (decimal.TryParse(dr_answer[0]["Number"].ToString(), out convertOutput))
                                _number_field = convertOutput.ToString("G29");
                            else
                                _number_field = dr_answer[0]["Number"].ToString();
                            break;
                        case ORM_Types.Question_Type.Date:
                            _question_id = QID;
                            _date_field = dr_answer[0]["Date"].ToString();
                            break;
                        case ORM_Types.Question_Type.ComboBoxList:
                        case ORM_Types.Question_Type.DropDownList:
                        case ORM_Types.Question_Type.RadioButtonList:
                            _question_id = QID;
                            foreach (DataRow dr_question in dr_answer)
                            {
                                if (ttype == ORM_Types.Template_Type.Manual)
                                    _list_choice.Add(dr_question["Selected_Item"].ToString());
                                else if (ttype == ORM_Types.Template_Type.Branch)
                                    _branch_choice.Add(dr_question["Branch_Item"].ToString());
                                else if (ttype == ORM_Types.Template_Type.Department)
                                    _dep_choice.Add(dr_question["dep_Item"].ToString());
                                else if (ttype == ORM_Types.Template_Type.State)
                                    _state_choice.Add(dr_question["State_Item"].ToString());
                            }
                            break;
                    }
                }
                Form_Question_Instance new_instance = new Form_Question_Instance(this);
                output.Add(new_instance);
                this.Clear();
            }
            return output;
        }

        public static void Delete(String instance_id)
        {
            String sql_query = "DELETE FROM [Form_Question_Instance] WHERE form_instance_id =" + instance_id;
            sql_query += " DELETE FROM [Branch_Choice] WHERE form_instance_id =" + instance_id;
            sql_query += " DELETE FROM [State_Choice] WHERE form_instance_id =" + instance_id;
            sql_query += " DELETE FROM [Dep_Choice] WHERE form_instance_id =" + instance_id;
            sql_query += " DELETE FROM [Item_Choice] WHERE form_instance_id =" + instance_id;
            SQL_Connector connector = new SQL_Connector();
            connector.Execute(sql_query);
        }

        public string getString()
        {
            string s = Text_Field;
            if (Date_Field != null && Date_Field != "")
            {
                PersianDateTextBox pdt = new PersianDateTextBox();
                pdt.DateValue = Convert.ToDateTime(Date_Field);
                s += pdt.Text;
            }
            s += Number_filed;
            foreach (object o in List_Choice)
            {
                SQL_Connector connector = new SQL_Connector();
                String Sql_query = "Select Item_Text From [List_Items] where Item_ID = " + o.ToString();
                DataTable dt_answers = connector.Select(Sql_query);
                if (s.Length != 0)
                    s += " , ";
                if(dt_answers!=null)
                    s = dt_answers.Rows[0][0].ToString();
            }
            return s;
        }

        private void Clear()
        {
            _question_id = "";
            _text_field = "";
            _date_field = "";
            _number_field = "";
            _list_choice = new List<string>();
            _state_choice = new List<string>();
            _branch_choice = new List<string>();
            _dep_choice = new List<string>();
        }
       
    }
}
