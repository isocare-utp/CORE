<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.ws_as_genrequest_education_to_promote_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>วันที่ทำรายการ:</span>
                    </div>
                </td>
                <td width="15%">
                    <asp:TextBox ID="operate_date" runat="server" ReadOnly="true" Style="text-align:center" BackColor="#DDDDDD" ></asp:TextBox>
                </td>
                <td width="10%">
                    <span>ปีประมวล:</span>
                </td>
                <td width="15%">
                    <asp:DropDownList ID="assist_year" runat="server" Style="text-align:center">
                    </asp:DropDownList>
                </td>               
            </tr>
            <tr>
                <td>
                    <span>สวัสดิการต้นทาง:</span>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="assisttype_code" runat="server" Enabled="False" BackColor="White"></asp:DropDownList>
                </td>
                 <td style="width: 18%">
                    <span>สวัสดิการรับโอนย้าย:  </span>
                </td>
                <td colspan="2">
                  <asp:DropDownList ID="assisttype_code2" runat="server" Enabled="False" BackColor="White"></asp:DropDownList> 
                </td>
            </tr>
           <%-- <tr>
                <td>
                    <span>ประเภทเงิน:</span>
                </td>
                <td>
                    <asp:DropDownList ID="moneytype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <span>ธนาคาร:</span>
                </td>
                <td>
                    <asp:DropDownList ID="expense_bank" runat="server" >
                    </asp:DropDownList>
                </td>
                <td>
                    <span>สาขา:</span>
                </td>
                <td>
                    <asp:DropDownList ID="expense_branch" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>--%>
           <%-- <tr>
                <td>
                    <span>รหัสบัญชี:</span>
                </td>
                <td>
                    <asp:DropDownList ID="tofrom_accid" runat="server" BackColor="#FFFF99" >
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td align = "center" colspan = "6">
                    <br />
                    <asp:Button ID="b_process" runat="server" Text="ดึงข้อมูลใบคำขอ" />  
              <%--      <asp:Button ID="b_save" runat="server" Text="ประมวลใบคำขอ" />  --%>
                </td>
            </tr>
        </table>
        <br /> 
        <div><u>รายการใบคำขอ</u></div>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%" colspan="2">
                    <asp:CheckBox ID="all_check" runat="server" Text=" เลือกทั้งหมด" />
                </td>
                <td width="7%">
                </td>
                <td width="33%">
                </td>
                <td width="15%">
                </td>
                <td width="13%">
                </td>
                <td width="17%">
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>

