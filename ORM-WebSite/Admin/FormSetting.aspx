<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FormSetting.aspx.cs"
    Inherits="FormSetting" Title="كاربران" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h3>تعیین فرم های گروه
    <asp:Label ID="lblGroup" runat="server"
        ForeColor="#0099FF"></asp:Label>
    </h3>
    <hr />
        <br/>
                <asp:LinkButton ID="btnUserSetting" OnClick="btnUserSetting_Click"
                    runat="server">
                    <asp:Label ID="lblUserSetting" runat="server">

                    </asp:Label>
                </asp:LinkButton>
            
                <asp:LinkButton ID="btnAdminSetting" OnClick="btnAdminSetting_Click"
                    runat="server">
                    <asp:Label ID="lblAdminSetting" runat="server"> </asp:Label>
                </asp:LinkButton>
            <a href="AddGroup.aspx">کلیه عملیات گروه</a>

    <h3>لیست فرم ها</h3>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound">

        <Columns>

            <asp:TemplateField>
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField Visible="False">

                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField HeaderText="نام فرم">

                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FormName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="خواندن">
                <ItemTemplate>
                    <asp:CheckBox ID="cbR" runat="server" />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="RSelectAll" runat="server"
                        Text="خواندن" />
                </HeaderTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="نوشتن">
                <ItemTemplate>
                    <asp:CheckBox ID="cbW" runat="server" />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="WSelectAll" runat="server"  Text="نوشتن" />
                </HeaderTemplate>


            </asp:TemplateField>
            <asp:TemplateField HeaderText="بروزرساني">
                <ItemTemplate>
                    <asp:CheckBox ID="cbU" runat="server" />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="USelectAll" runat="server"  
                        Text="به روز رسانی" />
                </HeaderTemplate>


            </asp:TemplateField>
            <asp:TemplateField HeaderText="حذف">
                <ItemTemplate>
                    <asp:CheckBox ID="cbD" runat="server" />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="DSelectAll" runat="server"  Text="حذف" />
                </HeaderTemplate>


            </asp:TemplateField>
            <asp:TemplateField HeaderText="طراحی سوال">
                <ItemTemplate>
                    <asp:CheckBox ID="cbAddQuestion" runat="server" />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="addSelectAll" runat="server" 
                        Text="طراحی سوال" />
                </HeaderTemplate>


            </asp:TemplateField>
            <asp:TemplateField HeaderText="مشاهده پاسخ های دیگران">
                <ItemTemplate>
                    <asp:CheckBox ID="cbReadOtherAnswers" runat="server" />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="readSelectAll" runat="server" 
                        Text="مشاهده پاسخ های دیگران" />
                </HeaderTemplate>


            </asp:TemplateField>
        </Columns>

        <SelectedRowStyle BackColor="#738A9C" />

        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>
    <br />
    <asp:Button ID="btnSave" runat="server" Text="ثبت تغييرات" 
        OnClick="btnSave_Click" />
       <script src="/jquery.min.js"></script>
    <script src="../JavaScript/FormsettingFnctn.js"></script>
</asp:Content>

