﻿<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="ws_dlg_sl_add_grpmangrtpermiss.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.ws_dlg_sl_add_grpmangrtpermiss_ctrl.ws_dlg_sl_add_grpmangrtpermiss" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        //DsMain
        function OnDsMainClicked(s, r, c) {
            if (c == "b_clear") {

            }
            else if (c == "b_add") {
                PostSave();

                window.close();

            }
            else if (c == "b_cancel") {
                parent.RemoveIFrame();
            }
        }

        function OnDsMainItemChanged(s, r, c, v) {
           
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<br />
    <center>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <uc1:DsMain ID="dsMain" runat="server" />
    </center>
</asp:Content>
