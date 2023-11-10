<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.keeping.w_sheet_kp_adjust_kptp_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="12%">
                    <div>
                        <span>ทะเบียน :</span>
                    </div>
                </td>
                <td width="14%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td width="12%">
                    <div>
                        <span>ชื่อ-ชื่อสกุล :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="member_name" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td width="12%">
                    <div>
                        <span>ประเภท :</span>
                    </div>
                </td>
                <td width="17%">
                    <div>
                        <asp:TextBox ID="member_typet" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>เลขพนักงาน :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="salary_id" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>หน่วย :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="member_group" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>สถานะ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="member_statust" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
