﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_ctrl.ws_dlg_sl_editcollateral_master_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" border="0" style="width: 600px;">
            <tr>
                <td colspan="8">
                    <strong style="font-size: 14px;">ค้นหา</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center;"></asp:TextBox>
                    <%--<asp:Button ID="b_memsearch" runat="server" Text="..." Style="width: 20px; margin-left: 2px;" />--%>
                </td>
                <td>
                    <div>
                        <span>ชื่อ-สกุล:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="cp_name" runat="server" Width="295px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>เลขพนักงาน:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="salary_id" runat="server"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span>เลขหลักทรัพย์:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="collmast_no" runat="server"></asp:TextBox>
                </td>
                <td align="right" width="30%">
                    <asp:Button ID="b_search" runat="server" Text="ค้นหา" Width="75px" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
