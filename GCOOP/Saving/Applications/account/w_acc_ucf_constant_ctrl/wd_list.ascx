<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wd_list.ascx.cs" Inherits="Saving.Applications.account.w_acc_ucf_constant_ctrl.wd_list" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" width="100%">
    <table class="DataSourceRepeater" align="center"   style="width: 100%" 
        border="0">
        <tr>
           
            <th width="100%">
                รายการ
            </th>
            
            
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto"  HorizontalAlign="Center"  width="100%" >
    <table class="DataSourceRepeater" align="center" style="width: 100%" border="0">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    
                    <td width="100%">
                        <asp:TextBox ID="ACCOUNT_NAME" runat="server" Style="text-align: left;"></asp:TextBox>
                    </td>
                   
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
