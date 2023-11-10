<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wd_list.ascx.cs" Inherits="Saving.Applications.account.w_acc_ucf_accyear_ctrl.wd_list" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <tr>
            <th width="20%">
                ปีบัญชี
            </th>
            <th width="20%">
                วันที่เริ่มต้นรอบปี
            </th>
            <th width="20%">
                วันที่สิ้นสุดรอบบัญชี
            </th>
            <th width="10%">
                สถานะปิดสิ้นปี
            </th>
            <th width="5%">
                 
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
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
                       
                        <asp:CheckBox ID="close_account_stat" runat="server"/>
                        
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_delete" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
