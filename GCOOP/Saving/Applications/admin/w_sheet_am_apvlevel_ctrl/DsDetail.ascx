<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.admin.w_sheet_am_apvlevel_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 400px;">
            <tr>
                <td colspan="2" style="text-decoration: underline; font-size: 13px; font-weight: bold;">
                    รหัสและชื่อระดับการอนุมัติ:
                </td>
            </tr>
            <tr>
                <td width="30%">
                    <div>
                        <span>รหัส:</span>
                    </div>
                </td>
                <td width="70%">
                    <div>
                        <asp:TextBox ID="apvlevel_id" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>คำอธิบาย:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="description" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-decoration: underline; font-size: 13px; font-weight: bold;">
                    ระดับการอนุมัติ(เงินกู้):
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วงเงินกู้สูงสุด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="loan_appovmax" runat="server" ToolTip="##,000.00" Style="text-align: right"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-decoration: underline; font-size: 13px; font-weight: bold;">
                    ระดับการอนุมัติ(เงินฝาก):
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ถอนได้สูงสุด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="dept_withdrawmax" runat="server" ToolTip="##,000.00" Style="text-align: right"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ฝากได้สูงสุด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="dept_depositmax" runat="server" ToolTip="##,000.00" Style="text-align: right"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>apv score:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="app_score" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-decoration: underline; font-size: 13px; font-weight: bold;">
                    ระดับการอนุมัติ(การเงิน): 
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ยอดเงินสดในลิ้นชักสูงสุด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="fin_maxbalance" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-decoration: underline; font-size: 13px; font-weight: bold;">
                    การปรับปรุงรายการ
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="undo_status" runat="server" Text="สามารถทำรายการย้อนรายการ" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="adjustment_status" runat="server" Text="สามารถย้อนและปรับปรุงรายการได้" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="changedate_status" runat="server" Text="สามารถเปลี่ยนวันทำการได้" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="nofee_status" runat="server" Text="สามารถยกเลิกการปรับเงินฝากได้" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="dto_wto_flag" runat="server" Text="สามารถทำรายการฝากถอน ย้อนวันได้" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="sequest_status" runat="server" Text="สามารถอายัดเงินฝากได้" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="freeze_flag" runat="server" Text="สามารถระงับการเข้าระบบได้" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
