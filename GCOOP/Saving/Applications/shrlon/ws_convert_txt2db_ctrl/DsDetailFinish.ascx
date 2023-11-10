<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetailFinish.ascx.cs" Inherits="Saving.Applications.shrlon.ws_convert_txt2db_ctrl.DsDetailFinish" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" class = "Detail_F" runat="server" Height="240px" Width="750px" ScrollBars="Auto"
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
                <tr class="td_rowFinish">
                    <td style="width: 1.5%;">
                        <asp:TextBox class="num_rowFinish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 3.5%;">
                        <asp:TextBox ID="ls_memnoFinish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 9%;">
                        <asp:TextBox ID="ls_cusnameFinish" class="ls_cusname_Finish" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td style="width: 3%;">
                        <asp:TextBox ID="ls_trnscodeFinish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 4%;">
                        <asp:TextBox ID="ls_paydateFinish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 4%;">
                        <asp:TextBox ID="ls_ref1Finish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_ref2Finish" class="ls_ref2_Finish" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td style="width: 7%;">
                        <asp:TextBox ID="ldc_trnsamtFinish" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td style="width: 1.5%; text-align: center;">
                        <asp:CheckBox ID="reject_statusFinish" Checked=true runat="server" />
                    </td>
                    <td style="width: 5%;">
                        <asp:TextBox ID="ls_chqbankFinish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_chqnoFinish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 4%;">
                        <asp:TextBox ID="ls_filedocnoFinish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_trnsnoFinish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 5%;">
                        <asp:TextBox ID="ls_bankcodeFinish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 5%;">
                        <asp:TextBox ID="ls_branchcodeFinish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 4%;">
                        <asp:TextBox ID="ls_tellernoFinish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_trnstypeFinish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 6%;">
                        <asp:TextBox ID="ls_paytimeFinish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="ls_accountnoFinish" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="ldtm_paymentFinish" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="ldc_intallaccFinish" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="membgroup_codeFinish" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="td_hiddenF">
                        <asp:TextBox ID="chk_memberFinish" class="chk_member_cFinish" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
