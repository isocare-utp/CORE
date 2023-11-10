<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_fund_cancel_payfundcoll.aspx.cs" Inherits="Saving.Applications.fund.ws_fund_cancel_payfundcoll_ctrl.ws_fund_cancel_payfundcoll" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();


        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "all_check") {
                var all_check = dsMain.GetItem(0, "all_check");
                for (var ii = 0; ii < dsList.GetRowCount(); ii++) {
                    dsList.SetItem(ii, "choose_flag", v);
                }
            }
        }
        function OnDsListClicked(s, r, c, v) {

        }
        function OnDsListItemChanged(s, r, c, v) {

        }
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
   <center>
        <uc1:DsMain ID="dsMain" runat="server" />
    </center>
    <uc2:DsList ID="dsList" runat="server" />   
</asp:Content>