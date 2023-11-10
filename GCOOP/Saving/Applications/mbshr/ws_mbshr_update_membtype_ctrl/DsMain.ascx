<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_update_membtype_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>ทะเบียน :</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="member_no" runat="server" Width="150px" Style="text-align: center"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Width="20px" />
                </td>
                <td width="15%">
                    <div>
                        <span>ชื่อ - นามสกุล :</span></div>
                </td>
                <td width="45%">
                    <asp:TextBox ID="c_memb_name" runat="server" BackColor="#DDDDDD" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สังกัด :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="membgroup_desc" runat="server" BackColor="#DDDDDD" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong><u>ข้อมูลประเภทสมาชิกเก่า</u></strong>
                </td>
                <td colspan="2">
                    <strong><u>ข้อมูลประเภทสมาชิกใหม่</u></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเภทสมาชิก :</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="membtype_code" runat="server" Style="text-align: center" BackColor="#DDDDDD" ReadOnly="true" Disabled="true"></asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>ประเภทสมาชิก :</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="new_membtype_code" runat="server" Width="170px" Style="text-align: center" ></asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
