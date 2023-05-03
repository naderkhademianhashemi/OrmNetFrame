<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserSetting.aspx.cs" Inherits="UserSetting" Title="پرسشنامه" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="/jquery.min.js"></script>
    <script src="/JavaScript/UserSettingFnctn.js"></script>

    <h3>تعیین کاربران گروه
       <asp:Label ID="Label1" runat="server"  
           ></asp:Label>
    </h3>
    <hr />
                <asp:Label ID="lblSearch" runat="server" Text="نام کاربری یا سمت کاربر :"></asp:Label>
                <asp:TextBox ID="txtSearch" runat="server" Text="" ></asp:TextBox>
                <asp:Button  ID="btnSearch" runat="server"
                      Text="جستجو" OnClick="btnSearch_Click" />
                <asp:Label ID="lblCodePerson" runat="server" Text=" شماره پرسنلی :"></asp:Label>
                <asp:TextBox ID="txtCodePerson" runat="server" Text="" MaxLength="10" ></asp:TextBox>
                <asp:Button  ID="ImgBtnCodePerson" runat="server"
                      Text="جستجو" OnClick="ImgBtnCodePerson_Click" 
                    OnClientClick=" return check_CodePersonel()" EnableViewState="false" />
                <asp:LinkButton ID="btnAdminSetting" OnClick="btnAdminSetting_Click"
                    runat="server">
                    <asp:Label ID="lblAdminSetting" runat="server"> </asp:Label>
                </asp:LinkButton>
                <asp:LinkButton ID="btnFormSetting" OnClick="btnFormSetting_Click"
                    runat="server">
                    <asp:Label ID="lblFormSetting" runat="server">

                    </asp:Label>
                </asp:LinkButton>
                <a href="AddGroup.aspx">کلیه عملیات گروه</a>

    <h3>لیست کلیه کاربران 
    </h3>
    <asp:GridView ID="GridView1" runat="server"
        AutoGenerateColumns="False"   AllowPaging="true"
        OnDataBound="GridView1_DataBound"
        OnPageIndexChanging="GridView1_PageIndexChanging"
        OnRowEditing="GridView1_RowEditing"
        OnRowCancelingEdit="GridView1_RowCancelingEdit"
        OnRowUpdating="GridView1_RowUpdating">
        
        <Columns>
            <%-- cell 0 --%>
            <asp:TemplateField>
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- cell 1  --%>
            <asp:BoundField DataField="FullName" HeaderText="نام و نام خانوادگی" ReadOnly="true" Visible="true" />
            
            <%-- cell 2 --%>
            <asp:BoundField DataField="ShomarehPersenely" HeaderText="شماره پرسنلی" ReadOnly="true" Visible="true" />
            
            <%-- cell 3 --%>
            <asp:TemplateField HeaderText="نام کاربری">
                <ItemTemplate>
                    <asp:Label ID="lblUser_Name" runat="server" Text='<%#Bind("UserName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            
            <%-- cell 4 --%>
            <asp:BoundField DataField="Semat" HeaderText="سمت کاربر" ReadOnly="true" />
            
            <%-- cell edit group users --%>
            <asp:TemplateField HeaderText="عضویت" >
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="تغییر عضویت" />
                    <asp:CheckBox ID="cbSelect" runat="server" Enabled="false" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="لغو" />
                    <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="تایید" />
                    <asp:CheckBox ID="cbSelect" runat="server" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>

        
        <SelectedRowStyle BackColor="#738A9C"   />
        
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>
    <br />
    <hr />
    <h3>تعیین کاربران گروه
        <asp:Label ID="lblGroup" runat="server"  
            ></asp:Label>
        براساس سمت کاربری
    </h3>

                <h3>سمت کاربر : </h3>
                <asp:TextBox runat="server" ID="txtSemat" Text=""  />
    <br />
    <br />
    <asp:Button ID="btnAddMembers" runat="server" Text="اضافه کردن کاربران به گروه " OnClick="btnAddMembers_Click" OnClientClick="return confirm('آیا از سمت کاربری وارد شده اطمینان دارید ؟')" />
    <asp:Button runat="server" ID="btnRemoveMembers" Text="حذف کاربران از گروه " OnClick="btnRemoveMembers_Click" OnClientClick="return confirm('آیا از سمت کاربری وارد شده اطمینان دارید ؟')" />
    <hr />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
        ProviderName="System.Data.SqlClient"
        SelectCommand="select [User_Name],dbo.Group_Users.[Group_ID],[Group_Name] from dbo.Group_Users,dbo.Groups where dbo.Group_Users.[Group_ID] = dbo.Groups.[Group_ID] and [Group_Name] Like N' + @GroupName + '  and [User_Name] Like N'% + @UserName  + %'">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtSearch" PropertyName="Text" Name="UserName" DefaultValue="%" />
            <asp:Parameter Name="GroupName" DefaultValue="%" />
        </SelectParameters>
    </asp:SqlDataSource>
    
</asp:Content>

