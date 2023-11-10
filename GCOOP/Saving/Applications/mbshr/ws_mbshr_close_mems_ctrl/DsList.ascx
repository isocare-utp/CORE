<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_close_mems_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 720px;">
    <tr>
        <th width="4%">
            <asp:CheckBox ID="cb_all" runat="server" />
        </th>
        <th width="4%">
        </th>
        <th width="8%">
            ทะเบียน
        </th>
        <th width="22%">
            ชื่อ-ชื่อสกุล
        </th>
        <th width="22%">
            สังกัด
        </th>
        <th width="10%">
            วันที่ลาออก
        </th>
        <th width="25%">
            สาเหตุที่ลาออก
        </th>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="450px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 720px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%" align="center">
                        <asp:CheckBox ID="operate_flag" runat="server" />
                    </td>
                    <td width="4%" align="center">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="22%">
                        <asp:TextBox ID="cp_name" runat="server"></asp:TextBox>
                    </td>
                    <td width="22%">
                        <asp:TextBox ID="cp_groupdesc" runat="server"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="resign_date" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="resigncause_desc" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
