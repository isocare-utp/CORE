<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications._global.w_dlg_sh_detail_share_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 700px;">
            <tr>
                <td>
                    <div>
                        <span>ชื่อ-ชื่อสกุล:</span>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                        <asp:TextBox ID="COMPUTE_1" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ประเภทหุ้น:</span>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                        <asp:TextBox ID="COMPUTE_2" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div>
                    <U>
                        ทุนเรือนหุ้น
                        </U>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                      <U>
                        หุ้นค้างจ่าย
                         </U>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                     <U>
                     อื่นๆ
                      </U>
                    </div>
                  
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>ยกมาต้นปี:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="COMPUTE_3" runat="server" ReadOnly="true" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ยกมาต้นปี:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="COMPUTE_4" runat="server" ReadOnly="true" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>งวดล่าสุด:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="last_period" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ค่าหุ้นสะสม:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="COMPUTE_5" runat="server" ReadOnly="true" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ค่าหุ้นค้างจ่าย:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="COMPUTE_6" runat="server" ReadOnly="true" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ผิดนัดชำระ:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="misspay_amt" runat="server" ReadOnly="true" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หุ้นต่อเดือน:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="COMPUTE_7" runat="server" ReadOnly="true" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>หุ้นฐานงด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="COMPUTE_8" runat="server" ReadOnly="true" Style="text-align: right;" ToolTip="#,##0"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>การอายัดหุ้น:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="COMPUTE_9" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div>
                    <U>
                        ผ่อนผัน
                        </U>
                    </div>
                </td>
                <td>
                    <div>
                        <span>การส่งหุ้น:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="sequest_status" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>การผ่อนผัน:</span>
                    </div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="TextBox11" runat="server" ReadOnly="true" Width="99%"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>สถานะหุ้น:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="COMPUTE_10" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
        <br />
    </EditItemTemplate>
</asp:FormView>
