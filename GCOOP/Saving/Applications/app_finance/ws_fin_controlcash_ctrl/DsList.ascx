<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_controlcash_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />

<asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Auto" >
  
    <span style="text-decoration:underline;color:Navy;font-weight:bolder">รายชื่อผู้ใช้</span>
    <table class="DataSourceRepeater"  style="width:180px;border:0px">
        <asp:Repeater ID="Repeater1" runat="server" >
            <ItemTemplate>
                <tr>   
                    <td style="width:18px;border-width:1px;">
                       <asp:TextBox ID="BOX" runat="server" style="width:18px;" ReadOnly="true" Enabled="false"></asp:TextBox>                       
                    </td>                 
                    <td style="border-width:0px;width:100%">
                        <asp:TextBox ID="USER_NAME" runat="server" style="text-decoration:underline;font-weight:bolder" ReadOnly="true"></asp:TextBox>
                    </td>              
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>