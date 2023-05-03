<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FormDefine.aspx.cs" Inherits="FormDefine" Title="پرسشنامه" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <asp:Panel ID="Panel3" runat="server">
                    <asp:Image ID="Image3" runat="server" />
                    <asp:Label ID="Label5" runat="server"
                        Text="تعریف فرم"></asp:Label>
                    <br>
                    <asp:Label ID="Label1" runat="server" Text="نام فرم :"></asp:Label>
                    <asp:TextBox ID="txtFName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvForm" runat="server"
                        ControlToValidate="txtFName" ErrorMessage="تکمیل نام فرم ضروری است.">*</asp:RequiredFieldValidator>
                    <br>
                    <asp:Label ID="Label2" runat="server" Text="توضیحات :"></asp:Label>
                    <asp:TextBox ID="txtFDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server">
                    <asp:Image ID="Image2" runat="server"></asp:Image>
                    <asp:Label ID="Label7" runat="server" Text="تخصیص فرم به دپارتمان و استانها"></asp:Label>
                    <br>
                    <asp:Label ID="Label8" runat="server" Text="حوزه عملیاتی:"></asp:Label>
                    <asp:CheckBox ID="Dep_selectall" runat="server" __designer:wfdid="w6"
                        AutoPostBack="True" OnCheckedChanged="selectall_CheckedChanged"
                        Text="انتخاب تمامي دپارتمان ها" />
                    <asp:CheckBoxList ID="CheckBoxList_Department" runat="server"
                        DataSourceID="SqlDataSource_department" DataTextField="DepName"
                        DataValueField="DepID">
                    </asp:CheckBoxList>

                    <br>
                    <br>
                    <asp:Label ID="Label9" runat="server" Text="نام استان ها :"
                        Visible="False"></asp:Label><br />
                    <asp:CheckBoxList ID="CheckBoxList_State" runat="server"
                        DataSourceID="SqlDataSource_state" DataTextField="LocName"
                        DataValueField="locid" ForeColor="Black"
                        Visible="False">
                    </asp:CheckBoxList>
                    <br />
                    <br />
                    <asp:CheckBox ID="state_selectall" runat="server" __designer:wfdid="w6"
                        AutoPostBack="True" OnCheckedChanged="selectall_CheckedChanged"
                        Text="انتخاب همه استان ها" Checked="True" Visible="False" />
                    <br>
                    <asp:Label ID="ValidityTest" runat="server"
                        ForeColor="#FF3300"></asp:Label>
                    <br>
                    <asp:Button ID="btnAddDep" OnClick="btnAddDep_Click" runat="server" Text="ثبت فرم و تخصيص مجدد"
                        Width="150px"></asp:Button>
                    <asp:Button ID="btnAddForm" OnClick="btnAddForm_Click" runat="server" Text="ثبت فرم  و تعریف سوال"
                        Width="150px"></asp:Button>
                    <br>
                </asp:Panel>
                <br>
                <br>
                <br>
                <asp:SqlDataSource ID="SqlDataSource_department" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
                    SelectCommand="SELECT [DepID], [DepName] FROM [Department] where [DepID]&lt;&gt;0"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_state" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
                    SelectCommand="SELECT [locid], [LocName] FROM [State] where  [locid]&lt;&gt;0"></asp:SqlDataSource>
                <br>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
