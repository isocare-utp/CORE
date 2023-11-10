<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMort.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.DsMort" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 304px;">
            <tr>
                <td width="40%">
                    <div>
                        <span>ราคาจำนองรวม 50%</span>
                    </div>
                </td>
                <td width="60%">
                    <div>
                        <asp:TextBox ID="MORTGAGE_HALFPAYMENT" runat="server" style="text-align:right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>