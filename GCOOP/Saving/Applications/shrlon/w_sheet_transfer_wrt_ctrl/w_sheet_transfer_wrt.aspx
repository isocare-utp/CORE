<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_transfer_wrt.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_transfer_wrt_ctrl.w_sheet_transfer_wrt" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = DataSourceTool();
        function SheetLoadComplete() { 
        }
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDsMainClicked(s, r, c) { 
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "acc_id") {
                PostBlank();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">

    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
