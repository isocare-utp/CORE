<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_sl_proc_moneyreturn.aspx.cs" 
Inherits="Saving.Applications.shrlon.ws_sl_proc_moneyreturn_ctrl.ws_sl_proc_moneyreturn" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var dsMain = new DataSourceTool();

    function OnDsMainClicked(s, r, c) {
        if (c == "b_proc") {
            var isconfirm = confirm("ต้องการประมวลผลตั้งข้อมูลดอกเบี้ยคืน ใช่หรือไม่ ?");
            if (!isconfirm) {
                return false;
            }
            PostProcMoneyreturn();
        }
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <%=outputProcess%>
</asp:Content>
