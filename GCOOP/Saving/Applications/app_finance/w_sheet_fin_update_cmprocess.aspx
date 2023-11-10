<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_fin_update_cmprocess.aspx.cs" 
Inherits="Saving.Applications.app_finance.w_sheet_fin_update_cmprocess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=jsPostUpCmprocessFin%>
<script type="text/javascript">
    function UpCmprocessFin() {
        //alert("call funtion to post up fin");
        jsPostUpCmprocessFin();
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <input type="button" value="อัพเดทสถานะประมวลการเงิน" onclick="UpCmprocessFin()" />
</asp:Content>
