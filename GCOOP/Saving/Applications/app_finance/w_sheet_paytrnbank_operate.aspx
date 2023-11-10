<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_paytrnbank_operate.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_paytrnbank_operate" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postListPaymentDoc%>
    <%=postMainAndDetail%>
    <%=postSearchPaymentDoc%>
    <script type="text/javascript">
        function OnDwFindItemChanged(sender, rowNumber, colunmName, newValue) {
            if (colunmName == "payment_docno") {
                objDwFind.SetItem(rowNumber, colunmName, newValue);
                objDwFind.AcceptText();
                Gcoop.GetEl("HdPaymentDoc").value = newValue;
                postSearchPaymentDoc();
            }
        }

        function MenubarNew() {
            if (confirm("ยืนยันการลบข้อมูล ??")) {
                window.location = Gcoop.GetUrl() + "Applications/app_finance/w_sheet_paytrnbank_operate.aspx";
            }
        }

        function OnDwDateItemChanged(sender, rowNumber, colunmName, newValue) {
            if (colunmName == "entry_tdate") {
                objDwDate.SetItem(rowNumber, colunmName, newValue);
                objDwDate.SetItem(rowNumber, "entry_date", Gcoop.ToEngDate(newValue));
                objDwDate.AcceptText();
                postListPaymentDoc();
            }
        }

        function DwMainButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_search") {
                Gcoop.OpenDlg(340, 350, "w_dlg_paytrnbank.aspx", "");
            }
        }

        function OnDwListClicked(sender, rowNumber, colunmName) {
            if (colunmName != "t_member" && colunmName != "t_paymentdoc" && rowNumber != 0) {
                var payTrnDoc = objDwList.GetItem(rowNumber, "paytrnbank_docno") + "";
                Gcoop.GetEl("HdPayTrnDoc").value = payTrnDoc;
                Gcoop.GetEl("HdRowList").value = rowNumber + "";
                postMainAndDetail();
            }
        }

        function Validate() {
            return confirm("ต้องการบันทึกข้อมูล ใช่หรือไม่?");
        }

        function GetPaymentDocno(paymentdocno) {
            paymentdocno = Gcoop.Trim(paymentdocno);
            objDwFind.SetItem(1, "payment_docno", paymentdocno);
            objDwFind.AcceptText();
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td class="style5">
                <asp:Panel ID="Panel3" runat="server" Height="61px" Width="200px" BorderStyle="Ridge">
                    <span class="style3">
                        <dw:WebDataWindowControl ID="DwDate" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_fn_paytrnbank_operate_date"
                            LibraryList="~/DataWindow/app_finance/paytrnbank.pbl" ClientFormatting="True"
                            ClientEventItemChanged="OnDwDateItemChanged">
                        </dw:WebDataWindowControl>
                    </span>
                </asp:Panel>
            </td>
            <td rowspan="2" valign="top">
                <asp:Panel ID="Panel4" runat="server" BorderStyle="Ridge" Height="100px">
                    <span class="style3">
                        <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_fn_paytrnbank_operate_main"
                            LibraryList="~/DataWindow/app_finance/paytrnbank.pbl" ClientFormatting="True">
                        </dw:WebDataWindowControl>
                    </span>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <span class="style3">
                    <asp:Panel ID="Panel2" runat="server" BorderStyle="Ridge" Height="30px" Width="200px">
                        <dw:WebDataWindowControl ID="DwFind" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_fn_paytrnbank_operate_find"
                            LibraryList="~/DataWindow/app_finance/paytrnbank.pbl" ClientFormatting="True"
                            ClientEventItemChanged="OnDwFindItemChanged" ClientEventButtonClicked="DwMainButtonClick">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </span>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <span class="style3">
                    <asp:Panel ID="Panel5" runat="server" Height="300px" ScrollBars="Vertical" BorderStyle="Ridge">
                        <dw:WebDataWindowControl ID="DwList" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_fn_paytrnbank_operate_list"
                            LibraryList="~/DataWindow/app_finance/paytrnbank.pbl" ClientFormatting="True"
                            ClientEventClicked="OnDwListClicked">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </span>
            </td>
            <td>
                <span class="style3">
                    <asp:Panel ID="Panel6" runat="server" BorderStyle="Ridge" Height="300px" ScrollBars="Vertical">
                        <dw:WebDataWindowControl ID="DwDetail" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            DataWindowObject="d_fn_paytrnbank_operate_detail" LibraryList="~/DataWindow/app_finance/paytrnbank.pbl"
                            ClientFormatting="True">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </span>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdPayTrnDoc" runat="server" />
    <asp:HiddenField ID="HdRowList" runat="server" />
    <asp:HiddenField ID="HdPaymentDoc" runat="server" />
</asp:Content>
