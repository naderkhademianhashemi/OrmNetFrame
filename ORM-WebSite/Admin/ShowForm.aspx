<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ShowForm.aspx.cs" Inherits="ShowForm" Title="پرسشنامه" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="/JavaScript/ShowFormFnctn.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="Label4" runat="server" ForeColor="#3366FF"
        Text="مشاهده پاسخ های داده شده به پرسشنامه"></asp:Label>
    <br />
    <hr />

    انتخاب فرم :
                <asp:DropDownList ID="tblname" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource3"
                    DataTextField="Form_Name" DataValueField="Form_ID" OnDataBound="tblname_DataBound"
                    OnSelectedIndexChanged="tblname_SelectedIndexChanged" Width="277px">
                </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server"
        ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
        SelectCommand="SELECT [Form_ID], [Form_Name] FROM [Forms]"></asp:SqlDataSource>
    <asp:CheckBox ID="chkSearch" runat="server"
        AutoPostBack="True"
        Text="جستجو در سوالها"
        OnCheckedChanged="Dep_selectall_CheckedChanged" />
    انتخاب سوال : 
                <asp:DropDownList ID="WhereColname" runat="server" Width="276px"
                    OnDataBound="WhereColname_DataBound"
                    OnSelectedIndexChanged="Wherecolname_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
    <br />
    <br />
    <asp:MultiView ID="WhereView" runat="server" ActiveViewIndex="0">
        <asp:View ID="NVarChar" runat="server">
            <br />
            شرط مقايسه :
                                    <asp:DropDownList ID="WherecondopNvar" runat="server">
                                        <asp:ListItem Value="=">برابر با</asp:ListItem>
                                        <asp:ListItem Value="&lt;&gt;">نا مساوی </asp:ListItem>
                                        <asp:ListItem Value="Like">شباهت </asp:ListItem>
                                        <asp:ListItem Value="NOT Like">عدم شباهت</asp:ListItem>
                                    </asp:DropDownList>
            <br />
            مقدار براي مقايسه :
                                    <asp:TextBox ID="WherevalueNvar" runat="server"
                                        CssClass="textboxlrt" ValidationGroup="addcond"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                ControlToValidate="WherevalueNvar"
                Display="Dynamic" ErrorMessage="این فیلد را خالی نگذارید."
                ValidationGroup="addcond"></asp:RequiredFieldValidator>
        </asp:View>
        <asp:View ID="Num" runat="server">
            مقدار براي مقايسه :
                                    <asp:DropDownList ID="WherecondopNum" runat="server">
                                        <asp:ListItem Value="=">برابر با</asp:ListItem>
                                        <asp:ListItem Value="&lt;&gt;">نا مساوی با</asp:ListItem>
                                        <asp:ListItem Value="&gt;">بزرگتر از</asp:ListItem>
                                        <asp:ListItem Value="&lt;">کوچکتر از</asp:ListItem>
                                        <asp:ListItem Value="&gt;=">بزرگتر مساوی با</asp:ListItem>
                                        <asp:ListItem Value="&lt;=">کوچکتر مساوی با </asp:ListItem>
                                    </asp:DropDownList>
            <br />
            شرط مقايسه :
                                    <asp:TextBox ID="WherevalueNum" runat="server"
                                        CausesValidation="True" CssClass="textboxlrt"
                                        ValidationGroup="addcond"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                ControlToValidate="WherevalueNum"
                Display="Dynamic" ErrorMessage="این فیلد را خالی نگذارید."
                ValidationGroup="addcond"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="WherevalueNum"
                    Display="Dynamic" ErrorMessage="لطفاً عدد وارد کنید."
                    ValidationExpression="^-?(\d+(.\d*)*|.\d+)$"
                    ValidationGroup="addcond"></asp:RegularExpressionValidator>
        </asp:View>
        <asp:View ID="Date" runat="server">
            <br />
            شرط مقايسه :
                                
                                    <asp:DropDownList ID="WherecondopDate" runat="server">
                                        <asp:ListItem Value="=">برابر با</asp:ListItem>
                                        <asp:ListItem Value="&lt;&gt;">نا مساوی با</asp:ListItem>
                                        <asp:ListItem Value="&gt;">بزرگتر از</asp:ListItem>
                                        <asp:ListItem Value="&lt;">کوچکتر از</asp:ListItem>
                                        <asp:ListItem Value="&gt;=">بزرگتر مساوی با </asp:ListItem>
                                        <asp:ListItem Value="&lt;=">کوچکتر مساوی با</asp:ListItem>
                                    </asp:DropDownList>
            <br />
            مقدار براي مقايسه :
                                    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server"
                                        CalendarDayWidth="50" FooterCSS="PickerFooterCSS"
                                        ForbidenCSS="PickerForbidenCSS" ForbidenWeekDays="6" FrameCSS="PickerCSS"
                                        HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                                        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
                                    </pdc:PersianDateScriptManager>
            <pdc:PersianDateTextBox ID="WherevalueDate" runat="server" CausesValidation="True"
                CssClass="textboxlrt" DefaultDate="1363/10/22" IconUrl="~/images/cal.gif"
                SetDefaultDateOnEvent="OnClick" ValidationGroup="addcond"
                ShowPickerOnEvent="OnClick" ShowPickerOnTop="True" />
            <asp:RequiredFieldValidator
                ID="RequiredFieldValidator3" runat="server" ControlToValidate="WherevalueDate"
                Display="Dynamic" ErrorMessage="این فیلد را خالی نگذارید." ValidationGroup="addcond">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                ID="RegularExpressionValidator2" runat="server" ControlToValidate="WherevalueDate"
                Display="Dynamic" ErrorMessage="لطفاً تاریخ شمسی وارد کنید"
                ValidationExpression="1(3|4)\d{2}/\d{2}/\d{2}"
                ValidationGroup="addcond"></asp:RegularExpressionValidator>
        </asp:View>
        <asp:View ID="List" runat="server">
            <br />
            شرط مقايسه :
                                    <asp:DropDownList ID="WherecondopList" runat="server">
                                        <asp:ListItem Value="=">برابر با </asp:ListItem>
                                        <asp:ListItem Value="&lt;&gt;">نامساوی با</asp:ListItem>
                                    </asp:DropDownList>
            <br />
            مقدار براي مقايسه :
                                    <asp:DropDownList ID="WherevalueList" runat="server" ValidationGroup="1">
                                    </asp:DropDownList>
        </asp:View>
        <asp:View ID="locView1" runat="server">
            <br />
            شرط مقايسه :
                                    <asp:DropDownList ID="WherecondopLoc" runat="server">
                                        <asp:ListItem Value="=">برابر با</asp:ListItem>
                                        <asp:ListItem Value="&lt;&gt;">نا مساوی با</asp:ListItem>
                                    </asp:DropDownList>
            <br />
            مقدار براي مقايسه :
                                    <asp:DropDownList ID="WherevalueLoc" runat="server"
                                        DataSourceID="SqlDataSource2"
                                        DataTextField="LocName" DataValueField="locid" ValidationGroup="1">
                                    </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
                SelectCommand="SELECT [locid], [LocName] FROM [State] where [locid]&lt;&gt;0"></asp:SqlDataSource>
        </asp:View>
        <asp:View ID="branchview" runat="server">
            شرط مقايسه :
                                    <asp:DropDownList ID="WherecondopBranch" runat="server">
                                        <asp:ListItem Value="=">برابر با</asp:ListItem>
                                        <asp:ListItem Value="&lt;&gt;">نا مساوی با</asp:ListItem>
                                    </asp:DropDownList>
            <br />
            مقدار براي مقايسه :
                                    <asp:DropDownList ID="WherevalueBranch" runat="server" DataSourceID="SqlDataSource6"
                                        DataTextField="BranchName" DataValueField="BID" ValidationGroup="1">
                                    </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource6" runat="server"
                ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
                SelectCommand="SELECT convert(nvarchar,[Branch]) +' - ' +  [BranchName] as BranchName , [BID] FROM [Branch] where BID&lt;&gt;0"></asp:SqlDataSource>
        </asp:View>
        <asp:View ID="DepView" runat="server">
            <br />
            شرط مقايسه :
                               
                                
                                    <asp:DropDownList ID="WherecondopDep" runat="server">
                                        <asp:ListItem Value="=">مساوی با</asp:ListItem>
                                        <asp:ListItem Value="&lt;&gt;">نا مساوی با</asp:ListItem>
                                    </asp:DropDownList>
            <br />
            مقدار براي مقايسه :
                               
                                
                                    <asp:DropDownList ID="WherevalueDep" runat="server"
                                        DataSourceID="SqlDataSource7"
                                        DataTextField="DepName" DataValueField="DepID" ValidationGroup="1">
                                    </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource7" runat="server"
                ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
                SelectCommand="SELECT [DepID], [DepName] FROM [Department] WHERE ([DepID] &lt;&gt; @DepID)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="DepID" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </asp:View>
    </asp:MultiView>
    <br />
    <asp:Button Text="جستجو" ID="btnSearchAnswers" runat="server"
        OnClick="btnSearchAnswers_Click"
        ToolTip="Alt + S"
        AccessKey="S" />
    <asp:Button ID="btnXML_Save" Text="XML" runat="server"
        OnClick="btnXML_Save_Click"
        AccessKey="X"
        ToolTip="Alt + X"
        OnClientClick="return check_Grid()" />

    <h4>اخذ خروجی پاسخ های اخیر براساس تعداد سطر</h4>
    <hr />

    تعداد سطر خروجی 
                    
                <asp:TextBox ID="txtCount" runat="server" MaxLength="3" Text="10"></asp:TextBox>
    <asp:Button ID="btnXMLTop" Text="XML" runat="server"
        OnClick="btnXMLTop_Click" />

    <br />
    به منظور اخذ خروجی با تعداد سطر بالا می بایستی منتظر بمانید  
            <br />
    از تاریخ 
                <input type="text" id="txtFromDate" class="persianDate" runat="server" />
    <br />
    تا تاریخ
                <input type="text" id="txtToDate" class="persianDate" runat="server" />
    <asp:Button ID="btnSearchBetweenDates" Text="جستجو" runat="server"
        OnClick="btnSearchBetweenDates_Click" />
    <asp:Button ID="btnXmlBetweenDates" Text="XML" runat="server"
        OnClick="btnXmlBetweenDates_Click" />

    <br />
    <hr />

    <asp:Label ID="lblresult" runat="server"
        ForeColor="#000099"></asp:Label>
    <span>Panel1</span>
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
        <asp:Panel ID="temp_Panel" runat="server">
        </asp:Panel>
        <span>GridView_Form_Instance</span>
        <asp:GridView ID="GridView_Form_Instance" runat="server" AllowPaging="True"
            AutoGenerateColumns="False"
            OnPageIndexChanging="GridView_Form_Instance_PageIndexChanging"
            OnRowCommand="GridView_Form_Instance_RowCommand"
            EmptyDataText="موردي يافت نشد" Height="344px" Width="294px">
            <Columns>
                <%-- 0 --%>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkAll_Form_Instance" runat="server" onclick="checkAll_Form_Instance(this);" Text="انتخاب" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_Form_Instance" runat="server" CssClass="firstColumnChk" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- 1 --%>
                <asp:TemplateField HeaderText="شناسه" SortExpression="Instance_ID">
                    <ItemTemplate>
                        <asp:Label ID="lblInstance_ID" Text='<%#Bind("Instance_ID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- 2 --%>
                <asp:TemplateField HeaderText="نام كاربري" SortExpression="User_Name">
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("User_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="تاريخ ثبت" SortExpression="filldate">
                    <ItemTemplate>
                        <asp:Label ID="lblFillDate" runat="server"
                            Text='<%# OrmUtility.ConvertToShamsi(Eval("filldate").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="گزارش" SortExpression="Reported">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="checkAll(this);" Text="گزارش" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="Chk01" runat="server"
                            ToolTip='<%# Eval("Instance_ID")%>'
                            Checked='<%# IsCHK(Eval("Reported").ToString()) %>'></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="نمايش جواب">
                    <ItemTemplate>
                        <asp:Button ID="Button_Show_Answer" runat="server"
                            CausesValidation="False" CommandArgument='<%# Eval("Instance_ID") %>'
                            CommandName="Show_Instance" Text="نمايش جواب" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="حذف">
                    <ItemTemplate>
                        <asp:Button ID="ImageButton_Delete" runat="server"
                            Text="Delete"
                            CommandArgument='<%# Eval("Instance_ID") %>' CommandName="Delete"
                            OnClientClick="return confirm('sure')" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ويرايش">
                    <ItemTemplate>
                        <asp:Button ID="ImageButton_Edit"
                            Text="Edit"
                            runat="server" CommandName="Edit"
                            CommandArgument='<%# Eval("Instance_ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <SelectedRowStyle CssClass="RowSelected" BackColor="#738A9C" />

            <AlternatingRowStyle BackColor="#F7F7F7" />
        </asp:GridView>
        <br />

        <asp:Button ID="Btn_Reported" runat="server"
            OnClick="Btn_Reported_Click" Text="ثبت تغییرات" />
        <asp:Label ID="lblSearch" runat="server" Text="نام کاربر:"></asp:Label>
        <asp:TextBox ID="txtSearch" runat="server" Text=""></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server"
            Text="جستجو" OnClick="btnSearch_Click" />


        <br />
        <br />
        <span>panelHtmlForms</span>
        <asp:Panel ID="panelHtmlForms" runat="server">
        </asp:Panel>
        

        <pdc:PersianDateScriptManager ID="PersianDateScriptManager0" runat="server"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS"
            ForbidenCSS="PickerForbidenCSS" ForbidenWeekDays="6" FrameCSS="PickerCSS"
            HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>

        <br />
    </asp:Panel>

    <br />

    <script src="../jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        function check_Grid() {
            var chk_Form_InstanceArray = $('input[id$=chk_Form_Instance]');
            var messageAlert = 'به منظور گرفتن خروجی ، ابتدا می بایست سطری را از جدول پاسخ ها انتخاب کنید';

            var flagChecked = false;

            for (var i = 0; i < chk_Form_InstanceArray.length; i++) {
                if (chk_Form_InstanceArray[i].checked) {
                    flagChecked = true;
                }
            }

            if (flagChecked == false) {
                alert(messageAlert);
                return false;
            }
            else {
                return true;
            }
        }

        document.querySelector("input[id*='txtCount']").addEventListener("keypress", function (evt) {
            if (evt.which < 48 || evt.which > 57) {
                evt.preventDefault();
            }
        });

        $('input[type=text]').focus(function () {
            $(this).select();
        });

    </script>
</asp:Content>
