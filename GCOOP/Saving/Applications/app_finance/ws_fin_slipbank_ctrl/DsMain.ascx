<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_slipbank_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Style="width:100%;">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>เลขที่ใบรายการ :</span>
                    </div>
                </td>
                <td width="15%">
                    <asp:TextBox ID="slip_no" runat="server" Style="text-align: center" Font-Bold="True" ForeColor="Black"
                        Enabled="False"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>รหัสรายการ :</span>
                    </div>
                </td>
                <td width="15%">
                    <asp:DropDownList ID="item_code" runat="server">
                        <asp:ListItem Value="DCA">ฝากเงิน</asp:ListItem>
                        <asp:ListItem Value="WCA">ถอนเงิน</asp:ListItem>
                        <asp:ListItem Value="OCA">เปิด บ/ช</asp:ListItem>
                        <asp:ListItem Value="CCA">ปิด บ/ช</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="15%">
                    <div>
                        <span>เลขที่สมุด :</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="book_no" runat="server" Style="text-align: center" Font-Bold="True"  Enabled="False" ForeColor="Black"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันทำการ :<//span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="operate_date" runat="server" Style="text-align: center; width: 99%"  Enabled="False" ForeColor="Black"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>คู่บัญชี :<//span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="account_id" runat="server" Style="text-align: center; width: 99%"  Enabled="False" ForeColor="Black"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ประเภทบ/ช :<//span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="account_type" runat="server"  Enabled="False" ForeColor="Black">
                        <asp:ListItem Value="00">ออมทรัพย์</asp:ListItem>
                        <asp:ListItem Value="01">กระแส</asp:ListItem>
                        <asp:ListItem Value="02">ประจำ</asp:ListItem>
                        <asp:ListItem Value="03">ออมทรัพย์พิเศษ</asp:ListItem>
                        <asp:ListItem Value="04">ตั๋วสัญญาใช้เงิน</asp:ListItem>
                        <asp:ListItem Value="05">OD</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>
                    <div>
                        <span>รายละเอียดรายการ :<//span>
                    </div>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="ITEM_DESC" runat="server" Style="width: 99%" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ธนาคาร :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="bank_code" runat="server" Style="text-align: center; width: 99%"  Enabled="False" ForeColor="Black">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>สาขา :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="bank_branch" runat="server" Style="text-align: center; width: 99%"  Enabled="False" ForeColor="Black">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่บัญชี :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="account_no" runat="server" Style="text-align: center; width: 98%" BackColor="Yellow" ForeColor="Red" TabIndex="1" onfocus="this.select()" Font-Bold="true" ></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>วันที่เปิดบัญชี :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="open_date" runat="server" Style="text-align: center; width: 98%"  Enabled="False" ForeColor="Black"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อบัญชี :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="account_name" runat="server" Style="text-align: center; width: 98%"  Enabled="False" ForeColor="Black"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>รายการล่าสุด :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="laststm_seq" runat="server" Style="text-align: center; width: 98%"  Enabled="False" ForeColor="Black"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ยอดคงเหลือ :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="balance" runat="server" Style="text-align: right; width: 98%" Font-Bold="True" ForeColor="Black"
                         Enabled="False" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>จำนวนเงิน :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="item_amt" runat="server" Style="text-align: right; width: 98%" Font-Bold="True"
                        ToolTip="#,##0.00" TabIndex="2" onfocus="this.select()" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สาขา :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="coop_id" runat="server" Style="text-align: center; width: 98%" Enabled="False" ForeColor="Black"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>วันที่ทำรายการ :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="entry_date" runat="server" Style="text-align: center; width: 98%" ForeColor="Black"
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รหัสเครื่อง :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="machine_id" runat="server" Style="text-align: center; width: 98%" ForeColor="Black"
                        Enabled="False"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ผู้ทำรายการ :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="entry_id" runat="server" Style="text-align: center; width: 98%" ForeColor="Black"
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
