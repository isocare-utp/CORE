<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_sl_closemonth_wizard.aspx.cs" Inherits="Saving.Applications.shrlonclsproc.w_dlg_sl_closemonth_wizard" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript %>
<%=postCloseMonth%>
<%=clearProcess%>

    <script type="text/javascript">
     function CloseMonthFinish(){
        Gcoop.RemoveIFrame();
        clearProcess()
     }
     
    function OnDwDateClick(s,r,c) {
        if (c == "b_closemonth") { 
           postCloseMonth();
        }
    }
    
    function SheetLoadComplete(){
        if(Gcoop.GetEl("HdRunProcess").value == "true"){
            //Gcoop.OpenIFrame(450, 200, "w_dlg_dp_closeday.aspx", "");
            Gcoop.OpenProgressBar("ประมวลผลปิดสิ้นเดือน", true, true, CloseMonthFinish);
        }
    }
    
    function Validate() {
        alert("หน้าจอประมวลผลปิดสิ้นเดือน ไม่มีคำสั่งเซฟ");
        return false;
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width:100%;">

        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="dw_criteria" runat="server" DataWindowObject="d_sl_closemonth_wizard_month"
                    LibraryList="~/DataWindow/shrlonclsproc/sl_shlnproc.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="True" ClientEventButtonClicked="OnDwDateClick" >
                </dw:WebDataWindowControl>
            </td>
        </tr>

    </table>
    <asp:HiddenField ID="HdRunProcess" runat="server" />
</asp:Content>
