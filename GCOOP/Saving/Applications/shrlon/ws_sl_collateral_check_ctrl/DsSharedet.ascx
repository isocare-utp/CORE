<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsSharedet.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_collateral_check_ctrl.DsSharedet" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 270px;">
            <tr>
                <td width="40%">
                    <div>
                        <span>ยกมาต้นปี:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_sharebegin_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทุนเรือนหุ้น:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_sharestk_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>งวดสะสม:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="last_period" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หุ้น/เดือน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_periodshare_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>การส่งหุ้น:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="payment_status" runat="server">
                        <asp:ListItem Value="1">ส่งปกติ</asp:ListItem>
                        <asp:ListItem Value="-1">งดส่ง</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สถานะหุ้น:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="sharemaster_status" runat="server" style="text-align:center;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
