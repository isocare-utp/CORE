<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCriteria.ascx.cs"
    Inherits="Saving.Applications._global.w_dlg_ln_loanrequest_search_ctrl.DsCriteria" %>
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
                <td width="8%">
                    <div>
                        <span>ชื่อ</span>
                    </div>
                </td>
                <td width="30%">
                    <div>
                        <asp:TextBox ID="memb_name" runat="server"></asp:TextBox>
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
                <td>
                    <div>
                        <span>เลขคำขอกู้</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="loanrequest_docno" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ประเภท</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="loantype_code" runat="server" Style="width: 65px;">
                        </asp:DropDownList>
                        <span style="width: 35px;">สาขา</span>
                        <asp:DropDownList ID="coop_id" runat="server" Enabled="false" Style="background-color: #E7E7E7;
                            margin-left: 2px; width: 70px;">
                        </asp:DropDownList>
                    </div>
                </td>
                <td>
                    <div>
                        <span>วันขอกู้</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="loanrequest_date" runat="server" ReadOnly="true" Style="width: 70px;
                            text-align: center;"></asp:TextBox>
                        <span style="margin-left: 2px; width: 45px;">สถานะ</span>
                        <asp:DropDownList ID="loanrequest_status" runat="server" Style="width: 57px;">
                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                            <asp:ListItem Value="8" Text="รอ"></asp:ListItem>
                            <asp:ListItem Value="1" Text="อนุมัติ"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="ยกเลิก"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
