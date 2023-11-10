<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_finslip_spc_others_noslip.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_finslip_spc_others_noslip" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postSetHeadAndList%>
    <%=postInitDet%>
    <%=postPrintSlip %>

    <script type="text/javascript">

        function Validate() {
            objDwList.AcceptText();
            objDwFromOther.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function MenubarNew() {
            if (confirm("ยืนยันการลบข้อมูล ??")) {
                window.location = Gcoop.GetUrl() + "Applications/app_finance/w_sheet_finslip_spc_others.aspx";
            }
        }

        function DwFromOtherItemChamge(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objDwFromOther.SetItem(rowNumber, columnName, newValue);
                objDwFromOther.AcceptText();
                postSetHeadAndList();
            }
        }

        function DwSlipNetItemChange(sender, rowNumber, columnName, newValue) {
            objDwSlipNet.SetItem(rowNumber, columnName, newValue);
            var row = Gcoop.GetEl("HdRow").value;
            var cash_type = objDwSlipNet.GetItem(rowNumber, "cash_type");
            var recv_status = objDwList.GetItem(row, "pay_recv_status");
            if (columnName == "cash_type") {
                objDwList.SetItem(row, "cash_type", newValue);
                if (cash_type == "CHQ" && recv_status == 1) {
                    Gcoop.OpenIFrame(390, 170, "w_dlg_recvchq.aspx", "");
                }
            }
            else if (columnName == "tofrom_accid") {
                objDwList.SetItem(row, "tofrom_accid", newValue);
            }
        }

        function DwListClick(sender, rowNumber, objectName) {
            if (objectName == "select_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "select_flag", 1, 0);
                var flag = objDwList.GetItem(rowNumber, "select_flag");
                Gcoop.GetEl("HdRow").value = rowNumber;
                Gcoop.GetEl("HdCheck").value = flag;
                objDwList.AcceptText();
                postInitDet();
            }
        }

        function SetId(account_no, dateon_chq, bank_code, bank_branch) {
            objDwList.SetItem(1, "account_no", account_no);
            objDwList.SetItem(1, "dateon_chq", dateon_chq);
            objDwList.SetItem(1, "bank_code", bank_code);
            objDwList.SetItem(1, "bank_branch", bank_branch);
            objDwList.AcceptText();
        }

        function DwFromOtherButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_membsearch") {
                Gcoop.OpenDlg(700, 530, "w_dlg_fin_member_search.aspx", "");
            }
        }

        function GetValueFromDlg(memberNo) {
            objDwFromOther.SetItem(1, "member_no", memberNo);
            objDwFromOther.AcceptText();
            postSetHeadAndList();
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("Hslipno").value != "") {
                if (Gcoop.GetEl("Hprintslip").value == "true") {
                    if (confirm("ยืนยันการพิมพ์ใบสำคัญจ่าย / ใบเสร็จ")) {
                        Gcoop.GetEl("Hprintslip").value = "false";
                        postPrintSlip();
                    }
                    Gcoop.GetEl("Hprintslip").value = "false";
                }
            }

            for (var i = 1; i <= objDwItem.RowCount(); i++) {
                objDwItem.SelectRow(i, false);
            }
            var rowNumber = Gcoop.GetEl("HdDetailRow").value;
            objDwItem.SelectRow(rowNumber, true);
            objDwItem.SetRow(rowNumber);
        }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table border="0" width="100%">
        <tr>
            <td align="center">
                <dw:WebDataWindowControl ID="DwFromOther" runat="server" DataWindowObject="d_fin_slipspc_fromother"
                    LibraryList="~/DataWindow/app_finance/finslip_spc.pbl" AutoRestoreContext="false"
                    ClientScriptable="true" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    ClientEventButtonClicked="DwFromOtherButtonClick" ClientEventItemChanged="DwFromOtherItemChamge">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <br />
    <hr />
    <br />
    <table border="0">
        <tr>
            <td valign="top">
                <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Auto">
                    <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_fin_slipspc_otherlist"
                        LibraryList="~/DataWindow/app_finance/finslip_spc.pbl" AutoRestoreContext="false"
                        ClientScriptable="true" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientEventClicked="DwListClick">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
            <td valign="top">
                <dw:WebDataWindowControl ID="DwSlipNet" runat="server" DataWindowObject="d_fin_slipspc_otherlist_net"
                    LibraryList="~/DataWindow/app_finance/finslip_spc.pbl" AutoRestoreContext="false"
                    ClientScriptable="true" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    ClientEventItemChanged="DwSlipNetItemChange">
                </dw:WebDataWindowControl>
                <br />
                <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="100px">
                    <dw:WebDataWindowControl ID="DwSlipDet" runat="server" DataWindowObject="d_fin_slipspc_other_det"
                        LibraryList="~/DataWindow/app_finance/finslip_spc.pbl" AutoRestoreContext="false"
                        ClientScriptable="true" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <dw:WebDataWindowControl ID="DwCancelList" runat="server" DataWindowObject="d_fin_slipspc_otherlistcancel"
        LibraryList="~/DataWindow/app_finance/finslip_spc.pbl" AutoRestoreContext="false"
        ClientScriptable="true" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdRow" runat="server" />
    <asp:HiddenField ID="HdCheck" runat="server" />
    <asp:HiddenField ID="Hslipno" runat="server" />
    <asp:HiddenField ID="Hprintslip" runat="server" Value="false" />
     <asp:HiddenField ID="HdcheckPdf" runat="server" Value="False" />
      <asp:HiddenField ID="HdOpenIFrame" runat="server" Value="False" />
</asp:Content>
