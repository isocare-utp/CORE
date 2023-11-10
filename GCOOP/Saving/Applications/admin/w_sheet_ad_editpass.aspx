<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_ad_editpass.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_editpass" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsSearch%>
    <script type="text/javascript">
        function Validate() {
            return confirm("คุณต้องการบันทึกข้อมูล ใช่หรือไม่?");
        }
        function MainChanged(s, r, c, v) {
            if (c == "user_name") {
                s.SetItem(r, c, v);
                s.AcceptText();
                jsSearch();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientValidation="False"
            DataWindowObject="d_um_repassword" LibraryList="~/DataWindow/admin/ad_user.pbl" ClientEventItemChanged="UserCheck">
        </dw:WebDataWindowControl>
</asp:Content>
