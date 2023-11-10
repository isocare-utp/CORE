<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.ap_deposit.dlg.wd_dep_search_deptaccount_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Auto">
    <table class="DataSourceRepeater" style="width:650px;margin-left:20px">
        <tr>
            <th width="10%">
                เลขที่บัญชี
            </th>
            <th width="23%">
                ชื่อบัญชี
            </th>
            <th width="12%">
                เลขสมาชิก
            </th>
            <th width="23%">
                ชื่อ - นามสกุล
            </th>
            <th width="12%">
                คงเหลือ
            </th>
        </tr>
    </table>
   <table class="DataSourceRepeater" style="width:650px;margin-left:20px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="10%">
                        <asp:TextBox ID="deptaccount_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="23%">
                        <asp:TextBox ID="deptaccount_name" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td  width="12%">
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="23%">
                        <asp:TextBox ID="fullname" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="prncbal" runat="server" Style="text-align:right;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
<div style="margin-top: 20px; background-color:#DCF4FB;" >
    <table style="width: 400px;" class="tbPage">
        <tr>
            <td style="width: 20px">
                <asp:LinkButton ID="lbFirst" runat="server"  OnClick="lbFirst_Click"><<</asp:LinkButton>
            </td>
            <td style="width: 20px">
                <asp:LinkButton ID="lbPrevious" runat="server"  OnClick="lbPrevious_Click"><</asp:LinkButton>
            </td>
            <td align="center" style="width: 320px">
                <asp:DataList ID="rptPaging" runat="server"  OnItemCommand="rptPaging_ItemCommand"
                    OnItemDataBound="rptPaging_ItemDataBound" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>'
                            CommandName="newPage" Text='<%# Eval("PageText") %> ' Width="40px" >
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
                <asp:Label ID="lblpage" runat="server" Text="" style="color:Blue;font-weight:bolder"></asp:Label>
            </td>
        </tr>
    </table>
</div>