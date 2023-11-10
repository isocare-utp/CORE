<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollmast.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_collateral_check_ctrl.DsCollmast" %>
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
                        <span>เลขที่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="collmast_refno" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเภท:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="collmasttype_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong>ประเมิน</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ราคาที่ดิน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="landestimate_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ราคาบ้าน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="houseestimate_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ราคาประเมิน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="estimate_price" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong>ราคาจำนอง</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จดจำนอง:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mortgage_price" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันจดจำนอง:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mortgage_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>
                            <asp:CheckBox ID="redeem_flag" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ไถ่ถอน:
                        </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="redeem_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
