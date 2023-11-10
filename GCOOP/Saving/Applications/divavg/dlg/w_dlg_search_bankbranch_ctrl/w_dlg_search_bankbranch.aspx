<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_search_bankbranch.aspx.cs" Inherits="Saving.Applications.divavg.dlg.w_dlg_search_bankbranch_ctrl.w_dlg_search_bankbranch" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "bank_code") {
                PostBank();
            }
        }

        function OnDsMainClicked(s, r, c) {            
            if (c == "b_search") {
                PostBranchName();
            }
            else if (c == "b_submit") {                
                var expense_bank = dsMain.GetItem(0, "bank_code");                
                var expense_branch = dsMain.GetItem(0, "branch_id");
                var expense_accid = dsMain.GetItem(0, "expense_accid");      
                          
                parent.SetExpense(expense_bank, expense_branch, expense_accid);
                parent.RemoveIFrame();
            }
        }

        function OnDsListClicked(s, r, c) {
            if (c == "branch_id" || c == "branch_name") {
                var branch_id = dsList.GetItem(r, "branch_id");
                dsMain.SetItem(0, "branch_id", branch_id);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
