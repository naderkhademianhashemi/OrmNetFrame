<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FillFormerNewQuestion.aspx.cs" Inherits="FillFormerNewQuestion" Title="پرسشنامه" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
                <span >تنظيم سوالات پرسشنامه
                </span>
                <asp:Label ID="Label9" runat="server"  
                    ForeColor="#000099" Text="Label" ></asp:Label>
    <span>GridView_Form_Questions</span>
                <asp:GridView ID="GridView_Form_Questions" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="QID"   
                    AllowPaging="True" 
                    OnRowDeleting="GridView_Form_Questions_RowDeleting"
                    OnRowCommand="GridView_Form_Questions_RowCommand" 
                    OnRowDataBound="GridView_Form_Questions_RowDataBound"
                    OnSelectedIndexChanged="GridView_Form_Questions_SelectedIndexChanged" 
                    OnRowEditing="GridView_Form_Questions_RowEditing"
                     OnRowCancelingEdit="GridView_Form_Questions_RowCancelingEdit"
                    OnRowUpdating="GridView_Form_Questions_RowUpdating" 
                    OnPageIndexChanging="GridView_Form_Questions_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="QID" HeaderText="QID" InsertVisible="False" ReadOnly="True"
                            SortExpression="QID" Visible="False" />
                        <asp:TemplateField HeaderText="شماره سوال" SortExpression="QueNum">
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("QueNum") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" سوال" SortExpression="Que">
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("Que") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Que") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="  جواب " SortExpression="ِStatus">
                            <ItemTemplate>
                                <asp:Label ID="stlbl0" runat="server" Text='<%# GetStatus(Eval("Status")) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList2" runat="server"
                                    SelectedIndex='<%# GetselectIndex(Eval("Status")) %>'>
                                    <asp:ListItem Value="0">اختياري</asp:ListItem>
                                    <asp:ListItem Value="1">اجباري</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="نوع" SortExpression="Type">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# GetType(Eval("Type")) %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderTemplate>
                                نوع سوال
                            </HeaderTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="سئوال فعال">
                            <ItemTemplate>
                                <asp:Label ID="lblQueIsActive" runat="server" 
                                    Text='<%# IsQueActiv(Eval("QueIsActive")) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="drpQueIsActive" runat="server"
                                    SelectedIndex='<%# GetselectIndex(Eval("QueIsActive")) %>'>
                                    <asp:ListItem Value="1">فعال</asp:ListItem>
                                    <asp:ListItem Value="0">غیر فعال</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FormID" HeaderText="FormID" SortExpression="FormID" Visible="False" />
                        <asp:BoundField DataField="ColNum" SortExpression="ColNum" Visible="False" />
                        <asp:TemplateField ShowHeader="False" HeaderText="حذف سوال">
                            <ItemTemplate>
                                
                                <asp:Button ID="ImageButton22" runat="server" CausesValidation="False" CommandName="Delete"
                                     Text="Delete"
                                    OnClientClick="return confirm(' اطمینان ');"
                                    CommandArgument='<%# Eval("QID") %>' />
                            </ItemTemplate>
                            <ControlStyle BackColor="#999999" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderText="ویرایش سوال">
                            <EditItemTemplate>
                                <asp:Button ID="ImageButton23" runat="server" CausesValidation="True"
                                    CommandName="Update"  Text="Update" />
                                <asp:Button ID="ImageButton24" runat="server" CausesValidation="False"
                                    CommandName="Cancel"  Text="Cancel" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Button ID="ImageButton25" runat="server" CausesValidation="False"
                                    CommandName="Edit"  Text="Edit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="تخصیص گزینه">
                            <ItemTemplate>
                                <div >
                                    <asp:Button ID="allocate0" runat="server" Text="تخصیص گزینه"
                                        Enabled='<%# linkIsEnable(Eval("Type")) %>'
                                        CausesValidation="false" CommandArgument='<%# Eval("QID") %>' 
                                        OnClick="ImageButton2_Click" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="جابجایی">
                            <ItemTemplate>
                                            <asp:Button  ID="Button3" runat="server" CausesValidation="False"
                                                CommandArgument='<%#Eval("QID")+";"+Eval("quenum")  %>'
                                                OnClick="Button1_Click" Text="بالا" />
                                            <asp:Button  ID="Button4" runat="server" CausesValidation="False"
                                                CommandArgument='<%# Eval("QID")+";"+Eval("quenum") %>'
                                                OnClick="Button2_Click" Text="پايين" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle BackColor="#738A9C"   />
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                </asp:GridView>
    <br />
                <asp:Label ID="Label7" runat="server" Text="تعریف سوال جدید" 
                     ></asp:Label>
        <br />
                <asp:Label ID="Label8" runat="server" Text="صورت سوال" 
                    ></asp:Label>
            <br />
                <asp:TextBox ID="txtQue" runat="server"  
                     ></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtQue" ErrorMessage="لطفا صورت سوال را وارد كنيد."
                    Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server"
                    ControlToValidate="txtQue" Display="Dynamic"
                    ErrorMessage="اين نام هم اكنون وجود دارد."
                    OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="1"></asp:CustomValidator>
            <br />
                <asp:Label ID="Label4" runat="server"  Text="نوع سوال"  ></asp:Label>
            
                <asp:RadioButtonList ID="rblTypeQue" runat="server">
                    <asp:ListItem Value="1" Selected="True">متنی </asp:ListItem>
                    <asp:ListItem Value="2">چندگزینه ای چند جوابی</asp:ListItem>
                    <asp:ListItem Value="3">چند گزینه ای تک جوابی </asp:ListItem>
                    <asp:ListItem Value="4">چند گزینه ای تک جوابی نمایش به صورت آبشاری</asp:ListItem>
                    <asp:ListItem Value="5">مقدار عددی</asp:ListItem>
                    <asp:ListItem Value="6">تاریخ</asp:ListItem>
                    <asp:ListItem Value="7">جدول</asp:ListItem>
                    <asp:ListItem Value="8" >چندگزینه ای چند جوابی و متنی</asp:ListItem>
			<asp:ListItem Value="9" >چندگزینه ای تک جوابی و متنی</asp:ListItem>
			<asp:ListItem Value="10" >چندگزینه ای آبشاری و متنی</asp:ListItem>
                </asp:RadioButtonList>
            <br />
                <asp:Label ID="Label6" runat="server"  Text="سوال اجباریست یا اختیاری"  ></asp:Label>
            
                <asp:RadioButtonList ID="rblStatus" runat="server">
                    <asp:ListItem Value="0"
                        Selected="True">اختیاری</asp:ListItem>
                    <asp:ListItem Value="1">اجباری</asp:ListItem>
                </asp:RadioButtonList>
            <br />
                <asp:Label ID="lblIsActv" runat="server" 
                    Text="سوال فعال است یا غیر فعال"></asp:Label>
                <asp:RadioButtonList ID="rblIsActv" runat="server">
                    <asp:ListItem Value="0">غیر فعال</asp:ListItem>
                    <asp:ListItem Value="1"
                        Selected="True">فعال</asp:ListItem>
                </asp:RadioButtonList>
            <br />
                <asp:Button ID="btnAddQue" runat="server"  
                    OnClick="btnAddQue_Click" Text="تعریف سوال" Width="150px"
                    ValidationGroup="1" />
            
</asp:Content>

