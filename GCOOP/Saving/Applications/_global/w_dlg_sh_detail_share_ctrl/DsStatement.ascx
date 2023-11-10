<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsStatement.ascx.cs"
    Inherits="Saving.Applications._global.w_dlg_sh_detail_share_ctrl.DsStatement" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" >
    <table class="DataSourceRepeater" style="width: 800px;">
        <tr>
            <th width="5%">
                ลำดับ
            </th>
            <th width="12%">
                วันทำรายการ
            </th>
            <th width="12%">
                วันที่ใบเสร็จ
            </th>
            <th width="12%">
                เลขที่ใบเสร็จ
            </th>
            <th width="10%">
                รายการ
            </th>
            <th width="10%">
                งวด
            </th>
            <th width="13%">
                ยอดรับ
            </th>
            <th width="14%">
                ยอดจ่าย/ยกเลิก
            </th>
            <th width="14%">
                หุ้นคงเหลือ
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Auto" HorizontalAlign="Left" style="width: 820px;" >
    <table class="DataSourceRepeater"  style="width: 800px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td  width="5%">
                        <asp:TextBox ID="running_number" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="operate_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="slip_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="ref_docno" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="shritemtype_code" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td  width="10%">
                        <asp:TextBox ID="period" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td  width="13%">
                        <asp:TextBox ID="COMPUTE_1" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="14%"> 
                        <asp:TextBox ID="COMPUTE_2" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                     <td width="14%">
                        <asp:TextBox ID="COMPUTE_3" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
