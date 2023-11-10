<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.assist.ws_as_genrequest_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel" runat="server">
    <table cellspacing="0" rules="all" class="DataSourceRepeater" border="1" style="border-collapse: collapse;
        width: 1113px">
        <tr align="center">
            <th width="3%">
            </th>
            <th width="5%">
                <span>ลำดับ</span>
            </th>
            <th width="8%">
                <span>ทะเบียน</span>
            </th>
            <th width="16%">
                <span>ชื่อ สกุล</span>
            </th>
            <th width="10%">
                <span>วันที่เป็นสมาชิก</span>
            </th>
            <th width="7%">
                <span>อายุสมาชิก</span>
            </th>
            <th width="5%">
                <span>อายุ</span>
            </th>
            <th width="7%">
                <span>งวดที่จ่าย</span>
            </th>
            <th width="10%">
                <span>จำนวนเงินตามสิทธิ์</span>
            </th>
            <th width="10%">
                <span>รายการหัก</span>
            </th>
            <th width="10%">
                <span>จำนวนเงิน</span>
            </th>
            <th width="13%">
                <span>เลขบัญชี</span>
            </th>
        </tr>
    </table>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 570px; width: 1113px">
        <table cellspacing="0" rules="all" class="DataSourceRepeater" style="width: 1113px;
            border-collapse: collapse;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="3%" align="center">
                            <asp:CheckBox ID="choose_flag" runat="server" />
                        </td>
                        <td width="5%">
                            <asp:TextBox ID="seq_no" runat="server" Style="text-align: center;" ReadOnly="true" ToolTip="#,##0"></asp:TextBox>
                        </td>
                        <td width="8%">
                            <asp:TextBox ID="member_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="16%">
                            <asp:TextBox ID="memb_name" runat="server" Style="text-align: left;" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="member_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="7%">
                            <asp:TextBox ID="mem_age" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="5%">
                            <asp:TextBox ID="birth_age" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="7%">
                            <asp:TextBox ID="period_pay" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="maxpermiss_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="assistcut_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="itempay_amt" class="amount" runat="server" Style="text-align: right;"
                                ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="account_no" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                                ReadOnly="true" Width="83%"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Panel>
<div id="paging" style="margin-top: 20px; background-color:#DCF4FB;" >
<center>
    <table  class="tbPage" width=" 1113px">
        <tr>
            <td style="width: 20px">
                <asp:LinkButton ID="lbFirst" runat="server"  OnClick="lbFirst_Click"><<</asp:LinkButton>
            </td>
            <td style="width: 20px">
                <asp:LinkButton ID="lbPrevious" runat="server"  OnClick="lbPrevious_Click"><</asp:LinkButton>
            </td>
            <td align="center" style="width: 320px;font-size:12px;font-weight:bold;">
                <asp:DataList ID="rptPaging" runat="server"  OnItemCommand="rptPaging_ItemCommand"
                    OnItemDataBound="rptPaging_ItemDataBound" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>'
                            CommandName="newPage" Text='<%# Eval("PageText") %> ' Width="30px">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:DataList>
            </td>
            <td style="width: 20px">
                <asp:LinkButton ID="lbNext" runat="server"  OnClick="lbNext_Click">></asp:LinkButton>
            </td>
            <td style="width: 20px">
                <asp:LinkButton ID="lbLast" runat="server"  OnClick="lbLast_Click">>></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="5">
                <asp:Label ID="lblpage" runat="server" Text="" style="color:Blue;font-weight:normal;font-size:12px"></asp:Label>
            </td>
        </tr>
    </table>
    </center>
</div>
