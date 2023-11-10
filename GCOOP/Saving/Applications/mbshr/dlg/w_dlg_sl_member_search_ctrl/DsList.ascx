<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr.dlg.w_dlg_sl_member_search_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" Width="580px" HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 560px;">
            <tr>
                <th width="15%">
                    รหัสพนักงาน
                </th>
                <th width="15%">
                    ทะเบียน
                </th>
                <th width="35%">
                    ชื่อ - นามสกุล
                </th>
                <th width="35%">
                    สังกัด
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="390px" ScrollBars="Auto" Width="580px"
        HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 560px;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="15%">
                            <asp:TextBox ID="SALARY_ID" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="MEMBER_NO" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="35%">
                            <asp:TextBox ID="FULLNAME" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="35%">
                            <asp:TextBox ID="MEMBGROUP_DESC" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
    <hr width="580" align="left" />
</div>
