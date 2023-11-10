<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCodept.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_deptdetail_ctrl.DsCodept" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Height="300px" HorizontalAlign="Left"
    ScrollBars="auto">
<table class="TbStyle" >
    <tr>
        <th rowspan="2" width="20%">
            ลำดับ
        </th>
        <th rowspan="2" width="30%">
            เลขที่อ้าง
        </th>
        <th rowspan="2" width="50%">
            ชื่อ - สกุล
        </th>
               
    </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="width: 20%;">
                        <asp:TextBox ID="SEQ_NO" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="width:30%;">
                        <asp:TextBox ID="REF_NO" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="width:50%;">
                        <asp:TextBox ID="cp_coname" runat="server" Style="text-align: left;" ReadOnly="True"></asp:TextBox>
                    </td >
                </tr>
                
            </ItemTemplate>      
        </asp:Repeater>
    </table>
</asp:Panel>

