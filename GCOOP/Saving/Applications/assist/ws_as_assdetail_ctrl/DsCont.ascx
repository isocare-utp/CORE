<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCont.ascx.cs" Inherits="Saving.Applications.assist.ws_as_assdetail_ctrl.DsCont" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 520px;">
            <tr>
                <td style="width: 20%">
                    <div>
                        <span>เลขสวัสดิการ:</span>
                    </div>
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="asscontract_no" runat="server" Style="text-align: center" ReadOnly="True">
                    </asp:TextBox>
                </td>
                <td style="width: 20%">
                    <div>
                        <span>ประจำปี:</span>
                    </div>
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="assist_tyear" runat="server" Style="text-align: center" ReadOnly="True">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเภท:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="asstypedesc" runat="server" ReadOnly="True">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>การจ่าย:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="asspaydesc" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ผู้รับทุน:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="ass_rcvname" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขบัตร ปชช: </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="ass_rcvcardid" runat="server" Style="text-align: center" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เลขบัตร Ref: </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="ass_prcardid" runat="server" Style="text-align: center" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันที่อนุมัติ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="approve_date" runat="server" Style="text-align: center" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ยอดอนุมัติ: </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="approve_amt" runat="server" ReadOnly="true" BackColor="#DCDCDC"
                        ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ยอดรับแล้ว: </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="pay_balance" runat="server" ReadOnly="true" BackColor="#DCDCDC"
                        ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เบิกได้อีก: </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="withdrawable_amt" runat="server" ReadOnly="true" BackColor="#DCDCDC"
                        ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>งวดรับเงิน: </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="last_periodpay" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>รับเงินล่าสุด: </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="lastpay_date" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทุนต่อเนื่อง: </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="stmflagdesc" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เงื่อนไข: </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="stmcondesc" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สถานะ: </span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="statusdesc" runat="server" ReadOnly="true" Style="text-align: center;width:99%;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                 <td width="5%" colspan="4">
                    <asp:Button ID="b_eimg" runat="server" Style="width:100%; height: 25px; font-size: 15px;"
                        Text="แก้ไขเอกสารการขอสวัสดิการ"/>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
