<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsBdingdet.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_collateral_detail_ctrl.DsBdingdet" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="TbStyle" style="width: 700px">
    <tr>
        <th width="5%">
        </th>
        <th width="20%">
            ชั้นที่
        </th>
        <th width="20%">
            เนื้อที่ (ตร.ม.)
        </th>
        <th width="25%">
            ราคาประเมิน (ตร.ม. ละ)
        </th>
        <th width="25%">
            รวม
        </th>
        <th width="5%">
        </th>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Auto">
    <table class="TbStyle" style="width: 700px">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="floor_no" runat="server" Style="text-align: center;"  ToolTip="#,##0"  ></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="floor_square" runat="server" Style="text-align: center;"  ToolTip="#,##0.00" ></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="floor_pricesquare" runat="server" Style="text-align: right;"  ToolTip="#,##0.00"  ></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="floor_sumprice" runat="server" Style="text-align: right;"  ToolTip="#,##0.00"  ></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
