<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cfinttable_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="480px">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 480px">
            <tr>
                <td width="15%">
                    <div>
                        <span>รหัส:</span></div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="loanintrate_code" runat="server" Style="text-align: center; background: #CCCCCC;"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td width="18%">
                    <div>
                        <span>รายละเอียด:</span></div>
                </td>
                <td width="47%">
                    <asp:TextBox ID="loanintrate_desc" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
