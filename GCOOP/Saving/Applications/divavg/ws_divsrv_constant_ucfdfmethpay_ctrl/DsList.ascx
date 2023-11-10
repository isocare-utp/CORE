<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_constant_ucfdfmethpay_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <tr>
            <th width="6%">
                ลำดับ
            </th>
            <th width="18%">
                ตั้งแต่ยอดเงิน (บาท)
            </th>
            <th width="18%">
                ถึงยอดเงิน (บาท)
            </th>
            <th width="18%">
                วิธีชำระ
            </th>
            <th width="21%">
                ธนาคาร
            </th>
            <th width="13%">
                แทนที่วิธีจ่าย<br />
                เงินปันผลเดิม
            </th>
            <th width="6%">
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="6%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="18%">
                        <asp:TextBox ID="start_value" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="18%">
                        <asp:TextBox ID="end_value" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="18%">
                        <asp:DropDownList ID="methpaytype_code" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="21%">
                        <asp:DropDownList ID="expense_bank" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="13%">
                        <asp:CheckBox ID="payreplace_flag" runat="server" />
                    </td>
                    <td width="6%">
                        <asp:Button ID="b_delete" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
