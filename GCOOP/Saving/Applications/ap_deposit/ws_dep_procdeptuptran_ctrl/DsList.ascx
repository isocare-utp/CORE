<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_procdeptuptran_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="15%">
            ทะเบียน
        </th>
        <th width="25%">
            ชื่อสมาชิก
        </th>
         <th width="15%">
            เลขที่บัญชี
        </th>
        <th width="25%">
            ชื่อบัญชี
        </th>
        <th width="20%">
            ยอดทำรายการ
        </th>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server"  ScrollBars="Auto"  Height="800px">
<table class="DataSourceRepeater" >
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td width="15%">
                    <asp:TextBox ID="MEMBER_NO" runat="server"  style="text-align:center;" ></asp:TextBox>
                </td>
                <td width="25%">
                    <asp:TextBox ID="FULLNAME" runat="server"></asp:TextBox>
                </td>
                <td width="15%">
                    <asp:TextBox ID="DEPTACCOUNT_NO" runat="server"  style="text-align:center;" ></asp:TextBox>
                </td>   
                <td width="25%">
                    <asp:TextBox ID="DEPTACCOUNT_NAME" runat="server" ></asp:TextBox>
                </td> 
                <td width="20%">
                    <asp:TextBox ID="DEPTITEM_AMT" runat="server" ToolTip="#,##0.00" style="text-align:right"></asp:TextBox>
                </td>             
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
</asp:Panel>