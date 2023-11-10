<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_lnucfprobitemtype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />

<table class="DataSourceRepeater">
    <tr>
        <th width="10%">
            รหัส
        </th>
        <th width="">
            ประเภทการทำรายการสัญญามีปัญหา
        </th>
        <th width="10%">
            ฝั่งรายการ
        </th>
        <th width="5%">
            ลบ
        </th>
    </tr>
    <tbody>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="PROBITEMTYPE_CODE" runat="server" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PROBITEMTYPE_DESC" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="SIGN_FLAG" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
