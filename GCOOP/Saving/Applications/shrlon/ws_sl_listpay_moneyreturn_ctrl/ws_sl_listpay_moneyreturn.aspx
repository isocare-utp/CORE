<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_listpay_moneyreturn.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_listpay_moneyreturn_ctrl.ws_sl_listpay_moneyreturn" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            } else if (c == "salary_id") {
                PostSalaryId();
            } else if (c == "moneytype_code") {
                PostMoneytype();
            }
        }
        function OnDsMainItemClicked(s, r, c) {
            if (c == "b_retrieve") {
                PostBtnRetieve();
            }

        }

        function OnDsListItemChanged(s, r, c, v) {
            if (c == "operate_flag") {
                SumAllReturn();
            }
        }

        function OnDsListClicked(s, r, c) {
           
        }

        function SumAllReturn() {
            var sum = parseFloat("0.00");
            var sumList = 0;
            for (var i = 0; i < dsList.GetRowCount(); i++) {
                if (dsList.GetItem(i, "operate_flag") == 1) {
                    sum += parseFloat(dsList.GetItem(i, "sum_return")).round(2);
                    sumList++;
                }
            }

            $("#<%= sum_all.ClientID %>").val(sum.toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $("#<%= LtList.ClientID %>").text("รวมจ่ายคืนทั้งหมด : " + sumList+" รายการ");
        }

        Number.prototype.round = function (p) {
            p = p || 10;
            return parseFloat(this.toFixed(p));
        };

        function SheetLoadComplete() {
            $("#chk_all").click(function () {
                if ($("#chk_all").prop('checked')) {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "operate_flag", 1);
                    }
                } else {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "operate_flag", 0);
                    }
                }
                SumAllReturn();
            });


        }
        function Validate() {
            var moneytype_code = dsMain.GetItem(0, "moneytype_code");
            var tofrom_accid = dsMain.GetItem(0, "tofrom_accid");
            if (!moneytype_code || !tofrom_accid) {
                alert("กรุณาเลือการทำรายการและคู่บัญชี");
            } else {
                return confirm("ยืนยันการบันทึก?");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
    <br />
    <div align="right"  >
        <asp:Label ID="LtList" runat="server" Text=""></asp:Label>
        <asp:TextBox ID="sum_all" runat="server" Style="background-color: Black; margin-left: 5px;
                        text-align: right; font-size: 24px;" Width="180" Height="45" ToolTip="#,##0.00"
                        ForeColor="GreenYellow"></asp:TextBox>
    </div>

    <br />
</asp:Content>
