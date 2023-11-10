<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_req_chgmthshr_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="14%">
                    <div>
                        <span>เลขที่เอกสาร :</span>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <asp:TextBox ID="payadjust_docno" runat="server" Style="background-color: ButtonFace;"
                            ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td width="14%">
                    <div>
                        <span>วันที่เปลี่ยน :</span>
                    </div>
                </td>
                <td width="28%">
                    <div>
                        <asp:TextBox ID="payadjust_date" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td width="14%">
                    <div>
                        <span>ผู้ทำรายการ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="entry_id" runat="server" Style="background-color: ButtonFace;" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทะเบียน :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center" width="11ex"></asp:TextBox><asp:Button ID="bt_search" runat="server" Text="..." width="3ex" />
                    </div>
                </td>
                <td>
                    <div>
                        <span>ชื่อ-ชื่อสกุล :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="cp_name" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>เงินเดือน :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="salary_amount" runat="server" Style="text-align: right; background-color: ButtonFace;"
                            ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รหัสพนักงาน :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="salary_id" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>วันเป็นสมาชิก :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="member_date" runat="server" Style="text-align: center; background-color: ButtonFace;"
                            ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>สังกัด :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="cp_membgroup" runat="server" Style="background-color: ButtonFace;"
                            ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>การอนุมัติ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:CheckBox ID="apvimmediate_flag" runat="server" Text="อนุมัติทันที" Height="22px"
                            Style="text-align: left; background-color: White;" />
                    </div>
                </td>
            </tr>
        </table>
        <table class="DataSourceFormView">
            <tr>
                <td colspan="4">
                    รายละเอียดหุ้น:
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>หุ้นยกมาต้นปี :</span>
                    </div>
                </td>
                <td width="35%">
                    <div>
                        <asp:TextBox ID="sharebegin_value" runat="server" Style="text-align: right; background-color: ButtonFace;"
                            ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>หุ้นสะสม :</span>
                    </div>
                </td>
                <td width="35%">
                    <div>
                        <asp:TextBox ID="sharestk_value" runat="server" Style="text-align: right; background-color: ButtonFace;"
                            ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>งวดหุ้น :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="shrlast_period" runat="server" Style="text-align: center; background-color: ButtonFace;"
                            ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>หุ้นฐาน ง/ด :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="periodbase_value" runat="server" Style="text-align: right; background-color: ButtonFace;"
                            ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    รายละเอียดการเปลี่ยนแปลง:
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div>
                        <span style="text-align: center">ก่อนเปลี่ยนแปลง</span>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                        <span style="text-align: center">หลังเปลี่ยนแปลง</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สถานะการส่ง :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="old_paystatus" runat="server" Enabled="false">
                            <asp:ListItem Value="1">ส่งหุ้นปกติ</asp:ListItem>
                            <asp:ListItem Value="-1">งดส่งหุ้นรายเดือน</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td>
                    <div>
                        <span>สถานะการส่ง :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="new_paystatus" runat="server">
                            <asp:ListItem Value="1">ส่งหุ้นปกติ</asp:ListItem>
                            <asp:ListItem Value="-1">งดส่งหุ้นรายเดือน</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หุ้น/เดือน :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="old_periodvalue" runat="server" Style="text-align: right; background-color: ButtonFace;"
                            ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>หุ้น/เดือน :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="new_periodvalue" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    หมายเหตุ:
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <textarea id="remark" rows="4" cols="90"></textarea>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
