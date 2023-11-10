<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_paychqfromslip_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" ScrollBars="Auto" Height="600px">
    <table class="DataSourceRepeater" style="width: 100%;">
        <tr>
            <th width="2%">
            </th>
            <th width="4%">
                ลำดับ
            </th>
            <th width="12%">
                วันที่
            </th>
            <th width="25%">
                สั่งจ่าย
            </th>
            <th width="25%">
                รายละเอียดการสั่งจ่าย
            </th>
            <th width="15%">
                หมายเหตุ
            </th>
            <th width="20%">
                จำนวนเงิน
            </th>        
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="2%">
                        <asp:CheckBox ID="choose_flag" runat="server" Style="text-align: center" />
                    </td>
                    <td width="4%">
                        <asp:TextBox ID="running_number" runat="server" ReadOnly="true" Style="text-align: center;" ></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="entry_date" runat="server" ReadOnly="true" Style="text-align: center;" ></asp:TextBox>
                    </td>
                    <td width="24%">
                        <asp:TextBox ID="NONMEMBER_DETAIL" runat="server" ></asp:TextBox>
                    </td>
                    <td width="24%">
                        <asp:TextBox ID="PAYMENT_DESC" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="REMARK" runat="server" ></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="item_amtnet" runat="server" ReadOnly="true" ToolTip="#,##0.00" style="text-align:right" ></asp:TextBox>
                    </td>                  
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
