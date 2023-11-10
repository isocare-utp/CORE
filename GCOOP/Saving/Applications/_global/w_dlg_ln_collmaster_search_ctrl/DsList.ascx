<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications._global.w_dlg_ln_collmaster_search_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server">
    <table class="DataSourceRepeater" style="width: 700px;">
        <tr>
            <th width="12%">
                ทะเบียนหลักทรัพย์
            </th>
            <th width="12%">
                เลขที่หลักทรัพย์
            </th>
            <th width="10%">
                ประเภทหลักทรัพย์
            </th>
            <th width="14%">
                จดจำนอง
            </th>
            <th width="12%">
                เลขสมาชิก
            </th>
            <th width="26%">
                ชื่อ-นามสกุล
            </th>
            <th width="8%">
                ประเภทผู้กู้
            </th>
            <th width="6%">
                ไถ่ถอน
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="262px" Width="718px" ScrollBars="Auto">
    <table class="DataSourceRepeater" style="width: 700px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="12%">
                        <asp:TextBox ID="COLLMAST_NO" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="COLLMAST_REFNO" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="COLLMASTTYPE_DESC" runat="server" ReadOnly="true" Style="cursor: pointer;"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="MORTGAGE_PRICE" runat="server" ReadOnly="true" ToolTip="#,##0.00"
                            Style="cursor: pointer; text-align: right;"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="MEMCO_NO" runat="server" ReadOnly="true" Style="cursor: pointer;
                            text-align: center;"></asp:TextBox>
                    </td>
                    <td width="26%">
                        <asp:TextBox ID="FULLNAME" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="COLLMASTMAIN_DESC" runat="server" ReadOnly="true" Style="cursor: pointer;
                            text-align: center;"></asp:TextBox>
                    </td>
                    <td width="6%" align="center">
                        <asp:CheckBox ID="REDEEM_FLAG" runat="server" Enabled="false" Style="cursor: pointer;" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
