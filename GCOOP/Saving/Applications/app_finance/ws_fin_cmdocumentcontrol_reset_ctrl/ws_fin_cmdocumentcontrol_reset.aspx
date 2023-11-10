<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_fin_cmdocumentcontrol_reset.aspx.cs" Inherits="Saving.Applications.app_finance.ws_fin_cmdocumentcontrol_reset_ctrl.ws_fin_cmdocumentcontrol_reset" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        function OnDsMainClicked(s, r, c) {
            if (c == "b_save") {
                var r = confirm("ต้องการ reset ประมวลผลใช่หรือไม่");
                if (r == true) {
                    PostSave();
                } else {
                    return;
                }
            }
        }      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessege" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />    
</asp:Content>


