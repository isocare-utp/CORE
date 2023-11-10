<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_tabpage_depttype_saving.ascx.cs"
    Inherits="Saving.Applications.ap_deposit.dpdepttypecond.u_tabpage_depttype_saving" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_dp_depttype_saving"
    LibraryList="~/DataWindow/ap_deposit/dpdepttypecond.pbl">
</dw:WebDataWindowControl>
<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

