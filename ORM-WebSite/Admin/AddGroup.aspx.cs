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

public partial class AddGroup : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtSearch.Focus();
        }
    }

    public string BeforSendUsers(object t1, object t2)
    {
        return t1.ToString() + ";" + t2.ToString();
    }
    public string BeforSendForms(object t1, object t2)
    {
        return t1.ToString() + ";" + t2.ToString();
    }

    /// <summary>
    /// ثبت اررورر در فوتر سایت
    /// </summary>
    /// <param name="message"></param>
    public void SetMessages(string message)
    {
        MasterPage master = (MasterPage)Page.Master;
        Literal lit;
        lit = (Literal)master.FindControl("litMessage");
        if (lit != null)
            lit.Text = message;
    }
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        TextBox txt1 = GridView1.FooterRow.FindControl("txtAddGroupName") as TextBox;
        TextBox txt2 = GridView1.FooterRow.FindControl("txtAddDescription") as TextBox;
        if (txt2.Text == null | txt2.Text=="")
        {
            txt2.Text = "UnKnown Descrption";
        }
        if (txt1.Text == null | txt1.Text=="")
        {
            txt1.Text = "UnKnown Group";
        }
        SqlDataSource1.InsertParameters["Description"].DefaultValue = txt2.Text;
        SqlDataSource1.InsertParameters["GroupName"].DefaultValue = txt1.Text;
        SqlDataSource1.Insert();
    }
    
    protected void lbtnUsers_Click(object sender, EventArgs e)
    {
        LinkButton buttUsers = sender as LinkButton;
        string[] ArgforSendUser = buttUsers.CommandArgument.Split(';');
        Response.Redirect("UserSetting.aspx?GID=" + ArgforSendUser[0] + "&GName=" + ArgforSendUser[1],true );
    }
    protected void lbtnForms_Click(object sender, EventArgs e)
    {
        LinkButton buttForms = sender as LinkButton;
        string[] ArgforSendForm = buttForms.CommandArgument.Split(';');
        Response.Redirect("FormSetting.aspx?GID=" + ArgforSendForm[0] + "&GName=" + ArgforSendForm[1],true );
    }
    /// <summary>
    /// اضافه کردن کاربران جدید اکتیو دیرکتوری به تیبل گروپ یوزرز
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddADUsersToGroup_Click(object sender, EventArgs e)
    {
        string Group_ID = drpListGroups.SelectedValue;
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ORM_ConnectionString"].ConnectionString);
        using (con)
        {
            string Sql = @"INSERT INTO Group_Users(Group_ID,User_Name)
                                            SELECT @Group_ID,UserName
                                            FROM [V_Personel_Hot] 
                                            WHERE UserName NOT IN (SELECT user_name FROM group_users)";

            SqlCommand cmd = new SqlCommand(Sql, con);
            try
            {
                con.Open();
                cmd.CommandText = Sql;
                cmd.Parameters.AddWithValue("@Group_ID", Group_ID);

                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception er)
            {
                con.Close();
                Alert.Show();
                SetMessages(er.Message);
            }
        }//end using
    }//end btnAddAllUsersToGroup_Click



    protected void btnDefineGroupAdmin_Click(object sender, EventArgs e)
    {
        LinkButton buttForms = sender as LinkButton;
        string[] ArgforSendForm = buttForms.CommandArgument.Split(';');
        Response.Redirect("GroupAdminSetting.aspx?GID=" + ArgforSendForm[0] + "&GName=" + ArgforSendForm[1], true);
    }

}//end class
