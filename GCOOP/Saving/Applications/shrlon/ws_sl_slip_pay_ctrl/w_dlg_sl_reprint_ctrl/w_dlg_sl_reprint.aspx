<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_sl_reprint.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_slip_pay_ctrl.w_dlg_sl_reprint_ctrl.w_dlg_sl_reprint" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;

        function Validate() {
        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "membgroup_desc") {
                PostMembgroup();
            }
        }
        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                PostSearch();
            } else if (c == "b_cancel") {

                dsMain.SetRowFocus(r);

                dsMain.SetItem(r, "member_no", "");
                dsMain.SetItem(r, "payinslip_no", "");
                dsMain.SetItem(r, "memb_name", "");
                dsMain.SetItem(r, "memb_surname", "");
                dsMain.SetItem(r, "membgroup_code", "");
                dsMain.SetItem(r, "membgroup_desc", "");
                dsMain.SetItem(r, "slip_date_s", "");
                dsMain.SetItem(r, "slip_date_e", "");



            }
        }
        function OnClickAll() {
            var allrow = dsList.GetRowCount();
            for (var i = 0; i <= allrow; i++) {
                dsList.SetItem(i, "checkselect", 1);
            }
        }

        function OnUnClickAll() {
            var allrow = dsList.GetRowCount();
            for (var i = 0; i <= allrow; i++) {
                dsList.SetItem(i, "checkselect", 0);
            }
        }

        function OnClickPrint() {
            PostPrint();
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <br />
    <div align="center">
        <uc1:DsMain ID="dsMain" runat="server" />
        <br />
        <div align="left">
            <input type="button" value="เลือกทั้งหมด" style="width: 70px; margin-left: 115px;"
                onclick="OnClickAll()" />
            <input type="button" value="ไม่เลือกทั้งหมด" style="width: 80px" onclick="OnUnClickAll()" />
            <input type="button" value="พิมพ์" style="width: 80px" onclick="OnClickPrint()" />
        </div>
        <uc2:DsList ID="dsList" runat="server" />
    </div>
</asp:Content>
