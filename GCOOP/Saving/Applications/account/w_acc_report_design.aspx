<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_report_design.aspx.cs"
    Inherits="Saving.Applications.account.w_acc_report_design" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postMoneyCode %>
    <%=postInsertRow%>
    <%=postUpBotton %>
    <%=postDownBotton%>
    <%=postInsertAfterRow%>
    <%=postDeleterow%>
    <%=postNewClear%>
    <%=postPost%>
    <script type="text/javascript">

        function Validate() {
            objDw_master.AcceptText();
            objDw_detail.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }

        function ConfirmDelete(row) {
            Gcoop.GetEl("HdRowDelete").value = row + "";
            postDeleterow();
        }

        function MenubarNew() {
            if (confirm("ยืนยันการล้างหน้าจอ")) {
                window.location = "";
            }
        }

        function MenubarOpen() {
            Gcoop.OpenDlg(400, 500, "w_dlg_acc_report_design_head_response.aspx", "");
        }

        function GetValueDlg(moneysheet_code) {
            objDw_master.SetItem(1, "moneysheet_code", Gcoop.Trim(moneysheet_code));
            objDw_master.AcceptText();
            Gcoop.GetEl("HdMoneyCode").value = Gcoop.Trim(moneysheet_code);
            postMoneyCode();
        }

        function OnDw_masterItemChange(s, r, c, v)//เปลี่ยนวันที่
        {
            if (c == "moneysheet_code") {
                objDw_master.SetItem(r, "moneysheet_code", v);
                objDw_master.AcceptText();
                if (v == "" || v == null) {
                    alert("กรุณากรอกข้อมูลรหัสงบการเงิน")
                } else {
                    Gcoop.GetEl("HdMoneyCode").value = Gcoop.Trim(v);
                    postMoneyCode();
                }
            }
        }

        // ฟังก์ชันการลบข้อมูล
        function OnDwDetailBottonClick(s, r, c) {
            if (c == "b_del") {
                if (r > 0) {
                    //                    Gcoop.OpenIFrame(350, 150, "d_dlg_configm_delete.aspx", "?delete_row=" + r);
                    var isConfirm = confirm("ต้องการลบข้อมูลแถวที่ " + r + " ใช่หรือไม่");
                    if (isConfirm) {
                        Gcoop.GetEl("HdRowDelete").value = r + "";
                        postDeleterow();
                    }
                }
            }
            else if (c == "b_up") {
                Gcoop.GetEl("HdRowClick").value = r + "";
                postUpBotton();
            }
            else if (c == "b_down") {
                Gcoop.GetEl("HdRowClick").value = r + "";
                postDownBotton();
            }
        }

        function OnDwDetailItemChange(s, r, c, v) {
            if (c == "sort_order") {
                s.SetItem(r, c, v);
                s.AcceptText();
                postPost();
            }
            return 0;
        }

        //ฟังก์ชันการส่ง currentrow ให้ Hiden Field
        //     function Dw_detailItemFocusChanged(s,r,c) {
        //        Gcoop.GetEl("HdRowCurrent").value = r + "";
        //     }

        function OnDw_detailClick(s, r, c) {
            Gcoop.GetEl("HdRowCurrent").value = r + "";
            if (c == "cnt_moneydet") {
                Gcoop.CheckDw(s, r, c, "cnt_moneydet", 1, 0);
            } else if (c == "show_status") {
                Gcoop.CheckDw(s, r, c, "show_status", 1, 0);
            } else if (c == "show_det_status1") {
                Gcoop.CheckDw(s, r, c, "show_det_status1", 1, 0);
            } else if (c == "show_det_status3") {
                Gcoop.CheckDw(s, r, c, "show_det_status3", 1, 0);
            } else if (c == "up_line") {
                Gcoop.CheckDw(s, r, c, "up_line", 1, 0);
            }
            return 0;
        }

        function OnDw_masterClick(s, r, c) {
            if (c == "percent_status") {
                Gcoop.CheckDw(s, r, c, "percent_status", 1, 0);
            }
            return 0;
        }

        function Dw_detailInsertRow() {
            postInsertRow();
        }

        function Dw_detailInsertAfterRow() {
            postInsertAfterRow();
        }

        //     //ฟังก์ชันการส่ง currentrow ให้ Hiden Field
        //     function Dw_detailItemFocusChanged(s,r,c) {
        //        Gcoop.GetEl("HdCurrentrow").value = r + "";
        //     }

        function SheetLoadComplete() {
            SetFocusDWListClick("Dw_detail");
        }

        function SetFocusDWListClick(Dwobj) {
            var idx = Number(Gcoop.GetEl("HdRowCurrent").value) - 1;
            if (idx >= 0) {
                var sel = "#obj" + Dwobj + "_datawindow input[name='description_" + idx + "']";
                $(sel).focus();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%; font-size: small;">
            <tr>
                <td colspan="3">
                    <dw:WebDataWindowControl ID="Dw_master" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        DataWindowObject="d_acc_report_design_master_set" LibraryList="~/DataWindow/account/acc_report_design.pbl"
                        ClientEventItemChanged="OnDw_masterItemChange" ClientEventClicked="OnDw_masterClick">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="400px" Width="750px"
                        BorderStyle="Ridge">
                        <dw:WebDataWindowControl ID="Dw_detail" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            DataWindowObject="d_acc_report_design_detail_set" LibraryList="~/DataWindow/account/acc_report_design.pbl"
                            ClientEventButtonClicked="OnDwDetailBottonClick" ClientEventClicked="OnDw_detailClick" ClientEventItemChanged="OnDwDetailItemChange">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <input id="B_add" type="button" value="เพิ่มแถว" onclick="Dw_detailInsertRow()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="B_insert" type="button" value="แทรกแถว" onclick="Dw_detailInsertAfterRow()" />&nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="HdRowClick" runat="server" />
                    <asp:HiddenField ID="HdRowCurrent" runat="server" />
                    <asp:HiddenField ID="HdMoneyCode" runat="server" />
                    <asp:HiddenField ID="HdRowDelete" runat="server" />
                    <asp:HiddenField ID="HdRowInsert" runat="server" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
