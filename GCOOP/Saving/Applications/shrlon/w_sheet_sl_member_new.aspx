<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_member_new.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_member_new" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=changeDistrict%>
    <%=jsSalary%>
    <%=jsIdCard%>
    <%=jsCallRetry%>
    <%=jsRefresh%>
    <%=jsGetPostcode%>
    <%=jsmembgroup_code%>
    <%=jsChanegValue %>
    <%=jsMemberNo %>
    <%=jsGetdocno%>
<%=newclear %>
    <script type="text/javascript">
        function MenubarOpen() {
            Gcoop.OpenDlg("570", "590", "w_dlg_sl_member_new_search.aspx", "")
        }
        function Validate() {
            var result = false;
            result = ValidateForm();
            if (result) {

                objdw_main.AcceptText();
                objdw_data.AcceptText();
                // return confirm("ยืนยันการบันทึกข้อมูล");
                return true;

            }
            else return false;
        }

        //                    function MenubarSave() {
        //                        if (ValidateForm()) {
        //                             objdw_main.AcceptText();
        //                              objdw_data.AcceptText();
        //                              objdw_main.Update();
        //                              
        //                         }
        //                      }

        function ValidateForm() {
            var alertstr = "";
            var memb_no = objdw_main.GetItem(1, "member_no");
            var memb_name = objdw_main.GetItem(1, "memb_name");
            var memb_surname = objdw_main.GetItem(1, "memb_surname");
            var membgroup_code = objdw_main.GetItem(1, "membgroup_code");
            var salary_amount = objdw_main.GetItem(1, "salary_amount");

            if (memb_no == "" || memb_no == null) { alertstr = alertstr + "- กรุณากรอกเลขทะเบียน\n"; }
            if (memb_name == "" || memb_name == null) { alertstr = alertstr + "- กรุณากรอกชื่อผู้สมัคร\n"; }
            if (memb_surname == "" || memb_surname == null) { alertstr = alertstr + "- กรุณากรอกนามสกุลผู้สมัคร\n"; }
            if (membgroup_code == "" || membgroup_code == null) { alertstr = alertstr + "- กรุณากำหนดสังกัดให้กับสมาชิก\n"; }
            if (salary_amount == "" || salary_amount == null) { alertstr = alertstr + "- กรุณาป้อนเงินเดือนให้กับผู้สมัคร\n"; }

            if (alertstr == "") {

                return true;

            } else {
                alert(alertstr);
                return false;
            }
        }

        function MenubarNew() {
            newclear();
        }
        function GetValueFromDlg(strvalue) {
        Gcoop.GetEl("Hdoc_no").value = strvalue;
          jsGetdocno();
        }

        function itemChange(sender, rowNumber, columnName, newValue) {
            if (columnName == "province_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                changeDistrict();
            }
            else if (columnName == "district_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsGetPostcode();
            }
            else if (columnName == "salary_amount") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsSalary();
            }
            else if (columnName == "card_person") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsIdCard();
            }
            else if (columnName == "membgroup_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidgroup").value = newValue;
                jsmembgroup_code();
            }

            else if (columnName == "birth_tdate") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsCallRetry();
            }
            else if (columnName == "appltype_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("HidColname").value = "appltype_code";
                jsRefresh();
            }
            else if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "00000000"));
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidmem_no").value = Gcoop.StringFormat(newValue, "00000000");
                jsMemberNo();
            }

        }

        function OnKeyUpEnd(e) {
            if (e.keyCode == "115") {
                TryOpenDlgDeptWith();
            } else if (e.keyCode == "123") {
            }
        }
        function dw_mainClick(s, r, c) {
            if (c == "membdatefix_flag") {
                Gcoop.CheckDw(s, r, c, "membdatefix_flag", 0, 1);

                jsRefresh();

            }

        }
        function b_searchClick(s, r, c) {
            if (c == "b_group") {
                Gcoop.OpenDlg('580', '590', 'w_dlg_search_memgroup.aspx', '');
            }

        }
        function GetMemGroupFromDlg(membgroup_code) {
            objdw_main.SetItem(1, "membgroup_code", membgroup_code);
            objdw_main.AcceptText();
            Gcoop.GetEl("Hidgroup").value = membgroup_code;
            jsmembgroup_code();
        }
        function dw_dataClick(s, r, c) {
            if (c == "recv_shrstatus") {
                Gcoop.CheckDw(s, r, c, "recv_shrstatus", 0, 1);
            }
        }
        function ChangedValue(s, r, c, v) {
            if (c == "periodshare_value") {

                objdw_data.SetItem(r, "periodshare_value", v);
                objdw_data.AcceptText();
                var periodbase_value = objdw_data.GetItem(r, "periodbase_value");
                if (v < periodbase_value) {
                    alert("มูลค่าหุ้นต่ำกว่าหุ้นตามฐาน");
                    jsChanegValue();
                }
                else if (v > periodbase_value) {

                    alert("มูลค่าหุ้นมากกว่าหุ้นตามฐาน");

                }
            }
        }
        function OnUpLoad() {
            if (Gcoop.GetEl("Hidmem_no").value != "") {
                var member_no = Gcoop.GetEl("Hidmem_no").value
                Gcoop.OpenDlg("570", "590", "w_dlg_picture.aspx", "?member=" + member_no)
            }
            else {
                alert("ไม่พบเลขสมาชิก");
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HidColname" runat="server" />
    <asp:HiddenField ID="Hidmem_no" runat="server" />
     <asp:HiddenField ID="Hdoc_no" runat="server" />
    <asp:HiddenField ID="Hidgroup" runat="server" />
    <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
    <dw:WebDataWindowControl ID="dw_main" runat="server" ClientScriptable="True" DataWindowObject="d_sl_reqapplmain_design2"
        LibraryList="~/DataWindow/shrlon/sl_member_new.pbl"
        ClientEventItemChanged="itemChange" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientEvents="True" OnBeginUpdate="dw_main_BeginUpdate"
        OnEndUpdate="dw_main_EndUpdate" ClientFormatting="True" TabIndex="1" ClientEventClicked="dw_mainClick"
        ClientEventButtonClicked="b_searchClick">
    </dw:WebDataWindowControl>
    <table style="width: 90%;">
        <tr>
            <td valign="top" style="width: 40%;">
                <br />
                <asp:Label ID="lbl_dw_share" runat="server" Text="การส่งหุ้น" CssClass="font14px"></asp:Label>
                <br />
                <dw:WebDataWindowControl ID="dw_data" runat="server" DataWindowObject="d_sl_reqapplshare"
                    LibraryList="~/DataWindow/shrlon/sl_member_new.pbl"
                    ClientScriptable="True" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" OnBeginUpdate="dw_data_BeginUpdate" OnEndUpdate="dw_data_EndUpdate"
                    TabIndex="400" ClientEventClicked="dw_dataClick" ClientFormatting="True" ClientEventItemChanged="ChangedValue">
                </dw:WebDataWindowControl>
            </td>
            <td valign="middle" style="width: 50%;" align="center">
                <span class="linkSpan" onclick="OnUpLoad()">อัปโหลดรูป </span>
            </td>
        </tr>
    </table>
</asp:Content>
