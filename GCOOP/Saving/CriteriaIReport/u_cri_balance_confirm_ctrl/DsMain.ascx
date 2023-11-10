<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_balance_confirm_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td width="30%">
                    <div>
                        <span>ยอด ณ สิ้นปี/เดือน:</span></div>
                </td>
                <td width="35%">
                    <asp:TextBox ID="bal_yyyy" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="bal_mm" runat="server">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">มกราคม</asp:ListItem>
                        <asp:ListItem Value="2">กุมภาพันธ์</asp:ListItem>
                        <asp:ListItem Value="3">มีนาคม</asp:ListItem>
                        <asp:ListItem Value="4">เมษายน</asp:ListItem>
                        <asp:ListItem Value="5">พฤษภาคม</asp:ListItem>
                        <asp:ListItem Value="6">มิถุนายน</asp:ListItem>
                        <asp:ListItem Value="7">กรกฎาคม</asp:ListItem>
                        <asp:ListItem Value="8">สิงหาคม</asp:ListItem>
                        <asp:ListItem Value="9">กันยายน</asp:ListItem>
                        <asp:ListItem Value="10">ตุลาคม</asp:ListItem>
                        <asp:ListItem Value="11">พฤศจิกายน</asp:ListItem>
                        <asp:ListItem Value="12">ธันวาคม</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันที่เอกสาร:</span></div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="document_date" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>
                            <asp:CheckBox ID="operate_flag_1" runat="server" Checked="true" />&nbsp; ตามหน่วย</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="membgroup_start" runat="server">
                    </asp:DropDownList>
                    -
                </td>
                <td width="35%">
                    <asp:DropDownList ID="membgroup_end" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>
                            <asp:CheckBox ID="operate_flag_2" runat="server" />&nbsp; ตามเลขสมาชิก</span></div>
                </td>
                <td>
                    <asp:TextBox ID="memno_start" runat="server" Width="140px" ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>-
                </td>
                <td>
                    <asp:TextBox ID="memno_end" runat="server" ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
