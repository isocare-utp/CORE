<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_auditloan_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 730px;">
            <tr>
                <td colspan="8">
                    <strong style="font-size: 14px;">แก้ไขรายละเอียดสัญญา</strong>
                </td>
            </tr>
            <tr>
                <td width="12%">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="17%">
                    <asp:TextBox ID="member_no" runat="server" Style="width: 90px; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_memsearch" runat="server" Text="..." Style="width: 22px;" />
                </td>
                <td width="14%">
                    <div>
                        <span>ชื่อ-ชื่อสกุล:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="cp_name" runat="server" BackColor="#DDDDDD" ReadOnly="True"></asp:TextBox>
                </td>
                <td width="14%">
                    <div>
                        <span>วันที่เป็นสมาชิก:</span>
                    </div>
                </td>
                <td width="14%">
                    <asp:TextBox ID="member_date" runat="server" Style="text-align: center;" BackColor="#DDDDDD"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สังกัด:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="cp_membgroup" runat="server" BackColor="#DDDDDD" ReadOnly="True"></asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span>ทุนเรือนหุ้น:</span>
                    </div>
                </td>
                <td width="17%">
                    <asp:TextBox ID="cp_sharestk" runat="server" ToolTip="#,##0.00" Style="text-align: right;"
                        BackColor="#DDDDDD" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เงินเดือน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="salary_amount" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                        ReadOnly="True" BackColor="#E8E8E8"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
