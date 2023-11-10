<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.assist.dlg.wd_as_search_deptaccount_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" Width="500px" HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 500px;">
            <tr>  
                <th width="20%">
                    ประเภทเงินฝาก 
                </th>             
                <th width="10%">
                    เลขบัญชี 
                </th>
                <th width="20%">
                    ชื่อบัญชี
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="390px" ScrollBars="Auto" Width="500px"
        HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 500px;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="20%">
                            <asp:TextBox ID="display" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="deptaccount_no" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="deptaccount_name" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
    <hr width="580" align="left" />
</div>
<div style="margin-top: 20px;">
<center>
    <table style="width: 200px;" class="tbPage">
        <tr>
            <td style="width: 20px">
                <asp:LinkButton ID="lbFirst" runat="server" OnClick="lbFirst_Click"><<</asp:LinkButton>
            </td>
            <td style="width: 20px">
                <asp:LinkButton ID="lbPrevious" runat="server" OnClick="lbPrevious_Click"><</asp:LinkButton>
            </td>
            <td style="width: 390px">
                <asp:DataList ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand"
                    OnItemDataBound="rptPaging_ItemDataBound" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>'
                            CommandName="newPage" Text='<%# Eval("PageText") %> ' Width="20px">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:DataList>
            </td>
            <td style="width: 20px">
                <asp:LinkButton ID="lbNext" runat="server" OnClick="lbNext_Click">></asp:LinkButton>
            </td>
            <td style="width: 20px">
                <asp:LinkButton ID="lbLast" runat="server" OnClick="lbLast_Click">>></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="text-align: center">
                <asp:Label ID="lblpage" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    </center>
</div>
