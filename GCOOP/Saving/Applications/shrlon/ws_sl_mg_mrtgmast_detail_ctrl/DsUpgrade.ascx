<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsUpgrade.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_detail_ctrl.DsUpgrade" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel2" runat="server" Height="480px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 720px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <table class="DataSourceFormView" style="width: 550px; background-color: #CCFF99;
                    border: 1px dotted black;">
                    <tr>
                        <td width="20%">
                            <div>
                                <span>ครั้งที่ขึ้นเงิน:</span>
                            </div>
                        </td>
                        <td width="30%">
                            <asp:TextBox ID="upgrade_no" runat="server" Style="text-align: center;"></asp:TextBox>
                        </td>
                        <td width="20%">
                            <div>
                                <span>วันที่ขึ้นเงิน:</span>
                            </div>
                        </td>
                        <td width="30%">
                            <asp:TextBox ID="upgrade_date" runat="server" Style="text-align: center;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <span>จำนวนเงินที่ขึ้น:</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="upgrade_addamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                        </td>
                        <td>
                            <div>
                                <span>อัตรา ด/บ:</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="upgrade_intrate" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <strong><u>ผู้มอบอำนาจ 1</u></strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <span>ผู้มอบอำนาจ 1:</span>
                            </div>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="autrz_name1" runat="server" Style="width: 427px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <span>ตำแหน่ง 1:</span>
                            </div>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="autrz_pos1" runat="server" Style="width: 427px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <strong><u>ผู้มอบอำนาจ 2</u></strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <span>ผู้มอบอำนาจ 2:</span>
                            </div>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="autrz_name2" runat="server" Style="width: 427px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <span>ตำแหน่ง 2:</span>
                            </div>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="autrz_pos2" runat="server" Style="width: 427px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <strong><u>ผู้รับมอบอำนาจ</u></strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <span>ผู้รับมอบอำนาจ:</span>
                            </div>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="autzd_name" runat="server" Style="width: 427px;"></asp:TextBox>                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <span>อายุ:</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="autzd_age" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <div>
                                <span>เชื้อชาติ:</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="autzd_nationality" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <span>สัญชาติ:</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="autzd_citizenship" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <div>
                                <span>บิดา/มารดา ชื่อ:</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="autzd_parentname" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <strong><u>ที่อยู่ผู้รับมอบอำนาจ</u></strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <span>หมู่บ้าน:</span>
                            </div>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="autzd_village" runat="server" Style="width: 430px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <span>เลขที่:</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="autzd_address" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <div>
                                <span>หมู่:</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="autzd_moo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <span>ซอย:</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="autzd_soi" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <div>
                                <span>ถนน:</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="autzd_road" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <span>ตำบล:</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="autzd_tambol" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <div>
                                <span>อำเภอ:</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="autzd_amphur" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <span>จังหวัด:</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="autzd_province" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
