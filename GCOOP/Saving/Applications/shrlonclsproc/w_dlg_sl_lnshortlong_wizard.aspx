<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_sl_lnshortlong_wizard.aspx.cs" Inherits="Saving.Applications.shrlonclsproc.w_dlg_sl_lnshortlong_wizard" Title="Untitled Page" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=jsProcess %>
    <%=clearProcess%>
    <%=postSetYear%>
    
    <script type="text/javascript">
        function OnItemChange(s, r, c, v) {
            if (c == "acc_year") {
                objdw_criteria.SetItem(1, "acc_year", v);
                objdw_criteria.AcceptText();
                postSetYear();   
            }
            return 0;
        }
        function Validate() {
            alert("หน้าจอจัดทำหนี้สั้นยาว ไม่มีคำสั่งเซฟ");
            return false;
        }
        function ProcessFinish(){
            RemoveIFrame();
            clearProcess();   
         }
        function Click_process(s,r,c){
            if(c=="b_process"){      
             //Gcoop.OpenDlg("250","180", "w_dlg_RunRcvProcess_Progressbar.aspx","");
             objdw_criteria.AcceptText();
             jsProcess();          
            }
        }

        function SheetLoadComplete(){
            if(Gcoop.GetEl("HdRunProcess").value == "true"){
                Gcoop.OpenProgressBar("จัดทำหนี้สั้นยาว", true, true, ProcessFinish);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width:100%;">

        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="dw_criteria" runat="server" DataWindowObject="d_lnproc_shtlong_criteria"
                    LibraryList="~/DataWindow/shrlonclsproc/sl_shlnproc.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="True" ClientEventButtonClicked="Click_process" >
                </dw:WebDataWindowControl>
            </td>
        </tr>

    </table>
<asp:HiddenField ID="HdRunProcess" runat="server" />
</asp:Content>
