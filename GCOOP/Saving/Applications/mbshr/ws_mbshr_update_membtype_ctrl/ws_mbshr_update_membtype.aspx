<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_mbshr_update_membtype.aspx.cs" 
Inherits="Saving.Applications.mbshr.ws_mbshr_update_membtype_ctrl.ws_mbshr_update_membtype" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var dsMain = new DataSourceTool;

    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล");
    }

    function OnDsMainItemChanged(s, r, c, v) {
        if (c == "member_no") {
            PostMember();
        }
    }

    function OnDsMainClicked(s, r, c) {
        if (c == "b_search") {
            Gcoop.OpenIFrame2("630", "610", "w_dlg_sl_member_search_lite.aspx", "");
        }
    }

    function GetValueFromDlg(memberno) {
        dsMain.SetItem(0, "member_no", memberno);
        PostMember();
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
