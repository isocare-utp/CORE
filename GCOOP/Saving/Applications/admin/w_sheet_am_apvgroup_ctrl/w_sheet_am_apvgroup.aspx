<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_am_apvgroup.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_am_apvgroup_ctrl.w_sheet_am_apvgroup" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function SheetLoadComplete() {
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "apvlevel_id") {
                PostApvDd();
            }
        }
        function OnDsMainClicked(s, r, c) {
            if (c == "b_del") {
                if (confirm("ยืนยันการลบข้อมูล")) {
                    dsMain.SetRowFocus(r);
                    PostDelete();
                }

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span> &nbsp; &nbsp;
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
