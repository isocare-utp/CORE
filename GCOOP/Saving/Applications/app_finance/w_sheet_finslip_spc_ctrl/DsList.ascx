<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.w_sheet_finslip_spc_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 720px;">
        <tr>
            <th width="20%" colspan="3" >
                 รหัสรายการ
            </th>
            <th width="45%">
                คำอธิบายรายการ (ไม่เกิน 255 ตัวอักษร)
            </th>
            <th width="20%">
                จำนวนเงิน
            </th>                    
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="400px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 720px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="3%">
                        <asp:TextBox ID="running_number" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:DropDownList ID="slipitemtype_code" runat="server"></asp:DropDownList>
                    </td>
                    <td width="3%">
                        <asp:Button ID="b_sliptypecode" runat="server" Text="..." />                   
                    </td>
                    <td width="45%">
                        <asp:TextBox ID="slipitem_desc" runat="server" ></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="itempay_amt" runat="server"  ToolTip="#,##0.00" style="text-align:right" onfocus="this.select()"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
