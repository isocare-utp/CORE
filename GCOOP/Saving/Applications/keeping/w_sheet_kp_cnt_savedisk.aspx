<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_kp_cnt_savedisk.aspx.cs" Inherits="Saving.Applications.keeping.w_sheet_kp_cnt_savedisk" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: small;
            width: 184px;
        }
        .style2
        {
            width: 184px;
        }
        .style3
        {
            height: 28px;
        }
        .style4
        {
            color: #000066;
        }
    </style>
    <%=postInit%>
    <%=PostCheckGroup%>
    <%=PostCheckType%>
    <script type="text/javascript">

        function OnDwDiskClick(s, r, c) {
            if (c == "disk_code" || c == "disk_desc") {
                Gcoop.GetEl("Hdrow").value = r + "";
                postInit();
            }
        }
        function MenubarNew() {
            postNewClear();
        }

        function OnClicCheckGroup() {
            PostCheckGroup();
        }

        function OnClicCheckType() {
            PostCheckType();
        }

        //ฟังก์ชันการเช็คค่าข้อมูลก่อน save
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <span class="style4"><strong>กลุ่มแผ่น Disk</strong></span>
        <asp:Panel ID="Panel1" runat="server" Width="270">
            <dw:WebDataWindowControl ID="Dw_disktype" runat="server" AutoRestoreContext="False"
                AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                ClientScriptable="True" DataWindowObject="d_kp_cnt_savedisk_disktype" LibraryList="~/DataWindow/keeping/kp_cnt_savedisk_type.pbl"
                Style="top: 0px; left: -3px" Width="180px" ClientEventClicked="OnDwDiskClick">
            </dw:WebDataWindowControl>
        </asp:Panel>
        <br />
        <table>
            <tr>
                <td class="style2" valign="top" colspan="2">
                    <asp:Label ID="Label1" runat="server" Style="font-size: small; font-weight: 700;
                        color: #0000CC;" Text="รหัส : "></asp:Label>
                    <asp:Label ID="lbl_diskcode" runat="server" Style="font-size: small; color: #0000FF;
                        font-weight: 700;"></asp:Label>
                    &nbsp;
                    <asp:Label ID="lbl_diskdesc" runat="server" Style="font-size: small; color: #0000FF;
                        font-weight: 700;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:CheckBox ID="CheckGroupAll" runat="server" Style="font-size: small" Text="เลือกทั้งหมด"
                        onclick="OnClicCheckGroup()" />
                </td>
                <td valign="top">
                    <asp:CheckBox ID="CheckTypeAll" runat="server" Style="font-size: small" Text="เลือกทั้งหมด"
                        onclick="OnClicCheckType()" />
                </td>
            </tr>
            <tr>
                <td valign="top" class="style3" width="355px">
                    <asp:Panel ID="Panel2" runat="server" Height="445px" ScrollBars="Horizontal">
                        <dw:WebDataWindowControl ID="Dw_membgroup" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientScriptable="True" DataWindowObject="d_kp_cnt_savedisk_membgroup" LibraryList="~/DataWindow/keeping/kp_cnt_savedisk_type.pbl"
                            Style="top: 0px; left: 0px;">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td valign="top">
                    <asp:Panel ID="Panel3" runat="server">
                        <dw:WebDataWindowControl ID="Dw_membtype" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientScriptable="True" DataWindowObject="d_kp_cnt_savedisk_membtype" LibraryList="~/DataWindow/keeping/kp_cnt_savedisk_type.pbl"
                            Style="top: 0px; left: 30px;">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="Hdrow" runat="server" />
                    <asp:HiddenField ID="HdmembgroupRow" runat="server" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
