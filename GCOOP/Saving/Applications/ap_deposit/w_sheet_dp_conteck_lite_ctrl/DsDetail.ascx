<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_conteck_lite_ctrl.DsDetail" %>
<style type="text/css">
    .TbDetail
    {
        font-family: Tahoma;
        font-size: 13px;
        border: none;
    }
    .TbDetail th
    {
        font-family: Tahoma;
        font-size: 13px;
        font-weight: normal;
        text-align: center;
        border: 1px solid #000000;
        background-color: rgb(211, 231, 255);
        height: 22px;
    }
    .TbDetail td
    {
        height: 22px;
        border: 1px solid #000000;
        background-color: rgb(255, 255, 255);
    }
    .TbDetail input[type=text], select
    {
        font-family: Tahoma;
        font-size: 13px;
        font-weight: normal;
        border: 0px solid #000000;
        width: 100%;
        height: 100%;
        margin: 0px 0px 0px 0px;
        vertical-align: middle;
    }
    .TbDetail input[type=button]
    {
        height: 100%;
        width: 100%;
    }
</style>
<table width="735" class="TbDetail" cellpadding="2" cellspacing="2">
    <thead>
        <tr>
            <th width="7%">
                ลำดับ
            </th>
            <th width="14%">
                วันที่
            </th>
            <th width="15%">
                รายการ
            </th>
            <th width="15%">
                ถอน
            </th>
            <th width="15%">
                ฝาก
            </th>
            <th width="15%">
                ยอดคงเหลือ
            </th>
            <th>
                พิมพ์ book
            </th>
            <th>
                พิมพ์ card
            </th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="seq_no" runat="server" Style="text-align: center;" ReadOnly="true"
                            ToolTip="0"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="entry_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="CP001" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="EX_WID_AMT" runat="server" Style="text-align: right;" ReadOnly="true"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="EX_DEP_AMT" runat="server" Style="text-align: right;" ReadOnly="true"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="prncbal" runat="server" Style="text-align: right;" ReadOnly="true"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="prntopb_status" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="prntocard_status" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
