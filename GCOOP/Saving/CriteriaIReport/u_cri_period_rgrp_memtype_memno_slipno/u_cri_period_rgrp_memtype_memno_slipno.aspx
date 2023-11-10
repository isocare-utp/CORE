<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true"
    CodeBehind="u_cri_period_rgrp_memtype_memno_slipno.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_period_rgrp_memtype_memno_slipno.u_cri_period_rgrp_memtype_memno_slipno" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "smembgroup_desc") {
                dsMain.SetItem(0, "smembgroup_code", v);
            }
            else if (c == "emembgroup_desc") {
                dsMain.SetItem(0, "emembgroup_code", v);
            }
            else if (c == "smembgroup_code") {
                dsMain.SetItem(0, "smembgroup_desc", v);
            }
            else if (c == "emembgroup_code") {
                dsMain.SetItem(0, "emembgroup_desc", v);
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
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
