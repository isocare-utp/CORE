<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_receipandpay_cash_daily_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 750px;">
            <tr>
                <td width="15%">
                    <div>
                        <span>วันที่ป้อน:</span>
                    </div>
                </td>
                <td width="15%">
                    <asp:TextBox ID="entry_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span>เงื่อนไขการดูข้อมูล:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="allentry_flag" runat="server">
                        <asp:ListItem Value="0">รวมทุกรหัสผู้ใช้</asp:ListItem>
                        <asp:ListItem Value="1">ตามรหัสผู้ใช้</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td rowspan="2" colspan="2" align="center">
                    <asp:Button ID="b_sum" runat="server" Text="ดึงข้อมูล" Style="width: 100px; height: 50px;" Font-Bold="True" BackColor="#CCCCFF" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จำนวนเงินสด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="receipt_coop" runat="server" style="text-align:right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ผู้ป้อน:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="entry_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รับชำระเงินกู้รวม:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="sumreceipt_loan" runat="server" style="text-align:right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>จ่ายเงินกู้รวม:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="sumpay_loan" runat="server" style="text-align:right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span>คงเหลือสุทธิ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="netpay_loan" runat="server" style="text-align:right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
