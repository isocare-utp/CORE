<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCriteria.ascx.cs"
    Inherits="Saving.Applications.mbshr.dlg.w_dlg_mbshr_mem_search_ctrl.DsCriteria" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 560px;">
            <tr>
                <td width="12%">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>เลขพนักงาน:</span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="salary_id" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>บัตรประชาชน:</span>
                    </div>
                </td>
                <td width="22%">
                    <div>
                        <asp:TextBox ID="card_person" runat="server"></asp:TextBox>
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
                <td>
                    <div>
                        <span>เลขสัญญา:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="loancontract_no" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รหัสสังกัด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="membgroup_no" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                        <asp:DropDownList ID="membgroup_nodd" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ชื่อสังกัด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="membgroup_desc" runat="server"></asp:TextBox>
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
                        <asp:TextBox ID="membtype_code" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td colspan="4">
                    <div>
                        <asp:DropDownList ID="membtype_desc" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
