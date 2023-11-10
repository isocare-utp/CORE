<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsInt.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_contract_adjust_ctrl.DsInt" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 760px;">
            <tr>
                <td width="20%">
                    <div>
                        <span style="font-size: 12px">การคิด ด/บ:</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:DropDownList ID="int_continttype" runat="server" Style="width: 245px; font-size: 12px;">
                        <asp:ListItem Value="0">ไม่คิดดอกเบี้ย</asp:ListItem>
                        <asp:ListItem Value="1">อัตราคงที่</asp:ListItem>
                        <asp:ListItem Value="2">ดูอัตราจากประกาศ</asp:ListItem>
                        <asp:ListItem Value="3">อัตราพิเศษเป็นช่วง</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="20%">
                    <div>
                        <span style="font-size: 12px">อัตราคงที่:</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="int_contintrate" runat="server" Style="text-align: right; font-size: 12px;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px">ดูจากตาราง:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="int_continttabcode" runat="server" Style="width: 245px; font-size: 12px;">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px">อัตราเพิ่ม/ลด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="int_contintincrease" runat="server" Style="text-align: right; font-size: 12px;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px">ดูขั้นด/บจาก:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="int_intsteptype" runat="server" Style="width: 245px; font-size: 12px;">
                        <asp:ListItem Value="1">ยอดอนุมัติ</asp:ListItem>
                        <asp:ListItem Value="2">ยอดคงเหลือ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
