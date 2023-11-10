<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_reprintpayslip_loan.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_reprintpayslip_loan" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postPaySlipRetrieve %>
    <%=postSetItem %>
    <%=postPrint %>
    <script type="text/javascript">

        function Validate() {
            objDwList.AcceptText();
            objDwHead.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function DwlistClick(sender, rowNumber, objectName) {
            if (objectName == "ai_select") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "ai_select", 1, 0);
                DwList.AcceptText();
            }
        }

        function DwHeadButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_search") {
                objDwHead.AcceptText();
                postPaySlipRetrieve();
            }
        }

        function DwlistButtonclick(sender, rowNumber, buttonName) {
            if (buttonName == "b_print") {
                objDwList.AcceptText();
                postPrint();
            }
        }

        //        function DwHeadItemChange(sender, rowNumber, columnName, newValue) {
        //            objDwHead.SetItem(rowNumber, columnName, newValue);
                    Gcoop.GetEl("HfNewValue").value = newValue;
                    Gcoop.GetEl("HfColoum").value = columnName;
        //            objDwHead.AcceptText();
        //            postSetItem();
        //        }
        function DwHeadItemChange(sender, rowNumber, columnName, newValue) {
            Gcoop.GetEl("HfNewValue").value = newValue;
            Gcoop.GetEl("HfColoum").value = columnName;
            if (columnName == "adtm_tdate") {
                sender.SetItem(rowNumber, "adtm_tdate", newValue);
                sender.AcceptText();
                sender.SetItem(rowNumber, "adtm_date", Gcoop.ToEngDate(newValue));
                sender.AcceptText();
            }
            else if (columnName == "as_memberno") {
               
                sender.SetItem(rowNumber, "as_memberno", Gcoop.StringFormat(newValue, "00000000"));
                sender.AcceptText();
                postSetItem();
            }
            else if (columnName == "as_receipt") {
                
                sender.SetItem(rowNumber, "as_receipt", newValue);
                sender.AcceptText();
                postSetItem();
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwHead" runat="server" DataWindowObject="d_reprintreceipt_head_loan"
                    LibraryList="~/DataWindow/App_finance/reprintpayslip.pbl" AutoRestoreContext="False"
                    ClientFormatting="True" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    ClientScriptable="True" ClientEventButtonClicked="DwHeadButtonClick" ClientEventItemChanged="DwHeadItemChange">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_reprintpay_list_loan"
        LibraryList="~/DataWindow/App_finance/reprintpayslip.pbl" AutoRestoreContext="False"
        Width="750px" Height="700px" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientScriptable="True" ClientEventClicked="DwlistClick" ClientEventButtonClicked="DwlistButtonclick">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HfNewValue" runat="server" />
    <asp:HiddenField ID="HdOpenIFrame" runat="server" Value="False" />
    <asp:HiddenField ID="HdcheckPdf" runat="server" Value="False" />
    <asp:HiddenField ID="HfColoum" runat="server" />
</asp:Content>
