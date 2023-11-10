<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_closedayprocess_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Height="450px" ScrollBars="Auto" HorizontalAlign="Left">
    <span style="text-decoration: underline; text-align: left; font-size: 15px; font-style: inherit;
        color: #191970">รายละเอียดรายการ :</span>
    <table class="DataSourceRepeater">
        <tr>
             <th width="2%">
                ลำดับ
            </th>
            <th width="5%">
               วันที่ครบกำหนด
            </th>
            <th width="5%">
                เลขบัญชี
            </th>
            <th width="12%">
                ชื่อบัญชี
            </th>
            <th width="6%">
                ต้นเงิน
            </th>
            <th width="6%" Style="display:none">
                วันที่ปิดสิ้นวัน
            </th>           
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>                   
                    <td>
                        <asp:TextBox ID="running_number" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="prncdue_date" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="deptaccount_no" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                     <td>
                        <asp:TextBox ID="deptaccount_name" runat="server" ReadOnly="True" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="prnc_bal" runat="server" ReadOnly="True" Style="text-align: right"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td Style="display:none">
                        <asp:TextBox ID="select_date" runat="server" ReadOnly="True" Style="text-align: center;" BackColor="SkyBlue"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
