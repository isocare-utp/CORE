<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_slip_pay_ctrl.w_dlg_sl_reprint_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <br />
        <br />
        <br />
        <table class="DataSourceFormView" border="0" style="width: 600px;">
            <tr>
                <td width="20%">
                    <div>
                        <span>ทะเบียน :</span></div>
                </td>
                <td width="30%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <span>เลขที่ใบเสร็จ :</span></div>
                </td>
                <td width="30%">
                    <div>
                        <asp:TextBox ID="payinslip_no" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อ :</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="memb_name" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ชื่อสกุล :</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="memb_surname" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หน่วย :</span></div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="membgroup_code" runat="server" Style="width: 70px; margin-right: 1px;
                            text-align: center"></asp:TextBox>
                        <asp:DropDownList ID="membgroup_desc" runat="server" Style="width: 390px; margin-left: 5px;">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <div>
                        <span>ประเภทเงินกู้ :</span></div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="loantype_code" runat="server" Style="width: 70px; margin-right: 1px; text-align:center"></asp:TextBox>
                        <asp:DropDownList ID="loantype_desc" runat="server" Style="width: 312px; margin-left: 5px;">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <div>
                        <span>ตั้งแต่วันที่ :</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="slip_date_s" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ถึงวันที่ :</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="slip_date_e" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:Button ID="b_search" runat="server" Text="ดึงข้อมูล" Style="width: 60px;" />
                    <asp:Button ID="b_cancel" runat="server" Text="ล้าง" Style="width: 60px; margin-right: 6px" />
                  
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
