using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using PersianDateControls;
using AjaxControlToolkit;
using System.Linq;
using System.Linq.Expressions;



/// <summary>
/// Summary description for HTML_Forms
/// </summary>
/// 
namespace ORM
{
    public partial class HTML_Forms : BaseHtmlFrm
    {
        public HTML_Forms(ORM.Forms localform, Page sender)
        {
            _sender = sender;
            _local_form = localform;
        }

        public static Panel Get_Lbl()
        {
            Panel output_panel = new Panel();
            var req = new Label();
            req.Font.Size = 9;
            req.Font.Name = "Tahoma";
            req.Font.Bold = true;
            req.ForeColor = System.Drawing.Color.Red;
            req.Text += "FID";
            output_panel.Controls.Add(req);
            return output_panel;
        }
        public Panel Get_Html()
        {
            foreach (object obj in this.Local_Form.Form_Question)
            {
                var QUE = (Question)obj;
                if (!QUE.Question_active)
                    continue;
                //if answer1 is true no continue 
                this.AddFirstSection();
                AddLblStar(QUE);
                AddLblQuestionDes(QUE);
#warning remove
                switch (QUE.Question_Type)
                {
                    case ORM_Types.Question_Type.ChckBxLstTxt:
                        AddChkBxTxt(QUE);
                        break;
                    case ORM_Types.Question_Type.RdoBtnLstTxt:
                        AddRdoLstTxt(QUE);
                        break;
                    case ORM_Types.Question_Type.DrpDnLstTxt:
                        AddDrpLstTxt(QUE);
                        break;
                    case ORM_Types.Question_Type.Table:
                        AddTblBx(QUE);
                        break;
                    case ORM_Types.Question_Type.ComboBoxList:
                        AddChkBx(QUE);
                        break;
                    case ORM_Types.Question_Type.DropDownList:
                        AddDrpDnLst(QUE);
                        break;
                    case ORM_Types.Question_Type.RadioButtonList:
                        AddRdoLst(QUE);
                        break;
                    case ORM_Types.Question_Type.Text:
                        AddTxtBx(QUE);
                        break;
                    case ORM_Types.Question_Type.Number:
                        AddNumBx(QUE);
                        break;
                    case ORM_Types.Question_Type.Date:
                        AddDateBx(QUE);
                        break;
                }
                AddLastSection();
            }
            return output_panel;
        }
        #region MethodsGet_Html
        void AddLblStar(Question QUE)
        {
            var req = new Label();
            req.Font.Size = 9;
            req.Font.Name = "Tahoma";
            req.Font.Bold = true;
            req.ForeColor = System.Drawing.Color.Red;
            if (!QUE.Question_Optional)
                req.Text += " * ";
            output_panel.Controls.Add(req);
        }

        void AddLblQuestionDes(Question QUE)
        {
            var title = new Label();
            title.Font.Size = 9;
            title.Font.Name = "Tahoma";
            title.Font.Bold = true;
            title.ForeColor = System.Drawing.Color.Black;
            title.Text = QUE.Question_Index + ". " + QUE.Description;
            output_panel.Controls.Add(title);

        }

        void AddChkBx(Question QUE)
        {
#warning test
            var chklist = new CheckBoxList();
            chklist.ID = "CBL" + QUE.Question_ID + "_" +QUE.Question_Index;
            switch (QUE.Template_Type)
            {
                case ORM_Types.Template_Type.State:
                    chklist.DataSource = State.get_states();
                    chklist.DataTextField = "locname";
                    chklist.DataValueField = "locid";
                    break;
                case ORM_Types.Template_Type.Branch:
                    chklist.DataSource = Branch.get_Branchs();
                    chklist.DataTextField = "title";
                    chklist.DataValueField = "Bid";
                    break;
                case ORM_Types.Template_Type.Department:
                    chklist.DataSource = Department.get_deps();
                    chklist.DataTextField = "depname";
                    chklist.DataValueField = "depid";
                    break;
                case ORM_Types.Template_Type.Manual:
                    chklist.DataSource = ORM.List.get_dt_items(QUE.Question_ID);
                    chklist.DataTextField = "Item_Text";
                    chklist.DataValueField = "Item_ID";
                    break;
                default:
                    break;
            }
            output_panel.Controls.Add(chklist);
            chklist.DataBind();
            if (chklist.Items.Count > 0)
                chklist.Items[0].Selected = true;
        }

        void AddDrpDnLst(Question QUE)
        {
#warning test
            var drlist = new DropDownList();
            drlist.ID = "DDL" + QUE.Question_ID + "_" +
                QUE.Question_Index;
            switch (QUE.Template_Type)
            {
                case ORM_Types.Template_Type.State:
                    drlist.DataSource = State.get_states();
                    drlist.DataTextField = "locname";
                    drlist.DataValueField = "locid";
                    break;
                case ORM_Types.Template_Type.Branch:
                    drlist.DataSource = Branch.get_Branchs();
                    drlist.DataTextField = "title";
                    drlist.DataValueField = "Bid";
                    break;
                case ORM_Types.Template_Type.Department:
                    drlist.DataSource = Department.get_deps();
                    drlist.DataTextField = "depname";
                    drlist.DataValueField = "depid";
                    break;
                case ORM_Types.Template_Type.Manual:
                    drlist.DataSource = ORM.List.get_dt_items(QUE.Question_ID);
                    drlist.DataTextField = "Item_Text";
                    drlist.DataValueField = "Item_ID";
                    break;
                default:
                    break;
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            output_panel.Controls.Add(drlist);
            drlist.DataBind();
        }

        void AddRdoLst(Question QUE)
        {
#warning test
            var rblist = new RadioButtonList();
            rblist.ID = "RBL" + QUE.Question_ID + "_" + QUE.Question_Index;
            switch (QUE.Template_Type)
            {
                case ORM_Types.Template_Type.State:
                    rblist.DataSource = State.get_states();
                    rblist.DataTextField = "locname";
                    rblist.DataValueField = "locid";
                    break;
                case ORM_Types.Template_Type.Branch:
                    rblist.DataSource = Branch.get_Branchs();
                    rblist.DataTextField = "title";
                    rblist.DataValueField = "Bid";
                    break;
                case ORM_Types.Template_Type.Department:
                    rblist.DataSource = Department.get_deps();
                    rblist.DataTextField = "depname";
                    rblist.DataValueField = "depid";
                    break;
                case ORM_Types.Template_Type.Manual:
                    rblist.DataSource = ORM.List.get_dt_items(QUE.Question_ID);
                    rblist.DataTextField = "Item_Text";
                    rblist.DataValueField = "Item_ID";
                    break;
                default:
                    break;
            }
            if (!QUE.Question_Optional)
            {
                var rfv = GenerateReqField(QUE, rblist);
                output_panel.Controls.Add(rfv);
            }
            var cfv = GenerateCustomValid(QUE, rblist);

            output_panel.Controls.Add(cfv);
            output_panel.Controls.Add(rblist);
            rblist.DataBind();
        }


        void AddTxtBx(Question QUE)
        {

            var txt = GenerateTxt(QUE);
            var ft = GenerateFilterTxt(QUE, txt);
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            output_panel.Controls.Add(txt);
            output_panel.Controls.Add(ft);
            if (!QUE.Question_Optional)
            {
                var rfv = GenerateReqField(QUE, txt);
                output_panel.Controls.Add(rfv);
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
        }

        void AddNumBx(Question QUE)
        {
#warning test
            var txt1 = new TextBox();
            txt1.ID = "NUM" + QUE.Question_ID + "_" + QUE.Question_Index;
            var ft1 = GenerateFilterNum(QUE, txt1);
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            output_panel.Controls.Add(txt1);
            txt1.Attributes["value"] = "";
            txt1.Attributes["onKeyUp"] = "javascript:moneyCommaSep(this);";
            output_panel.Controls.Add(ft1);
            if (!QUE.Question_Optional)
            {
                var rfv = GenerateReqField(QUE, txt1);
                output_panel.Controls.Add(rfv);
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
        }

        void AddDateBx(Question QUE)
        {
#warning test
            var pdt = new PersianDateTextBox()
            {
                IconUrl = "images/cal.gif",
                ID = "PDT" + QUE.Question_ID + "_" + QUE.Question_Index,
                DefaultDate = DateTime.Now.ToString("d")
            };

            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            output_panel.Controls.Add(pdt);
            if (!QUE.Question_Optional)
            {
                var rfv = new RequiredFieldValidator()
                {
                    ID = "rfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                    ControlToValidate = pdt.ID,
                    Display = ValidatorDisplay.Dynamic,
                    ErrorMessage = "لطفا اين گزينه را خالي نگذاريد"
                };
                output_panel.Controls.Add(rfv);
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
        }

        void AddTblBx(Question QUE)
        {
#warning test
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            var gvlist = new GridView();
            gvlist.ID = "GV" + QUE.Question_ID + "_" + QUE.Question_Index;
            gvlist.AutoGenerateColumns = false;
            gvlist.RowDeleting += new GridViewDeleteEventHandler(gvlist_RowDeleting);
            gvlist.RowDataBound += new GridViewRowEventHandler(gvlist_RowDataBound);
            var dt = new DataTable();
            if (QUE.Template_Type == ORM_Types.Template_Type.Manual)
            {
                foreach (var VARIABLE in ORM.List.get_dt_items(
                    QUE.Question_ID).AsEnumerable())
                {
                    var bfield = new TemplateField();
                    var ltableId = VARIABLE.Field<Nullable<Int32>>("Table_ID");
                    bfield.HeaderTemplate = new GridViewTemplate(
                        ListItemType.Header,
                        VARIABLE.Field<Int64>("Item_id"),
                        VARIABLE.Field<string>("Item_Text"),
                        VARIABLE.Field<Boolean>("Optional"),
                        VARIABLE.Field<Int32>("Item_Type"),
                        VARIABLE.Field<Boolean>("Filled"), ltableId);
                    bfield.ItemTemplate = new GridViewTemplate(
                        ListItemType.Item, VARIABLE.Field<Int64>("Item_id"),
                        VARIABLE.Field<string>("Item_Text"), VARIABLE.Field<Boolean>("Optional"),
                        VARIABLE.Field<Int32>("Item_Type"), VARIABLE.Field<Boolean>("Filled"),
                        ltableId);
                    gvlist.Columns.Add(bfield);
                    dt.Columns.Add(VARIABLE.Field<Int64>("Item_id").ToString());
                }
                var cfield = GenerateCmdField();
                gvlist.Columns.Add(cfield);
                dt.Rows.Add();
                if (_sender.IsPostBack)
                {
                    if (_sender.Session[gvlist.ID] != null)
                        dt = (DataTable)_sender.Session[gvlist.ID];
                }
                else
                    _sender.Session[gvlist.ID] = null;
                gvlist.DataSource = dt;
                gvlist.DataBind();
            }
            output_panel.Controls.Add(gvlist);
            var b = new Button();
            b.ID = "btnAddNewRow" + QUE.Question_ID + "_" + QUE.Question_Index;
            b.Text = "ذخیره سطر";
            b.Click += btnAddNewRow_Click;
            b.CausesValidation = false;
            output_panel.Controls.Add(b);
        }

        void AddDrpLstTxt(Question QUE)
        {
#warning test
            var drlist = new DropDownList();
            drlist.ID = "DDL" + QUE.Question_ID + "_" +
                QUE.Question_Index;
            switch (QUE.Template_Type)
            {
                case ORM_Types.Template_Type.State:
                    drlist.DataSource = State.get_states();
                    drlist.DataTextField = "locname";
                    drlist.DataValueField = "locid";
                    break;
                case ORM_Types.Template_Type.Branch:
                    drlist.DataSource = Branch.get_Branchs();
                    drlist.DataTextField = "title";
                    drlist.DataValueField = "Bid";
                    break;
                case ORM_Types.Template_Type.Department:
                    drlist.DataSource = Department.get_deps();
                    drlist.DataTextField = "depname";
                    drlist.DataValueField = "depid";
                    break;
                case ORM_Types.Template_Type.Manual:
                    drlist.DataSource = ORM.List.get_dt_items(QUE.Question_ID);
                    drlist.DataTextField = "Item_Text";
                    drlist.DataValueField = "Item_ID";
                    break;
                default:
                    break;
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            output_panel.Controls.Add(drlist);
            drlist.DataBind();

            var txt = new TextBox()
            {
                Width = 400,
                TextMode = TextBoxMode.MultiLine,
                Wrap = true,
                ID = "TB" + QUE.Question_ID + "_" + QUE.Question_Index
            };
            var ft = new FilteredTextBoxExtender()
            {
                ID = "ft" + QUE.Question_ID + "_" + QUE.Question_Index,
                FilterInterval = 250,
                FilterMode = FilterModes.InvalidChars,
                InvalidChars = "'`;\\\"",
                FilterType = FilterTypes.Custom,
                TargetControlID = txt.ID
            };
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            output_panel.Controls.Add(txt);
            output_panel.Controls.Add(ft);
            if (!QUE.Question_Optional)
            {
                var rfv = new RequiredFieldValidator()
                {
                    ID = "rfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                    ControlToValidate = txt.ID,
                    Display = ValidatorDisplay.Dynamic,
                    ErrorMessage = "لطفا اين گزينه را خالي نگذاريد"
                };
                output_panel.Controls.Add(rfv);
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
        }

        void AddRdoLstTxt(Question QUE)
        {
#warning Test
            var rblist = new RadioButtonList();
            rblist.ID = "RBL" + QUE.Question_ID + "_" + QUE.Question_Index;
            switch (QUE.Template_Type)
            {
                case ORM_Types.Template_Type.State:
                    rblist.DataSource = State.get_states();
                    rblist.DataTextField = "locname";
                    rblist.DataValueField = "locid";
                    break;
                case ORM_Types.Template_Type.Branch:
                    rblist.DataSource = Branch.get_Branchs();
                    rblist.DataTextField = "title";
                    rblist.DataValueField = "Bid";
                    break;
                case ORM_Types.Template_Type.Department:
                    rblist.DataSource = Department.get_deps();
                    rblist.DataTextField = "depname";
                    rblist.DataValueField = "depid";
                    break;
                case ORM_Types.Template_Type.Manual:
                    rblist.DataSource = ORM.List.get_dt_items(QUE.Question_ID);
                    rblist.DataTextField = "Item_Text";
                    rblist.DataValueField = "Item_ID";
                    break;
                default:
                    break;
            }
            if (!QUE.Question_Optional)
            {
                var rfv = new RequiredFieldValidator()
                {
                    ID = "rfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                    ControlToValidate = rblist.ID,
                    Display = ValidatorDisplay.Static,
                    ErrorMessage = "لطفا جواب سئوال را بدهید",
                };
                output_panel.Controls.Add(rfv);
            }
            var cfv = new CustomValidator()
            {
                ID = "cfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                ControlToValidate = rblist.ID,
                Display = ValidatorDisplay.Static,
                ErrorMessage = "     این سئوال باید متناسب با شرط پاسخ داده شود"
            };

            output_panel.Controls.Add(cfv);
            output_panel.Controls.Add(rblist);
            rblist.DataBind();

            var txt = new TextBox()
            {
                Width = 400,
                TextMode = TextBoxMode.MultiLine,
                Wrap = true,
                ID = "TB" + QUE.Question_ID + "_" + QUE.Question_Index
            };
            var ft = new FilteredTextBoxExtender()
            {
                ID = "ft" + QUE.Question_ID + "_" + QUE.Question_Index,
                FilterInterval = 250,
                FilterMode = FilterModes.InvalidChars,
                InvalidChars = "'`;\\\"",
                FilterType = FilterTypes.Custom,
                TargetControlID = txt.ID
            };
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            output_panel.Controls.Add(txt);
            output_panel.Controls.Add(ft);
            if (!QUE.Question_Optional)
            {
                var rfv = new RequiredFieldValidator()
                {
                    ID = "rfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                    ControlToValidate = txt.ID,
                    Display = ValidatorDisplay.Dynamic,
                    ErrorMessage = "لطفا اين گزينه را خالي نگذاريد"
                };
                output_panel.Controls.Add(rfv);
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
        }


        void AddChkBxTxt(Question QUE)
        {
#warning test
            var chklist = new CheckBoxList()
            {
                ID = "CBL" + QUE.Question_ID + "_" + QUE.Question_Index,
            };
            switch (QUE.Template_Type)
            {
                case ORM_Types.Template_Type.State:
                    chklist.DataSource = State.get_states();
                    chklist.DataTextField = "locname";
                    chklist.DataValueField = "locid";
                    break;
                case ORM_Types.Template_Type.Branch:
                    chklist.DataSource = Branch.get_Branchs();
                    chklist.DataTextField = "title";
                    chklist.DataValueField = "Bid";
                    break;
                case ORM_Types.Template_Type.Department:
                    chklist.DataSource = Department.get_deps();
                    chklist.DataTextField = "depname";
                    chklist.DataValueField = "depid";
                    break;
                case ORM_Types.Template_Type.Manual:
                    chklist.DataSource = ORM.List.get_dt_items(QUE.Question_ID);
                    chklist.DataTextField = "Item_Text";
                    chklist.DataValueField = "Item_ID";
                    break;
                default:
                    break;
            }
            output_panel.Controls.Add(chklist);
            chklist.DataBind();
            if (chklist.Items.Count > 0)
                chklist.Items[0].Selected = true;

            var txt = GenerateTxt(QUE);
            var ft = GenerateFilterTxt(QUE, txt);
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            output_panel.Controls.Add(txt);
            output_panel.Controls.Add(ft);
            if (!QUE.Question_Optional)
            {
                var rfv = GenerateReqField(QUE, txt);
                output_panel.Controls.Add(rfv);
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
        }

        static CustomValidator GenerateCustomValid(Question QUE, RadioButtonList rblist)
        {
            return new CustomValidator()
            {
                ID = "cfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                ControlToValidate = rblist.ID,
                Display = ValidatorDisplay.Static,
                ErrorMessage = "     این سئوال باید متناسب با شرط پاسخ داده شود"
            };
        }

        static RequiredFieldValidator GenerateReqField(Question QUE, RadioButtonList rblist)
        {
            return new RequiredFieldValidator()
            {
                ID = "rfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                ControlToValidate = rblist.ID,
                Display = ValidatorDisplay.Static,
                ErrorMessage = "لطفا جواب سئوال را بدهید",
            };
        }

        static RequiredFieldValidator GenerateReqField(Question QUE, TextBox txt)
        {
            return new RequiredFieldValidator()
            {
                ID = "rfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                ControlToValidate = txt.ID,
                Display = ValidatorDisplay.Dynamic,
                ErrorMessage = "لطفا اين گزينه را خالي نگذاريد"
            };
        }

        static FilteredTextBoxExtender GenerateFilterTxt(Question QUE, TextBox txt)
        {
            return new FilteredTextBoxExtender()
            {
                ID = "ft" + QUE.Question_ID + "_" + QUE.Question_Index,
                FilterInterval = 250,
                FilterMode = FilterModes.InvalidChars,
                InvalidChars = "'`;\\\"",
                FilterType = FilterTypes.Custom,
                TargetControlID = txt.ID
            };
        }
        static FilteredTextBoxExtender GenerateFilterNum(Question QUE, TextBox txt1)
        {
            return new FilteredTextBoxExtender()
            {
                ID = "ft" + QUE.Question_ID + "_" + QUE.Question_Index,
                FilterInterval = 250,
                FilterMode = FilterModes.ValidChars,
                ValidChars = "0123456789-+.,",
                FilterType = FilterTypes.Custom,
                TargetControlID = txt1.ID
            };
        }
        static CommandField GenerateCmdField()
        {
            return new CommandField()
            {
                ButtonType = ButtonType.Image,
                DeleteImageUrl = "images/delete-small.jpg",
                ShowDeleteButton = true,
                DeleteText = "حذف",
            };
        }
        static TextBox GenerateTxt(Question QUE)
        {
#warning test
            return new TextBox()
            {
                Width = 400,
                TextMode = TextBoxMode.MultiLine,
                Wrap = true,
                ID = "TB" + QUE.Question_ID + "_" + QUE.Question_Index
            };
        }
        #endregion
        protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var gv = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // loop all data rows
                foreach (DataControlFieldCell cell in e.Row.Cells)
                {
                    // check all cells in one row
                    foreach (Control control in cell.Controls)
                    {
                        // Must use LinkButton here instead of ImageButton
                        // if you are having Links (not images) as the command button.
                        ImageButton button = control as ImageButton;
                        if (button != null && button.CommandName == "Delete")
                        {
                            if (gv.Rows.Count > 0)
                                // Add delete confirmation
                                button.OnClientClick = "if (!confirm('آیا مطمئن هستيد که می خواهید این ردیف را حذف کنید؟')) return;";

                        }

                    }
                }
            }
        }
        protected void gvlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv.Rows.Count > 1)
            {
                DataTable dt = (DataTable)gv.DataSource;
                dt.Rows[e.RowIndex].Delete();
                gv.DataSource = null;
                gv.DataSource = dt;
                gv.DataBind();
            }
        }
        public void btnAddNewRow_Click(object sender, EventArgs e)
        {
            var b = (Button)sender;
            string sBut = b.ID.Substring(12);
            String GridID = "GV" + sBut;
            var gv = output_panel.FindControl(GridID) as GridView;
            var dt = (DataTable)gv.DataSource;
            int k = 0;
            foreach (TableCell VARIABLE in gv.Rows[gv.Rows.Count - 1].Cells)
            {
                string ctrName = VARIABLE.Controls[0].GetType().Name;
                if (ctrName == "TextBox")
                {
                    var txt = VARIABLE.Controls[0] as TextBox;
                    dt.Rows[dt.Rows.Count - 1][k++] = txt.Text;
                }
                else if (ctrName == "DropDownList")
                {
                    var drd = VARIABLE.Controls[0] as DropDownList;
                    dt.Rows[dt.Rows.Count - 1][k++] = drd.Text;
                }
                else if (ctrName == "PersianDateTextBox")
                {
                    var pdf = VARIABLE.Controls[0] as PersianDateTextBox;
                    dt.Rows[dt.Rows.Count - 1][k++] = pdf.Text;
                }

            }
            for (int i = 0; i < HttpContext.Current.Session.Keys.Count; i++)
            {
                if (HttpContext.Current.Session.Keys[i].ToString().Contains("rad"))
                {
                    HttpContext.Current.Session[HttpContext.Current.Session.Keys[i].ToString()] = "";
                }
            }
            dt.Rows.Add();
            gv.DataSource = dt;
            gv.DataBind();
            _sender.Session["GV" + b.ID.Substring(12)] = dt;

        }
        public void btnAddOldRow_Click(object sender, EventArgs e)
        {
            var b = (Button)sender;
            string sBut = b.ID.Substring(12);
            String GridID = "GV" + sBut;
            string QuestID = sBut.Substring(0, sBut.Length - (sBut.Length - sBut.LastIndexOf("_")));
            SQL_Connector connector = new SQL_Connector();
            String Sql_query = "select Question1 from dbo.Question_Relation where Question2 =" + QuestID;
            string QuestID1 = connector.execute_scalar(Sql_query);
            var gv = output_panel.FindControl(GridID) as GridView;
            var dt = (DataTable)gv.DataSource;
            var k = 0;
            foreach (TableCell VARIABLE in gv.Rows[gv.Rows.Count - 1].Cells)
            {
                string ctrName = VARIABLE.Controls[0].GetType().Name;
                if (ctrName == "TextBox")
                {
                    var txt = VARIABLE.Controls[0] as TextBox;
                    dt.Rows[dt.Rows.Count - 1][k++] = txt.Text;
                }
                else if (ctrName == "DropDownList")
                {
                    var drd = VARIABLE.Controls[0] as DropDownList;
                    dt.Rows[dt.Rows.Count - 1][k++] = drd.Text;
                }
                else if (ctrName == "PersianDateTextBox")
                {
                    var pdf = VARIABLE.Controls[0] as PersianDateTextBox;
                    dt.Rows[dt.Rows.Count - 1][k++] = pdf.Text;
                }
            }
            for (int i = 0; i < HttpContext.Current.Session.Keys.Count; i++)
            {
                if (HttpContext.Current.Session.Keys[i].ToString().Contains("rad"))
                {
                    HttpContext.Current.Session[HttpContext.Current.Session.Keys[i].ToString()] = "";
                }
            }
            dt.Rows.Add();
            gv.DataSource = dt;
            gv.DataBind();
            _sender.Session["GV" + b.ID.Substring(12)] = dt;
        }

        public bool btnValidate(string QuestID, Panel input_form)
        {
            return true;
        }
        /// <summary>
        /// ////////////////////////////////////////
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txt1_TextChanged(object sender, EventArgs e)
        {
            TextBox numdata = (TextBox)sender;
            string sTxt = numdata.ID.Substring(12);
        }

        public bool register_form(Panel frmInpt, String fid, string user)
        {
            var frmInstnc = new Form_Instance()
            {
                Fill_Date = "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "'",
                Form_ID = fid,
                User_Name = user
            };
            foreach (object obj in this.Local_Form.Form_Question)
            {
                var QUE = (Question)obj;
                if (!QUE.Question_active)
                    continue;
                var ANSR = new Form_Question_Instance();
                ANSR.Question_ID = QUE.Question_ID;
                switch (QUE.Question_Type)
                {
                    case ORM_Types.Question_Type.ChckBxLstTxt:
                        AddAnsrChckBxLstTxt(QUE, frmInpt, ANSR, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.DrpDnLstTxt:
                        AddAnsrDrpDnLstTxt(QUE, frmInpt, ANSR, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.RdoBtnLstTxt:
                        AddAnsrRdoBtnLstTxt(QUE, frmInpt, ANSR, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.Table:
                        AddAnsrTbl(QUE, frmInpt, ANSR, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.ComboBoxList:
                        AddAnsrChkbxLst(QUE, frmInpt, ANSR, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.DropDownList:
                        AddDrpLst(QUE, frmInpt, ANSR, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.RadioButtonList:
                        AddRbLst(QUE, frmInpt, ANSR, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.Text:
                        AddAnsrTxt(QUE, frmInpt, ANSR, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.Number:
                        AddAnsrNum(QUE, frmInpt, ANSR, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.Date:
                        AddAnsrDate(QUE, frmInpt, ANSR, frmInstnc);
                        break;
                }
            }
            frmInstnc.Save();
            return true;
        }


        #region register_form
        bool AddAnsrTbl(Question QUE,
           Panel frmInpt,
           Form_Question_Instance ANSR,
           Form_Instance frmInstnc)
        {
            String GridID = "GV" + QUE.Question_ID + "_" + QUE.Question_Index;
            var result11 = frmInpt.FindControl(GridID) as GridView;
            var dt = (DataTable)result11.DataSource;
            if (dt.Rows.Count == 1)
                if (QUE.Question_Optional)
                    return false;
                else
                {
                    if (!btnValidate(ANSR.Question_ID, frmInpt)) return false;
                    for (int i = 0; i < dt.Rows.Count; i++)
                        foreach (DataColumn item in dt.Columns)
                        {
                            if (dt.Rows[i][item.ColumnName].ToString().Length == 0)
                                continue;
                            var newanswer2 = new Form_Question_Instance()
                            {
                                Question_ID = QUE.Question_ID,
                                Answer_ID = (i + 1).ToString(),
                                Item_ID = item.ColumnName,
                                Text_Field = dt.Rows[i][item.ColumnName].ToString()
                            };
                            frmInstnc.Add_Answer(newanswer2);
                        }
                }
            return true;
        }
        bool AddAnsrDate(Question Current_Question, Panel input_form, Form_Question_Instance newanswer, Form_Instance frmInstnc)
        {
            var PDTID = "PDT" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;
            var result5 = input_form.FindControl(PDTID) as PersianDateTextBox;
            if (result5.DateValue == null)
            {
                if (!Current_Question.Question_Optional) return false;
                else newanswer.Date_Field = null;
            }
            else
            {
                if (!btnValidate(newanswer.Question_ID, input_form)) return false;
                newanswer.Date_Field = (Convert.ToDateTime(result5.DateValue.ToString())).Date.ToString("d");
                newanswer.Date_Field = result5.DateValue.ToString().Substring(6, 4) + "-" + result5.DateValue.ToString().Substring(3, 2) + "-" + result5.DateValue.ToString().Substring(0, 2);
            }
            frmInstnc.Add_Answer(newanswer);
            return true;
        }

        bool AddAnsrNum(Question Current_Question, Panel input_form, Form_Question_Instance newanswer, Form_Instance frmInstnc)
        {
            String NumberID = "NUM" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;
            SQL_Connector connector2 = new SQL_Connector();
            TextBox result4 = input_form.FindControl(NumberID) as TextBox;
            if (result4.Text.Trim().Length == 0)
            { if (!Current_Question.Question_Optional) return false; }
            newanswer.Number_filed = result4.Text.Replace(",", "");
            frmInstnc.Add_Answer(newanswer);
            return true;
        }

        bool AddAnsrTxt(Question Current_Question, Panel input_form, Form_Question_Instance newanswer, Form_Instance frmInstnc)
        {
            var TextBoxID = "TB" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;
            var result3 = input_form.FindControl(TextBoxID) as TextBox;
            if (result3.Text.Trim().Length == 0)
            { if (!Current_Question.Question_Optional) return false; }
            newanswer.Text_Field = result3.Text;
            frmInstnc.Add_Answer(newanswer);
            return true;
        }

        bool AddRbLst(Question Current_Question, Panel input_form, Form_Question_Instance newanswer, Form_Instance frmInstnc)
        {
            String RadioButtonID = "RBL" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;
            RadioButtonList result2 = input_form.FindControl(RadioButtonID) as RadioButtonList;
            if (result2.SelectedValue.Length == 0) { if (!Current_Question.Question_Optional) return false; }
            SetValue(Current_Question.Template_Type, newanswer, result2.SelectedValue);
            if (!btnValidate(newanswer.Question_ID, input_form)) return false;
            frmInstnc.Add_Answer(newanswer);
            return true;
        }

        bool AddDrpLst(Question Current_Question, Panel input_form, Form_Question_Instance newanswer, Form_Instance frmInstnc)
        {
            String DropDownID = "DDL" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;
            DropDownList result1 = input_form.FindControl(DropDownID) as DropDownList;
            if (!btnValidate(newanswer.Question_ID, input_form)) return false;
            newanswer = SetValue(Current_Question.Template_Type, newanswer, result1.SelectedValue);
            frmInstnc.Add_Answer(newanswer);
            return true;
        }

        bool AddAnsrChkbxLst(Question Current_Question, Panel input_form, Form_Question_Instance newanswer, Form_Instance frmInstnc)
        {
            String ComboboxID = "CBL" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;
            CheckBoxList result = input_form.FindControl(ComboboxID) as CheckBoxList;
            foreach (ListItem item in result.Items)
                if (item.Selected)
                {
                    if (!btnValidate(newanswer.Question_ID, input_form)) return false;
                    newanswer = SetValue(Current_Question.Template_Type, newanswer, item.Value);
                }
            frmInstnc.Add_Answer(newanswer);
            return true;
        }


        bool AddAnsrRdoBtnLstTxt(Question Current_Question,
            Panel input_form,
            Form_Question_Instance newanswer,
            ORM.Form_Instance newinstance)
        {
#warning test
            String RadioButtonID = "RBL" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;
            RadioButtonList result2 = input_form.FindControl(RadioButtonID) as RadioButtonList;
            if (result2.SelectedValue.Length == 0) { if (!Current_Question.Question_Optional) return false; }
            SetValue(Current_Question.Template_Type, newanswer, result2.SelectedValue);
            if (!btnValidate(newanswer.Question_ID, input_form)) return false;
            newinstance.Add_Answer(newanswer);
            return true;
        }
        bool AddAnsrDrpDnLstTxt(Question Current_Question,
            Panel input_form,
            Form_Question_Instance newanswer,
            ORM.Form_Instance newinstance)
        {
#warning test
            String DropDownID = "DDL" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;
            DropDownList result1 = input_form.FindControl(DropDownID) as DropDownList;
            newanswer = SetValue(Current_Question.Template_Type, newanswer, result1.SelectedValue);
            newinstance.Add_Answer(newanswer);
            return true;
        }


        bool AddAnsrChckBxLstTxt(Question Current_Question,
            Panel input_form,
            Form_Question_Instance newanswer,
            ORM.Form_Instance newinstance)
        {
#warning test
            String ComboboxID = "CBL" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;
            CheckBoxList result = input_form.FindControl(ComboboxID) as CheckBoxList;
            foreach (ListItem item in result.Items)
                if (item.Selected)
                {
                    if (!btnValidate(newanswer.Question_ID, input_form)) return false;
                    newanswer = SetValue(Current_Question.Template_Type, newanswer, item.Value);
                }
            newinstance.Add_Answer(newanswer);
            return true;
        }
        #endregion
        public bool update_form(Panel input_form, String fid, String instance_id)
        {
            var frmInstnc = new Form_Instance();
            foreach (object obj in this.Local_Form.Form_Question)
            {
                var QUE = (Question)obj;
                var frmQueInstnc = new Form_Question_Instance();
                frmQueInstnc.Question_ID = QUE.Question_ID;
                switch (QUE.Question_Type)
                {
                    case ORM_Types.Question_Type.ChckBxLstTxt:
                        return UpdtAnsrChckBxLstTxt(QUE, input_form, frmQueInstnc, frmInstnc);
                    case ORM_Types.Question_Type.DrpDnLstTxt:
                        return UpdtAnsrDrpDnLstTxt(QUE, input_form, frmQueInstnc, frmInstnc);
                    case ORM_Types.Question_Type.RdoBtnLstTxt:
                        return UpdtAnsrRdoBtnLstTxt(QUE, input_form, frmQueInstnc, frmInstnc);
                    case ORM_Types.Question_Type.Table:
                        String GridID = "GV" + QUE.Question_ID + "_" + QUE.Question_Index;
                        var result11 = input_form.FindControl(GridID) as GridView;
                        var dt = (DataTable)result11.DataSource;
                        if (dt.Rows.Count == 1) { if (QUE.Question_Optional) break; else return false; }
                        else
                        {
                            if (!btnValidate(frmQueInstnc.Question_ID, input_form)) return false;
                            for (int i = 0; i < dt.Rows.Count; i++)
                                foreach (DataColumn item in dt.Columns)
                                {
                                    if (dt.Rows[i][item.ColumnName].ToString().Length == 0)
                                        continue;
                                    var newanswer2 = new Form_Question_Instance();
                                    newanswer2.Question_ID = QUE.Question_ID;
                                    newanswer2.Answer_ID = (i + 1).ToString();
                                    newanswer2.Item_ID = item.ColumnName;
                                    newanswer2.Text_Field = dt.Rows[i][item.ColumnName].ToString();
                                    frmInstnc.Add_Answer(newanswer2);
                                }
                        }
                        break;
                    case ORM_Types.Question_Type.ComboBoxList:

                        String ComboboxID = "CBL" + QUE.Question_ID + "_" + QUE.Question_Index;
                        CheckBoxList result = input_form.FindControl(ComboboxID) as CheckBoxList;

                        foreach (ListItem item in result.Items)
                            if (item.Selected)
                            {
                                if (!btnValidate(frmQueInstnc.Question_ID, input_form)) return false;
                                frmQueInstnc = SetValue(QUE.Template_Type, frmQueInstnc, item.Value);
                            }
                        frmInstnc.Add_Answer(frmQueInstnc);
                        break;
                    case ORM_Types.Question_Type.DropDownList:
                        String DropDownID = "DDL" + QUE.Question_ID + "_" + QUE.Question_Index;
                        DropDownList result1 = input_form.FindControl(DropDownID) as DropDownList;
                        if (!btnValidate(frmQueInstnc.Question_ID, input_form)) return false;
                        frmQueInstnc = SetValue(QUE.Template_Type, frmQueInstnc, result1.SelectedValue);
                        frmInstnc.Add_Answer(frmQueInstnc);
                        break;

                    case ORM_Types.Question_Type.RadioButtonList:
                        String RadioButtonID = "RBL" + QUE.Question_ID + "_" + QUE.Question_Index;
                        RadioButtonList result2 = input_form.FindControl(RadioButtonID) as RadioButtonList;
                        if (result2.SelectedValue.Length == 0) { if (!QUE.Question_Optional) return false; }
                        SetValue(QUE.Template_Type, frmQueInstnc, result2.SelectedValue);
                        if (!btnValidate(frmQueInstnc.Question_ID, input_form)) return false;
                        frmInstnc.Add_Answer(frmQueInstnc);
                        break;
                    case ORM_Types.Question_Type.Text:
                        String TextBoxID = "TB" + QUE.Question_ID + "_" + QUE.Question_Index;
                        TextBox result3 = input_form.FindControl(TextBoxID) as TextBox;
                        if (result3.Text.Trim().Length == 0)
                        { if (!QUE.Question_Optional) return false; }
                        else
                        {
                            if (!btnValidate(frmQueInstnc.Question_ID, input_form)) return false;
                        }
                        frmQueInstnc.Text_Field = result3.Text;
                        frmInstnc.Add_Answer(frmQueInstnc);
                        break;
                    case ORM_Types.Question_Type.Number:
                        String NumberID = "NUM" + QUE.Question_ID + "_" + QUE.Question_Index;
                        TextBox result4 = input_form.FindControl(NumberID) as TextBox;
                        if (result4.Text.Trim().Length == 0)
                        { if (!QUE.Question_Optional) return false; }
                        else
                        {
                            if (!btnValidate(frmQueInstnc.Question_ID, input_form)) return false;
                        }
                        frmQueInstnc.Number_filed = result4.Text.Replace(",", "");
                        frmInstnc.Add_Answer(frmQueInstnc);
                        break;
                    case ORM_Types.Question_Type.Date:
                        String PDTID = "PDT" + QUE.Question_ID + "_" + QUE.Question_Index;
                        PersianDateTextBox result5 = input_form.FindControl(PDTID) as PersianDateTextBox;
                        if (result5.DateValue == null)
                        {
                            if (!QUE.Question_Optional) return false;
                            else frmQueInstnc.Date_Field = null;
                        }
                        else
                        {
                            if (!btnValidate(frmQueInstnc.Question_ID, input_form)) return false;
                            frmQueInstnc.Date_Field = (Convert.ToDateTime(result5.DateValue.ToString())).Date.ToString("d");
                        }
                        frmInstnc.Add_Answer(frmQueInstnc);
                        break;
                }
            }
            ORM.Form_Question_Instance.Delete(instance_id);
            frmInstnc.Update(instance_id);
            return true;
        }
        #region update_form
        bool UpdtAnsrRdoBtnLstTxt(Question Current_Question,
        Panel input_form,
                Form_Question_Instance newanswer,
                Form_Instance newinstance)
        {
#warning test
            String RadioButtonID = "RBL" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;
            RadioButtonList tmpRdo = input_form.FindControl(RadioButtonID) as RadioButtonList;
            if (tmpRdo.SelectedValue.Length == 0) { if (!Current_Question.Question_Optional) return false; }
            SetValue(Current_Question.Template_Type, newanswer, tmpRdo.SelectedValue);
            if (!btnValidate(newanswer.Question_ID, input_form)) return false;
            newinstance.Add_Answer(newanswer);

            String TextBoxID = "TB" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;

            TextBox tmpTxt = input_form.FindControl(TextBoxID) as TextBox;
            if (tmpTxt.Text.Trim().Length == 0)
            { if (!Current_Question.Question_Optional) return false; }
            else
            {
                if (!btnValidate(newanswer.Question_ID, input_form)) return false;
            }
            newanswer.Text_Field = tmpTxt.Text;
            newinstance.Add_Answer(newanswer);
            return true;
        }

        bool UpdtAnsrDrpDnLstTxt(Question Current_Question,
                Panel input_form,
                Form_Question_Instance newanswer,
                Form_Instance newinstance)
        {
#warning test
            String DropDownID = "DDL" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;
            DropDownList tmpDrp = input_form.FindControl(DropDownID) as DropDownList;
            if (!btnValidate(newanswer.Question_ID, input_form)) return false;
            newanswer = SetValue(Current_Question.Template_Type, newanswer, tmpDrp.SelectedValue);
            newinstance.Add_Answer(newanswer);

            String TextBoxID = "TB" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;

            TextBox tmpTxt = input_form.FindControl(TextBoxID) as TextBox;
            if (tmpTxt.Text.Trim().Length == 0)
            { if (!Current_Question.Question_Optional) return false; }
            else
            {
                if (!btnValidate(newanswer.Question_ID, input_form)) return false;
            }
            newanswer.Text_Field = tmpTxt.Text;
            newinstance.Add_Answer(newanswer);
            return true;
        }


        bool UpdtAnsrChckBxLstTxt(Question Current_Question,
                Panel input_form,
                Form_Question_Instance newanswer,
                Form_Instance newinstance)
        {
#warning test
            String ComboboxID = "CBL" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;
            CheckBoxList tmpCmb = input_form.FindControl(ComboboxID) as CheckBoxList;
            foreach (ListItem item in tmpCmb.Items)
                if (item.Selected)
                {
                    if (!btnValidate(newanswer.Question_ID, input_form)) return false;
                    newanswer = SetValue(Current_Question.Template_Type, newanswer, item.Value);
                }

            String TextBoxID = "TB" + Current_Question.Question_ID + "_" + Current_Question.Question_Index;

            TextBox tmpTxt = input_form.FindControl(TextBoxID) as TextBox;
            if (tmpTxt.Text.Trim().Length == 0)
            { if (!Current_Question.Question_Optional) return false; }
            else
            {
                if (!btnValidate(newanswer.Question_ID, input_form)) return false;
            }
            newanswer.Text_Field = tmpTxt.Text;
            newinstance.Add_Answer(newanswer);
            return true;
        }

        #endregion
        public Panel Fill_Panel(string instncId, string cdMhlKhmt)
        {
            var frmInstnc = new Form_Instance(instncId);
            this.AddFirstSection();
            AddSctionCdMhlKhdmt(cdMhlKhmt);
            this.AddLastSection();
            foreach (object obj in this.Local_Form.Form_Question)
            {
                var QUE = (Question)obj;
                if (!QUE.Question_active)
                    continue;
                this.AddFirstSection();
                AddLblQuestionDes(QUE);
                AddLblStar(QUE);
                switch (QUE.Question_Type)
                {
                    case ORM_Types.Question_Type.ChckBxLstTxt:
                        FllAnsrChckBxLstTxt(QUE, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.DrpDnLstTxt:
                        FllAnsrDrpDnLstTxt(QUE, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.RdoBtnLstTxt:
                        FllAnsrRdoBtnLstTxt(QUE, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.Table:
                        FllAnsrTbl(QUE, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.ComboBoxList:
                        FllAnsrChkLst(QUE, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.DropDownList:
                        FllAnsrDrpDnLst(QUE, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.RadioButtonList:
                        FllAnsrRdoBtnLst(QUE, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.Text:
                        FllAnrTxt(QUE, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.Number:
                        FllAnsrNum(QUE, frmInstnc);
                        break;
                    case ORM_Types.Question_Type.Date:
                        FllAnsrDate(QUE, frmInstnc);
                        break;
                }//end switch questionType
                this.AddLastSection();
            }//end foreach
            return output_panel;
        }
        #region FllMethods
        void AddSctionCdMhlKhdmt(string cdMhlKhmt)
        {
            var lblCdMhlKhmt = new Label();
            lblCdMhlKhmt.Font.Size = 9;
            lblCdMhlKhmt.Font.Name = "Tahoma";
            lblCdMhlKhmt.Font.Bold = true;
            lblCdMhlKhmt.ForeColor = System.Drawing.Color.Blue;
            lblCdMhlKhmt.Text = "کد محل خدمت";

            output_panel.Controls.Add(lblCdMhlKhmt);

            var txtCdMhlKhmt = new TextBox()
            {
                ID = "txtCodeMahalKhemat",
                Width = 400,
                TextMode = TextBoxMode.MultiLine,
                Wrap = true,
                Text = cdMhlKhmt
            };
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            output_panel.Controls.Add(txtCdMhlKhmt);
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
        }

        void FllAnsrDate(Question QUE, Form_Instance instnc)
        {
            var pdt = new PersianDateTextBox()
            {
                IconUrl = "images/cal.gif",
                ID = "PDT" + QUE.Question_ID + "_" + QUE.Question_Index,
                DefaultDate = DateTime.Now.ToString("d")
            };

            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            var RES = instnc.ReturnString(QUE.Question_ID, QUE.Question_Type);
            if (RES != "")
            {
                var datetime = Convert.ToDateTime(RES);
                pdt.DateValue = datetime;
            }
            output_panel.Controls.Add(pdt);
            if (!QUE.Question_Optional)
            {
                var rfv = new RequiredFieldValidator()
                {
                    ID = "rfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                    ControlToValidate = pdt.ID,
                    Display = ValidatorDisplay.Dynamic,
                    ErrorMessage = "لطفا اين گزينه را خالي نگذاريد"
                };

                output_panel.Controls.Add(rfv);
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
        }

        void FllAnsrNum(Question QUE, Form_Instance instnc)
        {
            var txt1 = new TextBox()
            {
                ID = "NUM" + QUE.Question_ID + "_" + QUE.Question_Index
            };
            var ft1 = new FilteredTextBoxExtender()
            {
                ID = "ft" + QUE.Question_ID + "_" + QUE.Question_Index,
                FilterInterval = 250,
                FilterMode = FilterModes.ValidChars,
                ValidChars = "0123456789-+.,",
                FilterType = FilterTypes.Custom,
                TargetControlID = txt1.ID
            };

            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            txt1.Text = instnc.ReturnString(QUE.Question_ID, QUE.Question_Type);
            output_panel.Controls.Add(txt1);
            txt1.Attributes["value"] = "";
            txt1.Attributes["onKeyUp"] = "javascript:moneyCommaSep(this);";
            output_panel.Controls.Add(ft1);
            if (!QUE.Question_Optional)
            {
                var rfv = new RequiredFieldValidator()
                {
                    ID = "rfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                    ControlToValidate = txt1.ID,
                    Display = ValidatorDisplay.Dynamic,
                    ErrorMessage = "لطفا اين گزينه را خالي نگذاريد"
                };
                output_panel.Controls.Add(rfv);
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
        }

        void FllAnrTxt(Question QUE, Form_Instance instnc)
        {
            var txt = new TextBox()
            {
                ID = "TB" + QUE.Question_ID + "_" + QUE.Question_Index,
                Width = 400,
                TextMode = TextBoxMode.MultiLine,
                Wrap = true
            };

            var ft = new FilteredTextBoxExtender()
            {
                ID = "ft" + QUE.Question_ID + "_" + QUE.Question_Index,
                FilterInterval = 250,
                FilterMode = FilterModes.InvalidChars,
                InvalidChars = "'`;\\\"",
                FilterType = FilterTypes.Custom,
                TargetControlID = txt.ID
            };

            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            txt.Text = instnc.ReturnString(QUE.Question_ID, QUE.Question_Type);
            output_panel.Controls.Add(txt);
            output_panel.Controls.Add(ft);
            if (!QUE.Question_Optional)
            {
                var rfv = new RequiredFieldValidator()
                {
                    ID = "rfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                    ControlToValidate = txt.ID,
                    Display = ValidatorDisplay.Dynamic,
                    ErrorMessage = "لطفا اين گزينه را خالي نگذاريد"
                };
                output_panel.Controls.Add(rfv);
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
        }

        void FllAnsrRdoBtnLst(Question QUE, Form_Instance instnc)
        {
            var rblist = new RadioButtonList();
            rblist.ID = "RBL" + QUE.Question_ID + "_" + QUE.Question_Index;
            switch (QUE.Template_Type)
            {
                case ORM_Types.Template_Type.State:
                    rblist.DataSource = State.get_states();
                    rblist.DataTextField = "locname";
                    rblist.DataValueField = "locid";
                    break;
                case ORM_Types.Template_Type.Branch:
                    rblist.DataSource = Branch.get_Branchs();
                    rblist.DataTextField = "title";
                    rblist.DataTextField = "Bid";
                    break;
                case ORM_Types.Template_Type.Department:
                    rblist.DataSource = Department.get_deps();
                    rblist.DataTextField = "depname";
                    rblist.DataValueField = "depid";
                    break;
                case ORM_Types.Template_Type.Manual:
                    rblist.DataSource = ORM.List.get_dt_items(QUE.Question_ID);
                    rblist.DataTextField = "Item_Text";
                    rblist.DataValueField = "Item_ID";
                    break;
                default:
                    break;
            }
            var tmpSlctd = instnc.ReturnLString(QUE.Question_ID, QUE.Template_Type);
            rblist.DataBind();
            if (tmpSlctd != null && rblist.Items.FindByValue(tmpSlctd[0].ToString()) != null)
                rblist.Items.FindByValue(tmpSlctd[0].ToString()).Selected = true;
            var cfv = new CustomValidator()
            {
                ID = "cfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                ControlToValidate = rblist.ID,
                Display = ValidatorDisplay.Dynamic,
                ErrorMessage = " این سئوال باید متناسب با شرط پاسخ داده شود"
            };

            output_panel.Controls.Add(cfv);
            output_panel.Controls.Add(rblist);
        }

        void FllAnsrDrpDnLst(Question QUE, Form_Instance instnc)
        {
            var drlist = new DropDownList();
            drlist.ID = "DDL" + QUE.Question_ID + "_" +
                QUE.Question_Index;
            switch (QUE.Template_Type)
            {
                case ORM_Types.Template_Type.State:
                    drlist.DataSource = State.get_states();
                    drlist.DataTextField = "locname";
                    drlist.DataValueField = "locid";
                    break;
                case ORM_Types.Template_Type.Branch:
                    drlist.DataSource = Branch.get_Branchs();
                    drlist.DataTextField = "title";
                    drlist.DataValueField = "Bid";
                    break;
                case ORM_Types.Template_Type.Department:
                    drlist.DataSource = Department.get_deps();
                    drlist.DataTextField = "depname";
                    drlist.DataValueField = "depid";
                    break;
                case ORM_Types.Template_Type.Manual:
                    drlist.DataSource = ORM.List.get_dt_items(QUE.Question_ID);
                    drlist.DataTextField = "Item_Text";
                    drlist.DataValueField = "Item_ID";
                    break;
                default:
                    break;
            }
            var tmpSlctd = instnc.ReturnLString(QUE.Question_ID, QUE.Template_Type);
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            output_panel.Controls.Add(drlist);
            drlist.DataBind();
            if (tmpSlctd != null && tmpSlctd.Count > 0)
                drlist.Items.FindByValue(tmpSlctd[0].ToString()).Selected = true;
        }

        void FllAnsrChkLst(Question QUE, Form_Instance instnc)
        {
            var chklist = new CheckBoxList();
            chklist.ID = "CBL" + QUE.Question_ID + "_" +
                QUE.Question_Index;
            switch (QUE.Template_Type)
            {
                case ORM_Types.Template_Type.State:
                    chklist.DataSource = State.get_states();
                    chklist.DataTextField = "locname";
                    chklist.DataValueField = "locid";
                    break;
                case ORM_Types.Template_Type.Branch:
                    chklist.DataSource = Branch.get_Branchs();
                    chklist.DataTextField = "title";
                    chklist.DataValueField = "Bid";
                    break;
                case ORM_Types.Template_Type.Department:
                    chklist.DataSource = Department.get_deps();
                    chklist.DataTextField = "depname";
                    chklist.DataTextField = "depid";//Err.prob
                    break;
                case ORM_Types.Template_Type.Manual:
                    chklist.DataSource = ORM.List.get_dt_items(QUE.Question_ID);
                    chklist.DataTextField = "Item_Text";
                    chklist.DataValueField = "Item_ID";
                    break;
                default:
                    break;
            }
            output_panel.Controls.Add(chklist);
            chklist.DataBind();
            var tmpSlctd = instnc.ReturnLString(QUE.Question_ID, QUE.Template_Type);
            if (tmpSlctd != null && tmpSlctd.Count > 0)
                foreach (Object obj1 in tmpSlctd)
                {
                    var value = (String)obj1;
                    if (chklist.Items.FindByValue(value) == null) break;
                    chklist.Items.FindByValue(value).Selected = true;
                }
        }

        void FllAnsrTbl(Question QUE, Form_Instance instnc)
        {
            var gvlist = new GridView();
            gvlist.ID = "GV" + QUE.Question_ID + "_" +
                QUE.Question_Index;
            gvlist.AutoGenerateColumns = false;
            gvlist.RowDeleting += new GridViewDeleteEventHandler(gvlist_RowDeleting);
            gvlist.RowDataBound += new GridViewRowEventHandler(gvlist_RowDataBound);
            var dt = new DataTable();
            if (QUE.Template_Type == ORM_Types.Template_Type.Manual)
            {
                foreach (var VARIABLE in ORM.List.get_dt_items(
                    QUE.Question_ID).AsEnumerable())
                {
                    var bfield = new TemplateField();
                    var ltableId = VARIABLE.Field<Nullable<Int32>>("Table_ID");
                    bfield.HeaderTemplate = new GridViewTemplate(
                        ListItemType.Header, VARIABLE.Field<Int64>("Item_id"),
                        VARIABLE.Field<string>("Item_text"), VARIABLE.Field<Boolean>("Optional"),
                        VARIABLE.Field<Int32>("Item_Type"), VARIABLE.Field<Boolean>("Filled"),
                        ltableId);
                    bfield.ItemTemplate = new GridViewTemplate(
                        ListItemType.Item, VARIABLE.Field<Int64>("Item_id"),
                        VARIABLE.Field<string>("Item_text"), VARIABLE.Field<Boolean>("Optional"),
                        VARIABLE.Field<Int32>("Item_Type"), VARIABLE.Field<Boolean>("Filled"),
                        ltableId);
                    gvlist.Columns.Add(bfield);
                }
                var cfield = new CommandField();
                cfield.ButtonType = ButtonType.Image;
                cfield.DeleteImageUrl = "images/delete-small.jpg";
                cfield.ShowDeleteButton = true;
                cfield.DeleteText = "حذف";
                gvlist.Columns.Add(cfield);
                dt.Rows.Add();
                var DTAnswers = instnc.ReturnDTAnswers(QUE.Question_ID);
                if (_sender.IsPostBack)
                {
                    if (_sender.Session[gvlist.ID] != null)
                        dt = (DataTable)_sender.Session[gvlist.ID];
                    else
                    {
                        dt = DTAnswers;
                        dt.Rows.Add();
                        _sender.Session[gvlist.ID] = dt;
                    }
                }
                else
                {
                    _sender.Session[gvlist.ID] = null;
                    dt = DTAnswers;
                    dt.Rows.Add();
                    _sender.Session[gvlist.ID] = dt;
                }
                gvlist.DataSource = dt;
                gvlist.DataBind();
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            output_panel.Controls.Add(gvlist);
            var b = new Button();
            b.ID = "btnAddNewRow" + QUE.Question_ID + "_" + QUE.Question_Index;
            b.Text = "ذخیره سطر";
            b.Click += btnAddOldRow_Click;
            b.CausesValidation = false;
            output_panel.Controls.Add(b);
        }

        void FllAnsrRdoBtnLstTxt(Question QUE, ORM.Form_Instance instnc)
        {
            var rblist = new RadioButtonList();
            rblist.ID = "RBL" + QUE.Question_ID + "_" + QUE.Question_Index;
            switch (QUE.Template_Type)
            {
                case ORM_Types.Template_Type.State:
                    rblist.DataSource = State.get_states();
                    rblist.DataTextField = "locname";
                    rblist.DataValueField = "locid";
                    break;
                case ORM_Types.Template_Type.Branch:
                    rblist.DataSource = Branch.get_Branchs();
                    rblist.DataTextField = "title";
                    rblist.DataTextField = "Bid";
                    break;
                case ORM_Types.Template_Type.Department:
                    rblist.DataSource = Department.get_deps();
                    rblist.DataTextField = "depname";
                    rblist.DataValueField = "depid";
                    break;
                case ORM_Types.Template_Type.Manual:
                    rblist.DataSource = ORM.List.get_dt_items(QUE.Question_ID);
                    rblist.DataTextField = "Item_Text";
                    rblist.DataValueField = "Item_ID";
                    break;
                default:
                    break;
            }
            var tmpSlctd = instnc.ReturnLString(QUE.Question_ID, QUE.Template_Type);
            rblist.DataBind();
            if (tmpSlctd != null && tmpSlctd.Count > 0)
                rblist.Items.FindByValue(tmpSlctd[0].ToString()).Selected = true;
            var cfv = new CustomValidator()
            {
                ID = "cfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                ControlToValidate = rblist.ID,
                Display = ValidatorDisplay.Dynamic,
                ErrorMessage = "این سئوال باید متناسب با شرط پاسخ داده شود"
            };

            output_panel.Controls.Add(cfv);
            output_panel.Controls.Add(rblist);

            var txt = new TextBox()
            {
                ID = "TB" + QUE.Question_ID + "_" + QUE.Question_Index,
                Width = 400,
                TextMode = TextBoxMode.MultiLine,
                Wrap = true
            };

            var ft = new FilteredTextBoxExtender()
            {
                ID = "ft" + QUE.Question_ID + "_" + QUE.Question_Index,
                FilterInterval = 250,
                FilterMode = FilterModes.InvalidChars,
                InvalidChars = "'`;\\\"",
                FilterType = FilterTypes.Custom,
                TargetControlID = txt.ID
            };

            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            txt.Text = instnc.ReturnString(QUE.Question_ID, QUE.Question_Type);
            output_panel.Controls.Add(txt);
            output_panel.Controls.Add(ft);
            if (!QUE.Question_Optional)
            {
                var rfv = new RequiredFieldValidator()
                {
                    ID = "rfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                    ControlToValidate = txt.ID,
                    Display = ValidatorDisplay.Dynamic,
                    ErrorMessage = "لطفا اين گزينه را خالي نگذاريد"
                };
                output_panel.Controls.Add(rfv);
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
        }

        void FllAnsrDrpDnLstTxt(Question QUE, ORM.Form_Instance instnc)
        {
            var drlist = new DropDownList();
            drlist.ID = "DDL" + QUE.Question_ID + "_" + QUE.Question_Index;
            switch (QUE.Template_Type)
            {
                case ORM_Types.Template_Type.State:
                    drlist.DataSource = State.get_states();
                    drlist.DataTextField = "locname";
                    drlist.DataValueField = "locid";
                    break;
                case ORM_Types.Template_Type.Branch:
                    drlist.DataSource = Branch.get_Branchs();
                    drlist.DataTextField = "title";
                    drlist.DataValueField = "Bid";
                    break;
                case ORM_Types.Template_Type.Department:
                    drlist.DataSource = Department.get_deps();
                    drlist.DataTextField = "depname";
                    drlist.DataValueField = "depid";
                    break;
                case ORM_Types.Template_Type.Manual:
                    drlist.DataSource = ORM.List.get_dt_items(QUE.Question_ID);
                    drlist.DataTextField = "Item_Text";
                    drlist.DataValueField = "Item_ID";
                    break;
                default:
                    break;
            }
            var tmpSlctd = instnc.ReturnLString(QUE.Question_ID, QUE.Template_Type);
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            output_panel.Controls.Add(drlist);
            drlist.DataBind();
            if (tmpSlctd != null && tmpSlctd.Count > 0)
                drlist.Items.FindByValue(tmpSlctd[0].ToString()).Selected = true;

            var txt = new TextBox()
            {
                ID = "TB" + QUE.Question_ID + "_" + QUE.Question_Index,
                Width = 400,
                TextMode = TextBoxMode.MultiLine,
                Wrap = true
            };
            var ft = new FilteredTextBoxExtender()
            {
                ID = "ft" + QUE.Question_ID + "_" + QUE.Question_Index,
                FilterInterval = 250,
                FilterMode = FilterModes.InvalidChars,
                InvalidChars = "'`;\\\"",
                FilterType = FilterTypes.Custom,
                TargetControlID = txt.ID
            };

            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            txt.Text = instnc.ReturnString(QUE.Question_ID, QUE.Question_Type);
            output_panel.Controls.Add(txt);
            output_panel.Controls.Add(ft);
            if (!QUE.Question_Optional)
            {
                var rfv = new RequiredFieldValidator()
                {
                    ID = "rfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                    ControlToValidate = txt.ID,
                    Display = ValidatorDisplay.Dynamic,
                    ErrorMessage = "لطفا اين گزينه را خالي نگذاريد"
                };
                output_panel.Controls.Add(rfv);
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
        }

        void FllAnsrChckBxLstTxt(Question QUE, ORM.Form_Instance frmInstnc)
        {
            var chklist = new CheckBoxList()
            {
                ID = "CBL" + QUE.Question_ID + "_" + QUE.Question_Index,
            };
            switch (QUE.Template_Type)
            {
                case ORM_Types.Template_Type.State:
                    chklist.DataSource = State.get_states();
                    chklist.DataTextField = "locname";
                    chklist.DataValueField = "locid";
                    break;
                case ORM_Types.Template_Type.Branch:
                    chklist.DataSource = Branch.get_Branchs();
                    chklist.DataTextField = "title";
                    chklist.DataValueField = "Bid";
                    break;
                case ORM_Types.Template_Type.Department:
                    chklist.DataSource = Department.get_deps();
                    chklist.DataTextField = "depname";
                    chklist.DataTextField = "depid";//Err.prob
                    break;
                case ORM_Types.Template_Type.Manual:
                    chklist.DataSource = ORM.List.get_dt_items(QUE.Question_ID);
                    chklist.DataTextField = "Item_Text";
                    chklist.DataValueField = "Item_ID";
                    break;
                default:
                    break;
            }
            output_panel.Controls.Add(chklist);
            chklist.DataBind();
            var tmpSlctd = frmInstnc.ReturnLString(QUE.Question_ID, QUE.Template_Type);

            if (tmpSlctd != null && tmpSlctd.Count > 0)
                foreach (Object obj1 in tmpSlctd)
                {
                    String value = (String)obj1;
                    if (chklist.Items.FindByValue(value) == null) break;
                    chklist.Items.FindByValue(value).Selected = true;
                }

            var txt = new TextBox()
            {
                ID = "TB" + QUE.Question_ID + "_" + QUE.Question_Index,
                Width = 400,
                TextMode = TextBoxMode.MultiLine,
                Wrap = true
            };
            var ft = new FilteredTextBoxExtender()
            {
                ID = "ft" + QUE.Question_ID + "_" + QUE.Question_Index,
                FilterInterval = 250,
                FilterMode = FilterModes.InvalidChars,
                InvalidChars = "'`;\\\"",
                FilterType = FilterTypes.Custom,
                TargetControlID = txt.ID
            };
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
            txt.Text = frmInstnc.ReturnString(QUE.Question_ID, QUE.Question_Type);
            output_panel.Controls.Add(txt);
            output_panel.Controls.Add(ft);
            if (!QUE.Question_Optional)
            {
                var rfv = new RequiredFieldValidator()
                {
                    ID = "rfv" + QUE.Question_ID + "_" + QUE.Question_Index,
                    ControlToValidate = txt.ID,
                    Display = ValidatorDisplay.Dynamic,
                    ErrorMessage = "لطفا اين گزينه را خالي نگذاريد"
                };
                output_panel.Controls.Add(rfv);
            }
            output_panel.Controls.Add(GetLiteral("<br/><br/>"));
        }
        #endregion
        Literal GetLiteral(String litstr)
        {
            Literal l = new Literal();
            l.Text = litstr;
            return l;
        }
        /// <summary>
        /// add table tag to outputPanel
        /// </summary>
        void AddFirstSection()
        {
            output_panel.Controls.Add(GetLiteral("<Table width='100%' style='Border:solid 1px #0676c4;background-color:#d0e4f2;'><tr><td>"));
        }
        /// <summary>
        /// end table tag
        /// </summary>
        void AddLastSection()
        {
            output_panel.Controls.Add(GetLiteral("</td></tr></table><br/>"));
        }
        public ORM.Forms Local_Form
        {
            get { return _local_form; }
            set { _local_form = value; }
        }
        private Form_Question_Instance SetValue(ORM_Types.Template_Type ttype, Form_Question_Instance newanswer, String value)
        {
            if (ttype == ORM_Types.Template_Type.Branch)
                newanswer.Branch_Choice.Add(value);
            if (ttype == ORM_Types.Template_Type.State)
                newanswer.State_Choice.Add(value);
            if (ttype == ORM_Types.Template_Type.Department)
                newanswer.Dep_Choice.Add(value);
            if (ttype == ORM_Types.Template_Type.Manual)
                newanswer.List_Choice.Add(value);
            return newanswer;
        }
    }
}