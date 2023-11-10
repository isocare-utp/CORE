<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.dlg.wd_dep_search_deptaccount_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:FormView ID="FormViewMain" runat="server" DefaultMode="Edit">
   <EditItemTemplate>
        <table class="DataSourceFormView" style="width:680px;">
            <tr>
                <td width="24%">
                    <div><span>ประเภทบัญชี :</span></div>
                </td>
                <td width="25%">
                    <asp:DropDownList ID="depttype_code" runat="server"></asp:DropDownList>
                </td>
                <td width="24%">
                    <div><span>เลขที่บัญชี :</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="deptaccount_no" runat="server" Style="text-align:center;" ></asp:TextBox>
                </td>
                <td colspan="2" rowspan="5">
                    <asp:Button ID="b_searchdeptacc" runat="server" Text="ค้นหา" Height="140px" Width="70px" />
                </td>
            </tr>
             <tr>
                <td>
                    <div><span>ชื่อบัญชี :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="deptaccount_name" runat="server" Style="text-align:center; width:99.1%;"></asp:TextBox>
                </td>
                <td>
                    <div><span>สถานะบัญชี :</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="deptclose_status" runat="server" >
                        <asp:ListItem Value="0">เปิดบัญชี</asp:ListItem>
                        <asp:ListItem Value="1">ปิดบัญชี</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div><span>เลขที่สมาชิก :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Style="text-align:center;"></asp:TextBox>
                </td>
                <td width="15%">
                    <div><span>เลขพนักงาน :</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="salary_id" runat="server" Style="text-align:center;" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div><span>ชื่อ :</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="memb_name" runat="server" Style="text-align:center;"></asp:TextBox>
                </td>
                <td width="15%">
                    <div><span>นามสกุล :</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="memb_surname" runat="server" Style="text-align:center;" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div><span>บัตรประชาชน :</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="card_person" runat="server" Style="text-align:center;"></asp:TextBox>
                </td>
                <td width="15%">
                    <div><span>เบอร์โทรศัพท์ :</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="mem_telmobile" runat="server" Style="text-align:center;" ></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>