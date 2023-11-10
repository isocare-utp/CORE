<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsRightcollmast.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsRightcollmast" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
        <table class="DataSourceRepeater">
            <tr>
                <th width="4%">
                </th>
                <th width="21%">
                    กลุ่ม<br />
                    หลักประกัน
                </th>
                <th width="21%">
                    ประเภท<br />
                    หลักประกัน
                </th>
                <th width="9%">
                    รูปแบบ<br />
                    สัดส่วน
                </th>
                <th width="9%">
                    สัดส่วน<br />
                    การกู้ได้
                </th>
                <th width="15%">
                    จากยอด
                </th>
                <th width="17%">
                    เงินกู้สูงสุด
                </th>
                <th width="4%">
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="110px" Width="750px" ScrollBars="Auto">
        <table class="DataSourceRepeater">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="4%">
                            <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                        </td>
                        <td width="21%">
                            <asp:DropDownList ID="loancolltype_code" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td width="21%">
                            <asp:DropDownList ID="collmasttype_code" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td width="9%">
                            <asp:DropDownList ID="right_format" runat="server">
                                <asp:ListItem Value="0" Text=""></asp:ListItem>
                                <asp:ListItem Value="1">%</asp:ListItem>
                                <asp:ListItem Value="2">อัตราส่วน</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="9%">
                            <asp:TextBox ID="right_perc" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            <asp:TextBox ID="right_ratio" runat="server" Style="text-align: right;"></asp:TextBox>
                        </td>
                        <td width="15%">
                            <asp:DropDownList ID="right_type" runat="server">
                                <asp:ListItem Value="0" Text=""></asp:ListItem>
                                <asp:ListItem Value="1">ค้ำคงเหลือ</asp:ListItem>
                                <asp:ListItem Value="2">เต็ม</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="17%">
                            <asp:TextBox ID="right_maxamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                        </td>
                        <td width="4%">
                            <asp:Button ID="b_del" runat="server" Text="-" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
</div>
