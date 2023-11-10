<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCriteria.ascx.cs" 
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_redeem_search_ctrl.DsCriteria" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 550px;">
            <tr>
                <td width="16%">
                    <div>
                        <span>ทะเบียนสมาชิก:</span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <span>ทะเบียนจำนอง:</span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="mrtgmast_no" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
            </tr>
          </table>
    </EditItemTemplate>
</asp:FormView>
