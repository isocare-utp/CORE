<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wd_main.ascx.cs" Inherits="Saving.Applications.account.w_acc_ucf_accperiod_ctrl.wd_main" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" width="100%">
    <table class="DataSourceRepeater" align="center" style="width: 100%">
        <tr>
            <th width="20%">
                ปีบัญชี
            </th>
            <th width="20%">
                วันที่เริ่มต้น
            </th>
            <th width="20%">
                วันที่สิ้นสุด
            </th>
            <th width="10%">
                สถานะ
            </th>
            
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center"  width="100%" >
    <table class="DataSourceRepeater" align="center" style="width: 100%">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="20%">
                        <asp:TextBox ID="account_year" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="beginning_of_accou" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="ending_of_account" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="10%">
                       
                        <asp:CheckBox ID="close_account_stat"  Enabled = "false" runat="server"/>
                        
                    </td>
                   
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
