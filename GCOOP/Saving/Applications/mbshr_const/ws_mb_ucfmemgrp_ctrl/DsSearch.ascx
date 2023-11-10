<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsSearch.ascx.cs" Inherits="Saving.Applications.mbshr_const.ws_mb_ucfmemgrp_ctrl.DsSearch" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" border="0">
            <tr>
                <td>
                    <asp:Button ID="b_add" runat="server" Text="เพิ่มสังกัด" />
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>รหัส :</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="membgroup_code" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span>ชื่อหน่วย :</span>
                    </div>
                </td>
                <td width="35%">
                    <asp:TextBox ID="membgroup_desc" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="b_search" runat="server" Text="ค้นหา" Width="50px" />
                    <asp:Button ID="b_next" runat="server" Text="ถัดไป" Width="50px" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หน่วยคุม :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="cp_groupcontrol" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
