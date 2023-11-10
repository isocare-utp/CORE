<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.ws_as_assistpaygroup_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="10%" valign="top">
                    <div>
                        <span>ประเภทเงิน:</span>
                    </div>
                </td>
                <td colspan="2" width="14%" valign="top">
                    <asp:DropDownList ID="moneytype_code" runat="server">
                    </asp:DropDownList> 
                </td>
                <td width="10%" valign="top">
                    <div>
                        <span>ประเภทสวัสดิการ:</span>
                    </div>
                </td>
                <td width="20%" valign="top">
                    <asp:DropDownList ID="assisttype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="20%" valign="top">
                    <asp:Button ID="b_search" runat="server" Text="ดึงข้อมูล" Width="60px" />   
  
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
               <%-- <td width="7%" valign="top">
                    <div>
                        <span>วันที่คำขอ:</span>
                    </div>
                </td>
                <td width="14%" valign="top">
                    <asp:TextBox ID="req_date" runat="server" Style="text-align: left; "></asp:TextBox> 
                </td>--%>
                <td colspan="2" width="35%" valign="top">
                    <asp:Button ID="b_pay" runat="server" Text="ประมวลจ่ายสวัสดิการ" Width="170px" BackColor="Blue" Font-Bold="true" Font-Size="16px" ForeColor="White"/>
                </td>
        </tr>
        </table>

        <br />
        <table class="DataSourceFormView">
            <tr>
                <td width="15%" colspan="2">
                    <asp:CheckBox ID="select_check" runat="server" Text=" เลือกทั้งหมด" />
                </td>                             
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
