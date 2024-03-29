using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;

public partial class UserSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //GridView1 DataSource is selected from V_Personel_Hot in [FG_DB] database
        if (IsPostBack)
            return;
        txtSearch.Focus();
        Label1.Text = Request.QueryString["GName"];
        lblGroup.Text = Request.QueryString["GName"];
        FillGridView_01();
        lblAdminSetting.Text = " تعیین ارشد گروه" + "&nbsp" + Request.QueryString["GName"];
        lblFormSetting.Text = " تعیین فرم های گروه" + "&nbsp" + Request.QueryString["GName"];
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        //تیک گذاشتن یا برداشتن تیک عضویت
        GridView1_CheckBoxBound();
    }

    /// <summary>
    /// تیک گذاشتن یا برداشتن تیک عضویت
    /// </summary>
    public void GridView1_CheckBoxBound()
    {
        (GridView1.Rows[0].FindControl("cbSelect") as CheckBox).Checked = true;
        
    }
    //دقیقا قبل از تغییر ایندکس صفحه ،این متد اجرا می شود
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        if (txtSearch.Text.Length > 2 && txtSearch.Text.Length < 50)
            FillGridViewBySematOrUserName(txtSearch.Text);
        else
            FillGridView_01();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //با هر بار جستجوی جدید ایندکس صفحه به مقدار اولیه اش بر می گردد. 
        GridView1.PageIndex = 0;
        txtCodePerson.Text = string.Empty;
        if (txtSearch.Text.Length > 2 && txtSearch.Text.Length < 50)
        {
            FillGridViewBySematOrUserName(txtSearch.Text);
        }
        else
        {
            FillGridView_01();
        }
    }

    /// <summary>
    /// select username and semat  and Fill GridView1
    /// </summary>
    protected void FillGridView_01()
    {
        SQL_Connector connector = new SQL_Connector();
        DataTable dt = connector.Select("SELECT *  FROM [V_Personel_Hot] ");

        GridView1.DataSource = null;
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    /// <summary>
    /// select username and semat based on username or Semat entered in txtSearch TextBox and Fill GridView1
    /// </summary>
    /// <param name="input"></param>
    protected void FillGridViewBySematOrUserName(string input)
    {
        SQL_Connector connector = new SQL_Connector();
        String sql_query_Uname = "SELECT *  FROM [V_Personel_Hot] where UserName like N'%" + input.Trim() + "%'";
        DataTable dt = connector.Select(sql_query_Uname);

        String sql_query_Semat = "SELECT *  FROM [V_Personel_Hot] where Semat like N'%" + input.Trim() + "%'";
        DataTable dt2 = connector.Select(sql_query_Semat);

        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = null;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        if (dt.Rows.Count <= 0 && dt2.Rows.Count > 0)
        {
            GridView1.DataSource = null;
            GridView1.DataSource = dt2;
            GridView1.DataBind();
        }
        if (dt.Rows.Count <= 0 && dt2.Rows.Count <= 0)
        {
            txtSearch.Text = "";
            FillGridView_01();
        }
    }


    #region تغییر عضویت
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        if (txtSearch.Text.Length <= 0 || txtCodePerson.Text.Length <= 0)
        {
            GridView1.EditIndex = e.NewEditIndex;
            if (txtSearch.Text.Length > 2 && txtSearch.Text.Length < 50)
            {
                FillGridViewBySematOrUserName(txtSearch.Text);
            }
            int result;
            bool isInteger = Int32.TryParse(txtCodePerson.Text, out result);
            //چک کردن فرمت تکس باکس و طول آن
            if (txtCodePerson.Text.Length > 0 && isInteger)
            {
                SearchByCodePersenely(txtCodePerson.Text);
            }
            if (txtCodePerson.Text.Length <= 0 && txtSearch.Text.Length <= 0)
            {
                FillGridView_01();
            }
        }
        //اگر 2 تکس باکس همزمان تکمیل شده بودند .
        else
        {
            txtSearch.Text = string.Empty;

            string cleanMessage = "در هنگام ویرایش تک سطری ، هر دو ورودی نام کاربری و شماره پرسنلی همزمان نباید تکمیل باشند ";
            string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";

            this.ClientScript.RegisterClientScriptBlock(typeof(Alert), "alert", script);
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        if (txtSearch.Text.Length > 2 && txtSearch.Text.Length < 50)
        {
            FillGridViewBySematOrUserName(txtSearch.Text);
        }
        int result;
        bool isInteger = Int32.TryParse(txtCodePerson.Text, out result);
        //چک کردن فرمت تکس باکس و طول آن
        if (txtCodePerson.Text.Length > 0 && isInteger)
        {
            SearchByCodePersenely(txtCodePerson.Text);
        }
        if (txtCodePerson.Text.Length <= 0 && txtSearch.Text.Length <= 0)
        {
            FillGridView_01();
        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        bool chk = false;
        string lblUser_Name = (GridView1.Rows[e.RowIndex].FindControl("lblUser_Name") as Label).Text;
        string Group_ID = Request.QueryString["gid"].ToString();


        //if checked
        //insert into [FG_DB].[dbo].[Group_Users] Table 
        chk = (GridView1.Rows[e.RowIndex].FindControl("cbSelect") as CheckBox).Checked;
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ORM_ConnectionString"].ConnectionString);
        if (chk == true)
        {
            String sqlInsert = @"INSERT INTO Group_Users (Group_ID,User_Name)
                                                    VALUES(@Group_ID,@User_Name)";

            SqlCommand cmd = new SqlCommand(sqlInsert, con);
            cmd.Parameters.AddWithValue("@Group_ID", Group_ID);
            cmd.Parameters.AddWithValue("@User_Name", lblUser_Name);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //else 
        //delete from [FG_DB].[dbo].[Group_Users] Table
        else
        {
            String sqlDelete = @"DELETE FROM Group_Users 
                           WHERE Group_ID=@Group_ID AND User_Name=@User_Name";

            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("@Group_ID", Group_ID);
            cmd.Parameters.AddWithValue("@User_Name", lblUser_Name);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        GridView1.EditIndex = -1;
        if (txtSearch.Text.Length > 2 && txtSearch.Text.Length < 50)
        {
            FillGridViewBySematOrUserName(txtSearch.Text);
        }
        int result;
        bool isInteger = Int32.TryParse(txtCodePerson.Text, out result);
        //چک کردن فرمت تکس باکس و طول آن
        if (txtCodePerson.Text.Length > 0 && isInteger)
        {
            SearchByCodePersenely(txtCodePerson.Text);
        }
        if (txtCodePerson.Text.Length <= 0 && txtSearch.Text.Length <= 0)
        {
            FillGridView_01();
        }
    }

    #endregion تغییر عضویت

    #region تغییر عضویت براساس سمت 
    protected void btnAddMembers_Click(object sender, EventArgs e)
    {
        if (txtSemat.Text.Length > 2 && txtSemat.Text.Length < 50 && Request.QueryString["gid"] != null)
        {
            string Group_ID = Request.QueryString["gid"].ToString();
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ORM_ConnectionString"].ConnectionString);
            using (con)
            {
                string Sql = @"DELETE FROM Group_Users WHERE Group_ID=@Group_ID 
                                                       AND User_Name IN 
                                                        (SELECT  UserName FROM V_Personel_Hot 
                                                        WHERE Semat= N'" + txtSemat.Text + "') ";
                Sql += @"INSERT INTO Group_Users(Group_ID,User_Name)
                                            SELECT @Group_ID,UserName
                                            FROM [V_Personel_Hot] 
                                            WHERE Semat = N'" + txtSemat.Text + "'";
                txtSemat.Text = string.Empty;

                SqlTransaction tran = null;
                SqlCommand cmd = new SqlCommand(Sql, con);
                    con.Open();
                    tran = con.BeginTransaction();
                    cmd.Transaction = tran;
                    cmd.CommandText = Sql;
                    cmd.Parameters.AddWithValue("@Group_ID", Group_ID);

                    cmd.ExecuteNonQuery();
                    tran.Commit();
                    con.Close();

            }//end using

            GridView1_CheckBoxBound();
        }//end if
    }

    protected void btnRemoveMembers_Click(object sender, EventArgs e)
    {
        if (txtSemat.Text.Length > 2 && txtSemat.Text.Length < 50 && Request.QueryString["gid"] != null)
        {

            string Group_ID = Request.QueryString["gid"].ToString();
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ORM_ConnectionString"].ConnectionString);
            using (con)
            {
                string Sql = @"DELETE FROM Group_Users WHERE Group_ID=@Group_ID 
                                                       AND User_Name IN 
                                                        (SELECT  UserName FROM V_Personel_Hot 
                                                        WHERE Semat = N'" + txtSemat.Text + "') ";
                txtSemat.Text = string.Empty;
                SqlCommand cmd = new SqlCommand(Sql, con);
                    con.Open();
                    cmd.CommandText = Sql;
                    cmd.Parameters.AddWithValue("@Group_ID", Group_ID);
                    cmd.ExecuteNonQuery();
                    con.Close();
            }//end using

            GridView1_CheckBoxBound();


        }//end if
    }

    #endregion تغییر عضویت براساس سمت 

    /// <summary>
    /// ثبت اررورر در فوتر سایت
    /// </summary>
    /// <param name="message"></param>
    public void SetMessagesFooter(string message)
    {
        MasterPage master = (MasterPage)Page.Master;
        Literal lit;
        lit = (Literal)master.FindControl("litMessage");
        if (lit != null)
            lit.Text = message;
    }
    public void SetMessages(string message)
    {
        MasterPage master = (MasterPage)Page.Master;
        Literal lit;
        lit = (Literal)master.FindControl("MSG");
        if (lit != null)
            lit.Text = message;
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
            GridView1.DataSource = null;
            GridView1.DataSource = dt_ShomarehPersenely;
            GridView1.DataBind();

        }
        else
        {
            txtCodePerson.Text = string.Empty;

            Alert.Show();
        }
    }

    protected void ImgBtnCodePerson_Click(object sender, EventArgs e)
    {
        txtSearch.Text = string.Empty;

        int result;
        bool isInteger = Int32.TryParse(txtCodePerson.Text, out result);

        //چک کردن فرمت تکس باکس 
        if (isInteger)
        {
            SearchByCodePersenely(txtCodePerson.Text);
        }
        else
        {
            txtCodePerson.Text = string.Empty;
            //FillGridView_01();
        }
    }
    protected void btnAdminSetting_Click(object sender, EventArgs e)
    {

        Response.Redirect("GroupAdminSetting.aspx?GID=" + Request.QueryString["GID"].ToString() +
            "&GName=" + Request.QueryString["GName"].ToString(), true);

    }

    protected void btnFormSetting_Click(object sender, EventArgs e)
    {

        Response.Redirect("FormSetting.aspx?GID=" + Request.QueryString["GID"].ToString() +
           "&GName=" + Request.QueryString["GName"].ToString(), true);
    }

}//end class