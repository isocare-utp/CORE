<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_dp_criteria_date_midtype_range.ascx.cs"
    Inherits="Saving.Applications.ap_deposit.report.u_dp_criteria_date_midtype_range" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<script type="text/javascript">
</script>
<dw:WebDataWindowControl ID="dw_data" runat="server" DataWindowObject="d_dp_criteria_date_midtype_range"
    LibraryList="~/DataWindow/ap_deposit/dpdeptrpt_obj.pbl" 
        Style="text-align: center" AutoRestoreContext="False" 
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
        ClientScriptable="True">
</dw:WebDataWindowControl>

<script type="text/javascript">

</script>
<br />
<center>
    <asp:HiddenField ID="Hf_cmd" runat="server" />
    <table style="width: 320px;" border="0">
        <tr>
            <td align="center" style="width: 50%;">
                <asp:Button ID="b_cancel" runat="server" Text="ยกเลิก" />
            </td>
            <td align="center">
                <asp:Button ID="b_ok" runat="server" Text="ตกลง" OnClick="getDataForReport" />
            </td>
            
        </tr>
    </table>
    <asp:Panel ID="PnReport" runat="server">
    
    <dw:WebDataWindowControl ID="dw_report" runat="server" 
        DataWindowObject="rpt01044_dp_open_account_us" 
        LibraryList="~/DataWindow/ap_deposit/dpdeptrpt_obj.pbl" 
            AutoRestoreContext="False" AutoRestoreDataCache="True" 
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" Height="600px">
    </dw:WebDataWindowControl>
    </asp:Panel>
</center>
