<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_chgstatuschq_pay_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server"  Height="340px" ScrollBars="Auto" HorizontalAlign="Left">
    <table class="DataSourceRepeater">
        <tr>
            <div style="text-decoration: underline; text-align:left; font-size:15px; font-style:inherit; color:#191970" >
                    <span>รายการเช็ค :</span>
            </div>   
        </tr>
        <tr>
            <th colspan="2" width="4%">
                เลขที่เช็ค                
            </th>
            <th width="5%">
                วันที่หน้าเช็ค
            </th>
            <th width="10%">
                ธนาคาร
            </th>
            <th  width="11%">
                สั่งจ่าย
            </th>
            <th width="7%">
                สถานะเช็ค
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="text-align:center">
                     <asp:CheckBox ID="STATUS" runat="server"  style="text-align:center; width:10px" > </asp:CheckBox>
                     </td>
                     <td>
                     <asp:TextBox ID="CHEQUE_NO" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="DATE_ONCHQ" runat="server" ReadOnly="True" Style="text-align: center" ></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="BANK_DESC" runat="server" ReadOnly="True" Style="text-align: left"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="TO_WHOM" runat="server" ReadOnly="True" ></asp:TextBox>
                    </td>
                    <td >
                        <asp:DropDownList ID="CHQEUE_STATUS" runat="server"  style="text-align:center">
                            <asp:ListItem Value="1">ขึ้นเงิน</asp:ListItem>    
                            <asp:ListItem Value="0">ยังไม่ได้รับเช็ค</asp:ListItem><%--ค้างจ่าย--%>
                            <asp:ListItem Value="8">รับเช็คแต่ยังไม่ได้ขึ้นเงิน</asp:ListItem><%--ระหว่างทาง--%>
                            <%--<asp:ListItem Value="2">เช็คสำรองจ่าย</asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>