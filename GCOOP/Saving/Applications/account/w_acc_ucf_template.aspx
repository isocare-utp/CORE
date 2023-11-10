<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_acc_ucf_template.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_template" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostVouchertypeChange%>
    <%=jsPostDwmainInsertRow%>
    <%=jsPostGetDetail%>
    <%=jsPostDwmainDeleteRow%>
    <%=jsPostDwdetailInsertRow%>
    <%=jsPostAccountid%>
    <%=jsPostSelectDetail%>
    <%=jsPostDwdetailDeleteRow%>
    <%=jsPostAllDeleteRow%>
    <script type="text/javascript">
        function Validate() {
            var isconfirm = confirm("ยืนยันการบันทึกข้อมูล ?");

            if (!isconfirm) {
                return false;
            }
            var rowcount = objDwmain.RowCount();
            var rowcountd = objDwdetail.RowCount();
            var alert = "";
            for (var i = 1; i <= rowcount; i++) {
                var vcauto_code = objDwmain.GetItem(i, "tvcauto_code");
                var vcauto_desc = objDwmain.GetItem(i, "vcauto_desc");
                if (vcauto_code == "" || vcauto_code == null || vcauto_desc == "" || vcauto_desc == null) {
                    alert = "กรุณาระบุข้อมูลให้ครบถ้วน";
                    break;
                }
            }
            for (var j = 1; j <= rowcountd; j++) {
                var account_id = objDwdetail.GetItem(j, "account_id");
                if (account_id == "" || account_id == null) {
                    alert = "กรุณาระบุข้อมูลให้ครบถ้วน";
                    break;
                }
            }
            if (alert != "") {
                confirm(alert);
                return false;
            }
            else {

                return true;
            }
        }
        function OnDwheadItemChange(sender, row, col, value) {
            sender.SetItem(row, col, value);
            sender.AcceptText();
            jsPostVouchertypeChange();
        }

        function OnDwmainInsertRow() {
            var voucher_type = objDwhead.GetItem(1, "voucher_type");
            if (voucher_type != "" && voucher_type != null) {
                jsPostDwmainInsertRow();
            }
            else {
                alert("กรุณาเลือกประเภทก่อน");
            }
        }

        function OnDwmainDeleteRow() {
            if (Gcoop.GetEl("Hdrow_mas").value == "") {
                alert("กรุณาเลือกแถวที่ต้องการลบ");
            }
            else {
                var detailrow = objDwdetail.RowCount();
                if (detailrow > 0) {
                    //กรณีที่มีข้อมูลในตารางด้านขวา
                    var isconfirm = confirm("ข้อมูลรายการของ Template แถวที่ " + Gcoop.GetEl("Hdrow_mas").value + " จะถูกลบไปด้วย ต้องการลบหรือไม่");
                    if (isconfirm) {
                        jsPostAllDeleteRow();
                    }
                }
                else {
                    var isconfirm = confirm("ต้องการลบ Template แถวที่ " + Gcoop.GetEl("Hdrow_mas").value + " ใช่หรือไม่");
                    if (isconfirm) {
                        jsPostDwmainDeleteRow();
                    }
                }
            }
        }

        function OnDwmainClickRow(sender, row, col) {
            if (Gcoop.GetEl("Hdrow_mas").value != row) {
                Gcoop.GetEl("Hdrow_mas").value = row + "";
                jsPostGetDetail();
            }
        }

        function OnDwdetailInsertRow() {
            if (Gcoop.GetEl("Hdrow_mas").value != "") {
                jsPostDwdetailInsertRow();
            }
            else {
                alert("กรุณาเลือกรายการ Template ก่อน");
            }
        }

        function OnDwdetailButtonClick(sender, row, col) {
            Gcoop.GetEl("Hdrow_det").value = row + "";
            Gcoop.OpenIFrame("575", "455", "w_dlg_search_accmaster.aspx", "");
        }

        function OnFindShow(acc_id) {
            Gcoop.GetEl("Hdacc_id").value = acc_id;
            jsPostAccountid();
        }

        function OnDwdetailClickRow(sender, row, col) {
            if (Gcoop.GetEl("Hdrow_det").value != row) {
                Gcoop.GetEl("Hdrow_det").value = row + "";
                jsPostSelectDetail();
            }
        }

        function OnDwdetailDeleteRow() {
            if (Gcoop.GetEl("Hdrow_det").value == "") {
                alert("กรุณาเลือกแถวที่ต้องการลบ");
            }
            else {
                var isconfirm = confirm("ต้องการลบรายการแถวที่ " + Gcoop.GetEl("Hdrow_det").value + " ใช่หรือไม่");
                if (isconfirm) {
                    jsPostDwdetailDeleteRow();
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>

    <%--<input id="printBtn" type="button" value="พิมพ์หน้าจอ" onclick="printPage();"   />--%>

    <table style="width: 100%;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="Dwhead" runat="server" DataWindowObject="d_vc_template_vctype"
                    LibraryList="~/DataWindow/account/cm_constant_config.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="OnDwDateClicked"
                    ClientScriptable="True" ClientEventItemChanged="OnDwheadItemChange" ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <span onclick="OnDwmainInsertRow()" style="cursor: pointer;">
                    <asp:Label ID="LbInsert2" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="Blue" /></span> &nbsp;&nbsp;
                <span onclick="OnDwmainDeleteRow()" style="cursor: pointer;">
                    <asp:Label ID="LbDel2" runat="server" Text="ลบแถว" Font-Bold="False" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="Red" /></span>
            </td>
            <td>
                <span onclick="OnDwdetailInsertRow()" style="cursor: pointer;">
                    <asp:Label ID="Label1" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="Blue" /></span> &nbsp;&nbsp;
                <span onclick="OnDwdetailDeleteRow()" style="cursor: pointer;">
                    <asp:Label ID="Label2" runat="server" Text="ลบแถว" Font-Bold="False" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="Red" /></span>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Panel ID="Panel3" runat="server" Height="300px" Width="305px" BorderStyle="Ridge">
                    <dw:WebDataWindowControl ID="Dwmain" runat="server" DataWindowObject="d_vc_template_master"
                        LibraryList="~/DataWindow/account/cm_constant_config.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientFormatting="True" ClientEventClicked="OnDwmainClickRow" Height="300px" Width="305px">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
            <td valign="top">
                <asp:Panel ID="Panel4" runat="server" BorderStyle="Ridge" Height="300px" Width="400px">
                    <dw:WebDataWindowControl ID="Dwdetail" runat="server" DataWindowObject="d_vc_template_detail"
                        LibraryList="~/DataWindow/account/cm_constant_config.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" ClientEventButtonClicked="OnDwdetailButtonClick" ClientEventClicked="OnDwdetailClickRow"
                        Height="300px" Width="400px">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="Hdrow_mas" runat="server" />
    <asp:HiddenField ID="Hdrow_det" runat="server" />
    <asp:HiddenField ID="Hdacc_id" runat="server" />
    <asp:HiddenField ID="Hdyear" runat="server" />
</asp:Content>
