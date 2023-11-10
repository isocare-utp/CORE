<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_conteck.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_conteck" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=newClear%>
    <%=postAccountNo%>
    <script type="text/javascript">
        function CommitPrintFirstPage() {
            Gcoop.RemoveIFrame();
        }

        function UpNewBookFinish(newBookNo) {
            objDwMain.SetItem(1, "deptpassbook_no", newBookNo);
            Gcoop.RemoveIFrame();
        }

        function DialBookNew() {
            Gcoop.GetEl("ShifF11").style.visibility = "hidden";
            var deptAccountNo = Gcoop.GetEl("HdDeptAccountNo").value;
            var deptPassBookNo = "deptpassbook_no";
            var seqNo = Gcoop.GetEl("HdClickedSeqNo").value;
            try {
                //deptPassBookNo = objDwMain.GetItem(1, "deptpassbook_no");
                //deptPassBookNo = Gcoop.StringFormat(Gcoop.ParseInt(deptPassBookNo), "0000000000");
                deptPassBookNo = Gcoop.GetEl("HdDeptPassNo").value;  
            } catch (err) { deptPassBookNo = ""; }
            if (deptAccountNo != "") {
                var deptTypeCode = Gcoop.GetEl("HdDeptTypeCode").value;
//                alert(deptTypeCode);
                Gcoop.OpenIFrame(450, 150, "w_dlg_dp_booknew.aspx", "?deptAccountNo=" + deptAccountNo + "&deptPassBookNo=" + deptPassBookNo + "&deptTypeCode=" + deptTypeCode + "&seqNo=" + seqNo);
            }
        }

        function DialogPrint() {
            var accno = objDwMain.GetItem(1, "deptaccount_no");
            var row = objDwDetail.RowCount();
            var coopid = objDwMain.GetItem(1, "coop_id");
            var seqno = -1;
            for (i = 1; i <= row; i++) {
                if (objDwDetail.GetItem(i, "prntopb_status") == 0) {
                    seqno = objDwDetail.GetItem(i, "seq_no");
                    break;
                }
            }
            var seqnoend = "";
            seqnoend = objDwDetail.GetItem(row, "seq_no");
            if (seqno != -1) {
                var end = seqnoend;
                Gcoop.OpenDlg(400, 200, "w_dlg_dp_dpgetpb_lastpageline.aspx", "?accno=" + accno + "&seqno=" + seqno + "&seqnoend=" + end + "&coopid =" + coopid);
            }
        }

        function MenubarNew() {
            if (confirm("ยืนยันการล้างหน้าจอ")) {
                newClear();
            }
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame(900, 600, "w_dlg_dp_account_search.aspx", "?coopid=" + objDwMain.GetItem(1, "coop_id"));
        }

        function NewAccountNo(coopId, accNo) {
            if (coopId != undefined && coopId != null) {
                objDwMain.SetItem(1, "coop_id", coopId);
            }
            Gcoop.GetEl("Hdeptpass_flag").value = objDwMain.GetItem(1, "deptpass_flag");
            objDwMain.SetItem(1, "deptaccount_no", Gcoop.Trim(accNo));
            objDwMain.AcceptText();
            Gcoop.GetEl("HdnewAcc").value = accNo;
            postAccountNo();
        }

        function OnDwMainItemChanged(s, r, c, v) {
            if (c == "deptaccount_no") {
                objDwMain.SetItem(r, c, v);
                objDwMain.AcceptText();
                NewAccountNo(null, v);
                return 0;
            } else if (c == "deptpass_flag") {
                objDwMain.SetItem(1, "deptpass_flag", v);
                Gcoop.GetEl("Hdeptpass_flag").value = v;
                objDwMain.AcceptText();

            }
        }

        function PrintNewBook() {

            var deptAccountNo = Gcoop.GetEl("HdDeptAccountNo").value;
            //var deptPassBookNo = objDwMain.GetItem(1, "deptpassbook_no") + "";
            var deptPassBookNo = Gcoop.GetEl("HdDeptPassNo").value;  
//            alert(deptPassBookNo)

            //deptPassBookNo = Gcoop.StringFormat(Gcoop.ParseInt(deptPassBookNo), "0000000000"); //by p num แปลงแล้วค่าผิด edit 3/12/2555

            //alert(deptPassBookNo);
            Gcoop.OpenIFrame(450, 110, "w_iframe_dp_printfirstpage.aspx", "?deptAccountNo=" + deptAccountNo + "&deptPassBookNo=" + deptPassBookNo);
        }

        function PrintBook() {
            var deptAccountNo = Gcoop.GetEl("HdDeptAccountNo").value;
            alert(deptAccountNo);
            if (deptAccountNo != "") {
                Gcoop.OpenIFrame(900, 550, "w_dlg_dp_printbook.aspx", "?deptAccountNo=" + deptAccountNo);
            }
        }

        function PrintBookReload() {
            postAccountNo();
        }

        function SheetLoadComplete() {
            var isPostBack = Gcoop.GetEl("HdIsPostBack").value == "true";
            if (!isPostBack) {
                Gcoop.Focus("deptaccount_no_0");
            }
        }

        function OnDwDetailClicked(s, r, c) {
            if (r > 0 && c != "datawindow") {
                var v = s.GetItem(r, "seq_no");
                Gcoop.GetEl("HdClickedSeqNo").value = v + "";
                Gcoop.GetEl("ShifF11").value = "ออกสมุดใหม่ [เริ่มพิมพ์จากลำดับ " + v + "]";
                Gcoop.GetEl("ShifF11").style.visibility = "visible";
            }
        }

        function Validate() {
            return confirm("ต้องการบันทึก ใช่หรือไม่ ?");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dept_cancle_master"
                    LibraryList="~/DataWindow/ap_deposit/dp_conteck.pbl" ClientEventItemChanged="OnDwMainItemChanged">
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
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" DataWindowObject="d_dp_dept_contack_item"
                    LibraryList="~/DataWindow/ap_deposit/dp_conteck.pbl" RowsPerPage="15" ClientScriptable="True"
                    ClientFormatting="True" ClientEventClicked="OnDwDetailClicked">
                    <PageNavigationBarSettings NavigatorType="NumericWithQuickGo">
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
                <input id="ShifF5" type="button" value="พิมพ์ปกสมุด" onclick="PrintNewBook()" style="height: 40px;
                    width: 100px;" />
                <input id="ShifF7" type="button" value="พิมพ์ BOOK" onclick="PrintBook()" style="height: 40px;
                    width: 100px;" />
                <input id="ShifF11" type="button" value="ออกสมุดใหม่" onclick="DialBookNew()" style="height: 40px;
                    width: 220px; visibility:hidden;" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdDwDetailRow" runat="server" />
    <asp:HiddenField ID="HdDeptAccountNo" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" />
    <asp:HiddenField ID="HdDeptTypeCode" runat="server" />
    <asp:HiddenField ID="HdMemcoopDlg" runat="server" />
    <asp:HiddenField ID="HdnewAcc" runat="server" />
    <asp:HiddenField ID="Hdeptpass_flag" runat="server" />
    <asp:HiddenField ID="HdDeptPassNo" runat="server" />
    <asp:HiddenField ID="HdClickedSeqNo" Value="" runat="server" />
</asp:Content>
