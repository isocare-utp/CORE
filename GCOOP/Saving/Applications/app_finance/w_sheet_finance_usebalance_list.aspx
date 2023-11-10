<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_finance_usebalance_list.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_finance_usebalance_list" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
    <script type="text/javascript">


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table>
            <tr>
                <td>
                    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        ClientScriptable="True" DataWindowObject="d_finquery_cashbalance" Height="150px" LibraryList="~/DataWindow/app_finance/finquery.pbl"
                        Width="220px" HorizontalScrollBar="None" VerticalScrollBar="None" 
                        AutoSaveDataCacheAfterRetrieve="True">
                    </dw:WebDataWindowControl>
                </td>
            <tr>
            </tr>
                <td align="center">
                    <dw:WebDataWindowControl ID="DwDetail" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" ClientScriptable="True" DataWindowObject="d_finquery_usedetail_list"
                        Height="500px" LibraryList="~/DataWindow/app_finance/finquery.pbl" Width="500px"  
                        HorizontalScrollBar="None" VerticalScrollBar="None"  AutoSaveDataCacheAfterRetrieve="True">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
        </table>
        <br />
        <hr />
    </asp:Panel>
</asp:Content>

