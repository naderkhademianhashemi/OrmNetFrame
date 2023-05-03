using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;


public partial class FillFormerNewQuestion : System.Web.UI.Page
{
    String _FID;
    bool _addQue = false;//permission to add Question
    protected void Page_Load(object sender, EventArgs e)
    {
        var Tint = 0L;
        var FID = Request.QueryString["FID"];
        if (FID == null ||
            !long.TryParse(FID, out Tint))
            Response.Redirect("homepage.aspx", true);

        _FID = Tint.ToString();
        var PRM = ORM.Group_Permissions.Get_Permission(_FID, RSRC.RSUsers.risk);
        _addQue = PRM._addQuestion;

        if (!_addQue)
            Response.Redirect("homepage.aspx", true);

        if (IsPostBack)
            return;

        var F = new ORM.Forms(_FID);
        Databind();
        Label9.Text = F.Form_Name;
    }

    #region functions
    public string IsQueActiv(object input)
    {
        if (Convert.ToBoolean(input))
            return "فعال";
        else
            return "غیر فعال";
    }
    public String GetStatus(object obj)
    {
        if (Convert.ToBoolean(obj))
            return "اختياري";
        else
            return "اجباري";
    }
    public int GetselectIndex(object obj)
    {
        int index = 0;
        if (!Convert.ToBoolean(obj.ToString().Trim()))
            index = 1;
        return index;
    }
    public bool GetSelect(object obj, object obj2)
    {
        if (obj.ToString() == obj2.ToString())
            return true;
        return false;
    }


    public bool linkIsEnable(object obj)
    {
        ORM.ORM_Types.Question_Type temptype = ORM.ORM_Types.Get_Question_Type(int.Parse(obj.ToString()));
        if (temptype == ORM.ORM_Types.Question_Type.Text ||
            temptype == ORM.ORM_Types.Question_Type.Number ||
            temptype == ORM.ORM_Types.Question_Type.Date)
            return false;
        return true;
    }
    public String GetType(object obj)
    {
        switch (obj.ToString())
        {
            case "1": return "متنی";
            case "2": return "چندگزینه ای چند جوابی";
            case "3": return "چند گزینه ای تک جوابی ";
            case "4": return "چند گزینه ای تک جوابی نمایش به صورت آبشاری";
            case "5": return "مقدار عددی";
            case "6": return "تاریخ";
            case "7": return "جدول";
            case "8": return "چندگزینه ای چند جوابی و متنی";
            case "9": return "چندگزینه ای تک جوابی و متنی";
            case "10": return "چندگزینه ای آبشاری و متنی";
            default:
                return "";
        }
    }

    private void Databind()
    {
        GridView_Form_Questions.DataSource = ORM.Question.Get_DT_Questions(_FID);
        String[] keys = { "QID" };
        GridView_Form_Questions.DataKeyNames = keys;
        GridView_Form_Questions.DataBind();
    }


    #endregion functions

    #region events

    protected void btnAddQue_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        var queIsOpton = rblStatus.SelectedIndex == 0;
        var queIsActv = rblIsActv.SelectedValue == "1";
        var queType = ORM.ORM_Types.Get_Question_Type(int.Parse(rblTypeQue.SelectedValue));

        var newQue = new ORM.Question(
            txtQue.Text,
            _FID,
            queIsOpton,
            queType,
        queIsActv);

        newQue.Save();
        Response.Redirect("FillFormerNewQuestion.aspx?FID=" + _FID + "", true);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        var l = sender as System.Web.UI.WebControls.Button;
        Response.Redirect("FillFormerEditOption.aspx?Qid=" + l.CommandArgument);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        String FID = Request.QueryString["FID"].ToString();
        String First_ID = ((sender as System.Web.UI.WebControls.Button).CommandArgument.Split(';')[0]);
        int Question_index = int.Parse(((sender as System.Web.UI.WebControls.Button).CommandArgument.Split(';')[1]));

        if (Question_index == 1)
            return;
        else
        {
            String Seccond_ID = ORM.Question.Get_Question_ID(FID, (Question_index - 1).ToString());
            ORM.Forms.Swap_Qestion(First_ID, Seccond_ID, FID);
            Databind();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        String FID = Request.QueryString["FID"].ToString();
        String First_ID = ((sender as System.Web.UI.WebControls.Button).CommandArgument.Split(';')[0]);
        int Question_index = int.Parse(((sender as System.Web.UI.WebControls.Button).CommandArgument.Split(';')[1]));

        if (Question_index >= GridView_Form_Questions.Rows.Count)
            return;
        else
        {
            String Seccond_ID = ORM.Question.Get_Question_ID(FID, (Question_index + 1).ToString());
            ORM.Forms.Swap_Qestion(First_ID, Seccond_ID, FID);
            Databind();
        }
    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (ORM.Question.isvalid(txtQue.Text, _FID))
        {
            args.IsValid = false;
            return;
        }
        else
        {
            args.IsValid = true;
        }
    }

    protected void txtQue_TextChanged(object sender, EventArgs e)
    {

    }
    protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }

    #endregion events

    #region gridEvents

    protected void GridView_Form_Questions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState & DataControlRowState.Edit) > 0)
        {

            var btnUpdate = (System.Web.UI.WebControls.Button)e.Row.FindControl("ImageButton23");

            this.Form.DefaultButton = btnUpdate.UniqueID;

        }
    }
    protected void GridView_Form_Questions_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView_Form_Questions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            ORM.Forms thisform = new ORM.Forms(_FID);
            thisform.delete_question((String)e.CommandArgument);
            Response.Redirect("FillFormerNewQuestion.aspx?FID=" + _FID + "", true);
        }
    }
    protected void GridView_Form_Questions_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GridView_Form_Questions_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView_Form_Questions.EditIndex = e.NewEditIndex;
        Databind();

    }


    protected void GridView_Form_Questions_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView_Form_Questions.EditIndex = -1;
        Databind();
    }

    protected void GridView_Form_Questions_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int editindex = GridView_Form_Questions.EditIndex;
        String QID = GridView_Form_Questions.DataKeys[editindex].Value.ToString();
        ORM.Question que = new ORM.Question(QID);
        que.Description = ((System.Web.UI.WebControls.TextBox)GridView_Form_Questions.Rows[editindex].FindControl("TextBox2")).Text;
        que.Question_Optional = Convert.ToBoolean(((DropDownList)GridView_Form_Questions.
            Rows[editindex].FindControl("DropDownList2")).Items[0].Selected);
        que.update();

        GridView_Form_Questions.EditIndex = -1;
        Databind();
    }
    protected void GridView_Form_Questions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_Form_Questions.PageIndex = e.NewPageIndex;
        Databind();
    }

    #endregion gridEvents

}
