<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_mg_template_autzd.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_template_autzd_ctrl.ws_sl_mg_template_autzd" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsMain = new DataSourceTool();

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function MenubarOpen() {
            Gcoop.OpenDlg2("650", "500", "w_dlg_mg_autzd_search.aspx", "");
        }        

        function GetTemplateNoFromDlg(template_no) {
            dsMain.SetItem(0, "template_no", template_no);
            PostTemplateNo();
        }

        function SheetLoadComplete() {
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc2:DsMain ID="dsMain" runat="server" />
</asp:Content>
