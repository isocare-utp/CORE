<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_approve_chk.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_approve_chk" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล");
    }

    $(function () {
    // กำหนดว่าจะให้ reload เมื่อนับครบ 10 วิ
        setTimeout(function () { window.location = window.location }, 30000)
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="Dw_Main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_deptapprove_list_chk"
                    LibraryList="~/DataWindow/ap_deposit/dp_deptapprove.pbl" ClientEventButtonClicked="OnDwMainButtonClick"
                    ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
   
</asp:Content>
