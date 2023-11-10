<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wd_main.ascx.cs" Inherits="Saving.Applications.account.w_acc_ucf_accmaster_ctrl.wd_main" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" width="100%">
    <table class="DataSourceRepeater" align="center" style="width: 100%">
       <tr>
            <th width="100%">
                รายละเอียด
            </th>
            
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Left"  width="100%" >
    <table class="DataSourceRepeater" align="center" style="width: 100%">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
            
                    <tr >
                        <th width="20%" Style ="text-align: right;" >รหัสบัญชี: </th>
                        <td><asp:TextBox ID="ACCOUNT_ID" runat="server" Style="text-align: left;"></asp:TextBox></td>
                    </tr>
                     <tr >
                         <th width="20%" Style ="text-align: right;" >แผนก/ธุรกิจ:</th>
                        <td> <asp:TextBox ID="SECTION_ID" runat="server" Style="text-align: left;"></asp:TextBox></td>
                    </tr>
                    <tr >
                         <th width="20%" Style ="text-align: right;" >ชื่อบัญชี:</th>
                         <td><asp:TextBox ID="ACCOUNT_NAME" runat="server" Style="text-align: left;"></asp:TextBox></td>
                    </tr>
                        <tr >
                         <th width="20%" Style="text-align: right;" >ระดับบัญชี:</th>
                        <td><asp:DropDownList ID="account_level" runat="server">
                        <asp:ListItem Value="1">หมวดบัญชี</asp:ListItem>
                        <asp:ListItem Value="2">หัวข้อกลุ่ม</asp:ListItem>
                        <asp:ListItem Value="3">หัวข้อกลุ่มย่อย</asp:ListItem>
                        <asp:ListItem Value="4">รายละเอียด</asp:ListItem>
                        </asp:DropDownList>
                        </td>
                        </tr>
                        <tr >
                         <th width="20%" Style="text-align: right;" >ประเภทบัญชี:</th>
                        <td> 
                        <asp:DropDownList ID="account_type_id" runat="server"></asp:DropDownList>
                    </td>
                        </tr>
                        <tr >
                         <th width="20%" Style="text-align: right;" >ออกงบทดลอง:</th>
                         <td> <asp:CheckBox ID="ON_REPORT" runat="server" Style="text-align: right;"  ></asp:CheckBox></td>
                        
                        </tr>
                        <tr >
                         <th width="20%" Style="text-align: right;" >หมวดบัญชี:</th>
                        <td> 
                        <asp:DropDownList ID="account_group_id" runat="server"></asp:DropDownList>
                    </td>
                        </tr>
                       
                        <tr >
                         <th width="20%" Style="text-align: right;" >ฝั่งบัญชี:</th>
                        <td> <asp:TextBox ID="ACCOUNT_NATURE" runat="server" Style="text-align: left;"></asp:TextBox></td>
                        </tr>
                         <tr >
                         <th width="20%" Style="text-align: right;" >Active:</th>
                           <td> <asp:CheckBox ID="ACTIVE_STATUS" runat="server"  Style="text-align: left;"></asp:CheckBox></td>
                       
                        </tr>
                         <tr >
                         <th width="20%" Style="text-align: right;" >รหัสบัญชีคุมยอด:</th>
                        <td> <asp:TextBox ID="ACCOUNT_CONTROL_ID" runat="server" Style="text-align: left;"></asp:TextBox></td>
                        </tr>
                         <tr >
                         <th width="20%" Style="text-align: right;" >กิจกรรม:</th>
                        <td> <asp:TextBox ID="account_activity" runat="server" Style="text-align: left;"></asp:TextBox></td>
                        </tr>
                         <tr >
                         <th width="20%" Style="text-align: right;" >หมวดงบการเงิน:</th>
                        <td> <asp:TextBox ID="acc_astimate_dc" runat="server" Style="text-align: left;"></asp:TextBox></td>
                        </tr>
                        <tr >
                         <th width="30%" Style="text-align: right;" >หมวดบัญชีกรณีข้ามฝั่ง:</th>
                        <td>
                        <asp:DropDownList ID="account_rev_group" runat="server"></asp:DropDownList>
                         
                         </td>
                        </tr>
                        <tr>
                        <div align =center>
                        
                        <asp:Button ID="bt_addaccid" runat="server" Width="30%"  Text="เพิ่ม" />
                           <asp:Button ID="bt_edit" runat="server" Width="30%"  Text="แก้ไข" />
                            <asp:Button ID="bt_delete" runat="server" Width="30%"  Text=" ลบ " />
                           </div>
                        </tr>
                       
            </ItemTemplate>
        </asp:Repeater>
    </table>
    
   
</asp:Panel>
