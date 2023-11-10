<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="wd_as_expense_detail.aspx.cs" Inherits="Saving.Applications.assist.dlg.wd_as_expense_detail_ctrl.wd_as_expense_detail" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();

        function PostSubmit() {

            var bank_code = dsList.GetItem(0, "bank_code");
            var branch_code = dsList.GetItem(0, "branch_code");
            var expense_accid = dsList.GetItem(0, "expense_accid");
            var pay_name = "";// dsList.GetItem(0, "pay_name");
            var tofrom_accid = dsList.GetItem(0, "tofrom_accid");
            var cashtype = dsList.GetItem(0, "cashtype");
            var exp_clr = ""; // dsList.GetItem(0, "expense_clearing");
            var deptacc_no = dsList.GetItem(0, "deptaccount_no");

            if (cashtype == "CHQ" || cashtype == "CBT") {
                if (bank_code == null) {
                    alert("กรุณาเลือกธนาคาร!!!"); return;
                } else if (branch_code == null) {
                    alert("กรุณาเลือกสาขา!!!"); return;
                } else if (expense_accid == null) {
                    alert("กรุณากรอกเลขธนาคาร!!!"); return;
                } else if (tofrom_accid == null) {
                    alert("กรุณาเลือกรหัสบัญชี!!!"); return;
                }
            }

            if (cashtype == "CSH" || cashtype == "TRN") {
                if (tofrom_accid == null) {
                    alert("กรุณาเลือกรหัสบัญชี!!!"); return;
                }
            }
            try {
                window.opener.GetValueFromDlg(bank_code, branch_code, expense_accid, pay_name, tofrom_accid, exp_clr , deptacc_no);
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
            else if (c == "cashtype") {
                PostCashtype();
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
    <asp:HiddenField ID="HdCash" runat="server" />
</asp:Content>
