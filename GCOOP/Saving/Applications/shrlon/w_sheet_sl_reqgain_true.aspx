<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_reqgain_true.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_reqgain_true" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <dw:WebDataWindowControl ID="dw_gain" runat="server" 
        DataWindowObject="d_mb_gain_master" 
        LibraryList="~/DataWindow/Shrlon/sl_reqgain_true.pbl">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="dw_gaindetail" runat="server" 
        DataWindowObject="d_mb_gain_details" 
        LibraryList="~/DataWindow/Shrlon/sl_reqgain_true.pbl">
    </dw:WebDataWindowControl>
</asp:Content>
