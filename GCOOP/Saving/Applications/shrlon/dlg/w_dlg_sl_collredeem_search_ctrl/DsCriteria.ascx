<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCriteria.ascx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_collredeem_search_ctrl.DsCriteria" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 550px;">
            <tr>
                <td width="16%">
                    <div>
                        <span>ทะเบียนสมาชิก:</span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <span>ประเภทหลักทรัพย์:</span>
                    </div>
                </td>
                <td width="40%">
                    <div>
                        <asp:DropDownList ID="collmasttype_desc" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อ:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="memb_name" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>นามสกุล:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="memb_surname" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สังกัด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="membgroup_code" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                        <asp:DropDownList ID="membgroup_desc" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
