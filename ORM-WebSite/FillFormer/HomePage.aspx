<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" Title="پرسشنامه" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language= "javascript" type="text/javascript">

function roll_over(img_name, img_src)
   {
   document[img_name].src = img_src;
   }

</script>

    <table  >
        <tr>
            <td >
                </td>
            <td >
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td >
                <p >
                    مشاهده فرمهای کاربری</td>
            <td >
                <asp:Panel ID="Panel2" runat="server" ToolTip="مشاهده فرمهای پرسشنامه">
                    <a href="Forms.aspx" 
                        onmouseout="roll_over('but1', 'images/Disable/Documents Search.png')" 
                        onmouseover="roll_over('but1', 'images/Regular/Documents Search.png')">
                    <img alt="" name="but1" src="images/Disable/Documents Search.png" /> </a>
                </asp:Panel>
                </p>
            </td>
        </tr>
        <tr>
            <td >
                </td>
            <td >
                <br />
                <br />
            </td>
        </tr>
        </table>
</asp:Content>

