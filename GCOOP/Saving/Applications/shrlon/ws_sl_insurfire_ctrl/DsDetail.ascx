<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_insurfire_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table width="740px">
            <tr>
                <td>
                    <table class="DataSourceFormView" style="width: 360px;">
                        <tr>
                            <td colspan="2">
                                <b><u>รายละเอียดกรมธรรม์</u></b>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <span>เลขที่กรมธรรม์:</span>
                                </div>
                            </td>
                            <td width="30%">
                                <div>
                                    <asp:TextBox ID="insurance_no" runat="server" Style="text-align: right"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>อ้างอิงหลักทรัพย์:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="collmast_no" runat="server" Style="width: 175px; text-align: center"></asp:TextBox>
                                    <asp:Button ID="b_contsearch" runat="server" Text="..." Style="width: 30px; margin-left: 2px;" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>วันที่ป้อนรายการ:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="operate_date" runat="server" Style="text-align: right"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>วันที่เริ่มคุ้มครอง:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="startinsure_date" runat="server" Style="text-align: right"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>วันที่หมดอายุ:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="expireinsure_date" runat="server" Style="text-align: right"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>สถานะการชำระ:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="insurepay_status" runat="server">
                                        <asp:ListItem Value="8">ยังไม่ชำระ</asp:ListItem>
                                        <asp:ListItem Value="1">ชำระแล้ว</asp:ListItem>
                                        <asp:ListItem Value="-9">ยกเลิก</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>สถานะการเรียกเก็บ:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="mthkeep_status" runat="server">
                                        <asp:ListItem Value="0">ไม่ระบุ</asp:ListItem>
                                        <asp:ListItem Value="8">รอเรียกเก็บ</asp:ListItem>
                                        <asp:ListItem Value="1">เรียกเก็บแล้ว</asp:ListItem>
                                        <asp:ListItem Value="-9">คืนใบเสร็จ</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top;">
                    <table class="DataSourceFormView" style="width: 360px;">
                        <tr>
                            <td colspan="2">
                                <b><u>ค่าเบี้ยประกัน</u></b>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <span>เบี้ยประกัน:</span>
                                </div>
                            </td>
                            <td width="30%">
                                <div>
                                    <asp:TextBox ID="insurance_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>อากรแสตมป์:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="stampduty_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>VAT:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="vat_percent" runat="server" Style="width: 50px; text-align: right"
                                        ToolTip="#,##0.00%"></asp:TextBox>
                                    <asp:TextBox ID="c_vatamt" runat="server" Style="width: 175px; text-align: right"
                                        ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>รวม:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="vat_sum" runat="server" ToolTip="#,##0.00" Style="text-align: right"
                                        ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="DataSourceFormView" style="width: 360px;">
                        <tr>
                            <td colspan="2">
                                <b><u>สมาชิกต้องชำระ</u></b>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <span>ส่วนลด:</span>
                                </div>
                            </td>
                            <td width="30%">
                                <div>
                                    <asp:TextBox ID="discount_percent" runat="server" Style="width: 50px; text-align: right"
                                        ToolTip="#,##0.00%"></asp:TextBox>
                                    <asp:TextBox ID="dp_sum" runat="server" Style="width: 175px; text-align: right" ToolTip="#,##0.00"
                                        ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <span>เบี้ยอุทกภัย:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="floodinsure_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>สมาชิกชำระ:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="insurenet_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top">
                    <table class="DataSourceFormView" style="width: 360px;">
                        <tr>
                            <td colspan="2">
                                <b><u>สุทธิจ่ายให้บริษัท</u></b>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <span>ค่าประสานงาน:</span>
                                </div>
                            </td>
                            <td width="30%">
                                <div>
                                    <asp:TextBox ID="coordinate_percent" runat="server" Style="width: 50px; text-align: right"
                                        ToolTip="#,##0.00%"></asp:TextBox>
                                    <asp:TextBox ID="coor_sum" runat="server" Style="width: 175px; text-align: right"
                                        ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <span>สุทธิจ่ายให้บริษัท:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="txt_sum" runat="server" ToolTip="#,##0.00" Style="text-align: right"
                                        ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
