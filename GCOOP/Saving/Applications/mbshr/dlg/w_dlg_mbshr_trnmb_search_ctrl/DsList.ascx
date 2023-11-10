<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr.dlg.w_dlg_mbshr_trnmb_search_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 630px;">
    <tr>
        <th width="10%">
            ลำดับ
        </th>
        <th width="20%">
            เลขที่ใบคำขอ
        </th>
        <th width="15%">
            เลขสมาชิก
        </th>
        <th width="55%">
            ชื่อ - นามสกุล
        </th>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 630px;">
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td width="10%">
                    <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                    <asp:TextBox ID="trnmbreq_docno" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="15%">
                    <asp:TextBox ID="memold_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="55%">
                    <asp:TextBox ID="memnameold" runat="server"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
