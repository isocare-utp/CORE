<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_conteck_new.aspx.cs"
    Inherits="Saving.Applications.mbshr.w_sheet_dp_conteck_new" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postAccountNo %>
    <%=jsPostPrintNewBook%>
    <script type="text/javascript">

        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                Gcoop.SetLastFocus("check_no_0"); // _0 ¤×Í á¶Ç
                Gcoop.Focus();
            }
        }



        function OnDwMainItemChanged(s, r, c, v) {
            if (c == "check_no") {
                objDwMain.SetItem(r, c, v);
                objDwMain.AcceptText();
                postAccountNo();

            }

        }

        function PrintBook() {
            var mem_no = Gcoop.GetEl("Hdmem_no").value;
            if (mem_no != "") {
                Gcoop.OpenIFrame(900, 550, "w_dlg_dp_printbook_new.aspx", "?mem_no=" + mem_no);
            }
        }

        function PrintNewBook() {
            if (confirm("ต้องการพิมพ์ปกสมดใช่หรือไม่?")) {
                jsPostPrintNewBook();
            }
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }

        function OnDwDetailClicked(s, r, c) {
            if (r > 0 && c != "datawindow") {
                var v = s.GetItem(r, "seq_no");
                Gcoop.GetEl("HdClickedSeqNo").value = v + "";
                Gcoop.GetEl("ShifF11").value = "ออกสมุดใหม่ [เริ่มพิมพ์จากลำดับ " + v + "]";
                Gcoop.GetEl("ShifF11").style.visibility = "visible";

            }
        }

        function DialBookNew() {
            Gcoop.GetEl("ShifF11").style.visibility = "hidden";
            var deptAccountNo = Gcoop.GetEl("Hdmem_no").value;
            var deptPassBookNo = "sharepassbook_no";
            var seqNo = Gcoop.GetEl("HdClickedSeqNo").value;
            try {
                deptPassBookNo = objDwMain.GetItem(1, "sharepassbook_no");
                //deptPassBookNo = Gcoop.StringFormat(Gcoop.ParseInt(deptPassBookNo), "0000000000");
            } catch (err) { deptPassBookNo = ""; }
            if (deptAccountNo != "") {
                var deptTypeCode = Gcoop.GetEl("HdDeptTypeCode").value;
                Gcoop.OpenIFrame(450, 150, "w_dlg_dp_booknew.aspx", "?member_no=" + deptAccountNo + "&deptPassBookNo=" + deptPassBookNo + "&deptTypeCode=" + deptTypeCode + "&seqNo=" + seqNo);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_conntack_main"
                    LibraryList="~/DataWindow/mbshr/sl_contack.pbl" ClientEventItemChanged="OnDwMainItemChanged">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="12px"
                    Text="รายการเคลื่อนไหว"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwDetail" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" DataWindowObject="d_conntack_detail"
                    LibraryList="~/DataWindow/mbshr/sl_contack.pbl" RowsPerPage="10" ClientScriptable="True"
                    ClientFormatting="True" ClientEventClicked="OnDwDetailClicked">
                    <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
                        <BarStyle HorizontalAlign="Center" />
                        <NumericNavigator FirstLastVisible="True" />
                    </PageNavigationBarSettings>
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td height="10">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <input id="ShifF5" type="button" value="พิพม์ปกสมุด" onclick="PrintNewBook()" style="height: 40px;
                    width: 100px;" />
                <input id="ShifF7" type="button" value="พิมพ์ BOOK" onclick="PrintBook()" style="height: 40px;
                    width: 100px;" />
                <input id="ShifF11" type="button" value="ÍÍ¡ÊÁØ´ãËÁè" onclick="DialBookNew()" style="height: 40px;
                    width: 220px; visibility: hidden;" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="Hdmem_no" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" />
    <asp:HiddenField ID="HdClickedSeqNo" runat="server" />
    <asp:HiddenField ID="HdDeptTypeCode" runat="server" />
</asp:Content>
