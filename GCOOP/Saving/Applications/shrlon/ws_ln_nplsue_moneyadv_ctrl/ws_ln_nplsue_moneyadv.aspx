<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_ln_nplsue_moneyadv.aspx.cs" Inherits="Saving.Applications.shrlon.ws_ln_nplsue_moneyadv_ctrl.ws_ln_nplsue_moneyadv" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsMain = new DataSourceTool;        
        var dsDetail = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame("585", "540", "w_dlg_sl_member_search_lite.aspx", "")
        }
        function OnDsMainClicked(s, r, c) {
            if (c == "b_memsearch") {
                Gcoop.OpenIFrame("585", "540", "w_dlg_sl_member_search_lite.aspx", "")
            }
        }
        function GetValueFromDlg(member_no) {
            dsMain.SetItem(0, "member_no", member_no);
            PostMemberNo();
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc2:DsDetail ID="dsDetail" runat="server" />
</asp:Content>
