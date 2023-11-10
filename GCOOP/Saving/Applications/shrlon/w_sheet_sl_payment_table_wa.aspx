<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_payment_table_wa.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_payment_table_wa" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostCalculate%>
    <%=jsPostReset%>
     <%=jsPrintreport%>
    <script type="text/javascript">


        function onCriteriaButtonClicked(s, r, c) {
            if (c == "b_retrieve") {
                jsPostCalculate();
            }
            if (c == "b_reset") {
                jsPostReset();
            }
            if (c == "b_print") {
                jsPrintreport();
                
            }
        }
        
        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_criteria" runat="server" DataWindowObject="d_loansrv_lnpaytab_inputloan"
                    LibraryList="~/DataWindow/shrlon/sl_payment_table.pbl" ClientEventItemChanged="onCriteriaItemchanged"
                    AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    ClientFormatting="True" ClientScriptable="True" ClientEventButtonClicked="onCriteriaButtonClicked">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_result" runat="server" DataWindowObject="d_loansrv_lnpaytab_outputloan"
                    LibraryList="~/DataWindow/shrlon/sl_payment_table.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                    ClientScriptable="True" >
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdOpenIFrame" runat="server" Value="False" />
    <asp:HiddenField ID="HdcheckPdf" runat="server" Value="False" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:HiddenField ID="Hdcommitreport" runat="server" Value="false" />
</asp:Content>
