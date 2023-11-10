<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.ws_as_approve_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="9%" valign="top">
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td width="14%" valign="top">
                    <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                </td>
                <td width="3%" valign="top">
                    <asp:Button ID="b_searchMem" runat="server" Text="..." Width="20px" />
                </td>

                <td width="17%" valign="top">
                    <div>
                        <span>ประเภทสวัสดิการ:</span>
                    </div>
                </td>
                <td width="30%" valign="top">
                    <asp:DropDownList ID="assisttype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="10%" valign="top">
                    <asp:Button ID="b_search" runat="server" Text="ดึงข้อมูล" Width="60px" />
                </td>
                <td width="10%" valign="top">
                 <asp:Button ID="b_clear" runat="server" Text="ล้างข้อมูล" Width="60px" />
                 </td>
            </tr>
            <tr>
             <td width="13%" valign="top">
                    <div>
                        <span>ตั้งแต่ประเภทการจ่าย:</span>
                    </div>
                </td>
                <td colspan="2" width="30%" valign="top">
                   <asp:DropDownList ID="assistpay_code1" runat="server">
                    </asp:DropDownList>
                </td>
   
                <td width="13%" valign="top">
                    <div>
                        <span>ถึงประเภทการจ่าย:</span>
                    </div>
                </td>
                <td width="30%" valign="top">
                    <asp:DropDownList ID="assistpay_code2" runat="server">
                    </asp:DropDownList>
                </td>
               
            </tr>
             <tr>
                <td width="9%" valign="top">
                    <div>
                        <span>มติ:</span>
                    </div>
                </td>
                <td colspan="4" width="100%" valign="top">
                    <asp:TextBox ID="conclusion_no" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="17%" valign="top">
                    <div>
                        <span>วันที่อนุมัติ:</span>
                    </div>
                </td>
                <td colspan="2" style="width:100%">
                    <asp:TextBox ID="conclusion_date" runat="server"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td colspan="6">
                    <hr />
                    <br />
                </td>
            </tr>
        </table>
        <table class="DataSourceFormView">
            <tr>
                <td width="35%">
                    <asp:CheckBox ID="select_check" runat="server" Text=" เลือกทั้งหมด" />
                </td>
                <td width="10%">
                    <span>การเรียง :</span>
                </td>
                <td width="15%">
                    <asp:DropDownList ID="sort_type" runat="server">
                     <asp:ListItem Value="0" Text="--กรุณาเลือก--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="เงินเดือน"></asp:ListItem>
                        <asp:ListItem Value="2" Text="วันที่ส่งไปรษณีย์"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <%--<td width="20%">
                    <asp:DropDownList ID="req_status" runat="server">
                        <asp:ListItem Value="8" Text="รออนุมัติ"></asp:ListItem>
                        <asp:ListItem Value="1" Text="อนุมัติ"></asp:ListItem>
                        <asp:ListItem Value="0" Text="ไม่อนุมัติ"></asp:ListItem>
                    </asp:DropDownList>
                </td>--%>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
