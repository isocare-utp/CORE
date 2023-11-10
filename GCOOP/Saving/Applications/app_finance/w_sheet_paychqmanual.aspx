<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_paychqmanual.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_paychqmanual" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postBankBranch %>
    <%=postSumChqList %>
    <%=postGetChqBook%>
    <%=postInit %>
    <%=postSearch %>
    <script type="text/javascript">

        function Validate() {
            objDwMain.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function MenubarNew() {
            if (confirm("ยืนยันการลบข้อมูล ??")) {
                window.location = Gcoop.GetUrl() + "Applications/app_finance/w_sheet_paychqmanual.aspx";
            }
        }

        function DwMainButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_1") {
                var bank = objDwMain.GetItem(rowNumber, "frombank");
                var bankbranch = objDwMain.GetItem(rowNumber, "frombranch");
                Gcoop.OpenDlg(455, 200, "w_dlg_bank_list.aspx", "?bank=" + bank + "&bankbranch=" + bankbranch);
            }
        }


        function DwMainItemChange(sender, rowNumber, columnName, newValue) {
            objDwMain.SetItem(rowNumber, columnName, newValue);
            if (columnName == "bank_code" || columnName == "frombank") {
                objDwMain.AcceptText();
                postBankBranch();
            }
            else if (columnName == "bank_branch") {
                objDwMain.AcceptText();
                postGetChqBook();
            }
            else if (columnName == "cheque_bookno") {
                objDwMain.AcceptText();
                postInit();
            }
        }

        function GetAccid(accid, accname) {
            objDwMain.SetItem(1, "fromaccount_no", accid);
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Panel ID="Panel1" runat="server" Width="720px" Height="600px" ScrollBars="Auto">
        <table width="100%" border="0">
            <tr>
                <td>
                    เลือกเครื่องพิมพ์ :
                    <asp:DropDownList ID="DdPrintSetProfile" runat="server" Width="200px">
                        <asp:ListItem Text="printfin-23" Value="printfin-23" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="printfin-22" Value="printfin-22"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_pay_cheque"
                        LibraryList="~/DataWindow/App_finance/paychq.pbl" ClientEventItemChanged="DwMainItemChange"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientScriptable="True" ClientEventButtonClicked="DwMainButtonClick" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
