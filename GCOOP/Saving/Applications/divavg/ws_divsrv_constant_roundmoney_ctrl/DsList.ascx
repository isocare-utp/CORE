<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_constant_roundmoney_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <tr>
            <th width="5%">
            </th>
            <th width="20%">
                ประเภท
            </th>
            <th width="22%">
                ประเภทการปัดสตางค์
            </th>
            <th width="15%">
                ทศนิยมที่ใช้คำนวน
            </th>
            <th width="17%">
                วิธีการปัด
            </th>
            <th width="10%">
                ปัดจนถึงตำแหน่ง
            </th>
            <th width="6%">
                เปิดใช้งาน
            </th>
            <th width="5%">
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="function_code" runat="server"></asp:TextBox>
                    </td>
                    <td width="22%">
                        <asp:DropDownList ID="satang_type" runat="server">
                            <asp:ListItem Value="00">ไม่ปัด</asp:ListItem>
                            <asp:ListItem Value="10">ปัดขึ้นเต็ม 5 สตางค์</asp:ListItem>
                            <asp:ListItem Value="11">ปัดขึ้นเต็ม 10 สตางค์</asp:ListItem>
                            <asp:ListItem Value="12">ปัดขึ้นเต็ม 25 สตางค์</asp:ListItem>
                            <asp:ListItem Value="13">ปัดขึ้นเต็ม 50 สตางค์</asp:ListItem>
                            <asp:ListItem Value="14">ปัดขึ้นเต็ม 1 บาท</asp:ListItem>
                            <asp:ListItem Value="48">ปัด5/4เต็ม 5 สตางค์</asp:ListItem>
                            <asp:ListItem Value="49">ปัด5/4เต็ม 1 บาท</asp:ListItem>
                            <asp:ListItem Value="50">ปัดลงเต็ม 5 สตางค์</asp:ListItem>
                            <asp:ListItem Value="51">ปัดลงเต็ม 10 สตางค์</asp:ListItem>
                            <asp:ListItem Value="52">ปัดลงเต็ม 25 สตางค์</asp:ListItem>
                            <asp:ListItem Value="53">ปัดลงเต็ม 50 สตางค์</asp:ListItem>
                            <asp:ListItem Value="54">ปัดลงเต็ม 1 บาท</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="truncate_pos_amt" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="17%">
                        <asp:DropDownList ID="round_type" runat="server">
                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                            <asp:ListItem Value="1">ปัดแต่ละขั้น</asp:ListItem>
                            <asp:ListItem Value="2">รวมแล้วปัด</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="round_pos_amt" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:CheckBox ID="use_flag" runat="server" />
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_delete" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
