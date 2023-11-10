<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_am_amsecwins.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_am_amsecwins" %>

<%@ Register Src="DsGroup.ascx" TagName="DsGroup" TagPrefix="uc1" %>
<%@ Register Src="DsWins.ascx" TagName="DsWins" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsGroup = new DataSourceTool();
        var dsWins = new DataSourceTool();

        function OnDsGroupItemChanged(s, r, c, v) {
            if (c == "application") {
                PostApplication();
            } else if (c == "group_code") {
                PostGroupCode();
            }
        }

        function OnDsWinsItemChanged(s, r, c, v) {
            if (c == "group_code") {
            } else if (c == "used_flag") {
            }
        }

        function OnDsWinsClicked(s, r, c) {
            if (c == "b_del") {
                dsWins.SetRowFocus(r);
                PostDeleteRow();
            }
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame3(400, 300, "w_dlg_ad_test.aspx", "");
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function SheetLoadComplete() {
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsGroup ID="dsGroup" runat="server" />
    <br />
    <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span> &nbsp; &nbsp;
    <asp:CheckBox CssClass="CheckBoxLine" ID="CbReAdmin" runat="server" Text="ปรับสิทธิ์ admin"
        Checked="True" />
    <uc2:DsWins ID="dsWins" runat="server" />
</asp:Content>
