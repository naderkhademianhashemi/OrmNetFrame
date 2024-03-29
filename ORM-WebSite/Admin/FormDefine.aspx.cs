using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.IO;
using System.Collections.Generic;
using ORM;

public partial class FormDefine : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { }

    private ORM.Forms Create_Form()
    {
        var array_dep = new List<Department>();
        var array_state = new List<State>();
        if (Dep_selectall.Checked)
        {
            ORM.Department dep = new ORM.Department("0");
            array_dep.Add(dep);
        }
        else
        {
            foreach (ListItem list in CheckBoxList_Department.Items)
                if (list.Selected)
                {
                    ORM.Department dep = new ORM.Department(list.Value);
                    array_dep.Add(dep);
                }
        }

        if (array_dep.Count == 0)
        {
            ValidityTest.ForeColor = System.Drawing.Color.Red;
            ValidityTest.Text = "دپارتمان باید انتخاب شود.";
            return null;
        }//bug_fix:Iman:null dep choice

        if (state_selectall.Checked)
        {
            ORM.State state = new ORM.State("0");
            array_state.Add(state);
        }
        else
        {
            foreach (ListItem list in CheckBoxList_State.Items)
                if (list.Selected)
                {
                    ORM.State state = new ORM.State(list.Value);
                    array_state.Add(state);
                }
        }
        return new ORM.Forms(txtFName.Text, txtFDesc.Text, array_dep, array_state, null);
    }

    protected void btnAddForm_Click(object sender, EventArgs e)
    {
        if (ORM.Forms.Isvalid(txtFName.Text))
        {
            ValidityTest.ForeColor = System.Drawing.Color.Red;
            ValidityTest.Text = "نام فرم تکراری است .";
            return;
        }
        else
        {
            ORM.Forms newform = Create_Form();
            if (newform == null)
                return;//bug_fix:Iman:null dep choice
            String formid = newform.Save();
            Response.Redirect("Newquestion.aspx?FID=" + formid + "", true);
        }
    }

    protected void btnAddDep_Click(object sender, EventArgs e)
    {
        if (ORM.Forms.Isvalid(txtFName.Text))
        {
            ValidityTest.ForeColor = System.Drawing.Color.Red;
            ValidityTest.Text = "نام فرم تکراری است . لطفاً نام دیگری وارد کنید.";
            return;
        }
        else
        {
            ORM.Forms newform = Create_Form();
            if (newform == null)
                return;//bug_fix:Iman:null dep choice
            String formid = newform.Save();
            Response.Redirect("EditForm.aspx?FID=" + formid + "", true);
        }

    }

    protected void selectall_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox input = (CheckBox)sender;
        if (input.ID == "Dep_selectall")
            CheckBoxList_Department.Enabled = !input.Checked;
        else if (input.ID == "state_selectall")
            CheckBoxList_State.Enabled = !input.Checked;
    }
}
