<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_req_chgcontlaw_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>เลขที่คำขอ:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="lnchgcontlaw_docno" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td width="5%">
                </td>
                <td width="15%">
                    <div>
                        <span>วันที่ตั้งลูกหนี้:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="lnchgcontlaw_date" runat="server" Style="width: 120px; text-align: center"></asp:TextBox>
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
                        <asp:TextBox ID="member_no" runat="server" Style="width: 100px;"></asp:TextBox>
                        <asp:Button ID="b_memsearch" runat="server" Text="..." Style="width: 30px; margin-left: 2px;" />
                    </div>
                </td>
                <td>
                </td>
                <td>
                    <div>
                        <span>ชื่อสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="memb_name" runat="server" ReadOnly="true"></asp:TextBox>
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
                </td>
                <td>
                    <div>
                        <span>ประเภทเงินกู้:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="compute_2" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <hr style="width: 720px;" align="left" />
        <br />
        <table width="725">
            <tr>
                <td align="left" width="60%">
                    <table class="DataSourceRepeater" style="width: 100%;">
                        <tr>
                            <th colspan="2">
                                ข้อมูลก่อนเปลี่ยนแปลง
                            </th>
                        </tr>
                        <tr>
                            <th width="30%">
                                สถานะทางกฎหมาย:
                            </th>
                            <td>
                                <asp:DropDownList ID="bfcontlaw_status" runat="server" Enabled="false">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="ลูกหนี้ปกติ" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="ลูกหนี้สงสัยจะสูญ" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="ลูกหนี้ดำเนินคดี" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="ลูกหนี้ตามคำพิพากษา" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table class="DataSourceRepeater" style="width: 100%;">
                        <tr>
                            <th width="25%">
                                วันที่คิด ด/บ ล่าสุด
                            </th>
                            <th width="25%">
                                เงินกู้คงเหลือ
                            </th>
                            <th width="25%">
                                ดอกเบี้ยค้างชำระ
                            </th>
                            <th width="25%">
                                ดอกเบี้ยตั้งลูกหนี้
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="bflastcalint_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="bfprnbal_amt" runat="server" ReadOnly="true" Style="text-align: right;"
                                    ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="bfintarrear_amt" runat="server" ReadOnly="true" Style="text-align: right;"
                                    ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="bfintarrset_amt" runat="server" ReadOnly="true" Style="text-align: right;"
                                    ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="right">
                    <table class="DataSourceRepeater" style="width: 98%;">
                        <tr>
                            <th colspan="2">
                                เปลี่ยนแปลงเป็น
                            </th>
                        </tr>
                        <tr>
                            <th width="45%">
                                สถานะทางกฎหมาย:
                            </th>
                            <td width="55%">
                                <asp:DropDownList ID="contlaw_chgtostatus" runat="server">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="ลูกหนี้ปกติ" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="ลูกหนี้สงสัยจะสูญ" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="ลูกหนี้ดำเนินคดี" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="ลูกหนี้ตามคำพิพากษา" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table class="DataSourceRepeater" style="width: 98%;">
                        <tr>
                            <th width="45%">
                                เงินต้น
                            </th>
                            <th width="55%">
                                ดอกเบี้ยตั้งลูกหนี้
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="contlaw_prnset" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="contlaw_intarrset" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
