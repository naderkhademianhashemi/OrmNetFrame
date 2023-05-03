<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FillFormerShowForm.aspx.cs" Inherits="FillFormerShowForm" Title="پرسشنامه" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="/jquery.min.js" type="text/javascript"></script>
    <script src="/JavaScript/FllFrmrShwFrmFnctn.js"></script>
    <asp:Label ID="Label4" runat="server"  Text="مشاهده پاسخ های داده شده به پرسشنامه"></asp:Label>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
           نام جدول :
           
                <asp:DropDownList ID="tblname" runat="server" AutoPostBack="True" 
                    OnDataBound="tblname_DataBound"
                    OnSelectedIndexChanged="tblname_SelectedIndexChanged" >
                </asp:DropDownList>
                <asp:CheckBox ID="chkSearch" runat="server" 
                    AutoPostBack="True"
                    Text="جستجو در سوالها" 
                    OnCheckedChanged="Dep_selectall_CheckedChanged" />
        <br />
            نام ستون :
                <asp:DropDownList ID="WhereColname" runat="server"  OnDataBound="WhereColname_DataBound"
                    OnSelectedIndexChanged="Wherecolname_SelectedIndexChanged"
                    AutoPostBack="True" Enabled="False">
                </asp:DropDownList>
        <br />
                <asp:MultiView ID="WhereView" runat="server" ActiveViewIndex="0"
                    Visible="False">
                    <asp:View ID="NVarChar" runat="server">
                                شرط مقايسه :
                                    <asp:DropDownList ID="WherecondopNvar" runat="server" >
                                        <asp:ListItem Value="=">برابر با</asp:ListItem>
                                        <asp:ListItem Value="&lt;&gt;">نا مساوی </asp:ListItem>
                                        <asp:ListItem Value="Like">شباهت </asp:ListItem>
                                        <asp:ListItem Value="NOT Like">عدم شباهت</asp:ListItem>
                                    </asp:DropDownList>
                                مقدار براي مقايسه :
                                    <asp:TextBox ID="WherevalueNvar" runat="server"
                                        CssClass="textboxlrt" ValidationGroup="addcond"
                                        ></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="WherevalueNvar"
                                        Display="Dynamic" ErrorMessage="این فیلد را خالی نگذارید." ValidationGroup="addcond"></asp:RequiredFieldValidator>
                    </asp:View>
                    <asp:View ID="Num" runat="server">
                                مقدار براي مقايسه :
                                    <asp:DropDownList ID="WherecondopNum" runat="server" >
                                        <asp:ListItem Value="=">برابر با</asp:ListItem>
                                        <asp:ListItem Value="&lt;&gt;">نا مساوی با</asp:ListItem>
                                        <asp:ListItem Value="&gt;">بزرگتر از</asp:ListItem>
                                        <asp:ListItem Value="&lt;">کوچکتر از</asp:ListItem>
                                        <asp:ListItem Value="&gt;=">بزرگتر مساوی با</asp:ListItem>
                                        <asp:ListItem Value="&lt;=">کوچکتر مساوی با </asp:ListItem>
                                    </asp:DropDownList>
                        <br />
                                شرط مقايسه :
                                    <asp:TextBox ID="WherevalueNum" runat="server" CausesValidation="True" CssClass="textboxlrt"
                                        ValidationGroup="addcond" ></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="WherevalueNum"
                                        Display="Dynamic" ErrorMessage="این فیلد را خالی نگذارید." ValidationGroup="addcond"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator1" runat="server" ControlToValidate="WherevalueNum"
                                            Display="Dynamic" ErrorMessage="لطفاً عدد وارد کنید." ValidationExpression="^-?(\d+(.\d*)*|.\d+)$"
                                            ValidationGroup="addcond"></asp:RegularExpressionValidator>
                            <br />
                    </asp:View>
                    <asp:View ID="Date" runat="server">
                                شرط مقايسه :
                                <br />
                                    <asp:DropDownList ID="WherecondopDate" runat="server" >
                                        <asp:ListItem Value="=">برابر با</asp:ListItem>
                                        <asp:ListItem Value="&lt;&gt;">نا مساوی با</asp:ListItem>
                                        <asp:ListItem Value="&gt;">بزرگتر از</asp:ListItem>
                                        <asp:ListItem Value="&lt;">کوچکتر از</asp:ListItem>
                                        <asp:ListItem Value="&gt;=">بزرگتر مساوی با </asp:ListItem>
                                        <asp:ListItem Value="&lt;=">کوچکتر مساوی با</asp:ListItem>
                                    </asp:DropDownList>
                                <br />
                                مقدار براي مقايسه :
                                <br />
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
                                        Display="Dynamic" ErrorMessage="لطفاً تاریخ شمسی وارد کنید" ValidationExpression="1(3|4)\d{2}/\d{2}/\d{2}"
                                        ValidationGroup="addcond"></asp:RegularExpressionValidator>
                               <br />
                    </asp:View>
                    <asp:View ID="List" runat="server">
                        
                                شرط مقايسه :
                                <br />
                                    <asp:DropDownList ID="WherecondopList" runat="server" >
                                        <asp:ListItem Value="=">برابر با </asp:ListItem>
                                        <asp:ListItem Value="&lt;&gt;">نامساوی با</asp:ListItem>
                                    </asp:DropDownList>
                               
                                <br />مقدار براي مقايسه :
                                
                                    <asp:DropDownList ID="WherevalueList" runat="server" ValidationGroup="1"
                                        >
                                    </asp:DropDownList>
                               <br />
                    </asp:View>
                    <asp:View ID="locView1" runat="server">
                        شرط مقايسه :
                                
                                    <asp:DropDownList ID="WherecondopLoc" runat="server" >
                                        <asp:ListItem Value="=">برابر با</asp:ListItem>
                                        <asp:ListItem Value="&lt;&gt;">نا مساوی با</asp:ListItem>
                                    </asp:DropDownList>
                              
                                <br />مقدار براي مقايسه :
                                
                                    <asp:DropDownList ID="WherevalueLoc" runat="server" DataSourceID="SqlDataSource2"
                                        DataTextField="LocName" DataValueField="locid" ValidationGroup="1" >
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
                                        SelectCommand="SELECT [locid], [LocName] FROM [State] where [locid]&lt;&gt;0"></asp:SqlDataSource>
                               <br />
                    </asp:View>
                    <asp:View ID="branchview" runat="server">
                        شرط مقايسه :
                              
                                    <asp:DropDownList ID="WherecondopBranch" runat="server" >
                                        <asp:ListItem Value="=">برابر با</asp:ListItem>
                                        <asp:ListItem Value="&lt;&gt;">نا مساوی با</asp:ListItem>
                                    </asp:DropDownList>
                                <br />مقدار براي مقايسه :
                               
                                    <asp:DropDownList ID="WherevalueBranch" runat="server" DataSourceID="SqlDataSource6"
                                        DataTextField="BranchName" DataValueField="BID" ValidationGroup="1" >
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource6" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
                                        SelectCommand="SELECT convert(nvarchar,[Branch]) +' - ' +  [BranchName] as BranchName , [BID] FROM [Branch] where BID&lt;&gt;0"></asp:SqlDataSource>
                                <br />
                    </asp:View>
                </asp:MultiView>
            
                <asp:Button ID="btnSearchAnswers"
                    runat="server"
                    Text="جستجو"
                    OnClick="btnSearchAnswers_Click" />

                <asp:Button ID="btnXML_Save" Text="XML" runat="server"
                    OnClick="btnXML_Save_Click"
                    AccessKey="X"
                    ToolTip="Alt + X"
                    OnClientClick="return check_Grid()" />

            

     <h4>اخذ خروجی پاسخ های اخیر براساس تعداد سطر</h4>
    <hr />
    
    

       
            <br />تعداد سطر خروجی 
                    
           
                <asp:TextBox ID="txtCount" runat="server" MaxLength="3" Text="10"></asp:TextBox>
                <asp:Button ID="btnXMLTop" Text="XML" runat="server"
                    OnClick="btnXMLTop_Click" />
            <br />

            به منظور اخذ خروجی با تعداد سطر بالا می بایستی منتظر بمانید  
           <br />
           از تاریخ 
            <br />
                <input type="text" id="txtFromDate" class="persianDate" runat="server" />
            <br />
            تا تاریخ
            <br />
                <input type="text" id="txtToDate" class="persianDate" runat="server" />
                <asp:Button ID="btnSearchBetweenDates" Text="جستجو" runat="server"
                     OnClick="btnSearchBetweenDates_Click" />
                <asp:Button ID="btnXmlBetweenDates" Text="XML" runat="server"
                    OnClick="btnXmlBetweenDates_Click" />
           <br />
    <hr />
    <asp:Label ID="lblresult" runat="server"   
        ></asp:Label>
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                    <asp:Panel ID="temp_Panel" runat="server" Visible="False">
                    </asp:Panel>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <span>GridView_Form_Instance</span>
                            <asp:GridView ID="GridView_Form_Instance" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" 
                                
                                EmptyDataText="جوابي با محدوديت ها مورد نطر يافت نشد "
                                OnPageIndexChanging="GridView_Form_Instance_PageIndexChanging"
                                OnRowCommand="GridView_Form_Instance_RowCommand"
                                OnRowDeleting="GridView_Form_Instance_RowDeleting">
                                
                                
                                <%-- col number is dependent to binder and filteredBinder method --%>
                                <%-- take care when you add or remove col --%>
                                <Columns>
                                    <%-- 0 --%>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll_Form_Instance" runat="server"
                                                onclick="checkAll_Form_Instance(this);" Text="انتخاب" />

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_Form_Instance" runat="server" CssClass="firstColumnChk" />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <%-- 1 --%>
                                    <asp:TemplateField HeaderText="شناسه" SortExpression="Instance_ID"
                                         >
                                        <ItemTemplate>
                                            <asp:Label ID="lblInstance_ID" Text='<%#Bind("Instance_ID") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- 2 --%>
                                    <asp:TemplateField HeaderText="نام كاربري" SortExpression="User_Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUser_name" runat="server" Text='<%# Bind("User_name") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <%-- 3 --%>
                                    <asp:TemplateField HeaderText="تاريخ ثبت" SortExpression="filldate">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="Label11" runat="server" Text='<%# Bind("filldate") %>'></asp:Label>--%>
                                            <asp:Label ID="lblFillDate" runat="server"
                                                Text='<%# OrmUtility.ConvertToShamsi(Eval("filldate").ToString()) %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <%-- 4 --%>
                                    <asp:TemplateField HeaderText="گزارش" SortExpression="Reported">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server"
                                                onclick="checkAll(this);" Text="گزارش" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Chk01" runat="server" ToolTip='<%# Eval("Instance_ID")%>'
                                                Checked='<%#Eval("Reported").ToString()=="True"?true:false %>'></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- 5  --%>
                                    <asp:TemplateField HeaderText="نمايش جواب">
                                        <ItemTemplate>
                                            <table >
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="Button_Show_Answer" runat="server"
                                                            CausesValidation="False" CommandArgument='<%# Eval("Instance_ID") %>'
                                                            CommandName="Show_Instance"  Text="نمايش جواب" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>

                                    </asp:TemplateField>



                                    <%-- 6   شماره ردیف وابسته به متد بایندر--%>
                                    <asp:TemplateField HeaderText="حذف">
                                        <ItemTemplate>
                                            <asp:Button ID="ImageButton_Delete" runat="server"
                                                CommandArgument='<%# Eval("Instance_ID") %>' CommandName="Delete"
                                                Text="delete"
                                                OnClientClick="return confirm('sure')" />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <%-- 7 شماره ردیف وابسته به متد بایندر--%>
                                    <asp:TemplateField HeaderText="ویرایش">
                                        <ItemTemplate>

                                            <asp:Button ID="ImageButton_Edit" runat="server" CommandName="Edit"
                                                CommandArgument='<%# Eval("Instance_ID") %>' Text="edit"
                                                 />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>

                                
                                <SelectedRowStyle BackColor="#738A9C"   />
                                
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                            </asp:GridView>

                            <%-- Visible="false" --%>
                            <asp:Button ID="Btn_Reported" runat="server" 
                                 OnClick="Btn_Reported_Click" Text="ثبت تغییرات"
                                Visible="False" />
                                            <br />
                                        <asp:Label ID="lblSearch" runat="server" Text="نام کاربر:" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtSearch" runat="server" Text="" Visible="false"></asp:TextBox>
                                    <br />
                                        <asp:Button  ID="btnSearch" runat="server"
                                              Text="جستجو" Visible="false" 
                                            OnClick="btnSearch_Click" />
                            <%-- Visible="false" --%>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <span>panelHtmlForms</span>
                            <asp:Panel ID="panelHtmlForms" runat="server">
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager0" runat="server"
                        CalendarDayWidth="50" FooterCSS="PickerFooterCSS"
                        ForbidenCSS="PickerForbidenCSS" ForbidenWeekDays="6" FrameCSS="PickerCSS"
                        HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
                    </pdc:PersianDateScriptManager>

        <br />
    </asp:Panel>
</asp:Content>
