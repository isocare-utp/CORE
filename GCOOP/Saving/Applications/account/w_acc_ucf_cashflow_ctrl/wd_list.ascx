<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wd_list.ascx.cs" Inherits="Saving.Applications.account.w_acc_ucf_cashflow_ctrl.wd_list" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <tr>
         
            <th width="10%">
                ลำดับ
            </th>
            <th  width="35%">
                ชื่อรายการ
            </th>
            <th width="40%">
                รหัสบัญชีที่เกี่ยวข้อง
            </th>
             <th width="10%">
                รายการที่
            </th>
            <th>
                
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                
                    <td width="10%">
                        <asp:TextBox ID="seq_no" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="35%">
                        <asp:TextBox ID="account_name" runat="server"></asp:TextBox>
                    </td>
                     <td width="35%">
                        <asp:TextBox ID="accid_list" runat="server"></asp:TextBox>
                    </td>
                     <td  width="5%">
                        <asp:Button ID="getaccidbutton" runat="server" Text="..." />
                    </td>
                     <td width="10%">
                        <asp:TextBox ID="account_activity" runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:Button ID="b_delete" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
