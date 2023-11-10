<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wd_list.ascx.cs" Inherits="Saving.Applications.account.w_acc_ucf_accperiod_ctrl.wd_list" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" width="100%">
    <table class="DataSourceRepeater" align="center" style="width: 100%">
        <tr>
            <th width="10%">
                งวดที่
            </th>
            <th width="30%">
                วันสิ้นงวด
            </th>
            <th width="20%">
                ปีบัญชี
            </th>
            <th width="20%">
                งวดที่ยกมา
            </th>
            <th width="10%">
                สถานะ
            </th>
             <th width="10%">
               
            </th>
            
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center"  width="100%" >
    <table class="DataSourceRepeater" align="center" style="width: 100%">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="10%">
                        <asp:TextBox ID="period" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="period_end_date" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="account_year" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="period_prev" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                     
                    <td width="10%">
                       
                        <asp:CheckBox ID="close_flag" runat="server"    Enabled = "false"/>
                        
                    </td>
                    <td width="10%">
                     <asp:Button ID="b_delete" runat="server" Text="ลบ" />
                      </td>
                   
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
