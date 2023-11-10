<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true"
    CodeBehind="u_cri_coopid_rempno.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_rempno.u_cri_coopid_rempno" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "semp_name") {
                dsMain.SetItem(0, "semp_no", v);
            }
            else if (c == "eemp_name") {
                dsMain.SetItem(0, "eemp_no", v);
            }
            else if (c == "semp_no") {
                dsMain.SetItem(0, "semp_name", v);
            }
            else if (c == "eemp_no") {
                dsMain.SetItem(0, "eemp_name", v);
            }
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
