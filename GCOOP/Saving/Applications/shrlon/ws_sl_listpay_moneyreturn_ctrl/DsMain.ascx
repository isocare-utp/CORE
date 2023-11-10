<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_listpay_moneyreturn_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" width="500">
            <tr>
                <td width="15%">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="35%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>รหัสพนักงาน:</span>
                    </div>
                </td>
                <td width="35%">
                    <div>
                        <asp:TextBox ID="salary_id" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>ธนาคาร:</span>
                    </div>
                </td>
                <td width="35%">
                    <div>
                    <asp:DropDownList ID="bank_code" runat="server">
                    </asp:DropDownList>
                    </div>
                </td>
            
                <td width="15%">
                    <div>
                        <span>รายการ:</span>
                    </div>
                </td>
                <td width="35%">
                    <div>
                    <asp:DropDownList ID="cash_type" runat="server">
                    </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รายการ :</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="return_type" runat="server">
                        <asp:ListItem Text="ทั้งหมด" Value="ALL">
                        </asp:ListItem>
                        <asp:ListItem Text="เงินต้นดอกเบี้ย" Value="RET">
                        </asp:ListItem>
                        <asp:ListItem Text="กสส" Value="WRT">
                        </asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="b_retrieve" runat="server" Text="ดึงข้อมูล" />
                </td>
            </tr>
        </table>
        <br />
        <hr />
        <br />
        <table class="DataSourceFormView" width="500">
            <tr>
                <td width="15%">
                    <div>
                        <span>ทำรายการโดย :</span>
                    </div>
                </td>
                <td width="35%">
                    <asp:DropDownList ID="moneytype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="15%">
                    <div>
                        <span>คู่บัญชี :</span>
                    </div>
                </td>
                <td width="35%">
                    <asp:DropDownList ID="tofrom_accid" runat="server" Style="width: 200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันที่จ่ายคืน :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="slip_date" runat="server" style="text-align:center"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
