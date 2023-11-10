<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_approve_reprint.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_approve_reprint_ctrl.ws_sl_approve_reprint_ctrl" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();

        function Validate() {
            
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_retrieve") {
                var alertstr = "";
                var loantype_code = dsMain.GetItem(0, "loantype_code");
                var member_no = dsMain.GetItem(0, "member_no");
                if (dsMain.GetItem(0, "loantype_code") == "" || dsMain.GetItem(0, "loantype_code") == null) { alertstr = alertstr + "- กรุณาเลือกประเภทเงินกู้!!\n"; }
                if (dsMain.GetItem(0, "member_no") == "" || dsMain.GetItem(0, "member_no") == null) {
                    alertstr = alertstr + " - กรุณากรอกทะเบียน!!\n";
                }
                if (alertstr == "") {
                    if (confirm("ยืนยันการดึงข้อมูล")) {
                        PostRetrieve();
                    }
                }
                else {
                    alert(alertstr);
                    return false;
                }
            }
        }
        function Setfocus() {
            dsMain.Focus(0, "member_no");
        }

        function OnClickAll() {
            var allrow = dsList.GetRowCount();
            for (var i = 0; i <= allrow; i++) {
                dsList.SetItem(i, "print_flag", 1);
            }
        }

        function OnUnClickAll() {
            var allrow = dsList.GetRowCount();
            for (var i = 0; i <= allrow; i++) {
                dsList.SetItem(i, "print_flag", 0);
            }
        }

        function OnClickPrintCont() {
            PrintCont();
        }

        function OnClickPrintColl() {
            PrintColl();
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <input type="button" value="เลือกทั้งหมด" style="width: 75px" onclick="OnClickAll()" />
    <input type="button" value="ไม่เลือกทั้งหมด" style="width: 90px" onclick="OnUnClickAll()" />
    <input type="button" value="พิมพ์สัญญา" style="width: 80px" onclick="OnClickPrintCont()" />
    <uc2:DsList ID="dsList" runat="server" />
    <asp:HiddenField ID="hdrow" runat="server" Value="false" />
</asp:Content>
