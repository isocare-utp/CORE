<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_reqgain_true_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="5%">
            ลำดับ
        </th>
        <th width="20%">
            ชื่อ
        </th>
        <th width="20%">
            นามสกุล
        </th>
        <th width="37%">
            ที่อยู่ผู้รับโอน
        </th>
        <th width="15%">
            ความสัมพันธ์
        </th>
        <th width="3%">
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="running_number" runat="server" Style="text-align: center" ReadOnly="true"
                        BackColor="#cccccc"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="gain_name" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="gain_surname" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="gain_addr" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="gain_relation" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="condition_type" runat="server" />
                    <asp:HiddenField ID="condition_etc" runat="server" />
                    <asp:HiddenField ID="write_at" runat="server" />
                    <asp:HiddenField ID="write_date" runat="server" />
                </td>
                <td>
                    <asp:Button ID="b_del" runat="server" Text="-" Width="20" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
