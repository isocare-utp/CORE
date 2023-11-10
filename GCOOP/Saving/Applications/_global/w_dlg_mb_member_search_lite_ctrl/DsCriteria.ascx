<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCriteria.ascx.cs"
    Inherits="Saving.Applications._global.w_dlg_mb_member_search_lite_ctrl.DsCriteria" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 500px;">
            <tr>
                <td width="17%">
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <span>ชื่อ:</span>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <asp:TextBox ID="memb_name" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <span>นามสกุล:</span>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <asp:TextBox ID="memb_surname" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขสัญญา:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="loancontract_no" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>หน่วย:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="membgroup_no" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                        <asp:DropDownList ID="membgroup_nodd" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
