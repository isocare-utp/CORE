<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsBoard.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.DsBoard" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server">
    <table class="DataSourceRepeater" style="width: 686px;">
        <tr>
            <th width="11%">
                วันที่
            </th>
            <th width="18%">
                คณะกรรมการที่ประชุม
            </th>
            <th width="16%">
                เรื่องเสนอที่ประชุม
            </th>
            <th>
                รายละเอียดการประชุม
            </th>
            <th width="8%">
                <span style="color: Blue; text-decoration: underline; cursor: pointer;" onclick="<%=OnClickAddRow%>">
                    เพิ่มแถว</span>
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="705px" Height="141px" ScrollBars="Vertical">
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <table class="DataSourceRepeater" style="width: 686px;">
                <tr>
                    <td align="center" width="11%">
                        <asp:TextBox ID="SUB_DATE" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="18%">
                        <asp:TextBox ID="TOPIC" runat="server"></asp:TextBox>
                    </td>
                    <td width="16%">
                        <asp:TextBox ID="PROPOSAL" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="MEET_DESC" runat="server"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
