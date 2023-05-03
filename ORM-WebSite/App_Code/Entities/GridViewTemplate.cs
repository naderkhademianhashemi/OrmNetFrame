using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PersianDateControls;
using AjaxControlToolkit;



/// <summary>
/// Summary description for HTML_Forms
/// </summary>
/// 
namespace ORM
{
    public partial class HTML_Forms
    {
        public class GridViewTemplate : ITemplate
        {
            //A variable to hold the type of ListItemType.
            ListItemType _templateType;

            //A variable to hold the column name.
            string _columnName;
            long _colID;
            Boolean _colOption, _colFilled;
            Int32 _colType;
            Nullable<Int32> _colTable;
            //Constructor where we define the template type and column name.
            public GridViewTemplate(ListItemType type, long colID, string colname, Boolean coloption, Int32 coltype, Boolean colfilled, Nullable<Int32> coltable)
            {
                //Stores the template type.
                _templateType = type;

                //Stores the column name.
                _columnName = colname;
                _colID = colID;
                _colOption = coloption;
                _colType = coltype;
                _colFilled = colfilled;
                _colTable = coltable;

            }
            void ITemplate.InstantiateIn(System.Web.UI.Control container)
            {
                switch (_templateType)
                {
                    case ListItemType.Header:
                        //Creates a new label control and add it to the container.
                        Label lbl = new Label();            //Allocates the new label object.
                        lbl.Text = _columnName; //Assigns the name of the column in the lable.
                        if (_colTable == 1)
                        {
                            lbl.Width = 20;
                            lbl.Enabled = false;
                        }
                        if ((_colOption) && ((_colTable > 1) || (!_colFilled)))
                            lbl.Text += " *";
                        container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                        break;

                    case ListItemType.Item:
                        //Creates a new text box control and add it to the container.
                        DropDownList cb1;
                        TextBox tb1;
                        if (_colFilled)
                        {
                            if (_colTable > 1)
                            {
                                cb1 = new DropDownList();
                                SQL_Connector connector = new SQL_Connector();
                                String SQL_query = "SELECT [Name] FROM [FG_DB].[dbo].[Tables] WHERE ID = " + _colTable.ToString();
                                String tablename = connector.execute_scalar(SQL_query);
                                SQL_query = "SELECT * FROM [FG_DB].[dbo].[" + tablename + "]";
                                DataTable dt = connector.Select(SQL_query);
                                cb1.DataSource = dt;
                                cb1.DataValueField = dt.Columns[1].ColumnName;
                                cb1.DataTextField = dt.Columns[1].ColumnName;
                                cb1.EnableViewState = true;
                                cb1.DataBinding += new EventHandler(cb1_DataBinding);
                                container.Controls.Add(cb1);
                            }
                            else
                            {
                                tb1 = new TextBox();
                                tb1.DataBinding += new EventHandler(tbradif_DataBinding);   //Attaches the data binding event.
                                tb1.ID = "rad" + _colID.ToString();
                                container.Controls.Add(tb1);
                                if (HttpContext.Current.Session["rad" + _colID.ToString()] == null || HttpContext.Current.Session["rad" + _colID.ToString()] == "")
                                    HttpContext.Current.Session["rad" + _colID.ToString()] = 1;
                                tb1.Text = HttpContext.Current.Session["rad" + _colID.ToString()].ToString();
                            }
                        }
                        else
                        {
                            switch (_colType)
                            {
                                case 1:
                                    {
                                        tb1 = new TextBox();                            //Allocates the new text box object.
                                        tb1.DataBinding += new EventHandler(tb1_DataBinding);   //Attaches the data binding event.
                                        tb1.ID = "txt" + _colID;
                                        container.Controls.Add(tb1);
                                        FilteredTextBoxExtender ft1 = new FilteredTextBoxExtender();
                                        ft1.ID = "ft" + _colID + "_";
                                        ft1.FilterInterval = 250;
                                        ft1.FilterMode = FilterModes.InvalidChars;
                                        ft1.InvalidChars = "'`;\\\"";
                                        ft1.FilterType = FilterTypes.Custom;
                                        ft1.TargetControlID = tb1.ID;
                                        container.Controls.Add(ft1);
                                        if (_colOption)
                                        {
                                            RequiredFieldValidator rfv = new RequiredFieldValidator();
                                            rfv.ID = "rfv" + _colID;
                                            rfv.ControlToValidate = tb1.ID;
                                            rfv.EnableClientScript = false;
                                            rfv.Display = ValidatorDisplay.Static;
                                            rfv.ErrorMessage = "لطفا متن را خالي نگذاريد";
                                            container.Controls.Add(rfv);
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        tb1 = new TextBox();                            //Allocates the new text box object.
                                        tb1.DataBinding += new EventHandler(tb1_DataBinding);   //Attaches the data binding event.
                                        tb1.ID = "txt" + _colID;
                                        container.Controls.Add(tb1);
                                        FilteredTextBoxExtender ft1 = new FilteredTextBoxExtender();
                                        ft1.ID = "ft" + _colID.ToString() + "_";
                                        ft1.FilterInterval = 250;
                                        ft1.FilterMode = FilterModes.ValidChars;
                                        ft1.ValidChars = "0123456789";
                                        ft1.FilterType = FilterTypes.Custom;
                                        ft1.TargetControlID = tb1.ID;
                                        container.Controls.Add(ft1);
                                        if (_colOption)
                                        {
                                            RequiredFieldValidator rfv = new RequiredFieldValidator();
                                            rfv.ID = "rfv" + _colID;
                                            rfv.EnableClientScript = false;
                                            rfv.ControlToValidate = tb1.ID;
                                            rfv.Display = ValidatorDisplay.Static;
                                            rfv.ErrorMessage = "لطفا عدد را خالي نگذاريد";
                                            container.Controls.Add(rfv);
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        tb1 = new TextBox();                            //Allocates the new text box object.
                                        tb1.DataBinding += new EventHandler(tb1_DataBinding);   //Attaches the data binding event.
                                        tb1.ID = "txt" + _colID;
                                        tb1.Attributes["onKeyUp"] = "javascript:moneyCommaSep(this);";
                                        container.Controls.Add(tb1);
                                        FilteredTextBoxExtender ft1 = new FilteredTextBoxExtender();
                                        ft1.ID = "ft" + _colID.ToString() + "_";
                                        ft1.FilterInterval = 250;
                                        ft1.FilterMode = FilterModes.ValidChars;
                                        ft1.ValidChars = "0123456789-+.,";
                                        ft1.FilterType = FilterTypes.Custom;
                                        ft1.TargetControlID = tb1.ID;
                                        container.Controls.Add(ft1);
                                        if (_colOption)
                                        {
                                            RequiredFieldValidator rfv = new RequiredFieldValidator();
                                            rfv.ID = "rfv" + _colID;
                                            rfv.ControlToValidate = tb1.ID;
                                            rfv.Display = ValidatorDisplay.Static;
                                            rfv.EnableClientScript = false;
                                            rfv.ErrorMessage = "لطفا عدد مالی را خالي نگذاريد";
                                            container.Controls.Add(rfv);
                                        }
                                        break;
                                    }
                                case 5:
                                    {
                                        PersianDateTextBox pdt = new PersianDateTextBox();
                                        pdt.IconUrl = @"~/Images/cal.gif";
                                        pdt.ID = "PDT" + _colID + "_";
                                        pdt.DefaultDate = DateTime.Now.ToString("d");
                                        pdt.DataBinding += new EventHandler(pdt_DataBinding);
                                        container.Controls.Add(pdt);
                                        if (_colOption)
                                        {
                                            RequiredFieldValidator rfv = new RequiredFieldValidator();
                                            rfv.ID = "rfv" + _colID;
                                            rfv.ControlToValidate = pdt.ID;
                                            rfv.EnableClientScript = false;
                                            rfv.Display = ValidatorDisplay.Static;
                                            rfv.ErrorMessage = "لطفا تاریخ را خالي نگذاريد";
                                            container.Controls.Add(rfv);
                                        }
                                        break;
                                    }
                            }

                        }
                        break;

                    case ListItemType.EditItem:
                        //As, I am not using any EditItem, I didnot added any code here.
                        break;

                    case ListItemType.Footer:
                        CheckBox chkColumn = new CheckBox();
                        chkColumn.ID = "Chk" + _columnName;
                        container.Controls.Add(chkColumn);
                        break;
                }

            }
            /// <summary>
            /// This is the event, which will be raised when the binding happens.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void tb1_DataBinding(object sender, EventArgs e)
            {
                TextBox txtdata = (TextBox)sender;
                GridViewRow container = (GridViewRow)txtdata.NamingContainer;
                object dataValue = DataBinder.Eval(container.DataItem, _colID.ToString());
                if (dataValue != DBNull.Value)
                {
                    txtdata.Text = dataValue.ToString();
                }
            }
            void pdt_DataBinding(object sender, EventArgs e)
            {
                PersianDateTextBox datedata = (PersianDateTextBox)sender;
                GridViewRow container = (GridViewRow)datedata.NamingContainer;
                object dataValue = DataBinder.Eval(container.DataItem, _colID.ToString());
                if (dataValue != DBNull.Value)
                    datedata.Text = dataValue.ToString();
            }
            void tbradif_DataBinding(object sender, EventArgs e)
            {
                TextBox txtdata = (TextBox)sender;
                GridViewRow container = (GridViewRow)txtdata.NamingContainer;
                object dataValue = DataBinder.Eval(container.DataItem, _colID.ToString());
                if (dataValue != DBNull.Value)
                    HttpContext.Current.Session["rad" + _colID.ToString()] = (Convert.ToInt32(dataValue) + 1).ToString();
            }
            void cb1_DataBinding(object sender, EventArgs e)
            {

                DropDownList drldata = (DropDownList)sender;
                GridViewRow container = (GridViewRow)drldata.NamingContainer;
                object dataValue = DataBinder.Eval(container.DataItem, _colID.ToString());
                if (dataValue != DBNull.Value)
                    drldata.SelectedValue = dataValue.ToString();
            }
        }
    }
}