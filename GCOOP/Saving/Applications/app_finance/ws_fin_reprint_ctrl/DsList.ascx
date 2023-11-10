<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_reprint_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server"  Height="450px" ScrollBars="Auto" HorizontalAlign="Left">
    <table class="DataSourceRepeater">
        <tr>
            <th width="4%">                
            </th>
            <th width="10%">
                รายการ
            </th>
            <th width="12%">
                เลขที่ใบเสร็จ
            </th>
            <th width="12%">
                วันที่ใบเสร็จ
            </th>
            <th width="10%">
                ทะเบียน
            </th>
            <th width="30%">
                ชื่อ-สกุล
            </th>
            <th width="10%">
                ผู้ทำรายการ
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="checkselect" runat="server" Style="text-align: center" />
                    </td>
                    <td align="center">
                        <asp:TextBox ID="payment_desc" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="slip_no" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td align="center" class="HiddenPanel">
                        <asp:TextBox ID="operate_date" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="member_no" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="pay_towhom" runat="server" ReadOnly="True" Style="text-align: left"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="entry_id" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
