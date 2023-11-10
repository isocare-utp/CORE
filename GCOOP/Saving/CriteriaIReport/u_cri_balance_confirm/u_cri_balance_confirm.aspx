<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true"
    CodeBehind="u_cri_balance_confirm.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_balance_confirm.u_cri_balance_confirm" %>

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
            if (c == "operate_flag_1") {
                if (v == "1") {
                    dsMain.SetItem(0, "operate_flag_2", 0);
                    dsMain.GetElement(0, "membgroup_start").disabled = false;
                    dsMain.SetItem(0, "membgroup_start", "");
                    dsMain.GetElement(0, "membgroup_end").disabled = false;
                    dsMain.SetItem(0, "membgroup_end", "");
                    dsMain.GetElement(0, "memno_start").readOnly = true;
                    dsMain.GetElement(0, "memno_start").style.background = "#CCCCCC";
                    dsMain.SetItem(0, "memno_start", "");
                    dsMain.GetElement(0, "memno_end").readOnly = true;
                    dsMain.GetElement(0, "memno_end").style.background = "#CCCCCC";
                    dsMain.SetItem(0, "memno_end", "");
                } else {
                    dsMain.SetItem(0, "operate_flag_2", 1);
                    dsMain.GetElement(0, "membgroup_start").disabled = true;
                    dsMain.SetItem(0, "membgroup_start", "");
                    dsMain.GetElement(0, "membgroup_end").disabled = true;
                    dsMain.SetItem(0, "membgroup_end", "");
                    dsMain.GetElement(0, "memno_start").readOnly = false;
                    dsMain.GetElement(0, "memno_start").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "memno_end").readOnly = false;
                    dsMain.GetElement(0, "memno_end").style.background = "#FFFFFF";
                }
            } else if (c == "operate_flag_2") {
                if (v == "1") {
                    dsMain.SetItem(0, "operate_flag_1", 0);
                    dsMain.GetElement(0, "membgroup_start").disabled = true;
                    dsMain.SetItem(0, "membgroup_start", "");
                    dsMain.GetElement(0, "membgroup_end").disabled = true;
                    dsMain.SetItem(0, "membgroup_end", "");
                    dsMain.GetElement(0, "memno_start").readOnly = false;
                    dsMain.GetElement(0, "memno_start").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "memno_end").readOnly = false;
                    dsMain.GetElement(0, "memno_end").style.background = "#FFFFFF";
                } else {
                    dsMain.SetItem(0, "operate_flag_1", 1);
                    dsMain.GetElement(0, "membgroup_start").disabled = false;
                    dsMain.SetItem(0, "membgroup_start", "");
                    dsMain.GetElement(0, "membgroup_end").disabled = false;
                    dsMain.SetItem(0, "membgroup_end", "");
                    dsMain.GetElement(0, "memno_start").readOnly = true;
                    dsMain.GetElement(0, "memno_start").style.background = "#CCCCCC";
                    dsMain.SetItem(0, "memno_start", "");
                    dsMain.GetElement(0, "memno_end").readOnly = true;
                    dsMain.GetElement(0, "memno_end").style.background = "#CCCCCC";
                    dsMain.SetItem(0, "memno_end", "");
                }
            }
        }

        function SheetLoadComplete() {
            if (dsMain.GetItem(0, "operate_flag_2") == "1") {
                dsMain.SetItem(0, "operate_flag_1", 0);
                dsMain.GetElement(0, "membgroup_start").disabled = true;
                dsMain.SetItem(0, "membgroup_start", "");
                dsMain.GetElement(0, "membgroup_end").disabled = true;
                dsMain.SetItem(0, "membgroup_end", "");
                dsMain.GetElement(0, "memno_start").readOnly = false;
                dsMain.GetElement(0, "memno_start").style.background = "#FFFFFF";
                dsMain.GetElement(0, "memno_end").readOnly = false;
                dsMain.GetElement(0, "memno_end").style.background = "#FFFFFF";
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
