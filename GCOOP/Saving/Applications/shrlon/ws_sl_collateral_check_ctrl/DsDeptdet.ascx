<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDeptdet.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_check_ctrl.DsDeptdet" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 270px;">
            <tr>
                <td colspan="2">
                    <strong>รายละเอียดหลักประกัน</strong>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <div>
                        <span>เลขที่บัญชี:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="deptaccount_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อบัญชี:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="deptaccount_name" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเภทบัญชี:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_depttype" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>อ้างอิงทะเบียน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันเปิดบัญชี:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="deptopen_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>ยอดเงิน</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ยกมาต้นปี:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="beginbal" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ยอดคงเหลือ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="prncbal" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ยอดถอนได้:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="withdrawable_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ยอดอายัด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="sequest_amount" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
