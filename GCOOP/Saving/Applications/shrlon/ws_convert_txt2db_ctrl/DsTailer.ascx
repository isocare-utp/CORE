<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsTailer.ascx.cs" Inherits="Saving.Applications.shrlon.ws_convert_txt2db_ctrl.DsTailer" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width='20%'>
                    <div>
                        <span>จำนวนรายการ : </span>
                    </div>
                </td>
                <td width='40%'>
                    <div>
                        <asp:TextBox ID="sum_row" runat="server" Style="width: 50%; text-align: right;" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td width='15%' rowspan="2">
                    <div>
                        <asp:Button ID="b_notpost" class="b_notpost" runat="server" Text="ดึงข้อมูลที่ไม่ผ่าน" />
                    </div>
                </td>
                <td width='15%' rowspan="2">
                    <div class="b_post_H">
                        <asp:Button ID="b_post" class="b_post" runat="server" Text="ผ่านรายการ" />
                    </div>
                    <div class="b_postagin">
                        <asp:Button ID="b_postagin" runat="server" Text="ผ่านรายการ(แก้ไข)" />
                    </div>
                </td>
                <td class="td_hiddenF">
                    <asp:TextBox ID="ls_tofromaccid" runat="server" ReadOnly="true" Text=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จำนวนเงิน : </span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="sum_amt" runat="server" Style="width: 50%; text-align: right;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
