<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_deptconstant.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_deptconstant" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>

    <script type="text/javascript">
    function Validate(){
        return confirm("ยืนยันการบันทึกข้อมูล");
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Label ID="Label1" runat="server" Text="ข้อกำหนดระบบเงินฝาก" Font-Bold="True"
        Font-Names="Tahoma" Font-Size="14px"></asp:Label>&nbsp&nbsp;
    <asp:Label ID="LbMessage" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px"
        ForeColor="Red"></asp:Label>
    <br />
    <br />
    <dw:WebDataWindowControl ID="DwMain" runat="server" ClientScriptable="True" DataWindowObject="d_dp_deptconstant"
        LibraryList="~/DataWindow/ap_deposit/dp_deptconstant.pbl" 
        AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True">
    </dw:WebDataWindowControl>
</asp:Content>
