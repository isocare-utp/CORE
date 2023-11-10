<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCriteria.ascx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_ln_loan_search_ctrl.DsCriteria" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
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
                <td width="10%">
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
                        <span>เลขสัญญา</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="loancontract_no" runat="server"></asp:TextBox>
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
                        <span>ชั้นลูกหนี้</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="lawtype_code" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
