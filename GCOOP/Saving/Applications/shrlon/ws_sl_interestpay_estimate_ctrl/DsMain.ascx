<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_interestpay_estimate_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView3" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 770px;">
            <tr>
                <td colspan="8">
                    <strong style="font-size: 16px;"><u>การรับชำระจากสมาชิก</u></strong>
                </td>
            </tr>
            <tr>
                <td width="12%">
                    <div>
                        <span style="font-size: 12px;">ทะเบียนสมาชิก :</span>
                    </div>
                </td>
                <td width="18%">
                    <asp:TextBox ID="member_no" runat="server" Style="width: 95px; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_memsearch" runat="server" Text="..." Style="width: 25px; margin-left: 7px;" />
                </td>
                <td width="12%">
                    <div>
                        <span style="font-size: 12px;">ชื่อ-ชื่อสกุล :</span>
                    </div>
                </td>
                <td colspan="3" width="30%">
                    <asp:TextBox ID="name_m" runat="server" Style="font-size: 12px; text-align: left;
                        background-color: #FFFF99;" Width="98%" ReadOnly="true"></asp:TextBox>
                </td>
                <td colspan="2" width="24%">
                    <strong style="margin-left: 5px; font-size: 18px;">รวมยอดชำระ : </strong>
                </td>
            </tr>
            <tr>
                <td width="12%">
                    <div>
                        <span>ด/บ ถึงวันที่ :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="operate_date" runat="server" Style="text-align: center; background-color: #FFFFFF;"
                        ReadOnly="false"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">หน่วย :</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="membgroup_desc" runat="server" Style="font-size: 12px; text-align: left;
                        background-color: #FFFF99;" Width="98%" ReadOnly="true"></asp:TextBox>
                </td>
                <td colspan="2" rowspan="2">
                    <asp:TextBox ID="slip_amt" runat="server" Style="background-color: Black; margin-left: 5px;
                        text-align: right; font-size: 24px;" Width="180" Height="45" ToolTip="#,##0.00"
                        ForeColor="GreenYellow" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">ทุนเรือนหุ้น :</span>
                    </div>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="sharestk_value" runat="server" Style="width: 475px; text-align: right;
                        background-color: #FFFF99;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                   <asp:Button ID="b_save" runat="server" Text="พิมพ์ใบประมาณการ" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
