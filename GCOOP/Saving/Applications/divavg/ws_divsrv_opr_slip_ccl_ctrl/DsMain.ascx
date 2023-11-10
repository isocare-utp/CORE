<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_opr_slip_ccl_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>ปีปันผล:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="div_year" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%" colspan="2">
                    <asp:CheckBox ID="member_status" runat="server" />&nbsp;ปิดบัญชีสมาชิก
                </td>
                <td width="15%">
                    <div>
                        <span>วันที่ทำรายการ:</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="operate_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่สมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Style="width: 110px; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Style="width: 25px;" />
                </td>
                <td width="2%">
                    <asp:CheckBox ID="resign_status" runat="server" />
                </td>
                <td width="18%" id="text_resign_status">
                    ลาออก
                </td>
                <td id="text_free">
                </td>
                <td>
                    <div>
                        <span>วันที่เป็นสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
                <td>
                    <div id="text_resign_date">
                        <span>วันที่ลาออก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="resign_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อสมาชิก</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="cp_name" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ชื่อสมาชิก(eng):</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_name_eng" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สังกัด:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="cp_membgroup" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ประเภทสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_membtype" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
