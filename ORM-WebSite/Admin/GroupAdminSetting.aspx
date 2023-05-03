<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="GroupAdminSetting.aspx.cs" Inherits="Admin_GroupAdminSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <h3>تعیین کابران ارشد در گروه
        <asp:Label ID="lblGruop" runat="server"  
            ForeColor="#0099FF"></asp:Label>
    </h3>
    <hr />


    <table>

        <tr>
            <td >نام کاربر 

            </td>

            <td>
                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>

            </td>

            <td align="left">
                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="جستجو" />

            </td>
        </tr>
        <tr>
            <td >شماره پرسنلی
            </td>
            <td>
                <asp:TextBox ID="txtCodePerson" runat="server"></asp:TextBox>

            </td>
            <td align="left">

                <asp:Button ID="btnSearchCodePerson" OnClick="btnSearchCodePerson_Click"
                    OnClientClick="check_CodePersonel()"
                    runat="server" Text="جستجو" />
            </td>

        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="btnUserSetting" OnClick="btnUserSetting_Click"
                    runat="server">
                    <asp:Label ID="lblUserSetting" runat="server">

                    </asp:Label>
                </asp:LinkButton></td>
            <td>
                <asp:LinkButton ID="btnFormSetting" OnClick="btnFormSetting_Click"
                    runat="server">
                    <asp:Label ID="lblFormSetting" runat="server">

                    </asp:Label>
                </asp:LinkButton></td>
            <td align="left"><a href="AddGroup.aspx">کلیه عملیات گروه</a></td>
        </tr>
    </table>


    <h3>لیست کاربران گروه
    <asp:Label ID="lblGroup2" runat="server"  
        ForeColor="#0099FF"></asp:Label>
    </h3>

    <asp:GridView ID="GridView1" runat="server"
         
          
        AutoGenerateColumns="False"
        OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
        OnRowUpdating="GridView1_RowUpdating"
        EmptyDataText="اطلاعاتی یافت نشد"
        AllowPaging="true"
        OnPageIndexChanging="GridView1_PageIndexChanging">
        <SelectedRowStyle BackColor="#738A9C"   />
        
        <AlternatingRowStyle BackColor="#F7F7F7" />
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
            <asp:BoundField DataField="Semat" HeaderText="سمت کاربر" ReadOnly="true" />
            <%-- cell 4 --%>

            <asp:TemplateField HeaderText="نام کاربر">
                <ItemTemplate>
                    <asp:Label ID="lblUserName" runat="server"
                        Text='<%# Bind("User_Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <%-- cell 5 --%>
            <asp:TemplateField HeaderText="کاربر ارشد">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="ویرایش" />
                    <asp:CheckBox ID="cbSelect" runat="server" Enabled="false"
                        Checked='<%# Bind("IsAdmin") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="لغو" />
                    <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="تایید" />
                    <asp:CheckBox ID="cbSelect" runat="server" Checked='<%# Bind("IsAdmin") %>' />
                </EditItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>
    
    <script type="text/javascript">
        UsersInGroups();
        function UsersInGroups() {
            var grd = document.getElementById('<%=this.GridView1.ClientID%>');
            var Inputs = grd.getElementsByTagName('input');
            for (var i = 0; i < Inputs.length; i++) {
                if (Inputs[i].type == 'checkbox' && Inputs[i].checked == true)
                    Inputs[i].parentNode.parentNode.parentNode.style.backgroundColor = "gold";
            }
        }

        function check_CodePersonel() {
            var id = '<%=this.txtCodePerson.ClientID%>';
            var CodePersonel = document.getElementById(id);

            if (isNaN(CodePersonel.value) || CodePersonel.value.length < 5 ||
                CodePersonel.value.length > 8) {
                alert('لطفا یک شماره پرسنلی معتبر وارد کنید ');
                CodePersonel.value = "";
                return false;
            }
            else {

                return true;
            }
        }

    </script>

</asp:Content>

