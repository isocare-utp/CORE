<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wd_detail.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_share_edit_ctrl.wd_detail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 700px;">
            <tr>
                <td>
                    <div>
                        <span>ประเภท:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="sharetype_desc" runat="server" ReadOnly="true" BackColor="#DDDDDD"
                        Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ทุนเรือนหุ้น:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_shrstk" runat="server" Style="text-align: right" ReadOnly="true"
                        BackColor="#DDDDDD" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><u>ทุนเรือนหุ้น</u></strong>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <div>
                        <span>จำนวนหุ้นยกมาต้นปี:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="sharebegin_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="25%">
                    <div>
                        <span>มูลค่าหุ้นยกมาต้นปี:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="shrbegin_value" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จำนวนหุ้นสะสม:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="sharestk_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>มูลค่าหุ้นสะสม:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="shrstk_value" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">จำนวนหุ้นต่อเดือน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="periodshare_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>มูลค่าหุ้นต่อเดือน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="shrperiod_value" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จำนวนหุ้นตามฐาน งด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="periodbase_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>มูลค่าหุ้นตามฐาน งด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="shrbase_value" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong><u>ผ่อนผัน</u></strong>
                </td>
                <td colspan="2">
                    <strong><u>ลำดับล่าสุด</u></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สถานะการผ่อนผัน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="compound_status" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>งวดล่าสุด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="last_period" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันที่ขอผ่อนผัน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="compound_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>Statement ล่าสุด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="last_stm_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จน.งวดขอผ่อนผัน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="compound_period" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <strong><u>อื่นๆ</u></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันครบกำหนดผ่อนผัน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="compounddue_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>จำนวนครั้งที่เก็บไม่ได้:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="misspay_amt" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชำระหลังครบกำหนด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="afterdue_shramt" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ทำรายการต่อวัน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="doperate_bal" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สถานะหลังครบกำหนด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="afterdue_shrstatus" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <strong><u>สถานะ</u></strong>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong><u>เรียกเก็บ</u></strong>
                </td>
                <td>
                    <div>
                        <span>สถานะการอายัดหุ้น:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="sequest_status" runat="server">
                        <asp:ListItem Value="0">ปกติ</asp:ListItem>
                        <asp:ListItem Value="1">อายัด</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เงินรอเรียกเก็บ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="rkeep_sharevalue" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สถานะการส่งหุ้น:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="payment_status" runat="server">
                        <asp:ListItem Value="1">ปกติ</asp:ListItem>
                        <asp:ListItem Value="-1">งดหุ้น</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
