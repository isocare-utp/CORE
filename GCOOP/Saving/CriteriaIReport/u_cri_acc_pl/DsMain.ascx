<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_acc_pl.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td colspan="1">
                    <div>
                        <span>รหัสงบการเงิน:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="moneysheet_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="1">
                    <div>
                        <span>แสดงเปอร์เซ็นต์:</span>
                    </div>
                </td>
                <td colspan="1">
                <div>
                    <asp:DropDownList ID="percent_status" runat="server">
                        <asp:ListItem Value="0"> ไม่แสดง</asp:ListItem>
                        <asp:ListItem Value="1"> แสดง</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </td>

                <td>
                    <div>
                    <span>การแสดงผล :</span>
                    </div>
                </td>
                <td >
                    <div>
                    <asp:DropDownList ID="show_all" runat="server">
                        <asp:ListItem Value="1"> ทุกรายการ</asp:ListItem>
                        <asp:ListItem Value="0"> เฉพาะมากว่า 0</asp:ListItem>
                    </asp:DropDownList>
                    </div>
                </td>
            </tr>
             <tr>
                <td colspan="1">
                    <div>
                    <span>จำนวนช่องแสดงผล :</span>
                    </div>
                </td>
                <td colspan="1">
                    <div>
                    <asp:DropDownList ID="total_show" runat="server">
                        <asp:ListItem Value="1"> 1 ช่อง</asp:ListItem>
                        <asp:ListItem Value="2"> 2 ช่อง</asp:ListItem>
                    </asp:DropDownList>
                    </div>
                </td>
                <td colspan="1">
                    <div>
                    <span>หมายเหตุ :</span>
                    </div>
                </td>
                <td colspan="1">
                    <div>
                    <asp:DropDownList ID="show_remark" runat="server">
                        <asp:ListItem Value="0">ไม่แสดง</asp:ListItem>
                        <asp:ListItem Value="1">แสดง</asp:ListItem>
                    </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="1">
                    <div>
                    <span>ข้อมูลที่ 1 :</span>
                    </div>
                </td>
                <td colspan="3">
                    <div>
                    <asp:DropDownList ID="data_1" runat="server">
                        <asp:ListItem Value="1"> ยอดสะสม</asp:ListItem>
                        <asp:ListItem Value="2"> ยอดประจำเดือน</asp:ListItem>
                    </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="1">
                    <div>
                    <span>ปี :</span>
                    </div>
                </td>
                <td colspan="1">
                    <div>
                    <asp:TextBox ID="year_1" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td colspan="1">
                    <div>
                    <span>เดือน :</span>
                    </div>
                </td>
                <td colspan="1">
                    <div>
                    <asp:DropDownList ID="month_1_1" runat="server">
                        <asp:ListItem Value="1"> มกราคม</asp:ListItem>
                        <asp:ListItem Value="2"> กุมภาพันธ์</asp:ListItem>
                        <asp:ListItem Value="3"> มีนาคม</asp:ListItem>
                        <asp:ListItem Value="4"> เมษายน</asp:ListItem>
                        <asp:ListItem Value="5"> พฤษภาคม</asp:ListItem>
                        <asp:ListItem Value="6"> มิถุนายน</asp:ListItem>
                        <asp:ListItem Value="7"> กรกฎาคม</asp:ListItem>
                        <asp:ListItem Value="8"> สิงหาคม</asp:ListItem>
                        <asp:ListItem Value="9"> กันยายน</asp:ListItem>
                        <asp:ListItem Value="10"> ตุลาคม</asp:ListItem>
                        <asp:ListItem Value="11"> พฤษจิกายน</asp:ListItem>
                        <asp:ListItem Value="12"> ธันวาคม</asp:ListItem>
                    </asp:DropDownList>
            <tr>
                <td colspan="1">
                    <div>
                    <span>ข้อมูลที่ 2 :</span>
                    </div>
                </td>
                <td colspan="3">
                    <div>
                    <asp:DropDownList ID="data_2" runat="server">
                        <asp:ListItem Value="1"> ยอดสะสม</asp:ListItem>
                        <asp:ListItem Value="2"> ยอดประจำเดือน</asp:ListItem>
                    </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="1">
                    <div>
                    <span>ปี :</span>
                    </div>
                </td>
                <td colspan="1">
                    <div>
                    <asp:TextBox ID="year_2" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td colspan="1">
                    <div>
                    <span>เดือน :</span>
                    </div>
                </td>
                <td colspan="1">
                    <div>
                    <asp:DropDownList ID="month_2_1" runat="server">
                        <asp:ListItem Value="1"> มกราคม</asp:ListItem>
                        <asp:ListItem Value="2"> กุมภาพันธ์</asp:ListItem>
                        <asp:ListItem Value="3"> มีนาคม</asp:ListItem>
                        <asp:ListItem Value="4"> เมษายน</asp:ListItem>
                        <asp:ListItem Value="5"> พฤษภาคม</asp:ListItem>
                        <asp:ListItem Value="6"> มิถุนายน</asp:ListItem>
                        <asp:ListItem Value="7"> กรกฎาคม</asp:ListItem>
                        <asp:ListItem Value="8"> สิงหาคม</asp:ListItem>
                        <asp:ListItem Value="9"> กันยายน</asp:ListItem>
                        <asp:ListItem Value="10"> ตุลาคม</asp:ListItem>
                        <asp:ListItem Value="11"> พฤษจิกายน</asp:ListItem>
                        <asp:ListItem Value="12"> ธันวาคม</asp:ListItem>
                    </asp:DropDownList>
                  </div>
                </td>
            </tr>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
