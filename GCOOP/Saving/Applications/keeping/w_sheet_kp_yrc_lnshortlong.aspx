<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_kp_yrc_lnshortlong.aspx.cs"
    Inherits="Saving.Applications.keeping.w_sheet_kp_yrc_lnshortlong" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=jsProcess %>
    <%=clearProcess%>
    <%=postSetDate%>
    <script type="text/javascript">
        function Validate() {
            alert("หน้าจอประมวลผลจัดทำหนี้สั้นยาว ไม่มีคำสั่งเซฟ");
            return false;
        }

        function ProcessFinish() {
            RemoveIFrame();
            clearProcess();
        }

        function Click_process(s, r, c) {
            if (c == "b_process") {
                //Gcoop.OpenDlg("250","180", "w_dlg_RunRcvProcess_Progressbar.aspx","");
                objdw_criteria.AcceptText();
                jsProcess();
            }
        }

        function OnDwCriteriaItemChange(s, r, c, v) {
            if (c == "acc_year") {
                objdw_criteria.SetItem(r, c, v);
                objdw_criteria.AcceptText();
                postSetDate();
            }
            else if (c == "acc_month") {
                objdw_criteria.SetItem(r, c, v);
                objdw_criteria.AcceptText();
                postSetDate();
            }
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdRunProcess").value == "true") {
                Gcoop.OpenProgressBar("ประมวลผลจัดทำหนี้สั้นยาว", true, true, ProcessFinish);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="dw_criteria" runat="server" DataWindowObject="d_lnproc_shtlong_criteria_gsb"
                    LibraryList="~/DataWindow/keeping/keeping.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="True" ClientEventButtonClicked="Click_process" ClientEventItemChanged="OnDwCriteriaItemChange">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdRunProcess" runat="server" />
    <%=outputProcess%>
</asp:Content>
