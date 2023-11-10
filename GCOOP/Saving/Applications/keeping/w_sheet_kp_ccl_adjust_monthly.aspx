<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_kp_ccl_adjust_monthly.aspx.cs"
    Inherits="Saving.Applications.keeping.w_sheet_kp_ccl_adjust_monthly" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postCheckStatus %>
    <%=postSumItemadj %>
    <%=jsPostMember %>
    <%=postRefresh %>
    <%=postSetPrinInt%>
    <%=postGetPrinInt %>
    <script type="text/javascript">
        function OnDwDetailItemChange(s, r, c, v) {
            if (c == "item_adjamt") {
                objdw_detail.SetItem(r, "item_adjamt", v);
                objdw_detail.AcceptText();
                Gcoop.GetEl("Hdrow").value = r + "";
                postSetPrinInt();
            } else if (c == "principal_adjamt" || c == "interest_adjamt") {
                objdw_detail.SetItem(r, c ,v);
                objdw_detail.AcceptText();
                SumItemAdj();
            }
        }

        function OnDwDetailClick(s, r, c) {
            if (c == "operate_flag") {
                Gcoop.GetEl("Hdrow").value = r + "";
                Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
                postSumItemadj();

            }

        }

        function checkMain(s, r, c) {
            if (c == "slipretall_flag") {
                Gcoop.CheckDw(s, r, c, "slipretall_flag", 1, 0);
                postCheckStatus();
            }
        }

        function Validate() {
            objdw_main.AcceptText();
            objdw_detail.AcceptText();

            return confirm("ยืนยันการบันทึกข้อมูล?");
        }

        function MenubarNew() {
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            newclear();
        }
        function MenubarOpen() {
            Gcoop.OpenIFrame2('580', '590', 'w_dlg_sl_member_search_tks.aspx', '');
        }

        function Click_search(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenDlg('580', '590', 'w_dlg_sl_member_search.aspx', '');
            }
            else if (c == "b_refsearch") {
                var member_no = objdw_main.GetItem(1, "member_no");
                if (member_no == "" || member_no == null) {
                    alert("กรุณากรอกเลขที่สมาชิก");
                }
                else {
                    Gcoop.OpenIFrame('600', '400', 'w_dlg_kp_mastreceive_search.aspx', "?member_no=" + member_no + "");
                }

            }

        }

        function GetValueFromDlg(memberno) {
            objdw_main.SetItem(1, "member_no", memberno);
            objdw_main.AcceptText();
            Gcoop.GetEl("Hfmember_no").value = memberno;
            //jsPostMember();
        }

        function GetMemDetFromDlg(member_no) {
            objdw_main.SetItem(1, "member_no", memberno);
            objdw_main.AcceptText();
        }

        function GetValueKpSlipno(recv_period, kpslip_no, receipt_date) {
            objdw_main.SetItem(1, "ref_recvperiod", recv_period);
            objdw_main.SetItem(1, "ref_slipno", kpslip_no);
            //  objdw_main.SetItem(1, "adjslip_date", receipt_date);
            objdw_main.AcceptText();
            jsPostMember();
        }

        function itemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objdw_main.GetItem(rowNumber, "member_no");
                var member_no = objdw_main.GetItem(1, "member_no");
                postRefresh();
                //Gcoop.OpenDlg('580', '590', 'w_dlg_kp_mastreceive_search.aspx', "?member_no=" + member_no + "");
            }

            return 0;

        }


        function OnDwdetailButtonClicked(sender, rowNumber, columnName) {

            return 0;
        }

        function SheetLoadComplete() {

            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
        }

        function SumItemAdj() {
            var nRow = objdw_detail.RowCount();
            var chk = 0, check_flag = 0, operate_flag = 0, principal_adjamt = 0, interest_adjamt = 0, item_adjamt = 0, bfmthpay_prnamt = 0;
            var bfmthpay_intamt = 0, bfmthpay_itemamt = 0, sum_cancel = 0.00, sum_pay = 0.00, item_pay = 0.00; ;
            for (var r = 1; r <= nRow; r++) {
                check_flag = 0;
                operate_flag = 0;
                principal_adjamt = 0.00;
                interest_adjamt = 0.00;
                item_adjamt = 0.00;
                bfmthpay_prnamt = 0.00;
                bfmthpay_intamt = 0.00;
                bfmthpay_itemamt = 0.00;
                operate_flag = objdw_detail.GetItem(r, "operate_flag");
                if (operate_flag == 1) {
                    //bfmthpay_prnamt = objdw_detail.GetItem(r, "bfmthpay_prnamt");
                    //bfmthpay_intamt = objdw_detail.GetItem(r, "bfmthpay_intamt");

                    bfmthpay_itemamt = objdw_detail.GetItem(r, "bfmthpay_itemamt");
                    principal_adjamt = objdw_detail.GetItem(r, "principal_adjamt");
                    interest_adjamt = objdw_detail.GetItem(r, "interest_adjamt");
                    if (!interest_adjamt) {
                        interest_adjamt = 0.00;
                    }
                    item_adjamt = parseFloat(principal_adjamt).round(2) + parseFloat(interest_adjamt).round(2);
                    sum_cancel += parseFloat(item_adjamt).round(2);
                    item_pay = parseFloat(bfmthpay_itemamt).round(2) - parseFloat(item_adjamt).round(2);
                    sum_pay += parseFloat(item_pay).round(2);
                    objdw_detail.SetItem(r, "item_adjamt", item_adjamt);
                } else {

                    //bfmthpay_prnamt = objdw_detail.GetItem(r, "bfmthpay_prnamt");
                    //bfmthpay_intamt = objdw_detail.GetItem(r, "bfmthpay_intamt");
                    bfmthpay_itemamt =  objdw_detail.GetItem(r, "bfmthpay_itemamt");
                    sum_pay += parseFloat(bfmthpay_itemamt).round(2);
                }
            }
            $("#<%=lbl_nocancel.ClientID %>").text(sum_pay.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $("#<%=lbl_cancel.ClientID %>").text(sum_cancel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));

        }

        Number.prototype.round = function (p) {
            p = p || 10;
            return parseFloat(this.toFixed(p));
        };

    </script>
    <style type="text/css">
        .style1
        {
            font-size: small;
            width: 135px;
        }
        .style2
        {
            width: 135px;
        }
        .style3
        {
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="Hidmembsection" runat="server" />
    <asp:HiddenField ID="Hidgroup" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:TextBox ID="TextDwmain" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="Textdwhistory" runat="server" Visible="False"></asp:TextBox>
    <asp:RadioButtonList ID="SearchType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" 
        Font-Size="Medium" AutoPostBack="true">
        <asp:ListItem Value="memberNo" Selected="True"> ค้นหาตามทะเบียนสมาชิก </asp:ListItem>
        <asp:ListItem Value="saralyId"> ค้นหาตามรหัสพนักงาน </asp:ListItem>
    </asp:RadioButtonList>
    <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_kp_adjust_monthly_main"
        LibraryList="~/DataWindow/keeping/kp_ccl_adjust_monthly.pbl" ClientScriptable="True"
        ClientEventItemChanged="itemChanged" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientEventButtonClicked="Click_search"
        ClientEventClicked="checkMain" TabIndex="1">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_kp_adjust_monthly_detail"
        LibraryList="~/DataWindow/keeping/kp_ccl_adjust_monthly.pbl" ClientScriptable="True"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" TabIndex="500" ClientEventButtonClicked="OnDwdetailButtonClicked"
        ClientEventClicked="OnDwDetailClick" ClientEventItemChanged="OnDwDetailItemChange">
    </dw:WebDataWindowControl>
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                รวมชำระ :
                <asp:Label ID="lbl_nocancel" runat="server" Style="font-weight: 700" Text="0.00"
                    Visible="True"></asp:Label>
            </td>
            <td class="style3">
                รวมยกเลิก :
                <asp:Label ID="lbl_cancel" runat="server" Style="font-weight: 700; color: #FF0000"
                    Text="0.00" Visible="True"></asp:Label>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;
            </td>
            <td>
                <asp:HiddenField ID="Hdrow" runat="server" />
                <asp:HiddenField ID="HdPrin" runat="server" />
                <asp:HiddenField ID="HdInt" runat="server" />
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
