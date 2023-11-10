<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsBonus_instead.ascx.cs" Inherits="Saving.Applications.assist.ws_as_request_ctrl.DsBonus_instead" %>

<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server">
    <table class="DataSourceRepeater" style="width: 100%">
                  <div align="left" style=" width:100%;">
    <span >รับแทนสมาชิก</span>
    <span class="NewRowLink" onclick="PostInsertRowss()" style="margin-left: 90%;">เพิ่มแถว</span></div>
        <tr>
            <th width="15%">
                ทะเบียน
            </th>
            <th width="3%">
            </th>
            <th width="35%">
                ชื่อ-สกุล
            </th>
            <th width="29%">
                ประเภทของขวัญ
            </th>
            <th width="15%">
                หน่วยนับ
            </th>
             <th width="3%">
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="6%">
                        <asp:TextBox ID="member_no_ref" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="3%">
                        <center>
                            <asp:Button ID="b_search" runat="server" Font-Size="12" Width="45px" Height="23px" Text="..." /></center>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="membname_ref" runat="server" Style="text-align: left;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="29%">
                       <asp:DropDownList ID="bonus_type" runat="server">
                    </asp:DropDownList>
                    <td width="15%">
                       <asp:DropDownList ID="bonus_unit" runat="server" Enabled="False">
                    </asp:DropDownList>
                    </td>
                    <td width="3%">
                        <center>
                            <asp:Button ID="b_del" runat="server" Font-Size="10" Width="45px" Height="23px" Text="ลบ" /></center>
                    </td>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
