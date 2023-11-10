<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.deposit_const.w_sheet_dp_const_cmucfbankbranch_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="750px">
    <table class="DataSourceRepeater">
        <tr>
            <th width="10%">
                รหัสสาขา
            </th>
            <th width="35%">
                ชื่อสาขาธนาคาร
            </th>
            <th width="15%">
                อำเภอ
            </th>
            <th width="15%">
                จังหวัด
            </th>
            <th width="5%">
                Fee
            </th>
            <th width="15%">
                ค่าบริการ
            </th>
            <th width="5%">
                ลบ!
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="750px" Height="435px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="10%">
                        <asp:TextBox ID="branch_id" runat="server"></asp:TextBox>
                    </td>
                    <td width="35%">
                        <asp:TextBox ID="branch_name" runat="server"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="branch_amphur" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="branch_province" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:CheckBox ID="fee_status" runat="server" Style="text-align: center;" />
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="service_amt" runat="server" Style="text-align: right;"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_delete" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
