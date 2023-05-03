using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseOrmFrm
/// </summary>
public abstract class BaseOrmFrm
{
    public String _form_ID = "";
    public String _form_Name = "";
    public String _form_Description = "";
    public List<Question> _form_question;
    public List<Department> _dep_related;
    public List<State> _state_related;
    public List<State> State_Related
    {
        get { return _state_related; }
        set { _state_related = value; }
    }
    public List<Department> Dep_Related
    {
        get { return _dep_related; }
        set { _dep_related = value; }
    }
    public String Form_Description
    {
        get { return _form_Description; }
        set { _form_Description = value; }
    }
    public String Form_ID
    {
        get { return _form_ID; }
        set { _form_ID = value; }
    }
    public String Form_Name
    {
        get { return _form_Name; }
        set { _form_Name = value; }
    }
    public List<Question> Form_Question
    {
        get { return _form_question; }
        set { _form_question = value; }
    }
}