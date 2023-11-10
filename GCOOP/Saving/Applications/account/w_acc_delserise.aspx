<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_acc_delserise.aspx.cs" Inherits="Saving.Applications.account.w_acc_delserise" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostEdit%>
    <%=jsPostDelete%>
    <%=jsPostAccountid%>
    <%=jsPostInsertDwRate%>
    <%=jsPostDeleteDwRate%>
    <%=jsPostBlank%>
    <%=jsPostDupComplete%>
    <%=jsPostReplaceAll%>
    <script type="text/javascript">
        function Validate() {
            var isconfirm = confirm("ยืนยันการบันทึกข้อมูล?");

            if (!isconfirm) {
                return false;
            }

            var alert = "";
            var asset_doc = objDwmain.GetItem(1, "asset_doc");
            var desc_text = objDwmain.GetItem(1, "desc_text");
//            var account_id = objDwmain.GetItem(1, "account_id");
            var type_series = objDwmain.GetItem(1, "type_series");
//            var desc_why = objDwmain.GetItem(1, "desc_why");
            var before_amt = objDwmain.GetItem(1, "before_amt");
            var add_amount = objDwmain.GetItem(1, "add_amount");
            var receive_tdate = objDwmain.GetItem(1, "receive_tdate");
            var date_range = receive_tdate.trim().length;

            if (asset_doc == "" || asset_doc == null || desc_text == "" || desc_text == null || type_series == "" ||
                 type_series == null || before_amt == "" || before_amt == null || add_amount == "" || add_amount == null ||
                 receive_tdate == "00000000" || date_range != 8) {
                alert += "กรุณาระบุข้อมูลให้ครบถ้วน\n";
            }

            for (var i = 1; i <= objDwRate.RowCount(); i++) {
                var pad = function (val) { var str = val.toString(); return (str.length < 2) ? "0" + str : str };
                var dateString = "";
                var efective_date = objDwRate.GetItem(i, "efective_date");
                var ef_date_split = efective_date.toString().split("/");
                var y_ef = ef_date_split[2].substring(0, 4);
                var effect_date = [y_ef, pad(ef_date_split[0]), pad(ef_date_split[1])].join("-");

                var expire_date = objDwRate.GetItem(i, "expire_date");
                var ex_date_split = expire_date.toString().split("/");
                var y_ex = ex_date_split[2].substring(0, 4);
                var expi_date = [y_ex, pad(ex_date_split[0]), pad(ex_date_split[1])].join("-");

                if (i > 1) {
                    //Add day
                    var exp_date = objDwRate.GetItem(i - 1, "expire_date");
                    var exp_date_split = exp_date.toString().split("/");
                    var y_exp = exp_date_split[2].substring(0, 4);
                    var myDate = new Date([y_exp, pad(exp_date_split[0]), pad(exp_date_split[1])].join("-"));
                    myDate.setDate(myDate.getDate() + 1);
                    var y = myDate.getFullYear();
                    var m = myDate.getMonth() + 1;
                    var d = myDate.getDate();
                    dateString = [y, pad(m), pad(d)].join("-");
                }

                if (effect_date > expi_date || (effect_date != dateString && i > 1)) {
                    alert += "กรุณาตรวจสอบระยะวันที่อัตราค่าเสื่อม\n";
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

        function OnEditClickButton(sender, row, bName) {
            Gcoop.GetEl("Hdrow").value = row + "";
            if (bName == "b_edit") {
                jsPostEdit();
            }
            else if (bName == "b_del") {
                var asset_doc = objDwlist.GetItem(row, "asset_doc");
                if (confirm("ต้องการลบเลขทะเบียนสินทรัพย์ " + asset_doc + " ใช่หรือไม่")) {
                    jsPostDelete();
                }
            }
        }

        function OnDwmainButtonClicked(sender, row, bName) {
            if (bName == "b_search") {
                Gcoop.OpenIFrame("575", "455", "w_dlg_search_accmaster.aspx", "");
            }
            else if (bName == "b_dup") {
                var asset_doc = Gcoop.GetEl("Hdasset_doc").value;
                var receive_tdate = Gcoop.GetEl("Hdreceive_tdate").value;
                Gcoop.OpenIFrame("310", "470", "w_dlg_duplicate_asset.aspx", "?asset_doc=" + asset_doc + "&receive_tdate=" + receive_tdate);
            }
        }

        function OnFindShow(acc_id) {
            Gcoop.GetEl("Hdacc_id").value = acc_id;
            jsPostAccountid();
        }

        function InsertDwRate() {
            jsPostInsertDwRate();
        }

        function OnDwRateButtonClicked(sender, row, bName) {
            if (bName == "b_del") {
                Gcoop.GetEl("Hdrow").value = row + "";
                jsPostDeleteDwRate();
            }
        }

        function OnDwRateItemChanged(s, r, c, v)//เปลี่ยนวันที่
        {
            s.SetItem(r, c, v);
            s.AcceptText();
            if (c == "efective_tdate" || c == "expire_tdate") {
                jsPostBlank();
            }
        }

        function DuplicateComplete() {
            jsPostDupComplete();
        }

        function SheetLoadComplete() {
//            if (Gcoop.GetEl("IsOpenDuplicate").value == "true") {
//                if (confirm("ต้องการคัดลอกสินทรัพย์หรือไม่")) {
//                    var asset_doc = Gcoop.GetEl("Hdasset_doc").value;
//                    var receive_tdate = Gcoop.GetEl("Hdreceive_tdate").value;
//                    Gcoop.OpenIFrame("310", "470", "w_dlg_duplicate_asset.aspx", "?asset_doc=" + asset_doc + "&receive_tdate=" + receive_tdate);
//                }
//                Gcoop.GetEl("IsOpenDuplicate").value = "false";
//            }
//            else if (Gcoop.GetEl("IsReplaceAll").value == "true") {
//                Gcoop.GetEl("IsReplaceAll").value = "false";
//                if (confirm("ต้องการเปลี่ยนเลขที่ Lot สินทรัพย์ " + Gcoop.GetEl("HdAssGroup").value + " เป็น " + Gcoop.GetEl("HdAssGroup_new").value + " ทุกรายการหรือไม่")) {
//                    jsPostReplaceAll();
//                }
//            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="2">
                <dw:WebDataWindowControl ID="Dwmain" runat="server" DataWindowObject="d_acc_add_newasset"
                    LibraryList="~/DataWindow/account/asset.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="OnDwmainButtonClicked"
                    ClientScriptable="True" ClientEventItemChanged="OnDwmainItemChange" ClientFormatting="True"
                    TabIndex="1">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <span onclick="InsertDwRate()" style="cursor: pointer;">
                    <asp:Label ID="LbInsert2" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="Blue" /></span>
                <dw:WebDataWindowControl ID="DwRate" runat="server" DataWindowObject="d_acc_dp_rate"
                    LibraryList="~/DataWindow/account/asset.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="OnDwRateButtonClicked"
                    ClientScriptable="True" ClientFormatting="True" TabIndex="1000" ClientEventItemChanged="OnDwRateItemChanged">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:RadioButton ID="Radio1" runat="server" AutoPostBack="True" OnCheckedChanged="Radio1_CheckChanged"
                    Text="รายการสินทรัพย์" Checked="True" Font-Size="Medium" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <%--<asp:RadioButton ID="Radio2" runat="server" AutoPostBack="True" OnCheckedChanged="Radio2_CheckChanged"
                    Text="รายการสินทรัพย์ไม่มีตัวตน" Font-Size="Medium" />--%>   
                <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" Height="400px" Width="640px">
                    <dw:WebDataWindowControl ID="Dwlist" runat="server" DataWindowObject="d_acc_list_asset_2"
                        LibraryList="~/DataWindow/account/asset.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                        Height="400px" Width="640px" TabIndex="2000" ClientEventButtonClicked="OnEditClickButton">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="Hdrow" runat="server" />
    <asp:HiddenField ID="Hdyear" runat="server" />
    <asp:HiddenField ID="Hdacc_id" runat="server" />
    <asp:HiddenField ID="Hdasset_doc" runat="server" />
    <asp:HiddenField ID="Hdreceive_tdate" runat="server" />
    <asp:HiddenField ID="HdAssGroup" runat="server" />
    <asp:HiddenField ID="HdAssGroup_new" runat="server" />
    <asp:HiddenField ID="IsOpenDuplicate" runat="server" />
    <asp:HiddenField ID="IsReplaceAll" runat="server" />
</asp:Content>
