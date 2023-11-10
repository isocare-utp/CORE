<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_ucf_typeserise.aspx.cs"
    Inherits="Saving.Applications.account.w_acc_ucf_typeserise" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postInsertRow%>
    <%=postDeleteRow%>
    <script type="text/javascript">
        function MenubarNew() {
            postInsertRow();
        }
        // ฟังก์ชันการเพิ่มแถวข้อมูล
        function Insert_acc_typeserise() {
            postInsertRow();
        }

        function Delete_acc_typeserise(sender, row, bName) {
            if (bName == "b_del") {
                var type_series = objDw_acc_typeserise.GetItem(row, "type_series");

                if (type_series == "" || type_series == null) {
                    Gcoop.GetEl("Hdrow").value = row + "";
                    postDeleteRow();
                } else {
                    var isConfirm = confirm("ต้องการลบข้อมูลหมวดค่าเสื่อม " + type_series + "ใช่หรือไม่ ?");
                    if (isConfirm) {
                        Gcoop.GetEl("Hdrow").value = row + "";
                        postDeleteRow();
                    }
                }
            }
            return 0;
        }

        //ฟังก์ชันการเช็คค่าข้อมูลก่อน save
        function Validate() {
            var isconfirm = confirm("ยืนยันการบันทึกข้อมูล ?");
            if (!isconfirm) {
                return false;
            }
            var RowDetail = objDw_acc_typeserise.RowCount();
            var alertstr = "";
            var type_series = objDw_acc_typeserise.GetItem(RowDetail, "type_series");
            var type_desc = objDw_acc_typeserise.GetItem(RowDetail, "type_desc");

            if (type_series == "" || type_series == null) {
                alertstr = alertstr + "กรุณาระบุรหัสหมวดค่าเสื่อม\n";
            }
            if (type_desc == "" || type_desc == null) {
                alertstr = alertstr + "_กรุณาระบุคำอธิบาย\n";
            }
            if (alertstr == "") {
                return true;
            }
            else {
                alert(alertstr);
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr>
                <td>
                    <span class="linkSpan" onclick="Insert_acc_typeserise()" style="font-family: Tahoma;
                        font-size: small; float: left; color: #0000CC;">เพิ่มแถว</span>
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
                    <dw:WebDataWindowControl ID="Dw_acc_typeserise" runat="server" DataWindowObject="d_acc_typeserise"
                        LibraryList="~/DataWindow/account/cm_constant_config.pbl" ClientEventButtonClicked="Delete_acc_typeserise"
                        ClientScriptable="True" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="Hdrow" runat="server" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
    </p>
</asp:Content>
