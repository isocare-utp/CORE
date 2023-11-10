<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" CodeBehind="w_dlg_ass_deptaccountno_search_v2.aspx.cs" Inherits="Saving.Applications.assist.dlg.w_dlg_ass_deptaccountno_search_v2_ctrl.w_dlg_ass_deptaccountno_search_v2" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();

        function PostSubmit() {
                        
            var bank_code = dsList.GetItem(0, "bank_code");
            var branch_code = dsList.GetItem(0, "branch_code");
            var expense_accid = dsList.GetItem(0, "expense_accid");
            var pay_name = dsList.GetItem(0, "pay_name");
            var tofrom_accid = dsList.GetItem(0, "tofrom_accid");
            var cashtype = dsList.GetItem(0, "cashtype");
            if (cashtype == "CHQ" || cashtype == "DRF" || cashtype == "BEX" || cashtype == "CBT" || cashtype == "CBO" || cashtype == "TBK") {
                if (bank_code == null) {
                    alert("กรุณาเลือกธนาคาร!!!"); return;
                }
                if (cashtype == "CBT" || cashtype == "CBO" || cashtype == "TBK") {
                    if (branch_code == null) {
                        alert("กรุณาเลือกสาขา!!!"); return;
                    } else if (expense_accid == null) {
                        alert("กรุณากรอกเลขธนาคาร!!!"); return;          
                    }
                } else if (cashtype == "CHQ" || cashtype == "DRF" || cashtype == "BEX") {
                    if (pay_name == null) {
                        alert("กรุณากรอกสั่งจ่ายในนาม!!!"); return;                    
                    }
                }
                /*if (tofrom_accid == null) {
                    alert("กรุณาเลือกรหัสบัญชี!!!"); return;
                }*/
            }
            if (cashtype == "CSH" || cashtype == "TRN" || cashtype == "TDF") {
                /*if (tofrom_accid == null) {
                    alert("กรุณาเลือกรหัสบัญชี!!!"); return;
                }*/
            }

            if (bank_code == null) {
                bank_code = "";
            }
            if (branch_code == null) {
                branch_code = "";
            }
            if (expense_accid == null) {
                expense_accid = "";
            }
            if (pay_name == null) {
                pay_name = "";
            }
            if (tofrom_accid == null) {
                tofrom_accid = "";
            }

            try {
                window.opener.GetValueFromDlg(bank_code, branch_code, expense_accid, pay_name, tofrom_accid);
                window.close();
            } catch (err) {
                parent.GetDeptNoFromDlg(deptno);
                parent.RemoveIFrame();
            }
        }
        function PostCancel() {
            window.close();
        }
        function OnDsListItemChanged(s, r, c, v) {
            if (c == "bank_code") {                
               PostBranch();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>   
   
    <uc1:DsList ID="dsList" runat="server" />
    <input type="button" value="ตกลง" id="btnSubmit" onclick="PostSubmit()" style="margin-left: 230px;" />
    <input type="button" value="ยกเลิก" onclick="PostCancel()" style="margin-left: 2px;" />
    <asp:HiddenField ID="HdPayoutNo" runat="server" />
    <asp:HiddenField ID="hd_row" runat="server" />
    <asp:HiddenField ID="HdCash" runat="server" />
</asp:Content>