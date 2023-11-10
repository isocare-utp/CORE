<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_auditloan.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_auditloan" Title="Untitled Page" %>
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
    <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_sl_audit_lnedit_det_main"
        LibraryList="~/DataWindow/shrlon/sl_audit_lnedit.pbl" 
        ClientScriptable="True" ClientEvents="true"
        ClientEventItemChanged="itemChanged" AutoRestoreContext="False" 
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
    </dw:WebDataWindowControl>
    <table style="width: 100%;">
        <tr>
            <td valign="top" width="30%">
                <div>
                    <dw:WebDataWindowControl ID="dw_list" runat="server" 
                        DataWindowObject="d_sl_audit_lnedit_det_list" 
                        LibraryList="~/DataWindow/shrlon/sl_audit_lnedit.pbl" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
                
            </td>
            <td valign="top">
                <div>
                    <dw:WebDataWindowControl ID="dw_detail" runat="server" 
                        DataWindowObject="d_sl_audit_lnedit_det_detail" 
                        LibraryList="~/DataWindow/shrlon/sl_audit_lnedit.pbl" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
