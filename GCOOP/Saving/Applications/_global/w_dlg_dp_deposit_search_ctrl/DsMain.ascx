<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications._global.w_dlg_dp_deposit_search_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 600px;">
            <tr>
                <td width="15%">
                    <div>
                        <span>เลขที่บัญชี:</span>
                    </div>
                </td>
                <td width="30%">
                    <div>
                        <asp:TextBox ID="deptaccount_no" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Width="98%"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อบัญชี:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="deptaccount_name" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>สาขา:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="coop_id" runat="server" Width="99%">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อ:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="memb_name" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>นามสกุล:</span>
                    </div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="memb_surname" runat="server" Width="98%"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
