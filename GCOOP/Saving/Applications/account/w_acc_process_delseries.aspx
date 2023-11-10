<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_process_delseries.aspx.cs" Inherits="Saving.Applications.account.w_acc_process_delseries" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostProcess%>
    <script type="text/javascript">
        function OnProcessClickButton(sender, row, bName) {
            var end_tdate = objDwmain.GetItem(row, "end_tdate");

            if (end_tdate == null) {
                confirm("กรุณาระบุวันที่");
            }
            else {
                var end_tdate_lange = end_tdate.trim().length;
                if (end_tdate_lange != 8) {
                    confirm("กรุณาตรวจสอบรูปแบบของวันที่");
                }
                else {
                    jsPostProcess();
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="2">
                <dw:WebDataWindowControl ID="Dwmain" runat="server" DataWindowObject="d_acc_daterange_process_asset"
                    LibraryList="~/DataWindow/account/asset.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="OnProcessClickButton"
                    ClientScriptable="True" ClientEventItemChanged="OnDWDateItemChange" ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="Radio1" runat="server" AutoPostBack="True" OnCheckedChanged="Radio1_CheckChanged"
                    Text="รายการสินทรัพย์" Checked="True" Font-Size="Medium" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <%-- <asp:RadioButton ID="Radio2" runat="server" AutoPostBack="True" OnCheckedChanged="Radio2_CheckChanged"
                    Text="รายการสินทรัพย์ไม่มีตัวตน" Font-Size="Medium" />--%>
                <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" Height="400px" Width="640px">
                    <dw:WebDataWindowControl ID="Dwlist" runat="server" DataWindowObject="d_acc_list_process_asset_3"
                        LibraryList="~/DataWindow/account/asset.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                        Height="400px" Width="640px" ClientEventButtonClicked="OnEditClickButton">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="Hdyear" runat="server" />
    <asp:HiddenField ID="Hdperiod" runat="server" />
</asp:Content>
