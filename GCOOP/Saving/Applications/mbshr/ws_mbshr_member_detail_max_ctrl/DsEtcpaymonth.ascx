<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsEtcpaymonth.ascx.cs"
    Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl.DsEtcpaymonth" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>หักประจำเดือนปัจจุบัน</strong></u></font></span>
<table class="TbStyle" id="content-index">
    <tr>
        <th width="5%">
        </th>
        <th width="21%">
            รายละเอียด
        </th>
        <th width="7%">
            งวด
        </th>
        <th width="13%">
            ชำระต้น
        </th>
        <th width="13%">
            ชำระ ด/บ
        </th>
        <th width="17%">
            รวมชำระ
        </th>
        <th width="17%">
            คงเหลือ
        </th>
        <th width="7%">
            สถานะ
        </th>
        <th style="display:none;">
            เลขที่ใบเสร็จ
        </th>
        <th style="display:none;">
            เลขสมาชิก
        </th>
        <th style="display:none;">
            เลขสมาชิกอ้างอิง
        </th>
    </tr>
     <tr>
        <td colspan="8">
            <asp:TextBox ID="cp_receipt_no" runat="server"></asp:TextBox>
        </td>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
          
            <tr>
                <td>
                    <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_keepitemtype_code" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="period" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="principal_payment" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_netintpay" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_netitempay" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="item_balance" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_keepitem_status" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                </td>
                <td style="display:none; ">
                    <asp:TextBox ID="RECEIPT_NO" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                </td>
                <td style="display:none; ">
                    <asp:TextBox ID="MEMBER_NO" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                </td>
                <td style="display:none; ">
                    <asp:TextBox ID="REF_MEMBNO" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <tr>
        <td style="border-style: none">
        </td>
        <td style="border-style: none; text-align: right" colspan="2">
            <strong>รวมชำระ:</strong>
        </td>
        <td style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000;">
            <asp:TextBox ID="cp_sum_principal_payment" runat="server" Style="text-align: right;
                font-weight: bold;"></asp:TextBox>
        </td>
        <td style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000;">
            <asp:TextBox ID="cp_sum_interest_payment" runat="server" Style="text-align: right;
                font-weight: bold;"></asp:TextBox>
        </td>
        <td style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000;">
            <asp:TextBox ID="cp_sum_item_payment" runat="server" Style="text-align: right; font-weight: bold;"></asp:TextBox>
        </td>
        <td style="border-style: none">
        </td>
        <td style="border-style: none">
        </td>
    </tr>
</table>
<script type="text/javascript" language="javascript">
    function makeTable(container, id) {
        var rowCount = $('table#' + id + ' tr:last').index() + 1;
        //alert('Row: = '+rowCount);     
        var sumAmt = 0.00;
        var textPrefix = "ใบเสร็จเลขที่  ";
        var textMidfix = " ยอดเงิน ";
        var textSuffix = " บาท ชำระแทน ";
        var tmp_receipt_no = "";
        var tmp_receipt_no_pre = "";
        var tmp_mbno = "";
        var tmp_refmbno = "";
        var i = 0;
        var show_flag = false;
        var txt = "";
        $('table#' + id).find('tr').each(function () {
            i++;
            var RECEIPT_NO = $(this).find("input[name*='RECEIPT_NO']").val();
            var REF_MEMBNO = $(this).find("input[name*='REF_MEMBNO']").val();
            var memb_no = $(this).find("input[name*='MEMBER_NO']").val();
            var item_payment = $(this).find("input[name*='cp_netitempay']").val() + "";
            item_payment = item_payment.replace(/\,/g, '');

            try {
                if (RECEIPT_NO != null && RECEIPT_NO != undefined) {

                    if (tmp_receipt_no != RECEIPT_NO) {

                        tmp_receipt_no = RECEIPT_NO;

                        if (tmp_receipt_no_pre == "") {
                            tmp_receipt_no_pre = RECEIPT_NO;
                        }

                        //alert(tmp_receipt_no_pre+","+tmp_receipt_no);
                        if ((tmp_receipt_no_pre != "" && tmp_receipt_no_pre != tmp_receipt_no)/*ตรวจว่าเป็นรายการสุดท้ายของใบเสร็จหรือไม่*/) {
                            if (tmp_mbno == tmp_refmbno) {
                                txt = textPrefix + tmp_receipt_no_pre + textMidfix + addPeriod(sumAmt);
                            }
                            else {
                                txt = textPrefix + tmp_receipt_no_pre + textMidfix + addPeriod(sumAmt) + textSuffix + REF_MEMBNO;
                            }
                            //alert(txt);
                            txt = '<td colspan="8" style="background-color:blue;"><input type="text" value="' + txt + '" style="background-color:blue;color:white;"/></td>';
                            $(this).before(txt);
                        }
                        sumAmt = parseFloat(item_payment);
                    } else {
                        sumAmt += parseFloat(item_payment);
                    }
                    if (tmp_receipt_no != "") {
                        sumAmt += "";
                        if (sumAmt.indexOf('.') < 0) {
                            sumAmt = sumAmt + ".00";
                        }
                    }

                    sumAmt = parseFloat(sumAmt);
                    tmp_receipt_no_pre = RECEIPT_NO;
                    tmp_mbno = memb_no;
                    tmp_refmbno = REF_MEMBNO;

                    if (((i + 1) == rowCount)/*ตรวจว่าเป็นรายการสุดท้ายของใบเสร็จหรือไม่*/) {
                        if (tmp_mbno == tmp_refmbno) {
                            txt = textPrefix + tmp_receipt_no_pre + textMidfix + addPeriod(sumAmt);
                        }
                        else {
                            txt = textPrefix + tmp_receipt_no_pre + textMidfix + addPeriod(sumAmt) + textSuffix + REF_MEMBNO;
                        }
                        //alert(txt);
                        txt = '<td colspan="8" style="background-color:blue;"><input type="text" value="' + txt + '" style="background-color:blue;color:white;"/></td>';
                        $(this).after(txt);
                    }
                }
            } catch (epxp) { }
        });
    }
    $(document).ready(function () {
        var cityTable = makeTable($(document.body), "content-index");
    });


    function addPeriod(nStr) {
        nStr += '';
        x = nStr.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        return x1 + x2;
    }
</script>


