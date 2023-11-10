<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_npl_contcoll.DsList" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 260px;">
    <tr>
        <th>
            <div style="text-align:left">
                เลือกสถานะลูกหนี้
            </div>
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td align="left" style="border: none; background: none;">
                    <asp:CheckBox ID="CHECK_FLAG" runat="server" Text='<%#Eval("lawtype_desc")%>' />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
