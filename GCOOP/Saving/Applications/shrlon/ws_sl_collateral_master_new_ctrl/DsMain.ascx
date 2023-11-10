<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_new_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 720px;">
            <tr>
                <td colspan="8">
                    <strong style="font-size: 14px;">รายละเอียดสมาชิก</strong>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>เลขประจำตัว:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="member_no" runat="server" Style="width: 95px; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_memsearch" runat="server" Text="..." Style="width: 25px; margin-left: 7px;" />
                </td>
                <td width="15%">
                    <div>
                        <span>ชื่อ-สกุล:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_name" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>ทุนเรือนหุ้น:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="sharestk_value" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span>หน่วย:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_memgroup" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
