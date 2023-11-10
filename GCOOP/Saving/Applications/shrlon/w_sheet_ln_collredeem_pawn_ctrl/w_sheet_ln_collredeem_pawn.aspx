<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_ln_collredeem_pawn.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_collredeem_pawn_ctrl.w_sheet_ln_collredeem_pawn"
    ValidateRequest="false" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function OnDsMainItemChanged(s, r, c, v) {

            if (c == "mrtgmast_no") {
                UpdateMortgage();
            }
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_sh_pawn") {
                Gcoop.OpenDlg2('680', '690', 'w_dlg_sl_redeem_search.aspx', '');
            }
        }

        function MenubarOpen() {
            Gcoop.OpenDlg2('580', '590', 'w_dlg_loan_collredeem.aspx', '');
        }

        function GetValueFromDlg(mrtgmast_no, member_no, memb_name, memb_surname, mortgage_date, mortgagesum_amt) {
            Gcoop.RemoveIFrame();
            dsMain.SetItem(0, "mrtgmast_no", mrtgmast_no);
            dsMain.SetItem(0, "member_no", member_no);
            dsMain.SetItem(0, "full_name", memb_name + " " + memb_surname);
            dsMain.SetItem(0, "mortgage_date", mortgage_date);
            dsMain.SetItem(0, "mortgagesum_amt", mortgagesum_amt);

        }

        function Validate() {
            return confirm("ยืนยันการไถ่ถอนจำนอง");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
</asp:Content>
