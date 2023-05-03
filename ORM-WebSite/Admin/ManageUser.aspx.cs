using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.WebControls.WebParts;
using System.Web.Configuration;

public partial class ManageUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        txtSearch.Focus();
        FillGridView_02();
        FillGrdAdminRoles();
    }

    protected void FillGridView_02()
    {
        SQL_Connector connector = new SQL_Connector();
        DataTable dt = connector.Select("SELECT *  FROM [V_Personel_Hot] ");

        GridView2.DataSource = null;
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
    private void FillGrdAdminRoles()
    {
        SQL_Connector CN = new SQL_Connector();
        string SQL_query = @"SELECT        RoleName,
                                            UserName
                            FROM            V_AdminRoleUsers";
        var dt = CN.Select(SQL_query);
        grdAdminRoles.DataSource = dt;
        grdAdminRoles.DataBind();
    }
    protected void FillGridViewBySematOrUserName(string input)
    {
        SQL_Connector connector = new SQL_Connector();

        String sql_query_Uname = "SELECT *  FROM [V_Personel_Hot] where UserName like N'%" + input + "%' ";
        DataTable dt = connector.Select(sql_query_Uname);

        String sql_query_Semat = "SELECT *  FROM [V_Personel_Hot] where Semat like N'%" + input + "%' ";
        DataTable dt2 = connector.Select(sql_query_Semat);

        if (dt.Rows.Count > 0)
        {
            GridView2.DataSource = null;
            GridView2.DataSource = dt;
            GridView2.DataBind();

        }
        if (dt.Rows.Count <= 0 && dt2.Rows.Count > 0)
        {
            GridView2.DataSource = null;
            GridView2.DataSource = dt2;
            GridView2.DataBind();
        }
        if (dt.Rows.Count <= 0 && dt2.Rows.Count <= 0)
        {
            txtSearch.Text = "";
            FillGridView_02();
        }
    }
    public String GetDestUrl(object username)
    {
        if (username.ToString() == Profile.UserName)
            return "#";
        if (Roles.IsUserInRole(username.ToString(), "admin"))
            return "#";
        return "EditUser.aspx?u=" + username.ToString();
    }

    public String GetCommand(object username)
    {
        if (username.ToString() == Profile.UserName)
            return "confirm_delete1();return false;";
        if (Profile.UserName == "tatadmin")
            return "return confirm_delete();";
        if (Roles.IsUserInRole(username.ToString(), "admin"))
            return "confirm_delete1();return false;";
        return "return confirm_delete();";
    }


    /// <summary>
    /// جستجو بر اساس شماره پرسنلی
    /// </summary>
    /// <param name="input"></param>
    protected void SearchByCodePersenely(string input)
    {
        SQL_Connector connector = new SQL_Connector();
        String sql_query_ShomarehPersenely = "SELECT *  FROM [V_Personel_Hot] where ShomarehPersenely = " + input.Trim();
        DataTable dt_ShomarehPersenely = connector.Select(sql_query_ShomarehPersenely);

        if (dt_ShomarehPersenely.Rows.Count > 0)
        {
            GridView2.DataSource = null;
            GridView2.DataSource = dt_ShomarehPersenely;
            GridView2.DataBind();
        }
        else
        {
            Alert.Show();
            txtCodePerson.Text = string.Empty;
        }
    }

    protected void btnCodePerson_Click(object sender, EventArgs e)
    {
        txtSearch.Text = string.Empty;
        GridView2.SelectedIndex = -1;
        int result;
        bool isInteger = Int32.TryParse(txtCodePerson.Text, out result);

        //چک کردن فرمت تکس باکس 
        if (isInteger)
            SearchByCodePersenely(txtCodePerson.Text);
        else
            txtCodePerson.Text = string.Empty;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        txtCodePerson.Text = string.Empty;
        GridView2.SelectedIndex = -1;
        GridView2.PageIndex = 0;
        if (txtSearch.Text.Length > 0 && txtSearch.Text.Length < 20)
            FillGridViewBySematOrUserName(txtSearch.Text);
        else
        {
            txtSearch.Text = "";
            FillGridView_02();
        }
    }

    protected void btnAddUserToRole_Click(object sender, EventArgs e)
    {
        //اگر تکس باکس یوزر خالی نبود 
        if (txtUser.Text.Length > 0)
        {
            SqlConnection Sqlconnection_aspnetdb =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);

            string sql_query = "SELECT [UserName] FROM [V_Personel_Hot] WHERE [UserName] =N'" + 
                txtUser.Text + "'";

            SQL_Connector con = new SQL_Connector();
            string output = con.execute_scalar(sql_query);
            //اگر یوزری با نام کاربری وارد شده در ویوی پرسنل هات وجود داشت .
            if (output != "0")
            {
                if (Roles.IsUserInRole(txtUser.Text, "FillFormer"))
                {
                    Roles.RemoveUserFromRole(txtUser.Text, "FillFormer");
                }
                Roles.AddUserToRole(txtUser.Text, "admin");
                //رفرش کردن گرید ادمین
                FillGrdAdminRoles();
            }
        }
        //خالی کردن تکس باکس
        txtUser.Text = string.Empty;
    }
    protected void btnAddUserToFillFormer_Click(object sender, EventArgs e)
    {
        //اگرکاربر در نقش ادمین بود و تکس باکس یوزر خالی نبود 
        if (Roles.IsUserInRole(txt_UserToFillFormer.Text, "admin") &&
            txt_UserToFillFormer.Text.Length > 0)
        {
            Roles.RemoveUserFromRole(txt_UserToFillFormer.Text, "admin");
            //اگر کاربر نقش فیل فرمر نداشت
            if (!Roles.IsUserInRole(txt_UserToFillFormer.Text, "FillFormer"))
            {
                Roles.AddUserToRole(txt_UserToFillFormer.Text, "FillFormer");
            }
            //refresh GrdAdminRoles
            FillGrdAdminRoles();
        }
        //خالی کردن تکس باکس فیل فرمر
        txt_UserToFillFormer.Text = string.Empty;
        //DeSelect GridView grdAdminRoles row
        grdAdminRoles.SelectedIndex = -1;
    }


    protected void GridView2_DataBound(object sender, EventArgs e)
    {
        String strConnection1 = WebConfigurationManager.ConnectionStrings["ORM_ConnectionString"].ConnectionString;
        SqlConnection cnn1 = new SqlConnection(strConnection1);
        SqlCommand com = new SqlCommand();
        com.Parameters.AddWithValue("@uname", " ");

        com.Connection = cnn1;

        cnn1.Open();
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            ProfileCommon p = Profile.GetProfile(GridView2.Rows[i].Cells[0].Text);
            string sx = p.UserName.ToString();

            com.CommandText = "select depname from department where depid = " + p.Department.ToString();
            GridView2.Rows[i].Cells[2].Text = com.ExecuteScalar().ToString();
            com.Parameters[0].Value = sx;

        }
        cnn1.Close();
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.SelectedIndex = -1;
        if (txtSearch.Text.Length <= 0)
            FillGridView_02();

        if (txtSearch.Text != null && txtSearch.Text.Length > 0)
            FillGridViewBySematOrUserName(txtSearch.Text);
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtUser.Text = GridView2.SelectedRow.Cells[1].Text;
    }

    protected void grdAdminRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAdminRoles.SelectedIndex = -1;
        grdAdminRoles.PageIndex = e.NewPageIndex;
        FillGrdAdminRoles();
    }

    protected void grdAdminRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lblUserName = (Label)grdAdminRoles.SelectedRow.FindControl("lblUserName");
        txt_UserToFillFormer.Text = lblUserName.Text;
    }
}
