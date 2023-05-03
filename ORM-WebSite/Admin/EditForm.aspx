<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditForm.aspx.cs" Inherits="EditForm" Title="پرسشنامه" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br>
    <span>ويرايش فرم خصوصيات پرسشنامه</span>

    <asp:Label ID="lblForm" runat="server"></asp:Label>
    <br>
    <br>
    <br>
    <asp:Image ID="Image2" runat="server"></asp:Image>
    <asp:Label ID="Label7" runat="server" Text="تخصیص فرم به دپارتمان و استانها"></asp:Label>
    <br>

    <br>

    <asp:Label ID="Label8" runat="server" Text="حوزه عملیاتی  :"></asp:Label>
    <asp:CheckBox ID="Dep_selectall"
        runat="server" 
        AutoPostBack="True" OnCheckedChanged="selectall_CheckedChanged"
        Text="انتخاب تمامي دپارتمان ها" />


    <div>
        <asp:CheckBoxList ID="CheckBoxList_Department" runat="server"
            DataSourceID="SqlDataSource_department" DataTextField="DepName"
            DataValueField="DepID">
        </asp:CheckBoxList>
    </div>


    <br>

    <asp:Label ID="Label9" runat="server" Text="نام استان ها :"
        Visible="False"></asp:Label>



    <br>

    <br>
        <asp:CheckBox ID="selectall" runat="server" Text="انتخاب همه استان ها" 
            OnCheckedChanged="selectall_CheckedChanged"
            AutoPostBack="True" Checked="True" Visible="False"></asp:CheckBox>
    </br>
    <br />


    <div>
        <asp:CheckBoxList ID="chkloc" runat="server"
            DataSourceID="SqlDataSource_state" DataValueField="LocID"
            DataTextField="LocName">
        </asp:CheckBoxList>
    </div>


    <br>

    <asp:Label ID="ValidityTest" runat="server"></asp:Label>
    <br />
    <asp:Button ID="btnAddForm" OnClick="btnAddForm_Click" runat="server" Text="تایید تخصیص"
        Width="150px"></asp:Button>


    <br>

    <asp:SqlDataSource ID="SqlDataSource_department" runat="server"
        ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
        SelectCommand="SELECT [DepID], [DepName] FROM [Department] where [DepID]&lt;&gt;0"></asp:SqlDataSource>


    <asp:SqlDataSource ID="SqlDataSource_state" runat="server"
        ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
        SelectCommand="SELECT [locid], [LocName] FROM [State] where  [locid]&lt;&gt;0"></asp:SqlDataSource>


    <br>

    <asp:Label ID="Label3" runat="server" Text="بازگشت به صفحه قبل "></asp:Label>
    <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" />
</asp:Content>

