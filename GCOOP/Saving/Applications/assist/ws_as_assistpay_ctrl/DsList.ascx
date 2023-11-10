<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.assist.ws_as_assistpay_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 697px">
    <tr>
        <th width="4%">
        </th>
        <th width="26%">
            ชื่อ - สกุล
        </th>
        <th width="32%">
            ประเภท
        </th>
        <th width="13%">
            วันที่อนุมัติ
        </th>
        <th width="13%">
            เงินสวัสดิการ
        </th>
     </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="450px" Width="728px" ScrollBars="Vertical" >
    <table class="DataSourceRepeater" style="width: 98%; ">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%" align="center">
                        <asp:CheckBox ID="choose_flag" runat="server" />
                    </td>
                    <td width="26%">
                        <asp:TextBox ID="cp_name" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="32%">
                        <asp:TextBox ID="assisttype_desc" runat="server" Style="text-align: left" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="approve_date" runat="server" ReadOnly="true" Style="text-align: center" ></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="approve_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>                   
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
