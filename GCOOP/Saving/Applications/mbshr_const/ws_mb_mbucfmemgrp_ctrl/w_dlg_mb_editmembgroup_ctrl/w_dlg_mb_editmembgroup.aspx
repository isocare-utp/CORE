<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_mb_editmembgroup.aspx.cs" Inherits="Saving.Applications.mbshr_const.ws_mb_mbucfmemgrp_ctrl.w_dlg_mb_editmembgroup_ctrl.w_dlg_mb_editmembgroup" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function Validate() {
        }
        function OnDsMainClicked(s, r, c) {
            if (c == "b_save") {
                PostSave();
            }
        }

        function OnDsMainItemChanged(s, r, c) {
            if (c == "addr_province") {
                PostProvince();
            } else if (c == "addr_amphur") {
                PostAmphur();
            }
        }
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <div align="center">
        <uc1:DsMain ID="dsMain" runat="server" />
    </div>
    <br />
</asp:Content>
