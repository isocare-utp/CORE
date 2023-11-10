<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_adt_mbhistory_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="6%">
            ลำดับ
        </th>
        <th width="9%">
            การแก้ไข
        </th>
        <th>
            รายการแก้ไข
        </th>
        <th width="22%">
            ค่าเก่า
        </th>
        <th width="22%">
            ค่าใหม่
        </th>
        <th width="4%">
            
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="seq_no" runat="server" ReadOnly="true"></asp:TextBox>
                    <td>
                        <asp:TextBox ID="modtb_code" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="clm_name" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="clmold_desc" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="clmnew_desc" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btn_detail" runat="server" Text="..." />
                    </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
