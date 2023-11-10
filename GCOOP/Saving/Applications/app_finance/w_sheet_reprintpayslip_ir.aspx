<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_reprintpayslip_ir.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_reprintpayslip_ir" %>

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

        function DwHeadItemChange(sender, rowNumber, columnName, newValue) {
            objDwHead.SetItem(rowNumber, columnName, newValue);
            Gcoop.GetEl("HfNewValue").value = newValue;
            Gcoop.GetEl("HfColoum").value = columnName;
            objDwHead.AcceptText();
            postSetItem();
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwHead" runat="server" DataWindowObject="d_reprintreceipt_head"
                    LibraryList="~/DataWindow/App_finance/reprintpayslip.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientEventButtonClicked="DwHeadButtonClick" ClientEventItemChanged="DwHeadItemChange">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_reprintpay_list_ir"
        LibraryList="~/DataWindow/App_finance/reprintpayslip.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventClicked="DwlistClick" ClientEventButtonClicked="DwlistButtonclick">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HfNewValue" runat="server" />
    <asp:HiddenField ID="HfColoum" runat="server" />
</asp:Content>
