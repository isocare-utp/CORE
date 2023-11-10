<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mb_mbucfmembgroup.aspx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_mbucfmembgroup_ctrl.w_sheet_mb_mbucfmembgroup" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function OnDsMainClicked(s, r, c) {

            if (c == "b_search") {
                Gcoop.OpenIFrame3("630", "450", "w_dlg_sl_searchmembgroup.aspx", "");

            }
        }
        function OnDsMainItemChanged(s, r, c) {

            if (c == "membgroup_code") {
                PostMembGroup();

            } else if (c == "addr_province") {
                PostProvince();
            }
        }
        function GetIMembgroup(membgroup_code) {
            Gcoop.RemoveIFrame();
            Gcoop.GetEl("HdMembgroup").value = membgroup_code;
            dsMain.SetItem(0, "membgroup_code", membgroup_code);
            //            Gcoop.GetEl("HdSlipStatus").value = 1;
            PostMembGroup();
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <asp:HiddenField ID="HdMembgroup" runat="server" />
</asp:Content>
