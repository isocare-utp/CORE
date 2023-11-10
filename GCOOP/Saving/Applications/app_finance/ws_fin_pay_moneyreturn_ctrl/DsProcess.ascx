<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsProcess.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_pay_moneyreturn_ctrl.DsProcess" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <%--<tr>
                <td colspan="4" style="font-size: 16px;">
                    การทำรายการ
                </td>
            </tr>--%>
            <tr>
                <td width="15%">
                    <span>วันที่ใบเสร็จ :</span>
                </td>
                <td width="35%">
                    <asp:TextBox ID="slip_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="15%">
                    <span>งวดส่งหัก :</span>
                </td>
                <td width="35%">
                    <asp:TextBox ID="RECV_PERIOD" runat="server"></asp:TextBox>
                </td>
            </tr>
            </tr>
            <tr>
                <td>
                    <span>ตั้งแต่วันที่ :</span>
                </td>
                <td>
                    <asp:TextBox ID="MRCREATE_SDATE" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <span>ถึงวันที่ :</span>
                </td>
                <td>
                    <asp:TextBox ID="MRCREATE_EDATE" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span>วันที่ทำรายการ :</span>
                </td>
                <td>
                    <asp:TextBox ID="operate_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="b_process" runat="server" Text="ทำรายการ" Style="float: right;" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
