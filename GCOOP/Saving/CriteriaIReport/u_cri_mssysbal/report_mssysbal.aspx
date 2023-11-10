<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="report_mssysbal.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_mssysbal.report_mssysbal" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
function OnDsMainItemChanged(s,r,c,v){

}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<br />
    <span type="text" id="ReportName" class="txtReportName" style="font-weight: bold;
        font-size: medium;"></span>
         <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Medium">[--------] - รายงาน--------  </asp:Label>
        <br />
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
