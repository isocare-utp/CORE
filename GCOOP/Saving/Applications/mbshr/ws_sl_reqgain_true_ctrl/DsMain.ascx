<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_reqgain_true_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="12%">
                    <div>
                        <span>ทะเบียน :</span></div>
                </td>
                <td width="12%">
                    <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span>ชื่อ-สกุล :</span></div>
                </td>
                <td colspan="3" width="34%">
                    <asp:TextBox ID="c_name" runat="server" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span>อายุตอนเขียน :</span></div>
                </td>
                <td width="12%">
                    <asp:TextBox ID="c_age" runat="server" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขพนักงาน :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="salary_id" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>หน่วยงาน :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="membgroup_code" runat="server" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span>ชื่อหน่วยงาน :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="membgroup_desc" runat="server" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>วันเกษียณ :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="retry_date" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันที่เขียน :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="gaincond_date" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เขียนที่ :</span></div>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="write_at" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เงื่อนไข :</span></div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="gaincond_type" runat="server">
                        <asp:ListItem Value="0">ไม่ระบุ</asp:ListItem>
                        <asp:ListItem Value="1">ให้รับเต็มจำนวนแต่เพียงผู้เดียว</asp:ListItem>
                        <asp:ListItem Value="2">ให้รับส่วนแบ่งเท่าๆกัน</asp:ListItem>
                        <asp:ListItem Value="3">ให้รับตามลำดับ</asp:ListItem>
                        <asp:ListItem Value="99">อื่นๆ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="gaincond_desc" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="b_search" runat="server" Text="ค้นหา"  Width="50px"/>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
