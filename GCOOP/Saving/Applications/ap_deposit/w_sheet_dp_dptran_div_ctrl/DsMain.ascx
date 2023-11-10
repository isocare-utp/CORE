<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_dptran_div_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="20%">
                    <p>
                        ปีปันผล</p>
                </td>
                <td>
                </td>
                <td width="3%">
                </td>
                <td>
                    <p>
                        ค้นหาข้อมูล</p>
                </td>
                <td>
                </td>
                <td width="10%">
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>ปีปันผล/เฉลี่ยคืน :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="tran_year" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td width="3%">
                </td>
                <td width="20%">
                    <div>
                        <span>เลขที่สมาชิก :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td width="10%">
                    <div>
                        <asp:Button ID="btn_memsreach" runat="server" Text="ค้นหา" Style="width: 50px; height: 25px" />
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
