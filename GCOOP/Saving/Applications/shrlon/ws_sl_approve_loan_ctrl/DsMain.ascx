<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_approve_loan_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <asp:Button ID="b_cont" runat="server" Text="พิมพ์สัญญาเงินกู้" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="b_coll" runat="server" Text="พิมพ์สัญญาคนค้ำ" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="b_int" runat="server" Text="พิมพ์ประกัน" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="b_spc" runat="server" Text="พิมพ์สัญญาเงินกู้พิเศษ" />
        &nbsp;&nbsp;&nbsp;
        <table class="DataSourceFormView">
            <tr>
                <td width="8%" valign="top">
                    <div>
                        <span>ผู้บันทึก:</span>
                    </div>
                </td>
                <td width="15%" valign="top">
                    <asp:TextBox ID="entry_id" runat="server"></asp:TextBox>
                </td>
                <td width="9%" valign="top">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="14%" valign="top">
                    <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                </td>
                <td width="15%" valign="top">
                    <div>
                        <span>ประเภทสัญญา:</span>
                    </div>
                </td>
                <td width="4%" valign="top">
                    <asp:TextBox ID="loantype_code_txt" runat="server"></asp:TextBox>
                </td>
                <td width="25%" valign="top">
                    <asp:DropDownList ID="loantype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="10%" valign="top">
                    <asp:Button ID="b_search" runat="server" Text="ดึงข้อมูล" Width="60px" />
                </td>
            </tr>
            <tr>
                <td>
                    <span>การเรียง:</span>
                </td>
                <td colspan="3">
                    <div>
                        <asp:DropDownList ID="order_type" runat="server">
                            <asp:ListItem Value="1">ประเภทเงินกู้-เลขคำขอ</asp:ListItem>
                            <asp:ListItem Value="2">กลุ่ม-ทะเบียนสมาชิก</asp:ListItem>
                            <asp:ListItem Value="3">เลขคำขอ</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td colspan="3">
                </td>
                <td>
                    <asp:Button ID="b_clear" runat="server" Text="ล้างข้อมูล" Width="60px" />
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <hr />
                    <br />
                </td>
            </tr>
        </table>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%" colspan="2">
                    <asp:CheckBox ID="select_check" runat="server" Text=" เลือกทั้งหมด" />
                </td>
                <td width="8%">
                </td>
                <td width="33%">
                </td>
                <td width="15%">
                </td>
                <td width="17%">
                    <asp:DropDownList ID="appv_status" runat="server">
                        <asp:ListItem Value="8" Text="รออนุมัติ"></asp:ListItem>
                        <asp:ListItem Value="1" Text="อนุมัติ"></asp:ListItem>
                        <asp:ListItem Value="11" Text="อนุมัติ (ไม่สร้างเลข)"></asp:ListItem>
                        <asp:ListItem Value="0" Text="ไม่อนุมัติ"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="13%">
                    <asp:Button ID="b_gencontno" runat="server" Text="สร้างเลขสัญญา" Width="85px" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
