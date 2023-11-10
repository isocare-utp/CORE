<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_est_moneyreturn_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="406px">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 770px;">
            <tr>
                <td width="16%">
                    <div>
                        <span>รายการ :</span>
                    </div>
                </td>
                <td width="28%">
                    <div>
                        <asp:CheckBox ID="kss_flag" runat="server" Text="กองทุนกสส" />
                    </div>
                </td>
                <td width="28%">
                    <div>
                    </div>
                </td>
                <td width="28%">
                    <div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันที่ทำรายการ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="operate_date" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                    </div>
                </td>
                <td>
                    <div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ช่วงข้อมูล :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="choosemem_flag" runat="server">
                            <asp:ListItem Value="0" Text="ทั้งหมด" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="รายคน"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                        <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:Button ID="b_ret" runat="server" Text="ดึงข้อมูล" />
                    </div>
                </td>
                <td>
                    <div>
                    </div>
                </td>
                <td>
                    <div>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
