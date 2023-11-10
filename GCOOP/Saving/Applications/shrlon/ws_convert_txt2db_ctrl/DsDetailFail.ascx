<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetailFail.ascx.cs" Inherits="Saving.Applications.shrlon.ws_convert_txt2db_ctrl.DsDetailFail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" class="DetailFail_H" runat="server" Height="240px" Width="750px" ScrollBars="Auto"
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
                <tr class="td_rowFail">
                    <td style="width: 1.5%;">
                        <asp:TextBox class="num_rowFail" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 3.5%;">
                        <asp:TextBox ID="ls_memnoFail" runat="server" ReadOnly="false" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 9%;">
                        <asp:TextBox ID="ls_cusnameFail" class="ls_cusname_Fail" runat="server" ReadOnly="false"></asp:TextBox>
                    </td>
                    <td style="width: 3%;">
                        <asp:TextBox ID="ls_trnscodeFail" runat="server" ReadOnly="false" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 4%;">
                        <asp:TextBox ID="ls_paydateFail" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 4%;">
                        <asp:TextBox ID="ls_ref1Fail" runat="server" ReadOnly="false" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_ref2Fail" runat="server" ReadOnly="false"></asp:TextBox>
                    </td>
                    <td style="width: 7%;">
                        <asp:TextBox ID="ldc_trnsamtFail" class="ldc_trnsamtFail" runat="server" ReadOnly="false" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td style="width: 1.5%; text-align: center;">
                        <asp:CheckBox ID="reject_statusFail" runat="server" />
                    </td>
                    <td style="width: 5%;">
                        <asp:TextBox ID="ls_chqbankFail" runat="server" ReadOnly="false" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_chqnoFail" runat="server" ReadOnly="false" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 4%;">
                        <asp:TextBox ID="ls_filedocnoFail" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_trnsnoFail" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 5%;">
                        <asp:TextBox ID="ls_bankcodeFail" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 5%;">
                        <asp:TextBox ID="ls_branchcodeFail" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 4%;">
                        <asp:TextBox ID="ls_tellernoFail" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_trnstypeFail" runat="server" ReadOnly="false" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_paytimeFail" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="ls_accountnoFail" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="ldtm_paymentFail" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="ldc_intallaccFail" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="membgroup_codeFail" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="chk_memberFail" class="chk_member_cFail" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
