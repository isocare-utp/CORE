<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_cancel_yearclosegl.aspx.cs"
    Inherits="Saving.Applications.account.w_acc_cancel_yearclosegl" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postCancelCloseYear %>
    <%=postNewClear%>
    <%=postSetDateStartEnd%>
    <script type="text/javascript">
        function OnDwMainItemChange(s, r, c, v) {
            if (c == "account_year_t") {
                objDw_main.SetItem(1, "account_year_t", v);
                objDw_main.AcceptText();
                postSetDateStartEnd();
            }
        }

        function Validate() {

        }

        function MenubarNew() {
            postNewClear();
        }

    function CancelCloseYear(){
        var account_year_t = objDw_main.GetItem(1, "account_year_t");
        var isconfirm = confirm("ยืนยันการยกเลิกปิดสิ้นปีบัญชีประจำ ปี : " + account_year_t);
        if (isconfirm){
            postCancelCloseYear();      
        }
        return 0;
    }   
    </script>

    <style type="text/css">
        .style2
        {
            width: 135px;
        }
        .style3
        {
            width: 426px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%; font-size: small;">
            <tr>
                <td colspan="3">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_acc_closeyear_current"
                        LibraryList="~/DataWindow/account/close_year.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientFormatting="True" ClientEventItemChanged="OnDwMainItemChange">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <input id="B_cancelclose" type="button" value="ยกเลิกปิดสิ้นปี" onclick="CancelCloseYear()" /><br />
                </td>
                <td class="style2">
                    <asp:HiddenField ID="HdIsFinished" runat="server" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
