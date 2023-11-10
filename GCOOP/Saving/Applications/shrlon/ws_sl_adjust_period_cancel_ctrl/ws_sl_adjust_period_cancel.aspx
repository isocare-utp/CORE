<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_adjust_period_cancel.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_adjust_period_cancel_ctrl.ws_sl_adjust_period_cancel" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //        var dsCriteria = new DataSourceTool;
        //        var dsList = new DataSourceTool;
        var dsMain = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }
            else if (c == "loancontract_no") {
                PostLoanContractNo();
            } else if (c == "period_payamt") {
                var oldperiod_payamt = dsMain.GetItem(r, "oldperiod_payamt");
                var div = v - oldperiod_payamt;

                dsMain.SetItem(r, "cp_div", div);
            }
        }

        //        function OnDsCriteriaClicked(s, r, c) {
        //            if (c == "b_retrieve") {
        //                PostRetrieve();
        //            }
        //        }

        //        function OnDsListClicked(s, r, c) {
        //            if (c == "b_del") {
        //                dsList.SetRowFocus(r);
        //                PostDeleteRow();
        //            }
        //        }

        //        function OnDsListItemChanged(s, r, c, v) {
        //            if (c == "period_payamt") {
        //                dsList.SetRowFocus(r);
        //                var oldperiod_payamt = dsList.GetItem(r, "oldperiod_payamt");
        //                var div = v - oldperiod_payamt;

        //                dsList.SetItem(r, "cp_div", div);
        //            }
        //        }

        function sum_cont() {
            var sum_cont = 0;
            sum_cont = dsList.GetRowCount();
            $("#ctl00_ContentPlace_sum_cont").val(sum_cont);
        }

        function SheetLoadComplete() {
            //sum_cont();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <%--
    <uc1:DsCriteria ID="dsCriteria" runat="server" />
    <br />
    <uc2:DsList ID="dsList" runat="server" />
    <br />
    <table class="DataSourceFormView">
        <tr>
            <td width="15%">
                <div>
                    <span style="text-align: center">รวมทั้งหมด:</span>
                </div>
            </td>
            <td width="15%">
                <div>
                    <asp:TextBox ID="sum_cont" runat="server" Style="text-align: center"></asp:TextBox>
                </div>
            </td>
            <td width="15%">
                <div>
                    <span style="text-align: center">สัญญา</span>
                </div>
            </td>
            <td>
            </td>
        </tr>
    </table>--%>
</asp:Content>
