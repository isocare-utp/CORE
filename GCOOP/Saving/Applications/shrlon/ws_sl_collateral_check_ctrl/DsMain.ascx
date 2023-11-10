<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_check_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 770px;">
        
            <tr>
                <td>
                <asp:Button ID="b_print" runat="server" Text="พิมพ์สิทธิการค้ำ" />
                    <strong>หลักประกัน</strong>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>ประเภทหลักประกัน:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="colltype_code_name" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="colltype_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่อ้างอิง:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="collateral_no" runat="server" Style="width: 110px; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Style="width: 25px; margin-left: 7px;" />
                </td>
                <td>
                    <asp:TextBox ID="collateral_desc" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
