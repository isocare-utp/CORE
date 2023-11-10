<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_detail_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 720px;">
            <tr>
                <td>
                    <table style="width: 720px;">
                        <tr>
                            <td width="17%">
                                <div>
                                    <span style="font-size: 12px;">ทะเบียนจำนอง:</span>
                                </div>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="mrtgmast_no" runat="server" Style="text-align: center; font-size: 12px;"></asp:TextBox>
                            </td>
                            <td width="20%">
                                <div>
                                    <span style="font-size: 12px;">ประเภทหลักทรัพย์:</span>
                                </div>
                            </td>
                            <td width="43%">
                                <asp:DropDownList ID="assettype_code" runat="server" Style="font-size: 12px;">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="DataSourceFormView" style="width: 720px;">
                        <tr>
                            <td colspan="6">
                                <strong style="font-size: 14px;">รายละเอียดการจำนอง</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <asp:CheckBox ID="collmate_flag" runat="server" Text="ค้ำคู่สมรส" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="collmate_memno" runat="server"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="cp_matename" runat="server" BackColor="#EBEBEB" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span>วันที่จำนอง:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mortgage_date" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ประเภทจำนอง:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="mortgage_type" runat="server">
                                    <asp:ListItem Value="0">แปลงเดียว</asp:ListItem>
                                    <asp:ListItem Value="1">จำนองเฉพาะส่วน</asp:ListItem>
                                    <asp:ListItem Value="2">จำนวนรวมหลายแปลง</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <div>
                                    <span>จำนวนแปลง:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mortgage_landnum" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span>เงินจำนองครั้งแรก:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mortgagefirst_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                <div>
                                    <span>ส่วนของตัวเอง:</span>
                                </div>
                            </td>
                            <td width="18%">
                                <asp:TextBox ID="mortgage_partamt" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                            <td width="15%">
                                <div>
                                    <span>ส่วนของทั้งหมด:</span>
                                </div>
                            </td>
                            <td width="17%">
                                <asp:TextBox ID="mortgage_partall" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                            <td width="17%">
                                <div>
                                    <span>เงินจำนองรวม:</span>
                                </div>
                            </td>
                            <td width="18%">
                                <asp:TextBox ID="mortgagesum_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ชื่อผู้จำนอง:</span>
                                </div>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="mortgage_partname" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span>อัตรา ด/บ:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="interest_rate" runat="server" Style="text-align: center;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="DataSourceFormView" style="width: 720px;">
                        <tr>
                            <td width="28%">
                                <div>
                                    <span>เป็นประกันเงินกู้ทุกประเภทของ:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mortgage_grtname" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="DataSourceFormView" style="width: 720px;">
                        <tr>
                            <td colspan="2">
                                <strong style="font-size: 14px;">สำนักงานที่ดิน</strong>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                <div>
                                    <span>สำนักงานที่ดิน:</span>
                                </div>
                            </td>
                            <td width="85%">
                                <asp:TextBox ID="land_office" runat="server" Style="width: 602px;"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
