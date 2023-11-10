<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_proc_trnpayin.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_proc_trnpayin_ctrl.ws_sl_proc_trnpayin" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();

        function OnDsMainItemChanged(s, r, c, v) {

        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_check") {
                PostCheck();
            }
        }

        function PostProc() {
            PostProc();
        }

        function PostPrint() {
            PostPrint();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
    <br />
    <input type="button" value="ผ่านรายการ" style="width: 80px" onclick="PostProc()" />
    <input type="button" value="ออกใบเสร็จ" style="width: 80px" onclick="PostPrint()" />
    <asp:HiddenField ID="Hdstartslipno" runat="server" />
    <asp:HiddenField ID="Hdendslipno" runat="server" />
    <%=outputProcess%>
</asp:Content>
