<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_insurance_type_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="510px">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 510px">
            <tr>
                <td colspan="6">
                    <strong style="font-size: 12px;">รายละเอียดประกัน</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รหัสประกัน:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="instype_code" runat="server" Style="text-align: center;" Enabled="false"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ชื่อประกัน:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="instype_desc" runat="server" Enabled="False">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อบริษัทประกัน:</span>
                    </div>
                </td>
                <td colspan="5">
                    <div>
                        <asp:TextBox ID="inscompay_name" runat="server" Style="text-align: center;" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขกรมธรรม์:</span>
                    </div>
                </td>
                <td colspan="5">
                    <div>
                        <asp:TextBox ID="insurencedoc_no" runat="server" Style="text-align: center;" ></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วงเงินประกัน:</span>
                    </div>
                </td>
                <td colspan="1">
                    <div>
                        <asp:TextBox ID="min_inscost" runat="server"  Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ถึง :</span>
                    </div>
                </td>
                <td colspan="1">
                    <div>
                        <asp:TextBox ID="max_inscost" runat="server"  Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="19%">
                    <div>
                        <span>วันที่เริ่มกรมธรรม์:</span>
                    </div>
                </td>
                <td width="17%">
                    <div>
                        <asp:TextBox ID="start_date" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="19%">
                    <div>
                        <span>วันที่สิ้นสุดกรมธรรม์:</span>
                    </div>
                </td>
                <td width="17%">
                    <div>
                        <asp:TextBox ID="end_date" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="16%">
                    <div>
                        <span>เงินเบี้ยประกัน:</span>
                    </div>
                </td>
                <td width="12%">
                    <div>
                        <asp:TextBox ID="period_payment" runat="server" Style="text-align: center;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td width="19%">
                    <div>
                        <span>ค่าธรรมเนียม:</span>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <asp:TextBox ID="fee_amt" runat="server" Style="text-align: center;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="16%">
                    <div>
                        <span>อายุที่สมัครได้:</span>
                    </div>
                </td>
                <td width="12%">
                    <div>
                        <asp:TextBox ID="MIN_AGE" runat="server" Style="text-align: center;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td width="19%">
                    <div>
                        <span>ถึงอายุ:</span>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <asp:TextBox ID="MAX_AGE" runat="server" Style="text-align: center;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ค่งคงที่อนุมัติ:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="approveins_code" runat="server" Rows="3" SelectionMode="Single" Style="text-align: center;" ToolTip="#,##0.00">
                    <asp:ListItem Text="อนุมัติ" Value="1"></asp:ListItem>
                    <asp:ListItem Text="ไม่อนุมัติ" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="รออนุมัติ" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>
                    <div>
                        <span>การเรียกเก็บ:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="keeping_flag" runat="server" Rows="3" SelectionMode="Single" Style="text-align: center;" ToolTip="#,##0.00">
                    <asp:ListItem Text="เรียกเก็บ" Value="8"></asp:ListItem>
                    <asp:ListItem Text="ปกติ" Value="1"></asp:ListItem>
                    <asp:ListItem Text="งดส่ง" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>
                    <div>
                        <span>การเก็บเบี้ยประกัน:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="paymentlevel_flag" runat="server" Rows="2" SelectionMode="Single" Style="text-align: center;" ToolTip="#,##0.00">
                    <asp:ListItem Text="รายเดือน" Value="1"></asp:ListItem>
                    <asp:ListItem Text="รายปี" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

        </table>
    </EditItemTemplate>
</asp:FormView>
