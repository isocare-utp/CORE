<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_ln_nplsue_moneyadv_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td colspan="4">
                    <strong style="font-size: 14px;">รายละเอียดสมาชิก</strong>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="member_no" runat="server" Style="width: 95px; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_memsearch" runat="server" Text="..." Style="width: 25px;" />
                </td>
                <td width="15%">
                    <div>
                        <span>ชื่อ-สกุล:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_name" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>ประเภทสมาชิก:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="membtype_desc" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span>สังกัด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="membgroup_desc" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
