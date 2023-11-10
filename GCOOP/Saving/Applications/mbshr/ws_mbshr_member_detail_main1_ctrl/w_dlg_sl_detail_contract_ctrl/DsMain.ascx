<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.w_dlg_sl_detail_contract_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <span class="TitleSpan">รายละเอียดสัญญา</span>
        <table class="FormStyle" style="width: 860px;">
            <tr>
                <td width="15%">
                    <div>
                        <span>เลขที่สัญญา:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span>ประเภทเงินกู้:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="loantype_desc" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ชื่อ-ชื่อสกุล:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_name" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
