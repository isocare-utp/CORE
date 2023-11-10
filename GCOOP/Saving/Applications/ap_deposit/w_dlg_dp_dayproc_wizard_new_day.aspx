<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_dp_dayproc_wizard_new_day.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_dlg_dp_dayproc_wizard_new_day" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postCloseDay%>

    <script type="text/javascript">
        function CloseDayFinish() {
            Gcoop.RemoveIFrame();
            //window.location = Gcoop.GetUrl() + "flash/index.aspx?cmd=logout";
            //http://localhost/GCOOP/Saving/Exit.aspx
            //        window.location = Gcoop.GetUrl() + "Exit.aspx";
        }

        function OnDwDateClick(sender, row, bName) {
            if (bName == "b_closeday") {
                var isConfirm = confirm("ต้องการปิดงานสิ้นวันใช่หรือไม่");
                if (isConfirm) {
                    postCloseDay();
                }
            }
            return 0;
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdCloseday").value == "true") {
                //Gcoop.OpenIFrame(450, 200, "w_dlg_dp_closeday.aspx", "");
                Gcoop.OpenProgressBar("ประมวลผลปิดสิ้นวัน", true, false, CloseDayFinish);
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
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="Dw_date" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_dayproc_closeday"
                    LibraryList="~/DataWindow/ap_deposit/dp_closeday.pbl" ClientEventClicked="OnDwDateClick"
                    ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdCloseday" runat="server" />
    <%=outputProcess%>
</asp:Content>
