<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_close_month.aspx.cs"
    Inherits="Saving.Applications.account.w_sheet_close_month" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postCloseMonth %>
    <%=postNewClear %>
    <script type="text/javascript">
        function CloseMonth() {
            var account_tyear = objDw_main.GetItem(1, "account_tyear");
            var period = objDw_main.GetItem(1, "period");
            var acc_year = account_tyear - 543;

            var isconfirm = confirm("ยืนยันการปิดสิ้นเดือน ประจำงวด ที่ " + period + " ประจำปี พ.ศ." + account_tyear);
            if (isconfirm) {
                Gcoop.GetEl("Hd_year").value = acc_year + "";
                postCloseMonth();
            }
        }
        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdIsFinished").value == "true") {
                $('#B_closemonth').hide();
            }
        }

        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                postNewClear();
            }
        }
    
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width: 100%;">
            <tr>
                <td colspan="2">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_acc_closemonth_current"
                        LibraryList="~/DataWindow/account/close_month.pbl" ClientEventButtonClicked="OnDwmainClosemonthClick"
                        ClientEventItemChanged="OnDwmainItemChange">
                    </dw:WebDataWindowControl>
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dw:WebDataWindowControl ID="Dw_detail" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" Height="200px" Width="531px" ScrollBars="Auto" AutoSaveDataCacheAfterRetrieve="True"
                        ClientScriptable="True" DataWindowObject="d_acc_accsumledgerperiod_current" LibraryList="~/DataWindow/account/close_month.pbl">
                    </dw:WebDataWindowControl>
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <input id="B_closemonth" type="button" value="ปิดสิ้นเดือน" onclick="CloseMonth()" />
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="HdIsFinished" runat="server" />
                    <asp:HiddenField ID="Hd_year" runat="server" />
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
