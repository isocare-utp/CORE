<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_cmucfsalarybalance_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <thead>
        <tr>
            <th width="8%">
                รหัส
            </th>
            <th width="30%">
                รายละเอียด
            </th>
            <th width="10%">
                คงเหลือ(บาท)
            </th>
            <th width="10%">
                คงเหลือ(%)
            </th>
            <th width="17%">
                คลาดเคลื่อนไม่เกิน(บาท)
            </th>
            <th width="10%">
                นับรวมเงินฝากรายเดือน
            </th>
            <th width="5%">
            ลบ
        </th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="SALARYBAL_CODE" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="SALARYBAL_DESC" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="SALARYBAL_AMT" runat="server" style="text-align:right;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="SALARYBAL_PERCENT" runat="server" ToolTip="###.00%" style="text-align:right;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="INCREMENT_AMT" runat="server" style="text-align:right;"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:CheckBox ID="CHKDEPT_FLAG" runat="server" />
                    </td>
                    <td >
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
