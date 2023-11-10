<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_principal_balance.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_principal_balance" Title="Untitled Page" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postRun%>
    <%=postReport%>
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
    function OpenProgressDlg(){
        Gcoop.OpenIFrame("240","260", "w_dlg_principal_balance_progress.aspx","");
    }
    function OnButtonClicked(){
        objdw_criteria.AcceptText();
        postRun();
    }
    function SheetLoadComplete(){
        if( Gcoop.GetEl("HdOpenIFrame").value == "True" ){
            OpenProgressDlg();
        }
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"/>
    <table style="width:100%;">
        <tr>
            <td colspan=3 class="style1">
                <b>ประมวลผลหุ้นหนี้คงเหลือ</b></td>
        </tr>
        <tr>
            <td colspan=3 class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="center">
                <dw:WebDataWindowControl ID="dw_criteria" runat="server" 
                    AutoRestoreContext="False" AutoRestoreDataCache="True" 
                    AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" 
                    ClientScriptable="True" DataWindowObject="d_criteria_principal_balance" 
                    LibraryList="~/DataWindow/shrlon/sl_principal_balance.pbl" RowsPerPage="1">
                </dw:WebDataWindowControl>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="center">
                <input id="btnCall" style="width: 200px" type="button" onclick="OnButtonClicked()"
                    value="เริ่ม" /></td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:HiddenField ID="HdOpenIFrame" runat="server" value="False"/>
</asp:Content>
