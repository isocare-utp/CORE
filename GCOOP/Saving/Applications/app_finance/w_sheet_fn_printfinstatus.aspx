<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_fn_printfinstatus.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_fn_printfinstatus" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=Getstring%>
    <%=runProcess%>
    <%=popupReport%>
    <%=postSaveItem %>
    <%=postGetList %>
    <%=postOpenEdit %>
    <%=postjsExptxt %>
    <%=postSetSort %>
    <%=postSelectMemb%>
    <%=postRefresh%>
    <%=postFillReject %>
    <script type="text/javascript">

        //        function Validate() {
        //            return confirm("ยืนยันการบันทึกข้อมูล");
        //        }
        function ondwmaimclick() {
            Getstring();
        }

        function MenubarOpen() {
            Gcoop.OpenDlg(670, 570, "w_dlg_billpayment_search.aspx", "");
        }

        function printclick() {
            objdw_main.AcceptText();
            runProcess();
        }

        function passitem() {
            postSaveItem();
        }
        function jsexptext() {
            postjsExptxt();
        }

        //        function searchbank() {
        ////            alert("test");
        //            Gcoop.OpenDlg(700, 250, "w_dlg_bankaccount_search.aspx", "");
        //        }

        function SheetLoadComplete() {
            var pageCommand = Gcoop.GetEl("HfPageCommand").value;
            if (Gcoop.GetEl("HdOpenIFrame").value == "True") {
                Gcoop.OpenIFrame("220", "200", "../../../Criteria/dlg/w_dlg_report_progress.aspx?&app=<%=app%>&gid=<%=gid%>&rid=<%=rid%>&pdf=<%=pdf%>", "");
                Gcoop.GetEl("HfPageCommand").value = "";
            }
            if (pageCommand == "opendialog") {
                if (objdw_main.RowCount() > 0 && Gcoop.GetEl("HfRow").value != "") {
                    Gcoop.GetEl("HfPageCommand").value = "";
                    Gcoop.OpenIFrame(570, 250, 'w_dlg_billpayment_edit.aspx', '');                    
                } else {
                    alert("ไม่มีข้อมูล ไม่สามารถทำรายการได้");
                    Gcoop.GetEl("HfPageCommand").value = "";
//                    return false;
                }
            }

            if (Gcoop.GetEl("hf_sorttype").value == "DESC") {
                Gcoop.GetEl("rd_desc").checked = true;
            } else {
                Gcoop.GetEl("rd_asc").checked = true;
            }

            Gcoop.GetEl("sl_item").selectedIndex = Gcoop.GetEl("IndexItem").value;
            Gcoop.GetEl("sl_reject").value = Gcoop.GetEl("hf_reject").value;

            for (var i = 1; i <= objdw_main.RowCount(); i++) {
                objdw_main.SelectRow(i, false);
            }
            var rowNumber = Gcoop.GetEl("HfRow").value;
            objdw_main.SelectRow(rowNumber, true);
            objdw_main.SetRow(rowNumber);

        }

        function DwMainClick(sender, rowNumber, objectName) {
            //Gcoop.OpenDlg(670, 570, "w_dlg_billpayment_edit.aspx", "");
            Gcoop.GetEl("HfRow").value = rowNumber;
//            var rowNumber = Gcoop.GetEl("HfRow").value;
            objdw_main.SelectRow(rowNumber, true);
            objdw_main.SetRow(rowNumber);
            objdw_main.AcceptText();
            postRefresh();
            //            postOpenEdit();
        }


        function GetDlgBillPaymentSearch(file_docno, bank_code, branch_code, account_no, as_billitem) {
            //            alert(file_docno + "" + bank_code + "" + branch_code + "" + account_no + "" + as_billitem);
            Gcoop.GetEl("hf_sorttype").value = "ASC";
            Gcoop.GetEl("IndexItem").value = "0";

            Gcoop.GetEl("HfFileDocno").value = file_docno;
            Gcoop.GetEl("HfBank").value = bank_code;
            Gcoop.GetEl("HfBranch").value = branch_code;
            Gcoop.GetEl("HfAccountNo").value = account_no;
            Gcoop.GetEl("HfBillItem").value = as_billitem;
            objdw_maccid.AcceptText();
            postGetList();
        }

        function GetDlgBillPaymentEdit(file_docno, bank_code, branch_code, account_no, as_billitem) {
            //            alert(file_docno + "" + bank_code + "" + branch_code + "" + account_no + "" + as_billitem);

            Gcoop.GetEl("HfFileDocno").value = file_docno;
            Gcoop.GetEl("HfBank").value = bank_code;
            Gcoop.GetEl("HfBranch").value = branch_code;
            Gcoop.GetEl("HfAccountNo").value = account_no;
            Gcoop.GetEl("HfBillItem").value = as_billitem;
            objdw_maccid.AcceptText();
            postGetList();
        }

        function SetSortItem() {
            var RadioValue;
            if (Gcoop.GetEl("rd_asc").checked == true) {
                RadioValue = Gcoop.GetEl("rd_asc").value;
            } else {
                RadioValue = Gcoop.GetEl("rd_desc").value;
            }
            if (Gcoop.GetEl("sl_item").value == "payment_date") {
                Gcoop.GetEl("hf_textsort").value = Gcoop.GetEl("sl_item").value + " " + RadioValue + ", payment_time " + RadioValue;
            } else {
                Gcoop.GetEl("hf_textsort").value = Gcoop.GetEl("sl_item").value + " " + RadioValue;
            }
            Gcoop.GetEl("IndexItem").value = Gcoop.GetEl("sl_item").selectedIndex;
            Gcoop.GetEl("hf_sorttype").value = RadioValue;
            Gcoop.GetEl("hf_sortitem").value = Gcoop.GetEl("sl_item").value;
            // alert("qq");
            if (objdw_main.RowCount() > 0) {
                postSetSort();

            } else {postRefresh(); }
        }

        function ButtonClikedSelectMem(sender, rowNumber, butttonName) {
            if (butttonName == "b_selectmb") {
                postSelectMemb();
            }
        }

        function SetReject() {
            Gcoop.GetEl("hf_reject").value = Gcoop.GetEl("sl_reject").value;
            if (objdw_main.RowCount() > 0) {
                postFillReject();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HfRow" runat="server" Value="" />
    <asp:HiddenField ID="HdfTextpath" runat="server" Value="" />
    <asp:HiddenField ID="HdOpenIFrame" runat="server" Value="False" />
    <asp:HiddenField ID="HfFileDocno" runat="server" />
    <asp:HiddenField ID="HfBank" runat="server" />
    <asp:HiddenField ID="HfBranch" runat="server" />
    <asp:HiddenField ID="HfAccountNo" runat="server" />
    <asp:HiddenField ID="HfPageCommand" runat="server" />
    <asp:HiddenField ID="HfBillItem" runat="server" />
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_finstatus_main"
                    LibraryList="~/DataWindow/app_finance/start_day.pbl" ClientScriptable="True"
                    AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    Width="600px" ClientEventButtonClicked="ButtonClikedSelectMem">
                </dw:WebDataWindowControl>
                <%--<input id="banksearch" type="button" onclick="searchbank();" value="ค้นเลขที่บัญชี ธ." style="margin-left:365px;"/>--%>
                <%--<input id="fiUpload" type="file" onchange="ondwmaimclick();" />--%>
                <%--<input id="btnUpload" type="button" onclick="ondwmaimclick();" value="Upload" />--%>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <%--<dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_fn_billpayment_table"
                    LibraryList="~/DataWindow/app_finance/billpayment.pbl" ClientScriptable="True"
                    AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    Width="680" VerticalScrollBar="Auto" Height="500" BorderWidth="1" ClientEventClicked="DwMainClick">
                </dw:WebDataWindowControl>--%>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <input id="printpast" type="button" onclick="printclick();" value="พิมพ์รายการ" />
                <input id="saveitem" type="button" onclick="passitem();" value="ผ่านรายการ" />
                <input id="exptxt" type="button" onclick="jsexptext();" value="Export Text SMS" />&nbsp;
                <asp:HiddenField ID="hf_textsort" runat="server" Value="" />
                <asp:HiddenField ID="hf_sorttype" runat="server" Value="" />
                <asp:HiddenField ID="hf_sortitem" runat="server" Value="" />
                <asp:HiddenField ID="IndexItem" runat="server" Value="" />
                <asp:HiddenField ID="hf_reject" runat="server" Value="" />
            </td>
        </tr>
    </table>
</asp:Content>
