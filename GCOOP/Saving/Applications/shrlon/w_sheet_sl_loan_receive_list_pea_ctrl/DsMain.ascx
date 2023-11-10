<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_pea_ctrl.DsMain1" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="20%">
                    <span>รหัสพนักงาน :</span>
                </td>
                <td width="20%">
                    <asp:TextBox ID="salary_id" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                </td>
                <td width="20%">
                </td>
                <td width="20%">
                    <asp:Button ID="b_receipt_pay" runat="server" Text="พิมพ์ใบสำคัญจ่าย" />
                </td>
                <td width="20%">
                    <asp:Button ID="b_receipt" runat="server" Text="พิมพ์ใบเสร็จ" />
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <span>ทะเบียนสมาชิก :</span>
                </td>
                <td width="20%">
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                    <span>สถานะ :</span>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="status" runat="server" BackColor="#FFFFCC">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="0">แสดงข้อมูลทั้งหมด</asp:ListItem>
                        <asp:ListItem Value="1">อนุมัติมีเลข</asp:ListItem>
                        <asp:ListItem Value="2">อนุมัติไม่มีเลข</asp:ListItem>
                        <asp:ListItem Value="3">พิมพ์จ่ายแล้ว</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="20%">
                    <span style="visibility: <%=IsShow%>;">
                        <asp:CheckBox ID="PAYADVANCE_FLAG" runat="server" />
                        จ่ายล่วงหน้า : </span>
                </td>
                <td width="20%">
                    <asp:TextBox ID="SLIP_DATE" runat="server" Style="text-align: center;"></asp:TextBox>
                    <%--<asp:Button ID="b_print" runat="server" Text="พิมพ์จ่าย" Style="margin-right: 2px;
                        font-size: 16px;" Font-Bold="true" />--%>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <span>ผู้ทำรายการ :</span>
                </td>
                <td width="20%" align="left">
                    <asp:DropDownList ID="ENTRY_ID" runat="server" Style="margin-right: 20px" BackColor="#FFFFCC">
                    </asp:DropDownList>
                </td>
                <td width="20%">
                    <span>เลือกประเภท :</span>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="group" runat="server" BackColor="#FFFFCC">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="0">แสดงข้อมูลทั้งหมด</asp:ListItem>
                        <asp:ListItem Value="01">เงินกู้ฉุกเฉิน</asp:ListItem>
                        <asp:ListItem Value="02">เงินกู้สามัญ</asp:ListItem>
                        <asp:ListItem Value="03">เงินกู้พิเศษ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="20%">
                    <asp:Button ID="b_show" runat="server" Text="ดึงข้อมูล" Style="font-size: 15px; width: 55px;"
                        Font-Bold="true" BackColor="#FFCCCC" />
                    <!--<asp:Button ID="b_loop_save" runat="server" Text="postจ่าย" Style="margin-right: 2px;
                            font-size: 16px; width: 70px;" Font-Bold="true " />-->
                </td>
                <td width="20%" align="right">
                    <asp:Button ID="b_loan" runat="server" Text="จ่ายเงินกู้" Style="margin-right: 2px;
                        font-size: 16px;" Font-Bold="true" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
