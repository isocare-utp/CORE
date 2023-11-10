<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mb_mbucfposition.aspx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_mbucfposition_ctrl.w_sheet_mb_mbucfposition" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r); //ให้รู้ว่ากดแถวไหนในลิส
                PostDeleteRow(); //PostDeleteRow(); ไว้ลบแถวที่เลือก
            }
        }
        function OnListItemChanged(s, r, c, v) {
            if (c == "position_code") {
                var ls_row = dsList.GetRowCount() - 1;
                var ls_position = dsList.GetItem(r, "position_code");
                for (var i = 0; i < ls_row; i++) {
                    var ls_position_code = dsList.GetItem(i, "position_code");
                    if (ls_position == ls_position_code) {
                        alert("รหัสตำแหน่งซ้ำ");
                        dsList.SetItem(r, "position_code", "");
                        break;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div align="right" style="margin-right: 1px; width: 720px;">
        <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span>
        <uc1:DsList ID="dsList" runat="server" />
    </div>
</asp:Content>
