<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mb_chgdetail.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_mb_chgdetail" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <%=initJavaScript%>
    <%=jsPostMember%>
    <%=changeDistrict%>
    <%=newClear%>
    <%=jsGetTambol%>
    <%=jsInsertRow%>
    <%=jsInsertRowRemarkstat%>
    <%=jsSetData%>
    <%=jsCallRetry%>
    <%=getpicture %>
    <%=jsIdCard%>
    <%=jsGetShareBase%>
    <%=jsgetpicMember_no%>
    <%=setpausekeep_date %>

    <script type="text/JavaScript">

        function Validate() {
            objdw_main.AcceptText();
            objdw_detail.AcceptText();
            objdw_status.AcceptText();
            objdw_moneytr.AcceptText();
            objdw_remarkstat.AcceptText();
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            //            var periodbase_value = objdw_data1.GetItem(1, "periodbase_value");
            //            var periodshare_value = objdw_data1.GetItem(1, "periodshare_value");
            //            if (periodbase_value > periodshare_value) {
            //                alert("กรุณาปรับค่าหุ้น/เดือน ของสมาชิกท่านนี้ใหม่");
            //            }
            return true; // confirm("ยืนยันการบันทึกข้อมูล");

        }
        function showTabPage(tab) {
            var i = 1;
            var tabamount = 5;
            for (i = 1; i <= tabamount; i++) {
                document.getElementById("tab" + i).style.visibility = "hidden";
                document.getElementById("stab" + i).style.backgroundColor = "rgb(211,213,255)";
                if (i == tab) {
                    document.getElementById("tab" + i).style.visibility = "visible";
                    document.getElementById("stab" + i).style.backgroundColor = "rgb(200, 235, 255)";
                    Gcoop.GetEl("HiddenFieldTab").value = i + "";
                }
            }
        }
        function SheetLoadComplete() {
            var CurTab = Gcoop.ParseInt(Gcoop.GetEl("HiddenFieldTab").value);

            if (isNaN(CurTab)) {
                CurTab = 1;
            }
            showTabPage(CurTab);
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                // alert(Gcoop.GetEl("HdIsPostBack").value);
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                // alert(Gcoop.GetEl("HdIsPostBack").value);
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
        }

        function MenubarOpen() {
            Gcoop.OpenDlg('580', '590', 'w_dlg_member_search.aspx', '');
        }
        function MenubarNew() {
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            newClear();

        }
        function GetMemDetFromDlg(memberno) {
            objdw_main.SetItem(1, "member_no", memberno);
            objdw_main.AcceptText();
            Gcoop.GetEl("Hfmember_no").value = memberno;
            jsPostMember();
        }
        function ItemChangedMain(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "000000"));
                objdw_main.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objdw_main.GetItem(rowNumber, "member_no");
                jsPostMember();
            }

        }
        function ItemChangedData(sender, rowNumber, columnName, newValue) {
            if (columnName == "province_code") {
                objdw_detail.SetItem(rowNumber, columnName, newValue);
                objdw_detail.AcceptText();
                Gcoop.SetLastFocus("district_code_0");
                changeDistrict();
            }
            else if (columnName == "district_code") {
                objdw_detail.SetItem(rowNumber, columnName, newValue);
                objdw_detail.AcceptText();
                Gcoop.SetLastFocus("tambol_code_0");
                jsGetTambol();
            }
            else if (columnName == "birth_tdate") {
                objdw_detail.SetItem(rowNumber, columnName, newValue);
                objdw_detail.AcceptText();
                Gcoop.GetEl("Hbirth_tdate").value = newValue;
                Gcoop.SetLastFocus("birth_tdate_0");
                jsCallRetry();
            }
            else if (columnName == "card_person") {
                objdw_detail.SetItem(rowNumber, columnName, newValue);
                objdw_detail.AcceptText();
                Gcoop.SetLastFocus("card_person_0");
                jsIdCard();
            }
            else if (columnName == "salary_amount") {
                objdw_detail.SetItem(rowNumber, columnName, newValue);
                objdw_detail.AcceptText();
                //  Gcoop.SetLastFocus("salary_amount_0");
                jsGetShareBase();

            }
        }
        function checkStatus(s, r, c) {
            Gcoop.CheckDw(s, r, c, "klongtoon_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "transright_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "allowloan_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "sequest_divavg", 1, 0);
            Gcoop.CheckDw(s, r, c, "dividend_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "average_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "divavgshow_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "insurance_flag", 1, 0);
        }
        function itemdw_status(sender, rowNumber, columnName, newValue) {
            if (columnName == "appltype_code") {
                objdw_status.SetItem(rowNumber, columnName, newValue);
                objdw_status.AcceptText();
            }
            else if (columnName == "pausekeep_flag") {
                objdw_status.SetItem(rowNumber, columnName, newValue);
                objdw_status.AcceptText();
                setpausekeep_date();
            }
        }
        function OnInsert() {
            //            objdw_moneytr.InsertRow(objdw_moneytr.RowCount() + 1);
            //            var member_no = Gcoop.GetEl("Hfmember_no").value;
            //            alert(Gcoop.GetEl("Hfmember_no").value);
            //            objdw_moneytr.SetItem(objdw_moneytr.RowCount(), "member_no", member_no);
            //            objdw_moneytr.AcceptText();
            jsInsertRow();
        }


        function OnDeleteMoneytr(s, r, c) {
            if (c == "b_delete") {
                alert(c);
                var detail = "รหัส " + objdw_moneytr.GetItem(r, "moneytype_code");
                detail += " : " + objdw_moneytr.GetItem(r, "bank_accid");
                if (confirm("คุณต้องการลบรายการ " + detail + " ใช่หรือไม่?")) {
                    objdw_moneytr.DeleteRow(r);
                }
            }
            return 0;

        }
        // function OnDeleteRemarkstat(s, r, c) {
        //            if (c == "b_delete") {
        //                alert(c);
        //                var detail = "รหัส " + objdw_remarkstat.GetItem(r, "remarkstattype_code");
        //                if (confirm("คุณต้องการลบรายการ " + detail + " ใช่หรือไม่?")) {
        //                    objdw_remarkstat.DeleteRow(r);
        //                }
        //            }
        //            return 0;

        //        }

        function checkRemarkstat(s, r, c) {
            Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
            if (c == "operate_flag") {
                if (objdw_remarkstat.GetItem(r, "operate_flag") == 1) {
                    Gcoop.GetEl("Hrow").value = r + "";
                    jsSetData();
                }
            }
        }
        function OnUpLoad() {
            if (Gcoop.GetEl("Hfmember_no").value != "") {
                var member_no = Gcoop.GetEl("Hfmember_no").value;
                Gcoop.OpenDlg("570", "590", "w_dlg_picture.aspx", "?member=" + member_no);
                // getpicture();
            }
            else {
                alert("ไม่พบเลขสมาชิก");
            }

        }
        function GetShow() { jsgetpicMember_no(); }
        function Click_search(s, r, c) {
            if (c == "b_branch") {
                Gcoop.GetEl("HdRows").value = r;
                var bankcode = objdw_moneytr.GetItem(r, "bank_code");
                Gcoop.OpenDlg("580", "590", "w_dlg_search_bankbranch.aspx", "?bank_code=" + bankcode);
            }
        }

        function GetBankBranchFromDlg(branch_id, branch_name) {
            var row = Gcoop.GetEl("HdRows").value;
            objdw_moneytr.SetItem(row, "bank_branch", branch_id);
            objdw_moneytr.SetItem(row, "branch_name", branch_name);
            objdw_moneytr.AcceptText();
        }

        
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="Hbirth_tdate" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:HiddenField ID="HdRows" runat="server" />
    <asp:TextBox ID="TextDwmain" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="Textdwdata1" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="Textdwdata2" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="Textdwdata3" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="Textdwdata4" runat="server" Visible="False"></asp:TextBox>
    <asp:HiddenField ID="HiddenFieldTab" runat="server" />
    <asp:HiddenField ID="Hrow" runat="server" />
    <div>
        <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_mb_adjust_master"
            LibraryList="~/DataWindow/keeping/mb_chgdetail.pbl" ClientScriptable="True" ClientEventButtonClicked="MenubarOpen"
            AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
            ClientFormatting="True" ClientEventItemChanged="ItemChangedMain">
        </dw:WebDataWindowControl>
    </div>
    <table style="width: 100%;">
        <tr align="center">
            <td style="background-color: rgb(200, 235, 255); cursor: pointer;" id="stab1" onclick="showTabPage(1);"
                width="16.66%">
                ข้อมูลสมาชิก
            </td>
            <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab2" onclick="showTabPage(2);"
                width="16.66%">
                สถานะสมาชิก
            </td>
            <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab3" onclick="showTabPage(3);"
                width="16.66%">
                การเชื่อมโยงกับสหกรณ์
            </td>
            <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab4" onclick="showTabPage(4);"
                width="16.66%">
                หมายเหตุ-สถานะ
            </td>
            <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab5" onclick="showTabPage(5);"
                width="16.66%">
                รูปสมาชิก
            </td>
        </tr>
    </table>
    <table style="width: 100%; height: 400px;">
        <tr align="center">
            <td valign="top">
                <div id="tab1" style="visibility: visible; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_mb_adjust_mbdetail"
                        LibraryList="~/DataWindow/keeping/mb_chgdetail.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" ClientEventItemChanged="ItemChangedData">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab2" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_status" runat="server" DataWindowObject="d_mb_adjust_status"
                        LibraryList="~/DataWindow/keeping/mb_chgdetail.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" ClientEventClicked="checkStatus" ClientEventItemChanged="itemdw_status">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab3" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_moneytr" runat="server" DataWindowObject="d_mb_adjust_moneytr"
                        LibraryList="~/DataWindow/keeping/mb_chgdetail.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" ClientEventClicked="OnDeleteMoneytr" ClientEventButtonClicked="Click_search">
                    </dw:WebDataWindowControl>
                    <span class="linkSpan" onclick="OnInsert()" style="font-size: small; color: Green;
                        float: left">เพิ่มแถว</span>
                </div>
                <div id="tab4" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_remarkstat" runat="server" DataWindowObject="d_mb_adjust_remarkstat"
                        LibraryList="~/DataWindow/keeping/mb_chgdetail.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" ClientEventClicked="checkRemarkstat" ClientEventItemChanged="itemdw_status">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab5" style="visibility: hidden; position: absolute;">
                    <span class="linkSpan" onclick="OnUpLoad()">อัปโหลดรูปใหม่&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </span>
                    <asp:Image ID="Image1" runat="server" ImageAlign="Middle" Height="182px" Width="163px" />
                </div>
            </td>
        </tr>
        <%-- <tr>
            <td align="left">
                <div>
                  &nbsp;</div>
            </td>
        </tr>--%>
    </table>
</asp:Content>
