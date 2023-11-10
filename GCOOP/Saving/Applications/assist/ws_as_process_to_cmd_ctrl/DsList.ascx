<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.assist.ws_as_process_to_cmd_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr align="center">  
         
        <th width="15%">
            <span>ทะเบียน</span>
        </th>
        <th width="40%">
            <span>ชื่อ สกุล</span>
        </th>
         
        <th width="30%">
            <span>วิธีการรับ</span>
        </th>
        <th width="15%">
            <span>จำนวน</span>
        </th>

    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="550px" ScrollBars="Auto">
    <table class="DataSourceRepeater">      
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>    
                <tr>
                 
                    <td width="15%">
                        <asp:TextBox ID="member_no" runat="server" Style="text-align:center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="40%">
                        <asp:TextBox ID="full_name" runat="server" Style="text-align:left;" ReadOnly="true"></asp:TextBox>
                    </td>
                   
                    <td width="30%">
                        <asp:TextBox ID="receive_code" runat="server" Style="text-align:center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="amount_amt" runat="server" Style="text-align:right;" ReadOnly="true"></asp:TextBox>
                    </td>

                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>