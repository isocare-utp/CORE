<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_cashflow_cash.DsMain" %>
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
                    <asp:DropDownList ID="as_coopid" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
             <td>
                    <div>
                        <span>รหัสบัญชี:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="account_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>ปี:</span></div>
                </td>
                <td width="20%">
                  <asp:DropDownList ID="year" runat="server">
                    </asp:DropDownList>
                    
                </td>
                <td width="20%">
                    <div>
                        <span>เดือน:</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="month" runat="server">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="01">มกราคม</asp:ListItem>
                        <asp:ListItem Value="02">กุมภาพันธ์</asp:ListItem>
                        <asp:ListItem Value="03">มีนาคม</asp:ListItem>
                        <asp:ListItem Value="04">เมษายน</asp:ListItem>
                        <asp:ListItem Value="05">พฤษภาคม</asp:ListItem>
                        <asp:ListItem Value="06">มิถุนายน</asp:ListItem>
                        <asp:ListItem Value="07">กรกฎาคม</asp:ListItem>
                        <asp:ListItem Value="08">สิงหาคม</asp:ListItem>
                        <asp:ListItem Value="09">กันยายน</asp:ListItem>
                        <asp:ListItem Value="10">ตุลาคม</asp:ListItem>
                        <asp:ListItem Value="11">พฤศจิกายน</asp:ListItem>
                        <asp:ListItem Value="12">ธันวาคม</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            
        </table>
    </EditItemTemplate>
</asp:FormView>
