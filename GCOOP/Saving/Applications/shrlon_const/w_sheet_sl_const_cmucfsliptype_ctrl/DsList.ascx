<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon_const.w_sheet_sl_const_cmucfsliptype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel2" runat="server" Width="750px"  ScrollBars="Auto">
    <table class="DataSourceRepeater" style="width:900px;">
        <tr>
            <th width="7%">
                รหัส
            </th>
            <th>
                ประเภทรายการ
            </th>
            <th width="5%">
                รหัสหุ้น
            </th>
            <th width="5%">
                รหัสหนี้
            </th>
            <th width="17%">
                ชำระโดย
            </th>
            <th width="7%">
                ยกเลิกหุ้น
            </th>
            <th width="7%">
                ยกเลิกหนี้
            </th>
            <th width="7%">
                Sort Order
            </th>
            <th width="7%">
                ฝั่งรายการ
            </th>
            <th width="7%">
                Manual Flag
            </th>
            <th width="5%">
                ลบ
            </th>
        </tr>


        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td >
                        <asp:TextBox ID="SLIPTYPE_CODE" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="SLIPTYPE_DESC" runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="SHSTM_ITEMTYPE" runat="server"></asp:TextBox>
                    </td>
                     <td>
                        <asp:TextBox ID="LNSTM_ITEMTYPE" runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="MONEYTYPE_SUPPORT" runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="CSHSTM_ITEMTYPE" runat="server"></asp:TextBox>
                    </td>
                   
                    <td >
                        <asp:TextBox ID="CLNSTM_ITEMTYPE" runat="server"></asp:TextBox>
                    </td>

                    <td >
                        <asp:TextBox ID="SLIPTYPE_SORT" runat="server"></asp:TextBox>
                    </td>
                   
                    
                    
                     <td >
                        <asp:TextBox ID="SLIPTYPESIGN_FLAG" runat="server"></asp:TextBox><%--ฝั่งรายการ--%>
                    </td>
                    
                    <td align="center">
                        <asp:CheckBox ID="SLIPMANUAL_FLAG" runat="server" />
                    </td>
                    <td >
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
