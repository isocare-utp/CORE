<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.deposit_const.w_sheet_dp_const_cmucfbankbranch_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 400px;">
            <tr>
                <td width="35%">
                    <div>
                        <span>เลือกรหัสธนาคาร</span>
                    </div>
                </td>
                <td width="65%">
                    <div>
                        <asp:DropDownList ID="bank_code" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
