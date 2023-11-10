<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_pay_moneyreturn_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" >
            <%--<tr>
                <td colspan="4" style="font-size: 16px;">
                    การค้นหาข้อมูล
                </td>
            </tr>--%>
            <tr>
                <td width="15%">
                    <span>ทะเบียน :</span>
                </td>
                <td width="35%">
                    <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                </td>
                <td width="15%">
                    <span>รหัสพนักงาน :</span>
                </td>
                <td width="35%">
                    <asp:TextBox ID="salary_id" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span>ตั้งแต่สังกัด :</span>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="start_membgroup" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <span>ถึงสังกัด :</span>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="end_membgroup" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <span>ธนาคาร :</span>
                </td>
                <td>
                    <asp:DropDownList ID="bank_code" runat="server">
                        <asp:ListItem Text="ทั้งหมด" Value="%"></asp:ListItem>
                        <asp:ListItem Text="กรุงไทย" Value="006"></asp:ListItem>
                        <asp:ListItem Text="CIMB" Value="022"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <span>รายการ :</span>
                </td>
                <td>
                    <asp:DropDownList ID="itemtype_code" runat="server">
                        <asp:ListItem Text="ทั้งหมด" Value="%"></asp:ListItem>
                        <asp:ListItem Text="กสส" Value="WRT"></asp:ListItem>
                        <asp:ListItem Text="เงินต้น/ดอกเบี้ย" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td colspan="3">
                </td>
                <td>
                    <asp:Button ID="b_retrieve" runat="server" Text="ดึงข้อมูล" Style="float: right;" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="font-size: 16px;">
                    การทำรายการ
                </td>
            </tr>
            <tr>
                <td>
                    <span>วันที่ใบเสร็จ :</span>
                </td>
                <td>
                    <asp:TextBox ID="slip_date" runat="server" style="text-align:center;"></asp:TextBox>
                </td>
                <td>
                    <span>วันที่ทำรายการ :</span>
                </td>
                <td>
                    <asp:TextBox ID="operate_date" runat="server" style="text-align:center;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
