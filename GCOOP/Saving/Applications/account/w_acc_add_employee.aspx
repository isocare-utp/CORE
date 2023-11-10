<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_add_employee.aspx.cs" Inherits="Saving.Applications.account.w_acc_add_employee" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%=postNewClear%>
    <%=postMember%>
    <%=postEmployeeShow%>
    <style type="text/css">
        .style5
        {
            font-size: small;
            font-weight: bold;
        }
        .style6
        {
            font-size: small;
        }
        </style>

    <script type="text/javascript">

        function OnDwMainClick(s, r, c) {

        }

        function OnDwMainItemChange(s, r, c, v) {
            if (c == "member_no") {
                s.SetItem(r, c, v);
                s.AcceptText();
                postMember();
            }
        }

        function OnDwDetailClick(s, r, c, v) {
            var mem_no = "";
            mem_no = objdw_detail.GetItem(r, "member_no");
            Gcoop.GetEl("HdMemNo").value = mem_no;
            postEmployeeShow();
        }
        
        //Clear หน้าจอ
        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                postNewClear();
            }
        }
        //แก้ไขข้อมูล
        function EditData() {
            postEditData();
        }
             

        //ฟังก์ชันการเช็คค่าข้อมูลก่อน save
        function Validate() {
            try {
                var isconfirm = confirm("ยืนยันการบันทึกข้อมูล?");

                if (!isconfirm) {
                    return false;
                }

                var alertstr = "";
                var memb_no = objDw_master.GetItem(1, "member_no");
                var work_date = objDw_master.GetItem(1, "work_date");
                var prename_code = objDw_master.GetItem(1, "prename_code");
                var memb_name = objDw_master.GetItem(1, "memb_name");
                var memb_surname = objDw_master.GetItem(1, "memb_surname");
                var resign_status = objDw_master.GetItem(1, "resign_status");
                var resign_date = objDw_master.GetItem(1, "resign_date");

                if (memb_no == "" || memb_no == null) {
                    alertstr = alertstr + "กรุณากรอกเลขทะเบียนสมาชิก\n";
                }
                if (work_date == "" || work_date == null) {
                    alertstr = alertstr + "กรุณากรอกวันที่เริ่มงาน\n";
                }
                if (prename_code == "" || prename_code == null) {
                    alertstr = alertstr + "กรุณากรอกคำนำหน้าชื่อ\n";
                }
                if (memb_name == "" || memb_name == null) {
                    alertstr = alertstr + "กรุณากรอกชื่อ\n";
                }

                if (memb_surname == "" || memb_surname == null) {
                    alertstr = alertstr + "กรุณากรอกสกุล\n";
                }
                if (resign_status == null) {
                    alertstr = alertstr + "กรุณากรอกสถานะ\n";
                }
                if (resign_status == 1 && resign_date == null) {
                    alertstr = alertstr + "กรุณากรอกวันที่ลาออก\n";
                }
              
                if (alertstr == "") {
                    return true;
                    alert(objDw_master.Describe("Datawindow.Data.Xml"));

                }
                else {
                    alert(alertstr);
                    return false;
                }
            } catch (err) {
                alert(err);
                return false;
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width:100%;">
            <tr>
                <td class="style5" colspan="2">
                    รายละเอียดพนักงาน</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                    <asp:Panel ID="Panel1" runat="server" Height="135px" BorderStyle="Ridge">
                        <dw:WebDataWindowControl ID="Dw_master" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            DataWindowObject="d_acc_add_employee" LibraryList="~/DataWindow/account/acc_contuse_profit.pbl"
                            ClientEventClicked="OnDwMainClick" ClientFormatting="True" Height="155px" ClientEventItemChanged="OnDwMainItemChange">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                    <asp:Button ID="B_add" runat="server" OnClick="B_add_Click" Text="เพิ่มข้อมูล" Width="70px"
                        UseSubmitBehavior="False" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel2" runat="server" Width="496px">
                                                            <asp:Button ID="B_edit" runat="server" Text="แก้ไขข้อมูล" Width="70px" OnClick="B_edit_Click"
                                                            UseSubmitBehavior="False" />
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
        <table style="width: 100%;">
            
           
            
            <tr>
                <td>
                <asp:Panel ID="Panel3" runat="server" Height="300px" ScrollBars="Vertical" BorderStyle="Ridge" TabIndex="100">
                        <dw:WebDataWindowControl ID="dw_detail" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            DataWindowObject="d_acc_employee" LibraryList="~/DataWindow/account/acc_contuse_profit.pbl"
                            ClientEventClicked="OnDwDetailClick" ClientFormatting="True" Height="155px">
                        </dw:WebDataWindowControl>
                    </asp:Panel>

                    <asp:HiddenField ID="Hd_aistatus" runat="server" />
                    <asp:HiddenField ID="Hd_checkclear" runat="server" Value="false" />
                    <asp:HiddenField ID="Hd_accid_master" runat="server" />
                    <asp:HiddenField ID="HdMemNo" runat="server" />
  
                </td>
            </tr>
        </table>
    </p>
</asp:Content>

