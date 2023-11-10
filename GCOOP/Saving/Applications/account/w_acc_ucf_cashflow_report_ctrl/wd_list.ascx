<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wd_list.ascx.cs" Inherits="Saving.Applications.account.w_acc_ucf_cashflow_report_ctrl.wd_list" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <tr>
         
            <th  width="32%">
                ชื่อรายการ
            </th>
            <th width="40%">
                รหัสบัญชีที่เกี่ยวข้อง
            </th>
             <th width="5%">
                
            </th>
            
            <th width="3%">
                   ค่าคงที่
            </th>
            <th width="7%">
                หัวข้อรายงาน
            </th>
            <th width="13%">
                การคำนวณ
            </th>
            
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="700px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
               
               
                    <td width="32%">
                        <asp:TextBox ID="account_name" runat="server"></asp:TextBox>
                    </td>
                     <td width="40%">
                        <asp:TextBox ID="accid_list" runat="server"></asp:TextBox>
                    </td>
                     <td  width="5%">
                        <asp:Button ID="getaccidbutton" runat="server" Text="..." />
                    </td>
                    
                    <td width="3%">
                      <asp:CheckBox ID="cont_status" runat="server"/>
                        
                    </td>
                    <td  width="7%">
                   
                    <asp:DropDownList ID="report_id" runat="server">
                       
                        <asp:ListItem Value="1 ">1</asp:ListItem>
                        <asp:ListItem Value="21">2.1</asp:ListItem>
                        <asp:ListItem Value="22">2.2</asp:ListItem>
                        <asp:ListItem Value="3 ">3</asp:ListItem>
                        <asp:ListItem Value="41">4.1</asp:ListItem>
                        <asp:ListItem Value="42">4.2</asp:ListItem>
                        <asp:ListItem Value="5 ">5</asp:ListItem>
                        <asp:ListItem Value="6 ">6</asp:ListItem>
                        <asp:ListItem Value="7 ">7</asp:ListItem>
                        <asp:ListItem Value="8 ">8</asp:ListItem>
                        <asp:ListItem Value="9 ">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                        <asp:ListItem Value="13">13</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="17">17</asp:ListItem>
                        <asp:ListItem Value="18">18</asp:ListItem>
                        <asp:ListItem Value="19">19</asp:ListItem>
                       
                    </asp:DropDownList>
                </td>
                     <td  width="13%">
                   
                    <asp:DropDownList ID="account_activity" runat="server">
                       
                        <asp:ListItem Value="1 ">ประจำเดือน</asp:ListItem>
                        <asp:ListItem Value="od">เดือนก่อนหน้า</asp:ListItem>
                       
                    </asp:DropDownList>
                </td>
                    
                    
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
