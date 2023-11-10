<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr.dlg.w_dlg_mbshr_mbgain_history_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 630px;">
    <tr>
        <th width="8%">
            ลำดับ
        </th>
        <th width="20%">
            เลขที่ใบคำขอ
        </th>
        <th width="15%">
            เลขสมาชิก
        </th>
        <th width="21%">
            ชื่อ
        </th>
        <th width="21%">
            นามสกุล 
        </th>
        <th>
            วันที่ทำรายการ
        </th>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 630px;">
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td width="8%">
                    <asp:TextBox ID="seq_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="20%">
                    <asp:TextBox ID="gain_docno" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="15%">
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="21%">
                    <asp:TextBox ID="memb_name" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="21%">    
                    <asp:TextBox ID="memb_surname" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="WRITE_DATE" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
