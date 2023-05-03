
<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewQuestionResult.aspx.cs" 
    Inherits="ViewQuestionResult" Title="پرسشنامه" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:Label ID="Label1" runat="server"  ForeColor="#990000" 
            Text="متشکریم. پاسخهای شما ثبت شد."></asp:Label>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="ثبت مجدد" />
</asp:Content>