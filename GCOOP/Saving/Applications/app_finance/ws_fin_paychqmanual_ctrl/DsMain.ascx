<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_paychqmanual_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <div style="text-decoration: underline; text-align: left; font-size: 15px; font-style: inherit;
            color: #191970">
            <span>ออกเช็ค<span>
        </div>        
        <table class="DataSourceFormView">
            <tr>
                <td width="20%">
                    <div>
                        <span>ธนาคาร :<span>
                    </div>
                </td>
                <td width="30%">
                    <asp:DropDownList ID="bank_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="20%">
                    <div>
                        <span>เล่มที่เช็ค :<span>
                    </div>
                </td>
                <td width="30%">
                    <asp:DropDownList ID="cheque_bookno" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สาขาธนาคาร :<span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="bank_branch" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>เลขที่เช็ค :<span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="account_no" runat="server" Font-Bold="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันที่หน้าเช็ค :<span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cheque_date" runat="server" Style="text-align: center" Font-Bold="True"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>รหัสบัญชี CR :<span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="as_tofromaccid" runat="server">
                    </asp:DropDownList>
                </td>                
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สั่งจ่าย :<span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="pay_whom" runat="server" Style="text-align: left; width: 99%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หมายเหตุ :<span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="remark" runat="server" Style="text-align: left;width: 99%"></asp:TextBox>
                </td>
            </tr>
            <tr>                                            
                <td>
                    <div>
                        <span>ประเภทเช็ค :<span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="cheque_type" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>สถานะเช็ค :<span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="cheque_status" runat="server">
                        <asp:ListItem Value="0">ยังไม่ได้รับเช็ค</asp:ListItem>
                        <%--ค้างจ่าย--%>
                        <asp:ListItem Value="8">รับเช็คแต่ยังไม่ได้ขึ้นเงิน</asp:ListItem>
                        <%--ระหว่างทาง--%>
                        <%--<asp:ListItem Value="2">เช็คล่วงหน้า</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="display: none;">
                <td>
                    <div>
                        <span>เครื่องพิมพ์ :<span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="print_type" runat="server">
                        <asp:ListItem Value="LAS">Laser</asp:ListItem>
                        <asp:ListItem Value="DOT">Dotmetrix</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>ขนาดเช็ค :<span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="chq_size" runat="server">
                        <asp:ListItem Value="0">เล็ก</asp:ListItem>
                        <asp:ListItem Value="1">ใหญ่</asp:ListItem>
                        <asp:ListItem Value="2">อื่นๆ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div style="text-decoration: underline; text-align: left; font-size: 15px; color: #191970">
            <span>ตัดจาก<span>
        </div>
        <table class="DataSourceFormView">                
            <tr>
                <td width="20%">
                    <div>
                        <span>ธนาคาร :<span>
                    </div>
                </td>
                <td width="30%">
                    <asp:DropDownList ID="frombank" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="20%">
                    <div>
                        <span>วันที่เช็ค :<span>
                    </div>
                </td>
                <td width="30%">
                    <asp:DropDownList ID="ai_prndate" runat="server">
                        <asp:ListItem Value="1" Selected="True">พิมพ์</asp:ListItem>
                        <asp:ListItem Value="0">ไม่พิมพ์</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สาขา :<span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="frombranch" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>ผู้ถือ :<span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="ai_killer" runat="server">
                        <asp:ListItem Value="1" Selected="True">ขีดฆ่า</asp:ListItem>
                        <asp:ListItem Value="0">ไม่ขีดฆ่า</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่บัญชี :<span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="fromaccount_no" runat="server" Style="text-align: center;width:90%"
                        Font-Bold="True"></asp:TextBox>
                    <asp:Button ID="b_1" runat="server" Text="..." Style="height: 30px" />
                </td>
                <td>
                    <div>
                        <span>A/C PAYEE :<span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="ai_payee" runat="server">
                        <asp:ListItem Value="1">พิมพ์</asp:ListItem>
                        <asp:ListItem Value="0" Selected="True">ไม่พิมพ์</asp:ListItem>
                    </asp:DropDownList>
                </td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="cheque_amt" runat="server" Style="background-color: Black; color: #00FF00;
                        width: 99%; font-size: 30px; height: 40px; text-align: right" ToolTip=".00"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
