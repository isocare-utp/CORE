<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_fin_pay_moneyreturn.aspx.cs" Inherits="Saving.Applications.app_finance.ws_fin_pay_moneyreturn_ctrl.ws_fin_pay_moneyreturn" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsProcess.ascx" TagName="DsProcess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostInitList();
            }
        }

        function OnDsListItemChanged(s, r, c, v) {
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_retrieve") {
                PostInitList();
            } else if (c == "b_process") {

            }
        }

        function OnDsListClicked(s, r, c) {
        }

        function MenubarOpen() {
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function SheetLoadComplete() {
        }
    </script>
    <script type="text/javascript">
        $(function () {
            var tabIndex = Gcoop.ParseInt($("#<%=hdTabIndex.ClientID%>").val());
            $("#tabs").tabs({
                active: tabIndex,
                activate: function (event, ui) {
                    $("#<%=hdTabIndex.ClientID%>").val(ui.newTab.index() + "");
                }
            });
        });
    </script>
    <style type="text/css">
        .ui-tabs
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        #tabs
        {
            width: 760px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <%--<div id="tabs">
        <ul>
            <li><a href="#tabs-1">ค้นหาข้อมูล</a></li>
            <li><a href="#tabs-2">ทำรายการ</a></li>
        </ul>
        <div id="tabs-1">--%>
            <uc1:DsMain ID="dsMain" runat="server" />
        <%--</div>
        <div id="tabs-2">
            <uc3:DsProcess ID="dsProcess" runat="server" />
        </div>
    </div>--%>
    <uc2:DsList ID="dsList" runat="server" />
    <asp:HiddenField ID="Hdprocess" runat="server" />
    <asp:HiddenField ID="hdTabIndex" Value="0" runat="server" />
</asp:Content>
