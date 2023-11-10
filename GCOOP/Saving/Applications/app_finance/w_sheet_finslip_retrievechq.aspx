<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_finslip_retrievechq.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_finslip_retrievechq" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postCashType %>

    <script type="text/javascript">

        function DwMainItemChange(sender, rowNumber, columnName, newValue) {
            if (columnName == "cash_type") {
                var cashType = objDwMain.GetItem(rowNumber, "cash_type");
                Gcoop.GetEl("HfCashType").value = cashType;
                postCashType();
            }
        }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_fin_slip_recvchq"
        LibraryList="~/DataWindow/App_finance/finslip_retrievechq.pbl">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="d_fin_slipspc_det_itemtype" runat="server" DataWindowObject="d_fin_slipspc_det_itemtype"
        LibraryList="~/DataWindow/App_finance/finslip_retrievechq.pbl" Width="720px"
        Height="200px">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HfCashType" runat="server" />
</asp:Content>
