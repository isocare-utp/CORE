<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_export_mrtgmast_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="12%">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <span>ประเภทหลักทรัพย์:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:DropDownList ID="assettype_code" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>ประเภทจำนอง:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:DropDownList ID="mortgage_type" runat="server">
                            <asp:ListItem Text="" Value="-1" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0">แปลงเดียว</asp:ListItem>
                            <asp:ListItem Value="1">จำนองเฉพาะส่วน</asp:ListItem>
                            <asp:ListItem Value="2">จำนวนรวมหลายแปลง</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
                <td align="right">
                    <asp:Button ID="b_retrieve" runat="server" Text="ดึงข้อมูล" Width="80px" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
