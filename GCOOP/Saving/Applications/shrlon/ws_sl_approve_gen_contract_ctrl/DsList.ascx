<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_approve_gen_contract_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="5%">
        </th>
        <th width="13%">
            ใบคำร้อง
        </th>
        <th width="9%">
            ประเภท
        </th>
        <th width="37%">
            ชื่อ - สกุล
        </th>
        <th width="19%">
            เงินขอกู้
        </th>
        <th width="16%">
            เลขสัญญา
        </th>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%" align="center">
                        <asp:CheckBox ID="choose_flag" runat="server" />
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="loanrequest_docno" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="cp_type" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="37%">
                        <asp:TextBox ID="cp_name" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="19%">
                        <asp:TextBox ID="loanrequest_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="16%">
                        <asp:TextBox ID="loancontract_no" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
