<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_detail_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 700px;">
            <tr>
                <td colspan="2">
                    <table style="width: 700px;">
                        <tr>
                            <td width="17%">
                                <div>
                                    <span>ทะเบียนหลักทรัพย์:</span>
                                </div>
                            </td>
                            <td width="15%">
                                <asp:TextBox ID="collmast_no" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                            <td width="15%">
                                <div>
                                    <span>ประเภท:</span>
                                </div>
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="collmasttype_code" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td width="15%">
                                <div>
                                    <span>เลขหลักทรัพย์:</span>
                                </div>
                            </td>
                            <td width="18%">
                                <asp:TextBox ID="collmast_refno" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <strong><u>รายละเอียด, ตำแหน่ง, เลขที่หลักทรัพย์</u></strong>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:TextBox ID="collmast_desc" runat="server" TextMode="MultiLine" Width="690px"
                                    Height="70px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 350px;">
                        <tr>
                            <td>
                                <strong><u>ราคาประเมิน</u></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ราคาที่ดิน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="landestimate_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ราคาสิ่งปลูกสร้าง:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="houseestimate_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>รวมประเมิน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="estimate_price" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="width: 350px;">
                        <tr>
                            <td>
                                <strong><u>จดจำนอง</u></strong>
                            </td>
                        </tr>
                        <tr>
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
                                    <span>เจ้าของที่ดิน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="collrelation_code" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>
                                        <asp:CheckBox ID="redeem_flag" runat="server" />ไถ่ถอนวันที่:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="redeem_date" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="700px">
                        <tr>
                            <td width="40%">
                                <div style="font-size: 22px; text-align: right;">
                                    ราคาจำนอง
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mortgage_price" runat="server" Height="42px" Width="525px" Style="text-align: right;
                                    font-size: 20px; background-color: Black;" ForeColor="#66FF33" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <strong><u>เลขที่หลักทรัพย์ที่มีพื้นที่ติดกัน:</u></strong><asp:CheckBox ID="blindland_flag"
                                    runat="server" />ที่ดินตาบอด
                            </td>
                        </tr>
                        <tr>
                            <td width="30%">
                                <asp:TextBox ID="landside_no" runat="server" Style="width: 165px; text-align: center;"></asp:TextBox>
                                <asp:Button ID="b_landsideno" runat="server" Text="..." Style="width: 20px; margin-left: 7px;" />
                            </td>
                            <td width="15%">
                                <div>
                                    <span>ใช้ไปแล้ว:</span>
                                </div>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="colluse_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                            </td>
                            <td width="15%">
                                <div>
                                    <span>คงเหลือ:</span>
                                </div>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="cp_mortgage_colluse" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <strong><u>หมายเหตุ:</u></strong>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:TextBox ID="remark" runat="server" TextMode="MultiLine" Width="695px" Height="50px"
                                    Font-Bold="True"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="DataSourceFormView">
            <tr>
                <td>
                    <span>ผู้ตรวจสอบ:</span>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="inspecter_desc" runat="server" Style="text-align: right;"></asp:TextBox>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="cp" runat="server" Style="text-align: right;" Width="270px"></asp:TextBox>
                    <asp:Button ID="b_" runat="server" Text="..." Width="25px" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="2">
                    <span style="text-align: center;">ตารางวาละ</span>
                </td>
                <td>
                    <span style="text-align: center;">ราคาที่ดิน</span>
                </td>
                <td>
                    <span style="text-align: center;">ราคาบ้าน</span>
                </td>
                <td>
                    <span style="text-align: center;">(%)</span>
                </td>
                <td>
                    <span style="text-align: center;">ราคากลาง</span>
                </td>
            </tr>
            <tr>
                <td width="16%">
                    <span>เจ้าหน้าที่ที่ดิน:</span>
                </td>
                <td width="10%">
                    <asp:TextBox ID="total_area1" runat="server" Style="text-align: right;"></asp:TextBox>
                </td>
                <td width="16%">
                    <asp:TextBox ID="price_area1" runat="server" Style="text-align: right;"></asp:TextBox>
                </td>
                <td width="16%">
                    <asp:TextBox ID="null" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="16%">
                    <asp:TextBox ID="houseestimate1_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="landest_percent" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="16%">
                    <asp:TextBox ID="TextBox1" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span>บริษัทประเมิน:</span>
                </td>
                <td>
                    <asp:TextBox ID="total_area2" runat="server" Style="text-align: right;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="price_area2" runat="server" Style="text-align: right;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="null2" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="houseestimate2_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="houseest_percent" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="2">
                    <span>ราคาอื่นๆ(ระบุ):</span>
                </td>
                <td>
                    <asp:TextBox ID="otherest_desc" runat="server" Style="text-align: right;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="otherest_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="othernet_percent" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="othernet_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
