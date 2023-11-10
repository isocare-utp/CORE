<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon_const.w_sheet_sl_const_lnucfontracttype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="10%">
            รหัส
        </th>
        <th>
            ประเภทสัญญา
        </th>
        <th width="12%">
            สถานะ
        </th>
        <th width="8%">
            ตั้ง ด/บ ค้าง
        </th>
        <th width="8%">
            ตั้งเงินทดลอง
        </th>
        <th width="8%">
            คำนวณ ด/บ ค้าง
        </th>
        <th width="8%">
            ประเภทถัดไป
        </th>
        <th width="5%">
            ลบ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="CONTRACT_TYPE" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="CONTRACTTYPE_DESC" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="PROBLEM_FLAG" runat="server"></asp:TextBox>
                </td>
               
                <td>
                    <asp:TextBox ID="SETINTARR_FLAG" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="SETPAYADVANCE_FLAG" runat="server"></asp:TextBox>
                </td>
                 <td>
                    <asp:TextBox ID="CALINTARR_FLAG" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="NEXTCONTRACT_TYPE" runat="server"></asp:TextBox>
                </td>
                <td >
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
