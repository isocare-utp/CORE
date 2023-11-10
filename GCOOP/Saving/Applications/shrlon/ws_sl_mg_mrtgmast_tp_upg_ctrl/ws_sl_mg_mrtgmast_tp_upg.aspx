<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_mg_mrtgmast_tp_upg.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_tp_upg_ctrl.ws_sl_mg_mrtgmast_tp_upg" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;
        var dsDetail = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_search") {
                Gcoop.OpenDlg("630", "720", "w_dlg_member_search.aspx", "")
            }
        }

        function GetMemDetFromDlg(memberno, prename_desc, memb_name, memb_surname, card_person) {
            dsMain.SetItem(0, "member_no", memberno);
            PostMemberNo();

        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }
        }

        function OnDsListClicked(s, r, c, v) {
            if (c == "upgrade_no" || c == "upgrade_date") {
                PostUpgradeNo();
            }
        }

        function OnDsDetailClicked(s, r, c, v) {
            if (c == "b_autzd") {
                Gcoop.OpenIFrame2("650", "500", "w_dlg_mg_autzd_search.aspx", "");
            }
            else if (c == "b_mrtgsearch") {
                var member_no = dsMain.GetItem(0, "member_no");
                Gcoop.OpenIFrame2("520", "520", "w_dlg_mg_mrtgmast.aspx", "?member_no=" + member_no)
            }
        }

        function GetMrtgmastNoFromDlg(mrtgmast_no) {
            dsDetail.SetItem(0, "mrtgmast_no", mrtgmast_no);
            PostMrtgeNo();
        }

        function GetTemplateNoFromDlg(template_no) {
            Gcoop.GetEl("HdTemplateNo").value = template_no;
            PostTemplateNo();
        }

        function OnDsDetailItemChanged(s, r, c, v) {
        }

        function SheetLoadComplete() {
        }
   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HdTemplateNo" runat="server" />
    <br />
    <table width="770px;">
        <tr>
            <td colspan="2">
                <uc1:DsMain ID="dsMain" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <uc2:DsList ID="dsList" runat="server" />
            </td>
            <td valign="top">
                <uc3:DsDetail ID="dsDetail" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
