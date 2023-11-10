﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.admin.ws_am_accountyear_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="750px">
    <table class="DataSourceRepeater">
        <tr>
            <th width="10%">
                ปีบัญชี
            </th>
            <th width="25%">
                วันที่เริ่ม
            </th>
            <th width="25%">
                วันที่สิ้นสุด
            </th>
            <th width="8%">
                สถานะปิดปี
            </th>
            <th width="5%">
                ลบ!
            </th>           
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="750px" Height="435px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="10%">
                        <asp:TextBox ID="account_year" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="accstart_date" runat="server"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="accend_date" runat="server"></asp:TextBox>
                    </td>   
                    <td align="center" width="8%">                        
                        <asp:CheckBox ID="accsyscls_status" runat="server" />
                    </td>               
                    <td width="5%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
