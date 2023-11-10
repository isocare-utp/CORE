<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_mbshr_trnmb_search.aspx.cs" Inherits="Saving.Applications.mbshr.dlg.w_dlg_mbshr_trnmb_search_ctrl.w_dlg_mbshr_trnmb_search" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();


        function OnDsMainItemChanged(s, r, c, v) {

        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                PostSearch();
            } else if (c == "b_clear") {
                PostClearData();
                //                dsMain.setSetItem("member_no", "");
                //                dsMain.setSetItem("memb_name", "");
                //                dsMain.setSetItem("memb_surname", "");



            }
        }

        function OnDsListItemChanged(s, r, c, v) {
        }

        function OnDsListClicked(s, r, c) {
            if (c == "trnmbreq_docno" || c == "memold_no" || c == "memnameold") {
                dsList.SetRowFocus(r);
                var memold_no = dsList.GetItem(r, "memold_no");
                try {
                    window.opener.GetValueFromDlg(memold_no);
                    window.close();
                } catch (err) {
                    parent.GetValueFromDlg(memold_no);
                    window.close();
                }

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div align="center">
        <uc1:DsMain ID="dsMain" runat="server" />
        <br />
        <uc2:DsList ID="dsList" runat="server" />
    </div>
</asp:Content>
