<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCriteria.ascx.cs" Inherits="Saving.Applications.assist.dlg.wd_as_search_request_old_ctrl.DsCriteria" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 650px;">
            <tr>
                <td width="14%">
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="14%">
                    <div>
                        <span>ชื่อ:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="memb_name" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="14%">
                    <div>
                        <span>นามสกุล:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="memb_surname" runat="server"></asp:TextBox>
                    </div>
                </td>

                
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเภทสวัสดิการ:</span>
                    </div>
                </td>
               <td colspan="3">
                    <div>
                        <asp:DropDownList ID="assisttype_code" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ปีสวัสดิการ:</span>
                    </div>
                </td>
               <td>
                    <div>
                        <asp:DropDownList ID="assist_year" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
               
            </tr>
            <tr>
                
                <td>
                    <div>
                        <span>ตั้งแต่ประเภทการจ่าย:</span>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                        <asp:DropDownList ID="assistpay_code1" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ถึงประเภทการจ่าย:</span>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                        <asp:DropDownList ID="assistpay_code2" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
