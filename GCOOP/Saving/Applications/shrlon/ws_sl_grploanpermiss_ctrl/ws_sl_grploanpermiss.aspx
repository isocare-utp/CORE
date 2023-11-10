<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_grploanpermiss.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_grploanpermiss_ctrl.ws_sl_grploanpermiss" %>

<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsDetail = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsDetailClicked(s, r, c) {
            if (c == "b_del") {
                dsDetail.SetRowFocus(r);
                if (confirm("ลบข้อมูลแถวที่ " + (r + 1) + " ?") == true) {

                    PostDelRow();
                }


            }
        }

        function OnDsDetailItemChanged(s, r, c, v) {
            if (c == "loanpermgrp_code") {
                for (var i = 0; i < dsDetail.GetRowCount()-1; i++) {
                    var loanpermgrp_code = dsDetail.GetItem(r, "loanpermgrp_code");
                    var loanpermgrp_code_1 = dsDetail.GetItem(i, "loanpermgrp_code");
                    if (loanpermgrp_code == loanpermgrp_code_1) {
                        alert("รหัสซ้ำ");
                        dsDetail.SetItem(r, "loanpermgrp_code", "");
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
    <uc1:DsDetail ID="dsDetail" runat="server" />
</asp:Content>
