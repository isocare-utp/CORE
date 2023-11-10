<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_kp_adjust_kptp.aspx.cs" Inherits="Saving.Applications.keeping.w_sheet_kp_adjust_kptp_ctrl.w_sheet_kp_adjust_kptp" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsListSlip.ascx" TagName="DsListSlip" TagPrefix="uc2" %>
<%@ Register Src="DsRecieveMain.ascx" TagName="DsRecieveMain" TagPrefix="uc3" %>
<%@ Register Src="DsRecieveList.ascx" TagName="DsRecieveList" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsListSlip = new DataSourceTool();
        var dsRecieveList = new DataSourceTool();
        var dsMain = new DataSourceTool();


        function Validate() {

            return confirm("ยืนยันการบันทึกข้อมูล?");
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                postMemberNo();
            }
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame('685', '590', 'w_dlg_kp_member_search.aspx', '');
        }

        function GetValueFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno);
            PostMember();
        }

        function OnDsListSlipClicked(s, r, c) {
            if (r >= 0) {
                dsListSlip.GetRowFocus();
                postRecv();
            }
        }

        function OnDsRecieveListItemChanged(s, r, c, v) {            
            if (r >= 0) {

                if (c == "principal_payment" || c == "interest_payment") {
                    var str = dsRecieveList.GetItem(r, "keepitemtype_code");
                    var res = str.substring(0, 1);
                    if (res == "L") {
                        
                        var bfprinbalance_amt = dsRecieveList.GetItem(r, "bfprinbalance_amt");
                        var principal_payment = dsRecieveList.GetItem(r, "principal_payment");
                        var interest_payment = dsRecieveList.GetItem(r, "interest_payment");
                        var item_payment = principal_payment + interest_payment;
                        var principal_balance = bfprinbalance_amt - principal_payment;
                        dsRecieveList.SetItem(r, "principal_balance", principal_balance);
                        dsRecieveList.SetItem(r, "item_payment", item_payment);
                    } else {
                        dsRecieveList.SetItem(r, "principal_payment", 0);
                        dsRecieveList.SetItem(r, "interest_payment", 0);
                    }

                } else if (c == "posting_flag") {
                    if (v == "Y" || v == "y") {
                        dsRecieveList.SetItem(r, "keepitem_status", 1);
                    } else if (v == "N" || v == "n") {
                        dsRecieveList.SetItem(r, "keepitem_status", -9);
                    }
                
                }
            }
            if (c == "principal_payment" || c == "interest_payment" || c == "item_payment") {                
                PostCalPayinAmount();
            }
        }

        $(function () {
            $('.El_hiddenF').hide();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <uc1:DsMain ID="dsMain" runat="server" />
                <br />
            </td>
            
        </tr>
        <tr>
            <td>
                <uc2:DsListSlip ID="dsListSlip" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <uc3:DsRecieveMain ID="dsRecieveMain" runat="server" />
                <br />
                <uc4:DsRecieveList ID="dsRecieveList" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
