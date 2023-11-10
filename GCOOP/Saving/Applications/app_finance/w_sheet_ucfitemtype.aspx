<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_ucfitemtype.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_ucfitemtype" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูลรายการ");
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Panel ID="Panel1" runat="server" Width="720px" ScrollBars="Auto" HorizontalAlign="Center">
        <asp:Button ID="Button1" runat="server" Text="อัพเดทรายการการเงิน" Width="150px" Height="35px" OnClick="itemtype_click" />
    </asp:Panel>
</asp:Content>
