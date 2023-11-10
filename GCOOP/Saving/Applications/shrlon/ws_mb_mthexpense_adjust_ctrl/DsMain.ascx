<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_mb_mthexpense_adjust_ctrl.DsMain" %>
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
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="width: 75px;"></asp:TextBox>
                        <asp:Button ID="b_memsearch" runat="server" Text="..." Style="width: 23px; margin-left: 1px;" />
                    </div>
                </td>
                <td width="12%">
                    <div>
                        <span>ชื่อสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="cp_name" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ปกติ/สมทบ:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <%-- <asp:TextBox ID="cp_membtype" runat="server"></asp:TextBox>--%>
                        <asp:DropDownList ID="member_type" runat="server">
                            <asp:ListItem Value="1">สมาชิกปกติ</asp:ListItem>
                            <asp:ListItem Value="2">สมาชิกสมทบ</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เพศ:</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="cp_sex" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>สังกัด:</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="cp_membgroup" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ประเภทสมาชิก:</span></div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="membtype_code" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
        <br />
    </EditItemTemplate>
</asp:FormView>
