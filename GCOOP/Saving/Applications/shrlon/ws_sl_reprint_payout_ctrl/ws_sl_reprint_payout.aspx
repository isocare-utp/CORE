<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_sl_reprint_payout.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_reprint_payout_ctrl.ws_sl_reprint_payout" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>
<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_retrieve") {
                PostRetrieve();
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
        function Setfocus() {
            dsMain.Focus(0, "member_no");
        }

        function OnClickPrint() {
            PostPrint();
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <input type="button" value="เลือกทั้งหมด" style="width: 70px" onclick="OnClickAll()" />
    <input type="button" value="ไม่เลือกทั้งหมด" style="width: 80px" onclick="OnUnClickAll()" />
    <input type="button" value="พิมพ์" style="width: 80px" onclick="OnClickPrint()" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
