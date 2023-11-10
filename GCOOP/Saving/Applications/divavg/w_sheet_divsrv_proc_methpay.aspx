<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_divsrv_proc_methpay.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_divsrv_proc_methpay" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postNewClear%>
    <%=postProcess %>
    <%=postRefresh%>
    <script>function postRefresh() { __doPostBack('__Page', 'postRefresh') }</script>
    <script type="text/JavaScript">
        //Function Main

        function OnDwOptionButtonClick(s, r, b) {
            if (b == "b_process") {
                B_ProcessClick();
            }
            else if (c == "proc_type") {
                postRefresh();
            }
        }

        function B_ProcessClick() {
            //var isconfirm = confirm("ต้องการประมวลวิธีจ่ายปันผลเฉลี่ยคืน ใช่หรือไม่ ?");
            //if (!isconfirm) {
            //    return false;
            //}
            postProcess();
        }

        function OnDwOptionItemChange(s, r, c, v) {
            s.SetItem(r, c, v);
            s.AcceptText();
            if (c == "proc_type") {
                postRefresh();
            }
        }
        //Function Default
        //=============================================================
        function Validate() {

        }

        function MenubarOpen() {

        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("Hd_process").value == "true") {
                Gcoop.OpenProgressBar("ประมวลวิธีจ่ายปันผล - เฉลี่ยคืน", true, true, ProcComplete);
            }
        }

        function MenubarNew() {
            postNewClear();
        }

        function ProcComplete() {
            postNewClear();
        } 

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width:100%;">
            <tr>
                <td colspan="3">
                    <dw:WebDataWindowControl ID="Dw_option" runat="server" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" 
                        ClientScriptable="True" DataWindowObject="d_divsrv_prc_methpay_option" 
                        LibraryList="~/DataWindow/divavg/divsrv_proc_methpay.pbl" 
                        ClientEventItemChanged="OnDwOptionItemChange" 
                        ClientEventButtonClicked="OnDwOptionButtonClick">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:HiddenField ID="Hd_process" runat="server" /> &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </p>
    <%=outputProcess%>
</asp:Content>
