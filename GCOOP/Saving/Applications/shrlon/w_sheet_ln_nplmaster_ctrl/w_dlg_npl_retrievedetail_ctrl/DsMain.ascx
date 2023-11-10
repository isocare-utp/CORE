<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.w_dlg_npl_retrievedetail_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 300px;">
            <tr>
                <td width="35%">
                    <div>
                        <span>วันที่ลงรับเรื่อง</span>
                    </div>
                </td>
                <td width="45%">
                    <div>
                        <asp:TextBox ID="receive_date" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td width="10%">
                    <div>
                        <asp:CheckBox ID="receive_date_flag" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เงินต้นฟ้อง</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="indict_prnamt" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:CheckBox ID="indict_prnamt_flag" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชั้นลูกหนี้</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="debtor_class" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:CheckBox ID="debtor_class_flag" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สถานะ</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="status" runat="server">
                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                            <asp:ListItem Value="1" Text="1 - ผู้กู้ชำระหนี้"></asp:ListItem>
                            <asp:ListItem Value="2" Text="2 - ผู้ค้ำชำระหนี้"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:CheckBox ID="status_flag" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชำระ/งวด</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="period_payment" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:CheckBox ID="period_payment_flag" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="background-color: #449900; color: #DDFF55">ต้นเงินยกมาสิ้นปี</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="prn_last_year" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:CheckBox ID="prn_last_year_flag" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="background-color: #449900; color: #DDFF55">ดอกเบี้ยสิ้นปี</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="int_last_year" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:CheckBox ID="int_last_year_flag" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="background-color: #449900; color: #DDFF55">ดอกเบี้ยปัจจุบัน</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="int_balance" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:CheckBox ID="int_balance_flag" runat="server" />
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
