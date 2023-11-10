<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_as_genrequest_education_to_promote.aspx.cs" Inherits="Saving.Applications.assist.ws_as_genrequest_education_to_promote_ctrl.ws_as_genrequest_education_to_promote" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_process") {
                var chk_assistcode = dsMain.GetItem(0, "assisttype_code");
                if (chk_assistcode == "00" || chk_assistcode == null) {
                    alert("กรุณาเลือก ประเภทสวัสดิการ!!!"); return;
                } else {
                    PostProcess();
                }
            } else if (c == "b_save") {
                if (confirm("ยืนยันการบันทึก")) {
                    PostSave();
                }
            }
        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "all_check") {
                for (var ii = 0; ii < dsList.GetRowCount(); ii++) {
                    dsList.SetItem(ii, "choose_flag", v);
                }
            }

            else if (c == "expense_bank") {
                var ls_expensebank = dsMain.GetItem(0, "expense_bank");
                dsMain.SetItem(0, "expense_bank", ls_expensebank);
                dsMain.SetItem(0, "expense_branch", "");
                PostBranch();
            }
            else if (c == "moneytype_code") {
                var ls_montype = dsMain.GetItem(0, "moneytype_code");
                if (ls_montype == "CSH") {
                    dsMain.SetItem(0, "expense_bank", "");
                    dsMain.SetItem(0, "expense_branch", "");
                    dsMain.GetElement(0, "expense_bank").disabled = true;
                    dsMain.GetElement(0, "expense_branch").disabled = true;
                    dsMain.GetElement(0, "expense_bank").style.background = "#CCCCCC";
                    dsMain.GetElement(0, "expense_branch").style.background = "#CCCCCC";
                } else if (ls_montype == "TRN") {
                    dsMain.SetItem(0, "expense_bank", "");
                    dsMain.SetItem(0, "expense_branch", "");
                    dsMain.GetElement(0, "expense_bank").disabled = true;
                    dsMain.GetElement(0, "expense_branch").disabled = true;
                    dsMain.GetElement(0, "expense_bank").style.background = "#CCCCCC";
                    dsMain.GetElement(0, "expense_branch").style.background = "#CCCCCC";

                } else {
                    dsMain.GetElement(0, "expense_bank").disabled = false;
                    dsMain.GetElement(0, "expense_branch").disabled = false;
                    dsMain.GetElement(0, "expense_bank").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "expense_branch").style.background = "#FFFFFF";
                    PostBank();
                }
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
   
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
    
</asp:Content>

