<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_confirm_rcvproc_wizard.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_dlg_confirm_rcvproc_wizard" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=jsProcess %>
    <%=typeProcStatus%>
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
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function Click_process(s,r,c){
            if(c=="b_process"){      
             Gcoop.OpenDlg("250","180", "w_dlg_RunCfbalProcess_progessbar.aspx","");
             objdw_criteria.AcceptText();
             jsProcess();
             
            }
        
        }
        function CheckClick(s, r, c) { 
            if(c == "pcshare_flag"){
                Gcoop.CheckDw(s, r, c, "pcshare_flag", 1, 0);
            }else  if(c == "pcloan_flag"){
                Gcoop.CheckDw(s, r, c, "pcloan_flag", 1, 0);
            }else if(c == "pcdeposit_flag"){
                Gcoop.CheckDw(s, r, c, "pcdeposit_flag", 1, 0);
            }else if(c == "pccoll_flag"){
                Gcoop.CheckDw(s, r, c, "pccoll_flag", 1, 0);
            }       

        }
        function ItemChanged(s, r, c, v){
            if(c == "random_type"){
                objdw_criteria.SetItem(r,c,v);
                objdw_criteria.AcceptText();
                typeProcStatus();
                
            }
            else if(c == "range_type"){
                objdw_criteria.SetItem(r,c,v);
                objdw_criteria.AcceptText();
                typeProcStatus();
            
            }

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>

    <table style="width:100%;">

        <tr>
            <td colspan="3">
                
                <dw:WebDataWindowControl ID="dw_criteria" runat="server" DataWindowObject="d_lnproc_cfbal_criteria"
                    LibraryList="~/DataWindow/shrlon/sl_shlnproc.pbl" 
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" AutoRestoreContext="False" ClientScriptable="True"
                    ClientFormatting="True" ClientEventButtonClicked="Click_process" ClientEventClicked="CheckClick" ClientEventItemChanged="ItemChanged">
                </dw:WebDataWindowControl>
            </td>
        </tr>

    </table>


</asp:Content>
