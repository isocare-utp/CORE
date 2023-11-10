<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_deptadjust_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="13%">
                </td>
                <td width="20%">
                </td>
                <td width="13%">
                </td>
                <td width="5%">
                </td>
                <td width="15%">
                </td>
                <td width="14%">
                </td>
                <td width="20%">
                </td>
            </tr>
            <tr>
                <td>
                    <span>เลขบัญชี :</span>
                </td>
                <td>
                    <asp:TextBox ID="deptaccount_no" runat="server"></asp:TextBox>
                </td>
                <td>
                    <span>ชื่อบัญชี :</span>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="deptaccount_name" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span>เลขสมาชิก :</span>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server"  ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <span>ประเภทบัญชี :</span>
                </td>
                <td>
                    <asp:TextBox ID="depttype_code" runat="server"  ReadOnly="True"></asp:TextBox>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="depttype_desc" runat="server"  ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span>ยอดคงเหลือ :</span>
                </td>
                <td>
                    <asp:TextBox ID="prncbal" runat="server"  ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <span>ยอดถอนได้ :</span>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="withdrawable_amt" runat="server"  ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <span>ดอกเบี้ยสะสม :</span>
                </td>
                <td>
                    <asp:TextBox ID="accuint_amt" runat="server"  ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
