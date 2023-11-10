<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_dptran_div_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 300px;">
            <tr>
                <td width="45%">
                    <p>
                        ปีปันผล</p>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ปีปันผล/เฉลี่ยคืน :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="tran_year" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขสมาชิก :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่บัญชี :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="deptaccount_no" runat="server" Style="text-align: center" ></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="45%">
                    <p>
                        รายการปันผล/เฉลี่ยคืน</p>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ยอดรวม :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="deptitem_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สถานะ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="tran_text" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
