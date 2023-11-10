<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr_const.ws_mb_mbconstant_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td colspan="4">
                    <div>
                        <b><span style="text-align: center; width: 99%;">ข้อกำหนดสมาชิก</span></b>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <div>
                        <span>ประเภทสมาชิก:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:DropDownList ID="member_type" runat="server" Style="font-size: 12px;">
                        <asp:ListItem Value="1">สมาชิกปกติ</asp:ListItem>
                        <asp:ListItem Value="2">สมาชิกสมทบ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <table class="DataSourceFormView">
            <tr>
                <td>
                    <table style="width: 360px;">
                        <tr>
                            <td colspan="2">
                                <u><b>เงื่อนไขรหัสสมาชิก</b></u>
                            </td>
                        </tr>
                        <tr>
                            <td width="50%">
                                <div>
                                    <span>เลขเอกสาร:</span>
                                </div>
                            </td>
                            <td width="50%">
                                <div>
                                    <asp:TextBox ID="doccument_code" runat="server"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>Fomat เลขสมาชิก:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="membno_format" runat="server"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>Suffix เลขสมาชิก:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="memno_suffix" runat="server"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="width: 360px;">
                        <tr>
                            <td colspan="2">
                                <u><b>เงื่อนไขการเกษียณ</b></u>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ประเภทการคำนวณ:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="retry_type" runat="server"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>อายุเกษียณ:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="retry_age" runat="server" Style="text-align: center;"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>วันเดือนครบกำหนด:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="retry_date" runat="server" Style="text-align: center;"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="DataSourceFormView">
            <tr>
                <td colspan="6">
                    <u><b>เงื่อนไขอื่นๆ</b></u>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จำนวนการสมัครใหม่:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="maxregister_amt" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ระยะเวลาการสมัครใหม่(เดือน):</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="minresign_mth" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
