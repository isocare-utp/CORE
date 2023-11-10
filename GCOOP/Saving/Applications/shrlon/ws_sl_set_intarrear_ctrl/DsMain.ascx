<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_set_intarrear_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 730px;">
            <tr>
                <td width="15%">
                    <div>
                        <span>เลขที่คำขอ:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="intarrset_docno" runat="server" ReadOnly="true" Style="text-align: center;
                            background-color: InfoBackground;"></asp:TextBox>
                    </div>
                </td>
                <td width="5%">
                    &nbsp;
                </td>
                <td width="15%">
                    <div>
                        <span>วันที่ตั้งดอกเบี้ย:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="intarrset_date" runat="server" Style="width: 120px; text-align: center;"
                            BackColor="#cccccc" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่สมาชิก:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="width: 100px; text-align: center;"></asp:TextBox>
                        <asp:Button ID="b_memsearch" runat="server" Text="..." Style="width: 30px; margin-left: 2px;" />
                    </div>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <div>
                        <span>ชื่อสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="cp_name" runat="server" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่สัญญา:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="loancontract_no" runat="server" Style="width: 100px;">
                        </asp:DropDownList>
                        <asp:Button ID="b_contsearch" runat="server" Text="..." Style="width: 30px; margin-left: 2px;" />
                    </div>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <div>
                        <span>ประเภทเงินกู้:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="cp_loantype" runat="server" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <hr align="left" style="width: 720px;" />
        <br />
        <table style="width: 720px;">
            <tr>
                <td width="50%" align="left">
                    <table class="DataSourceRepeater" style="width: 98%">
                        <tr>
                            <th width="36%">
                                เงินกู้คงเหลือ
                            </th>
                            <th width="32%">
                                วันที่คิด ด/บ ล่าสุด
                            </th>
                            <th width="32%">
                                ดอกเบี้ยค้างชำระ
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="bfprnbal_amt" ToolTip="#,##0.00" runat="server" Style="text-align: right;"
                                    BackColor="#cccccc" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="bflastcalint_date" runat="server" Style="text-align: center;" BackColor="#cccccc"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="bfintarrear_amt" ToolTip="#,##0.00" runat="server" Style="text-align: right;"
                                    BackColor="#cccccc" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" align="right">
                    <table class="DataSourceRepeater" style="width: 240px;">
                        <tr>
                            <th width="50%">
                                คำนวนดอกเบี้ยถึง
                            </th>
                            <th width="50%">
                                ดอกเบี้ยตั้งค้าง
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="intarrset_caldate" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="intarrset_amt" ToolTip="#,##0.00" runat="server" Style="text-align: right;"
                                    BackColor="#cccccc" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
