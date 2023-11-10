<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsKeepdata.ascx.cs"
    Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl.DsKeepdata" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>รายการเรียกเก็บประจำเดือน</strong></u></font></span>
<table class="TbStyle" style="width: 710px;">
    <tr>
        <th width="15%">
            ประจำงวด
        </th>
        <th width="15%">
            วันที่ใบเสร็จ
        </th>
        <th width="15%">
            เลขที่ใบเสร็จ
        </th>
        <th width="20%">
            ยอดเรียกเก็บในใบเสร็จ
        </th>
        <th width="20%">
            สังกัด
        </th>
        <th width="10%">
            สถานะ
        </th>
        <th width="5%">
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="TbStyle">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="15%">
                        <asp:TextBox ID="recv_period" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="receipt_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="receipt_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="receive_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="membgroup_code" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="cp_keeping_status" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_detail" runat="server" Text=".." />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
