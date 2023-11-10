<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_sl_receive_ref_slip.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_receive_ref_slip_ctrl.w_dlg_sl_receive_ref_slip" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();

        function OnDsMainClicked(s, r, c) {

        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "ref_system") {
                PostRefSystem();
            }else if(c == "member_no"){
                PostMemberNo();
            }
        }

        function OnDsListClicked(s, r, c) {
            var memberno = dsList.GetItem(r, "member_no");
            var acc_id = dsList.GetItem(r, "acc_id");
            var slip_no = dsList.GetItem(r, "slip_no");
            var slip_amt = dsList.GetItem(r, "slip_amt");
            var ref_system = dsMain.GetItem(0, "ref_system");
            try {
                window.opener.GetRefSlipFromDialog(acc_id, ref_system, slip_no, slip_amt);
                window.close();
            } catch (err) {
                parent.GetRefSlipFromDialog(acc_id, ref_system, slip_no, slip_amt);
                parent.RemoveIFrame();
            }
        }


        function OnDsListItemChanged(s, r, c, v) {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessageDlg" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
