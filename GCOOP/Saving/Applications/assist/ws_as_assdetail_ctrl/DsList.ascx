<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.assist.ws_as_assdetail_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="210px" Height="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater" style="width: 210px;">
        <tr>
            <th width="10%">
                ประเภท
            </th>
            <th width="50%">
                เลขที่
            </th>
            <th width="40%">
                วันที่อนุมัติ
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="10%" align="center">
                        <asp:TextBox ID="assisttype_code" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="50%">
                        <asp:TextBox ID="asscontract_no" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="40%">
                        <asp:TextBox ID="approve_date" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
