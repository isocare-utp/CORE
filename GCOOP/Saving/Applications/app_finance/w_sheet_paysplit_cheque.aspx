<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_paysplit_cheque.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_paysplit_cheque" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: small;
            font-weight: bold;
        }
        .style2
        {
            font-size: small;
            font-weight: bold;
            color: #FF0000;
        }
    </style>
    <%=initJavaScript %>
    <%=postBankBranch %>
    <%=postChqBook%>
    <%=postCheckList %>
    <%=postInit %>
    <%=postDeleteRow %>
    <%=postRefresh %>>
    <script type="text/javascript">

        function Validate() {
            objDwCon.AcceptText();
            objDwFrom.AcceptText();
            objDwType.AcceptText();
            objDwSlip.AcceptText();
            objDwSlipChq.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function MenubarNew() {
            if (confirm("ยืนยันการลบข้อมูล ??")) {
                window.location = Gcoop.GetUrl() + "Applications/app_finance/w_sheet_paysplit_cheque.aspx";
            }
        }

        function DwMainItemChange(sender, rowNumber, columnName, newValue) {
            objDwCon.SetItem(rowNumber, columnName, newValue);
            if (columnName == "as_bank") {
                objDwCon.AcceptText();
                postBankBranch();
            }
            else if (columnName == "as_bankbranch") {
                objDwCon.AcceptText();
                postChqBook();
            }
            else if (columnName == "as_chqbookno") {
                objDwCon.AcceptText();
                postInit();
            }
        }

        function DwFromButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_1") {
                var bank = objDwCon.GetItem(rowNumber, "as_bank");
                var bankbranch = objDwCon.GetItem(rowNumber, "as_bankbranch");
                Gcoop.OpenDlg(455, 200, "w_dlg_bank_list.aspx", "?bank=" + bank + "&bankbranch=" + bankbranch);
            }
        }

        function DwSlipItemChange(sender, rowNumber, columnName, newValue) {
            objDwSlip.SetItem(rowNumber, columnName, newValue);
            if (columnName == "ai_selected") {
                Gcoop.GetEl("HdRow").value = rowNumber;
                Gcoop.GetEl("HdCheck").value = newValue;
                objDwSlip.AcceptText();
                postCheckList();
            }
        }

        function GetAccid(accid, accname) {
            objDwFrom.SetItem(1, "as_fromaccno", accid);
        }

        function DwSlipClick(sender, rowNumber, objectName) {
            if (objectName == "ai_selected") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "ai_selected", 1, 0);
                var flag = objDwSlip.GetItem(rowNumber, "ai_selected");
                Gcoop.GetEl("HdRow").value = rowNumber;
                Gcoop.GetEl("HdCheck").value = flag;
                objDwSlip.AcceptText();
                postCheckList();
            }
        }

        function ItemInsertRow() {
            objDwCon.AcceptText();
            objDwFrom.AcceptText();
            objDwType.AcceptText();
            objDwSlip.AcceptText();
            objDwSlipChq.AcceptText();
            objDwSlipChq.InsertRow(0);
        }

        function DwSlipChqButtonClicked(sender, rowNumber, buttonName) {
            objDwCon.AcceptText();
            objDwFrom.AcceptText();
            objDwType.AcceptText();
            objDwSlip.AcceptText();
            objDwSlipChq.AcceptText();
            Gcoop.GetEl("HdRow").value = rowNumber;
            postDeleteRow();
        }
        function ItemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "as_towhom") {
                objDwSlipChq.SetItem(rowNumber, columnName, newValue);
                objDwSlipChq.AcceptText();

            }
            else if (columnName == "adc_money") {
                var adc_money = 0;
                objDwSlipChq.SetItem(rowNumber, columnName, newValue);
                objDwSlipChq.AcceptText();
//               // alert("1");
//                for (i = 1; i <= objDwSlipChq.RowCount(); i++) {
//                  //  alert("2");
//                    adc_money = Gcoop.ParseFloat(objDwSlipChq.GetItem(i, "adc_money"));
//                    adc_money = adc_money + adc_money;
//                 //   alert(adc_money);
//                  Gcoop.GetEl("Hadc_money").value = adc_money;

//                }
                postRefresh();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Panel ID="Panel1" runat="server"  >
        <table border="0">
            <tr>
                <td valign="top">
                    <dw:WebDataWindowControl ID="DwCon" runat="server" DataWindowObject="d_conditionprint_cheque"
                        LibraryList="~/DataWindow/App_finance/paysplit_cheque.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientFormatting="True" ClientEventItemChanged="DwMainItemChange">
                    </dw:WebDataWindowControl>
                </td>
                <td valign="top" align="center">
                    <dw:WebDataWindowControl ID="DwFrom" runat="server" DataWindowObject="d_chqprint_cutfrom"
                        LibraryList="~/DataWindow/App_finance/paysplit_cheque.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientFormatting="True" ClientEventButtonClicked="DwFromButtonClick" >
                    </dw:WebDataWindowControl>
                </td>
                <td valign="top">
                    <dw:WebDataWindowControl ID="DwType" runat="server" DataWindowObject="d_chqprint_chqtype"
                        LibraryList="~/DataWindow/App_finance/paysplit_cheque.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <hr />
        <asp:Panel ID="Panel2" runat="server" Height="350px" ScrollBars="Auto">
        <table>
            <%--<tr>
                <td class="style2" align="left">
                    ยอดคงเหลือ
                    <asp:Label ID="lbl_itembalance" runat="server" Text="0"></asp:Label>
                    &nbsp;&nbsp;&nbsp; บาท
                </td>
            </tr>--%>
            <tr>
                <td colspan="2">
                    <dw:WebDataWindowControl ID="DwSlip" runat="server" DataWindowObject="d_chequelist_fromslip_split"
                        LibraryList="~/DataWindow/App_finance/paysplit_cheque.pbl" HorizontalScrollBar="Fixed"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" ClientScriptable="True" ClientEventItemChanged="DwSlipItemChange"
                        ClientEventClicked="DwSlipClick" >
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            
            
        </table>
        <table>
        </table>
    </asp:Panel>
     <table>
    <tr>
                <td>
                    รายการเช็ค &nbsp; &nbsp; <a href="#" onclick="ItemInsertRow()">
                        <asp:Label ID="lb_insertrow" runat="server" Text="เพิ่มแถว" CssClass="linkInsertRow"></asp:Label>
                    </a>
                </td>
                <td class="style2" align="left">
                  <%--  &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                    ยอดจ่ายคงเหลือ ::
                    <asp:Label ID="lbl_itembalance" runat="server" Text="0"></asp:Label>
                    &nbsp;&nbsp;&nbsp; บาท--%>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dw:WebDataWindowControl ID="DwSlipChq" runat="server" DataWindowObject="d_change_splitchq"
                        LibraryList="~/DataWindow/App_finance/paysplit_cheque.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" ClientEventButtonClicked="DwSlipChqButtonClicked" ClientEventItemChanged="ItemChanged">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
             </table>
    <asp:HiddenField ID="HdRow" runat="server" />
    <asp:HiddenField ID="HdCheck" runat="server" />
      <asp:HiddenField ID="Hadc_money" runat="server" />
</asp:Content>
