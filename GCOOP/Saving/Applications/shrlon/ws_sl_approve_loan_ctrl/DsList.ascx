<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_approve_loan_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="4%">
        </th>
        <th width="11%">
            ใบคำร้อง
        </th>
        <th width="8%">
            ประเภท
        </th>
        <th width="33%">
            ชื่อ - สกุล
        </th>
        <th width="15%">
            เงินขอกู้
        </th>
        <th width="17%">
            สถานะ
        </th>
        <th width="13%">
            เลขสัญญา
        </th>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%" align="center">
                        <asp:CheckBox ID="choose_flag" runat="server" />
                    </td>
                    <td width="11%">
                        <asp:TextBox ID="loanrequest_docno" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="cp_type" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="33%">
                        <asp:TextBox ID="cp_name" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="loanrequest_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="17%">
                        <asp:DropDownList ID="loanrequest_status" runat="server">
                            <asp:ListItem Value="8" Text="รออนุมัติ"></asp:ListItem>
                            <asp:ListItem Value="1" Text="อนุมัติ"></asp:ListItem>
                            <asp:ListItem Value="11" Text="อนุมัติ (ไม่สร้างเลข)"></asp:ListItem>
                            <asp:ListItem Value="0" Text="ไม่อนุมัติ"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="loancontract_no" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
