<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_edit_salary_ctrl.DsMain" %>
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
                    <asp:TextBox ID="fullname" runat="server" BackColor="#DDDDDD" ReadOnly="true"></asp:TextBox>
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
                    <strong><u>ข้อมูลเงินเดือนเก่า</u></strong>
                </td>
                <td colspan="2">
                    <strong><u>ข้อมูลเงินเดือนใหม่</u></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เงินเดือน :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="salary_amount" runat="server" Style="text-align: right" BackColor="#DDDDDD" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เงินเดือน :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="new_salary" runat="server" Width="170px" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ค่าหุ้นฐาน :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="periodbase_value" runat="server" Style="text-align: right" BackColor="#DDDDDD" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ค่าหุ้นฐาน :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="new_periodbase_value" runat="server" Width="170px" Style="text-align: right" BackColor="#DDDDDD" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ค่าหุ้นต่อเดือน :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="periodshare_value" runat="server" Style="text-align: right" BackColor="#DDDDDD" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ค่าหุ้นต่อเดือน :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="new_periodshare_value" runat="server" Width="170px" Style="text-align: right" BackColor="#DDDDDD" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                
            </tr>
            <tr><td>
                 <div>
                       </div>
                </td>
                <td>
                  
                </td>
            <td>
                 <div>
                        <span>เงินวิทยฐานะ :</span></div>
                </td> 
                <td>
                    <asp:TextBox ID="incomeetc_amt" runat="server" Width="170px" Style="text-align: right"  ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
