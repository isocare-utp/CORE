<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="u_cri_coopid_memno_date_name_position_number.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_memno_date_name_position_number.u_cri_coopid_memno_date_name_position_number" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     function OnDsMainItemChanged(s, r, c, v) {
         if (c == "memno") {
             PostMemberNo();
         }
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<center>
        <asp:Label ID="ReportName" runat="server" Text="ชื่อรายงาน" Enabled="False" EnableTheming="False"
            Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Large"
            Font-Underline="False"></asp:Label></center>
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
