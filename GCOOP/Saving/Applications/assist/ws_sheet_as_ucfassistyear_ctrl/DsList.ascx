<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.assist.ws_sheet_as_ucfassistyear_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="10%">
             ปีสวัสดิการ
        </th>
        <th width="10%">
             วันเริ่มต้น
        </th>
        <th width="10%">
            วันสิ้นสุด
        </th>        
       <%-- <th width="5%">
            ลบ!
        </th>--%>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <%--<asp:DropDownList ID="ASS_YEAR" runat="server"></asp:DropDownList>--%>
                    <asp:TextBox ID="ASS_YEAR" runat="server" Style="text-align: center;" MaxLength="4" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="start_year" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="end_year" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                </td>                
              <%--  <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>--%>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
