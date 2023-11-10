<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.divavg.dlg.w_dlg_search_bankbranch_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 400px;">
            <tr>
                <td width="30%">
                    <div>
                        <span>ธนาคาร:</span>
                    </div>
                </td>
                <td width="70%">
                    <div>
                        <asp:DropDownList ID="bank_code" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สาขา:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="branch_id" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <tr>
                    <td>
                        <div>
                            <span>เลขบัญชี:</span>
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID="expense_accid" runat="server" Style="text-align: center"></asp:TextBox>
                        </div>
                    </td>
                </tr>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="b_submit" runat="server" Text="ตกลง" Width="50" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อสาขา:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="branch_name" runat="server" Style="width: 205px"></asp:TextBox>
                        <asp:Button ID="b_search" runat="server" Text="ค้นหา" Width="50" />
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
