<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_deptadjust_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <span>ยอดที่ต้องการปรับ :</span>
                </td>
                <td width="35%">
                    <asp:DropDownList ID="ai_type_dec" runat="server">
                        <asp:ListItem Value="1" Text="ยอดคงเหลือ"></asp:ListItem>
                        <asp:ListItem Value="2" Text="ดอกเบี้ย"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="15%">
                    <span>วันที่ต้องการปรับ :</span>
                </td>
                <td width="35%">
                    <asp:TextBox ID="date_int" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="margin-top: -18px;">ต้องการปรับ :</span>
                </td>
                <td>
                    <asp:DropDownList ID="ai_adj_type" runat="server" style="margin-top: -18px;">
                    
                        <asp:ListItem Value="1" Text="ปรับเพิ่มขึ้น"></asp:ListItem>
                        <asp:ListItem Value="-1" Text="ปรับลดลง"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <span style="margin-top: -18px;">จำนวณเงิน :</span>
                </td>
                <td rowspan="2">
                    <asp:TextBox ID="adc_amount" runat="server" Style="background-color: Black; text-align: right;
                        font-size: 24px;" Height="45" ToolTip="#,##0.00" ForeColor="GreenYellow"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="margin-top: auto;">หมายเหตุ :</span>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="remark" runat="server" TextMode="MultiLine" ></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
