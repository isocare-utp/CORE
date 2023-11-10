<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_mbucfmembgroup_ctrl.w_dlg_sl_searchmembgroup_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" border="0" style="width: 600px;">
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
                <td width="40%">
                    <asp:TextBox ID="membgroup_desc" runat="server"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:Button ID="b_search" runat="server" Text="ค้นหา" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
