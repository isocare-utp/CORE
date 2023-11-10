<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.fund.ws_fund_cancel_payfundcoll_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Style="width: 760px;">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <div style="text-decoration: underline">
                    <span>ค้นหา :<span>
                </div>
            </tr>
        </table>
        <table class="DataSourceFormView">
            <tr>
                <td width="10%">
                    <div>
                        <span>ข้อมูลวันที่ :<span>
                    </div>
                </td>
                <td width="11%">
                    <asp:TextBox ID="ADT_DATE" runat="server" Style="text-align: center;" ></asp:TextBox>
                </td>                
                <td>
                    <asp:Button ID="b_search" runat="server" Text="ค้นหา" Style="width: 80px; height: 30px" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>