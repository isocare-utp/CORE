<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_edit_dept_namethaieng.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_edit_dept_namethaieng" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postAccountNo%>
    <%=postAddRow%>
    <%=postChangeTran%>
    <%=postExpenseAcc%>
    <%=postChangeDwTabCon%>
    <%=postMemberNo%>
    <%=postChangeTranDeptAcc%>
    <%=FilterBankBranch%>
    <%=FilterProvince%>
    <%=FilterDistrict%>
    <%=postPostOffice%>
    <%=CheckCoop%>
    <%=setCoopname%>
    <%=MemberNoSearch%>
    <%=postBankAcc%>
    <script type="text/javascript">

        var openDlgBy = "";

        function GetMemberNoFromDlg(Coopid, memberNo) {
            objDwTabMem.SetItem(1, "member_no", memberNo);
            Gcoop.GetEl("HfCoopid").value = Coopid + "";
            Gcoop.GetEl("HfDlg").value = "MemberDlg";
            objDwTabMem.AcceptText();
            setCoopname();
        }

        function InsertRowCoDept() {
            objDwMain.AcceptText();
            var accno = objDwMain.GetItem(1, "deptaccount_no");
            if (accno == null) {
                alert("กรุณาเลือกรายการเลขที่บัญชีก่อน");
                return;
            }
            else {
                postAddRow();
            }
        }

        function MenubarNew() {
            window.location = Gcoop.GetUrl() + "Applications/ap_deposit/w_sheet_dp_edit_dept.aspx";
        }

        function MenubarOpen() {
            openDlgBy = "Menubar";
            Gcoop.OpenIFrame(900, 600, "w_dlg_dp_account_search.aspx", "");
        }

        function NewAccountNo(memcoopid, accNo) {
            if (openDlgBy == "Menubar") {
                objDwMain.SetItem(1, "deptaccount_no", Gcoop.Trim(accNo));
                Gcoop.GetEl("HfCoopid").value = memcoopid + "";
                Gcoop.GetEl("HfDlg").value = "AccountDlg";
                objDwMain.AcceptText();
                setCoopname();
            }
            else {
                objDwTabCond.SetItem(1, "tran_deptacc_no", Gcoop.Trim(accNo));
                objDwTabCond.AcceptText();
                postChangeTranDeptAcc();
            }
        }

        function OnDwMainItemChanged(s, r, c, v) {
            if (c == "deptaccount_no") {
                objDwMain.SetItem(r, "deptaccount_no", v);
                objDwMain.AcceptText();
                postAccountNo();
            }
        }        
        function OnDwTabEditCoButtonClicked(sender, rowNumber, buttonName) {           
            if (buttonName == "b_delete") {             
                objDwTabEditCo.DeleteRow(rowNumber);
            }          
        }

        function OnDwTabEditCoItemChanged(sender, row, columnName, newValue) {
            if (columnName == "province") {
                Gcoop.GetEl("HdRowEditCo").value = row + "";
                objDwTabEditCo.SetItem(row, columnName, newValue);
                objDwTabEditCo.AcceptText();
                Gcoop.GetEl("HdProvinve").value = newValue
                FilterProvince();
            }
            else if (columnName == "district") {
                objDwTabEditCo.SetItem(row, columnName, newValue);
                objDwTabEditCo.AcceptText();
                Gcoop.GetEl("HdDistrict").value = newValue
                FilterDistrict();
            }
            else if (columnName == "ref_no") {
                Gcoop.GetEl("HdRowMemCo").value = row;
                objDwTabEditCo.SetItem(row, columnName, newValue);
                objDwTabEditCo.AcceptText();
                Gcoop.GetEl("HdRefNo").value = newValue
                MemberNoSearch();
            } 
        }

        function OnDwTabCondItemChanged(sender, row, columnName, newValue) {
            if (columnName == "spcint_rate_status") {
                objDwTabCond.SetItem(row, columnName, newValue);
                objDwTabCond.AcceptText();
                postChangeDwTabCon();
            }

            else if (columnName == "monthintpay_meth") {
                var montInt1 = objDwTabCond.GetItem(row, columnName);
                objDwTabCond.SetItem(row, columnName, newValue);
                var montInt = objDwTabCond.GetItem(row, columnName);
                if (montInt == 3) {
                    var deptType = objDwMain.GetItem(1, "depttype_code");
                    if (deptType == "10" || deptType == "11") {
                        alert("รายการนี้แยกได้เฉพาะ ประจำเท่านั้น");
                        setTimeout("objDwTabCond.SetItem(" + row + ", '" + columnName + "', '" + montInt1 + "')", 500);
                    }
                    else {
                        objDwTabCond.AcceptText();
                        postChangeDwTabCon();
                    }
                }
                else {
                    objDwTabCond.AcceptText();
                    postChangeDwTabCon();
                }
            }

            else if (columnName == "taxspcrate_status") {
                objDwTabCond.SetItem(row, columnName, newValue);
                objDwTabCond.AcceptText();
                postChangeDwTabCon();
            }

            else if (columnName == "tran_deptacc_no") {
                var montInt = objDwTabCond.GetItem(row, "monthintpay_meth");
                if (montInt == 5) {
                    objDwTabCond.SetItem(row, columnName, newValue);
                    objDwTabCond.AcceptText();
                    postPostOffice();
                }
                else {
                    objDwTabCond.SetItem(row, columnName, newValue);
                    objDwTabCond.AcceptText();
                    postChangeTranDeptAcc();
                }
            }

            else if (columnName == "bank_code") {
                objDwTabCond.SetItem(row, columnName, newValue);
                objDwTabCond.AcceptText();
                FilterBankBranch();
            }

            else if (columnName == "f_tax_rate") {
                objDwTabCond.SetItem(row, columnName, newValue);
                var taxRate = objDwTabCond.GetItem(row, columnName);
                taxRate = taxRate / 100;
                setTimeout("objDwTabCond.SetItem(" + row + ", '" + columnName + "', '" + taxRate + "')", 500);
                objDwTabCond.AcceptText();
            }

            else if (columnName == "spcint_rate") {
                objDwTabCond.SetItem(row, columnName, newValue);
                var spcIntRate = objDwTabCond.GetItem(row, columnName);
                spcIntRate = spcIntRate / 100;
                setTimeout("objDwTabCond.SetItem(" + row + ", '" + columnName + "', '" + spcIntRate + "')", 500);
                objDwTabCond.AcceptText();
            }
        }

        function OnDwTabCondButtonClicked(sender, rowNumber, buttonName) {
            if (buttonName == "b_accno") {
                openDlgBy = "DwTabCondClick";
                Gcoop.OpenDlg(610, 550, "w_dlg_dp_account_search.aspx", "");
            }
        }

        function OnDwTabMemItemChanged(sender, row, columnName, newValue) {
            if (columnName == "member_no") {
                objDwTabMem.SetItem(row, columnName, newValue);
                postMemberNo();
                objDwTabMem.AcceptText();
            }
        }

        function OnDwTabMemButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_memsearch") {
                Gcoop.OpenDlg(610, 550, "w_dlg_dp_member_search.aspx", "?coopid=" + Gcoop.GetEl("HfCoopid").value);
            }
        }

        function OnDwTabTranItemChanged(sender, row, columnName, newValue) {
            if (columnName == "expense_code") {
                objDwTabTran.SetItem(row, columnName, newValue);
                objDwTabTran.AcceptText();
                postChangeTran();
            }
            else if (columnName == "expense_accno") {
                objDwTabTran.SetItem(row, columnName, newValue);
                objDwTabTran.AcceptText();
                postExpenseAcc();
            }
            return 0;
        }
        function OnDwTabAtmItemChanged(sender, row, columnName, newValue) {
            if (columnName == "bank_accid") {
                objDwTabAtm.SetItem(row, columnName, newValue);
                objDwTabAtm.AcceptText();
                postBankAcc();
            }
            return 0;
        }
        function SheetLoadComplete() {
            var tab = Gcoop.ParseInt(Gcoop.GetEl("HdCurrentTab").value);
            ShowTabPage2(tab);
        }

        function ShowTabPage2(tab) {
            var tabamount = 5;
            for (i = 1; i <= tabamount; i++) {
                document.getElementById("tab_" + i).style.visibility = "hidden";
                document.getElementById("stab_" + i).className = "tabTypeTdDefault";
                if (i == tab) {
                    document.getElementById("tab_" + i).style.visibility = "visible";
                    document.getElementById("stab_" + i).className = "tabTypeTdSelected";
                    Gcoop.GetEl("HdCurrentTab").value = i + "";
                }
            }
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function OnDwListCoopClick(s, r, c) {
            if (c == "cross_coopflag") {
                Gcoop.CheckDw(s, r, c, "cross_coopflag", 1, 0);
                CheckCoop();
            }
        }
        function OnDwListCoopItemChanged(s, r, c, v) {
            if (c == "dddwcoopname") {
                s.SetItem(r, c, v);
                s.AcceptText();
                var coopid = s.GetItem(r, "dddwcoopname");
                Gcoop.GetEl("HfCoopid").value = coopid + "";
                objDwMain.SetItem(r, "slipcoop_id", coopid);
            }
        }

    </script>

    <style type="text/css">
        .tabTypeDefault
        {
            width: 100%;
            border-spacing: 2px;
        }
        .tabTypeTdDefault
        {
            width: 20%;
            height: 45px;
            font-family: Tahoma, Sans-Serif, Times;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            vertical-align: middle;
            color: #777777;
            border: solid 1px #55A9CD;
            background-color: rgb(200,235,255);
            cursor: pointer;
        }
        .tabTypeTdSelected
        {
            width: 20%;
            height: 45px;
            font-family: Tahoma, Sans-Serif, Times;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            vertical-align: middle;
            color: #660066;
            border: solid 1px #77CBEF;
            background-color: #76EFFF;
            cursor: pointer;
            text-decoration: underline;
        }
        .tabTypeTdDefault:hover
        {
            color: #882288;
            border: solid 1px #77CBEF;
            background-color: #98FFFF;
        }
        .tabTypeTdSelected:hover
        {
            color: #882288;
            border: solid 1px #77CBEF;
            background-color: #98FFFF;
        }
        .tabTableDetail
        {
            width: 99%;
        }
        .tabTableDetail td
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server" Text=""></asp:Literal>
    <asp:Literal ID="LtCurrentTab" runat="server"></asp:Literal>
    <br />
    <dw:WebDataWindowControl ID="DwListCoop" runat="server" DataWindowObject="d_dp_dept_cooplist"
            LibraryList="~/DataWindow/ap_deposit/cm_constant_config.pbl" ClientScriptable="True" AutoSaveDataCacheAfterRetrieve="True"
            AutoRestoreDataCache="True" ClientEventItemChanged="OnDwListCoopItemChanged"
            ClientEventItemError="OnError" AutoRestoreContext="False" ClientEventClicked="OnDwListCoopClick"
            ClientFormatting="True">
        </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dept_edit_deptmaster_namethaieng"
        LibraryList="~/DataWindow/ap_deposit/dp_edit_dept.pbl" ClientScriptable="True"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientEventItemChanged="OnDwMainItemChanged">
    </dw:WebDataWindowControl>
    <table class="tabTypeDefault">
        <tr>
            <td class="tabTypeTdSelected" id="stab_1" onclick="ShowTabPage2(1);">
                ข้อมูลสมาชิก
            </td>
            <td class="tabTypeTdDefault" id="stab_2" onclick="ShowTabPage2(2);">
                ผู้ฝากร่วม
            </td>
            <td class="tabTypeTdDefault" id="stab_3" onclick="ShowTabPage2(3);">
                เงื่อนไข
            </td>
            <td class="tabTypeTdDefault" id="stab_4" onclick="ShowTabPage2(4);">
                การโอนเงิน
            </td>
            <td class="tabTypeTdDefault" id="stab_5" onclick="ShowTabPage2(5);">
                จับคู่บัญชีธนาคารเพื่อ ATM
            </td>
        </tr>
    </table>
    <table class="tabTableDetail">
        <tr>
            <td style="height: 200px;" valign="top">
                <div id="tab_1" style="visibility: visible; position: absolute;">
                    <dw:WebDataWindowControl ID="DwTabMem" runat="server" DataWindowObject="d_dept_edit_mem"
                        LibraryList="~/DataWindow/ap_deposit/dp_edit_dept.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientEventButtonClicked="OnDwTabMemButtonClick" ClientEventItemChanged="OnDwTabMemItemChanged">
                    </dw:WebDataWindowControl>
                    <br />
                    <br />
                    <dw:WebDataWindowControl ID="DwTabAddress" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" DataWindowObject="d_db_mbdetail_address"
                        LibraryList="~/DataWindow/ap_deposit/dp_edit_dept.pbl" ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_2" style="visibility: hidden; position: absolute;">
                    <<
                    <asp:Label ID="LbChequeList" runat="server" Font-Bold="True" Font-Names="Tahoma"
                        Font-Size="14px"></asp:Label>
                    &nbsp; &nbsp; <a href="#" onclick="InsertRowCoDept()">
                        <asp:Label ID="Label1" runat="server" Text="เพิ่มแถว" CssClass="linkInsertRow"></asp:Label></a>
                    
                    <asp:Panel ID="Panel2" runat="server">
                        <dw:WebDataWindowControl ID="DwTabEditCo" runat="server" LibraryList="~/DataWindow/ap_deposit/dp_edit_dept.pbl"
                            AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                            ClientScriptable="True" HorizontalScrollBar="None" Visible="True" DataWindowObject="d_dp_edit_codeposit_dlg"
                            RowsPerPage="1" ClientEventItemChanged="OnDwTabEditCoItemChanged" ClientEventButtonClicked="OnDwTabEditCoButtonClicked">
                            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                            </PageNavigationBarSettings>
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </div>
                <div id="tab_3" style="visibility: hidden; position: absolute;">
                    <asp:Panel ID="Panel3" runat="server">
                        <dw:WebDataWindowControl ID="DwTabCond" runat="server" DataWindowObject="d_dept_edit_condition"
                            LibraryList="~/DataWindow/ap_deposit/dp_edit_dept.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" HorizontalScrollBar="None"
                            ClientEventItemChanged="OnDwTabCondItemChanged" ClientScriptable="True" ClientEventClicked="aa" ClientEventButtonClicked="OnDwTabCondButtonClicked" ClientFormatting="False">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </div>
                <div id="tab_4" style="visibility: hidden; position: absolute;">
                    <asp:Panel ID="Panel4" runat="server">
                        <dw:WebDataWindowControl ID="DwTabTran" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" DataWindowObject="d_dept_edit_tranfrom_other"
                            LibraryList="~/DataWindow/ap_deposit/dp_edit_dept.pbl" ClientScriptable="True"
                            ClientEventItemChanged="OnDwTabTranItemChanged" ClientFormatting="False">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </div>
                <div id="tab_5" style="visibility: hidden; position: absolute;">
                    <asp:Panel ID="Panel5" runat="server">
                        <dw:WebDataWindowControl ID="DwTabAtm" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" DataWindowObject="d_dept_edit_tranfrom_atmbank"
                            LibraryList="~/DataWindow/ap_deposit/dp_edit_dept.pbl" ClientScriptable="True"
                            ClientEventItemChanged="OnDwTabAtmItemChanged" ClientFormatting="False">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HSelect" runat="server" Value="01" />
    <asp:HiddenField ID="HdCurrentTab" runat="server" Value="1"/>
    <asp:HiddenField ID="HdDeptAccNo" runat="server" />
    <asp:HiddenField ID="HdMemcoopDlg" runat="server" />
    <asp:HiddenField ID="HdCoopidDlg" runat="server" />
    <asp:HiddenField ID="HdRowEditCo" runat="server" />
    <asp:HiddenField ID="HdRowMemCo" runat="server" />
    <asp:HiddenField ID="HfCoopid" runat="server" />
    <asp:HiddenField ID="HfDlg" runat="server" />
    <asp:HiddenField ID="HdRefNo" runat="server" />
    <asp:HiddenField ID="HdProvinve" runat="server" />
    <asp:HiddenField ID="HdDistrict" runat="server" />
</asp:Content>
