<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_set_salary_employee.aspx.cs" Inherits="Saving.Applications.account.w_acc_set_salary_employee" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=RetrieveList%>
    <%=jsPostDeleteRow%>
    <%=jsPostGetAllEmp%>

    <script type="text/javascript">

        function Validate() {
            var isconfirm = confirm("ยืนยันการบันทึกข้อมูล ?");

            if (!isconfirm) {
                return false;
            }

            var rowcount = objDwList.RowCount();
            var alert = "";

            for (var i = 1; i <= rowcount; i++) {
                var accbudgetgroup_typ = objDwList.GetItem(i, "accbudgetgroup_typ");
                var budget_detail = objDwList.GetItem(i, "budget_detail");
                var account_budget = objDwList.GetItem(i, "account_budget");
                var account_id_list = objDwList.GetItem(i, "account_id_list");
                var budget_type = objDwList.GetItem(i, "budget_type");

                if (!(accbudgetgroup_typ != "" && accbudgetgroup_typ != null && budget_detail != "" && budget_detail != null)) {
                    alert = "กรุณาระบุข้อมูลให้ครบถ้วน";
                    break;
                }
            }

            if (alert != "") {
                confirm(alert);
                return false;
            }
            else {
                return true;
            }
        }

        function OnDwMainButtonClicked(sender, rowNumber, buttonName) {
            if (buttonName == "cb_ok") {
                var salary_year = 0;
                try {
                    salary_year = objDwMain.GetItem(1, "salary_year");
                } catch (err) { bgYear = 0; }
                if (salary_year == 0 || salary_year == null) {
                    alert("กรุณาเลือกปี");
                }
                else {
                    Gcoop.GetEl("HdRetrive").value = "True";
                    RetrieveList();
                }
            }
        }

      
        function OnDwListButtonClick(sender, row, bName) {
            if (bName == "b_del") {
                var member_no = objDwList.GetItem(row, "member_no");

                if (member_no == "" || member_no == null) {
                    Gcoop.GetEl("Hdrow").value = row + "";
                    jsPostDeleteRow();
                } else {
                    var isConfirm = confirm("ต้องการลบข้อมูลเงินเดือนของสมาชิก " + member_no + " ใช่หรือไม่ ?");
                    if (isConfirm) {
                        Gcoop.GetEl("Hdrow").value = row + "";
                        jsPostDeleteRow();
                    }
                }
            }        
            return 0;
        }

        function GetAccount(acc_id) {
            Gcoop.GetEl("Hdacclist").value = acc_id;
            jsPostGetAccount();
        }

        function OnGetAllBudget() {
            if (Gcoop.GetEl("HdRetrive").value == "True") {
                jsPostGetAllEmp();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table width="100%">
        <tr>
            <td align="left">
                <asp:Label ID="Label1" runat="server" Text="บันทึกเงินเดือนพนักงาน" Font-Bold="True" Font-Names="Tahoma"
                    Font-Size="16px" Font-Underline="True" />
            </td>
        </tr>
    </table>
    <br />
    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_acc_set_year_salary"
        LibraryList="~/DataWindow/account/acc_contuse_profit.pbl" ClientFormatting="True" ClientEventButtonClicked="OnDwMainButtonClicked">
    </dw:WebDataWindowControl>
    <br />
    <asp:Label ID="Label3" runat="server" Text="รายละเอียดรายการ" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="14px" Font-Underline="True" /><br />
    <br />
    <span onclick="OnGetAllBudget()" style="cursor: pointer; margin-left: 80%;">
        <asp:Label ID="Label5" runat="server" Text="ดึงข้อมูลพนักงานทั้งหมด" Font-Bold="False"
            Font-Names="Tahoma" Font-Size="14px" Font-Underline="True" ForeColor="Blue" Font-Strikeout="False" />
    </span>
    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" ScrollBars="Auto" Height="360px"
        Width="750px">
        <dw:WebDataWindowControl ID="DwList" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_acc_emp_salary"
            LibraryList="~/DataWindow/account/acc_contuse_profit.pbl" ClientFormatting="True"
            Height="360px" Width="750px" ClientEventButtonClicked="OnDwListButtonClick">
        </dw:WebDataWindowControl>
    </asp:Panel>
    <asp:HiddenField ID="HdRetrive" runat="server" />
    <asp:HiddenField ID="Hdrow" runat="server" />
    <asp:HiddenField ID="Hdacclist" runat="server" />
    <asp:HiddenField ID="Hdyear" runat="server" />
    <asp:HiddenField ID="HdrowSelect" runat="server" Value="1" />
</asp:Content>

