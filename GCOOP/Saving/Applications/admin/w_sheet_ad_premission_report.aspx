<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_ad_premission_report.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_premission_report" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsinitPageSearch %>
    <%=jsSearch%>
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
            Gcoop.GetEl("AppID").value = objDwApplication.GetItem(rowNumber, "amsecuseapps_application");
            Gcoop.GetEl("HdRow").value = rowNumber;
            jsinitPageSearch();
        }
        function Validate() {
            return confirm("คุณต้องการบันทึกข้อมูล ใช่หรือไม่?");
        }
        function Confirmation() {
            group = objDwApplication.GetItem(1, "amsecuseapps_application");
            app = Gcoop.GetEl("AppID").value
            asn = confirm("ยืนยันการลบแอพพลิเคชั่น " + app + " หรือไม่ ?");
            if (asn) {
                return true;
            }
            else {
                return false;
            }
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
            }
        }
        function MenubarOpen() {
            Gcoop.OpenIFrame(700, 350, "w_dlg_ad_users_list.aspx", '')
        }
        function ReceiveUserName(user_name) {
            objDwUser.SetItem(1, "user_name", user_name);
            jsSearch();
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table class="tb_user_name" cellpadding="0" cellspacing="0">
        <tr>
            <dw:WebDataWindowControl ID="DwUser" runat="server" DataWindowObject="d_um_user_permiss"
                LibraryList="~/DataWindow/admin/ad_user.pbl;" ClientScriptable="True" ClientEventItemChanged="itemChange"
                AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                ClientFormatting="True" TabIndex="1">
            </dw:WebDataWindowControl>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel4" runat="server" Height="350px" ScrollBars="Auto" Width="180px">
                    <dw:WebDataWindowControl ID="DwApplication" runat="server" DataWindowObject="d_um_userapps_pre"
                        LibraryList="~/DataWindow/admin/ad_user.pbl;" ClientScriptable="True" ClientEventClicked="selectAppRow"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="20">
                    </dw:WebDataWindowControl>
                    <br />
                </asp:Panel>
            </td>
            <td>
                <asp:Panel ID="Panel3" runat="server" Height="350px" ScrollBars="Auto" Width="470px">
                    <dw:WebDataWindowControl ID="DwPermiss" runat="server" DataWindowObject="d_um_userpermiss_report"
                        LibraryList="~/DataWindow/admin/ad_user.pbl;" ClientScriptable="True" ClientEventItemChanged="itemChange"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="400" ClientEventButtonClicked="SelectAllClick">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="AppID" runat="server" />
    <asp:HiddenField ID="HdRow" runat="server" Value="" />
    <asp:HiddenField ID="HdNew" runat="server" Value="" />
    <asp:HiddenField ID="HdSave" runat="server" Value="" />
    <asp:HiddenField ID="HdDesc" runat="server" Value="" />
    <asp:HiddenField ID="HdSelectAll" runat="server" Value="" />
</asp:Content>
