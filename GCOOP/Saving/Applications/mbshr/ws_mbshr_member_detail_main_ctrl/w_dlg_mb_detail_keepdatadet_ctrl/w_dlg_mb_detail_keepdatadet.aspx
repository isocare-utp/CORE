<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_mb_detail_keepdatadet.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.w_dlg_mb_detail_keepdatadet_ctrl.w_dlg_mb_detail_keepdatadet" %>

<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Panel ID="Panel1" runat="server">
        <center>
            <table>
                <tr>
                    <th bgcolor="#B8D0D3" style="border-color: #729ea5; border-width: 1px; border-style: solid;">
                        <asp:Label ID="lbl_title" runat="server" Text="Label"></asp:Label>
                    </th>
                </tr>
                <tr>
                    <td align="center">
                        <uc1:DsDetail ID="dsDetail" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="TbStyle">
                            <tr>
                                <td colspan="3" style="border-style: none; text-align: left">
                                    <strong>รวมหักชำระ</strong>
                                </td>
                                <td width="17%" style="border-bottom-style: solid; border-bottom-width: medium; border-bottom-color: #000000;">
                                    <asp:TextBox ID="txt_sum_prin" runat="server" Font-Bold="True" Style="text-align: right"></asp:TextBox>
                                </td>
                                <td width="17%" style="border-bottom-style: solid; border-bottom-width: medium; border-bottom-color: #000000;">
                                    <asp:TextBox ID="txt_sum_int" runat="server" Font-Bold="True" Style="text-align: right"></asp:TextBox>
                                </td>
                                <td width="17%" style="border-bottom-style: solid; border-bottom-width: medium; border-bottom-color: #000000;">
                                    <asp:TextBox ID="txt_sum_item" runat="server" Font-Bold="True" Style="text-align: right"></asp:TextBox>
                                </td>
                                <td width="17%" style="border-style: none;">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </center>
    </asp:Panel>
</asp:Content>
