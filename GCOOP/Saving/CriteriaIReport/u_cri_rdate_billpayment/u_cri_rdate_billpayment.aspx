<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true"
    CodeBehind="u_cri_rdate_billpayment.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_rdate_billpayment.u_cri_rdate_billpayment" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        
    </style>
    <script type="text/javascript">
        //SheetLoadComplete
        $(function () {
            if (GetUrlValue("rid") == "FPB00001") {
                $('.txtReportName').text("[FPB00001] - รายงาน Billpayment (ทั้งหมด)")  
            }
            else if (GetUrlValue("rid") == "FPB00002") {
                $('.txtReportName').text("[FPB00002] - รายงาน Billpayment (รอตรวจสอบ)")
            }
            else if (GetUrlValue("rid") == "FPB00003") {
                $('.txtReportName').text("[FPB00003] - รายงาน Billpayment (Complete)")
            }
            dsMain.SetItem(0, "reportchk", GetUrlValue("rid"));
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <br />
    <span type="text" id="ReportName" class="txtReportName" style="font-weight: bold;
        font-size: medium;"></span>
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
