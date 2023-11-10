<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsPayment.ascx.cs" Inherits="Saving.Applications._global.w_dlg_sh_detail_share_ctrl.DsPayment" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" style="width: 700px;">
    <table class="DataSourceRepeater" style="width: 750px;">
        <tr>
            <th width="12%">
                เลขที่เอกสาร
            </th>
            <th width="12%">
                วันที่เปลี่ยน
            </th>
            <th width="7%">
                งวดหุ้น
            </th>
            <th width="12%">
                ทุนเรือนหุ้น
            </th>
            <th width="13%">
                หุ้น/เดือนก่อน
            </th>
            <th width="10%">
                สถานะก่อน
            </th>
            <th width="13%">
                หุ้น/เดือนหลัง
            </th>
            <th width="10%">
                สถานะหลัง
            </th>
            <th width="12%">
                ผู้ทำรายการ
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Auto" HorizontalAlign="Left" style="width: 800px;">
    <table class="DataSourceRepeater"  style="width: 750px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td  width="12%">
                        <asp:TextBox ID="payadjust_docno" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="approve_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="shrlast_period" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="sharestk_value" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="old_periodvalue" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td  width="10%">
                        <asp:TextBox ID="old_paystatus" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td  width="13%">
                        <asp:TextBox ID="new_periodvalue" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="10%"> 
                        <asp:TextBox ID="new_paystatus" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                     <td width="12%">
                        <asp:TextBox ID="approve_id" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
