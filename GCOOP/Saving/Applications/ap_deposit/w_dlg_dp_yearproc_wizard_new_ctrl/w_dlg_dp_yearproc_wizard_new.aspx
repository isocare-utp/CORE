<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_dp_yearproc_wizard_new.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_dlg_dp_yearproc_wizard_new" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postCloseYear%>
    <script type="text/javascript">
        function CloseDayFinish() {
            Gcoop.RemoveIFrame();
            //window.location = Gcoop.GetUrl() + "flash/index.aspx?cmd=logout";
            //http://localhost/GCOOP/Saving/Exit.aspx
            //        window.location = Gcoop.GetUrl() + "Exit.aspx";
        }

        function OnDsMainClicked(sender, row, bName) {
            if (bName == "b_closeyear") {
                var isConfirm = confirm("ต้องการปิดงานสิ้นปีใช่หรือไม่");
                if (isConfirm) {
                    postCloseYear();
                }
            }
            return 0;
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdCloseyear").value == "true") {
                //Gcoop.OpenIFrame(450, 200, "w_dlg_dp_closeday.aspx", "");
                //Gcoop.OpenProgressBar("ประมวลผลปิดสิ้นปี", true, false, CloseDayFinish);
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
    <uc1:DsMain ID="dsMain" runat="server" />
    <table style="width: 100%;">
        <tr>
            <%--<td colspan="3">
                <dw:WebDataWindowControl ID="Dw_date" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_yearproc_closeday"
                    LibraryList="~/DataWindow/ap_deposit/dp_closeyear.pbl" ClientEventClicked="OnDwDateClick" ClientEventItemChanged="DwDateChange"
                    ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>--%>
        </tr>
    </table>
    <asp:HiddenField ID="HdCloseyear" runat="server" />
</asp:Content>
