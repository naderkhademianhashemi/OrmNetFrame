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
using System.Activities.Expressions;
using System.Collections.Generic;

/// <summary>
/// Summary description for ORM_Question
/// </summary>
/// 
namespace ORM
{
    public class Question : BaseQuestion
    {
        public Question()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public Question(String question_id, String description, String form_id, Boolean question_optional,
            ORM_Types.Question_Type question_type, ORM_Types.Template_Type temlate_type, int question_index
            , bool question_active)
        {
            _question_ID = question_id;
            _description = description;
            _form_ID = form_id;
            _question_optional = question_optional;
            _question_type = question_type;
            _template_type = temlate_type;
            _question_index = question_index;
            _question_active = question_active;
            _list = null;

        }

        public Question(String description, String form_id, Boolean question_optional,
            ORM_Types.Question_Type question_type, bool que_active)
        {
            _question_ID = "";
            _description = description;
            _form_ID = form_id;
            _question_optional = question_optional;
            _question_type = question_type;
            _template_type = ORM.ORM_Types.Template_Type.Manual;
            _question_index = 0;
            _list = null;
            _question_active = que_active;
        }

        public Question(String questionid, String formid)
        {
            SQL_Connector CN = new SQL_Connector();
            String sql_query = @"select *
            from V_Questions where Form_ID = " + formid + "AND QID = " + questionid;
            DataTable dt = CN.Select(sql_query);
            foreach (DataRow dr in dt.Rows)
            {
                this._form_ID = formid;
                this._question_ID = questionid;
                this._description = dr["Description"].ToString();
                this._question_index = int.Parse(dr["Question_index"].ToString());
                this._question_optional = Convert.ToBoolean((dr["Question_optional"].ToString()));
                this._question_type = ORM_Types.Get_Question_Type(Convert.ToInt16(dr["question_type"]));
                this._template_type = ORM_Types.Get_Template_Type(Convert.ToInt16(dr["template_type"]));
                this._question_active = Convert.ToBoolean((dr["QueIsActive"].ToString()));

                if (this.Question_Type == ORM_Types.Question_Type.ComboBoxList ||
                    this.Question_Type == ORM_Types.Question_Type.DropDownList ||
                    this.Question_Type == ORM_Types.Question_Type.Table ||
                    this.Question_Type == ORM_Types.Question_Type.RadioButtonList ||
                    this.Question_Type == ORM_Types.Question_Type.ChckBxLstTxt ||
                    this.Question_Type == ORM_Types.Question_Type.RdoBtnLstTxt ||
                    this.Question_Type == ORM_Types.Question_Type.DrpDnLstTxt
                    )
                    if (this.Template_Type != ORM_Types.Template_Type.Manual)
                        _list = new List(questionid);
                    else
                        _list = null;
            }

        }

        public Question(String questionid)
        {
            SQL_Connector CN = new SQL_Connector();
            String QU = "select * from V_Questions where QID = " + questionid;
            DataTable dt = CN.Select(QU);
            if (dt == null)
                return;

            foreach (DataRow dr in dt.Rows)
            {
                this._form_ID = dr["Form_ID"].ToString();
                this._question_ID = questionid;
                this._description = dr["Description"].ToString();
                this._question_index = int.Parse(dr["Question_index"].ToString());
                this._question_optional = Convert.ToBoolean((dr["Question_optional"].ToString()));
                this._question_type = ORM_Types.Get_Question_Type(Convert.ToInt16(dr["question_type"]));
                this._template_type = ORM_Types.Get_Template_Type(Convert.ToInt16(dr["template_type"]));
                this._question_active = Convert.ToBoolean((dr["QueIsActive"].ToString()));

                if (this.Question_Type == ORM_Types.Question_Type.ComboBoxList ||
                this.Question_Type == ORM_Types.Question_Type.DropDownList ||
                this.Question_Type == ORM_Types.Question_Type.Table ||
                this.Question_Type == ORM_Types.Question_Type.RadioButtonList ||
                 this.Question_Type == ORM_Types.Question_Type.ChckBxLstTxt ||
                    this.Question_Type == ORM_Types.Question_Type.RdoBtnLstTxt ||
                    this.Question_Type == ORM_Types.Question_Type.DrpDnLstTxt
                )
                    if (this.Template_Type == ORM_Types.Template_Type.Manual)
                        _list = new List(questionid);
                    else
                        _list = null;
            }
        }
        public void removelist()
        {

            String Sql_query = "";
            if (List != null)
            {
                Sql_query = " DELETE FROM [List] WHERE List_ID =" + this.List.List_ID;
                _CN.Execute(Sql_query);
            }
        }

        public String Save()
        {
#warning Insert-QueIsActv
            var QueIsOptn = Convert.ToSByte(this.Question_Optional);
            var QueIsActv = Convert.ToSByte(this.Question_active);
            var QueTyp = ORM_Types.Get_Question_Type(this.Question_Type);
            var QueTmpTyp = ORM.ORM_Types.Get_Template_Type(this.Template_Type);
            var SQL = "declare @local int " +
                    "set @local = (select count(*) from questions where form_id ="
                    + this.Form_ID + ")" +
                    "INSERT INTO [Questions] ([Description],[Form_ID],[Question_Optional],QueIsActive,[Question_Type]" +
                    ",[Template_Type],[Question_Index])" +
                    " VALUES(N'" + this.Description +
                    "'," + this.Form_ID +
                    "," + QueIsOptn + "," +
                     QueIsActv + "," +
                    QueTyp + ","
                    + QueTmpTyp +
                    ",@local+1)";
            var dt = _CN.Select(SQL);
            if (dt != null && dt.Rows.Count > 0)//'Object reference not set to an instance of an object.'
                return dt.Rows[0][0].ToString();
            else
            {
                Logger.Info("Insert Que", "Question.cs");
                return "";
            }
        }

        public static DataTable Get_DT_Questions(String FID)
        {
            var CN = new SQL_Connector();
            var QUERY = "select Question_ID AS QID , Question_Index AS QueNum , Description AS Que ," +
                " Question_Optional AS Status, Question_Type AS Type,QueIsActive from V_Que " +
                " WHERE form_id = " + FID + " order by Question_Index";
            return CN.Select(QUERY);
        }

        public void Get_Instance()
        {

        }


        public static bool isvalid(String qname, String fid)
        {
            var CN = new SQL_Connector();
            var Sql_query = "select count(*) from Questions where Form_id = " + fid +
                " AND Description = N'" + qname + "'";
            var dt = CN.Select(Sql_query);
            if (dt != null && dt.Rows[0][0].ToString() != "0")
                return true;
            else
                return false;
        }
        public static String Get_Question_ID(String fid, String index_id)
        {
            var CN = new SQL_Connector();
            String Sql_query = "select Question_ID from Questions where form_ID=" + fid +
                " And Question_index =" + index_id;
            DataTable dt = CN.Select(Sql_query);
            return dt.Rows[0]["Question_ID"].ToString();
        }


        public void delete_question()
        {

            String Sql_query = "Begin transaction my_transaction";
            ///  توضیح کد کامنت شده زیر
            ///  اگر کد زیر اجرا شود مشکل تکرار شماره سوال حل شده ولی تمامی جواب های سوال مربوطه از دیتا بیس پاک خواهد شد
            /////  Sql_query += "delete   FROM [FG_DB].[dbo].[Form_Question_Instance]   WHERE Question_ID =" + this._question_ID;
            Sql_query += " DELETE FROM [Questions] WHERE Question_ID =" + this._question_ID + " AND Form_ID =" + this._form_ID;
            Sql_query += " update Questions Set [Question_index] = [Question_index]-1 where [Question_index]>" + this.Question_Index +
                " AND [Form_ID] =" + this.Form_ID;
            Sql_query += " Commit transaction my_transaction";
            _CN.Execute(Sql_query);
        }

        public void update()
        {
            byte isOptn = Convert.ToByte(this.Question_Optional);
            int QueTyp = ORM.ORM_Types.Get_Question_Type(this.Question_Type);
            int TempTyp = ORM.ORM_Types.Get_Template_Type(this.Template_Type);
            String Sql_query = "UPDATE [Questions] SET [Description] = N'" + this.Description +
                "' , [Form_ID] =" + this.Form_ID +
                " , [Question_Optional] = " + isOptn +
                " , [Question_Type] = " + QueTyp +
                " , [Template_Type] = " + TempTyp +
                " , [Question_Index] = " + Question_Index +
                ",QueIsActive=" + this.Question_active +
                " WHERE  [Question_ID] = " + this.Question_ID;

            _CN.Execute(Sql_query);
        }

        public static List<Question> Get_Form_Questions(String formid)
        {
            var CN = new SQL_Connector();
            var QUERY = "select Question_ID as[QID] ,Question_index , [description] ," +
                "Question_optional , question_type , template_type " +
            ", QueIsActive from Questions where Form_ID =" + formid + " order by Question_index";
            var DT = CN.Select(QUERY);
            var output = new List<Question>();
            foreach (DataRow dr in DT.Rows)
            {
                var isOptn = Convert.ToBoolean(dr["Question_optional"].ToString());
                var isActv = Convert.ToBoolean(dr["QueIsActive"].ToString());
                var qtype = ORM_Types.Get_Question_Type(Convert.ToInt16(dr["question_type"].ToString()));
                var ttype = ORM_Types.Get_Template_Type(Convert.ToInt16(dr["template_type"].ToString()));
                var qIndex = int.Parse(dr["Question_index"].ToString());
                var qDes = dr["Description"].ToString();
                var qID = dr["QID"].ToString();
                var que = new Question(qID, qDes, formid, isOptn, qtype, ttype, qIndex, isActv);
                output.Add(que);
            }
            return output;
        }

        public static ORM_Types.Template_Type get_template(String Qid)
        {
            var CN = new SQL_Connector();
            String sql_query = "select Question_ID as [QID] , question_type , template_type " +
            "from Questions where Question_ID =" + Qid;
            DataTable dt = CN.Select(sql_query);
            return ORM_Types.Get_Template_Type(int.Parse(dt.Rows[0]["template_type"].ToString()));
        }

        public static ORM_Types.Question_Type get_type(String Qid)
        {
            var CN = new SQL_Connector();
            String sql_query = "select Question_ID as[QID] , question_type , template_type " +
            "from Questions where Question_ID =" + Qid;
            DataTable dt = CN.Select(sql_query);
            return ORM_Types.Get_Question_Type(int.Parse(dt.Rows[0]["question_type"].ToString()));

        }
    }
}