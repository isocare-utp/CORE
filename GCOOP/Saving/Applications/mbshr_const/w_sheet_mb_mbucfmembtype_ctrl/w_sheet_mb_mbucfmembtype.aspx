<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mb_mbucfmembtype.aspx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_mbucfmembtype_ctrl.w_sheet_mb_mbucfmembtype" %>

<%@ Register Src="DsList.ascx" TagName="dsList" TagPrefix="uc1" %>
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
            if (c == "membtype_code") {
                var ls_row = dsList.GetRowCount() - 1;
                var ls_membtype = dsList.GetItem(r, "membtype_code");
                for (var i = 0; i < ls_row; i++) {
                    var ls_membtype_code = dsList.GetItem(i, "membtype_code");
                    if (ls_membtype == ls_membtype_code) {
                        alert("รหัสประเภทสมาชิกซ้ำ");
                        dsList.SetItem(r, "membtype_code", "");
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
        <uc1:dsList ID="dsList" runat="server" />
    </div>
</asp:Content>
