<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_reprintreceipt.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_reprintreceipt" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postReceiptRetrieve %>
    <%=postPrint %>
    <script type="text/javascript">
        $(function () {
            $('input[name="adtm_tdate_0"]').keyup(function () {
                ActiveChangeFormatDate('input[name="adtm_tdate_0"]');
            });
        });

        function DwHeadButtonClick(s, r, c) {
            if (c == "b_search") {
                postReceiptRetrieve();
            }
        }

        function DwlistButtonclick(s, r, c) {
            if (c == "b_print") {
                postPrint();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwHead" runat="server" DataWindowObject="d_reprintreceipt_head"
                    LibraryList="~/DataWindow/App_finance/reprintreceipt.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="true" ClientEvents="true"
                    ClientEventButtonClicked="DwHeadButtonClick" ClientEventItemChanged="DwHeadItemChange">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <dw:WebDataWindowControl ID="Dwlist" runat="server" DataWindowObject="d_reprintreceipt_list"
        LibraryList="~/DataWindow/App_finance/reprintreceipt.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventClicked="DwlistClick" ClientEventButtonClicked="DwlistButtonclick">
    </dw:WebDataWindowControl>
</asp:Content>
