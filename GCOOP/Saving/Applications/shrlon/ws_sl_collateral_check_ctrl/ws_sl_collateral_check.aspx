<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_collateral_check.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_check_ctrl.ws_sl_collateral_check" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsMemdet.ascx" TagName="DsMemdet" TagPrefix="uc2" %>
<%@ Register Src="DsCollwho.ascx" TagName="DsCollwho" TagPrefix="uc3" %>
<%@ Register Src="DsMemcoll.ascx" TagName="DsMemcoll" TagPrefix="uc4" %>
<%@ Register Src="DsCollwholnreq.ascx" TagName="DsCollwholnreq" TagPrefix="uc5" %>
<%@ Register Src="DsSharedet.ascx" TagName="DsSharedet" TagPrefix="uc6" %>
<%@ Register Src="DsDeptdet.ascx" TagName="DsDeptdet" TagPrefix="uc7" %>
<%@ Register Src="DsCollmast.ascx" TagName="DsCollmast" TagPrefix="uc8" %>
<%@ Register Src="DsCollmemco.ascx" TagName="DsCollmemco" TagPrefix="uc9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsMemdet = new DataSourceTool;


        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "colltype_code") {
                var colltype_code = dsMain.GetItem(0, "colltype_code");
                dsMain.SetItem = dsMain.SetItem(0, "colltype_code_name", colltype_code);
                PostColltypeCode();
            } else if (c == "collateral_no") {

                PostMemberNo();
            }
        }
        function OnDsMainClicked(s, r, c, v) {
            var collTypeCode = dsMain.GetItem(0, "colltype_code");
            if (c == "b_search" && collTypeCode == '01') {
                Gcoop.GetEl("HdColumnName").value = c;
                Gcoop.OpenDlg('600', '600', 'w_dlg_sl_member_search.aspx', '');

            } else if (c == "b_search" && collTypeCode == '02') {
                Gcoop.GetEl("HdColumnName").value = c;
                Gcoop.OpenDlg('600', '600', 'w_dlg_sl_member_search.aspx', '');
            } else if (c == "b_search" && collTypeCode == '03') {
                Gcoop.OpenDlg('600', '450', 'w_dlg_dp_account_search.aspx', '');
            } else if (c == "b_search" && collTypeCode == '04') {
                Gcoop.OpenDlg('600', '450', 'w_dlg_sl_collmaster_search.aspx', '');
            } else if (c == "b_print") {
                Postprint();
            }
        }
        function GetValueFromDlg(memberno) {
            var colunmName = Gcoop.GetEl("HdColumnName").value;
            if (colunmName == "b_search") {
                dsMain.SetItem(0, "collateral_no", memberno);
                PostMemberNo();
            }
        }
        function NewAccountNo(dept_no, deptaccount_name, prncbal) {

            dsMain.SetItem(0, "collateral_no", dept_no);
            PostMemberNo();
        }
        function GetValueFromDlgCollmast(collmast_refno) {
            dsMain.SetItem(0, "collateral_no", collmast_refno);
            PostMemberNo();
        }

        function SheetLoadComplete() {
            $('.DataSourceRepeater').find('input,select,button').attr('readOnly', true);
            $('#ctl00_ContentPlace_dsMemdet_FormView1').find('input,select,button').attr('readOnly', true);
        }      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Literal ID="LtServerMessage2" runat="server"></asp:Literal>
    <table>
        <tr>
            <td colspan="2">
                <uc1:DsMain ID="dsMain" runat="server" />
            </td>
        </tr>
        <tr>
            <td width="20%">
                <uc2:DsMemdet ID="dsMemdet" runat="server" />
                <uc6:DsSharedet ID="dsSharedet" runat="server" />
                <uc7:DsDeptdet ID="dsDeptdet" runat="server" />
                <uc8:DsCollmast ID="dsCollmast" runat="server" />
            </td>
            <td width="80%" valign="top">
                <uc3:DsCollwho ID="dsCollwho" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <uc4:DsMemcoll ID="dsMemcoll" runat="server" />
                <uc9:DsCollmemco ID="dsCollmemco" runat="server" />
            </td>
            <td valign="top">
                <uc5:DsCollwholnreq ID="dsCollwholnreq" runat="server" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdColumnName" runat="server" />
</asp:Content>
