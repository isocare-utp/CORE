<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_ad_addusergroup.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_addusergroup" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=jsSearch%>
    <%=jsRemove%>
    <%=jsAddUserTOGroup%>
    <%=jsaddusername%>
    <script type="text/javascript">
        function Validate() {
            return confirm("คุณต้องการบันทึกข้อมูล ใช่หรือไม่?");
        }
        function OnDwButtomClicked(s, r, c, v) {
            switch (c) {
                case "b_add":
                    jsAddUserTOGroup();
                    break;
                case "b_remove":
                    group = objDwGroup.GetItem(1, "user_name");
                    if (group == null || group == "") {
                        asn = confirm("กรุณากรอกรหัสกลุ่ม/เลือกรหัสผู้ใช้ที่ต้องการลบ");
                    } else {
                        asn = confirm("ยืนยันการลบรหัสผู้ใช้ที่เลือก ออกจากกลุ่ม " + group + " หรือไม่ ?");
                    }
                    if (asn) {
                        jsRemove();
                    }
                    else {
                        return false;
                    }

                    break;
            }

        }

        function itemChange(s, r, c, v) {
            if (c == "user_name") {
                s.SetItem(1, "user_name", v);
                s.AcceptText();
                jsSearch();
            }
        }

        function AddUser(username, full_name) {
            Gcoop.GetEl("HdNew").value = username;
            Gcoop.GetEl("HdName").value = full_name;
            jsaddusername();
        }

        function OnDwClicked(s, r, c, v) {

            switch (c) {
                case "user_name":

                    Gcoop.GetEl("HdRow").value = r;
                    Gcoop.GetEl("HdValue").value = objDwGroupUsers.GetItem(r, "user_name");

                    break;
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
    <dw:WebDataWindowControl ID="DwGroup" runat="server" DataWindowObject="d_um_group_eau"
        LibraryList="~/DataWindow/admin/ad_group.pbl;" ClientScriptable="True" ClientEventItemChanged="itemChange"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" TabIndex="1">
    </dw:WebDataWindowControl>
    <div align="left">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Height="20px" Width="100px"></asp:TextBox>
    <asp:Button ID="Button1"
        runat="server" Text="ค้นหา" onclick="Button1_Click" Width="53px" />
    <br />
    </div>
    <div align="center">
    <table class="tb_user_name" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Panel ID="Panel4" runat="server" Height="350px" ScrollBars="Auto" Width="370px">
                    <dw:WebDataWindowControl ID="DwUsers" runat="server" DataWindowObject="d_um_adduser_togroup"
                        LibraryList="~/DataWindow/admin/ad_user.pbl;" ClientScriptable="True" ClientEventItemChanged="itemChange"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" ClientEventClicked="OnDwClicked" TabIndex="50">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
            <td>
            <asp:Panel ID="Panel1" runat="server" Height="350px" ScrollBars="Auto" Width="370px">
                <dw:WebDataWindowControl ID="DwGroupUsers" runat="server" DataWindowObject="d_um_editgroupusers"
                    LibraryList="~/DataWindow/admin/ad_group.pbl;" ClientScriptable="True" ClientEventItemChanged="itemChange"
                    AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    ClientFormatting="True" ClientEventClicked="OnDwClicked" TabIndex="1000">
                </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </div>
    <br />
    <div align="center">
    <dw:WebDataWindowControl ID="DwButtom" runat="server" DataWindowObject="d_um_button"
        LibraryList="~/DataWindow/admin/ad_group.pbl;" ClientScriptable="True" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        ClientEventButtonClicked="OnDwButtomClicked">
    </dw:WebDataWindowControl>
    </div>
    <asp:HiddenField ID="HdRow" runat="server" Value="" />
    <asp:HiddenField ID="HdValue" runat="server" Value="" />
    <asp:HiddenField ID="HdNew" runat="server" Value="" />
    <asp:HiddenField ID="HdName" runat="server" Value="" />
</asp:Content>
