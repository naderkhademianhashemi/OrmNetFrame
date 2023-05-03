using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Collections.Generic;
using ORM;

public partial class EditForm : System.Web.UI.Page
{
    
    private ORM.Forms localform = new ORM.Forms();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["FID"] != null && !IsPostBack)
        {
            chkloc.DataBind();
            CheckBoxList_Department.DataBind();
            String formid = Request.QueryString["FID"].ToString();
            ORM.Forms localform = new ORM.Forms(formid);
            if (localform.Form_ID != "")
            {
                lblForm.Text = localform.Form_Description;
                foreach (var dep_obj in localform.Dep_Related)
                {
                    ORM.Department dep = (ORM.Department)dep_obj;
                    if (dep.Dep_ID == "0")
                    {
                        CheckBoxList_Department.Enabled = false;
                        Dep_selectall.Checked = true;
                        break;
                    }
                    CheckBoxList_Department.Items.FindByValue(dep.Dep_ID).Selected = true;
                }

                foreach (object state_obj in localform.State_Related)
                {
                    ORM.State state = (ORM.State)state_obj;
                    if (state.State_ID == "0")
                    {
                        {
                            chkloc.Enabled = false;
                            selectall.Checked = true;
                            break;
                        }
                    }

                    chkloc.Items.FindByValue(state.State_ID).Selected = true;
                }
            }

            Session.Add("localform", localform);
        }



    }
   
    protected void btnAddForm_Click(object sender, EventArgs e)
    {
        localform = (ORM.Forms)Session["localform"];
        localform.State_Related = new List<State>();
        localform.Dep_Related = new List<Department>();

        if (Dep_selectall.Checked)
        {
            ORM.Department dep = new ORM.Department("0");
            localform.Dep_Related.Add(dep);
        }
        else
        {
            foreach (ListItem list in CheckBoxList_Department.Items)
                if (list.Selected)
                {
                    ORM.Department dep = new ORM.Department(list.Value);
                    localform.Dep_Related.Add(dep);
                }
        }
        if (localform.Dep_Related.Count==0)
        {

            ValidityTest.ForeColor = System.Drawing.Color.Red;
            ValidityTest.Text = "دپارتمان باید انتخاب شود.";
            return;
        }//bug_fix:Iman:null dep choice
        if (selectall.Checked)
        {
            ORM.State state = new ORM.State("0");
            localform.State_Related.Add(state);
        }
        else
        {
            foreach (ListItem list in chkloc.Items)
                if (list.Selected)
                {
                    ORM.State state = new ORM.State(list.Value);
                    localform.State_Related.Add(state);
                }
        }
        localform.Save_relations();
       
    }
    protected void btnTakhsis_Click(object sender, EventArgs e)
    { }
    protected void gvForm_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }
    protected void gvForm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }

    protected void selectall_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox input = (CheckBox)sender;
        if (input.ID == "Dep_selectall")
            CheckBoxList_Department.Enabled = !input.Checked;
        else if (input.ID == "selectall")
            chkloc.Enabled = !input.Checked;
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("FormDefine.aspx");
    }
}
