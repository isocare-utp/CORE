<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_fin_cashflowmas.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_fin_cashflowmas" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postCloseDay%>

    <script type="text/javascript">


        function OnDwDateClick(sender, row, bName) {
            if (bName == "b_closeday") {

                postCloseDay();
            }
            return 0;
        }
        function OnItemChanged(s, r, c, v) {
            if (c == "start_date") {

                objDw_date.SetItem(r, c, v);               
                objDw_date.AcceptText();
            }
            else if (c == "cashflow") {

                objDw_date.SetItem(r, c, v);             
                objDw_date.AcceptText();
            }


        }
        function SheetLoadComplete() {

        }
        function Validate() {

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="Dw_date" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_cash_flow"
                    LibraryList="~/DataWindow/app_finance/finance.pbl" ClientEventClicked="OnDwDateClick" ClientEventItemChanged="OnItemChanged"
                    ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdCloseday" runat="server" />
</asp:Content>

