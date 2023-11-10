<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsSMain.ascx.cs" Inherits="Saving.Applications.assist.ws_as_assistpaygroup_ctrl.DsSMain" %>

<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView2" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table border="0">
            <br />
        </table>
        <table class="DataSourceFormView">
            <tr>
                <td style="width: 13%">
                </td>
                <td style="width: 17%">
                </td>
                <td style="width: 13%">
                </td>
                <td style="width: 17%">
                </td>
                <td style="width: 13%">
                </td>
                <td style="width: 27%">
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 11px;">ทะเบียน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center; background-color: #CCCCCC;" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 11px;">ชื่อ-สกุล:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="mbname" runat="server" ReadOnly="true" Style="background-color: #CCCCCC;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 11px;">สังกัด :</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="mbgroup" runat="server" Style="width: 407px; background-color: #CCCCCC;" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 11px;">ประเภทสมาชิก :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mbtype" runat="server" Style="text-align: center; background-color: #CCCCCC" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    <div>
                        <span style="font-size: 11px;">สถานะสมาชิก :</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="resign_desc" runat="server" Style="width: 407px; background-color: #CCCCCC;" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 11px;">สถานะการตาย :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="dead_desc" runat="server" Style="text-align: center; background-color: #CCCCCC" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="DataSourceFormView" border="0">
            <tr>
                <td colspan="6">
                    <strong style="font-size: 12px;">รายละเอียดสวัสดิการ</strong>
                </td>
            </tr>
            <tr>
                <td style="width: 15%">
                    <div>
                        <span style="font-size: 11px;">เลขที่สวัสดิการ:</span>
                    </div>
                </td>
                <td style="width: 15%">
                    <asp:TextBox ID="asscontract_no" runat="server" Style="text-align: center; background-color: Lime;" ReadOnly="True"></asp:TextBox>
                </td>
                <td style="width: 15%">
                    <div>
                        <span style="font-size: 11px;">งวดรับเงิน:</span>
                    </div>
                </td>
                <td style="width: 15%">
                    <asp:TextBox ID="pay_period" runat="server" Style="text-align: center; background-color: Lime;" ReadOnly="True"></asp:TextBox>
                </td>
                <td style="width: 15%">
                    <div>
                        <span style="font-size: 11px;">วันที่จ่าย:</span>
                    </div>
                </td>
                <td style="width: 25%">
                    <asp:TextBox ID="slip_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 11px;">ยอดอนุมัติ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="bfapv_amt" runat="server" Style="text-align: center; background-color: Lime;"
                        ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 11px;">คงเหลือเบิกได้:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="bfwtd_amt" runat="server" Style="text-align: center; background-color: Lime;"
                        ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px; font-weight: bold;">ยอดจ่าย :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="payout_amt" runat="server" Style="background-color: Black; text-align: right;
                        font-weight: bold;" ToolTip="#,##0.00" ForeColor="#CCFF33" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 11px;">ประเภทสวัสดิการ :</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="assisttype_desc" runat="server" Style="margin-left: 1px;" BackColor="#CCCCCC" ReadOnly="True">
                    </asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px; font-weight: bold;">ยอดหักชำระ :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="payoutclr_amt" runat="server" Style="background-color: Black; text-align: right;
                        font-weight: bold;" ToolTip="#,##0.00" ForeColor="Red" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 11px;">ประเภทการจ่าย :</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="assistpay_desc" runat="server" Style="margin-left: 1px;" BackColor="#CCCCCC" ReadOnly="True">
                    </asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px; font-weight: bold;">ยอดจ่ายสุทธิ :</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="payoutnet_amt" runat="server" Style="background-color: Black; text-align: right;
                        font-weight: bold;" ToolTip="#,##0.00" ForeColor="#CCFF33" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
