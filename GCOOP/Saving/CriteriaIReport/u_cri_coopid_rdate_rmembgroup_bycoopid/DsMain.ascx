﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_rdate_rmembgroup_bycoopid.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td width="30%">
                    <div>
                        <span>ตั้งแต่สาขา:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="30%">
                    <div>
                        <span>ถึงสาขา:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="coop_id2" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตั้งแต่วันที่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="start_date" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ถึงวันที่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="end_date" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        
                        <span>ตั้งแต่สังกัด:</span>
                    </div>
                </td>
                <td>
                <asp:DropDownList ID="start_membgroup" runat="server">
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ถึงสังกัด:</span>
                    </div>
                </td>
                <td>
                <asp:DropDownList ID="end_membgroup" runat="server">
                        </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
