<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_approve_reprint_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="9%">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="24%">
                    <asp:TextBox ID="member_no" runat="server" Width="166px"></asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span>วันที่อนุมัติ</span>
                    </div>
                </td>
                <td width="13%">
                    <asp:TextBox ID="apv_date_s" runat="server" Width="90px" ></asp:TextBox>
                </td>
                <td width="1%">
                    -
                </td>
                <td width="13%">
                    <asp:TextBox ID="apv_date_e" runat="server" Width="90px"> </asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span>ผู้อนุมัติ:</span>
                    </div>
                </td>
                <td width="18%">
                    <asp:TextBox ID="approve_id" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เงินกู้:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="loantype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>ช่วงสัญญา:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="contno_s" runat="server" Width="90px"></asp:TextBox>
                </td>
                <td>
                    -
                </td>
                <td>
                    <asp:TextBox ID="contno_e" runat="server" Width="90px"></asp:TextBox>
                </td>
                <td align="right" colspan="2">
                    <asp:Button ID="b_retrieve" runat="server" Text="ดึงข้อมูล" Width="55px" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
