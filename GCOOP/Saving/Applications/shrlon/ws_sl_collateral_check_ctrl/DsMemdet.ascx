<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMemdet.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_check_ctrl.DsMemdet" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 270px;">
            <tr>
                <td colspan="2">
                    <strong><u>รายละเอียดหลักประกัน</u></strong>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <div>
                        <span>ประเภทสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="membtype_desc" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หน่วย:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_membgroup" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันเกิด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="birth_date" runat="server" Style="width: 90px; text-align: center;"></asp:TextBox>
                    <asp:TextBox ID="birth_age" runat="server" Style="width: 58px; text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันเกษียณ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="retry_date" runat="server" Style="width: 90px; text-align: center;"></asp:TextBox>
                    <asp:TextBox ID="retry_age" runat="server" Style="width: 58px; text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันเป็นสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_date" runat="server" Style="width: 90px; text-align: center;"></asp:TextBox>
                    <asp:TextBox ID="member_age" runat="server" Style="width: 58px; text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันเข้าทำงาน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="work_date" runat="server" Style="width: 90px; text-align: center;"></asp:TextBox>
                    <asp:TextBox ID="work_age" runat="server" Style="width: 58px; text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เงินเดือน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="salary_amount" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สิทธิค้ำสูงสุด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_collmax_amt" runat="server" Style="text-align: right; background-color:red; font-weight:bold" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สิทธิ์คำคงเหลือ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_collbalance_amt" runat="server" Style="text-align: right; background-color:Lime; font-weight:bold" ToolTip="#,##0.00" BackColor="Lime"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><u>รายละเอียดหุ้น</u></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทุนเรือนหุ้น:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_sharestk_value" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หุ้น/เดือน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="last_period" runat="server" Style="width: 50px; text-align: center;"></asp:TextBox>
                    <asp:TextBox ID="cp_last_period" runat="server" Style="width: 98px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>การส่งหุ้น:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="payment_status" runat="server" Enabled="false">
                        <asp:ListItem Value="1">ส่งปกติ</asp:ListItem>
                        <asp:ListItem Value="-1">งดส่งหุ้น</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
