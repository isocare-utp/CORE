<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_divsrv_constant.aspx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_constant_ctrl.ws_divsrv_constant" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "divtype_code") {
                var ls_divtype_code = dsMain.GetItem(0, "divtype_code");
                if (ls_divtype_code == "MTH") {
                    $('#text_daycaltype').hide();
                    $('#text_daygrp').hide();
                    $('#text_mth').show();
                    dsMain.GetElement(0, "div_daycaltype_code").style.display = "none";
                    dsMain.GetElement(0, "div_daygrp_flag").style.display = "none";
                    dsMain.GetElement(0, "div_dayfix_flag").style.display = "none";
                } else {
                    $('#text_mth').hide();
                    $('#text_daycaltype').show();
                    $('#text_daygrp').show();
                    dsMain.GetElement(0, "div_daycaltype_code").style.display = "block";
                    dsMain.GetElement(0, "div_daygrp_flag").style.display = "block";
                    dsMain.GetElement(0, "div_dayfix_flag").style.display = "block";
                }
            } else if (c == "div_dayfix_flag") {
                var li_div_dayfix_flag = dsMain.GetItem(0, "div_dayfix_flag");
                if (li_div_dayfix_flag == 0) {
                    dsMain.GetElement(0, "div_day_amt").style.display = "none";
                } else {
                    dsMain.GetElement(0, "div_day_amt").style.display = "block";
                }
            }
        }

        function SheetLoadComplete(text_daycaltype) {
            var ls_divtype_code = dsMain.GetItem(0, "divtype_code");
            if (ls_divtype_code == "MTH") {
                $('#text_daycaltype').hide();
                $('#text_daygrp').hide();
                dsMain.GetElement(0, "div_daycaltype_code").style.display = "none";
                dsMain.GetElement(0, "div_daygrp_flag").style.display = "none";
                dsMain.GetElement(0, "div_dayfix_flag").style.display = "none";
            } else {
                $('#text_daycaltype').show();
                $('#text_daygrp').show();
                $('#text_mth').hide();
                dsMain.GetElement(0, "div_daycaltype_code").style.display = "block";
                dsMain.GetElement(0, "div_daygrp_flag").style.display = "block";
                dsMain.GetElement(0, "div_dayfix_flag").style.display = "block";
            }

        }
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
