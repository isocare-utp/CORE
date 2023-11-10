<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" CodeBehind="w_dlg_sl_detail.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_interestpay_estimate_ctrl.w_dlg_sl_detail_ctrl.w_dlg_sl_detail" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     var dsMain = new DataSourceTool;

     function Validate() {
     }
     function OnDsMainItemChanged(s, r, c, v) {
       
     }
     function OnDsMainClicked(s, r, c) {

        

     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<br />
<br />
<div align="center">

    <uc1:DsMain ID="dsMain" runat="server" />
    </div>
</asp:Content>
