using ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseFrmInstnc
/// </summary>
public abstract class BaseFrmInstnc
{
    public String _instance_id = "";
    public String _filldate = "";
    public String _user_name = "";
    public String _form_id = "";
    public List<Form_Question_Instance> _answers;
    public String Instance_ID
    {
        get { return _instance_id; }
        set { _instance_id = value; }
    }

    public String Fill_Date
    {
        get { return _filldate; }
        set { _filldate = value; }
    }

    public String Form_ID
    {
        get { return _form_id; }
        set { _form_id = value; }
    }

    public String User_Name
    {
        get { return _user_name; }
        set { _user_name = value; }
    }

    public List<Form_Question_Instance> Answers
    {
        get { return _answers; }
        set { _answers = value; }
    }

    public abstract string ReturnString(String QID, ORM_Types.Question_Type qtype)
   ;
    public abstract List<string> ReturnLString(String QID, ORM_Types.Template_Type ttype)
   ;
    public abstract DataTable ReturnDTAnswers(String QID)
    ;
    public abstract void Add_Answer(Form_Question_Instance input)
    ;

    public abstract void Save();

    public abstract void Update(String instance_id);

    public abstract String get_string_value(String input);

    public abstract String get_nonstring_value(String input);

    public abstract String get_date_value(String input);

}