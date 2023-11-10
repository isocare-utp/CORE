<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_const_dpucfrecppaytype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="8%">
            รหัส
        </th>
        <th width="30%">
            ประเภทการทำรายการ
        </th>
        <th>
            ฝั่งรายการ
        </th>
        <th>
            รหัส Stm
        </th>
        <th>
            ประเภทเงิน
        </th>
        <th>
            รหัสกลุ่ม
        </th>
        <th width="8%">
            ฝั่งบ/ช
        </th>
        <th width="8%">
            ทำ VC
        </th>
        <th width="8%">
            กลุ่ม VC
        </th>
        <th width="5%">
            ลบ!
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="RECPPAYTYPE_CODE" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="RECPPAYTYPE_DESC" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="recppaytype_flag" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="stm_itemtype" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="moneytype_support" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="group_itemtpe" runat="server"></asp:TextBox>
                </td>
                <td>
                    <%--<asp:TextBox ID="accnature_flag" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="accnature_flag" runat="server">
                        <asp:ListItem Text="" Value="0"></asp:ListItem>
                        <asp:ListItem Text="DR" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="CR" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <%--<asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>--%>
                    <asp:CheckBox ID="genvc_flag" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="accmap_code" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
