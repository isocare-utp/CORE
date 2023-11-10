<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsShrmonth.ascx.cs"
    Inherits="Saving.Applications.divavg.ws_divsrv_detail_ctrl.DsShrmonth" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 12px;"><font color="#330066"><u><strong>รายการปันผลแบบเดือน</strong></u></font></span>
<table class="DataSourceFormView" align="center">
    <tr>
        <td width="15%">
            <span>อัตราปันผล:(%)</span>
        </td>
        <td width="15%">
            <asp:TextBox ID="div_rate" runat="server" Style="text-align: center;" ToolTip="#,##0.00"></asp:TextBox>
        </td>
        <td>
        </td>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center">
        <tr>
            <th width="5%">
                ลำดับ
            </th>
            <th width="10%">
                เดือน
            </th>
            <th width="15%">
                มูลค่าหุ้น(คำนวณ)
            </th>
            <th width="15%">
                ปันผล(บาท)
            </th>
            <th width="15%">
                ปันผลจริง(บาท)
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="mth_code" runat="server" Style="text-align: left;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="sharecal_value" runat="server" Style="text-align: right;" ToolTip="#,##0.000"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="div_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.0000000"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="rdiv_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
