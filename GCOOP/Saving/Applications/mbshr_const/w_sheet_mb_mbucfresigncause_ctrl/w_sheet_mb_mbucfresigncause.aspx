<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mb_mbucfresigncause.aspx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_mbucfresigncause_ctrl.w_sheet_mb_mbucfresigncause" %>

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
            if (c == "resigncause_code") {
                var ls_row = dsList.GetRowCount() - 1;
                var ls_resigncause = dsList.GetItem(r, "resigncause_code");
                for (var i = 0; i < ls_row; i++) {
                    var ls_resigncause_code = dsList.GetItem(i, "resigncause_code");
                    if (ls_resigncause == ls_resigncause_code) {
                        alert("รหัสสาเหตุการลาออกซ้ำ");
                        dsList.SetItem(r, "resigncause_code", "");
                        break;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div style="text-align: right;">
        <span class="NewRowLink" onclick="PostInsertRow()" style="padding-left: 30px;">เพิ่มแถว</span>
    </div>
    <uc1:dsList ID="dsList" runat="server" />
</asp:Content>
