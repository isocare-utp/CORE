<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_kp_ccl_adjust_monthly_prc.aspx.cs" Inherits="Saving.Applications.keeping.w_sheet_kp_ccl_adjust_monthly_prc" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postNewClear %>
    <%=postInit%>
    <%=postRefresh%>
    <script type="text/javascript">
        function Validate() {
            objDw_main.AcceptText();
            return confirm("ยืนยันการประมวลผลยกเลิกใบเสร็จประจำเดือน");
        }

        function OnDwMainButtonClick(s, r, c) {
            if (c == "b_init") {
                objDw_main.AcceptText();
                objDw_detail.AcceptText();
                postInit();
            }
            return 0;
        }

        function OnDwMainItemChange(s, r, c, v) {
            if (c == "operate_tdate") {
                objDw_main.SetItem(1, "operate_tdate", v);
                objDw_main.AcceptText();
                objDw_main.SetItem(1, "operate_date", Gcoop.ToEngDate(v));
                objDw_main.AcceptText();
            }
            else if (c == "proc_type") {
                objDw_main.SetItem(1, "proc_type", v);
                objDw_main.AcceptText();
                postRefresh();
            }
            else if (c == "proc_text") {
                objDw_main.SetItem(1, "proc_text", v);
                objDw_main.AcceptText();
            }
            return 0;
        }


        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                postNewClear();
            }
        }



        function SheetLoadComplete() {
            if (Gcoop.GetEl("Hdprocess").value == "true") {
                Gcoop.OpenProgressBar("ประมวลผลยกเลิกใบเสร็จประจำเดือน", true, true, endproc);
            }
        }

        function endproc() {
            postNewClear();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                    DataWindowObject="d_kp_adjust_monthly_prc_option" LibraryList="~/DataWindow/keeping/kp_ccl_adjust_monthly_prc.pbl"
                    ClientEventButtonClicked="OnDwMainButtonClick" ClientEventItemChanged="OnDwMainItemChange">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Panel ID="Panel1" runat="server" Height="300px">
                    <dw:WebDataWindowControl ID="Dw_detail" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" DataWindowObject="d_kp_adjust_monthly_prc_rpt_sum" LibraryList="~/DataWindow/keeping/kp_ccl_adjust_monthly_prc.pbl">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <asp:HiddenField ID="Hmember_no" runat="server" />
    <asp:HiddenField ID="Hdprocess" runat="server" />
    <asp:HiddenField ID="Hdcreate" runat="server" />
</asp:Content>
