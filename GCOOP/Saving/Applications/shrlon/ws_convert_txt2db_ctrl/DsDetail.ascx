<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.shrlon.ws_convert_txt2db_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" class = "Detail_H" runat="server" Height="240px" Width="750px" ScrollBars="Auto"
    HorizontalAlign="Left">
    <table class="DataSourceRepeater" style="width: 1600px;">
        <tr>
            <th style="width: 1.5%;">
                #
            </th>
            <th style="width: 3.5%;">
                ทะเบียน
            </th>
            <th style="width: 9%;">
                ชื่อ - นามสกุล
            </th>
            <th style="width: 3%;">
                Money
            </th>
            <th style="width: 4%;">
                วันที่ชำระ
            </th>
            <th style="width: 4%;">
                Ref. 1
            </th>
            <th style="width: 6%;">
                Ref. 2
            </th>
            <th style="width: 7%;">
                จำนวนเงิน
            </th>
            <th style="width: 1.5%;">
                R
            </th>
            <th style="width: 5%;">
                เช็คจากธนาคาร
            </th>
            <th style="width: 6%;">
                เลขที่เช็ค
            </th>
            <th style="width: 4%;">
                File Docno
            </th>
            <th style="width: 6%;">
                Transaction No
            </th>
            <th style="width: 5%;">
                Bank Code
            </th>
            <th style="width: 5%;">
                Branch Code
            </th>
            <th style="width: 4%;">
                Teller No
            </th>
            <th style="width: 6%;">
                Transaction Type
            </th>
            <th style="width: 6%;">
                Payment Time
            </th>
        </tr>
    </table>
    <table class="DataSourceRepeater" style="width: 1600px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr class="td_row">
                    <td style="width: 1.5%;">
                        <asp:TextBox class="num_row" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 3.5%;">
                        <asp:TextBox ID="ls_memno" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 9%;">
                        <asp:TextBox ID="ls_cusname" class="ls_cusname_" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td style="width: 3%;">
                        <asp:TextBox ID="ls_trnscode" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 4%;">
                        <asp:TextBox ID="ls_paydate" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 4%;">
                        <asp:TextBox ID="ls_ref1" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_ref2" class="ls_ref2_" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td style="width: 7%;">
                        <asp:TextBox ID="ldc_trnsamt" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td style="width: 1.5%; text-align: center;">
                        <asp:CheckBox ID="reject_status" runat="server" />
                    </td>
                    <td style="width: 5%;">
                        <asp:TextBox ID="ls_chqbank" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_chqno" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 4%;">
                        <asp:TextBox ID="ls_filedocno" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_trnsno" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 5%;">
                        <asp:TextBox ID="ls_bankcode" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 5%;">
                        <asp:TextBox ID="ls_branchcode" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 4%;">
                        <asp:TextBox ID="ls_tellerno" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_trnstype" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_paytime" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="ls_accountno" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="ldtm_payment" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="ldc_intallacc" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="membgroup_code" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="chk_member" class="chk_member_c" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
