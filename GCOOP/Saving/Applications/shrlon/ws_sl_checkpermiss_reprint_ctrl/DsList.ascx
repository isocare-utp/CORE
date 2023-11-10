<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_checkpermiss_reprint_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
       <tr>
        <th width="4%">
        </th>
        <th width="13%">
            ใบคำร้อง
        </th>
        <th width="13%">
            ประเภท
        </th>
        <th width="40%">
            ชื่อ-สกุล
        </th>
        <th width="15%">
            เงินขอกู้
        </th>
        <th  width="15%">
            เลขสัญญา
        </th>
    </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="700px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%">
                        <asp:CheckBox ID="print_flag" runat="server" />
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="loanrequest_docno" runat="server"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="loantype_code" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="40%">
                        <asp:TextBox ID="fullname" runat="server"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="loanrequest_amt" runat="server" ToolTip="#,##0.00" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: right"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
