<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_reqchgdept_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />   
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <tr>
            <th width="12%">
                เลขที่คำขอ
            </th>
            <th width="15%">
                เลขที่บัญชี
            </th>
            <th width="15%">
                วันที่เปลี่ยนแปลง
            </th>
            <th width="15%">
                ยอดฝากเก่า
            </th>
            <th width="15%">
                ยอดฝากใหม่
            </th>
            <th width="10%">
                ผู้ทำรายการ
            </th>  
            <th width="20%">
                หมายเหตุ
            </th> 
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="200px" Width="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="12%">
                        <asp:TextBox ID="dpreqchg_doc" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="deptaccount_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="deptmontchg_date" runat="server"  Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="deptmonth_oldamt" runat="server"  ReadOnly="true" style="text-align:right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="deptmonth_newamt" runat="server"  ReadOnly="true" style="text-align:right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="entry_id" runat="server"  Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>   
                     <td width="20%">
                        <asp:TextBox ID="remark" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
