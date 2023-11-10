<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsStatement.ascx.cs" Inherits="Saving.Applications._global.w_dlg_sl_detail_contract_ctrl.DsStatement" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" ScrollBars="Auto"   Height="402px">
    <table class="DataSourceRepeater" style="width: 1500px;">
        <tr>
             <th width="2%">
                ลำดับ
            </th>
            <th width="6%">
                วันทำรายการ
            </th>
            <th width="6%">
                วันที่ใบเสร็จ
            </th>
            <th >
                เลขที่ใบเสร็จ
            </th>
            <th >
                รายการ
            </th>
            <th >
                งวด
            </th>
            <th >
                CR เงินต้น
            </th>
            <th >
                CR ดอกเบี้ย
            </th>
            <th >
                DR เงินต้น
            </th>
             <th >
                DR ดอกเบี้ย
            </th>
             <th >
                คงเหลือ
            </th>
             <th>
                ด/บ ตั้งแต่
            </th>
             <th >
                ด/บ ถึง
            </th>
             <th>
                ด/บ งวด
            </th>
             <th >
                ด/บ ค้าง
            </th>
             <th >
                ด/บ คืน
            </th>
             <th>
                ประเภทเงิน
            </th>
             <th width="15%">
                หมายเหตุ
            </th>
        </tr>
         <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td  width="2%">
                        <asp:TextBox ID="running_number" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="operate_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="slip_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="ref_docno" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="loanitemtype_code" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td  width="2%">
                        <asp:TextBox ID="period" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td  >
                        <asp:TextBox ID="COMPUTE_1" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td > 
                        <asp:TextBox ID="COMPUTE_2" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                     <td >
                        <asp:TextBox ID="COMPUTE_3" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                      <td >
                        <asp:TextBox ID="COMPUTE_4" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                     <td width="6%">
                        <asp:TextBox ID="principal_balance" runat="server" ReadOnly="true"  Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                     <td width="6%">
                        <asp:TextBox ID= "calint_from" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                     <td width="6%" >
                        <asp:TextBox ID="calint_to" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                     <td >
                        <asp:TextBox ID="interest_period" runat="server" ReadOnly="true"  Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                     <td >
                        <asp:TextBox ID="interest_arrear" runat="server" ReadOnly="true"  Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                     <td >
                        <asp:TextBox ID="interest_return" runat="server" ReadOnly="true"  Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                     <td >
                        <asp:TextBox ID="moneytype_code" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                     <td width = "15%">
                        <asp:TextBox ID="remark" runat="server" ReadOnly="true" ></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>