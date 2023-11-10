<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mb_member_loan_audit.aspx.cs" Inherits="Saving.Applications.mbshr.w_sheet_mb_member_loan_audit" %>

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
    <%=jsChanegValue%>
    <%=jsGetCurrTambol%>
    <%=jsGetCurrDistrict%>
    <script type="text/JavaScript">

        function Validate() {
            objdw_main.AcceptText();
            objdw_status.AcceptText();
            objdw_moneytr.AcceptText();

            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
           
            return true; 

        }
        function showTabPage(tab) {
            var i = 1;
            var tabamount = 2;
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
               Gcoop.SetLastFocus("member_no_0");
               Gcoop.Focus();
               var member_no = objdw_main.GetItem(1, "member_no");
               objdw_main.SetItem(1, "member_no",member_no); 
            }

        }

        function MenubarOpen() {
            Gcoop.OpenDlg('580', '590', 'w_dlg_sl_member_search.aspx', '');
        }
        function MenubarNew() {
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            newClear();

        }
        function GetValueFromDlg(memberno) {
            objdw_main.SetItem(1, "member_no", memberno);
            objdw_main.AcceptText();
            Gcoop.GetEl("Hfmember_no").value = memberno;
            jsPostMember();
        }
        function ItemChangedMain(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "00000000"));
                objdw_main.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objdw_main.GetItem(rowNumber, "member_no");
                jsPostMember();
            }
            else if (columnName == "province_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.SetLastFocus("amphur_code");
                changeDistrict();
            }
            else if (columnName == "amphur_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.SetLastFocus("tambol_code_0");
                jsGetTambol();
            }
            else if (columnName == "currprovince_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.SetLastFocus("curramphur_code");
                jsGetCurrDistrict();
            }
            else if (columnName == "curramphur_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.SetLastFocus("currtambol_code");
                jsGetCurrTambol();
            }
            else if (columnName == "birth_tdate") {
                objdw_main.SetItem(1, "birth_tdate", newValue);
                objdw_main.AcceptText();
                objdw_main.SetItem(1, "birth_date", Gcoop.ToEngDate(newValue));
                objdw_main.AcceptText();

//                objdw_main.SetItem(rowNumber, columnName, newValue);
//                objdw_main.AcceptText();
                Gcoop.GetEl("Hbirth_tdate").value = newValue;
                Gcoop.SetLastFocus("birth_tdate_0");
                jsCallRetry();
            }
            else if (columnName == "card_person") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.SetLastFocus("card_person_0");
                jsIdCard();
            }
            else if (columnName == "periodshare_value") {

                objdw_main.SetItem(rowNumber, "periodshare_value", newValue);
                objdw_main.AcceptText();
//                Gcoop.GetEl("H_periodshare_value").value = newValue;
//                Gcoop.GetEl("Max_periodshare_value").value = newValue;
                var periodbase_value = objdw_main.GetItem(rowNumber, "periodbase_value");
                var maxshare_value = objdw_main.GetItem(rowNumber, "maxshare_value");
                if (newValue < periodbase_value) {
                    alert(newValue + "<" + "  " + periodbase_value + "มูลค่าหุ้นต่ำกว่าหุ้นตามฐาน");
                 
                    jsChanegValue();
                }
                if (newValue > maxshare_value) {
                    alert(newValue + ">" + "  " + maxshare_value + "มูลค่าหุ้นสูงกว่าหุ้นตามฐาน");
                   
                    jsChanegValue();
                }

                               

            }
            else if (columnName == "salary_amount") {


                objdw_main.SetItem(rowNumber, "salary_amount", newValue);
                objdw_main.AcceptText();
                //  alert(newValue);
                jsGetShareBase();

            }

            if (Gcoop.GetEl("HdIsPostBack").value == "false") {
                SheetLoadComplete();
            }
        }
        function ItemChangedData(sender, rowNumber, columnName, newValue) {

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

        //        function SheetLoadComplete() {
        //            if (Gcoop.GetEl("HdIsPostBack").value != "true") {

        //                Gcoop.SetLastFocus("member_no_0");
        //                Gcoop.Focus();
        //            }
        //        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="Hbirth_tdate" runat="server" />
    <asp:HiddenField ID="H_periodshare_value" runat="server" />
    <asp:HiddenField ID="Max_periodshare_value" runat="server" />
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
        <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_mb_audit_membmaster"
            LibraryList="~/DataWindow/mbshr/mb_member_loan_audit.pbl" ClientScriptable="True"
            ClientEventButtonClicked="MenubarOpen" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientEventItemChanged="ItemChangedMain">
        </dw:WebDataWindowControl>
    </div>
<%--    <table style="width: 100%;">
        <tr align="center">
            <%--<td style="background-color: rgb(200, 235, 255); cursor: pointer;" id="stab1" onclick="showTabPage(1);"
                width="16.66%">
                หมายเหตุ-สถานะ
            </td>
            <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab2" onclick="showTabPage(2);"
                width="16.66%">
                การเชื่อมโยงกับสหกรณ์
            </td>
        </tr>
    </table>--%>
  <%--  <table style="width: 100%; height: 400px;">
        <tr align="center">
            <td valign="top">
                <%--<div id="tab1" style="visibility: visible; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_status" runat="server" DataWindowObject="d_mb_audit_membremarkstat"
                        LibraryList="~/DataWindow/mbshr/mb_member_loan_audit.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" ClientEventItemChanged="ItemChangedData">
                    </dw:WebDataWindowControl>
                </div>
               
                    <dw:WebDataWindowControl ID="dw_moneytr" runat="server" DataWindowObject="d_mb_audit_membmoneytr"
                        LibraryList="~/DataWindow/mbshr/mb_member_loan_audit.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" ClientEventClicked="OnDeleteMoneytr" ClientEventItemChanged="itemdw_status">
                    </dw:WebDataWindowControl>
                
            </td>
        </tr>
        <%-- <tr>
            <td align="left">
                <div>
                  &nbsp;</div>
            </td>
        </tr>
    </table>--%>
</asp:Content>
