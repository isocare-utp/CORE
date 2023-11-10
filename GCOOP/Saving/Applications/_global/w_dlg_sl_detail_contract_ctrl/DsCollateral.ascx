<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollateral.ascx.cs" Inherits="Saving.Applications._global.w_dlg_sl_detail_contract_ctrl.DsCollateral" %>
  <link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" >
    <table class="DataSourceRepeater" style="width: 720px;">
        <tr>
            <th width="15%">
                ประเภท
            </th>
            <th width="9%">
                เลขที่อ้างอิง
            </th>
            <th width="27%">
                รายการ
            </th>
            <th width="10%">
                คงเหลือ/รอเบิก
            </th>
            <th width="8%">
                % ค้ำ
            </th>
            <th width="10%">
                จำนวนเงินค้ำ
            </th>
           
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="374px" ScrollBars="Auto" HorizontalAlign="Left" >
    <table class="DataSourceRepeater"  style="width: 720px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td  width="15%">
                        <asp:TextBox ID="loancolltype_desc" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="ref_collno" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="27%">
                        <asp:TextBox ID="description" runat="server" ReadOnly="true" ></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="COMPUTE_1" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="coll_percent" runat="server" ReadOnly="true" Style="text-align: center;"
                            ToolTip="#.00%"></asp:TextBox>
                    </td>
                    <td  width="10%">
                        <asp:TextBox ID="COMPUTE_2" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
