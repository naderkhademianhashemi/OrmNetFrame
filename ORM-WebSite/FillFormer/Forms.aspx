<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Forms.aspx.cs" Inherits="_FillFormer_Forms" Title="پرسشنامه" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblMess" runat="server" Text="فرمی برای نمایش وجود ندارد." 
        Visible="false"></asp:Label>
                <%-- GridView_Forms --%>
                <%-- GridView_Forms.DataSource = ORM.Forms.get_Forms_By_Permission(Profile.UserName); --%>
                <asp:GridView ID="GridView_Forms" runat="server"
                    AutoGenerateColumns="False"
                     OnRowCommand="GridView_Forms_RowCommand" 
                      AllowPaging="True" PageSize="20"
                    EmptyDataText="هيچ فرمي براي نمايش وجود ندارد ">
                    <Columns>
                        <asp:BoundField DataField="FormID" HeaderText="FormID" InsertVisible="False" 
                            ReadOnly="True"
                            SortExpression="FormID" Visible="False">
                            
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="نام فرم" SortExpression="FormName"
                             >
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox_Edit_Name" runat="server" BackColor="#E0E0E0" 
                                     Text='<%# Bind("FormName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("FormName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="توضیحات" SortExpression="Description"
                             >
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox_Edit_Description" runat="server" BackColor="#E0E0E0" 
                                     Text='<%# Bind("Description") %>' TextMode="MultiLine"
                                    Width="95%"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="مشاهده پاسخها">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" Visible='<%# Bind("readable") %>'
                                    NavigateUrl='<%# GetFid(Eval("Formid")) %>' Text="نمایش"
                                    ></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="پاسخ به فرم پرسشنامه">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="View" 
                                    Visible='<%# Bind("writeable") %>'
                                    CommandArgument='<%# Bind("FormID") %>'>پاسخ به فرم 
                                    پرسشنامه</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="طراحی سوال">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonAddQuestion" runat="server" 
                                    CommandName="NewQuestion"
                                     Visible='<%# Bind("AddQuestion") %>'
                                    CommandArgument='<%# ConcatArgs(Eval("FormID"),Eval("FormName")) %>'> 
                                    طراحی سوال
                                    </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle   />
                    <SelectedRowStyle BackColor="#738A9C"   />
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                </asp:GridView>
    <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
</asp:Content>
