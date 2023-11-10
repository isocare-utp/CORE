<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_sl_search_slip.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_slip_pay_ctrl.w_dlg_sl_search_slip_ctrl.w_dlg_sl_search_slip" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;

        function Validate() {
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostSearch();
            }
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                PostSearch();
            } else if (c == "b_cancel") {

                dsMain.SetRowFocus(r);

                dsMain.SetItem(r, "member_no", "");
                dsMain.SetItem(r, "payinslip_no", "");
                dsMain.SetItem(r, "memb_name", "");
                dsMain.SetItem(r, "memb_surname", "");
                dsMain.SetItem(r, "membgroup_code", "");
                dsMain.SetItem(r, "membgroup_desc", "");
                dsMain.SetItem(r, "slip_date_s", "");
                dsMain.SetItem(r, "slip_date_e", "");
            }
        }

        function OnDsListClicked(s, r, c) {
            if (c == "document_no" || c == "slip_date" || c == "member_no" || c == "compute1" || c == "membgroup_code" || c == "sliptype_code" || c == "slip_status") {
                dsList.SetRowFocus(r);
                var payinslip_no = dsList.GetItem(r, "payinslip_no");
                try {
                    window.opener.GetItemLoan(payinslip_no);
                    window.close();
                } catch (err) {
                    parent.GetItemLoan(payinslip_no);
                    window.close();
                }
                //PostRetive();
            }
        }    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <div align="center">
        <uc1:DsMain ID="dsMain" runat="server" />
        <br />
        <uc2:DsList ID="dsList" runat="server" />
    </div>
</asp:Content>
