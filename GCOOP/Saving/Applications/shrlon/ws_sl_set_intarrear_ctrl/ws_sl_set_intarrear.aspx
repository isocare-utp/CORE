<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_set_intarrear.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_set_intarrear_ctrl.ws_sl_set_intarrear" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            } else if (c == "loancontract_no") {
                PostLoanContractNo();
            } else if (c == "intarrset_caldate") {
                PostCalIntDate();
            }
        }

        function ReceiveMemberNo(member_no, memb_name) {
            dsMain.SetItem(0, "member_no", member_no);
            PostMemberNo();
        }

        function PostLoancontractNo(member_no, loancontract_no) {
            dsMain.SetItem(0, "loancontract_no", loancontract_no);
            PostLoanContractNo();
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_memsearch") {
                Gcoop.OpenIFrame2("630", "720", "w_dlg_sl_member_search_tks.aspx", "")
            } else if (c == "b_contsearch") {
                var member_no = dsMain.GetItem(0, "member_no");
                Gcoop.OpenIFrame('630', '600', 'w_dlg_sl_loancontract_search_memno.aspx', "?memno=" + member_no);
            }
        }

        function GetValueFromDlg(member_no) {
            dsMain.SetItem(0, "member_no", member_no);
            PostMemberNo();
        }

        function GetContFromDlg(loancontract_no) {
            dsMain.SetItem(0, "loancontract_no", loancontract_no);
            PostLoanContractNo();
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
