<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_fin_bankaccount.aspx.cs" Inherits="Saving.Applications.app_finance.ws_fin_bankaccount_ctrl.ws_fin_bankaccount" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function check_number() {
            e_k = event.keyCode
            if (e_k != 13 && e_k != 46 && (e_k < 48) || (e_k > 57)) {
                event.returnValue = false;
                alert("กรุณากรอกเฉพาะตัวเลขเท่านั้น!");
            }
        }
        function check_balance() {
            e_k = event.keyCode
            if (e_k != 13 && e_k != 45 && (e_k < 48) || (e_k > 57)) {
                event.returnValue = false;
                alert("กรุณากรอกเฉพาะตัวเลขเท่านั้น!");
            }
        }
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();

        function Validate() 
        {
            var alertstr = "";
            if (dsMain.GetItem(0, "account_no") == "" || dsMain.GetItem(0, "account_no") == null) {
                alertstr += "- กรุณากรอกเลขที่บัญชี\n";
            }
            if (dsMain.GetItem(0, "account_name") == "" || dsMain.GetItem(0, "account_name") == null) {
                alertstr += "- กรุณากรอกชื่อที่บัญชี\n";
            }
            if (dsMain.GetItem(0, "account_type") == "" || dsMain.GetItem(0, "account_type") == null) {
                alertstr += "- กรุณาเลือกประเภท\n";
            }
            if (dsMain.GetItem(0, "bank_code") == "" || dsMain.GetItem(0, "bank_code") == null) {
                alertstr += "- กรุณาเลือกธนาคาร\n";
            }
            if (dsMain.GetItem(0, "bankbranch_code") == "" || dsMain.GetItem(0, "bankbranch_code") == null) {
                alertstr += "- กรุณาเลือกสาขา\n";
            }
            if (dsMain.GetItem(0, "account_id") == "" || dsMain.GetItem(0, "account_id") == null) {
                alertstr += "- กรุณาเลือกคู่บัญชี\n";
            }
            if (alertstr == "") {
                return confirm("ยืนยันการบันทึกข้อมูล");
            } else {
                alert(alertstr);
                return false;
            }
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "bank_code") {
                dsMain.SetItem(0, "bankbranch_code", '');
                JsPostGetBank();
            } else if (c == "account_no") {
                //JsPostRrieveDataFrmAccno();
            }
        }
        function GetDataFromDlg(account_no, bank_code, bankbranch_code) {
            dsMain.SetItem(0, "account_no", account_no);
            dsMain.SetItem(0, "bank_code", bank_code);
            dsMain.SetItem(0, "bankbranch_code", bankbranch_code);
            Gcoop.GetEl("HdGetDlg").value="1";
            JsPostRrieveData();
        }
        function MenubarOpen() {
            Gcoop.OpenIFrame2(950, 550, 'wd_fin_bankaccount.aspx', "?chkclose_status=0");
        }
        function SheetLoadComplete() {
            if (dsMain.GetItem(0, "bank_code") == null) {
                dsMain.GetElement(0, "bankbranch_code").disabled = true;
            } else {
                dsMain.GetElement(0, "bankbranch_code").disabled = false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <br />
    <uc2:DsList ID="dsList" runat="server" />

    <asp:HiddenField ID="HdGetDlg" runat="server" Value="0"/>
</asp:Content>