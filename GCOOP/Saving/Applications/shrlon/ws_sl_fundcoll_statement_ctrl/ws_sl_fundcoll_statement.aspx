<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_sl_fundcoll_statement.aspx.cs" 
Inherits="Saving.Applications.shrlon.ws_sl_fundcoll_statement_ctrl.ws_sl_fundcoll_statement" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                JsPostMember();
            }
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_printbook") {
                JsPostPrintBook();
            }
            else if (c == "b_printstate") {
                JsPostPrintStatement();
            }
            else if (c == "b_processint") {
                JsPostProcessInt();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc2:DsList ID="dsList" runat="server" />
    <%=outputProcess%>
</asp:Content>
