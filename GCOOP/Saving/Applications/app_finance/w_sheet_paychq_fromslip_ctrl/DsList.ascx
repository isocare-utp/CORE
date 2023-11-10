<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.w_sheet_paychq_fromslip_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 720px;">
        <tr>
            <th width="2%">
            </th>
            <th width="1%">
                ลำดับ
            </th>
            <th width="6%">
                วันที่
            </th>
             <th width="13%">
                สั่งจ่าย
            </th>
             <th width="15%">
                รายละเอียดการสั่งจ่าย
            </th>
            <th width="10%">
                หมายเหตุ
            </th>
            <th width="6%">
                จำนวนเงิน
            </th>        
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="400px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 720px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td style=" width:24px">
                        <asp:CheckBox ID="choose_flag" runat="server" Style="text-align: center" />
                    </td>
                    <td style="width:34px">
                        <asp:TextBox ID="running_number" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    
                    <td style="width:75px">
                        <asp:TextBox ID="entry_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width:167px">
                        <asp:TextBox ID="pay_towhom" runat="server" ></asp:TextBox>
                    </td>
                    <td style="width:188px">
                        <asp:TextBox ID="PAYMENT_DESC" runat="server" ></asp:TextBox>
                    </td>
                    <td style="width:128px">
                        <asp:TextBox ID="REMARK" runat="server" ></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="item_amtnet" runat="server" ReadOnly="true" ToolTip="#,##0.00" style="text-align:right"></asp:TextBox>
                    </td>                  
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
