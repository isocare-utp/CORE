<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsColl.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_contract_adjust_coll_ctrl.DsColl" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 760px;">
    <tr>
        <th style="font-size: 12px">
            ประเภทหลักประกัน
        </th>
        <th style="font-size: 12px">
            เลขอ้างอิง<br />
            หลักประกัน
        </th>
        <th style="font-size: 12px">
            รายละเอียด
        </th>
        <th style="font-size: 12px">
            ค้ำคงเหลือ
        </th>
        <th style="font-size: 12px">
            ค้ำประกัน
        </th>
        <th style="font-size: 12px">
            %ค้ำได้
        </th>
        <th>
        </th>
        <th>
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td width="10%">
                    <asp:DropDownList ID="loancolltype_code" runat="server" Style="font-size: 11px;">
                    </asp:DropDownList>
                </td>
                <td width="12%">
                    <asp:TextBox ID="ref_collno" runat="server" Style="width: 53px; font-size: 11px;
                        text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Style="width: 25px; margin-left: 1px;
                        font-size: 11px;" />
                </td>
                <td width="40%">
                    <asp:TextBox ID="description" runat="server" Style="font-size: 11px;"></asp:TextBox>
                </td>
                <td width="12%">
                    <asp:TextBox ID="collbalance_amt" runat="server" Style="text-align: right; font-size: 11px;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="12%">
                    <asp:TextBox ID="collactive_amt" runat="server" Style="text-align: right; font-size: 11px;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="8%">
                    <asp:TextBox ID="collactive_percent" runat="server" Style="text-align: right; font-size: 11px;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td align="center" width="3%">
                    <asp:Button ID="b_show" runat="server" Text="..." Style="width: 20px; font-size: 11px;" />
                </td>
                <td align="center" width="3%">
                    <asp:Button ID="b_del" runat="server" Text="-" Style="width: 20px; font-size: 11px;" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
   
</table>
 <table class="DataSourceFormView">
            <tr>

            <td width="10%"></td>
                <td width="12%">
                    
                </td>
                <td width="42%"> 

                </td>
                <td width="12%">
                   รวม:
                </td>
                <td width="12%">
                   <asp:TextBox ID="sum_collactive_amt" runat="server" Style="text-align: right; margin-left: 4px;
                        background-color: Black;" ForeColor="#66FF66" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="8%">
                    <asp:TextBox ID="sum_collactive_percent" runat="server" Style="text-align: right; margin-left: 4px;
                        background-color: Black;" ForeColor="#66FF66" ToolTip="#,##0.00"></asp:TextBox>
                <td align="center" width="3%">
                   
                </td>
                <td align="center" width="3%">
                   
                </td>
               
            </tr>
        </table>
