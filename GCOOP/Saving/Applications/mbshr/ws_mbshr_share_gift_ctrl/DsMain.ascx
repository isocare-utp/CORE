<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_share_gift_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td></td>
                <td width="25%"> 
                    <div> <span>ประเภทสมาชิก :</span></div>
                </td>
                <td width="25%">
                    <asp:DropDownList ID="member_type" runat="server">
                        <asp:ListItem Value="1" Selected="True">ปกติ</asp:ListItem>
                        <asp:ListItem Value="2">สมทบ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td></td>
                <td width="25%"> 
                    <div> <span>จำนวนสมาชิกรับรางวัลหุ้น :</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="count_memb" runat="server" Style="text-align: center" ToolTip="#,##0"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td></td>
                <td width="25%"> 
                    <div> <span>รางวัลหุ้น :</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="share_amount" runat="server" Style="text-align: center" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td></td>
                <td width="25%"> 
                    <div> <span>ยอดจ่ายรางวัลหุ้น :</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="share_amtnet" runat="server" Style="text-align: center" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
                <td colspan="4" align="center">
                    <asp:Button ID="bprocess" runat="server" Text="ประมวลรางวัลหุ้น" Width="200px" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>