<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <span class="TitleSpan">ข้อมูลสมาชิก</span>
        <table class="DataSourceFormView" style="width: 770px;">
            <tr>
                <td>
                    <asp:Button ID="b_print" runat="server" Text="พิมพ์รายงานตรวจสอบสิทธิ์" />
                </td>
                <td colspan="3">
                    <asp:Button ID="b_pbreport" runat="server" Text="พิมพ์รายงานคุณสมบัติ" />
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="width: 100px;"></asp:TextBox>
                        <asp:Button ID="b_search" runat="server" Text="..." Style="width: 25px;" />
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ชื่อ-สกุล:</span>
                    </div>
                </td>
                <td width="56%">
                    <div>
                        <asp:TextBox ID="cp_name" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเภทสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="cp_membtype" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <%--<div>
                        <asp:DropDownList ID="sex" runat="server" Enabled="false">
                            <asp:ListItem Value="M">ชาย</asp:ListItem>
                            <asp:ListItem Value="F">หญิง</asp:ListItem>
                        </asp:DropDownList>
                    </div>--%>
                </td>
                <td>
                    <div>
                        <span>สังกัด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="cp_membgroup" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สามัญ/สมทบ:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="cp_member_type" runat="server" ReadOnly="true"></asp:TextBox>
                        <%--  <asp:DropDownList ID="member_type" runat="server" Enabled="false">
                            <asp:ListItem Value="1">สมาชิกปกติ</asp:ListItem>
                            <asp:ListItem Value="2">สมาชิกสมทบ</asp:ListItem>
                        </asp:DropDownList>--%>
                    </div>
                </td>
                <td>
                    <div>
                        <span>รหัสบริษัท:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="membgroup_control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="remark" runat="server" TextMode="MultiLine" ReadOnly="true" Style="width: 754px;
                        height: 50px;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
