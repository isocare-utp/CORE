<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCriteria.ascx.cs"
    Inherits="Saving.Applications._global.w_dlg_ln_collmaster_search_ctrl.DsCriteria" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 630px;">
            <tr>
                <td width="12%">
                    <div>
                        <span>เลขสมาชิก</span>
                    </div>
                </td>
                <td width="12%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="7%">
                    <div>
                        <span>ชื่อ</span>
                    </div>
                </td>
                <td width="31%">
                    <div>
                        <asp:TextBox ID="memb_name" runat="server" Style="width: 98%"></asp:TextBox>
                    </div>
                </td>
                <td width="8%">
                    <div>
                        <span>สกุล</span>
                    </div>
                </td>
                <td width="30%">
                    <div>
                        <asp:TextBox ID="memb_surname" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div>
                        <span style="width: 104px;">ทะเบียนหลักทรัพย์</span>
                        <asp:TextBox ID="collmast_no" runat="server" Style="margin-left: 2px; width: 83px;"></asp:TextBox>
                        <span style="margin-left: 0px; width: 90px;">เลขที่หลักทรัพย์</span>
                        <asp:TextBox ID="collmast_refno" runat="server" Style="margin-left: 2px; width: 90px;"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ประเภท</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="collmasttype_code" runat="server" Style="width: 65px;">
                        </asp:DropDownList>
                        <span style="margin-left: 2px; width: 35px;">สาขา</span>
                        <asp:DropDownList ID="coop_id" runat="server" Enabled="false" Style="background-color: #E7E7E7;
                            margin-left: 2px; width: 70px;">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
