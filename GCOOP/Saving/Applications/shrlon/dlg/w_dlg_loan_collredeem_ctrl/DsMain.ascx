<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_loan_collredeem_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width:550px;">
            <tr>
                <td width="16%">
                    <div>
                        <span>เลขสหกรณ์:</span>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <span>ประเภทหลักทรัพย์:</span>
                    </div>
                </td>
                <td width="40%">
                    <div>
                        <asp:DropDownList ID="collmasttype_code" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td>
                    <asp:Button ID="b_clear" runat="server" Text="Clear" Style="width: 100px" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อสหกรณ์:</span>
                    </div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="COMPUTE" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:Button ID="b_search" runat="server" Text="ค้น" Style="width: 100px" />
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
