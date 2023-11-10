<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.w_sheet_canecelchq_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Style="width: 760px;">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <div style="text-decoration: underline; text-align: left; font-size: 15px; font-style: inherit;
                    color: #191970">
                    <span>เงื่อนไขค้นหา :</span>
                </div>
            </tr>
        </table>
        <table class="DataSourceFormView">
            <tr>
                <td width="7%">
                    <div>
                        <span>วันที่ :<span>
                    </div>
                </td>
                <td width="11%">
                    <asp:TextBox ID="start_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="7%">
                    <div>
                        <span>ถึงวันที่ :<span>
                    </div>
                </td>
                <td width="12%">
                    <asp:TextBox ID="end_date" runat="server" Style="text-align: center;" MaxLength="8"></asp:TextBox>
                </td>
                <td width="5%">
                    <asp:Button ID="b_search" runat="server" Text="ค้นหา" Style="text-align: center;
                        height: 30px; width: 100%; font-size: 18px" />
                </td>
            </tr>
        </table>
        <table class="DataSourceFormView">
            <tr>
                <td width="7%">
                    <div>
                        <span>เลขที่เช็ค :<span>
                    </div>
                </td>
                <td width="8%">
                    <asp:TextBox ID="chq_no" runat="server" Style="text-align: center;" MaxLength="10"></asp:TextBox>
                </td>
                <td width="7%">
                    <div>
                        <span>ธนาคาร :<span>
                    </div>
                </td>
                <td width="12%">
                    <asp:DropDownList ID="bank_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="7%">
                    <div>
                        <span>สาขา :<span>
                    </div>
                </td>
                <td width="12%">
                    <asp:DropDownList ID="branch_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
