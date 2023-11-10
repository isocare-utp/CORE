<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_shrwtd_cancel.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_sl_shrwtd_cancel" Title="Untitled Page" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
   <script type="text/javascript">
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
       
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="2" valign= "top">               
                <dw:WebDataWindowControl ID="dw_main" runat="server" 
                    DataWindowObject="d_sl_lnccl_detailmem" 
                    LibraryList="~/DataWindow/shrlon/sl_slipall.pbl"
                    AutoRestoreContext="False" AutoRestoreDataCache="True" 
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" 
                    Width="720px">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td rowspan="2" valign= "top">                
                <dw:WebDataWindowControl ID="dw_list" runat="server" 
                    DataWindowObject="d_sl_lnccl_sliplist_shrwtd" 
                    LibraryList="~/DataWindow/shrlon/sl_slipall.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True" 
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
            </td>
            <td valign= "top">                
                <dw:WebDataWindowControl ID="dw_detail" runat="server" 
                    DataWindowObject="d_sl_lnccl_shrwtd" 
                    LibraryList="~/DataWindow/shrlon/sl_slipall.pbl"  AutoRestoreContext="False" AutoRestoreDataCache="True" 
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
               
            </td>
        </tr>
        <tr>
            <td valign= "top">                
                <dw:WebDataWindowControl ID="dw_clear" runat="server" 
                    DataWindowObject="d_sl_lnccl_payin_detail" 
                    LibraryList="~/DataWindow/shrlon/sl_slipall.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True" 
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
</asp:Content>
