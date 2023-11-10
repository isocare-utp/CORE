<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_fin_paychqmanual.aspx.cs" Inherits="Saving.Applications.app_finance.ws_fin_paychqmanual_ctrl.ws_fin_paychqmanual" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var dsMain = new DataSourceTool;

    function Validate() {
        var alertstr = "";
        if (dsMain.GetItem(0, "cheque_bookno") == "" || dsMain.GetItem(0, "cheque_bookno") == null) {
            alertstr = alertstr + "- กรุณากรอกเล่มที่เช็คที่จะทำการจ่าย!!\n";
        }
        if (dsMain.GetItem(0, "account_no") == "" || dsMain.GetItem(0, "account_no") == null) {
            alertstr = alertstr + "- กรุณากรอกเลขที่เช็คที่จะทำการจ่าย!!\n";
        }
        if (dsMain.GetItem(0, "pay_whom") == "" || dsMain.GetItem(0, "pay_whom") == null) {
            alertstr = alertstr + "- กรุณากรอกสั่งจ่ายที่จะทำการจ่าย!!\n";
        }
        if (dsMain.GetItem(0, "bank_code") == "" || dsMain.GetItem(0, "bank_code") == null) {
            alertstr = alertstr + "- กรุณาเลือกธนาคารที่จะทำการจ่าย!!\n";
        }
        if (dsMain.GetItem(0, "bank_branch") == "" || dsMain.GetItem(0, "bank_branch") == null) {
            alertstr = alertstr + "- กรุณาเลือกสาขาธนาคารที่จะทำการจ่าย!!\n";
        }
        if (dsMain.GetItem(0, "cheque_amt") == "" || dsMain.GetItem(0, "cheque_amt") == null) {
            alertstr = alertstr + "- กรุณากรอกจำนวนเงินที่จะทำการจ่าย!!\n";
        }
        if (dsMain.GetItem(0, "frombank") == "" || dsMain.GetItem(0, "frombank") == null) {
            alertstr = alertstr + "- กรุณาเลือกธนาคารที่จะทำการตัดเงินออก!!\n";
        }
        if (dsMain.GetItem(0, "frombranch") == "" || dsMain.GetItem(0, "frombranch") == null) {
            alertstr = alertstr + "- กรุณาเลือกสาขาธนาคารที่จะทำการตัดเงินออก!!\n";
        }
        if (dsMain.GetItem(0, "fromaccount_no") == "" || dsMain.GetItem(0, "fromaccount_no") == null) {
            alertstr = alertstr + "- กรุณากรอกเลขที่บัญชีที่จะทำการตัดเงินออก!!\n";
        }
        if (dsMain.GetItem(0, "as_tofromaccid") == "" || dsMain.GetItem(0, "as_tofromaccid") == null) {
            alertstr = alertstr + "- กรุณากรอกรหัสบัญชี cr!!\n";
        }
        if (alertstr == "" ) {
            if (confirm("ยืนยันการบันทึกข้อมูล")) {
                SaveWebSheet();
            }
        }
        else {
            alert(alertstr);
            return false;
        }
    }

    function OnDsMainItemChanged(s, r, c, v) {
        if (c == "bank_code") {
            PostGetBank();
        }
      else if (c == "bank_branch") {
          PostGetbook();
      }
      else if (c == "cheque_bookno") {
          PostGetchequeno();
      }
  }
  function OnDsMainClicked(s, r, c) {
      if (c == "b_1") {
          var bank_code = dsMain.GetItem(0, "frombank");
          var bank_branch = dsMain.GetItem(0, "frombranch").trim();
          Gcoop.OpenIFrame2(650, 600, 'wd_fin_deptaccount_search.aspx', "?frombank=" + bank_code + "&frombranch=" + bank_branch);
//          Gcoop.OpenDlg2(650, 600, "w_dlg_deptaccount_search.aspx", "");
            
      }
  }

  function GetDeptNoFromDlg(deptno) {
      dsMain.SetItem(0, "fromaccount_no", deptno);
  }
  function change_ai_killer() {
      var ai_killer = document.querySelectorAll('input[type="radio"]:checked');
      var ai_killertxt = ai_killer.length > 0 ? ai_killer[0].value : null;
      dsMain.SetItem(0, "ai_killer", ai_killertxt);
  }

  function change_ai_prndate() {
      var ai_prndate = document.querySelectorAll('input[type="radio"]:checked');
      var ai_prndatetxt = ai_prndate.length > 0 ? ai_prndate[0].value : null;
      dsMain.SetItem(0, "ai_prndate", ai_prndatetxt);
  }

  function change_ai_payee() {
      var ai_payee = document.querySelectorAll('input[type="radio"]:checked');
      var ai_payeetxt = ai_payee.length > 0 ? ai_payee[0].value : null;
      dsMain.SetItem(0, "ai_payee", ai_payeetxt);
  }
    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
<asp:Panel ID="Panel1" runat="server" Height="600px" ScrollBars="Auto">
        <table width="100%" border="0">
            <%--<tr>
                <td>
                    เลือกเครื่องพิมพ์ :
                    <asp:DropDownList ID="DdPrintSetProfile" runat="server" Width="200px">
                        <asp:ListItem Text="printfin-23" Value="printfin-23" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="printfin-22" Value="printfin-22"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td align="center">
                    <uc1:DsMain ID="dsMain" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
