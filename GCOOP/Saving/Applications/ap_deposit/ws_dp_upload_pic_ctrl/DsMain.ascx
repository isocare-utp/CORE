<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dp_upload_pic_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="20%">
                    <div>
                        <span>เลขบัญชีเงินฝาก :</span>
                    </div>
                </td>
                <td width="80%">
                    <div>
                        <asp:TextBox ID="deposit_no" runat="server" Style="width: 120px;"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:Button ID="b_del" runat="server" Text="ลบรูป" Style="width: 100px" />
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
