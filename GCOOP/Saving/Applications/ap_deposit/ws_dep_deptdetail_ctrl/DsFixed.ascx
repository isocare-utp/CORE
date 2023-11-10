<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsFixed.ascx.cs"
    Inherits="Saving.Applications.ap_deposit.ws_dep_deptdetail_ctrl.DsFixed" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel2" runat="server" Width="700px"  Height="425px" ScrollBars="Auto" HorizontalAlign="Center">
<table class="TbStyle" style="width: 1500px;">
    <tr>
        <th rowspan="2" width="3%">
            ต้นเงิน
        </th>
        <th rowspan="2" width="7%">
            จำนวนต้นเงิน
        </th>
        <th rowspan="2" width="10%">
            คงเหลือ
        </th>
        <th rowspan="2" width="8%">
            วันที่ต้นเงิน
        </th>
        <th rowspan="2" width="8%">
            วันครบกำหนด
        </th>
        <th width="8%">
            ทำรายการล่าสุด
        </th>
        <th width="5%">
            Int. ค้าง
        </th>
        <th rowspan="2" width="6%">
            ยอดจ่าย Int.
        </th>
        <th rowspan="2" width="6%">
            ยอดภาษี
        </th>
        <th rowspan="2" width="6%">
            อัตรา Int.
        </th>
        <th rowspan="2" width="6%">
            ยอดเช็คเรียกเก็บ
        </th>
        <th rowspan="2" width="8%">
            สถานะ
        </th>
        <th rowspan="2" width="11%">
           การรับดอกเบี้ย
        </th>
        <th >
        </th>
        
    </tr>
</table>

    <table class="TbStyle" style="width: 1500px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="width:3%;">
                        <asp:TextBox ID="PRNC_NO" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width:7%;">
                        <asp:TextBox ID="PRNC_AMT" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td style="width:10%;">
                        <asp:TextBox ID="PRNC_BAL" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td >
                    <td style="width:8%;">
                        <asp:TextBox ID="PRNC_DATE" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width:8%;">
                        <asp:TextBox ID="PRNCDUE_DATE" runat="server" Style="text-align: center;" ReadOnly="true" ></asp:TextBox>
                    </td>
                    <td style="width:8%;">
                        <asp:TextBox ID="LASTCALINT_DATE" runat="server" Style="text-align: center;" ReadOnly="true" ></asp:TextBox>
                    </td>
                    <td style="width:5%;">
                        <asp:TextBox ID="INTARR_AMT" runat="server" Style="text-align: right;" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="INTPAY_AMT" runat="server" Style="text-align: right;" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td style="width:6%;">
                        <asp:TextBox ID="TAXPAY_AMT" runat="server" Style="text-align: right;" ReadOnly="true" ></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="INTEREST_RATE" runat="server" Style="text-align: right;"  ToolTip="#,##0.000%"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="CHEQUEPEND_AMT" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 8%;">
                        <asp:TextBox ID="cp_prnfix" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 11%;">
                        <asp:TextBox ID="cp_upint" runat="server" Style="text-align: center;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="DEPTSLIP_NO" runat="server" Style="text-align: center;" ToolTip="#,##0.00" ></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>  
        </asp:Repeater>
        <tr>
                    <td colspan="2" style="border : 0px">
                    </td>

                    <td width="0%" style="font-size: 20px;">
                        <asp:TextBox ID="sum_brnbal" runat="server" Style="text-align: right ;  font-size: 13px; background-color:#33FF93;"
                        ToolTip="#,##0.00" ReadOnly="True" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
    </table>
</asp:Panel>


