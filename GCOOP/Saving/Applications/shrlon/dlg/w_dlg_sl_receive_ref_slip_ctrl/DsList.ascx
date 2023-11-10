<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_receive_ref_slip_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" Width="640px" HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 620px;">
            <tr>
                <th width="15%">
                    เลขที่ใบจ่าย/ถอน
                </th>
                <th width="25%">
                    เลขที่บัญชี
                </th>
                <th width="20%">
                    วันที่
                </th>
                <th width="20%">
                    รหัสทำรายการ
                </th>
                <th width="20%">
                    จำนวนเงิน
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="390px" ScrollBars="Auto" Width="640px"
        HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 620px;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="15%">
                            <asp:TextBox ID="slip_no" runat="server" ReadOnly="true" Style="cursor: pointer;
                                text-align: center;"></asp:TextBox>
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="ACC_ID" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="SLIP_DATE" runat="server" ReadOnly="true" Style="cursor: pointer;
                                text-align: center;"></asp:TextBox>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="RECPPAYTYPE_CODE" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="SLIP_AMT" runat="server" ReadOnly="true" Style="cursor: pointer;
                                text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
    <hr width="580" align="left" />
</div>
