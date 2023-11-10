<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsAdd.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loan_receive_ctrl.DsAdd" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 750px;">
    <tr>
        <th>
        </th>
        <th style="font-size: 11px;">
            รหัส
        </th>
        <th style="font-size: 11px;">
            รายละเอียด
        </th>
        <th style="font-size: 11px;">
            ยอดเงินชำระ
        </th>
        <th>
        </th>
    </tr>
   
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td width="5%" align="center">
                    <asp:CheckBox ID="operate_flag" runat="server" style="font-size: 11px; "/>
                </td>
                <td width="10%">
                    <asp:DropDownList ID="slipitemtype_code" runat="server" style="font-size: 11px;" BackColor="#EEEEEE">
                    </asp:DropDownList>
                </td>
                <td width="45%">
                    <asp:TextBox ID="slipitem_desc" runat="server" style="font-size: 11px; text-align: left; background-color: #EEEEEE;" ></asp:TextBox>
                </td>
                <td width="35%">
                    <asp:TextBox ID="item_payamt" runat="server" style="font-size: 11px; text-align: right; background-color: #99FFCC;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="4%" align="center">
                    <asp:Button ID="b_del" runat="server" Text="-" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
