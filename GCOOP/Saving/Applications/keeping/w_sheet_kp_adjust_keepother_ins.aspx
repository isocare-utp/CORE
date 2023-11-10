<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_kp_adjust_keepother_ins.aspx.cs" 
Inherits="Saving.Applications.keeping.w_sheet_kp_adjust_keepother_ins" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=jsPostRetrieveIns%>
<script type="text/javascript">

    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล?");
    }

    function OnDwMainClick(s, r, c) {
        if (c == "b_retrieve") {
            jsPostRetrieveIns();
        }
    }

    function OnDwMainItemChange(s, r, c, v) {
        s.SetItem(r, c, v);
        s.AcceptText();

    }

    function OnDwDetailClick(s, r, c) {

    }

    function OnDwDetailItemChange(s, r, c, v) {
        s.SetItem(r, c, v);
        s.AcceptText();

    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_kp_keepother_ins_main"
        LibraryList="~/DataWindow/keeping/kp_opr_receive_store_other.pbl" ClientScriptable="True"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" TabIndex="1" Style="top: 0px; left: 0px" ClientEventItemChanged="OnDwMainItemChange"
        ClientEventButtonClicked="OnDwMainClick">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_kp_keepother_ins_det"
        LibraryList="~/DataWindow/keeping/kp_opr_receive_store_other.pbl" ClientScriptable="True"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" TabIndex="1" Style="top: 0px; left: 0px" ClientEventItemChanged="OnDwDetailItemChange"
        ClientEventButtonClicked="OnDwDetailClick">
    </dw:WebDataWindowControl>
</asp:Content>
