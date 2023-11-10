<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
        <tr>
        
                <td colspan="6" align="left">
                    <asp:DropDownList ID="coop_id" runat="server" Style="margin-right: 20px" >
                    </asp:DropDownList>
                </td>
        </tr>
            <tr>
                <td width="20%" align="left">
                    <asp:DropDownList ID="checkselect" runat="server" Style="margin-right: 20px" BackColor="#FFFFCC">
                        <asp:ListItem Value="0">ไม่เลือกทั้งหมด</asp:ListItem>
                        <asp:ListItem Value="1">เลือกทั้งหมด</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="15%">
                <div><span>ทะเบียน :</span></div>
                </td>
                <td width="20%">
                <div>
                    <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                </div>
                </td>
                <td width="5%">
                <asp:Button ID="b_search" runat="server" Text="Go" Style="margin-right: 2px;
                        font-size: 16px;" Font-Bold="true" />
                </td>
                <td width="20%">
                    <asp:Button ID="b_print" runat="server" Text="พิมพ์ใบเสร็จ" Style="margin-right: 2px;
                        font-size: 16px;" Font-Bold="true" />
                </td>
                <td width="20%" align="right">
                    <asp:Button ID="b_withdraw" runat="server" Text="ถอนหุ้น" Style="margin-right: 2px;
                        font-size: 16px;" Font-Bold="true" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>