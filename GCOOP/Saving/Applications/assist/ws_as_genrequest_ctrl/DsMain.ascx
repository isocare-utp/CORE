<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.ws_as_genrequest_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="13%">
                    <div>
                        <span>วันที่ทำรายการ:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="work_date" runat="server" ReadOnly="true" Style="text-align:center" BackColor="#DDDDDD" ></asp:TextBox>
                </td>
                <td width="10%">
                    <span>ปีประมวล:</span>
                </td>
                <td width="13%">
                    <asp:DropDownList ID="process_year" runat="server" Style="text-align:center">
                    </asp:DropDownList>
                </td>
                <td width="15%">
                    <span>เดือนประมวล:</span>
                </td>
                <td width="15%">
                    <asp:DropDownList ID="process_month" runat="server" >
                        <asp:ListItem Value="01" >มกราคม</asp:ListItem>
                        <asp:ListItem Value="02" >กุมภาพันธ์</asp:ListItem>
                        <asp:ListItem Value="03" >มีนาคม</asp:ListItem>
                        <asp:ListItem Value="04" >เมษายน</asp:ListItem>
                        <asp:ListItem Value="05" >พฤษภาคม</asp:ListItem>
                        <asp:ListItem Value="06" >มิถุนายน</asp:ListItem>
                        <asp:ListItem Value="07" >กรกฎาคม</asp:ListItem>
                        <asp:ListItem Value="08" >สิงหาคม</asp:ListItem>
                        <asp:ListItem Value="09" >กันยายน</asp:ListItem>
                        <asp:ListItem Value="10" >ตุลาคม</asp:ListItem>
                        <asp:ListItem Value="11" >พฤศจิกายน</asp:ListItem>
                        <asp:ListItem Value="12" >ธันวาคม</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <span>ประเภทเงิน:</span>
                </td>
                <td>
                    <asp:DropDownList ID="moneytype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="10%">
                    <span>ประเภทบัญชีผู้รับโอน:</span>
                </td>
                <td width="13%">
                    <asp:DropDownList ID="trtype_code" runat="server" >
                        <asp:ListItem Value="DEP" Selected="True">บัญชีเงินฝาก</asp:ListItem>
                        <asp:ListItem Value="MAIN" Selected="True">บัญชีหลัก</asp:ListItem>
                        <asp:ListItem Value="DVAV1">บัญชีปันผล</asp:ListItem>
                        <asp:ListItem Value="SHR">บัญชีหุ้น</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <span>ประเภทบัญชีเงินฝาก:</span>
                </td>
                <td>
                    <asp:DropDownList ID="depttype_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <span>ประเภทสวัสดิการ:</span>
                </td>
                <td >
                    <asp:DropDownList ID="assisttype_code" runat="server" ></asp:DropDownList>
                </td>
                <td width="10%">
                    <div>
                        <span>เงื่อนไขการคำนวณ:</span>
                    </div>
                </td>
                <td width="13%">
                    <asp:TextBox ID="calculate_flag" runat="server" ReadOnly="true" BackColor="#DDDDDD" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <span>วันที่ใช้คำนวณสิทธิ์:</span>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="cal_date" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align = "center" colspan = "6">
                    <br />
                    <asp:Button ID="b_process" runat="server" Text="ดึงข้อมูล" />  
                </td>
            </tr>
        </table>
        <br /> 
        <div><u>รายการใบคำขอ</u></div>
        <table class="DataSourceFormView">
            <tr>
                <td width="35%">
                    <asp:CheckBox ID="all_check" runat="server" Text=" เลือกทั้งหมด" />
                </td>
                <td width="15%">
                    <span>ค้นหารายการไม่มีเลขบัญชี :</span>
                </td>
                <td width="15%">
                    <asp:DropDownList ID="search_account" runat="server">
                        <asp:ListItem Value="1" Text="รายการทั้งหมด"></asp:ListItem>
                        <asp:ListItem Value="2" Text="รายการไม่มีเลขบัญชี"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
