<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.ws_as_cancelass_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView2" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table border="0">
            <br />
        </table>
        <table class="DataSourceFormView">
            <tr>
                <td style="width: 13%">
                </td>
                <td style="width: 17%">
                </td>
                <td style="width: 13%">
                </td>
                <td style="width: 17%">
                </td>
                <td style="width: 13%">
                </td>
                <td style="width: 27%">
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Style="width: 75%; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Style="width: 25px;" />
                </td>
                <td>
                    <div>
                        <span>ชื่อ-สกุล:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="mbname" runat="server" ReadOnly="true" Style="background-color: #CCCCCC;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สังกัด :</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="mbgroup" runat="server" Style="background-color: #CCCCCC;" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ประเภทสมาชิก :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mbtype" runat="server" Style="text-align: center; background-color: #CCCCCC" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่สวัสดิการ:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="asscontract_no" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <strong style="font-size: 12px;">รายละเอียดสวัสดิการ</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเภทสวัสดิการ:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="asstypedesc" runat="server" BackColor="#CCCCCC" ReadOnly="True">
                    </asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ประจำปี:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="assist_year" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเภทการจ่าย:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="asspaydesc" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ยอดอนุมัติ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="approve_amt" runat="server" Style="text-align: right; font-weight: bold;"
                        ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>งวดรับทุน :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="last_periodpay" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>วันที่อนุมัติ :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="approve_date" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ทุนที่รับไปแล้ว :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="pay_balance" runat="server" Style="text-align: right; font-weight: bold;"
                        ToolTip="#,##0.00" ForeColor="Red" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เหตุผลที่ยกเลิก:</span>
                    </div>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="cancel_cause" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
