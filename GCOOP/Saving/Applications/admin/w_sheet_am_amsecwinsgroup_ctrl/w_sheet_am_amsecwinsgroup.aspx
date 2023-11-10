<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_am_amsecwinsgroup.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_am_amsecwinsgroup_ctrl.w_sheet_am_amsecwinsgroup" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsDetail = new DataSourceTool();

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "application") {
                PostApplication();
            }
        }

        function OnDsDetailItemChanged(s, r, c, v) {
        }

        function OnDsDetailClicked(s, r, c) {
            if (c == "b_del") {
                dsDetail.SetRowFocus(r);
                PostDeleteRow();
            }
        }

        function OnClickInsertRow() {
            var passInsert = true;
            if (dsMain.GetItem(0, "application") == null || dsMain.GetItem(0, "application") == "") {
                alert("กรุณาเลือกระบบก่อน");
                return false;
            }
            for (var i = 0; i < dsDetail.GetRowCount(); i++) {
                if (dsDetail.GetItem(i, "group_code") == null || dsDetail.GetItem(i, "group_code") == "") {
                    alert("กระณากรอก รหัสกลุ่ม ก่อน");
                    return false;
                }
            }
            PostInsertRow();
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function SheetLoadComplete() {
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <span class="NewRowLink" onclick="OnClickInsertRow()">เพิ่มแถว</span>
    <uc2:DsDetail ID="dsDetail" runat="server" />
</asp:Content>
