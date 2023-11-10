<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsGain.ascx.cs" Inherits="Saving.Applications.assist.ws_as_request_ctrl.DsGain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr align="center">
        <th width="5%">
            <span>ลำดับ</span>
        </th>
        <th width="72%">
            <span>ชื่อผู้รับผลประโยชน์</span>
        </th>
        <th width="20%">
            <span>เกี่ยวข้องเป็น</span>
        </th>
        <th width="3%">
            <span>ลบ</span>
        </th>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:TextBox ID="seq_no" runat="server" Style="text-align: center;" MaxLength="2" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td width="72%">
                        <asp:TextBox ID="gain_name" runat="server" MaxLength="100"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:DropDownList ID="gainrelation_code" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="3%">
                        <asp:Button ID="b_del" runat="server" Text="-" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
