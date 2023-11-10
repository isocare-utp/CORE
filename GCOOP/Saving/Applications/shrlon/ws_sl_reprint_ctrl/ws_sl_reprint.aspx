<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_reprint.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_reprint"
    ValidateRequest="true" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
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

        function OnClickPrint() {
            PostPrint();
        }
        function OnClickPrint2() {
            PostPrint2();
        }
        function Setfocus() {
            dsMain.Focus(0, "member_no");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <input type="button" value="เลือกทั้งหมด" style="width: 70px" onclick="OnClickAll()" />
    <input type="button" value="ไม่เลือกทั้งหมด" style="width: 80px" onclick="OnUnClickAll()" />
    <input type="button" value="พิมพ์" style="width: 80px" onclick="OnClickPrint()" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
