<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_install_ins_mu.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_install_ins_mu" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript %>
<script type="text/javascript">
    function Validate() {
        confirm("ยืนยันการบันทึกข้อมูล");
        }  
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <table>
    <tr>
    <td>
    <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_sl_main_install_ins_mu"
                    LibraryList="~/DataWindow/shrlon/sl_slip_operate.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    Width="720px" ClientEventItemChanged="ItemChanged" TabIndex="1" ClientFormatting="True">
                </dw:WebDataWindowControl>
    </td></tr>
    
    <tr>
    <td>
     <dw:WebDataWindowControl ID="Dw_list" runat="server" DataWindowObject="d_sl_install_ins_mu_list"
                    LibraryList="~/DataWindow/shrlon/sl_slip_operate.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    Width="720px" ClientEventItemChanged="ItemChanged" TabIndex="1" ClientFormatting="True">
                </dw:WebDataWindowControl>
    </td>
    </tr>

  
</table>
</asp:Content>
