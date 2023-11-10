<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_ad_adduser.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_adduser" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript%>
<%=jsSearch%>
<%=jsDescription%>
    <script type="text/javascript">
        function Validate() {
        return confirm("คุณต้องการบันทึกข้อมูลรหัสผู้ใช้ ใช่หรือไม่?");
    }
    function itemChange(s, r, c, v) {
        if (c == "user_name") {
            s.SetItem(1, "user_name", v);
            s.AcceptText();
            jsSearch();
        } else if (c == "full_name") {
            s.SetItem(1, "full_name", v);
            s.AcceptText();
            jsDescription();
        }
    }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
                <dw:WebDataWindowControl ID="DwUserName" runat="server" DataWindowObject="d_um_adduser"
                    LibraryList="~/DataWindow/admin/ad_user.pbl;"
                    ClientScriptable="True" 
                    ClientEventItemChanged="itemChange" AutoRestoreContext="False" 
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True">
                </dw:WebDataWindowControl>
                <asp:HiddenField ID="HdCkDes" runat="server" Value="" />
</asp:Content>
