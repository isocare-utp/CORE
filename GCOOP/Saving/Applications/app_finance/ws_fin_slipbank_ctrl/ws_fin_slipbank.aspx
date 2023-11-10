<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_fin_slipbank.aspx.cs" Inherits="Saving.Applications.app_finance.ws_fin_slipbank_ctrl.ws_fin_slipbank" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var dsMain = new DataSourceTool;

    function OnDsMainItemChanged(s, r, c, v) {
        if (c == "bank_code") {
            JsPostGetBank();
        } else if (c == "account_no") {
            JsPostRrieveDataFrmAccno();
        }
        else if (c == "item_code") {
            if (v == "CCA") {
                var amt = dsMain.GetItem(0, "balance");
                dsMain.SetItem(0, "item_amt", amt);
                dsMain.GetElement(0, "item_amt").disabled = true; 
            }
            else {
                dsMain.SetItem(0, "item_amt", 0);
                dsMain.GetElement(0, "item_amt").disabled = false; 
            }
        }
    }
    function GetDataFromDlg(account_no, bank_code, bankbranch_code)
    {
        dsMain.SetItem(0, "account_no", account_no); 
        dsMain.SetItem(0, "bank_code", bank_code); 
        dsMain.SetItem(0, "bank_branch", bankbranch_code);
        JsPostRrieveData();
    }
    function MenubarOpen() {
        Gcoop.OpenIFrame2(900, 550, 'wd_fin_bankaccount.aspx', "?chkclose_status=1");
    }
    function Validate() {
        var alertstr = "";
        if (dsMain.GetItem(0, "account_no") == null) {
            alertstr = alertstr + "- กรุณาเลือกเลขที่บัญชี \n";
        }
        if (dsMain.GetItem(0, "bank_code") == null) {
            alertstr = alertstr + "- กรุณากรอกเลขที่บัญชีให้ถูกต้อง \n";
        }
        if (dsMain.GetItem(0, "item_desc") == null) {
            alertstr = alertstr + "- กรุณากรอกรายละเอียดรายการ \n";
        }
        if (dsMain.GetItem(0, "item_code") != "OCA") {
            if (dsMain.GetItem(0, "item_amt") == 0) {
                alertstr = alertstr + "- กรุณากรอกจำนวนเงิน\n";
            }
        }
        if (alertstr == "") {
            if (confirm("ยืนยันการบันทึกข้อมูล")) {
                SaveWebSheet();
            }
        }
        else { alert(alertstr); return false; }
    }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
