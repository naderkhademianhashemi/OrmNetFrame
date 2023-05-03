<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="FllEdit_Answers.aspx.cs" 
    Inherits="Edit_Answers" Title="پرسشنامه" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="/JavaScript/Edit_AnswersFnctn.js"></script>

    <div style ="text-align:right ;">
        <br />
    <asp:Label ID="lblFname" runat="server"   
        ></asp:Label>
        <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>
    <asp:Panel ID="Panel1" runat="server" >
    </asp:Panel>
        <asp:Label ID="ValidityText" runat="server"></asp:Label>
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="lblType" runat="server"  Visible="False"></asp:Label><asp:Label
        ID="Label2" runat="server" Text="" Visible="False"></asp:Label>
        <br />
    <asp:Button ID="Button1" runat="server"   OnClick="Button1_Click1"
         Text="ويرايش پاسخ"  />
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" 
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
             ForbidenWeekDays="6" FrameCSS="PickerCSS"
            HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS"
            WorkDayCSS="PickerWorkDayCSS"></pdc:PersianDateScriptManager>

</div>
</asp:Content>

