<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_sl_closeyear_wizard.aspx.cs" Inherits="Saving.Applications.shrlonclsproc.w_dlg_sl_closeyear_wizard" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript %>
<%=postCloseYear%>
<%=clearProcess%>

    <script type="text/javascript">
     function CloseYearFinish(){
        Gcoop.RemoveIFrame();
        clearProcess()
     }
     
    function OnDwDateClick(s,r,c) {
        if (c == "b_closeyear") { 
           postCloseYear();
        }
    }
    
    function SheetLoadComplete(){
        if(Gcoop.GetEl("HdRunProcess").value == "true"){
            //Gcoop.OpenIFrame(450, 200, "w_dlg_dp_closeday.aspx", "");
            Gcoop.OpenProgressBar("ประมวลผลปิดสิ้นปี", true, true, CloseYearFinish);
        }
    }
    
    function Validate() {
        alert("หน้าจอประมวลผลปิดสิ้นปี ไม่มีคำสั่งเซฟ");
        return false;
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="dw_criteria" runat="server" DataWindowObject="d_sl_closeyear_criteria"
                        LibraryList="~/DataWindow/shrlonclsproc/sl_shlnproc.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientFormatting="True" ClientEventButtonClicked="OnDwDateClick" >
    </dw:WebDataWindowControl>
<asp:HiddenField ID="HdRunProcess" runat="server" />
</asp:Content>
