<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_create_file_tobank.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_create_file_tobank" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<%=buttonfunction%>
<script type="text/javascript">
    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล");
    }

    function OnDwMainButtonClick(sender, rowNumber, buttonName) {
        if (buttonName == "b_1") {
           // buttonfunction();
            Gcoop.OpenIFrame("200", "200", "w_dlg_dp_create_file_tobank.aspx");
        }
    }


</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="Dw_Main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_create_file_main"
                    LibraryList="~/DataWindow/ap_deposit/create_files_tobank.pbl" ClientEventButtonClicked="OnDwMainButtonClick"
                    ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
   
</asp:Content>
