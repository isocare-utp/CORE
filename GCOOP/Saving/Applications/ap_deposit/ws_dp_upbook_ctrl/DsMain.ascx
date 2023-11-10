<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dp_upbook_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="10%">
                </td>
                <td width="10%">
                </td>
                <td width="10%">
                </td>
                <td width="10%">
                </td>
                <td width="10%">
                </td>
                <td width="10%">
                </td>
                <td width="10%">
                </td>
            </tr>
            <tr>
                <td>
                    <span>เลขที่บัญชี :</span>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="deptaccount_no" runat="server" style="text-align:center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span>ชื่อบัญชี :</span>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="deptaccount_name" runat="server" style="text-align:center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span>เลขที่สมุด :</span>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="deptpassbook_no" runat="server" style="text-align:center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span>ตั้งแต่รายการ :</span>
                </td>
                <td>
                    <asp:TextBox ID="lastrec_no_pb" runat="server" style="text-align:center"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="laststmseq_no" runat="server" style="text-align:center"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <span>หน้าที่ :</span>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="lastpage_no_pb" runat="server" style="text-align:center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span>บรรทัดที่ :</span>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="lastline_no_pb" runat="server" style="text-align:center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="b_print" runat="server" Text="พิมพ์รายการเคลื่อนไหวสมุดเงินฝาก" Width="400px" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
