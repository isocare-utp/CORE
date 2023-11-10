<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_close_month.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_close_month_ctrl.w_sheet_close_month" %>
<%@ Register Src="dsMain.ascx" TagName="dsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        var dsMain = new DataSourceTool();

        function OnDsMainClicked(sender, row, bName) {
            if (bName == "button1") {
                PostProcess();
            }
        }

        function OnDsMainItemChanged(s, r, c, v) {
        }

        function SheetLoadComplete() {
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:Dsmain ID="dsMain" runat="server" />
</asp:Content>
