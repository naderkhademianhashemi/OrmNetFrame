<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Editoption.aspx.cs" Inherits="Editoption" Title="پرسشنامه" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="/JavaScript/EditOptionFnctn.js"></script>
    <span>ويرايش گزينه هاي سوال</span>
    <br />

    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="Branch" runat="server">
            <span>GridView1</span>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <span>GridView2</span>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                DataKeyNames="BID" DataSourceID="SqlDataSource2"
                EmptyDataText="no data records to display."
                AllowPaging="True">
                <Columns>
                    <asp:BoundField DataField="Branch" HeaderText="كد شعبه"
                        SortExpression="Branch" />
                    <asp:BoundField DataField="BranchName" HeaderText="نام شعبه"
                        SortExpression="BranchName" />
                    <asp:BoundField DataField="Description" HeaderText="توضيحات"
                        SortExpression="Description" />
                    <asp:BoundField DataField="BID" HeaderText="BID" ReadOnly="True"
                        SortExpression="BID" Visible="False" />
                </Columns>

                <SelectedRowStyle BackColor="#738A9C" />

                <AlternatingRowStyle BackColor="#F7F7F7" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
                ProviderName="<%$ ConnectionStrings:ORM_ConnectionString.ProviderName %>"
                SelectCommand="SELECT [Branch], [BranchName], [State_Of_Branch], [Branch_Rank], [Limit], [Description], [BID] FROM [Branch] where BID&lt;&gt;0"></asp:SqlDataSource>
        </asp:View>
        <asp:View ID="location" runat="server">
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"
                DataKeyNames="locid" DataSourceID="SqlDataSource3"
                EmptyDataText="There are no data records to display."
                AllowPaging="True">
                <Columns>
                    <asp:BoundField DataField="locid" HeaderText="كد محل" ReadOnly="True"
                        SortExpression="locid" />
                    <asp:BoundField DataField="LocName" HeaderText="نام محل"
                        SortExpression="LocName" />
                </Columns>
                <SelectedRowStyle BackColor="#738A9C" />
                <AlternatingRowStyle BackColor="#F7F7F7" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
                ProviderName="<%$ ConnectionStrings:ORM_ConnectionString.ProviderName %>"
                SelectCommand="SELECT [locid], [LocName], [description] FROM [state] where   [locid]&lt;&gt;0"></asp:SqlDataSource>
        </asp:View>
        <asp:View ID="Queitem" runat="server">
            <span>GridView_options</span>
            <asp:GridView ID="GridView_options" runat="server" AllowPaging="True"
                AllowSorting="True" AutoGenerateColumns="False"
                DataKeyNames="ITID"
                ShowFooter="True"
                OnRowEditing="GridView_options_RowEditing"
                OnRowDataBound="GridView_options_RowDataBound"
                OnRowCancelingEdit="GridView_options_RowCancelingEdit"
                OnRowUpdating="GridView_options_RowUpdating"
                OnRowCommand="GridView_options_RowCommand"
                OnRowDeleting="GridView_options_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="GroupID" 
                        HeaderText="ITID" InsertVisible="False"
                        ReadOnly="True" SortExpression="" Visible="False" />
                    <asp:TemplateField HeaderText="شماره گزینه" SortExpression="">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Itemnum") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item"
                        SortExpression="Item">
                        <EditItemTemplate>
                            <span>TextBox1</span>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Item_Text") %>'>
                            </asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <span>Panel1</span>
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAdd">
                                <table>
                                    <tr>
                                        <td>
                                            <span>txtAddItem</span>
                                            <asp:TextBox ID="txtAddItem" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAdd" runat="server" Text="Add"
                                                OnClick="btnAdd_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Item_Text") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="اضافه کردن" Visible="False"></asp:TemplateField>
                    <asp:TemplateField HeaderText="ویرایش" ShowHeader="False">
                        <EditItemTemplate>
                            <asp:Button ID="lbtnEdit" runat="server" CausesValidation="True"
                                CommandName="Update" Text="Update" />
                            <asp:Button ID="lbtnCancel" runat="server" CausesValidation="False"
                                CommandName="Cancel" Text="Cancel" />
                        </EditItemTemplate>

                        <ItemTemplate>
                            <asp:Button ID="lbtEdit" runat="server" CausesValidation="False"
                                CommandName="Edit" Text="ويرايش" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="ImageButton2" runat="server" Text="حذف"
                                CommandArgument='<%# Eval("Item_ID") %>'
                                OnClientClick="return confirm('آیا اطمینان دارید ');"
                                CommandName="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td>
                                <span>EmptyDataTemplate txtAddItem</span>
                                <asp:TextBox ID="txtAddItem2" runat="server" 
                                    Text='<%# Bind("Item_Text") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="ImageButton3" runat="server" CausesValidation="false"
                                    Text="Add item"
                                    OnClick="btnAdd_Click" />
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle BackColor="#738A9C" />
                <AlternatingRowStyle BackColor="#F7F7F7" />
            </asp:GridView>
        </asp:View>
        <asp:View ID="Department" runat="server">
            <asp:SqlDataSource ID="SqlDataSource4" runat="server"
                ConnectionString="<%$ ConnectionStrings:ORM_ConnectionString %>"
                SelectCommand="SELECT [DepID], [DepName] FROM [Department] WHERE ([DepID] &lt;&gt; @DepID)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="DepID" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:GridView ID="GridView4" runat="server" AllowPaging="True"
                AutoGenerateColumns="False"
                DataKeyNames="depid"
                DataSourceID="SqlDataSource4"
                EmptyDataText="There are no data records to display.">
                <Columns>
                    <asp:BoundField DataField="DepID" HeaderText="كد دپارتمان" ReadOnly="True"
                        SortExpression="depid" />
                    <asp:BoundField DataField="DepName" HeaderText="نام دپارتمان"
                        SortExpression="depname" />
                </Columns>
                <SelectedRowStyle BackColor="#738A9C" />
                <AlternatingRowStyle BackColor="#F7F7F7" />
            </asp:GridView>
        </asp:View>
    </asp:MultiView>

    <br />
    
    <asp:Panel ID="Panel2" runat="server" DefaultButton="button_add">
        <br />
        <br />
        <span>Panel2</span>
        <asp:Label ID="lbl_fieldtitle" runat="server">عنوان فیلد :  </asp:Label>
        <asp:TextBox ID="TextBox_Add" runat="server" Width="250px"></asp:TextBox>
        <br />

        <asp:CheckBox ID="chkField_Optional" runat="server" Text="اجباری"
            TextAlign="Left" />

        <asp:Label ID="lbl_fieldtype" runat="server">نوع فیلد :  </asp:Label>
        <asp:DropDownList ID="cmbFieldtype" runat="server" Width="146px">
        </asp:DropDownList>


        <asp:Button ID="button_add" runat="server" Text="add"
            OnClick="button_add_Click" />
        <br />

        <asp:CheckBox ID="chkField_Filled" runat="server" Text="دارای مقدار"
            TextAlign="Left" AutoPostBack="True"
            OnCheckedChanged="chkField_Filled_CheckedChanged" />

        <asp:Label ID="lbl_fieldfill" runat="server" Visible="False">نام جدول :</asp:Label>
        <asp:DropDownList ID="cmbFieldFilled" runat="server" Width="146px"
            Visible="False">
        </asp:DropDownList>
        <br />


        <span>GridView_ListItem</span>
        <asp:GridView ID="GridView_ListItem" runat="server" AutoGenerateColumns="False"
            DataKeyNames="Item_ID"
            AllowPaging="True"
            OnRowDataBound="GridView_ListItem_RowDataBound"
            OnRowCommand="GridView_ListItem_RowCommand"
            OnRowDeleting="GridView_ListItem_RowDeleting"
            OnRowEditing="GridView_ListItem_RowEditing"
            OnRowCancelingEdit="GridView_ListItem_RowCancelingEdit"
            OnRowUpdating="GridView_ListItem_RowUpdating">
            <Columns>
                <asp:BoundField DataField="Question_ID" HeaderText="Question_ID" InsertVisible="False" ReadOnly="True"
                    SortExpression="" Visible="False" />
                <asp:TemplateField HeaderText="عنوان فیلد" SortExpression="Item_Text">
                    <ItemTemplate>
                        <asp:Label ID="lbl_itemtext" runat="server" Text='<%# Bind("Item_Text") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_ItemText" runat="server" Text='<%# Bind("Item_Text") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="اجباری" SortExpression="Optional">
                    <ItemTemplate>
                        <asp:Label ID="lbl_optional" runat="server" Text='<%# Bind("Optional") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="drd_optional" runat="server"
                            SelectedIndex='<%# Bind("BOptional") %>'>
                            <asp:ListItem Value="0">اختياري</asp:ListItem>
                            <asp:ListItem Value="1">اجباري</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="نوع فیلد" SortExpression="ِType">
                    <ItemTemplate>
                        <asp:Label ID="lbl_type" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="drd_type" runat="server" AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="مقداردار" SortExpression="Filled">
                    <ItemTemplate>
                        <asp:Label ID="lbl_filled" runat="server" Text='<%# Bind("Filled") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="drd_filled" runat="server"
                            SelectedIndex='<%# Bind("BFilled") %>'>
                            <asp:ListItem Value="0">خالی</asp:ListItem>
                            <asp:ListItem Value="1">مقداردار</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="جدول" SortExpression="Table_Name">
                    <ItemTemplate>
                        <asp:Label ID="lbl_table" runat="server" Text='<%# Bind("Table_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="drd_table" runat="server" AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="[Item_ID]" HeaderText="ItemID" SortExpression="Item_ID"
                    Visible="False" />
                <asp:BoundField DataField="[Form_ID]" Visible="False" />
                <asp:TemplateField ShowHeader="False" HeaderText="حذف ">
                    <ItemTemplate>
                        <asp:Button ID="ImageButton22" runat="server"
                            CausesValidation="False" CommandName="Delete"
                            Text="Delete"
                            OnClientClick="return confirm('آیا اطمینان دارید ');"
                            CommandArgument='<%# Bind("Item_ID") %>' />
                    </ItemTemplate>
                    <ControlStyle BackColor="#999999" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="ویرایش">
                    <EditItemTemplate>
                        <asp:Button ID="ImageButton23" runat="server" CausesValidation="True"
                            CommandName="Update" Text="Update" />
                        <asp:Button ID="ImageButton24" runat="server" CausesValidation="False"
                            CommandName="Cancel" Text="Cancel" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="ImageButton25" runat="server" CausesValidation="False"
                            CommandName="Edit" Text="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <SelectedRowStyle BackColor="#738A9C" />
            <AlternatingRowStyle BackColor="#F7F7F7" />
        </asp:GridView>


    </asp:Panel>
    <br />
   
    <asp:Label ID="Label3" runat="server" Text="بازگشت به صفحه قبل "></asp:Label>

</asp:Content>
