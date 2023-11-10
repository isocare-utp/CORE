<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_sl_add_instype.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_insurance_type_ctrl.w_dlg_sl_add_instype_ctrl.w_dlg_sl_add_instype" %>

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
