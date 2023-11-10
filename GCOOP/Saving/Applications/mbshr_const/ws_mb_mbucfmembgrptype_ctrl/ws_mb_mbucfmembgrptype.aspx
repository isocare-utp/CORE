<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_mb_mbucfmembgrptype.aspx.cs" Inherits="Saving.Applications.mbshr_const.ws_mb_mbucfmembgrptype_ctrl.ws_mb_mbucfmembgrptype" %>
<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var dsList = new DataSourceTool;

    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล")
    }

    function OnDsListClicked(s, r, c) {
        if (c == "b_del") {
            dsList.SetRowFocus(r);
            if (confirm("ลบข้อมูลแถวที่ " + (r+1) + " ?") == true) {

                PostDelRow();
            }


        }
    }

    function OnDsListItemChanged(s, r, c, v) {
        if (c == "membgrptype_code") {
            for (var i = 0; i < dsList.GetRowCount() - 1; i++) {
                var membgrptype_code = dsList.GetItem(r, "membgrptype_code");
                var membgrptype_code_1 = dsList.GetItem(i, "membgrptype_code");
                if (membgrptype_code == membgrptype_code_1) {
                    alert("รหัสซ้ำ");
                    dsList.SetItem(r, "membgrptype_code", "");
                    break;
                }
            }
        }
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal><br />
    <span class="NewRowLink" onclick="PostInsertRow()" style="font-size: small;">เพิ่มแถว
    </span>
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>
