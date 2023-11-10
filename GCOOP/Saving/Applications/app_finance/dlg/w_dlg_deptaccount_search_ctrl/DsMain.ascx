<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.dlg.w_dlg_deptaccount_search_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" Width="500px" HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 500px;">
            <tr>       
                <th width="10%">
                    เลขบัญชี 
                </th>
                <th width="20%">
                    ชื่อบัญชี
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="390px" ScrollBars="Auto" Width="500px"
        HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 500px;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="10%">
                            <asp:TextBox ID="ACCOUNT_NO" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="ACCOUNT_NAME" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
    <hr width="580" align="left" />
</div>