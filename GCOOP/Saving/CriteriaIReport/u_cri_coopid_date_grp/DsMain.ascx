﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_date_grp.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td width="25%">
                    <div>
                        <span>สาขา:</span>
                    </div>
                </td>
                <td  width="12%">
                    <asp:TextBox ID="coopid" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตั้งแต่วันที่:</span>
                    </div>
                </td>
                <td  width="10%">
                    <asp:TextBox ID="as_date" runat="server"></asp:TextBox>
                </td>
                <td width="50%">
                    <asp:DropDownList ID="as_month" runat="server">
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
                <td>
                    <asp:TextBox ID="as_year" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ถึงวันที่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="as_date2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="as_month2" runat="server">
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
                <td>
                    <asp:TextBox ID="as_year2" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
