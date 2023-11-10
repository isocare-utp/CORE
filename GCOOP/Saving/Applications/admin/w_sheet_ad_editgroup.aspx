<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" 
CodeBehind="w_sheet_ad_editgroup.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_editgroup" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
        <%=jsSearch%>
        <%=jsDescription%>
    <script type="text/javascript">
        function Validate() {
            return confirm("คุณต้องการบันทึกข้อมูล ใช่หรือไม่?");
        }
        function itemChange(s, r, c, v) {
            if (c == "user_name") {
                s.SetItem(1, "user_name", v);
                s.AcceptText();
                jsSearch();
            } else if (c == "description") {
                s.SetItem(1, "description", v);
                s.AcceptText();
                jsDescription();
            }


        }

        function MenubarOpen() {
            Gcoop.OpenIFrame(600, 350, "w_dlg_ad_group_list.aspx", '')
        }
        function Receiveusername(user_name) {
            objDwCri.SetItem(1, "user_name", user_name);
            jsSearch();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table >
        <tr>
            <td>
                 <dw:WebDataWindowControl ID="DwCri" runat="server" DataWindowObject="d_um_group_ea"
                    LibraryList="~/DataWindow/admin/ad_group.pbl;"
                    ClientScriptable="True" 
                    ClientEventItemChanged="itemChange" AutoRestoreContext="False" 
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" TabIndex="1">
                </dw:WebDataWindowControl> 
            </td>
        </tr>
        <tr>
            <td>
            <div align="center">
                <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_um_groupuser_detail"
                LibraryList="~/DataWindow/admin/ad_group.pbl;"
                ClientScriptable="True" 
                ClientEventItemChanged="itemChange" AutoRestoreContext="False" 
                AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" TabIndex="50">
                </dw:WebDataWindowControl>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdCkDes" runat="server" Value="" />
</asp:Content>




