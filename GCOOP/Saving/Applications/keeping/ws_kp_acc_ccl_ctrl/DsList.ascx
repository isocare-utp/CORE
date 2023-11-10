<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.keeping.ws_kp_acc_ccl_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="3%">
        </th>
        <th width="16%">
            รายละเอียดส่งหัก
        </th>
        <th width="14%">
            ประเภทหุ้น/หนี้/<br />
            เงินฝาก/อื่นๆ
        </th>
        <th width="20%">
            รายละเอียดหุ้น/หนี้/<br />
            เงินฝาก/อื่นๆ
        </th>
        <th width="10%">
            ต้นเงิน
        </th>
        <th width="10%">
            ดอกเบี้ย
        </th>
        <th width="10%">
            ยอดเงินรวม
        </th>
        <th width="17%">
            คู่บัญชี
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="200px" Width="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="3%">
                        <asp:TextBox ID="running_number" runat="server" Style="background: #CCCCCC; text-align: center"
                            ForeColor="Red"></asp:TextBox>
                    </td>
                    <%--<td width="4%">
                        <asp:TextBox ID="keepitemtype_code" runat="server" Style="background: #CCCCCC" ForeColor="Red"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="keepitemtype_desc" runat="server" Style="background: #CCCCCC" ForeColor="Red"></asp:TextBox>
                    </td>--%><td width="16%">
                        <asp:TextBox ID="cp_kpitemtype_desc" runat="server" Style="background: #CCCCCC" ForeColor="Red"></asp:TextBox>
                    </td>
                    <%--<td width="4%">
                        <asp:TextBox ID="shrlontype_code" runat="server" Style="background: #CCCCCC" ForeColor="Red"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="loancontract_no" runat="server" Style="background: #CCCCCC" ForeColor="Red"></asp:TextBox>
                    </td>--%><td width="14%">
                        <asp:TextBox ID="cp_shrlon" runat="server" Style="background: #CCCCCC" ForeColor="Red"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="description" runat="server" Style="background: #CCCCCC" ForeColor="Red"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="principal_payment" runat="server" Style="background: #CCCCCC; text-align: right"
                            ForeColor="Red" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="interest_payment" runat="server" Style="background: #CCCCCC; text-align: right"
                            ForeColor="Red" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="item_payment" runat="server" Style="background: #CCCCCC; text-align: right"
                            ForeColor="Red" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="17%">
                        <asp:DropDownList ID="cancel_accid" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
