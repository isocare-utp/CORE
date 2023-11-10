<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_oper_cls_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 250px">
            <tr>
                <td width="25%">
                    <div>
                        <span>ปีปันผล:</span></div>
                </td>
                <td width="25%">
                    <div>
                        <asp:TextBox ID="div_year" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
