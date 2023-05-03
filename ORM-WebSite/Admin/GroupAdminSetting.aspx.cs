using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_GroupAdminSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            txtUserName.Focus();
            bindGrid1();
            lblGruop.Text = Request.QueryString["GName"];
            lblGroup2.Text = Request.QueryString["GName"];
            lblUserSetting.Text = " تعیین کاربران گروه" + "&nbsp" + Request.QueryString["GName"];
            lblFormSetting.Text = " تعیین فرم های گروه" + "&nbsp" + Request.QueryString["GName"];
        }

    }

    public void bindGrid1()
    {
        string str = "SELECT * from V_Personel WHERE Group_ID=" +
                Request.QueryString["GID"].ToString();

        SQL_Connector con = new SQL_Connector();

        GridView1.DataSource = con.Select(str);
        GridView1.DataBind();
    }

    public void bindGridByUser(string userName)
    {
        string str = "SELECT * from V_Personel WHERE User_Name LIKE N'%" + userName + "%' AND Group_ID=" +
                Request.QueryString["GID"].ToString();

        SQL_Connector con = new SQL_Connector();

        GridView1.DataSource = con.Select(str);
        GridView1.DataBind();
    }

    protected void bindGridByCodePersenely(string input)
    {
        SQL_Connector connector = new SQL_Connector();
        String sql_query_ShomarehPersenely = "SELECT * FROM [V_Personel] where ShomarehPersenely = "
            + input.Trim()
            + " and Group_ID=" + Request.QueryString["GID"].ToString();
        DataTable dt_ShomarehPersenely = connector.Select(sql_query_ShomarehPersenely);

        if (dt_ShomarehPersenely.Rows.Count > 0)
        {
            GridView1.DataSource = null;
            GridView1.DataSource = dt_ShomarehPersenely;
            GridView1.DataBind();

        }
        else
        {
            txtCodePerson.Text = string.Empty;
            Alert.Show();
        }
    }//end bindGridByCodePersenely

    #region Events

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        if (txtUserName.Text.Length > 0)
        {
            bindGridByUser(txtUserName.Text);
        }
        else if(txtCodePerson.Text.Length > 0)
        {
            bindGridByCodePersenely(txtCodePerson.Text);
        }
        else
        {
            bindGrid1();
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        if (txtUserName.Text.Length > 0)
        {
            bindGridByUser(txtUserName.Text);
        }
        else if (txtCodePerson.Text.Length > 0)
        {
            bindGridByCodePersenely(txtCodePerson.Text);
        }
        else
        {
            bindGrid1();
        }

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        bool chk = false;
        string UserName = (GridView1.Rows[e.RowIndex].FindControl("lblUserName") as Label).Text;
        string Group_ID = Request.QueryString["gid"].ToString();

        //if checked
        //UPDATE Group_Users Table --- IsAdmin
        chk = (GridView1.Rows[e.RowIndex].FindControl("cbSelect") as CheckBox).Checked;

        SQL_Connector con = new SQL_Connector();

        string str = "UPDATE Group_Users SET IsAdmin = "
            + Convert.ToInt16(chk) + " WHERE Group_ID = "
            + Group_ID + " AND User_Name = N'" + UserName + "'";

        con.Execute(str);

        GridView1.EditIndex = -1;

        if (txtUserName.Text.Length > 0)
        {
            bindGridByUser(txtUserName.Text);
        }
        else if (txtCodePerson.Text.Length > 0)
        {
            bindGridByCodePersenely(txtCodePerson.Text);
        }
        else
        {
            bindGrid1();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        if (txtUserName.Text.Length > 0)
        {
            bindGridByUser(txtUserName.Text);
        }
        else
        {
            bindGrid1();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        txtCodePerson.Text = string.Empty;
        bindGridByUser(txtUserName.Text);
    }

    protected void btnUserSetting_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserSetting.aspx?GID=" + Request.QueryString["GID"].ToString() +
            "&GName=" + Request.QueryString["GName"].ToString(), true);
    }

    protected void btnFormSetting_Click(object sender, EventArgs e)
    {

        Response.Redirect("FormSetting.aspx?GID=" + Request.QueryString["GID"].ToString() +
           "&GName=" + Request.QueryString["GName"].ToString(), true);
    }

    protected void btnSearchCodePerson_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;

        txtUserName.Text = string.Empty;

        int result;
        bool isInteger = Int32.TryParse(txtCodePerson.Text, out result);

        //چک کردن فرمت تکس باکس 
        if (isInteger)
        {
            bindGridByCodePersenely(txtCodePerson.Text);
        }
        else
        {
            bindGrid1();
            txtCodePerson.Text = string.Empty;

        }

    }//end btnSearchCodePerson_Click

    #endregion Events

}