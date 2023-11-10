<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true"
    CodeBehind="u_cri_date_bank_branch.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_date_bank_branch.u_cri_date_bank_branch" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
    <script type="text/javascript">
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "bank_code") {
                JsPostBank();
            }
        }
   
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <br />
    <span type="text" id="ReportName" class="txtReportName" style="font-weight: bold;
        font-size: medium;"></span>
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
