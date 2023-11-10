<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsInsurance.ascx.cs"
    Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl.DsInsurance" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="TbStyle" style="width: 710px">
    <tr>
        <th width="5%">
        </th>
        <th width="21%">
            เลขที่กรมธรรม์
        </th>
        <th width="15%">
            วันเริ่มคุ้มครอง
        </th>
        <th width="15%">
            วันหมดอายุ
        </th>
        <th width="15%">
            อ้างอิงสัญญา
        </th>
        <th width="18%">
            จำนวนเงิน
        </th>
        <th width="11%">
            การชำระ
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="TbStyle" style="width: 710px">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="21%">
                        <asp:TextBox ID="insurance_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="startinsure_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="expireinsure_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="18%">
                        <asp:TextBox ID="insurance_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="center" width="11%">
                        <asp:TextBox ID="cp_insurepay_status" runat="server" Style="text-align: center;"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
