<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" 
CodeBehind="u_cri_rdate_entryid.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_rdate_entryid.u_cri_rdate_entryid" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script type="text/javascript">
      var dsMain = new DataSourceTool();
      function OnDsMainItemChanged(s, r, c, v) {
          
      }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <center>
        <asp:Label ID="ReportName" runat="server" Text="ชื่อรายงาน" Enabled="False" EnableTheming="False"
            Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Large"
            Font-Underline="False"></asp:Label></center>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
