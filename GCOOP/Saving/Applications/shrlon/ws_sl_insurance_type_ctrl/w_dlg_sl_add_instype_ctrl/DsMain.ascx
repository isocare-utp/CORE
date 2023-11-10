<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_insurance_type_ctrl.w_dlg_sl_add_instype_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" border="0" style="width: 600px;">
            <tr>
                <td colspan="4" align="right">
                    <asp:Button ID="b_save" runat="server" Text="บันทึก" Style="width: 50px;" />
                </td>
            </tr>
                <tr>
                    <td>
                        <u><b>รายละเอียด</b></u>
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        <div>
                            <span>รหัสประกัน :</span>
                        </div>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="INSTYPE_CODE" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <div>
                            <span>ชื่อประกัน :</span>
                        </div>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="INSTYPE_DESC" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                     <td width="20%">
                        <div>
                            <span>ชื่อบริษัททำประกัน :</span>
                        </div>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="INSCOMPANY_NAME" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
