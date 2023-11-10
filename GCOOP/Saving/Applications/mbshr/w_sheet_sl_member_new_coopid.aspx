<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_member_new_coopid.aspx.cs" Inherits="Saving.Applications.mbshr.w_sheet_sl_member_new_coopid" %>

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
    <%=jsgetpicMember_no%>
    <%=newclear %>
    <%=CheckCoop %>
    <%=jsRunMemberNo%>
    <%=jsGetCurrDistrict %>
    <%=jsGetCurrPostcode %>
    <%=jsLinkAddress %>
    <%=jsChangmidgroupcontrol%>
    <%=jsChangmembsection %>
    <%=jsGainInsertRow %>
    <%=jsGainDeleteRow%>
    <%=jsRefreshCoop %>
    <%=jsChangSex %>
    <%=jsCheckNation %>
    <script type="text/javascript">
        function MenubarOpen() {
            Gcoop.OpenDlg("580", "720", "w_dlg_sl_member_new_search_coopid.aspx", "")
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
            else
                return false;
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
            var member_type = objdw_main.GetItem(1, "member_type");
            var membdatefix_date = objdw_main.GetItem(1, "membdatefix_date");
            var membtype_code = objdw_main.GetItem(1, "membtype_code");

            if (memb_no == "" || memb_no == null) { alertstr = alertstr + "- กรุณากรอกเลขทะเบียน\n"; }
            if (memb_name == "" || memb_name == null) { alertstr = alertstr + "- กรุณากรอกชื่อผู้สมัคร\n"; }
            if (memb_surname == "" || memb_surname == null) { alertstr = alertstr + "- กรุณากรอกนามสกุลผู้สมัคร\n"; }
            if (membgroup_code == "" || membgroup_code == null) { alertstr = alertstr + "- กรุณากำหนดสังกัดให้กับสมาชิก\n"; }
            if (membdatefix_date == "" || membdatefix_date == null) { alertstr = alertstr + "- กรุณากรอกวันที่เป็นสมาชิก\n"; }
            if (membtype_code == "" || membtype_code == null) { alertstr = alertstr + "- กรุณากรอกประเภทสมาชิกย่อย\n"; }

            if (member_type == 1) {
                if (salary_amount == "" || salary_amount == null
            ) { alertstr = alertstr + "- กรุณากรอกเงินเดือนให้กับผู้สมัคร\n"; }
            }

            if (alertstr == "") {

                return true;

            } else {
                alert(alertstr);
                return false;
            }
        }

        function MenubarNew() {
            //            Gcoop.SetLastFocus("member_no_0");
            //            Gcoop.Focus();
            newclear();
        }

        function GetValueFromDlgCoopId(docno, coop_id) {
            Gcoop.GetEl("Hdoc_no").value = docno;
            Gcoop.GetEl("Hdcoop_id").value = coop_id;
            jsGetdocno();
        }

        function GetValueFromDlg(memberno) {
            objdw_main.SetItem(1, "member_ref", memberno);
        }

       
//        function GetValueFromDlg(strvalue) {

//            if (Gcoop.GetEl("HidBtnClick").value == "btn_membsearch") {
//                objdw_main.SetItem(1, "member_ref", strvalue);
//                Gcoop.GetEl("HidBtnClick").value = "Hdoc_no"
//            }
//            else {
//                Gcoop.GetEl("Hdoc_no").value = strvalue;
//                jsGetdocno();
//            }
//        }

        function itemChange(sender, rowNumber, columnName, newValue) {
            if (columnName == "province_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "province_code_0";
                changeDistrict();

            }
            else if (columnName == "district_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "district_code_0";
                jsGetPostcode();

            }
            if (columnName == "currprovince_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "currprovince_code_0";
                jsGetCurrDistrict();

            }
            else if (columnName == "curramphur_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "curramphur_code_0";
                jsGetCurrPostcode();

            }
            else if (columnName == "salary_amount") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "salary_amount_0";
                jsSalary();

            }
            else if (columnName == "card_person") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "card_person_0";
                jsIdCard();

            }
            else if (columnName == "membgroup_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidgroup").value = newValue;
                Gcoop.GetEl("Hidlast_focus").value = "membgroup_code_0";
                jsmembgroup_code();

            }

            else if (columnName == "birth_tdate") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "birth_tdate_0";
                jsCallRetry();

            }
            else if (columnName == "appltype_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("HidColname").value = "appltype_code";
                Gcoop.GetEl("Hidlast_focus").value = "appltype_code_0";
                jsRefresh();

            }
            else if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "00000000"));
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidmem_no").value = Gcoop.StringFormat(newValue, "00000000");
                Gcoop.GetEl("Hidlast_focus").value = "member_no_0";
                jsMemberNo();

            }
            else if (columnName == "membsection_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidmembsection").value = newValue;
                Gcoop.GetEl("Hidlast_focus").value = "membsection_code_0";
                jsChangmidgroupcontrol();

            }
            //            else if (columnName == "membgroup_code_1") {
            //                objdw_main.SetItem(rowNumber, columnName, newValue);
            //                objdw_main.AcceptText();
            //                jsChangmembsection();
            //            }
            else if (columnName == "select_coop") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "select_coop_0";
                jsRefreshCoop();

            }
            else if (columnName == "membtype_code") {
                objdw_main.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "00"));
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "membtype_code_0";
                // Gcoop.GetEl("Hidgroup").value = newValue;
                //jsChangmembtype();
                //                alert("membtype_code");
                //                Gcoop.GetEl("Hidlast_focus").value = "prename_code_0";
            }
            else if (columnName == "prename_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                // Gcoop.GetEl("Hidgroup").value = newValue;
                Gcoop.GetEl("Hidlast_focus").value = "prename_code_0";
                jsChangSex();

            }
            else if (columnName == "nationality") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                // Gcoop.GetEl("Hidgroup").value = newValue;
                Gcoop.GetEl("Hidlast_focus").value = "nationality_0";
                jsCheckNation();

            }
           
        }

        function OnKeyUpEnd(e) {
            if (e.keyCode == "115") {
                TryOpenDlgDeptWith();
            } else if (e.keyCode == "123") {
            }
        }
        function dw_mainClick(s, r, c) {
            if (c == "check_coop") {
                Gcoop.CheckDw(s, r, c, "check_coop", 1, 0);
                CheckCoop();

            }
            if (c == "cre_membno") {
                Gcoop.CheckDw(s, r, c, "check_coop", 1, 0);
                jsRunMemberNo();

            }
            if (c == "blinkaddress") {
                Gcoop.CheckDw(s, r, c, "blinkaddress", 1, 0);

                jsLinkAddress();

            }
        }
        function b_searchClick(s, r, c) {
            if (c == "b_group") {
                Gcoop.OpenDlg('580', '590', 'w_dlg_search_memgroup.aspx', '');

            }
            if (c == "btn_membsearch") {
                Gcoop.OpenDlg('580', '590', 'w_dlg_sl_member_search.aspx', '');
                Gcoop.GetEl("HidBtnClick").value = "btn_membsearch";
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
            Gcoop.GetEl("Hidlast_focus").value = "periodshare_value_0";
            if (c == "periodshare_value") {
                objdw_data.SetItem(r, "periodshare_value", v);
                objdw_data.AcceptText();
                Gcoop.GetEl("H_periodshare_value").value = v;
                Gcoop.GetEl("Max_periodshare_value").value = v;
                var periodbase_value = objdw_data.GetItem(r, "periodbase_value");
                var max_periodbase_value = objdw_data.GetItem(r, "maxshare_value");
                if (v % 10 != 0) {
                    alert("มูลค่าหุ้นต่อ 1 หน่วย ไม่ตรงตามฐาน");
                    jsChanegValue();
                }
                else {
                    if (v < periodbase_value) {
                        alert(v + "<" + "  " + periodbase_value + "มูลค่าหุ้นต่ำกว่าหุ้นตามฐาน");
                        jsChanegValue();
                    } else {
                        if (v > max_periodbase_value) {
                            alert(v + ">" + "  " + max_periodbase_value + "มูลค่าหุ้นสูงกว่าหุ้นตามฐาน");
                            jsChanegValue();
                        }
                    }
                }

            }
        }
        function OnUpLoad() {
            if (Gcoop.GetEl("Hidmem_no").value != "") {
                var member_no = Gcoop.GetEl("Hidmem_no").value;
                Gcoop.OpenDlg("570", "590", "w_dlg_picture.aspx", "?member=" + member_no);

            }
            else {
                alert("ไม่พบเลขสมาชิก");
            }
        }
        function GetShow() { jsgetpicMember_no(); }


        function OnDwGainClick(s, r, c) {
            if (c == "b_add") {
                Gcoop.CheckDw(s, r, c, "b_add", 0, 1);
                Gcoop.GetEl("Hidlast_focus").value = "gain_name_0";
                jsGainInsertRow();
            }
            if (c == "b_del") {
                Gcoop.CheckDw(s, r, c, "b_del", 0, 1);
                if (confirm("คุณต้องการลบรายการแถว " + r + " ใช่หรือไม่?")) {
                    Gcoop.GetEl("HdChkRowDel").value = r;
                    jsGainDeleteRow();
                }
            }

        }
        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {

            }
            
            Gcoop.SetLastFocus(Gcoop.GetEl("Hidlast_focus").value);
            Gcoop.Focus();

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HidColname" runat="server" />
    <asp:HiddenField ID="Hidmem_no" runat="server" />
    <asp:HiddenField ID="Hdoc_no" runat="server" />
    <asp:HiddenField ID="Hidgroup" runat="server" />
    <asp:HiddenField ID="Hidlast_focus" runat="server" />
    <asp:HiddenField ID="Hidcheckgain" runat="server" />
    <asp:HiddenField ID="Hidmembsection" runat="server" />
    <asp:HiddenField ID="HidBtnClick" runat="server" />
    <asp:HiddenField ID="H_periodshare_value" runat="server" />
    <asp:HiddenField ID="Max_periodshare_value" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:HiddenField ID="HdChkRowDel" runat="server" Value="false" />
    <asp:HiddenField ID="Hdcoop_id" runat="server" Value="false" />
    <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
    <dw:WebDataWindowControl ID="dw_main" runat="server" ClientScriptable="True" DataWindowObject="d_sl_reqapplmain_design2"
        LibraryList="~/DataWindow/mbshr/sl_member_new.pbl" ClientEventItemChanged="itemChange"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientEvents="True" ClientFormatting="True" TabIndex="1" ClientEventClicked="dw_mainClick"
        ClientEventButtonClicked="b_searchClick">
    </dw:WebDataWindowControl>
    <table style="width: 90%;">
        <tr>
            <td valign="top" style="width: 40%;">
                <br />
                <asp:Label ID="lbl_dw_share" runat="server" Text="การส่งหุ้น" CssClass="font14px"></asp:Label>
                <br />
                <dw:WebDataWindowControl ID="dw_data" runat="server" DataWindowObject="d_sl_reqapplshare"
                    LibraryList="~/DataWindow/mbshr/sl_member_new.pbl" ClientScriptable="True" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" TabIndex="400"
                    ClientEventClicked="dw_dataClick" ClientFormatting="True" ClientEventItemChanged="ChangedValue">
                </dw:WebDataWindowControl>
            </td>
            <!--<td valign="middle" style="width: 50%;" align="center">
                <span class="linkSpan" onclick="OnUpLoad()">อัปโหลดรูป </span>
                <asp:Image ID="Image1" runat="server" ImageAlign="Middle" Height="182px" Width="163px" />
            </td>-->
        </tr>
    </table>
    <dw:WebDataWindowControl ID="dw_gain" runat="server" ClientScriptable="True" DataWindowObject="d_mb_gain_detail_mbnew"
        LibraryList="~/DataWindow/mbshr/sl_member_new.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEvents="True"
        ClientFormatting="True" TabIndex="500" ClientEventClicked="OnDwGainClick">
    </dw:WebDataWindowControl>
</asp:Content>
