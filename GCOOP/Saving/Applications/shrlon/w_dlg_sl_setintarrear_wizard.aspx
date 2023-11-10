<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_sl_setintarrear_wizard.aspx.cs" Inherits="Saving.Applications.shrlon.w_dlg_sl_setintarrear_wizard" Title="Untitled Page" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsprocintset%>
    <style type="text/css">
        .style3
        {
            width: 198px;
        }
        .style4
        {
            width: 47px;
        }
    </style>

    <script type="text/javascript">
        function ItemChanged(s, r, c, v){
            //tofrom_accid
            if(c == "intsetdata_type"){
                objDwMain.SetItem(r, c, v);
                objDwMain.AcceptText();
            }
            else if( c == "calintto_tdate" ){
                objDwMain.SetItem(1, "calintto_tdate", v);
                objDwMain.AcceptText();
                objDwMain.SetItem(1, "calintto_date", Gcoop.ToEngDate(v));
                objDwMain.AcceptText();
            }
        }
        
        function OpenProgressDlg(){
            Gcoop.OpenDlg("250","180", "w_dlg_sl_setintarrear_progressbar.aspx","");
        }
        function OnEventClick(s, r, c){
            Gcoop.CheckDw(s, r, c, "request_status", 1, 0);
            if ( c == "b_accept"){
                objdw_main.AcceptText();
                objdw_loantype.AcceptText();
                jsprocintset();
            }
        }
        function CallProcess(s,r,c){
            GetLoabType();
            jsprocintset();
            //OpenProgressDlg();
        }
        
        function GetLoabType(){
            //ทำ loantype_text ที่ js แทนเลย ต่อสตริง เซตให้ dwmain.setitem(1, "loantype_text", xx)
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal><br />
    <input id="Button1" type="button" value="ดูสถานะการประมวลผล" onclick="OpenProgressDlg()" />
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                
                <br />
                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_lnproc_intset_criteria"
                    LibraryList="~/DataWindow/shrlon/sl_setintarrear_wizard.pbl" AutoSaveDataCacheAfterRetrieve="True"
                    AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
                    ClientEventItemChanged="ItemChanged" ClientEventClicked="OnEventClick" 
                    ClientFormatting="True" Height="400px" Width="330px">
                </dw:WebDataWindowControl>
            </td>
            <td>
                <dw:WebDataWindowControl ID="dw_loantype" runat="server" DataWindowObject="d_lnproc_intset_loantype"
                    LibraryList="~/DataWindow/shrlon/sl_setintarrear_wizard.pbl" AutoSaveDataCacheAfterRetrieve="True"
                    AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
                    ClientEventClicked="OnEventClick" 
                    ClientFormatting="True" Height="400px" Width="350px">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
</asp:Content>
