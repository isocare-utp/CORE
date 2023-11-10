<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_dep_ucf_dptofromaccid.aspx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_ucf_dptofromaccid_ctrl.ws_dep_ucf_dptofromaccid" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsList = new DataSourceTool;
        var dsMain = new DataSourceTool;

        function Validate() {
            var alertstr = "";
            var row = dsList.GetRowCount();

           
            for (var n = 0; n < row; n++) {

                
                var cash_type = dsList.GetItem(n, "cash_type");
                if (cash_type == null) {
                    alertstr += "- กรุณาเลือก ประเภทเงิน\n";
                }
                var account_desc = dsList.GetItem(n, "account_desc");
                if (account_desc == null) {
                    alertstr += "- กรุณากรอก ชื่อคู่บัญชี\n";
                }
                var account_id = dsList.GetItem(n, "account_id");
                if (account_id == null) {
                    alertstr += "- กรุณาเลือก เลขคู่บัญชี\n";
                }

                /* var moneytype_code = dsList.GetItem(0, "moneytype_code");
                if (moneytype_code == null) {
                alertstr += "- กรุณากรอกประเภทเงินฝาก\n";
                }
                var bank_accno = dsList.GetItem(0, "bank_accno");
                if (bank_accno == null) {
                alertstr += "- กรุณากรอกบัญชีธนาคาร\n";
                }
                var bank_code = dsList.GetItem(0, "bank_code");
                if (bank_code == null) {
                alertstr += "- กรุณากรอกรหัสธนาคาร\n";
                }
           
                */
            }
            if (alertstr == "") {
                return confirm("ยืนยันการบันทึกข้อมูล");
            } else {
                alert(alertstr);
                return false;
            }
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "moneytype_code") {
                PostMoneytypeCode();
            } 
        }

        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r);
                var currentRow = r;
                currentRow = parseInt(currentRow) + 1;
                var confirmText = "ยืนยันการลบแถวที่ " + currentRow;                
                if (confirm(confirmText)) {
                    PostDeleteRow();
                }                
            }

        }
        function OnDsListItemChanged(s, r, c,v) { 
            if (c == "account_id") {
                for (var i = 0; i < dsList.GetRowCount(); i++) {
                    if (i != r) {
                        if (dsList.GetItem(i, "account_id") == v) {
                            dsList.SetItem(r, "account_id", "");
                            alert("รหัสรายการดังกล่าวมีการเพิ่มแล้ว"); return;
                        }
                    }
                }
            }
        }
        
        function SheetLoadComplete() {


        }
        function OnClickNewRow() {         
            var moneytype_code = dsMain.GetItem(0, "moneytype_code");
            if (moneytype_code != null) {
                PostInsertRow();
            } else {
                alert("กรุณาเลือกประเภทเงินก่อนเพิ่มแถว");
                return false;
            }  
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br/>
    <span class="NewRowLink" onclick="OnClickNewRow()">เพิ่มแถว</span>
    <uc2:DsList ID="dsList" runat="server"/>
</asp:Content>
