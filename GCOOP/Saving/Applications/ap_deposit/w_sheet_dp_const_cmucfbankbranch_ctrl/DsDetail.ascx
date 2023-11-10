<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_const_cmucfbankbranch_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
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
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="branch_id" runat="server" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="branch_name" runat="server" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="branch_amphur" runat="server" ReadOnly = "true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="branch_province" runat="server" ReadOnly = "true" ></asp:TextBox>
                </td>
                <td align ="center">
                    <asp:CheckBox ID="fee_status" runat="server" Style="text-align: center;"/>
                </td>
                <td>
                    <asp:TextBox ID="service_amt" runat="server" Style="text-align: right;" ></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="b_delete" runat="server" Text="..." />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>