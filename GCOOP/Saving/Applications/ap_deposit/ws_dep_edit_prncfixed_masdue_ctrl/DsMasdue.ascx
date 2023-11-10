<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMasdue.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_edit_prncfixed_masdue_ctrl.DsMasdue" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="700px" Height="500px"  ScrollBars="auto">
    <table class="DataSourceRepeater" style="width:700px;">
        <tr>
            <th width="3%">
                ลำดับ
            </th>
            <th width="10%">
                วันที่เริ่ม
            </th>
            <th width="10%">
                วันที่ครบกำหนด
            </th>
            <th width="5%">
                จำนวนครั้ง
            </th>
            <th width="3%">
                จำนวการฝาก
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="seq_no" runat="server"  ToolTip="#,##0" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="start_date" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="end_date" runat="server" Style="text-align: center;" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="peroid_count" runat="server"  ToolTip="#,##0" Style="text-align: right"></asp:TextBox>
                    </td>                   
                    <td>
                        <asp:TextBox ID="peroid_last" runat="server" ToolTip="#,##0" Style="text-align: right" ></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
