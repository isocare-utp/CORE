<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.keeping.ws_kp_adjmth_ccl_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>รายการใบเสร็จ</strong></u></font></span>
<table class="TbStyle" style="width: 710px;">
    <tr>
        <th width="5%">
            No.
        </th>
        <th width="25%">
            รายละเอียด
        </th>
        <th width="10%">
            งวด
        </th>
        <th width="12%">
            ต้นเงิน
        </th>
        <th width="12%">
            ดอกเบี้ย
        </th>
        <th width="12%">
            ดอกเบี้ยค้าง
        </th>
        <th width="12%">
            ยอดชำระ
        </th>
        <th width="12%">
            คู่บัญชี
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center">
    <table class="TbStyle" style="width: 710px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center" ForeColor="Red"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="shrlontype_code" runat="server" Width="20px" ReadOnly="true"></asp:TextBox>
                        <asp:TextBox ID="cp_shrlondesc" runat="server" Width="145px" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="REF_RECVPERIOD" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="principal_adjamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="interest_adjamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="intarrear_adjamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="item_adjamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:DropDownList ID="tofrom_accid" runat="server" Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
<table class="DataSourceFormView" style="width: 710px;">
    <tr>
        <td width="5%">
        </td>
        <td width="25%">
        </td>
        <td width="10%">
        </td>
        <td width="12%">
        </td>
        <td width="12%">
        </td>
        <td width="12%">
            รวมยกเลิก:
        </td>
        <td width="12%">
            <asp:TextBox ID="sum_itemadj" runat="server" Style="text-align: right; background-color: Black;"
                ForeColor="#66FF66" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
        </td>
        <td width="12%">
        </td>
    </tr>
</table>
