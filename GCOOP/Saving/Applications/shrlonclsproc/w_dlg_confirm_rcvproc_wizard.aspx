<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_confirm_rcvproc_wizard.aspx.cs"
    Inherits="Saving.Applications.shrlonclsproc.w_dlg_confirm_rcvproc_wizard" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=jsProcess %>
    <%=jsRandomType%>
    <%=jsRangeType%>
    <%=clearProcess%>
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
            alert("หน้าจอประมวลผลยืนยันยอด ไม่มีคำสั่งเซฟ");
            return false;
        }
        function ProcessFinish(){
            RemoveIFrame();
            clearProcess();   
         }
         function Click_process(s, r, c) {
             switch (c) {
                 case "b_process":
                     //Gcoop.OpenDlg("250","180", "w_dlg_RunRcvProcess_Progressbar.aspx","");
                     //objdw_criteria.AcceptText();
                     jsProcess();
                     break;
             }
        }
        function CheckClick(s, r, c) {
            switch (c) {
                case "pcshare_flag":
                    Gcoop.CheckDw(s, r, c, "pcshare_flag", 1, 0);
                    break;
                case "pcloan_flag":
                    Gcoop.CheckDw(s, r, c, "pcloan_flag", 1, 0);
                    break;
                case "pcdeposit_flag":
                    Gcoop.CheckDw(s, r, c, "pcdeposit_flag", 1, 0);
                    break;
                case "pccoll_flag":
                    Gcoop.CheckDw(s, r, c, "pccoll_flag", 1, 0);
                    break;
            }
        }
        function ItemChanged(s, r, c, v) {
            s.SetItem(r, c, v);
            s.AcceptText();
            switch (c) {
                case "random_type":
                    jsRandomType();
                    break;
                case "range_type":
                    jsRangeType();
                    break;
            }
        }
        function SheetLoadComplete(){
            if(Gcoop.GetEl("HdRunProcess").value == "true"){
                Gcoop.OpenProgressBar("ประมวลผลยืนยันยอด", true, true, ProcessFinish);
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
                    LibraryList="~/DataWindow/shrlonclsproc/sl_shlnproc.pbl" 
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" AutoRestoreContext="False" ClientScriptable="True"
                    ClientFormatting="True" ClientEventButtonClicked="Click_process" ClientEventClicked="CheckClick" ClientEventItemChanged="ItemChanged">
                </dw:WebDataWindowControl>
            </td>
        </tr>

    </table>
    <asp:HiddenField ID="HdRunProcess" runat="server" />

</asp:Content>
