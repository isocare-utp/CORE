<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.fund.ws_fund_ucf_fundcollkeeprate_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>ประเภทกองทุน:</span>
                    </div>
                </td>
                <td width="40%">
                    <asp:DropDownList ID="FUNDKEEPTYPE" runat="server">
                    </asp:DropDownList>
                </td>               
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>