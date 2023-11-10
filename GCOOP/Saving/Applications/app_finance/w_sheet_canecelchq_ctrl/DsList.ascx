<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.w_sheet_canecelchq_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server"  Height="450px" ScrollBars="Auto" HorizontalAlign="Left">
    <table class="DataSourceRepeater">
        <tr>
                  <div style="text-decoration: underline; text-align:left; font-size:15px; font-style:inherit; color:#191970" >
                            <span>รายการเช็คในระบบ :</span>
                        </div>  
        </tr>
        <tr>
            <th colspan="2" width="5%">
                เลขที่เช็ค                
            </th>
            <th width="5%">
                วันที่หน้าเช็ค
            </th>
            <th width="10%">
                สั่งจ่าย
            </th>
            <th width="5%">
                จำนวนเงิน
            </th>
            <th width="4%">
                เหตุผล
            </th>
            <th width="7%">
                เลือกรายการ
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="text-align:center;>
                     <asp:CheckBox ID="AI_FLAG" runat="server"  style="text-align:center; width:10px" > </asp:CheckBox>
                     </td>
                     <td>
                     <asp:TextBox ID="CHEQUE_NO" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="DATE_ONCHQ" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="TO_WHOM" runat="server" ReadOnly="True" Style="text-align: left"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="MONEY_AMT" runat="server" ReadOnly="True" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td >
                        <asp:DropDownList ID="CANCEL_RESON" runat="server"></asp:DropDownList>
                    </td>
                    <td align="center">
                        <asp:DropDownList ID="ACTION_FLAG" runat="server">
                            <asp:ListItem Value="1">ยกเลิกทิ้ง</asp:ListItem>
                            <asp:ListItem Value="2">ยกเลิกออกเช็คใหม่</asp:ListItem>
                            </asp:DropDownList>
                    </td>
                    
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>