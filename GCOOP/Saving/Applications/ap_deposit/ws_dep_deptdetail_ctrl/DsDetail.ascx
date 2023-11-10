<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_deptdetail_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
    <asp:Panel ID="Panel2" runat="server" Height="425px" ScrollBars="Auto" HorizontalAlign="Center">
<table class="TbStyle" style="width: 1300px;">
    <tr>
        <th rowspan="2" width="2%">
            ลำดับ
        </th>
        <th rowspan="2" width="7%">
            วันที่
        </th>
        <th rowspan="2" width="5%">
            รายการ
        </th>
        <th rowspan="2" width="9%">
            อ้างอิง
        </th>
        <th rowspan="2" width="8%">
            ถอน
        </th>
        <th colspan="2" width="8%">
            ฝาก
        </th>
        <th colspan="2" width="13%">
            ยอดคงเหลือ
        </th>
        <th rowspan="2" width="10%">
            Div สะสม
        </th>
        <th rowspan="2" width="10%">
            ผู้บันทึก
        </th>
        <th rowspan="2" width="6%">
            อ้างอิง
        </th>
        <th rowspan="2" width="6%">
            ต้นเงิน
        </th>
        <th rowspan="2" width="10%">
            Div ครั้งนี้
        </th>
        <th rowspan="2" width="10%">
           ค่าปรับ
        </th>
        
    </tr>
</table>

    <table class="TbStyle" style="width: 1300px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="width: 37px;">
                        <asp:TextBox ID="SEQ_NO" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 84px;">
                        <asp:TextBox ID="ENTRY_DATE" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 60px;">
                        <asp:TextBox ID="cp_book_flag" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 110px;">
                        <asp:TextBox ID="DEPTSLIP_NO" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 98px;">
                        <asp:TextBox ID="cp_withdraw" runat="server" Style="text-align: right; background-color:#DDDDDD" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td style="width: 97px;">
                        <asp:TextBox ID="cp_deposit" runat="server" Style="text-align: right;background-color:#A3F3C6" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td style="width: 162px;">
                        <asp:TextBox ID="PRNCBAL" runat="server" Style="text-align: right;" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td style="width: 123px;">
                        <asp:TextBox ID="ACCUINT_AMT" runat="server" Style="text-align: right;" ReadOnly="true" ToolTip="#,##0.0000"></asp:TextBox>
                    </td>
                    <td style="width: 123px;">
                        <asp:TextBox ID="ENTRY_ID" runat="server" Style="text-align: center;" ReadOnly="true" ></asp:TextBox>
                    </td>
                    <td style="width: 72px;">
                        <asp:TextBox ID="REF_SEQ_NO" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td style="width: 72px;">
                        <asp:TextBox ID="PRNC_NO" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 123px;">
                        <asp:TextBox ID="INT_AMT" runat="server" Style="text-align: center;" ToolTip="#,##0.0000"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="CHRG_AMT" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
