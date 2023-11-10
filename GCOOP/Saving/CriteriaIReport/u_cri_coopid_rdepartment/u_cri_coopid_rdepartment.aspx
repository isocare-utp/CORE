<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true"
    CodeBehind="u_cri_coopid_rdepartment.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_rdepartment.u_cri_coopid_rdepartment" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "sdepartment_desc") {
                dsMain.SetItem(0, "sdepartment_code", v);
            }
            else if (c == "edepartment_desc") {
                dsMain.SetItem(0, "edepartment_code", v);
            }
            else if (c == "sdepartment_code") {
                dsMain.SetItem(0, "sdepartment_desc", v);
            }
            else if (c == "edepartment_code") {
                dsMain.SetItem(0, "edepartment_desc", v);
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

