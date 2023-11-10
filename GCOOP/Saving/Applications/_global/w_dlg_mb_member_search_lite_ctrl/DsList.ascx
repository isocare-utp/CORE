<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications._global.w_dlg_mb_member_search_lite_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" Width="566px">
        <table class="DataSourceRepeater" style="width: 550px;">
            <tr>
                <th width="14%">
                    ทะเบียน
                </th>
                <th width="43%">
                    ชื่อ - นามสกุล
                </th>
                <th width="43%">
                    หน่วย
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="390px" ScrollBars="Auto" HorizontalAlign="Left"
        Width="566px">
        <table class="DataSourceRepeater" style="width: 550px;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="14%">
                            <asp:TextBox ID="MEMBER_NO" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="43%">
                            <asp:TextBox ID="FULLNAME" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="43%">
                            <asp:TextBox ID="FULLMEMBGROUP" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
    <hr align="left" style="width: 96%;" />
</div>
