<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_pea_ctrl.w_dlg_loan_search_receive_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
    <br />
    <br />
    <br />
        <table class="DataSourceFormView" border="0" style="width: 500px;">
        <tr>
                <td width="20%">
                    <div>
                        <span>เลขสัญญา :</span></div>
                </td>
                <td width="30%">
                    <div>
                        <asp:TextBox ID="loancontract_no" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <span>รหัสพนักงาน :</span></div>
                </td>
                <td width="30%">
                    <div>
                        <asp:TextBox ID="salary_id" runat="server" ></asp:TextBox>
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
                        <asp:TextBox ID="memb_surname" runat="server" ></asp:TextBox>
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
                        <asp:TextBox ID="membgroup_code" runat="server" Style="width: 70px; margin-right: 1px; text-align:center"></asp:TextBox>
                        <asp:DropDownList ID="membgroup_desc" runat="server" Style="width: 312px; margin-left: 5px;">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
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
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันที่อนุมัติ :</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="approve_date_s" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ถึงวันที่ :</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="approve_date_e" runat="server" ></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ผู้อนุมัติ :</span></div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="approve_id" runat="server" Style="width: 388px;"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <div>
                        <asp:Button ID="b_search" runat="server" Text="ตกลง" Style="width: 60px; margin-right: 1px;" />
                        <asp:Button ID="b_cancel" runat="server" Text="ยกเลิก" Style="width: 60px; margin-right: 7px;" />
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
