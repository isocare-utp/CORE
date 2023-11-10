<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.deposit.w_dlg_dp_slip_recppaytype_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 430px;">
            <tr>
                <td width="30%">
                    <div>
                        <span>เลือกทำรายการ</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="deptwith_flag" runat="server">
                         <asp:ListItem Text="เลือกทำรายการ" Value="0"></asp:ListItem>
                                <asp:ListItem Text="+ ทำรายการฝาก" Value="1"></asp:ListItem>
                                <asp:ListItem Text="- ทำรายการถอน" Value="2"></asp:ListItem>
                                <asp:ListItem Text="/ ทำรายการปิดบัญชี" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
