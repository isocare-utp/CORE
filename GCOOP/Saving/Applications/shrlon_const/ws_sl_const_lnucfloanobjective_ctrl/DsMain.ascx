<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" 
Inherits="Saving.Applications.shrlon_const.ws_sl_const_lnucfloanobjective_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="5%">
                    <span>ประเภทเงินกู้ :</span>
                </td>
                <td width="15%" align="left">
                    <asp:DropDownList ID="loantype_code" runat="server" Style="margin-right: 20px" BackColor="#FFFFCC">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>