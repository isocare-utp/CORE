﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsSearch.ascx.cs" Inherits="Saving.Applications._global.w_dlg_ln_loanrequest_search_ctrl.DsSearch" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <div align="left" style="width: 710px;">
        <asp:Panel ID="Panel1" runat="server">
            <table class="DataSourceRepeater" style="width: 690px; margin-left: 3px;">
                <tr>
                    <th width="5%">
                        ป.
                    </th>
                    <th width="12%">
                        เลขใบคำขอ
                    </th>
                    <th width="12%">
                        วันที่จ่าย
                    </th>
                    <th width="10%">
                        เลขสมาชิก
                    </th>
                    <th width="12%">
                        เลขสัญญา
                    </th>
                    <th>
                        ชื่อ - สกุล
                    </th>
                    <th width="10%">
                        จ่ายกู้เป็น
                    </th>
                    <th width="8%">
                        สถานะ
                    </th>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" Height="366px" ScrollBars="Auto">
            <table class="DataSourceRepeater" style="width: 690px; margin-left: 3px;">
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td width="5%">
                                <asp:TextBox ID="loantype_code" runat="server" ReadOnly="true" Style="text-align: center;
                                    cursor: pointer;"></asp:TextBox>
                            </td>
                            <td width="12%">
                                <asp:TextBox ID="loanrequest_docno" ReadOnly="true" Style="cursor: pointer;" runat="server"></asp:TextBox>
                            </td>
                            <td width="12%">
                                <asp:TextBox ID="loanrcvfix_date" runat="server" ReadOnly="true" Style="text-align: center;
                                    cursor: pointer;"></asp:TextBox>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="member_no" runat="server" ReadOnly="true" Style="text-align: center;
                                    cursor: pointer;"></asp:TextBox>
                            </td>
                            <td width="12%">
                                <asp:TextBox ID="loancontract_no" runat="server" ReadOnly="true" Style="cursor: pointer;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="compute_name" runat="server" ReadOnly="true" Style="cursor: pointer;"></asp:TextBox>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="expense_code" runat="server" ReadOnly="true" Style="text-align: center;
                                    cursor: pointer;"></asp:TextBox>
                            </td>
                            <td width="8%">
                                <asp:TextBox ID="STATUS_DESC" runat="server" ReadOnly="true" Style="cursor: pointer;"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:Panel>
    </div>
</div>
