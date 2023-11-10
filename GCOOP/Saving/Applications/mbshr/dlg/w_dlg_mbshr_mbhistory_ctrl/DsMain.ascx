<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.dlg.w_dlg_mbshr_mbhistory_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 550px;">
            <tr>
                <td width="20%">
                    <div>
                        <span>การแก้ไข :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="modtb_code" runat="server" Style="text-align: center" ReadOnly="true"
                            Width="10%"> </asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รายการแก้ไข :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="clm_name" runat="server" ReadOnly="true" Width="50%"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="height: 50px">
                        <span style="height: 45px;">ค่าเดิม :</span>
                    </div>
                </td>
                <td>
                    <div style="height: 50px">
                        <asp:TextBox ID="clmold_desc" runat="server" ReadOnly="true" TextMode="MultiLine"
                            Width="420px" Height="50px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="height: 50px;">
                        <span style="height: 45px;">ค่าใหม่ :</span>
                    </div>
                </td>
                <td colspan="2">
                    <div style="height: 50px;">
                        <asp:TextBox ID="clmnew_desc" runat="server" ReadOnly="true" TextMode="MultiLine"
                            Width="420px" Height="50px"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
