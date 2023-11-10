<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_rdate_durtgrp.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
        <tr>
                <td width="30%">
                    <div>
                        <span>หมวดครุภัณฑ์:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="durtgrp_code" runat="server">
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
                    <div>
                        <asp:TextBox ID="start_date" runat="server" Style="width: 130px;"></asp:TextBox>
                        <asp:TextBox ID="end_date" runat="server" Style="width: 130px;"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
