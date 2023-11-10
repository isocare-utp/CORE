<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_mbucfworkplace_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />

<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <tr>
            <th width="10%">
                รหัสประเภท
            </th>
            <th>
                ชื่อสถานที่ทำงาน
            </th>
          
            <th width="5%">
                ลบ
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="520px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="10%">
                        <asp:TextBox ID="DEPARTMENT_CODE" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="DEPARTMENT_DESC" runat="server"></asp:TextBox>
                    </td>
                  
                    <td width="5%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
    </EditItemTemplate>
</asp:FormView>
