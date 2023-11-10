<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_ad_grouppermiss.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_grouppermiss" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsinitPageSearch %>
    <%=jsRemove%>
    <%=jsSearch%>
    <%=jsaddappname%>
    <%=jsSelectAll%>
    <style type="text/css">
        .tb_user_name
        {
            border: 1;
        }
        .tb_user_name td
        {
            border: solid 1px black;
        }
    </style>
    <script type="text/javascript">
        function selectAppRow(sender, rowNumber, objectName) {
            Gcoop.GetEl("HdApp").value = objDwApplication.GetItem(rowNumber, "amsecuseapps_application");
            Gcoop.GetEl("HdAppDesc").value = objDwApplication.GetItem(rowNumber, "amappstatus_description");
            Gcoop.GetEl("HdRow").value = rowNumber;
            jsinitPageSearch();
        }
        function Validate() {
            return confirm("คุณต้องการบันทึกข้อมูล ใช่หรือไม่?");
        }
//        function Confirmation() {
//            group = objDwApplication.GetItem(1, "amappstatus_description");
//            app = Gcoop.GetEl("HdApp").value
//            asn = confirm("ยืนยันการลบแอพพลิเคชั่น " + app + " หรือไม่ ?");
//            if (asn) {
//                return true;
//            }
//            else {
//                return false;
//            }
//        }
        function OnDwButtomClicked(s, r, c) {
            switch (c) {
                case "b_add":
                    Gcoop.OpenIFrame("560", "500", "w_dlg_ad_addapps.aspx");
                    break;
                case "b_remove":
                    asn = confirm("ยืนยันการลบแอพพลิเคชั่น " + Gcoop.GetEl("HdAppDesc").value + " หรือไม่ ?");
                    if (asn) {
                        jsRemove();
                    }
                    else {
                        return false;
                    }

                    break;
            }

        }
        function AddApp(apps, desc) {
            Gcoop.GetEl("HdNew").value = apps;
            Gcoop.GetEl("HdDesc").value = desc;
            jsaddappname();
        }
        function itemChange(s, r, c, v) {
            if (c == "user_name") {
                s.SetItem(1, "user_name", v);
                s.AcceptText();
                jsSearch();
            }
        }
        function SelectAllClick(s, r, c) {
            if (c == "b_1") {
                jsSelectAll();
            } else if (c == "b_see") {
                var nRow = objDwPagePermiss.RowCount();
                var chk = 0;
                var check_flag = 0;
                for (var r = 1; r <= nRow; r++) {
                    check_flag = objDwPagePermiss.GetItem(r, "amsecpermiss_check_flag");
                    if (check_flag == 1) { chk = 1 };
                }
                if (chk == 1) {
                    for (var r = 1; r <= nRow; r++) {
                        objDwPagePermiss.SetItem(r, "amsecpermiss_check_flag", 0);
                    }
                } else {
                    for (var r = 1; r <= nRow; r++) {
                        objDwPagePermiss.SetItem(r, "amsecpermiss_check_flag", 1);
                    }
                }

            } else if (c == "b_save") {
                var nRow = objDwPagePermiss.RowCount();
                var chk = 0;
                var check_flag = 0;
                for (var r = 1; r <= nRow; r++) {
                    check_flag = objDwPagePermiss.GetItem(r, "amsecpermiss_save_status");
                    if (check_flag == 1) { chk = 1 };
                }
                if (chk == 1) {
                    for (var r = 1; r <= nRow; r++) {
                        objDwPagePermiss.SetItem(r, "amsecpermiss_save_status", 0);
                    }
                } else {
                    for (var r = 1; r <= nRow; r++) {
                        objDwPagePermiss.SetItem(r, "amsecpermiss_save_status", 1);
                    }
                }
            }
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame(600, 350, "w_dlg_ad_group_list.aspx", '')
        }
        function Receiveusername(user_name) {
            objDwGroup.SetItem(1, "user_name", user_name);
            jsSearch();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table class="tb_user_name" cellpadding="0" cellspacing="0">
        <tr>
            <dw:WebDataWindowControl ID="DwGroup" runat="server" DataWindowObject="d_um_group_permiss"
                LibraryList="~/DataWindow/admin/ad_group.pbl;" ClientScriptable="True" ClientEventItemChanged="itemChange"
                AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                ClientFormatting="True" TabIndex="1">
            </dw:WebDataWindowControl>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel4" runat="server" Height="350px" ScrollBars="Auto" Width="150px"
                    HorizontalAlign="Left">
                    <dw:WebDataWindowControl ID="DwApplication" runat="server" DataWindowObject="d_um_groupapps"
                        LibraryList="~/DataWindow/admin/ad_group.pbl;" ClientScriptable="True" ClientEventClicked="selectAppRow"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="20">
                    </dw:WebDataWindowControl>
                    <dw:WebDataWindowControl ID="DwButtom" runat="server" DataWindowObject="d_um_button_left"
                        LibraryList="~/DataWindow/admin/ad_group.pbl;" ClientScriptable="True" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientEventButtonClicked="OnDwButtomClicked">
                    </dw:WebDataWindowControl>
                    <%--                    <asp:Button ID="addButton" runat="server" OnClick="addButton_Click" Text="Add" Width="60px" />
                    <asp:Button ID="removeButton" runat="server" OnClientClick="return Confirmation();"
                        OnClick="removeButton_Click" Text="Remove" Width="60px" />--%>
                    <br />
                </asp:Panel>
            </td>
            <td>
                <asp:Panel ID="Panel3" runat="server" Height="350px" ScrollBars="Auto" Width="500px">
                    <dw:WebDataWindowControl ID="DwPagePermiss" runat="server" DataWindowObject="d_um_grouppages_permiss"
                        LibraryList="~/DataWindow/admin/ad_group.pbl;" ClientScriptable="True" ClientEventItemChanged="itemChange"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientEventButtonClicked="SelectAllClick" ClientFormatting="True" TabIndex="400">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdApp" runat="server" />
    <asp:HiddenField ID="HdAppDesc" runat="server" />
    <asp:HiddenField ID="HdRow" runat="server" Value="" />
    <asp:HiddenField ID="HdNew" runat="server" Value="" />
    <asp:HiddenField ID="HdDesc" runat="server" Value="" />
    <asp:HiddenField ID="HdSelectAll" runat="server" Value="" />
</asp:Content>
