<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDecease.ascx.cs" Inherits="Saving.Applications.assist.ws_as_request_ctrl.DsDecease" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td style="width: 15%">
                    <span>วันที่ถึงแก่กรรม:</span>
                </td>
                <td style="width: 15%">
                    <asp:TextBox ID="dec_deaddate" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td style="width: 15%">
                    <span>เงื่อนไขการจ่าย:</span>
                </td>
                <td style="width: 55%">
                    <asp:DropDownList ID="assistpay_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <span>สาเหตุการเสียชีวิต:</span>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="dec_cause" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
