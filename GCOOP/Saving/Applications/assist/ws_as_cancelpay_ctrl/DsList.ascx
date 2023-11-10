<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.assist.ws_as_cancelpay_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>        
        <th width="4%">
        </th>
        <th width="11%">
            วันที่จ่าย
        </th>
        <th width="8%">
            ประเภท
        </th>
        <th width="11%">
            เลขที่จ่าย
        </th>                
        <th width="10%">
            เลขสมาชิก
        </th>
        <th width="26%">
            ชื่อ - สกุล
        </th>
        <th width="5%">
            งวด
        </th>
        <th width="15%">
            เงินสวัสดิการ
        </th>
     </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%" align="center">
                        <asp:CheckBox ID="choose_flag" runat="server" />
                    </td>
                    <td width="11%">
                        <asp:TextBox ID="slip_date" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="assisttype_code" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="11%">
                        <asp:TextBox ID="assistslip_no" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                    </td>                    
                    <td width="10%">
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="26%">
                        <asp:TextBox ID="mbname" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:TextBox ID="pay_period" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="payout_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>                   
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
