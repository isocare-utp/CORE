<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_loan_search_receive.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_loan_receive_list_ctrl.w_dlg_loan_search_receive_ctrl.w_dlg_loan_search_receive" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function Validate() {
        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "membgroup_desc") {
                PostMembgroup();
            } else if (c == "loantype_desc") {
                PostLoanType();
            }
        }
        function OnDsMainClicked(s, r, c) {

            if (c == "b_search") {
                PostSearch();
                //parent.GetItem(sqlext_con, sqlext_req);
            } else if (c == "b_cancel") {
               
                dsMain.SetRowFocus(r);
               
                dsMain.SetItem(r, "loancontract_no", "");
                dsMain.SetItem(r, "member_no", "");
                dsMain.SetItem(r, "memb_name", "");
                dsMain.SetItem(r, "memb_surname", "");
                dsMain.SetItem(r, "membgroup_code", "");
                dsMain.SetItem(r, "membgroup_desc", "");
                dsMain.SetItem(r, "loantype_code", "");
                dsMain.SetItem(r, "loantype_desc", "");
                dsMain.SetItem(r, "approve_date_s", "");
                dsMain.SetItem(r, "approve_date_e", "");
                dsMain.SetItem(r, "approve_id", "");

                
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <div align="center">
        <uc1:DsMain ID="dsMain" runat="server" />
    </div>
</asp:Content>
