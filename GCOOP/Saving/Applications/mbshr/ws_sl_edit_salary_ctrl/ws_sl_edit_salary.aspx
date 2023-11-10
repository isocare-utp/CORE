<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_edit_salary.aspx.cs" Inherits="Saving.Applications.mbshr.ws_sl_edit_salary_ctrl.ws_sl_edit_salary" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function Validate() {
            var new_salary = dsMain.GetItem(0, "new_salary");
            if (new_salary == 0) {
                alert("กรุณากรอกเงินเดือนให้กับผู้สมัคร");
                return false;
            } else {
                return true;
            }
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenIFrame2("630", "610", "w_dlg_sl_member_search_lite.aspx", "");
            }
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMember();
            } else if (c == "new_salary") {
                var salary_amount = dsMain.GetItem(0, "salary_amount");
                if (salary_amount > v) {
                    alert("เงินเดือนที่เปลี่ยนแปลง ทำให้ค่าหุ้นที่ต้องส่งต่อเดือนลดลง \nถ้าต้องการปรับเงินค่าหุ้นที่ส่งประจำเดือน กรุณาไปปรับปรุงที่การส่งหุ้นประจำเดือน");
                }

                PostSalary();
            }
        }

        function GetValueFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno);
            PostMember();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
