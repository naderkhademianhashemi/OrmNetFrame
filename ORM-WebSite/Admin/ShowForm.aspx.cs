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
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelGeneratingClass;
using System.Globalization;
using ORM;
using System.Collections.Generic;
using System.Linq;

public partial class ShowForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        tblname.DataBind();
        String FID = Request.QueryString["Fid"];
        for (int i = 0; i < tblname.Items.Count; i++)
        {
            if (tblname.Items[i].Value.ToString() == FID)
                tblname.SelectedIndex = i;
        }
        if (tblname.SelectedIndex != -1)
            tblname_SelectedIndexChanged(null, null);//bug_fix:Iman

        Dep_selectall_CheckedChanged(null, null);
    }

    #region Functions
    public bool IsCHK(string rpt)
    {
        return rpt == "True" ? true : false;
    }
    /// <summary>
    /// Bind GridView_Form_Instance to Answers View in FG_DB filterd by user_name
    /// </summary>
    protected void Filtered_Binder()
    {
        String Sql_Query = Query_builder();
        var dt = new DataTable();

        if (txtSearch.Text.Length > 0)
            Sql_Query += " and User_name like '%" + txtSearch.Text + "%'";
        if (txtFromDate.Value.Length > 7 && txtToDate.Value.Length > 7)
        {
            var FromDate = txtFromDate.Value.ConvertToMiladi();
            var ToDate = txtToDate.Value.ConvertToMiladi();
            dt = ORM.Form_Instance.get_dt_answersBetweenDates(
                tblname.SelectedValue, WhereColname.SelectedValue, Sql_Query, FromDate, ToDate);
        }
        else
            dt = ORM.Form_Instance.get_dt_answers_unordered(tblname.SelectedValue,
                WhereColname.SelectedValue, Sql_Query);

        GridView_Form_Instance.DataSource = null;
        GridView_Form_Instance.DataSource = dt;
        GridView_Form_Instance.DataBind();
    }

    private void Binder()
    {
        String Sql_Query = Query_builder();
        var dt = new DataTable();
        if (txtFromDate.Value.Length > 7 && txtToDate.Value.Length > 7)
        {
            var FromDate = txtFromDate.Value.ConvertToMiladi();
            var ToDate = txtToDate.Value.ConvertToMiladi();
            dt = ORM.Form_Instance.get_dt_answersBetweenDates(
                tblname.SelectedValue, WhereColname.SelectedValue, Sql_Query, FromDate, ToDate);
        }
        else
            dt = ORM.Form_Instance.get_dt_answers_unordered(tblname.SelectedValue, WhereColname.SelectedValue, Sql_Query);

        GridView_Form_Instance.DataSource = dt;
        String[] Keys = { "Instance_ID" };
        GridView_Form_Instance.DataKeyNames = Keys;
        GridView_Form_Instance.PageIndex = 0;//(آمدن به صفحه اول گرید)GridView_Form_Instanceصفر کردن اندیس 
        GridView_Form_Instance.DataBind();
        GridView_Form_Instance.Visible = true;
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
    String Query_builder()
    {
        String Sql_Query = "";
        if (!chkSearch.Checked)
            return Sql_Query;
        ORM.ORM_Types.Question_Type qtype = ORM.Question.get_type(WhereColname.SelectedValue);
        ORM.ORM_Types.Template_Type ttype = ORM.Question.get_template(WhereColname.SelectedValue);
        switch (qtype)
        {
            case ORM.ORM_Types.Question_Type.Text:
                if (WherevalueNvar.Text == "") break;
                if (WherecondopNvar.SelectedValue == "Like")
                {
                    Sql_Query += " AND [Text]" + WherecondopNvar.SelectedValue + " '%'+N'" + WherevalueNvar.Text + "'+'%' ";
                }
                else
                    if (WherecondopNvar.SelectedValue == "NOT Like")
                {
                    Sql_Query += " AND ([Text]" + WherecondopNvar.SelectedValue + " '%'+N'" + WherevalueNvar.Text + "'+'%' OR [Text] is null) ";
                }
                else
                        if (WherecondopNvar.SelectedValue == "<>")
                    Sql_Query += " AND ([Text]" + WherecondopNvar.SelectedValue + "N'" + WherevalueNvar.Text + "' OR [Text] is null) ";
                else
                    Sql_Query += " AND [Text]" + WherecondopNvar.SelectedValue + "N'" + WherevalueNvar.Text + "'";

                break;
            case ORM.ORM_Types.Question_Type.Number:
                if (WherevalueNum.Text == "") break;
                Sql_Query += " AND [Number]" + WherecondopNum.SelectedValue + WherevalueNum.Text + " ";
                break;
            case ORM.ORM_Types.Question_Type.Date:
                DateTime dt = DateTime.Now;
                if (WherevalueDate.DateValue == null || !DateTime.TryParse(WherevalueDate.DateValue.ToString(), out dt))
                    return "";

                Sql_Query = " AND DateDiff(d,Date,'" + ((DateTime)WherevalueDate.DateValue).ToString("d") + "') "
                        + WherecondopDate.SelectedValue + " 0 ";
                break;
            case ORM.ORM_Types.Question_Type.ComboBoxList:
            case ORM.ORM_Types.Question_Type.DropDownList:
            case ORM.ORM_Types.Question_Type.RadioButtonList:
                if (ttype == ORM.ORM_Types.Template_Type.Manual)
                {
                    if (WherevalueList.SelectedItem == null)
                        return "";
                    if (WherecondopList.SelectedValue == "<>")
                        Sql_Query = " AND (Selected_Item " + WherecondopList.SelectedValue + " " + WherevalueList.SelectedValue + " OR Selected_Item is null) ";
                    else
                        Sql_Query = " AND Selected_Item " + WherecondopList.SelectedValue + " " + WherevalueList.SelectedValue;
                }
                else if (ttype == ORM.ORM_Types.Template_Type.State)
                {
                    if (WherevalueLoc.SelectedItem == null)
                        return "";

                    if (WherevalueList.SelectedItem == null)
                        return "";
                    if (WherecondopLoc.SelectedValue == "<>")
                        Sql_Query = " AND (State_Item " + WherecondopLoc.SelectedValue + " " + WherevalueLoc.SelectedValue + " OR State_Item is null) ";
                    else
                        Sql_Query = " AND State_Item " + WherecondopLoc.SelectedValue + " " + WherevalueLoc.SelectedValue;
                }
                else if (ttype == ORM.ORM_Types.Template_Type.Branch)
                {
                    if (WherevalueBranch.SelectedItem == null)
                        return "";
                    if (WherecondopLoc.SelectedValue == "<>")
                        Sql_Query = " AND (Branch_Item " + WherecondopBranch.SelectedValue + " " + WherevalueBranch.SelectedValue + " OR Branch_Item is null) ";
                    Sql_Query = " AND Branch_Item " + WherecondopBranch.SelectedValue + " " + WherevalueBranch.SelectedValue;

                }
                else if (ttype == ORM.ORM_Types.Template_Type.Department)
                {
                    if (WherevalueDep.SelectedItem == null)
                        return "";
                    if (WherecondopDep.SelectedValue == "<>")
                        Sql_Query = " AND (Dep_Item " + WherecondopDep.SelectedValue + " " + WherevalueDep.SelectedValue + " OR Dep_Item is null) ";
                    else
                        Sql_Query = " AND Dep_Item " + WherecondopDep.SelectedValue + " " + WherevalueDep.SelectedValue;
                }

                break;

        }
        return Sql_Query;
    }
    void update_controls()
    {
        try
        {
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
                    if (ttype == ORM.ORM_Types.Template_Type.Branch)
                        WhereView.ActiveViewIndex = 5;
                    else if (ttype == ORM.ORM_Types.Template_Type.State)
                        WhereView.ActiveViewIndex = 4;
                    else if (ttype == ORM.ORM_Types.Template_Type.Department)
                        WhereView.ActiveViewIndex = 6;
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
        catch
        {
            Alert.Show();
        }
    }


    #endregion Functionsb

    #region Events

    protected void Btn_Reported_Click(object sender, EventArgs e)
    {
        var CN = new SQL_Connector();
        for (int i = 0; i < GridView_Form_Instance.Rows.Count; i++)
        {
            var Q = "UPDATE [FG_DB].[dbo].[Form_Instance] SET [Reported] = '";
            var CHK = (CheckBox)GridView_Form_Instance.Rows[i].FindControl("Chk01");
            Q += CHK.Checked + "' WHERE Instance_ID = " + CHK.ToolTip;
            CN.Execute(Q);
        }
    }


    protected void tblname_DataBound(object sender, EventArgs e)
    {

    }

    protected void Wherecolname_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView_Form_Instance.Visible = false;
        update_controls();
    }


    protected void WhereColname_DataBound(object sender, EventArgs e)
    {
        update_controls();
    }
    protected void tblname_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView_Form_Instance.Visible = false;
        Btn_Reported.Visible = false;
        lblSearch.Visible = false;
        txtSearch.Visible = false;
        btnSearch.Visible = false;
        ORM.Forms form = new ORM.Forms(tblname.SelectedValue);
        DataTable dt = new DataTable();
        dt = ORM.Question.Get_DT_Questions(tblname.SelectedValue);

        WhereColname.DataSource = dt;
        WhereColname.DataTextField = "Que";
        WhereColname.DataValueField = "QID";

        WhereColname.DataBind();

    }
   
    protected void Dep_selectall_CheckedChanged(object sender, EventArgs e)
    {
        GridView_Form_Instance.Visible = false;
        WhereColname.Enabled = chkSearch.Checked;
        WhereView.Visible = chkSearch.Checked;
    }
    #region download
    protected void btnXlsSave_Click(object sender, EventArgs e)
    {
        try
        {
            SQL_Connector connector = new SQL_Connector();

            DataTable dt = new DataTable("Answers");
            dt.Columns.Add("نام کاربری");
            dt.Columns.Add("تاریخ ثبت");
            dt.Columns.Add("کد محل خدمت");

            ORM.Forms form = new ORM.Forms(tblname.SelectedValue);
            foreach (ORM.Question q in form.Form_Question)
            {
                dt.Columns.Add(q.Description);
                dt.Columns[q.Description].Caption = q.Question_ID.ToString();
            }

            #region Query_Answers

            string Sql_Query_Instance_Branch_Answers = "SELECT [USER_NAME]  ,[CodeMahalKhedmat] ,[filldate] ,[Instance_ID] ,[Form_ID],[Form_Instance_User_name] " +
                                                        "FROM V_Instance_Branch_Answers WHERE 1=2 ";


            for (int i = 0; i < GridView_Form_Instance.Rows.Count; i++)
            {
                CheckBox chk_Form_Instance = new CheckBox();
                chk_Form_Instance = (CheckBox)GridView_Form_Instance.Rows[i].FindControl("chk_Form_Instance");

                if (chk_Form_Instance.Checked == true)
                {

                    Label lblInstance_ID = (Label)GridView_Form_Instance.Rows[i].FindControl("lblInstance_ID");
                    var selectedInstance_ID = lblInstance_ID.Text;
                    Sql_Query_Instance_Branch_Answers += " or Instance_ID = " + selectedInstance_ID;
                }
            }

            DataTable dt_Answers = connector.Select(Sql_Query_Instance_Branch_Answers);

            #endregion Query_Instance_Branch_Answers

            #region ExcelLib
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            Excel.Style titlestyle = xlApp.ActiveWorkbook.Styles.Add("Tilte1");
            titlestyle.Font.Name = "Verdana";
            titlestyle.Font.Size = 12;
            titlestyle.Font.Bold = true;
            titlestyle.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            titlestyle.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
            titlestyle.Interior.Pattern = Excel.XlPattern.xlPatternSolid;

            Excel.Style questionstyle = xlApp.ActiveWorkbook.Styles.Add("Question1");
            questionstyle.Font.Name = "Arial";
            questionstyle.Font.Size = 11;
            questionstyle.Font.Bold = true;
            questionstyle.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
            questionstyle.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.BlanchedAlmond);
            questionstyle.Interior.Pattern = Excel.XlPattern.xlPatternSolid;

            Excel.Style tabletitlestyle = xlApp.ActiveWorkbook.Styles.Add("TabelTitle");
            tabletitlestyle.Font.Name = "Arial";
            tabletitlestyle.Font.Size = 11;
            tabletitlestyle.Font.Bold = true;
            tabletitlestyle.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Brown);
            tabletitlestyle.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.RosyBrown);
            tabletitlestyle.Interior.Pattern = Excel.XlPattern.xlPatternSolid;

            for (int i = 0; i < dt_Answers.Rows.Count - 1; i++)
            {
                xlWorkBook.Worksheets.Add(misValue, misValue, misValue, misValue);
            }
            #endregion

            for (int j = 0; j < dt_Answers.Rows.Count; j++)
            {
                #region for dt_Answers
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(j + 1);

                string codeMahalKhedmat = (dt_Answers.Rows[j]["CodeMahalKhedmat"].ToString().Length > 0) ?
                                            dt_Answers.Rows[j]["CodeMahalKhedmat"].ToString() :
                                            "--";
                string User_Name = (dt_Answers.Rows[j]["User_name"].ToString().Length > 0) ?
                                    dt_Answers.Rows[j]["User_name"].ToString() :
                                    dt_Answers.Rows[j]["Form_Instance_User_name"].ToString();


                string form_id = dt_Answers.Rows[j]["Form_ID"].ToString();

                string Instance_ID = dt_Answers.Rows[j]["Instance_ID"].ToString();
                DateTime filldate = Convert.ToDateTime(dt_Answers.Rows[j]["filldate"].ToString());
                PersianCalendar pDate = new PersianCalendar();

                string year = Convert.ToString(pDate.GetYear(filldate));
                string month = string.Format(Convert.ToString(pDate.GetMonth(filldate)), "00");
                string day = string.Format(Convert.ToString(pDate.GetDayOfMonth(filldate)), "00");
                string farsidate = year + "-" + month + "-" + day;

                xlWorkSheet.Name = farsidate + "_" + User_Name + "_" + (j + 1).ToString();

                //سطر زیر هدر
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1]["نام کاربری"] = User_Name;
                dt.Rows[dt.Rows.Count - 1]["تاریخ ثبت"] = farsidate;
                dt.Rows[dt.Rows.Count - 1]["کد محل خدمت"] = codeMahalKhedmat;

                Form_Question_Instance temp = new Form_Question_Instance();

                var AnswersList = temp.get_answers(Instance_ID, form_id);

                foreach (ORM.Form_Question_Instance q in AnswersList)
                {
                    //      [شماره-نام- ستون]              [شماره سطر]
                    dt.Rows[dt.Rows.Count - 1][new ORM.Question(q.Question_ID).Description] = q.getString();
                }

                //سطر اول اکسل
                xlWorkSheet.Cells[1, 1] = dt.Columns[0].ToString();//ستون نام کاربری از دیتا تیبل
                xlWorkSheet.Cells[1, 2] = dt.Rows[j][0].ToString();
                //سطر دوم اکسل
                xlWorkSheet.Cells[2, 1] = dt.Columns[1].ToString();//تاریخ
                xlWorkSheet.Cells[2, 2] = dt.Rows[j][1].ToString();
                //             ستون سطر 
                xlWorkSheet.Cells[3, 1] = dt.Columns[2].ToString();//کد محل خدمت
                xlWorkSheet.Cells[3, 2] = dt.Rows[j][2].ToString();

                Excel.Range rng = xlWorkSheet.get_Range("A1", "B3");
                rng.Style = titlestyle;
                rng.Columns.AutoFit();

                xlWorkSheet.Cells[3, 3] = "سوالات";
                xlWorkSheet.Cells[3, 4] = "پاسخ ها";
                Excel.Range rngHeader = xlWorkSheet.get_Range("C3", "D3");
                rngHeader.Style = titlestyle;
                rngHeader.Columns.AutoFit();

                string Sql_Query = "Select [Instance_ID],[filldate],[User_name],[Question_ID],[Question_Type],[Template_Type]" +
                        ",[Form_ID],[Text],[Date],[Number],[Dep_Item],[Branch_Item],[State_Item],[Answer_ID]" +
         ",[Item_ID],[Item_Text],[Question_Index] from V_Answers where Form_ID = " + tblname.SelectedValue;

                DataTable dt3 = connector.Select(Sql_Query);

                int columns = 2;
                int rows = 3;

                for (int k = 3; k < dt.Columns.Count; k++)
                {
                    DataRow[] dr = dt3.Select("Question_ID = " + dt.Columns[k].Caption);
                    //یرای سوالاتی که نوعشان غیر 7 می باشند
                    if (dr.GetLength(0) > 0 && Convert.ToInt32(dr[0]["Question_Type"]) != 7)
                    {
                        columns = 3;

                        #region Questions
                        // اکسل - مقدار دهی ستون سوالات 
                        //               ستون 3 و سطر متغیر
                        xlWorkSheet.Cells[rows += 1, columns] = dt.Columns[k].ToString();//سوال
                                                                                         //استایل کردن ستون  سوالات 
                        Excel.Range rngQuestion = xlWorkSheet.get_Range(xlWorkSheet.Cells[rows, 3], xlWorkSheet.Cells[rows, 3]);
                        rngQuestion.Style = questionstyle;

                        var cellLength = dt.Columns[k].ToString().Length;
                        var prevCellLength = dt.Columns[k - 1].ToString().Length;
                        var maxCellLength = dt.Columns[k - 1].ToString().Length;

                        if (cellLength > maxCellLength)
                        {
                            //فیت کردن ستون پاسخ ها
                            rngQuestion.Columns.AutoFit();
                            maxCellLength = cellLength;
                        }

                        #endregion

                        #region Answers
                        // اکسل - مقداردهی ستون پاسخ ها   
                        //                  ستون 4 و سطر متغیر
                        xlWorkSheet.Cells[rows, columns += 1] = dt.Rows[0][k].ToString();//جواب سوال
                                                                                         //استایل کردن ستون پاسخ ها
                        Excel.Range rngAnswer = xlWorkSheet.get_Range(xlWorkSheet.Cells[rows, 4], xlWorkSheet.Cells[rows, 4]);
                        rngAnswer.Style = questionstyle;

                        var cellAnswerLength = dt.Rows[0][k].ToString().Length;
                        var prevAnswerCellLength = dt.Rows[0][k - 1].ToString().Length;
                        var maxAnswerLength = prevAnswerCellLength;
                        if (cellAnswerLength > maxAnswerLength)
                        {
                            //فیت کردن ستون پاسخ ها
                            rngAnswer.Columns.AutoFit();
                            maxAnswerLength = cellAnswerLength;
                        }

                        #endregion
                    }
                    else if (dr.GetLength(0) > 0)
                    {
                        connector = new SQL_Connector();
                        string SQL_query = "select A.Item_ID,A.Item_Text,A.Item_Type,A.List_ID,B.Question_ID " +
                                                        "from dbo.List_Items A,dbo.List B " +
                                                        "where A.List_ID = B.List_ID And B.Question_ID = " + dt.Columns[k].Caption;
                        DataTable dtCol = connector.Select(SQL_query);
                        columns = 3;
                        rows += 1;
                        xlWorkSheet.Cells[rows, columns] = dt.Columns[k].ToString();
                        Excel.Range rngQuestion = xlWorkSheet.get_Range(xlWorkSheet.Cells[rows, columns], xlWorkSheet.Cells[rows, columns]);
                        rngQuestion.Style = questionstyle;
                        rngQuestion.Columns.AutoFit();
                        for (int m = 0; m < dtCol.Rows.Count; m++)
                        {
                            xlWorkSheet.Cells[rows, columns += 1] = dtCol.Rows[m]["Item_Text"].ToString();
                        }
                        Excel.Range rngTitleTable = xlWorkSheet.get_Range(xlWorkSheet.Cells[rows, 4], xlWorkSheet.Cells[rows, dtCol.Rows.Count + 4]);
                        rngTitleTable.Style = tabletitlestyle;
                        rngTitleTable.Columns.AutoFit();
                        columns = 4;
                        xlWorkSheet.Cells[rows += 1, columns] = dr[0]["Text"].ToString();

                        for (int i = 1; i < dr.GetLength(0); i++)
                        {
                            if (Convert.ToInt16(dr[i]["Answer_ID"]) == Convert.ToInt16(dr[i - 1]["Answer_ID"]))
                            {
                                for (int m = 0; m < dtCol.Rows.Count; m++)
                                {
                                    if (dr[i]["Item_Text"].ToString() == dtCol.Rows[m]["Item_Text"].ToString())
                                        xlWorkSheet.Cells[rows, columns + m] = dr[i]["Text"].ToString();
                                }
                                if (!((i + 1) < dr.GetLength(0) && Convert.ToInt16(dr[i]["Answer_ID"]) == Convert.ToInt16(dr[i + 1]["Answer_ID"])))
                                    rows += 1;
                            }
                            else
                            {
                                columns = 4;
                                xlWorkSheet.Cells[rows += 1, columns] = dr[i]["Text"].ToString();
                            }

                        }
                    }//end else if

                }//end for
                #endregion
            }//end foreach 

            xlApp.Visible = true;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        catch (Exception er)
        {
            Alert.Show();
        }

    }//end btnXlsSave_Click          

    protected void btnXML_Save_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable("Answers");

        ORM.Forms form = new ORM.Forms(tblname.SelectedValue);

        //سطر هدر
        dt.Columns.Add("تاریخ ثبت");
        dt.Columns.Add("نام کاربری");
        dt.Columns.Add("کد محل خدمت");
        foreach (ORM.Question q in form.Form_Question)
        {
            dt.Columns.Add(q.Description);
        }

        var User_Name = string.Empty;
        var form_id = string.Empty;
        var codeMahalKhedmat = string.Empty;
        var filldate = string.Empty;
        string farsidate = string.Empty;
        string Instance_ID = string.Empty;

        SQL_Connector connector = new SQL_Connector();

        String Sql_query = "SELECT [USER_NAME]  ,[CodeMahalKhedmat] ,[filldate] ,[Instance_ID] ," +
            "[Form_ID],[Form_Instance_User_name] FROM V_Instance_Branch_Answers where 1=2 ";
        for (int i = 0; i < GridView_Form_Instance.Rows.Count; i++)
        {
            CheckBox chk_Form_Instance = new CheckBox();
            chk_Form_Instance = (CheckBox)GridView_Form_Instance.Rows[i].FindControl("chk_Form_Instance");
            if (chk_Form_Instance.Checked == true)
            {

                Label lblInstance_ID = (Label)GridView_Form_Instance.Rows[i].FindControl("lblInstance_ID");
                var selectedInstance_ID = lblInstance_ID.Text;
                Sql_query += " or Instance_ID = " + selectedInstance_ID;
            }
        }

        DataTable dt_Answers = connector.Select(Sql_query);
        foreach (DataRow dr in dt_Answers.Rows)
        {
            #region ForDt_Answers
            codeMahalKhedmat = (dr["CodeMahalKhedmat"].ToString().Length > 0) ?
                dr["CodeMahalKhedmat"].ToString() :
                "--";

            User_Name = (dr["User_name"].ToString().Length > 0) ?
                                dr["User_name"].ToString() :
                                dr["Form_Instance_User_name"].ToString();

            form_id = dr["Form_ID"].ToString();
            Instance_ID = dr["Instance_ID"].ToString();
            filldate = dr["filldate"].ToString();
            farsidate = OrmUtility.ConvertToShamsi(filldate);

            //سطر زیر هدر
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1]["تاریخ ثبت"] = farsidate;
            dt.Rows[dt.Rows.Count - 1]["نام کاربری"] = User_Name;
            dt.Rows[dt.Rows.Count - 1]["کد محل خدمت"] = codeMahalKhedmat;

            Form_Question_Instance temp = new Form_Question_Instance();
            var AnswersList = temp.get_answers(Instance_ID, form_id);
            //AnswersList = temp.get_answers(Instance_ID, "137");
            foreach (ORM.Form_Question_Instance q in AnswersList)
            {
                //      [شماره-نام- ستون]              [شماره سطر]
                dt.Rows[dt.Rows.Count - 1][new ORM.Question(q.Question_ID).Description] = q.getString();
            }
            #endregion
        }//end foreach 

        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Answers.xml");

        System.IO.StringWriter writer = new System.IO.StringWriter();
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

        var dtAnswers = new DataTable();

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
    protected void IB_saveexcel_Click(object sender, EventArgs e)
    {
        var pDate = new PersianCalendar();
        var dt = new DataTable("Answers");
        dt.Columns.Add("نام کاربری");
        dt.Columns.Add("تاریخ ثبت");

        var form = new ORM.Forms(tblname.SelectedValue);
        foreach (ORM.Question q in form.Form_Question)
        {
            dt.Columns.Add(q.Description);
        }

        var Sql_Query = Query_builder();
        var dt2 = ORM.Form_Instance.get_dt_answersForExcel(tblname.SelectedValue, Sql_Query);
        for (int j = 0; j < dt2.Rows.Count; j++)
        {
            var filldate = Convert.ToDateTime(dt2.Rows[j]["filldate"]);
            var year = Convert.ToString(pDate.GetYear(filldate));
            var month = String.Format(
                Convert.ToString(pDate.GetMonth(filldate)), "00");
            var day = String.Format(
                Convert.ToString(pDate.GetDayOfMonth(filldate)), "00");
            var farsidate = year + month + day;
            var instance = new ORM.Form_Instance(dt2.Rows[j]["Instance_ID"].ToString());
            dt.Rows.Add(dt2.Rows[j]["User_name"].ToString());
            dt.Rows[dt.Rows.Count - 1]["تاریخ ثبت"] = farsidate;
            foreach (ORM.Form_Question_Instance q in instance.Answers)
            {
                //      [شماره-نام- ستون]              [شماره سطر]
                dt.Rows[dt.Rows.Count - 1][new ORM.Question(q.Question_ID).Description] = q.getString();
            }

        }
        var filename = "Answers.xml";
        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
        this.EnableViewState = false;
        Response.Write(OrmUtility.DataTableToXML(dt));
        Response.End();
    }


    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        var pDate = new System.Globalization.PersianCalendar();
        var connector = new SQL_Connector();
        String SQL_query = "";
        DataTable dt = new DataTable("Answers");
        dt.Columns.Add("نام کاربری");
        dt.Columns.Add("تاریخ ثبت");
        ORM.Forms form = new ORM.Forms(tblname.SelectedValue);
        foreach (ORM.Question q in form.Form_Question)
        {
            dt.Columns.Add(q.Description);
            dt.Columns[q.Description].Caption = q.Question_ID.ToString();
        }
        Excel.Application xlApp = new Excel.Application();
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;
        xlWorkBook = xlApp.Workbooks.Add(misValue);

        String Sql_Query = Query_builder();
        String Sql_Tables = "";
        DataTable dt2 = ORM.Form_Instance.get_dt_answersForExcel(tblname.SelectedValue, Sql_Query);
        for (int i = 0; i < dt2.Rows.Count - 1; i++)
        {
            xlWorkBook.Worksheets.Add(misValue, misValue, misValue, misValue);
        }
        Excel.Style titlestyle = xlApp.ActiveWorkbook.Styles.Add("Tilte1");
        titlestyle.Font.Name = "Verdana";
        titlestyle.Font.Size = 12;
        titlestyle.Font.Bold = true;
        titlestyle.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
        titlestyle.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
        titlestyle.Interior.Pattern = Excel.XlPattern.xlPatternSolid;
        Excel.Style questionstyle = xlApp.ActiveWorkbook.Styles.Add("Question1");
        questionstyle.Font.Name = "Arial";
        questionstyle.Font.Size = 11;
        questionstyle.Font.Bold = true;
        questionstyle.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
        questionstyle.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.BlanchedAlmond);
        questionstyle.Interior.Pattern = Excel.XlPattern.xlPatternSolid;
        Excel.Style tabletitlestyle = xlApp.ActiveWorkbook.Styles.Add("TabelTitle");
        tabletitlestyle.Font.Name = "Arial";
        tabletitlestyle.Font.Size = 11;
        tabletitlestyle.Font.Bold = true;
        tabletitlestyle.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Brown);
        tabletitlestyle.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.RosyBrown);
        tabletitlestyle.Interior.Pattern = Excel.XlPattern.xlPatternSolid;
        for (int j = 0; j < dt2.Rows.Count; j++)
        {
            Sql_Tables = "";
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(j + 1);
            DateTime filldate = Convert.ToDateTime(dt2.Rows[j]["filldate"]);
            string year = Convert.ToString(pDate.GetYear(filldate));
            string month = System.String.Format(Convert.ToString(pDate.GetMonth(filldate)), "00");
            string day = System.String.Format(Convert.ToString(pDate.GetDayOfMonth(filldate)), "00");
            string farsidate = year + month + day;
            xlWorkSheet.Name = farsidate + "_" + dt2.Rows[j]["User_name"].ToString() + "_" + (j + 1).ToString();
            ORM.Form_Instance instance = new ORM.Form_Instance(dt2.Rows[j]["Instance_ID"].ToString());
            if (Sql_Query.Contains("Where"))
                Sql_Tables = Sql_Query + "ORDER BY User_name,Form_Instance_ID,Question_Index,Answer_ID";
            else
                Sql_Tables = Sql_Query + "AND Instance_ID = " + dt2.Rows[j]["Instance_ID"].ToString() + " ORDER BY User_name,Form_Instance_ID,Question_Index,Answer_ID";
            DataTable dt3 = ORM.Form_Instance.get_dt_allanswersForExcel(tblname.SelectedValue, Sql_Tables);

            dt.Rows.Add(dt2.Rows[j]["User_name"].ToString());
            dt.Rows[dt.Rows.Count - 1]["تاریخ ثبت"] = farsidate;
            foreach (ORM.Form_Question_Instance q in instance.Answers)
            {
                dt.Rows[dt.Rows.Count - 1][new ORM.Question(q.Question_ID).Description] = q.getString();
            }
            xlWorkSheet.Cells[1, 1] = dt.Columns[0].ToString();
            xlWorkSheet.Cells[1, 2] = dt.Rows[j][0].ToString();
            xlWorkSheet.Cells[2, 1] = dt.Columns[1].ToString();
            xlWorkSheet.Cells[2, 2] = dt.Rows[j][1].ToString();
            Excel.Range rng = xlWorkSheet.get_Range("A1", "B2");
            rng.Style = titlestyle;
            rng.Columns.AutoFit();
            int columns = 2, rows = 2;
            DataRow[] dr;
            for (int k = 2; k < dt.Columns.Count; k++)
            {
                dr = dt3.Select("Question_ID = " + dt.Columns[k].Caption);
                if (dr.GetLength(0) > 0 && Convert.ToInt32(dr[0]["Question_Type"]) != 7)
                {
                    columns = 3;
                    xlWorkSheet.Cells[rows += 1, columns] = dt.Columns[k].ToString();
                    Excel.Range rngQuestion = xlWorkSheet.get_Range(xlWorkSheet.Cells[rows, 3], xlWorkSheet.Cells[rows, 3]);
                    rngQuestion.Style = questionstyle;
                    rngQuestion.Columns.AutoFit();
                    xlWorkSheet.Cells[rows, columns += 1] = dt.Rows[0][k].ToString();
                }
                else if (dr.GetLength(0) > 0)
                {
                    connector = new SQL_Connector();
                    SQL_query = "select A.Item_ID,A.Item_Text,A.Item_Type,A.List_ID,B.Question_ID " +
                                "from dbo.List_Items A,dbo.List B " +
                                "where A.List_ID = B.List_ID And B.Question_ID = " + dt.Columns[k].Caption;
                    DataTable dtCol = connector.Select(SQL_query);
                    columns = 3;
                    rows += 1;
                    xlWorkSheet.Cells[rows, columns] = dt.Columns[k].ToString();
                    Excel.Range rngQuestion = xlWorkSheet.get_Range(xlWorkSheet.Cells[rows, columns], xlWorkSheet.Cells[rows, columns]);
                    rngQuestion.Style = questionstyle;
                    rngQuestion.Columns.AutoFit();
                    for (int m = 0; m < dtCol.Rows.Count; m++)
                    {
                        xlWorkSheet.Cells[rows, columns += 1] = dtCol.Rows[m]["Item_Text"].ToString();
                    }
                    Excel.Range rngTitleTable = xlWorkSheet.get_Range(xlWorkSheet.Cells[rows, 4], xlWorkSheet.Cells[rows, dtCol.Rows.Count + 4]);
                    rngTitleTable.Style = tabletitlestyle;
                    rngTitleTable.Columns.AutoFit();
                    columns = 4;
                    xlWorkSheet.Cells[rows += 1, columns] = dr[0]["Text"].ToString();

                    for (int i = 1; i < dr.GetLength(0); i++)
                    {
                        if (Convert.ToInt16(dr[i]["Answer_ID"]) == Convert.ToInt16(dr[i - 1]["Answer_ID"]))
                        {
                            for (int m = 0; m < dtCol.Rows.Count; m++)
                            {
                                if (dr[i]["Item_Text"].ToString() == dtCol.Rows[m]["Item_Text"].ToString())
                                    xlWorkSheet.Cells[rows, columns + m] = dr[i]["Text"].ToString();
                            }
                        }
                        else
                        {
                            columns = 4;
                            xlWorkSheet.Cells[rows += 1, columns] = dr[i]["Text"].ToString();
                        }
                    }
                }
            }
        }

        xlApp.Visible = true;

    }
    protected void ID_Excel_Click(object sender, EventArgs e)
    {
        const string INSTANCE_TITLE_FORMAT_NAME = "instance";
        const string INSTANCE_HEADER_FORMAT_NAME = "greyBackground";
        const string QUESTION_TITLE_FORMAT_NAME = "question";
        const string TABLE_TITLE_FORMAT_NAME = "tabletitle";
        const string CELL_FONT_FORMAT = "answer";
        string fontName = "Arial";
        string path = "";
        try
        {
            System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
            XmlExcelHelper helper = new XmlExcelHelper(path, fontName, 12);
            helper.AddStringStyle(INSTANCE_TITLE_FORMAT_NAME, fontName, 13, "#6600FF", "#CC9900", true);
            helper.AddStringStyle(INSTANCE_HEADER_FORMAT_NAME, fontName, 14, "#800000", "#CC9900", true);
            helper.AddStringStyle(QUESTION_TITLE_FORMAT_NAME, fontName, 14, "#660033", "#FFFF99", true);
            helper.AddStringStyle(TABLE_TITLE_FORMAT_NAME, fontName, 14, "#663300", "#FFCC99", true);
            helper.AddStringStyle(CELL_FONT_FORMAT, fontName, 12, "#000000", false);
            DataTable dt = new DataTable("Answers");
            dt.Columns.Add("نام کاربری");
            dt.Columns.Add("تاریخ ثبت");
            dt.Columns.Add("شماره جواب");
            ORM.Forms form = new ORM.Forms(tblname.SelectedValue);
            foreach (ORM.Question q in form.Form_Question)
            {
                dt.Columns.Add(q.Description);
                dt.Columns[q.Description].Caption = q.Question_ID.ToString();
            }
            String Sql_Query = Query_builder();
            String Sql_Tables = "";
            DataTable dt2 = ORM.Form_Instance.get_dt_answersForExcel(tblname.SelectedValue, Sql_Query);
            for (int j = 0; j < dt2.Rows.Count; j++)
            {
                DateTime filldate = Convert.ToDateTime(dt2.Rows[j]["filldate"]);
                string year = Convert.ToString(pDate.GetYear(filldate));
                string month = System.String.Format(Convert.ToString(pDate.GetMonth(filldate)), "00");
                if (month.Length < 2) month = "0" + month;
                string day = System.String.Format(Convert.ToString(pDate.GetDayOfMonth(filldate)), "00");
                if (day.Length < 2) day = "0" + day;
                string farsidate = year + month + day;
                ORM.Form_Instance instance = new ORM.Form_Instance(dt2.Rows[j]["Instance_ID"].ToString());
                dt.Rows.Add(dt2.Rows[j]["User_name"].ToString());
                dt.Rows[dt.Rows.Count - 1]["تاریخ ثبت"] = farsidate;
                dt.Rows[dt.Rows.Count - 1]["شماره جواب"] = dt2.Rows[j]["Instance_ID"].ToString();
                foreach (ORM.Form_Question_Instance q in instance.Answers)
                {
                    dt.Rows[dt.Rows.Count - 1][new ORM.Question(q.Question_ID).Description] = q.getString();
                }
            }
            SQL_Connector connector;
            for (int k = 3; k < dt.Columns.Count; k++)
            {
                helper.CreateSheet((k - 2).ToString());
                helper.AddRow();
                helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_TITLE_FORMAT_NAME, dt.Columns[0].ToString());
                helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_HEADER_FORMAT_NAME, dt.Columns[1].ToString());
                bool bcol = true;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Sql_Query = Query_builder();
                    if (Sql_Query.Contains("Where"))
                        Sql_Tables = Sql_Query + "ORDER BY User_name,Instance_ID,Question_Index,Answer_ID,Item_ID";
                    else
                        Sql_Tables = Sql_Query + "AND Instance_ID = " + dt.Rows[i][2].ToString() +
                            " AND Question_ID = " + dt.Columns[k].Caption + " ORDER BY User_name,Instance_ID,Question_Index,Answer_ID,Item_ID";
                    DataTable dt3 = ORM.Form_Instance.get_dt_allanswersForExcel(tblname.SelectedValue, Sql_Tables);
                    if (dt3.Rows.Count > 0 && Convert.ToInt32(dt3.Rows[0]["Question_Type"]) != 7)
                    {
                        if (bcol)
                        {
                            helper.AddCell(XmlExcelHelper.CellType.String, QUESTION_TITLE_FORMAT_NAME, dt.Columns[k].ToString());
                            helper.AddRow();
                            bcol = false;
                        }
                        helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_TITLE_FORMAT_NAME, dt.Rows[i][0].ToString());
                        helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_HEADER_FORMAT_NAME, dt.Rows[i][1].ToString());
                        helper.AddCell(XmlExcelHelper.CellType.String, QUESTION_TITLE_FORMAT_NAME, dt.Rows[i][k].ToString());
                    }
                    else
                    {
                        connector = new SQL_Connector();
                        Sql_Query = "select A.Item_ID,A.Item_Text,A.Item_Type,A.List_ID,B.Question_ID " +
                                    "from dbo.List_Items A,dbo.List B " +
                                    "where A.List_ID = B.List_ID And B.Question_ID = " + dt.Columns[k].Caption;
                        DataTable dtCol = connector.Select(Sql_Query);
                        if (bcol)
                        {

                            for (int m = 0; m < dtCol.Rows.Count; m++)
                            {
                                helper.AddCell(XmlExcelHelper.CellType.String, TABLE_TITLE_FORMAT_NAME, dtCol.Rows[m]["Item_Text"].ToString());
                            }
                            helper.AddRow();
                            bcol = false;
                        }
                        bool bfind = false;
                        for (int j = 0; j < dt3.Rows.Count; j++)
                        {
                            for (int m = 0; m < dtCol.Rows.Count; m++)
                            {
                                helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_TITLE_FORMAT_NAME, dt3.Rows[j][2].ToString());
                                helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_HEADER_FORMAT_NAME, dt3.Rows[j][1].ToString());
                                if (dt3.Rows[j]["Item_Text"].ToString() == dtCol.Rows[m]["Item_Text"].ToString())
                                {
                                    helper.AddCell(XmlExcelHelper.CellType.String, QUESTION_TITLE_FORMAT_NAME, dt3.Rows[j]["Text"].ToString());
                                    bfind = true;
                                    break;
                                }
                                else
                                {
                                    if (!bfind)
                                        helper.AddCell(XmlExcelHelper.CellType.String, CELL_FONT_FORMAT, "");
                                }
                            }
                            break;
                        }
                        int pos = 1;
                        for (int j = 1; j < dt3.Rows.Count; j++)
                        {
                            if (Convert.ToInt16(dt3.Rows[j]["Answer_ID"]) == Convert.ToInt16(dt3.Rows[j - 1]["Answer_ID"]))
                            {
                                for (int m = 0; m < dtCol.Rows.Count; m++)
                                {
                                    if (dt3.Rows[j]["Item_Text"].ToString() == dtCol.Rows[m]["Item_Text"].ToString())
                                    {
                                        for (int l = 0; l < m - pos; l++)
                                        {
                                            helper.AddCell(XmlExcelHelper.CellType.String, "");
                                            pos += 1;
                                        }
                                        helper.AddCell(XmlExcelHelper.CellType.String, QUESTION_TITLE_FORMAT_NAME, dt3.Rows[j]["Text"].ToString());
                                        pos += 1;

                                    }
                                }

                            }
                            else
                            {
                                helper.AddRow();
                                helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_TITLE_FORMAT_NAME, dt3.Rows[j][2].ToString());
                                helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_HEADER_FORMAT_NAME, dt3.Rows[j][1].ToString());
                                helper.AddCell(XmlExcelHelper.CellType.String, QUESTION_TITLE_FORMAT_NAME, dt3.Rows[j]["Text"].ToString());
                                pos = 1;
                            }

                        }

                    }
                    helper.AddRow();
                }
            }
            string filename = "Answers.xml";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(helper.ExcelFileXml);
            Response.End();
            SQL_Connector sConn = new SQL_Connector();
            for (int i = 0; i < GridView_Form_Instance.Rows.Count; i++)
            {
                Sql_Query = "UPDATE [FG_DB].[dbo].[Form_Instance] SET [Reported] = '";
                CheckBox myCheck = new CheckBox();
                myCheck = (CheckBox)GridView_Form_Instance.Rows[i].FindControl("Chk01");
                Sql_Query += myCheck.Checked + "' WHERE Instance_ID = " + myCheck.ToolTip;
                sConn.Execute(Sql_Query);
            }
        }
        catch (Exception er) { Alert.Show(); }
    }
    protected void ID_download_Click(object sender, EventArgs e)
    {
        const string INSTANCE_TITLE_FORMAT_NAME = "instance";
        const string INSTANCE_HEADER_FORMAT_NAME = "greyBackground";
        const string QUESTION_TITLE_FORMAT_NAME = "question";
        const string TABLE_TITLE_FORMAT_NAME = "tabletitle";
        const string CELL_FONT_FORMAT = "answer";
        string path = "";
        try
        {
            System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
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
                DateTime filldate = Convert.ToDateTime(dt2.Rows[j]["filldate"]);
                string year = Convert.ToString(pDate.GetYear(filldate));
                string month = System.String.Format(Convert.ToString(pDate.GetMonth(filldate)), "00");
                string day = System.String.Format(Convert.ToString(pDate.GetDayOfMonth(filldate)), "00");
                string farsidate = year + month + day;
                helper.CreateSheet(farsidate + "_" + dt2.Rows[j]["User_name"].ToString() + "_" + (j + 1).ToString());
                ORM.Form_Instance instance = new ORM.Form_Instance(dt2.Rows[j]["Instance_ID"].ToString());
                if (Sql_Query.Contains("Where"))
                    Sql_Tables = Sql_Query + "ORDER BY User_name,Form_Instance_ID,Question_Index,Answer_ID,Item_ID";
                else
                    Sql_Tables = Sql_Query + "AND Instance_ID = " + dt2.Rows[j]["Instance_ID"].ToString() + " ORDER BY User_name,Form_Instance_ID,Question_Index,Answer_ID,Item_ID";
                DataTable dt3 = ORM.Form_Instance.get_dt_allanswersForExcel(tblname.SelectedValue, Sql_Tables);

                dt.Rows.Add(dt2.Rows[j]["User_name"].ToString());
                dt.Rows[dt.Rows.Count - 1]["تاریخ ثبت"] = dt2.Rows[j]["filldate"].ToString();
                foreach (ORM.Form_Question_Instance q in instance.Answers)
                {
                    dt.Rows[dt.Rows.Count - 1][new ORM.Question(q.Question_ID).Description] = q.getString();
                }
                helper.AddRow();
                helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_TITLE_FORMAT_NAME, dt.Columns[0].ToString());
                helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_HEADER_FORMAT_NAME, dt.Rows[j][0].ToString());
                helper.AddRow();
                helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_TITLE_FORMAT_NAME, dt.Columns[1].ToString());
                helper.AddCell(XmlExcelHelper.CellType.String, INSTANCE_HEADER_FORMAT_NAME, dt.Rows[j][1].ToString());
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
                                    //else
                                    //{
                                    //    helper.AddCell(XmlExcelHelper.CellType.String, "");
                                    //}

                                    //xlWorkSheet.Cells[rows, columns + m] = dr[i]["Text"].ToString();
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
        catch (Exception er) { Alert.Show(); }
    }
    protected void btnXmlBetweenDates_Click(object sender, EventArgs e)
    {
        var dtFormQue = new DataTable("Answers");
        dtFormQue.Columns.Add("نام کاربری");
        dtFormQue.Columns.Add("تاریخ ثبت");
        dtFormQue.Columns.Add("کد محل خدمت");

        var form = new ORM.Forms(tblname.SelectedValue);
        foreach (ORM.Question q in form.Form_Question)
        {
            dtFormQue.Columns.Add(q.Description);
        }
        var Sql_Query = Query_builder();
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
                dtAnswers = ORM.Form_Instance.GetDtAnswersCodeKhedmatTop(
                    tblname.SelectedValue, Sql_Query, txtCount.Text);
            else
                dtAnswers = ORM.Form_Instance.GetDtAnswersCodeKhedmatTop(
                    tblname.SelectedValue, Sql_Query, 10.ToString());
        }
        for (int j = 0; j < dtAnswers.Rows.Count; j++)
        {
            var fillDate = Convert.ToDateTime(dtAnswers.Rows[j]["filldate"]);
            var farsiDate = OrmUtility.ConvertToShamsi(fillDate.ToString());

            dtFormQue.Rows.Add();
            dtFormQue.Rows[dtFormQue.Rows.Count - 1]["نام کاربری"] =
                dtAnswers.Rows[j]["Form_Instance_User_name"].ToString();
            dtFormQue.Rows[dtFormQue.Rows.Count - 1]["تاریخ ثبت"] = farsiDate;
            dtFormQue.Rows[dtFormQue.Rows.Count - 1]["کد محل خدمت"] = dtAnswers.Rows[j]["CodeMahalKhedmat"];
            var instance = new ORM.Form_Instance(dtAnswers.Rows[j]["Instance_ID"].ToString());
            foreach (ORM.Form_Question_Instance q in instance.Answers)
            {
                //                                [شماره-نام- ستون]       [شماره سطر]
                dtFormQue.Rows[dtFormQue.Rows.Count - 1][new ORM.Question(q.Question_ID).Description] =
                    q.getString();
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
            dtFormQue.WriteXml(writer, XmlWriteMode.IgnoreSchema, false);

            Response.Write(writer.ToString());

            Response.Flush();
            Response.End();
        }
    }
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView_Form_Instance.PageIndex = 0;//every time user clicks the search button go to first page (page index = 0)
        GridView_Form_Instance.SelectedIndex = -1;
        Filtered_Binder();
    }

    protected void GridView_Form_Instance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Show_Instance")
        {
            var SlctdRw = e.CommandArgument.ToString();
            var cdMhlKdmt = "--";
            var CN = new SQL_Connector();
            var SQL = @"SELECT CodeMahalKhedmat
                                        FROM V_Instance_Branch_Answers 
                                        WHERE Instance_ID = "
                                        + SlctdRw;
            var DT = CN.Select(SQL);
            cdMhlKdmt = (DT.Rows[0]["CodeMahalKhedmat"] != DBNull.Value) ?
            DT.Rows[0]["CodeMahalKhedmat"].ToString() :
            "--";
            for (int i = 0; i < HttpContext.Current.Session.Keys.Count; i++)
            {
                if (HttpContext.Current.Session.Keys[i].ToString().Contains("rad"))
                    HttpContext.Current.Session[HttpContext.Current.Session.Keys[i].ToString()] = "";
                if (HttpContext.Current.Session.Keys[i].ToString().Contains("GV"))
                    HttpContext.Current.Session[HttpContext.Current.Session.Keys[i]] = null;
            }
            var instanceform = new ORM.Forms(tblname.SelectedValue);
            var form = new ORM.HTML_Forms(instanceform, this);
            panelHtmlForms.Controls.Clear();
            panelHtmlForms.Controls.Add(form.Fill_Panel(SlctdRw, cdMhlKdmt));
            panelHtmlForms.Enabled = false;
            panelHtmlForms.Visible = true;

        }//end if(e.CommandName == "Show_Instance")

        if (e.CommandName == "Delete")
        {
            ORM.Form_Instance.Delete(e.CommandArgument.ToString());
            Binder();
            return;
        }
        if (e.CommandName == "Edit")
            Response.Redirect("Edit_Answers.aspx?FID="
                + tblname.SelectedValue
                + "&&Ansid=" + e.CommandArgument.ToString());
    }

    protected void GridView_Form_Instance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_Form_Instance.SelectedIndex = -1;
        GridView_Form_Instance.PageIndex = e.NewPageIndex;
        //مشکل انتخاب شدن کلیه رکورد ها در هنگام تعویض اندیس گرید  
        //Binder();
        Filtered_Binder();
    }
    

    protected void btnSearchAnswers_Click(object sender, EventArgs e)
    {
        GridView_Form_Instance.SelectedIndex = -1;
        for (int i = 0; i < HttpContext.Current.Session.Keys.Count; i++)
        {
            if (HttpContext.Current.Session.Keys[i].ToString().Contains("rad"))
                HttpContext.Current.Session[HttpContext.Current.Session.Keys[i].ToString()] = "";
        }
        txtFromDate.Value = "";
        txtToDate.Value = "";
        Binder();
    }

    protected void btnSearchBetweenDates_Click(object sender, EventArgs e)
    {
        GridView_Form_Instance.SelectedIndex = -1;
        for (int i = 0; i < HttpContext.Current.Session.Keys.Count; i++)
        {
            if (HttpContext.Current.Session.Keys[i].ToString().Contains("rad"))
            {
                HttpContext.Current.Session[HttpContext.Current.Session.Keys[i].ToString()] = "";
            }
        }
        Binder();
    }

    #endregion Events

}//end class ShowForm

