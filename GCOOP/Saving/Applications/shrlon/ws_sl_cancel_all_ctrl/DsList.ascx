<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cancel_all_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="5%">
        </th>
        <th width="15%">
        ประเภท
        </th>
        <th width="25%">
        เลขที่ใบเสร็จ
        </th>
        <th width="25%">
        วันที่ใบเสร็จ
        </th>
        <th width="30%">
        จำนวนเงินในใบเสร็จ
        </th>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="500px" Width="750px" ScrollBars="Auto">
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <table class="DataSourceRepeater">
                <tr>
                    <td width="5%">
                        <asp:CheckBox ID="operate_flag" runat="server" />
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="bizztb_type" runat="server"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="bizzslip_no" runat="server"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="bizzslip_date" runat="server"></asp:TextBox>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="bizzslip_amt" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
