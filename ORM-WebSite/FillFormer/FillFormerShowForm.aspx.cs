using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ExcelGeneratingClass;
using System.Web.UI.WebControls.WebParts;
using ORM;
using System.Globalization;
using RSRC;

public partial class FillFormerShowForm : System.Web.UI.Page
{
    bool refresh = false;
    bool _ReadOtherAnswers = false;
    DataTable Forms
    {
        get
        {
            if (ViewState["Forms"] == null)
                return null;
            else
                return (DataTable)ViewState["Forms"];
        }
        set
        {
            if (ViewState["Forms"] == null)
                ViewState.Add("Forms", value);
            else
                ViewState["Forms"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        var FID = Request.QueryString["FID"];
        if (ORM.Group_Permissions.Get_Permission(FID, RSUsers.risk) == null ||
            !ORM.Group_Permissions.Get_Permission(FID, RSUsers.risk)._readable||
            FID == null)
            Response.Redirect("error.aspx", true);
        else
        {
            this.Forms = ORM.Forms.get_Forms_By_Permission(RSUsers.risk);
            tblname.DataSource = this.Forms;
            tblname.DataTextField = "FormName";
            tblname.DataValueField = "FormID";
            tblname.DataBind();
            for (int i = 0; i < tblname.Items.Count; i++)
            {
                if (tblname.Items[i].Value.ToString() == FID)
                    tblname.SelectedIndex = i;
            }
            tblname_SelectedIndexChanged(null, null);//bug_fix:Iman              
        }
    }//end pageLoad

    #region functions

    private void Binder()
    {
        String Sql_Query = Query_builder();
        var dt = new DataTable();
        if (txtFromDate.Value.Length > 7 && txtToDate.Value.Length > 7)
        {
            var FromDate = txtFromDate.Value.ConvertToMiladi();
            var ToDate = txtToDate.Value.ConvertToMiladi();

            dt = ORM.Form_Instance.get_dt_answersBetweenDates(
                tblname.SelectedValue, "", Sql_Query, FromDate, ToDate);
        }
        else
            dt = ORM.Form_Instance.get_dt_answers_unordered(tblname.SelectedValue, "", Sql_Query);

        ORM.Forms newform = new ORM.Forms(tblname.SelectedValue);
        SQL_Connector connector = new SQL_Connector();
        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(newform.Get_Answer_query());
        command.Parameters.Clear();
        command.Parameters.Add("@from_date", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.MinValue;
        command.Parameters.Add("@to_date", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.MaxValue;


        DataRow[] dr;
        dr = this.Forms.Select("FormID = " + tblname.SelectedValue);

        if (Convert.ToBoolean(dr[0]["Deleteable"]))
            GridView_Form_Instance.Columns[6].Visible = true;
        else
            GridView_Form_Instance.Columns[6].Visible = false;

        if (Convert.ToBoolean(dr[0]["Updateable"]))
            GridView_Form_Instance.Columns[7].Visible = true;
        else
            GridView_Form_Instance.Columns[7].Visible = false;

        GridView_Form_Instance.DataSource = dt;
        String[] Keys = { "Instance_ID" };
        GridView_Form_Instance.DataKeyNames = Keys;
        GridView_Form_Instance.DataBind();

        Btn_Reported.Visible = true;
        lblSearch.Visible = true;
        txtSearch.Visible = true;
        txtSearch.Text = string.Empty;//txtSearchخالی کردن 
        btnSearch.Visible = true;
    }

    private void setActiveViewIndex(int a)
    {
        WhereView.ActiveViewIndex = a;
    }

    void update_controls()
    {

        if (WhereColname.SelectedIndex == -1)
            return;

        ORM.ORM_Types.Question_Type qtype = ORM.Question.get_type(WhereColname.SelectedValue);
        ORM.ORM_Types.Template_Type ttype = ORM.Question.get_template(WhereColname.SelectedValue);

        switch (qtype)
        {
            case ORM.ORM_Types.Question_Type.Text:
                setActiveViewIndex(0);
                break;
            case ORM.ORM_Types.Question_Type.Number:
                setActiveViewIndex(1);
                break;
            case ORM.ORM_Types.Question_Type.Date:
                setActiveViewIndex(2);
                break;
            case ORM.ORM_Types.Question_Type.ComboBoxList:
            case ORM.ORM_Types.Question_Type.DropDownList:
            case ORM.ORM_Types.Question_Type.RadioButtonList:
            case ORM.ORM_Types.Question_Type.ChckBxLstTxt:
            case ORM.ORM_Types.Question_Type.DrpDnLstTxt:
            case ORM.ORM_Types.Question_Type.RdoBtnLstTxt:
                if (ttype == ORM.ORM_Types.Template_Type.Branch)
                    WhereView.ActiveViewIndex = 5;
                else if (ttype == ORM.ORM_Types.Template_Type.State)
                    WhereView.ActiveViewIndex = 4;
                else
                {
                    WhereView.ActiveViewIndex = 3;
                    WherevalueList.DataSource = ORM.List.get_dt_items(WhereColname.SelectedValue);
                    WherevalueList.DataTextField = "Item_Text";
                    WherevalueList.DataValueField = "Item_ID";
                    WherevalueList.DataBind();
                }
                break;
        }


    }
    String Query_builder()
    {
        String Sql_Query = "";
        if (Roles.IsUserInRole("Viewer"))
            Sql_Query = " and Dep_ID = 6";

#warning "_ReadOtherAnswers"
        ORM.Forms form = new ORM.Forms(tblname.SelectedValue);
        String FID = form.Form_ID;

        _ReadOtherAnswers = ORM.Group_Permissions.Get_Permission(
            FID, RSUsers.risk)._ReadOtherAnswers;

        if (!_ReadOtherAnswers)
            Sql_Query = " and [User_name]=N'" + RSUsers.risk + "' ";

        if (!chkSearch.Checked)
            return Sql_Query;
        ORM.ORM_Types.Question_Type qtype = ORM.Question.get_type(WhereColname.SelectedValue);
        ORM.ORM_Types.Template_Type ttype = ORM.Question.get_template(WhereColname.SelectedValue);
        switch (qtype)
        {
            case ORM.ORM_Types.Question_Type.Text:
                if (WherecondopNvar.SelectedValue == "Like" || WherecondopNvar.SelectedValue == "Not Like")
                {

                    Sql_Query += " AND [Text]" + WherecondopNvar.SelectedValue + " '%'+N'" + WherevalueNvar.Text + "'+'%' ";
                }
                else
                {
                    Sql_Query += " AND [Text]" + WherecondopNvar.SelectedValue + "N'" + WherevalueNvar.Text + "'";
                }

                break;
            case ORM.ORM_Types.Question_Type.Number:
                Sql_Query += " AND [Number]" + WherecondopNum.SelectedValue + WherevalueNum.Text + " ";
                break;
            case ORM.ORM_Types.Question_Type.Date:
                DateTime dt = DateTime.Now;
                if (WherevalueDate.DateValue == null || !DateTime.TryParse(WherevalueDate.DateValue.ToString(), out dt))
                    return "";

                Sql_Query += " AND DateDiff(d,Date,'" + ((DateTime)WherevalueDate.DateValue).ToString("d") + "') "
                        + WherecondopDate.SelectedValue + " 0 ";
                break;
            case ORM.ORM_Types.Question_Type.ComboBoxList:
            case ORM.ORM_Types.Question_Type.DropDownList:
            case ORM.ORM_Types.Question_Type.RadioButtonList:
                if (ttype == ORM.ORM_Types.Template_Type.Manual)
                {
                    if (WherevalueList.SelectedItem == null)
                        return "";
                    Sql_Query += " AND Selected_Item " + WherecondopList.SelectedValue + " " + WherevalueList.SelectedValue;
                }
                else if (ttype == ORM.ORM_Types.Template_Type.State)
                {
                    if (WherevalueLoc.SelectedItem == null)
                        return "";

                    if (WherevalueList.SelectedItem == null)
                        return "";
                    Sql_Query += " AND State_Item " + WherecondopLoc.SelectedValue + " " + WherevalueLoc.SelectedValue;
                }
                else if (WhereView.ActiveViewIndex == 5)
                {
                    if (WherevalueBranch.SelectedItem == null)
                        return "";

                    Sql_Query += " AND Branch_Item " + WherecondopBranch.SelectedValue + " " + WherevalueBranch.SelectedValue;

                }
                break;

        }

        return Sql_Query;
    }

    private void Filtered_Binder()
    {
        String Sql_Query = Query_builder();
        if (txtSearch.Text.Length > 0)
        {
            Sql_Query += " and User_name like '%" + txtSearch.Text + "%'";

            DataTable dtSearch = new DataTable();

            if (txtFromDate.Value.Length > 7 && txtToDate.Value.Length > 7)
            {
                var FromDate = txtFromDate.Value.ConvertToMiladi();
                var ToDate = txtToDate.Value.ConvertToMiladi();

                dtSearch = ORM.Form_Instance.get_dt_answersBetweenDates(
                 tblname.SelectedValue, WhereColname.SelectedValue, Sql_Query, FromDate, ToDate);
            }
            else
                dtSearch = ORM.Form_Instance.get_dt_answers_unordered(
                   tblname.SelectedValue, WhereColname.SelectedValue, Sql_Query);

            DataRow[] dr;
            dr = this.Forms.Select("FormID = " + tblname.SelectedValue);

            if (Convert.ToBoolean(dr[0]["Deleteable"]))
                GridView_Form_Instance.Columns[6].Visible = true;
            else
                GridView_Form_Instance.Columns[6].Visible = false;

            if (Convert.ToBoolean(dr[0]["Updateable"]))
                GridView_Form_Instance.Columns[7].Visible = true;
            else
                GridView_Form_Instance.Columns[7].Visible = false;

            GridView_Form_Instance.DataSource = null;
            GridView_Form_Instance.DataSource = dtSearch;
        }
        else//empty txtSearch 
        {

            DataTable dtSearch = new DataTable();

            if (txtFromDate.Value.Length > 7 && txtToDate.Value.Length > 7)
            {
                var FromDate = txtFromDate.Value.ConvertToMiladi();
                var ToDate = txtToDate.Value.ConvertToMiladi();

                dtSearch = ORM.Form_Instance.get_dt_answersBetweenDates(
                 tblname.SelectedValue, WhereColname.SelectedValue, Sql_Query, FromDate, ToDate);
            }
            else
                dtSearch = ORM.Form_Instance.get_dt_answers_unordered(
                   tblname.SelectedValue, WhereColname.SelectedValue, Sql_Query);

            DataRow[] dr;
            dr = this.Forms.Select("FormID = " + tblname.SelectedValue);

            if (Convert.ToBoolean(dr[0]["Deleteable"]))
                GridView_Form_Instance.Columns[6].Visible = true;
            else
                GridView_Form_Instance.Columns[6].Visible = false;

            if (Convert.ToBoolean(dr[0]["Updateable"]))
                GridView_Form_Instance.Columns[7].Visible = true;
            else
                GridView_Form_Instance.Columns[7].Visible = false;

            GridView_Form_Instance.DataSource = null;
            GridView_Form_Instance.DataSource = dtSearch;
        }
        GridView_Form_Instance.DataBind();
    }

    #endregion functions

    #region Events
    protected void ApplyOptionTitles(object sender, EventArgs e)
    {
        if (sender as DropDownList != null)
        {
            DropDownList ddl = sender as DropDownList;
            if (ddl != null)
            {
                foreach (ListItem item in ddl.Items)
                {
                    item.Attributes["title"] = item.Text;
                }
            }
        }
        else if (sender as ListBox != null)
        {
            ListBox ddl = sender as ListBox;
            if (ddl != null)
            {
                foreach (ListItem item in ddl.Items)
                {
                    item.Attributes["title"] = item.Text;
                }
            }
        }
    }

    protected void tblname_DataBound(object sender, EventArgs e)
    {
        foreach (ListItem item in tblname.Items)
        {
            item.Attributes["title"] = item.Text;
        }
        if (tblname.Items.Count > 0)
        {
            tblname.SelectedIndex = 0;
            ORM.Forms form = new ORM.Forms(tblname.SelectedValue);
            WhereColname.DataSource = ORM.Question.Get_DT_Questions(tblname.SelectedValue);
            WhereColname.DataTextField = "Que";
            WhereColname.DataValueField = "QID";
        }
        WhereColname.DataBind();
    }

    protected void Wherecolname_SelectedIndexChanged(object sender, EventArgs e)
    {
        update_controls();
    }

    protected void WhereColname_DataBound(object sender, EventArgs e)
    {
        update_controls();
    }
    protected void tblname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ORM.Forms form = new ORM.Forms(tblname.SelectedValue);
        if (tblname.SelectedIndex == -1)
            return;
        WhereColname.DataSource = ORM.Question.Get_DT_Questions(tblname.SelectedValue);
        WhereColname.DataTextField = "Que";
        WhereColname.DataValueField = "QID";
        WhereColname.DataBind();
        //GridView1.Visible = false;

    }

   

    protected void IB_saveexcel_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable("Answers");
        dt.Columns.Add("نام کاربری");
        dt.Columns.Add("تاریخ ثبت");

        ORM.Forms form = new ORM.Forms(tblname.SelectedValue);
        foreach (ORM.Question q in form.Form_Question)
        {
            dt.Columns.Add(q.Description);
        }

        String Sql_Query = Query_builder();
        DataTable dt2 = ORM.Form_Instance.get_dt_answersForExcel(tblname.SelectedValue, Sql_Query);

        for (int j = 0; j < dt2.Rows.Count; j++)
        {
            ORM.Form_Instance instance = new ORM.Form_Instance(dt2.Rows[j]["Instance_ID"].ToString());
            dt.Rows.Add(dt2.Rows[j]["User_name"].ToString());
            dt.Rows[dt.Rows.Count - 1]["تاریخ ثبت"] = dt2.Rows[j]["filldate"].ToString();
            foreach (ORM.Form_Question_Instance q in instance.Answers)
            {
                dt.Rows[dt.Rows.Count - 1][new ORM.Question(q.Question_ID).Description] = q.getString();
            }

        }
        string filename = "Answers.xml";
        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
        this.EnableViewState = false;

        System.IO.StringWriter writer = new System.IO.StringWriter();
        dt.WriteXml(writer, true);
        Response.Write(writer);
        Response.End();
    }

    protected void ID_download_Click(object sender, EventArgs e)
    {
        const string INSTANCE_TITLE_FORMAT_NAME = "instance";
        const string INSTANCE_HEADER_FORMAT_NAME = "greyBackground";
        const string QUESTION_TITLE_FORMAT_NAME = "question";
        const string TABLE_TITLE_FORMAT_NAME = "tabletitle";
        const string CELL_FONT_FORMAT = "answer";
        string path = "";

        string fontName = "Arial";
        SQL_Connector connector = new SQL_Connector();
        DataTable dt = new DataTable("Answers");
        dt.Columns.Add("نام کاربری");
        dt.Columns.Add("تاریخ ثبت");
        ORM.Forms form = new ORM.Forms(tblname.SelectedValue);
        foreach (ORM.Question q in form.Form_Question)
        {
            dt.Columns.Add(q.Description);
            dt.Columns[q.Description].Caption = q.Question_ID.ToString();
        }
        XmlExcelHelper helper = new XmlExcelHelper(path, fontName, 12);
        helper.AddStringStyle(INSTANCE_TITLE_FORMAT_NAME, fontName, 13, "#6600FF", "#CC9900", true);
        helper.AddStringStyle(INSTANCE_HEADER_FORMAT_NAME, fontName, 14, "#800000", "#CC9900", true);
        helper.AddStringStyle(QUESTION_TITLE_FORMAT_NAME, fontName, 14, "#660033", "#FFFF99", true);
        helper.AddStringStyle(TABLE_TITLE_FORMAT_NAME, fontName, 14, "#663300", "#FFCC99", true);
        helper.AddStringStyle(CELL_FONT_FORMAT, fontName, 12, "#000000", false);
        String Sql_Query = Query_builder();
        String Sql_Tables = "";
        DataTable dt2 = ORM.Form_Instance.get_dt_answersForExcel(tblname.SelectedValue, Sql_Query);
        for (int j = 0; j < dt2.Rows.Count; j++)
        {
            Sql_Query = Query_builder();
            Sql_Tables = "";
            helper.CreateSheet(dt2.Rows[j]["filldate"].ToString().Substring(0, 10).Replace("/", "") + "_" + (j + 1).ToString());
            ORM.Form_Instance instance = new ORM.Form_Instance(dt2.Rows[j]["Instance_ID"].ToString());
            if (Sql_Query.Contains("Where"))
                Sql_Tables = Sql_Query + "ORDER BY User_name,Form_Instance_ID,Question_Index,Answer_ID";
            else
                Sql_Tables = Sql_Query + "AND Instance_ID = " + dt2.Rows[j]["Instance_ID"].ToString() + " ORDER BY User_name,Form_Instance_ID,Question_Index,Answer_ID";
            DataTable dt3 = ORM.Form_Instance.get_dt_allanswersForExcel(tblname.SelectedValue, Sql_Tables);

            dt.Rows.Add(dt2.Rows[j]["User_name"].ToString());
            dt.Rows[dt.Rows.Count - 1]["تاریخ ثبت"] = dt2.Rows[j]["filldate"].ToString();
            foreach (ORM.Form_Question_Instance q in instance.Answers)
            {
                dt.Rows[dt.Rows.Count - 1][new ORM.Question(q.Question_ID).Description] = q.getString();
            }
            helper.AddRow();
            helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_TITLE_FORMAT_NAME, dt.Columns[0].ToString());
            helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_HEADER_FORMAT_NAME, dt.Rows[0][0].ToString());
            helper.AddRow();
            helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_TITLE_FORMAT_NAME, dt.Columns[1].ToString());
            helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_HEADER_FORMAT_NAME, dt.Rows[0][1].ToString());
            helper.AddRow();

            DataRow[] dr;
            for (int k = 2; k < dt.Columns.Count; k++)
            {
                dr = dt3.Select("Question_ID = " + dt.Columns[k].Caption);
                if (dr.GetLength(0) > 0 && Convert.ToInt32(dr[0]["Question_Type"]) != 7)
                {
                    helper.AddRow();
                    helper.AddCell(XmlExcelHelper.CellType.String, "");
                    helper.AddCell(XmlExcelHelper.CellType.String, "");
                    helper.AddCell(XmlExcelHelper.CellType.String, QUESTION_TITLE_FORMAT_NAME, dt.Columns[k].ToString());
                    helper.AddCell(XmlExcelHelper.CellType.String, CELL_FONT_FORMAT, dt.Rows[0][k].ToString());
                }
                else if (dr.GetLength(0) > 0)
                {
                    connector = new SQL_Connector();
                    Sql_Query = "select A.Item_ID,A.Item_Text,A.Item_Type,A.List_ID,B.Question_ID " +
                                       "from dbo.List_Items A,dbo.List B " +
                                       "where A.List_ID = B.List_ID And B.Question_ID = " + dt.Columns[k].Caption;
                    DataTable dtCol = connector.Select(Sql_Query);
                    helper.AddRow();
                    helper.AddCell(XmlExcelHelper.CellType.String, "");
                    helper.AddCell(XmlExcelHelper.CellType.String, "");
                    helper.AddCell(XmlExcelHelper.CellType.String, QUESTION_TITLE_FORMAT_NAME, dt.Columns[k].ToString());

                    for (int m = 0; m < dtCol.Rows.Count; m++)
                    {
                        helper.AddCell(XmlExcelHelper.CellType.String, TABLE_TITLE_FORMAT_NAME, dtCol.Rows[m]["Item_Text"].ToString());
                    }

                    helper.AddRow();
                    helper.AddCell(XmlExcelHelper.CellType.String, "");
                    helper.AddCell(XmlExcelHelper.CellType.String, "");
                    helper.AddCell(XmlExcelHelper.CellType.String, "");
                    helper.AddCell(XmlExcelHelper.CellType.String, CELL_FONT_FORMAT, dr[0]["Text"].ToString());

                    int pos = 1;
                    for (int i = 1; i < dr.GetLength(0); i++)
                    {
                        if (Convert.ToInt16(dr[i]["Answer_ID"]) == Convert.ToInt16(dr[i - 1]["Answer_ID"]))
                        {
                            for (int m = 0; m < dtCol.Rows.Count; m++)
                            {
                                if (dr[i]["Item_Text"].ToString() == dtCol.Rows[m]["Item_Text"].ToString())
                                {
                                    for (int l = 0; l < m - pos; l++)
                                    {
                                        helper.AddCell(XmlExcelHelper.CellType.String, "");
                                        pos += 1;
                                    }
                                    helper.AddCell(XmlExcelHelper.CellType.String, CELL_FONT_FORMAT, dr[i]["Text"].ToString());
                                    pos += 1;

                                }
                            }

                        }
                        else
                        {
                            helper.AddRow();
                            helper.AddCell(XmlExcelHelper.CellType.String, "");
                            helper.AddCell(XmlExcelHelper.CellType.String, "");
                            helper.AddCell(XmlExcelHelper.CellType.String, "");
                            helper.AddCell(XmlExcelHelper.CellType.String, CELL_FONT_FORMAT, dr[i]["Text"].ToString());
                            pos = 1;
                        }

                    }
                }

            }

        }
        string filename = "Answers.xml";
        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
        this.EnableViewState = false;
        Response.Write(helper.ExcelFileXml);
        Response.End();


    }
    protected void Dep_selectall_CheckedChanged(object sender, EventArgs e)
    {
        WhereColname.Enabled = chkSearch.Checked;
        WhereView.Visible = chkSearch.Checked;
    }


    protected void Btn_Reported_Click(object sender, EventArgs e)
    {
        SQL_Connector sConn = new SQL_Connector();
        for (int i = 0; i < GridView_Form_Instance.Rows.Count; i++)
        {
            String Sql_Query = "UPDATE [FG_DB].[dbo].[Form_Instance] SET [Reported] = '";
            CheckBox myCheck = new CheckBox();
            myCheck = (CheckBox)GridView_Form_Instance.Rows[i].FindControl("Chk01");
            Sql_Query += myCheck.Checked + "' WHERE Instance_ID = " + myCheck.ToolTip;
            sConn.Execute(Sql_Query);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView_Form_Instance.PageIndex = 0;//every time user clicks the search button go to first page (page index = 0)
        GridView_Form_Instance.SelectedIndex = -1;
        Filtered_Binder();
    }

    protected void btnSearchAnswers_Click(object sender, EventArgs e)
    {
        txtFromDate.Value = "";
        txtToDate.Value = "";
        GridView_Form_Instance.PageIndex = 0;
        Binder();
    }

    protected void btnXML_Save_Click(object sender, EventArgs e)
    {
        var dt = new DataTable("Answers");
        var form = new ORM.Forms(tblname.SelectedValue);
        //سطر هدر
        dt.Columns.Add("تاریخ ثبت");
        dt.Columns.Add("نام کاربری");
        dt.Columns.Add("کد محل خدمت");
        foreach (ORM.Question q in form.Form_Question)
        {
            dt.Columns.Add(q.Description);
        }

        var connector = new SQL_Connector();
        var Sql_query = "SELECT [USER_NAME] ,[CodeMahalKhedmat] ," +
            "[filldate] ,[Instance_ID] ,[Form_ID],[Form_Instance_User_name] " +
            "FROM V_Instance_Branch_Answers where 1=2 ";
        for (int i = 0; i < GridView_Form_Instance.Rows.Count; i++)
        {
            var chk_Form_Instance = (CheckBox)GridView_Form_Instance
                .Rows[i].FindControl("chk_Form_Instance");
            if (chk_Form_Instance.Checked == true)
            {
                var lblInstance_ID = (Label)GridView_Form_Instance.Rows[i]
                    .FindControl("lblInstance_ID");
                var selectedInstance_ID = lblInstance_ID.Text;
                Sql_query += " or Instance_ID = " + selectedInstance_ID;
            }
        }

        var dt_Answers = connector.Select(Sql_query);
        foreach (DataRow dr in dt_Answers.Rows)
        {
            #region ForDt_Answers
            var codeMahalKhedmat = (dr["CodeMahalKhedmat"].ToString().Length > 0) ?
                dr["CodeMahalKhedmat"].ToString() :
                "--";

            var User_Name = (dr["User_name"].ToString().Length > 0) ?
                                dr["User_name"].ToString() :
                                dr["Form_Instance_User_name"].ToString();

            var form_id = dr["Form_ID"].ToString();
            var Instance_ID = dr["Instance_ID"].ToString();
            var filldate = dr["filldate"].ToString();
            var farsidate = OrmUtility.ConvertToShamsi(filldate);

            //سطر زیر هدر
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1]["تاریخ ثبت"] = farsidate;
            dt.Rows[dt.Rows.Count - 1]["نام کاربری"] = User_Name;
            dt.Rows[dt.Rows.Count - 1]["کد محل خدمت"] = codeMahalKhedmat;

            var temp = new Form_Question_Instance();
            var AnswersList = temp.get_answers(Instance_ID, form_id);
            //AnswersList = temp.get_answers(Instance_ID, "137");
            foreach (ORM.Form_Question_Instance q in AnswersList)
            {
                //      [شماره-نام- ستون]              [شماره سطر]
                dt.Rows[dt.Rows.Count - 1][new ORM.Question(q.Question_ID).Description] = q.getString();
            }
            #endregion
        }//end foreach View_Instance_Branch_Answers

        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Answers.xml");

        var writer = new System.IO.StringWriter();
        dt.WriteXml(writer, XmlWriteMode.IgnoreSchema, false);
        Response.Write(writer);
        Response.End();

    }//end btnXML_Save_Click

    protected void btnXMLTop_Click(object sender, EventArgs e)
    {
        DataTable dtFormQuestions = new DataTable("Answers");
        dtFormQuestions.Columns.Add("نام کاربری");
        dtFormQuestions.Columns.Add("تاریخ ثبت");
        dtFormQuestions.Columns.Add("کد محل خدمت");

        ORM.Forms form = new ORM.Forms(tblname.SelectedValue);
        foreach (ORM.Question q in form.Form_Question)
        {
            dtFormQuestions.Columns.Add(q.Description);
        }

        String Sql_Query = Query_builder();
        //View_Instance_Branch_Answers

        DataTable dtAnswers = new DataTable();

        if (txtCount.Text.Length > 0)
        {
            dtAnswers = ORM.Form_Instance.GetDtAnswersCodeKhedmatTop(tblname.SelectedValue, Sql_Query, txtCount.Text);
        }
        else
        {
            dtAnswers = ORM.Form_Instance.GetDtAnswersCodeKhedmatTop(tblname.SelectedValue, Sql_Query, 10.ToString());
        }

        for (int j = 0; j < dtAnswers.Rows.Count; j++)
        {
            DateTime fillDate = Convert.ToDateTime(dtAnswers.Rows[j]["filldate"]);
            string farsiDate = OrmUtility.ConvertToShamsi(fillDate.ToString());

            dtFormQuestions.Rows.Add();
            dtFormQuestions.Rows[dtFormQuestions.Rows.Count - 1]["نام کاربری"] = dtAnswers.Rows[j]["Form_Instance_User_name"].ToString();
            dtFormQuestions.Rows[dtFormQuestions.Rows.Count - 1]["تاریخ ثبت"] = farsiDate;
            dtFormQuestions.Rows[dtFormQuestions.Rows.Count - 1]["کد محل خدمت"] = dtAnswers.Rows[j]["CodeMahalKhedmat"];


            ORM.Form_Instance instance = new ORM.Form_Instance(dtAnswers.Rows[j]["Instance_ID"].ToString());
            foreach (ORM.Form_Question_Instance q in instance.Answers)
            {
                //                                [شماره-نام- ستون]       [شماره سطر]
                dtFormQuestions.Rows[dtFormQuestions.Rows.Count - 1][new ORM.Question(q.Question_ID).Description] = q.getString();
            }

        }
        if (dtAnswers.Rows.Count == 0)
        {
            Alert.Show();
        }
        else
        {

            string filename = "Answers.xml";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dtFormQuestions.WriteXml(writer, XmlWriteMode.IgnoreSchema, false);

            Response.Write(writer.ToString());

            Response.Flush();
            Response.End();
        }
    }//end btnXMLTop_Click


    protected void btnXmlBetweenDates_Click(object sender, EventArgs e)
    {
        DataTable dtFormQuestions = new DataTable("Answers");
        dtFormQuestions.Columns.Add("نام کاربری");
        dtFormQuestions.Columns.Add("تاریخ ثبت");
        dtFormQuestions.Columns.Add("کد محل خدمت");

        ORM.Forms form = new ORM.Forms(tblname.SelectedValue);
        foreach (ORM.Question q in form.Form_Question)
        {
            dtFormQuestions.Columns.Add(q.Description);
        }

        String Sql_Query = Query_builder();

        var dtAnswers = new DataTable();

        if (txtFromDate.Value.Length > 7 && txtToDate.Value.Length > 7)
        {
            var FromDate = txtFromDate.Value.ConvertToMiladi();
            var ToDate = txtToDate.Value.ConvertToMiladi();

            dtAnswers = ORM.Form_Instance.get_dt_answersForExcelBetweenDates(
                tblname.SelectedValue, Sql_Query, FromDate, ToDate);
        }
        else
        {
            if (txtCount.Text.Length > 0)
            {
                dtAnswers = ORM.Form_Instance.GetDtAnswersCodeKhedmatTop(tblname.SelectedValue, Sql_Query, txtCount.Text);
            }
            else
            {
                dtAnswers = ORM.Form_Instance.GetDtAnswersCodeKhedmatTop(tblname.SelectedValue, Sql_Query, 10.ToString());
            }
        }

        for (int j = 0; j < dtAnswers.Rows.Count; j++)
        {
            DateTime fillDate = Convert.ToDateTime(dtAnswers.Rows[j]["filldate"]);
            string farsiDate = OrmUtility.ConvertToShamsi(fillDate.ToString());

            dtFormQuestions.Rows.Add();
            dtFormQuestions.Rows[dtFormQuestions.Rows.Count - 1]["نام کاربری"] = dtAnswers.Rows[j]["Form_Instance_User_name"].ToString();
            dtFormQuestions.Rows[dtFormQuestions.Rows.Count - 1]["تاریخ ثبت"] = farsiDate;
            dtFormQuestions.Rows[dtFormQuestions.Rows.Count - 1]["کد محل خدمت"] = dtAnswers.Rows[j]["CodeMahalKhedmat"];


            ORM.Form_Instance instance = new ORM.Form_Instance(dtAnswers.Rows[j]["Instance_ID"].ToString());
            foreach (ORM.Form_Question_Instance q in instance.Answers)
            {
                //                                [شماره-نام- ستون]       [شماره سطر]
                dtFormQuestions.Rows[dtFormQuestions.Rows.Count - 1][new ORM.Question(q.Question_ID).Description] = q.getString();
            }

        }
        if (dtAnswers.Rows.Count == 0)
        {
            Alert.Show();
        }
        else
        {

            string filename = "Answers.xml";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dtFormQuestions.WriteXml(writer, XmlWriteMode.IgnoreSchema, false);

            Response.Write(writer.ToString());

            Response.Flush();
            Response.End();
        }
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {

    }

    protected void GridView_Form_Instance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Show_Instance")
        {
            var SelectedRowInstance_ID = e.CommandArgument.ToString();
            var codeMahalKhedmat = "--";

            for (int i = 0; i < HttpContext.Current.Session.Keys.Count; i++)
            {
                if (HttpContext.Current.Session.Keys[i].ToString().Contains("rad"))
                {
                    HttpContext.Current.Session[HttpContext.Current.Session.Keys[i].ToString()] = "";
                }
                if (HttpContext.Current.Session.Keys[i].ToString().Contains("GV"))
                {
                    HttpContext.Current.Session[HttpContext.Current.Session.Keys[i]] = null;
                }
            }
            ORM.Forms instanceform = new ORM.Forms(tblname.SelectedValue);
            ORM.HTML_Forms form = new ORM.HTML_Forms(instanceform, this);
            panelHtmlForms.Controls.Clear();
            panelHtmlForms.Controls.Add(form.Fill_Panel(e.CommandArgument.ToString(), codeMahalKhedmat));
            panelHtmlForms.Enabled = false;
            panelHtmlForms.Visible = true;
            return;
        }

        if (e.CommandName == "Delete")
        {
            ORM.Form_Instance.Delete(e.CommandArgument.ToString());
            Binder();
            return;
        }

        if (e.CommandName == "Edit")
        {
            Response.Redirect("FllEdit_Answers.aspx?FID=" + tblname.SelectedValue + "&&Ansid=" + e.CommandArgument.ToString());
            return;
        }
    }

    protected void GridView_Form_Instance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_Form_Instance.SelectedIndex = -1;
        GridView_Form_Instance.PageIndex = e.NewPageIndex;
        //مشکل انتخاب شدن کلیه رکورد ها در هنگام تعویض اندیس گرید  
        //Binder();
        Filtered_Binder();
    }
    protected void GridView_Form_Instance_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnSearchBetweenDates_Click(object sender, EventArgs e)
    {
        GridView_Form_Instance.PageIndex = 0;
        Binder();
    }

    #endregion  Events


}
