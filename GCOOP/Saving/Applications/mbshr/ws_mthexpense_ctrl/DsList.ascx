<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mthexpense_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="5%">
            ลำดับ
        </th>
        <th width="50%">
            รายละเอียด
        </th>
        <th width="15%">
            ฝั่งรายการ
        </th>
        <th width="10%">
            จำนวนเงิน
        </th>
        <th width="5%">
            ลบ!
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                    <td width="5%">
                        <asp:TextBox ID="SEQ_NO" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="50%">
                        <asp:TextBox ID="MTHEXPENSE_DESC" runat="server"></asp:TextBox>
                    </td>
                    <td width="10%">
                    <asp:DropDownList ID="sign_flag" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="-1">จ่าย</asp:ListItem>
                            <asp:ListItem Value="1">รับ</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="MTHEXPENSE_AMT" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
