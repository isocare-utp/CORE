<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true"
    CodeBehind="u_cri_coopid_year_rgroup_new.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_year_rgroup_new.u_cri_coopid_year_rgroup_new" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c) {

        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "membgroup_start") {
                PostMemGroup_S();
            }
            if (c == "membgroup_end") {
                PostMemGroup_E();
            }
            if (c == "membgroup_s_code") {
                PostMemGroup_S_Code();
            }
            if (c == "membgroup_e_code") {
                PostMemGroup_E_Code();
            }
        }

        function SheetLoadComplete() {

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
