<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_dp_post_wizard.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_dlg_dp_post_wizard" Title="Untitled Page" %>

<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
    //alert("hihi");
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <dw:WebDataWindowControl ID="DwMain" runat="server" 
        DataWindowObject="amsecwins" LibraryList="~/DataWindow/ap_deposit/newpbl.pbl">
    </dw:WebDataWindowControl>
    <br />
    <br />
    &nbsp;
</asp:Content>
