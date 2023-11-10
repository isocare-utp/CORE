<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_sl_closeday_wizard.aspx.cs" Inherits="Saving.Applications.shrlonclsproc.w_dlg_sl_closeday_wizard" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript %>
<%=postCloseDay%>
<%=clearProcess%>
    <script type="text/javascript">
     function CloseDayFinish(){
        Gcoop.RemoveIFrame();
        clearProcess();
     }
     
    function OnDwDateClick(s,r,c) {
        if (c == "b_closeday") { 
           postCloseDay();
        }
    }
    
    function SheetLoadComplete(){
        if(Gcoop.GetEl("HdRunProcess").value == "true"){
            //Gcoop.OpenIFrame(450, 200, "w_dlg_dp_closeday.aspx", "");
            Gcoop.OpenProgressBar("ประมวลผลปิดสิ้นวัน", true, true, CloseDayFinish);
        }
    }
    
    function Validate() {
        alert("หน้าจอประมวลผลปิดสิ้นวัน ไม่มีคำสั่งเซฟ");
        return false;
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width:300px;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_criteria" runat="server" DataWindowObject="d_sl_closeday_criteria"
                    LibraryList="~/DataWindow/shrlonclsproc/sl_shlnproc.pbl" 
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" AutoRestoreContext="False" ClientScriptable="True"
                    ClientFormatting="True" ClientEventButtonClicked="OnDwDateClick" >
                </dw:WebDataWindowControl>
            <td>
        </tr>
    </table>
<asp:HiddenField ID="HdRunProcess" runat="server" />
</asp:Content>
