<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dwlist.ascx.cs" Inherits="Saving.Applications.keeping.w_sheet_sl_update_data_from_disk_ctrl.Dwlist" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
    <table class="DataSourceRepeater" style="width: 740px;">
        <tr>
            <th style="width: 8%;">
                เลขพนักงาน
            </th>
            <th style="width: 10%;">
                เลขสมาชิก
            </th>
            <th style="width: 10%;">
                รหัสรายการ
            </th>
            <th style="width: 10%;">
                สถานะ (ปกติ,โอน)
            </th>
            <th style="width: 10%;">
                เรียกเก็บได้
            </th>
            <th style="width: 10%;">
                สังกัด
            </th>
            <th style="width: 10%;">
                ประจำเดือน
            </th>
            <th style="width: 10%;">
                เงินเดือน
            </th>
            <th style="width: 10%;">
                ค่าไฟฟ้า
            </th>
            <th style="width: 10%;">
                ธนาคาร
            </th>
            <th style="width: 10%;">
                บัญชีธนาคาร
            </th>
            <th style="width: 10%;">
                ผ่าน
            </th>
        </tr>
    </table>
    <asp:Panel ID="Panel1" class = "Detail_H" runat="server" Height="240px" Width="755px" ScrollBars="Auto"
    HorizontalAlign="Left">
    <table class="DataSourceRepeater" style="width: 740px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr class="td_row">
                    <td style="width: 8%;">
                        <asp:TextBox ID="salary_id" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:TextBox ID="member_no" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:TextBox ID="itemtype_code" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:TextBox ID="contract_type" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:TextBox ID="item_amt" runat="server" ReadOnly="true" ToolTip="#,##0.00" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:TextBox ID="membgroup_code" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:TextBox ID="period" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:TextBox ID="salary_amount" runat="server" ReadOnly="true" ToolTip="#,##0.00" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:TextBox ID="electrictcity_amt" runat="server" ReadOnly="true" ToolTip="#,##0.00" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:TextBox ID="bank_code" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:TextBox ID="bank_accid" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 10%; text-align: center;">
                        <asp:CheckBox ID="post_status" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
