<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mb_mbucfprename.aspx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_mbucfprename_ctrl.w_sheet_mb_mbucfprename" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_del") {
                dsMain.SetRowFocus(r);
                PostDeleteRow();
            }
        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "prename_code") {
                var ls_row = dsMain.GetRowCount() - 1;
                var ls_prename = dsMain.GetItem(r, "prename_code");
                //alert(ls_prename);
                for (var i = 0; i < ls_row; i++) {
                    var ls_prename_code = dsMain.GetItem(i, "prename_code");
                    // alert(ls_prename_code);
                    if (ls_prename == ls_prename_code) {
                        alert("รหัสคำหน้านามซ้ำ");
                        dsMain.SetItem(r, "prename_code", "");
                        break;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <span style="text-decoration: underline; font-size: 13px; font-weight: bold;">รายการคำนำหน้าชื่อ</span>
    <div style="text-align: right;">
        <span class="NewRowLink" onclick="PostInsertRow()" style="padding-left: 30px;">เพิ่มแถว</span>
    </div>
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
