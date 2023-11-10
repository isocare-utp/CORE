<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.keeping.ws_kp_adjmth_ccl_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>รายละเอียดใบเสร็จ</strong></u></font></span>
<table class="TbStyle" style="width: 650px;">
    <tr>
        <th width="5%">
        </th>
        <th width="18%">
            เลขที่ยกเลิก
        </th>        
        <th width="15%">
            เลขที่ใบเสร็จ
        </th>
        <th width="15%">
            วันที่ใบเสร็จ
        </th>
        <th width="15%">
            ยอดเงิน
        </th>
        <th width="15%">
            ผู้ทำรายการ
        </th>
        <th width="17%">
            วันที่วันที่ยกเลิก
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="120px" ScrollBars="Auto">
    <table class="TbStyle" style="width: 650px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:CheckBox ID="operate_flag" runat="server" />
                    </td>
                    <td width="18%">
                        <asp:TextBox ID="adjslip_no" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>                    
                    <td width="15%">
                        <asp:TextBox ID="receipt_no" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="ref_slipdate" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="slip_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="entry_id" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="17%">
                        <asp:TextBox ID="adjslip_date" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
