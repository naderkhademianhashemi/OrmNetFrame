<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FormView.aspx.cs"
    Inherits="FormView" Title="پرسشنامه" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .Hide  {
            display:none;
        }
    </style>
    <asp:Label ID="Label1" runat="server">مشاهده پرسشنامه های موجود :</asp:Label>

    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/FormDefine.aspx">تعریف 
                                فرم پرسشنامه جدید</asp:HyperLink>

    <asp:Label ID="Label3" runat="server" Text="حوزه عملیاتی"></asp:Label>
    <asp:DropDownList ID="DropDownList_field" runat="server"
        DataSourceID="SqlDataSource_department" DataTextField="DepName" DataValueField="DepID"
        Enabled="False">
        <asp:ListItem Value="Select_All">همه</asp:ListItem>
    </asp:DropDownList>
    <br>
    <asp:CheckBox ID="CheckBox_field_Select_All" runat="server" AutoPostBack="True"
        Checked="True" OnCheckedChanged="CheckBox_field_Select_All_CheckedChanged"
        Text="تمامی حوزه های عملیاتی" />
    <asp:CheckBox ID="CheckBox_All_Location" runat="server" AutoPostBack="True"
        Checked="True" OnCheckedChanged="CheckBox_All_Location_CheckedChanged"
        Text="تمامی استانها" Visible="False" />


    <asp:Button ID="ImageButton_Search" runat="server" Text="search"
        OnClick="ImageButton_Search_Click" />
    <br />
    <asp:SqlDataSource ID="SqlDataSource_department" runat="server"
        ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
        SelectCommand="SELECT [DepID], [DepName] FROM [Department] where [DepID]&lt;&gt;0"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource_state" runat="server"
        ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
        SelectCommand="SELECT [locid], [LocName] FROM [State] where  [locid]&lt;&gt;0"></asp:SqlDataSource>
    <br />
    <span>GridView_Forms</span>
    <asp:GridView ID="GridView_Forms" runat="server"
        AutoGenerateColumns="False"
        AllowPaging="True"
        PageSize="5"
        
        OnPageIndexChanging="GridView_Forms_PageIndexChanging"
        OnPageIndexChanged="GridView_Forms_PageIndexChanged"
        OnRowCommand="GridView_Forms_RowCommand"
        OnRowCancelingEdit="GridView_Forms_RowCancelingEdit"
        OnRowDeleting="GridView_Forms_RowDeleting"
        OnRowEditing="GridView_Forms_RowEditing"
        OnRowUpdating="GridView_Forms_RowUpdating">
        <Columns>
            <asp:BoundField DataField="FormID" HeaderText="FormID" InsertVisible="False" ReadOnly="True"
                SortExpression="FormID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"></asp:BoundField>
            <asp:TemplateField HeaderText="نام فرم" SortExpression="FormName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox_Edit_Name" runat="server"
                        Text='<%# Bind("FormName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FormName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="توضیحات" SortExpression="Description">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox_Edit_Description" runat="server"
                        Text='<%# Bind("Description") %>' TextMode="MultiLine"
                        Width="95%"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="سوالات فرم ">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton_Question_Management" runat="server"
                        CausesValidation="false"
                        CommandName="Question" Text="مدیریت سوالات"
                        CommandArgument='<%# Eval("Formid") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle />
            </asp:TemplateField>
            <asp:ButtonField CommandName="View" Text="پاسخ به فرم پرسشنامه"
                HeaderText="پاسخ به فرم پرسشنامه">
                <ItemStyle />
            </asp:ButtonField>
            <asp:ButtonField HeaderText="ویرایش "
                CommandName="EditF" Text="ویرایش">
                <ItemStyle />
            </asp:ButtonField>
            <asp:TemplateField HeaderText="حذف" ShowHeader="False"
                Visible="False">
                <ItemStyle />
                <ItemTemplate>
                    <asp:Button ID="ImageButton1" runat="server" CausesValidation="False"
                        CommandName="Delete" CommandArgument='<%# Eval("Formid") %>'
                        OnClientClick="return confirm('sure');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" ShowHeader="True"
                HeaderText="ویرایش نام و توضیحات فرم"
                CancelText="لغو" UpdateText="تائید">
                <ItemStyle />
            </asp:CommandField>
            <asp:TemplateField HeaderText="مشاهده پاسخها">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server"
                        NavigateUrl='<%# GetFid(Eval("Formid")) %>' Text="نمایش"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="حذف">
                <ItemTemplate>
                    <asp:Button ID="ImageButton3" runat="server"
                        CommandName="Delete" CommandArgument='<%# Eval("Formid") %>'
                        Text="حذف"
                        OnClientClick="return confirm('sure')" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>
</asp:Content>

