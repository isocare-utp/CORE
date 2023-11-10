<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_chgstatuschq_recv_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Height="340px" ScrollBars="Auto" HorizontalAlign="Left">
     <div style="text-decoration: underline; text-align: left; font-size: 15px; font-style: inherit;
                color: #191970">
                <span>รายการเช็ค :</span>
            </div>
    <table class="DataSourceRepeater">      
        <tr>
            <th width="5%">
            </th>
            <th width="13%">
                วันที่รับเช็ค
            </th>
            <th width="25%">
                ธนาคาร
            </th>
            <th width="40%">
                รายละเอียด
            </th>
            <th width="17%">
                สถานะเช็ค
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="text-align: center">
                        <asp:CheckBox ID="CHK_STATUS" runat="server" Style="text-align: center; width: 10px">
                        </asp:CheckBox>
                    </td>                    
                    <td>
                        <asp:TextBox ID="CHECKDUE_DATE" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="BANK_DESC" runat="server" ReadOnly="True" Style="text-align: left"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="REFERDOC_NAME" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="CHECKCLEAR_STATUS" runat="server" Style="text-align: center">
                            <asp:ListItem Value="1">ขึ้นเงิน</asp:ListItem>
                            <asp:ListItem Value="0">รอทำรายการ</asp:ListItem>
                            <asp:ListItem Value="8">ส่งธนาคารแล้ว</asp:ListItem>
                            <asp:ListItem Value="-9">เช็คยกเลิกแล้ว</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
