<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCriteria.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_adjust_period_pay_ctrl.DsCriteria" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="20%">
                    <div>
                        <span>ช่วงประเภทเงินกู้:</span>
                    </div>
                </td>
                <td width="25%">
                    <div>
                        <asp:DropDownList ID="sloantype_code" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td width="2%">
                    -
                </td>
                <td width="25%">
                    <div>
                        <asp:DropDownList ID="eloantype_code" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td align="right" colspan="2" width="10%">
                    <div>
                        <asp:Button ID="b_retrieve" runat="server" Text="ดึงข้อมูล" Width="55px" />
                    </div>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
