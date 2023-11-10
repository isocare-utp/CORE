<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_constant_ucfmethpay_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <tr>
            <th width="5%">
            </th>
            <th width="10%">
                รหัสการจ่าย
            </th>
            <th width="20%">
                ประเภทการจ่าย
            </th>
            <th width="10%">
                เครื่องหมาย
            </th>
            <th width="10%">
                ลำดับการแสดง
            </th>
            <th width="13%">
                รหัสรายการเคลื่อนไหว
            </th>
            <th width="20%">
                ประเภทเงินทำรายการ
            </th>
            <th width="7%">
                แสดงรายการ
            </th>
            <th width="5%">
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="methpaytype_code" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="methpaytype_desc" runat="server"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="sign_flag_text" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="methpaytype_sort" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="methpaystm_itemtype" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:DropDownList ID="join_moneytype_code" runat="server">
                            <asp:ListItem Value="CSH">CSH-เงินสด</asp:ListItem>
                            <asp:ListItem Value="CBT">CBT-โอนธนาคาร</asp:ListItem>
                            <asp:ListItem Value="CHQ">CHQ-เช็คธนาคาร</asp:ListItem>
                            <asp:ListItem Value="TBK">TBK-โอนภายนอก</asp:ListItem>
                            <asp:ListItem Value="TRN">TRN-โอนภายในระบบ</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="7%">
                        <asp:CheckBox ID="showlist_flag" runat="server" />
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_delete" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
