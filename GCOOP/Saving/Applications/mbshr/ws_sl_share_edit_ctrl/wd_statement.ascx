<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wd_statement.ascx.cs"
    Inherits="Saving.Applications.mbshr.ws_sl_share_edit_ctrl.wd_statement" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 700px;">
        <tr>
            <th width="5%">
            </th>
            <th width="13%">
                วันที่ทำรายการ
            </th>
            <th width="13%">
                วันที่ใบเสร็จ
            </th>
            <th width="13%">
                เลขที่อ้างอิง
            </th>
            <th width="11%">
                รหัส
            </th>
            <th width="7%">
                งวด
            </th>
            <th width="16%">
                หุ้นทำรายการ(บาท)
            </th>
            <th width="18%">
                หุ้นสะสม(บาท)
            </th>
            <th width="5%">
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 700px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:TextBox ID="seq_no" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="operate_date" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="slip_date" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="ref_docno" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="11%">
                        <asp:DropDownList ID="shritemtype_code" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="period" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="16%">
                        <asp:TextBox ID="shramt_value" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="18%">
                        <asp:TextBox ID="shrstk_value" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_delete" runat="server" Text="-" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
