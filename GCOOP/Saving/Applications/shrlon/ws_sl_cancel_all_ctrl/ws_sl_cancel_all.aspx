<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_sl_cancel_all.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cancel_all_ctrl.ws_sl_cancel_all" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>
<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMembno();
            }
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenIFrame2("630", "720", "w_dlg_sl_member_search_tks.aspx", "");
            } 
        }
        function OnDsListItemChanged(s, r, c, v) {
           
        }


        function OnDsListClicked(s, r, c) { }

        function GetValueFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno);
            PostMembno();
        }

        
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
