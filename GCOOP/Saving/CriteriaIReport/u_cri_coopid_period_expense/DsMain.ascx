<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_period_expense.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td>
                    <div>
                        <span>สาขา:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>ปี:</span></div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="year" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>เดือน:</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="month" runat="server">
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
                        <span>วิธีการจ่ายเงินเดือน:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="expense" runat="server">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="CSH">จ่ายเงินสด</asp:ListItem>
                        <asp:ListItem Value="CBT">บัญชีสาขา</asp:ListItem>
                        <asp:ListItem Value="TRN">บัญชีสหกรณ์</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
