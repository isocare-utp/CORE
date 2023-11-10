<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_mb_mthexpense_adjust.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_mb_mthexpense_adjust" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>

    <script type="text/javascript">
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }           
        
        
        }
    </script>

    <style type="text/css">
        .style1
        {
            height: 19px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HfLncontno" runat="server" />
    <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_mb_mthexp_adjust"
        LibraryList="~/DataWindow/shrlon/mb_mthexp_adjust.pbl" ClientScriptable="True"
        ClientEvents="true" ClientEventItemChanged="itemChanged" 
        AutoRestoreContext="False" AutoRestoreDataCache="True" 
        AutoSaveDataCacheAfterRetrieve="True">
    </dw:WebDataWindowControl>
    <table style="width: 100%;">
        <tr>
            <td valign="top" width="30%">
                <div>
                    <dw:WebDataWindowControl ID="dw_plus" runat="server" DataWindowObject="d_mb_mthexp_adjust_plus"
                        LibraryList="~/DataWindow/shrlon/mb_mthexp_adjust.pbl" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
            </td>
            <td valign="top">
                <div>
                    <dw:WebDataWindowControl ID="dw_minus" runat="server" DataWindowObject="d_mb_mthexp_adjust_minus"
                        LibraryList="~/DataWindow/shrlon/mb_mthexp_adjust.pbl" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
