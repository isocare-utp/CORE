<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_bankaccount_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="20%">
                    <span>ธนาคาร :</span>
                </td>
                <td width="30%">
                    <asp:DropDownList ID="BANK_CODE" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="20%">
                    <span>ประเภท :</span>
                </td>
                <td width="30%">
                    <asp:DropDownList ID="ACCOUNT_TYPE" runat="server">
                        <asp:ListItem Value="00">ออมทรัพย์</asp:ListItem>
                        <asp:ListItem Value="01">กระแสรายวัน</asp:ListItem>
                        <asp:ListItem Value="02">ประจำ</asp:ListItem>
                        <asp:ListItem Value="03">ออมทรัพย์พิเศษ</asp:ListItem>
                        <asp:ListItem Value="04">ตั๋วสัญญาใช้เงิน</asp:ListItem>
                        <asp:ListItem Value="05">OD</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <span>สาขา :</span>
                </td>
                <td>
                    <asp:DropDownList ID="BANKBRANCH_CODE" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <span>เลขที่สมุด :</span>
                </td>
                <td>
                    <asp:TextBox ID="BOOK_NO" runat="server" Style="text-align: center;" onkeypress="check_number();" onfocus="this.select()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span>เลขที่บัญชี :</span>
                </td>
                <td>
                    <asp:TextBox ID="ACCOUNT_NO" runat="server" Style="text-align: center;" onfocus="this.select()"></asp:TextBox>
                </td>
                <td>
                    <span>อัตราดอกเบี้ย(%) :</span> 
                </td>
                <td>
                    <asp:TextBox ID="INT_RATE" runat="server" Style="text-align: center;" ToolTip="#,##0.00" onkeypress="check_number();" onfocus="this.select()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span>ชื่อบัญชี :</span>
                </td>
                <td>
                    <asp:TextBox ID="ACCOUNT_NAME" runat="server" Style="text-align: left;" onfocus="this.select()"></asp:TextBox>
                </td>
                <td>
                    <span>รายการล่าสุด :</span>
                </td>
                <td>
                    <asp:TextBox ID="LASTSTM_SEQ" runat="server" Style="text-align: center;" onkeypress="check_number();" onfocus="this.select()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span>คงเหลือธนาคาร :</span>
                </td>
                <td>
                    <asp:TextBox ID="SCO_BALANCE" runat="server" Style="text-align: right;" ToolTip="#,##0.00" onkeypress="check_balance();" onfocus="this.select()"></asp:TextBox>
                </td>
                <td>
                    <span>วันที่รายการล่าสุด :</span>
                </td>
                <td>
                    <asp:TextBox ID="LASTACCESS_DATE" runat="server" Style="text-align: center;" ReadOnly="True"
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span>คงเหลือสหกรณ์ :</span>
                </td>
                <td>
                    <asp:TextBox ID="BALANCE" runat="server" Style="text-align: right;" ToolTip="#,##0.00" onkeypress="check_balance();" onfocus="this.select()"></asp:TextBox>
                </td>
                <td>
                    <span>สาขา :</span>
                </td>
                <td>
                    <asp:TextBox ID="COOP_ID" runat="server" Style="text-align: center;" ReadOnly="True"
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>           
            <tr>
                <td>
                    <span>คู่บัญชี :</span>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ACCOUNT_ID" runat="server" style="width:99%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="height: 60px;">หมายเหตุ :</span>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="REMARK" runat="server" TextMode="MultiLine" Style="width:99%;height: 60px;" ></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
