<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ManageUser.aspx.cs" 
    Inherits="ManageUser" Title="پرسشنامه" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="/jquery.min.js"></script> 
    <script src="/JavaScript/ManageUserFnctn.js"></script>
    <h3>مدیریت کاربران سایت</h3>

    <hr />
    <br />
    <table>
        <tr>
            <td >
                <asp:Label ID="lblSearch" runat="server" Text="نام کاربری یا سمت کاربر :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" Text="" ></asp:TextBox>

            </td>
            <td align="left">
                <asp:Button  ID="btnSearch" runat="server"
                      Text="جستجو"
                    OnClick="btnSearch_Click"
                    AccessKey="S"
                    ToolTip="Alt + S" />
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblCodePerson" runat="server" Text=" شماره پرسنلی :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCodePerson" runat="server" Text="" MaxLength="10" ></asp:TextBox>

            </td>
            <td align="left">
                <asp:Button  ID="btnCodePerson" runat="server"
                      Text="جستجو"
                    OnClick="btnCodePerson_Click"
                    OnClientClick="return check_CodePersonel()"
                    AccessKey="C"
                    ToolTip="Alt + C" />
            </td>

        </tr>
    </table>
    <br />
    <h3>لیست کاربران</h3>
    <table width="600px" >

        <tr>
            <td  style="width: 698px">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                    
                     
                      AllowPaging="True" Width="720px"
                    OnPageIndexChanging="GridView2_PageIndexChanging"
                    OnDataBound="GridView2_DataBound"
                    OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
               
                    <Columns>

                        <%-- cell 0 --%>
                        <asp:CommandField ShowSelectButton="True" HeaderText="انتخاب سطر" SelectText="انتخاب" ButtonType="Button" />
                        <%-- cell 1 --%>
                        <asp:BoundField DataField="UserName" HeaderText="نام کاربری" />
                        <%-- cell 2 --%>
                        <asp:BoundField HeaderText="نام دپارتمان" />
                        <%-- cell 3 --%>
                        <asp:BoundField DataField="Semat" HeaderText="سمت کاربر" />
                        <%-- cell 4 --%>
                        <asp:BoundField DataField="CodeMahalKhedmat" HeaderText="کد محل خدمت(12:ستادی)" />
                        <%-- cell 5 --%>
                        <asp:BoundField DataField="MahalKhedmat" HeaderText="شرح محل خدمت" />
                       
                        <asp:BoundField DataField="FullName" HeaderText="نام و نام خانوادگی" ReadOnly="true" Visible="true" />
                        <asp:BoundField DataField="ShomarehPersenely" HeaderText="شماره پرسنلی" ReadOnly="true" Visible="true" />

                    </Columns>
                    
                    <SelectedRowStyle BackColor="#738A9C"   />
                    
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                </asp:GridView>
                <br />
                <br />
                <br />
            </td>
        </tr>


    </table>
    <h3>اعطا و منع دسترسی مدیریت ارشد</h3>
    <hr />
    <table>
        <tr>
            <td style="width: 60px;">نام کاربری : 
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtUser" />
            </td>
            <td style="padding-right: 15px;">
                <asp:Button ID="btnAddUserToRole" Text="اعطای دسترسی" runat="server" OnClick="btnAddUserToRole_Click" OnClientClick="return confirm_Admin_Grant()" />
            </td>
        </tr>
    </table>
    <p>
        به منظور اعطای سطح دسترسی مدیریت ارشد ، ردیف مورد نظر را از لیست کاربران انتخاب نموده و سپس بر روی دکمه - اعطای دسترسی - کلیک کنید . 
    </p>
    <table>
        <tr>
            <td colspan="3">
                <h3>کاربران ارشد</h3>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="grdAdminRoles" runat="server"
                        
                    AutoGenerateColumns="False"
                    AllowPaging="true"
                    OnPageIndexChanging="grdAdminRoles_PageIndexChanging"
                    OnSelectedIndexChanged="grdAdminRoles_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" SelectText="انتخاب" ButtonType="Button" />
                        <asp:TemplateField HeaderText="نام کاربر">
                            <ItemTemplate>
                                <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="میزان دسترسی">
                            <ItemTemplate>
                                <asp:Label ID="lblRoleName" runat="server" Text='<%# Bind("RoleName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    
                    <SelectedRowStyle BackColor="#738A9C"   />
                    
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                    <SelectedRowStyle BackColor="#738A9C"   />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td style="width: 60px;">نام کاربر : 
            </td>
            <td>
                <asp:TextBox ID="txt_UserToFillFormer" runat="server"></asp:TextBox>
            </td>
            <td style="padding-right: 15px;">
                <asp:Button ID="btnAddUserToFillFormer" runat="server" Text="منع دسترسی" OnClientClick="return  confirm_Admin_Revoke()" OnClick="btnAddUserToFillFormer_Click" />
            </td>
        </tr>

    </table>
    <p>
        به منظور منع سطح دسترسی مدیریت ارشد ، ردیف مورد نظر را از-لیست کاربران ارشد- انتخاب نموده و سپس بر روی دکمه - منع دسترسی - کلیک کنید . 
                
         
    </p>
    <hr />

    
</asp:Content>

