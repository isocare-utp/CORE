<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.dlg.wd_fin_bankaccount_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center"  Height="500px" ScrollBars="Auto" >
    <table class="DataSourceRepeater" style="width:930px">
        <tr>             
           <th width="25%">
                <span>ธนาคาร</span>
            </th>
            <th width="18%">
                <span>สาขา</span>
            </th>   
            <th width="13%">
                <span>เลขที่บัญชี</span>
            </th>
            <th width="26%">
                <span>ชื่อบัญชี</span>
            </th>
            <th width="10%">
                <span>ประเภทเงินฝาก</span>
            </th>
            <th width="8%">
                <span>สถานะบัญชี</span>
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                 <tr>
                    <td>
                        <asp:TextBox ID="bank_desc" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="branch_name" runat="server" ></asp:TextBox>
                    </td>   
                    <td>
                        <asp:TextBox ID="account_no" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="account_name" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ACCOUNT_DESC" runat="server" style="text-align:center"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="CLOSE_DESC" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
