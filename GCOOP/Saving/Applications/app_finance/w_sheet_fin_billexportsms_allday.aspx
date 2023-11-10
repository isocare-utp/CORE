<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_fin_billexportsms_allday.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_fin_billexportsms_allday" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <%=initJavaScript%>
  <%=jsSendSMS%><%--
    <%=postRefresh %>
    <%=postInsertRow%>
    <%=postDeleteRow%>--%>
    <script type="text/javascript">

        function DwMainButtonClicked(sender, rowNumber, buttonName) {
            if (buttonName == "b_sent") {
                objDwMain.SetItem(sender, rowNumber, buttonName);
                objDwMain.AcceptText();
                jsSendSMS();
            } 
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
<dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_fin_sms"
        LibraryList="~/DataWindow/App_finance/fin_sms.pbl" ClientEventButtonClicked="DwMainButtonClicked"
        ClientScriptable="True" AutoRestoreContext="false" AutoRestoreDataCache="true"
        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True">
    </dw:WebDataWindowControl>
</asp:Content>
  

