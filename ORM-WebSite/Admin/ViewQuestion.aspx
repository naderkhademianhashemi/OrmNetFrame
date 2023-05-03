<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewQuestion.aspx.cs" Inherits="ViewQuestion" Title="پرسشنامه" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="/JavaScript/ViewQuestionFnctn.js"></script>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:Label ID="lblFname" runat="server"></asp:Label>
    <br />
    <span>Panel1</span>
    <asp:Panel ID="Panel1" runat="server">
    </asp:Panel>


    <asp:Label ID="ValidityText" runat="server"></asp:Label>
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="lblType" runat="server" Visible="False"></asp:Label><asp:Label
        ID="Label2" runat="server" Text="" Visible="False"></asp:Label>
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1"
        Text="ثبت اطلاعات" />
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        ForbidenWeekDays="6" FrameCSS="PickerCSS"
        HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS"
        WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>

</asp:Content>

