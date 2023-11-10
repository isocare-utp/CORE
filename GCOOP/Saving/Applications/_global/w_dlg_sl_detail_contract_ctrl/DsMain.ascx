<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications._global.w_dlg_sl_detail_contract_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 700px;">
            <tr>
                <td width="13%">
                    <div>
                        <span>เลขที่สัญญา:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="loancontract_no" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ประเภทเงินกู้:</span>
                    </div>
                </td>
                <td >
                    <div>
                        <asp:TextBox ID="COMPUTE_1" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ชื่อ-ชื่อสกุล:</span>
                    </div>
                </td>
                <td >
                    <div>
                        <asp:TextBox ID="compute_2" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
        <br />
    </EditItemTemplate>
</asp:FormView>
