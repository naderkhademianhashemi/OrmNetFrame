<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddGroup.aspx.cs"
    Inherits="AddGroup" Title="پرسشنامه" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>کلیه عملیات گروه</h3>
    <asp:Label ID="lblSearch" runat="server" Text="نام گروه:"></asp:Label>
    <asp:TextBox ID="txtSearch" runat="server" Text=""></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server"
        Text="جستجو"
        AccessKey="S"
        ToolTip="Alt + S" />
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <span>GridView1</span>
    <asp:GridView ShowFooter="True" ID="GridView1" runat="server"
        DataSourceID="SqlDataSource1"
        DataKeyNames="Group_ID"
        AutoGenerateColumns="False"
        AllowPaging="True">
        <SelectedRowStyle BackColor="#738A9C" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="Group_ID" HeaderText="Group_ID" InsertVisible="False" ReadOnly="True"
                SortExpression="Group_ID" Visible="False" />

            <asp:TemplateField SortExpression="Group_Name">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Group_Name") %>'></asp:TextBox>
                </EditItemTemplate>

                <FooterTemplate>
                    <asp:TextBox ID="txtAddGroupName" runat="server" Text='<%# Bind("Group_Name") %>'></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Group_Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField SortExpression="Description">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAddDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="اضافه  کردن">
                <FooterTemplate>
                    <asp:LinkButton ID="lbtnAdd" runat="server" OnClick="lbtnAdd_Click">افزودن گروه</asp:LinkButton>
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False" HeaderText="ویرایش">
                <EditItemTemplate>
                    <asp:Button ID="lbtnEdit" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Update"></asp:Button>
                    <asp:Button ID="lbtnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:Button>
                </EditItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Button ID="lbtEdit" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False" HeaderText="حذف">
                <FooterTemplate>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Button ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="Delete"
                        Text="Delete" OnClientClick="return confirm('آیا از حذف این گروه اطمینان دارید؟');"></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="تعیین کاربران گروه">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnUsers" runat="server" CommandArgument='<%# BeforSendUsers(Eval("Group_ID"),Eval("Group_Name")) %>'
                        OnClick="lbtnUsers_Click">تعیین کاربران گروه</asp:LinkButton>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="تعیین فرمهای گروه">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnForms" runat="server" OnClick="lbtnForms_Click" CommandArgument='<%# BeforSendForms(Eval("Group_ID"),Eval("Group_Name")) %>'>تعیین فرمهای گروه</asp:LinkButton>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="تعیین ارشد گروه">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDefineGroupAdmin"
                        runat="server"
                        OnClick="btnDefineGroupAdmin_Click"
                        CommandArgument='<%# BeforSendForms(Eval("Group_ID"),Eval("Group_Name")) %>'>
                        <asp:Label ID="lblDefineGroupAdmin" runat="server"
                            Text="تعیین ارشد گروه">
                        </asp:Label>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

        <EmptyDataTemplate>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtAddGroupName" runat="server" Text='<%# Bind("Group_Name") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtAddDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox></td>
                    <td>
                        <asp:LinkButton ID="lbtnAdd" runat="server" OnClick="lbtnAdd_Click">Add</asp:LinkButton></td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:GridView>

    <br />
    <h4>ایجاد دسترسی همگانی برای کاربران جدید اکتیو دیرکتوری </h4>
    <hr />
    <asp:Label ID="lblGroupName" Text="نام گروه برای دسترسی : " runat="server" />
    <asp:DropDownList runat="server" ID="drpListGroups" DataSourceID="SqlDataSource1" DataTextField="Group_Name" DataValueField="Group_ID">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button runat="server" ID="btnAddADUsersToGroup" Text="دسترسی کاربران به گروه انتخابی"
        OnClick="btnAddADUsersToGroup_Click"
        OnClientClick="return confirm('آیا از دسترسی کاربران جدید اکتیو دیرکتوری به گروه انتخابی اطمینان دارید؟ ')" />
    <hr />

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
        ProviderName="System.Data.SqlClient"
        SelectCommand="SELECT Group_ID, Group_Name, Description FROM Groups WHERE (Group_Name LIKE '%' + @GroupName + '%')"
        InsertCommand="Insert into Groups (Group_Name,Description) VALUES (@GroupName,@Description);"
        DeleteCommand="Delete from Groups where [Group_ID]=@Group_ID"
        UpdateCommand="UPDATE Groups SET Group_Name =@Group_Name, Description =@Description WHERE [Group_ID]=@Group_ID ;">
        <DeleteParameters>
            <asp:Parameter Name="GroupID" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Group_Name" />
            <asp:Parameter Name="Description" />
            <asp:Parameter Name="GroupID" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="GroupName"></asp:Parameter>
            <asp:Parameter Type="String" Name="Description"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtSearch" PropertyName="Text"
                Name="GroupName" DefaultValue="%" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

