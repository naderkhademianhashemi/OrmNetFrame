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
using System.Web.Configuration;

public partial class FormSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Int64 gid = 0;
        if (Request.QueryString["Gid"] == null
           || Request.QueryString["Gname"] == null
           || !Int64.TryParse(Request.QueryString["GID"], out gid))
        {
            Response.Redirect("homepage.aspx", true);
        }

        if (!Page.IsPostBack)
        {
            lblGroup.Text =  Request.QueryString["GName"];
            lblAdminSetting.Text = " تعیین ارشد گروه" + "&nbsp" + Request.QueryString["GName"];
            lblUserSetting.Text = " تعیین کاربران گروه" + "&nbsp" + Request.QueryString["GName"];

            SqlCommand com = new SqlCommand();
            SQL_Connector conector = new SQL_Connector();
            com.CommandText = "select count(*) from groups where group_id = " + gid.ToString();
            String result = conector.execute_scalar(com);
            if (result.Equals("0"))
                Response.Redirect("homepage.aspx", true);

            GridView1.DataSource = ORM.Forms.get_forms("", "", "");
            GridView1.DataBind();
        }
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        ORM.Groups localgroup = new ORM.Groups(Request.QueryString["gid"]);

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            String formid = (GridView1.Rows[i].FindControl("Label1") as Label).Text;

            ORM.Group_Permissions localpermission = localgroup.Get_Permissions(formid);

            (GridView1.Rows[i].FindControl("cbR") as CheckBox).Checked = localpermission._readable;
            (GridView1.Rows[i].FindControl("cbW") as CheckBox).Checked = localpermission._writable;
            (GridView1.Rows[i].FindControl("cbU") as CheckBox).Checked = localpermission._updatable;
            (GridView1.Rows[i].FindControl("cbD") as CheckBox).Checked = localpermission._deleteable;
            (GridView1.Rows[i].FindControl("cbAddQuestion") as CheckBox).Checked = localpermission._addQuestion;
            (GridView1.Rows[i].FindControl("cbReadOtherAnswers") as CheckBox).Checked = localpermission._ReadOtherAnswers;
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        var user = User.Identity.Name;
        var groupName = Request.QueryString["gName"];

        ORM.Groups localgroup = new ORM.Groups(Request.QueryString["gid"]);
        localgroup.Group_Permissions.Clear();

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            bool cbr = (GridView1.Rows[i].FindControl("cbr") as CheckBox).Checked;
            bool cbw = (GridView1.Rows[i].FindControl("cbw") as CheckBox).Checked;
            bool cbu = (GridView1.Rows[i].FindControl("cbu") as CheckBox).Checked;
            bool cbd = (GridView1.Rows[i].FindControl("cbd") as CheckBox).Checked;
            bool cbAddQuestion = (GridView1.Rows[i].FindControl("cbAddQuestion") as CheckBox).Checked;
            bool cbReadOtherAnswers = (GridView1.Rows[i].FindControl("cbReadOtherAnswers") as CheckBox).Checked;

            if (cbr || cbw || cbu || cbd || cbAddQuestion || cbReadOtherAnswers)
            {
                String formid = (GridView1.Rows[i].FindControl("Label1") as Label).Text;
                String formName = (GridView1.Rows[i].FindControl("Label2") as Label).Text;

                ORM.Group_Permissions localpermission = new ORM.Group_Permissions(formid, Request.QueryString["gid"],
                    (GridView1.Rows[i].FindControl("cbr") as CheckBox).Checked,
                    (GridView1.Rows[i].FindControl("cbw") as CheckBox).Checked,
                    (GridView1.Rows[i].FindControl("cbu") as CheckBox).Checked,
                    (GridView1.Rows[i].FindControl("cbd") as CheckBox).Checked,
                    (GridView1.Rows[i].FindControl("cbAddQuestion") as CheckBox).Checked,
                    (GridView1.Rows[i].FindControl("cbReadOtherAnswers") as CheckBox).Checked);

                Group_PermissionsLog(user, groupName, formName, cbr, cbw, cbu, cbd, cbAddQuestion, cbReadOtherAnswers);

                localgroup.Group_Permissions.Add(localpermission);
            }//end if
        }//end for

        localgroup.Save_Permission();//DELETE FROM [Group_Permissions]-- INSERT INTO [Group_Permissions]
    }//end save button click

    /// <summary>
    /// لاگ کردن تغییرات در تنظیمات فرم های گروه
    /// </summary>
    /// <param name="user"></param>
    /// <param name="groupName"></param>
    /// <param name="formName"></param>
    /// <param name="cbr"></param>
    /// <param name="cbw"></param>
    /// <param name="cbu"></param>
    /// <param name="cbd"></param>
    public static void Group_PermissionsLog
        (string user, string groupName, string formName,
        bool cbr, bool cbw, bool cbu, bool cbd,
        bool cbAddQuestion, bool cbReadOtherAnswers)
    {
        try
        {
            string insertSql = @"INSERT INTO Group_PermissionsLog
                                        (user_Name
                                        ,dateEn
                                        ,formName
                                        ,groupName
                                        ,cbr
                                        ,cbw
                                        ,cbu
                                        ,cbd
                                        ,cbAddQuestion
                                        ,cbReadOtherAnswers)
                                  VALUES
                                        (@user
                                        ,getdate()
                                        ,@formName
                                        ,@groupName
                                        ,@cbr
                                        ,@cbw
                                        ,@cbu
                                        ,@cbd
                                        ,@cbAddQuestion
                                        ,@cbReadOtherAnswers)";


            using (SqlConnection connection = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["ORM_ConnectionString"].ConnectionString))
            {
                SqlCommand com = connection.CreateCommand();
                com.CommandText = insertSql;
                com.Parameters.AddWithValue("@user", user);
                com.Parameters.AddWithValue("@formName", formName);
                com.Parameters.AddWithValue("@groupName", groupName);
                com.Parameters.AddWithValue("@cbr", cbr);
                com.Parameters.AddWithValue("@cbw", cbw);
                com.Parameters.AddWithValue("@cbu", cbu);
                com.Parameters.AddWithValue("@cbd", cbd);
                com.Parameters.AddWithValue("@cbAddQuestion", cbAddQuestion);
                com.Parameters.AddWithValue("@cbReadOtherAnswers", cbReadOtherAnswers);

                connection.Open();
                com.ExecuteNonQuery();
            }//end using

        }
        catch (SqlException er)
        {
            Alert.Show();
        }
    }//end groupPermissionLog



    protected void btnUserSetting_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserSetting.aspx?GID=" + Request.QueryString["GID"].ToString() +
           "&GName=" + Request.QueryString["GName"].ToString(), true);
    }

    protected void btnAdminSetting_Click(object sender, EventArgs e)
    {
        Response.Redirect("GroupAdminSetting.aspx?GID=" + Request.QueryString["GID"].ToString() +
           "&GName=" + Request.QueryString["GName"].ToString(), true);
    }


}
