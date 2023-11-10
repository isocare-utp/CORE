<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_trancut_loankeep.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_trancut_loankeep" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=PostRetriveDepttrans%>
    <%=PostCutProcess%>>
    <script type="text/javascript">
        function CloseFinsh() {
            Gcoop.RemoveIFrame();
            //window.location = Gcoop.GetUrl() + "flash/index.aspx?cmd=logout";
            //http://localhost/GCOOP/Saving/Exit.aspx
            window.location = Gcoop.GetUrl() + "Exit.aspx";
        }

        function OnDwMainButtonClick(sender, row, bName) {
            if (bName == "b_2") {
                var isConfirm = confirm("ยืนยันการผ่านรายการ");
                if (isConfirm) {
                    sender.AcceptText()
                    PostCutProcess();
                }
            }
            if (bName == "b_1") {
                sender.AcceptText()
                PostRetriveDepttrans();

            }
            return 0;
        }


        function SheetLoadComplete() {
            //  alet(Gcoop.GetEl("HdMountCut").value);
            if (Gcoop.GetEl("HdMountCut").value == "true") {
                //Gcoop.OpenIFrame(450, 200, "w_dlg_dp_closeday.aspx", "");

                Gcoop.OpenProgressBar("ประมวลผลตัดยอดเงินฝาก", true, true, PostRetriveDepttrans);
                Gcoop.GetEl("HdMountCut").value = "false";
            }
            else if (Gcoop.GetEl("HdSkipError").value == "true") {
                confirm("มีข้อผิดพลาดในการโอนเงินกู้เข้าฝาก ต้องการข้ามไปทำรายการถัดไปหรือไม่")
            }
        }

        function Validate() {
            alert("หน้าจอผ่านรายการ ไม่มีคำสั่งเซฟ");
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="Dw_Main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dw_procdeptupmonth"
                    LibraryList="~/DataWindow/ap_deposit/dp_depttrans.pbl" ClientEventButtonClicked="OnDwMainButtonClick"
                    ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="Dw_Detail" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" DataWindowObject="d_dp_trancut_loankeep"
                    LibraryList="~/DataWindow/ap_deposit/dp_depttrans.pbl" RowsPerPage="20" ClientScriptable="True"
                    ClientFormatting="True">
                    <PageNavigationBarSettings Position="Bottom" Visible="True" NavigatorType="Numeric">
                        <BarStyle HorizontalAlign="Center" />
                        <NumericNavigator FirstLastVisible="True" />
                    </PageNavigationBarSettings>
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdMountCut" runat="server" />
    <asp:HiddenField ID="HdSkipError" runat="server" />
</asp:Content>
