<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_loan_receive_list_ctrl.DsMain1" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td>
                    <span>จำนวนรายการ:</span>
                </td>
                <td>
                    <asp:DropDownList ID="list_quantity" runat="server" BackColor="#FFFFCC">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="0" Selected=True>ทั้งหมด</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="40">40</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td width="12%">
                    <span>ผู้ทำรายการ:</span>
                </td>
                <td width="20%" align="left">
                    <asp:DropDownList ID="entry_id" runat="server" Style="margin-right: 20px" BackColor="#FFFFCC">
                    </asp:DropDownList>
                </td>
                <td width="12%">
                    <span>เลือกประเภท:</span>
                </td>
                <td width="22%">
                    <asp:DropDownList ID="group" runat="server" BackColor="#FFFFCC">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="0">แสดงข้อมูลทั้งหมด</asp:ListItem>
                        <asp:ListItem Value="01">เงินกู้ฉุกเฉิน</asp:ListItem>
                        <asp:ListItem Value="02">เงินกู้สามัญ</asp:ListItem>
                        <asp:ListItem Value="03">เงินกู้พิเศษ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="12%">
                    <span>ทะเบียน:</span>
                </td>
                <td width="12%">
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="10%" align="right">
                    <asp:Button ID="b_loan" runat="server" Text="จ่ายเงินกู้" Style="margin-right: 2px;
                        font-size: 16px;" Font-Bold="true" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
