<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_finslip_spc.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_finslip_spc" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postCalTax %>
    <%=postCalVat %>
    <%=postRefresh%>
    <%=postProtect %>
    <%=postInsertRow%>
    <%=postDeleteRow%>
    <%=postPrintTax %>
    <%=postPrintSlip %>
    <%=postItemtCalTax%>
    <%=postInitMember %>
    <%=postSumItemAmt %>
    <%=postSetItemDesc%>
    <%=postDefaultAccid %>
    <%=postItemDeleteRow %>
    <%=postBankBranch %>
    <%=postFilterItem %>
    <%=postInitBegin %>
    <%=postGetBankCode %>
    <%=postPrintRecPea%>
    <%=PostPrint%>
    <%=postInitMembgrp%>
    <%=postNewTaxSum%>
    <%=postNewVatSum%>
    
    <script type="text/javascript">

        function MenubarOpen() {
            Gcoop.OpenIFrame('675', '400', 'w_dlg_finconfrim.aspx');
        }
        function DwMainItemChanged(sender, rowNumber, columnName, newValue) {
            objDwMain.SetItem(rowNumber, columnName, newValue);
            objDwMain.AcceptText();
            var memb_flag = objDwMain.GetItem(1, "member_flag");
            var pay_recv_status = objDwMain.GetItem(1, "pay_recv_status");
            var cash_type = objDwMain.GetItem(1, "cash_type");

            if (columnName == "pay_recv_status") {
                Gcoop.GetEl("HColumName").value = columnName;
                Gcoop.GetEl("HStatus").value = newValue;
                postProtect();
            }
            else if (columnName == "member_flag") {
                //Gcoop.SetLastFocus("member_no");
                //Gcoop.Focus();
                postProtect();
            }
            else if (columnName == "member_no") {
                newValue = Gcoop.StringFormat(newValue, "00000000");
                objDwMain.SetItem(rowNumber, "member_no", newValue);
                Gcoop.GetEl("HdMemberNo").value = newValue;
                postInitMember();
            }
            else if (columnName == "membgroup_code") {
                Gcoop.GetEl("HdMembGrp").value = newValue;
                postInitMembgrp();
            }
            else if (columnName == "cash_type") {
                if (cash_type == "CHQ" && pay_recv_status == 1) {
                }
                Gcoop.GetEl("HColumName").value = newValue;
                Gcoop.GetEl("HStatus").value = pay_recv_status;
                postDefaultAccid();
            }
            else if (columnName == "tax_flag") {
                Gcoop.GetEl("HColumName").value = columnName;
                postRefresh();
            }
            else if (columnName == "tax_amt") {
                postNewTaxSum();
            }
            else if (columnName == "vat_amt") {
                postNewVatSum();
            }
            else if (columnName == "vat_flag") {
                postCalVat();
            }
            else if (columnName == "tax_code") {
                Gcoop.GetEl("HfRow").value = rowNumber;
                postRefresh();
            }
            else if (columnName == "from_bankcode") {
                objDwMain.AcceptText();
            }
            else if (columnName == "from_accno") {
                objDwMain.AcceptText();
                var bankbranch = "0" + newValue.substring(0, 3);
                objDwMain.SetItem(rowNumber, "from_branchcode", bankbranch);
                objDwMain.AcceptText();
                postRefresh();
            }
            else if (columnName == "tofrom_accid") {
                objDwMain.AcceptText();
                postGetBankCode();
            }
            else if (columnName == "bank_code") {
                alert(columnName + "  " + objDwMain.GetItem(rowNumber, "bank_code"));
                postBankBranch();
            }
        }

        function DwMainClick(sender, rowNumber, objectName) {
            if (objectName == "tax_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "tax_flag", 1, 0);
                var flag = objDwMain.GetItem(rowNumber, "tax_flag");
                Gcoop.GetEl("HTaxFlag").value = flag;
                Gcoop.GetEl("HColumName").value = objectName;
                objDwMain.AcceptText();
                postProtect();
            }
        }

        function DwMainButtonOnClicked(sender, rowNumber, buttonName) {
            objDwMain.AcceptText();
            var memb_flag = objDwMain.GetItem(1, "member_flag");
            var tax_flag = objDwMain.GetItem(1, "tax_flag");
            var cash_type = objDwMain.GetItem(1, "cash_type");
            var pay_recv_status = objDwMain.GetItem(1, "pay_recv_status");

            if (buttonName == "cb_bychq")
            { Gcoop.OpenDlg(610, 550, "w_dlg_accid.aspx", ""); }
            else if (buttonName == "cb_chq" && cash_type == "CHQ" && pay_recv_status == 1)
            { Gcoop.OpenIFrame(400, 150, "w_dlg_recvchq.aspx", ""); }
            else if (buttonName == "cb_member" && memb_flag == 1)
            { Gcoop.OpenDlg(700, 550, "w_dlg_fin_member_search.aspx", ""); }
            else if (buttonName == "cb_member" && memb_flag == 0)
            { Gcoop.OpenDlg(530, 500, "w_dlg_sl_extmember_search.aspx", ""); }
            else if (buttonName == "cb_member" && memb_flag == 4) {
               //Gcoop.OpenDlg(700, 550, "w_dlg_fin_member_search.aspx", ""); 
            }
            else if (buttonName == "b_accid") {
                Gcoop.OpenIFrame(370, 400, "w_dlg_recvpay_search1.aspx", "");
            }
        }
        // รับค่าจากการค้นหาสมาชิก
        function GetValueFromDlg(memberNo) {
            Gcoop.GetEl("HdMemberNo").value = memberNo;
            objDwMain.SetItem(1, "member_no", memberNo);
            objDwMain.AcceptText();
            postInitMember();
        }

        function GetValueFromDlgItemType(slipitemtype_code, item_desc) {
            var row = Gcoop.GetEl("HfRow").value;
            objDwItem.SetItem(row, "slipitemtype_code", slipitemtype_code);
            objDwItem.SetItem(row, "slipitem_desc", item_desc);
            objDwItem.AcceptText();
            postSetItemDesc();
        }

        function GetValueFromDlgItemTypeacc( slipitemtype_code ) {
            var row = Gcoop.GetEl("HfRow").value;
            objDwMain.SetItem(1, "tofrom_accid", slipitemtype_code);
            //objDwItem.SetItem(row, "slipitem_desc", item_desc);
            objDwMain.AcceptText();
            //alert(slipitemtype_code);

        }

        function DwItemChanged(sender, rowNumber, columnName, newValue) {
            objDwItem.SetItem(rowNumber, columnName, newValue);
            Gcoop.GetEl("HfRow").value = rowNumber;
            objDwMain.AcceptText();
            objDwItem.AcceptText();

            if (columnName == "itempay_amt") {
                var oldValue = objDwMain.GetItem(rowNumber, columnName);
                postCalVat();
                //postItemtCalTax();
            }
            else if (columnName == "slipitem_desc") {
                objDwMain.SetItem(rowNumber, "payment_desc", newValue);
            }
            else if (columnName == "slipitemtype_code") {
                var desc = objDwItem.GetItem(rowNumber, columnName);
                postSetItemDesc();
            } else if (columnName == "account_id") {
                postFilterItem();
            }
        }

        function ItemInsertRow() {
            objDwMain.AcceptText();
            objDwItem.AcceptText();
            objDwItem.InsertRow(0);
        }

        function DwItemButtonClicked(sender, rowNumber, buttonName) {
            if (buttonName == "b_item") 
            {
                Gcoop.GetEl("HfRow").value = rowNumber;
                Gcoop.OpenIFrame(370, 400, "w_dlg_recvpay_search.aspx", "");
            }
            else if (buttonName == "b_delete") 
            {
                Gcoop.GetEl("HfRow").value = rowNumber;
                postItemDeleteRow();
            }
           
        }

        function DwItemClick(sender, rowNumber, objectName) {
            for (var i = 1; i <= sender.RowCount(); i++) {
                sender.SelectRow(i, false);
            }
            sender.SelectRow(rowNumber, true);
            sender.SetRow(rowNumber);
            Gcoop.GetEl("HdDetailRow").value = rowNumber + "";

            Gcoop.GetEl("HfRow").value = rowNumber;
            if (objectName == "posttovc_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "posttovc_flag", 1, 0);
                Gcoop.CheckDw(objDwMain, 1, columnName, "posttovc_flag", 1, 0);
                objDwItem.AcceptText();
            }
            else if (objectName == "tax_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "tax_flag", 1, 0);
                objDwItem.AcceptText();
                postRefresh();
            }
            else if (objectName == "vat_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "vat_flag", 1, 0);
                objDwItem.AcceptText();
                postItemtCalTax();
            }
        }

        function DwTaxItemChange(sender, rowNumber, columnName, newValue) {
            objDwTax.SetItem(rowNumber, columnName, newValue);
            if (columnName == "taxpay_id") {
                Gcoop.GetEl("Htaxpay_id").value = newValue;
            }
            else if (columnName == "taxpay_addr") {
                Gcoop.GetEl("Htaxpay_addr").value = newValue;
            }
            else if (columnName == "taxpay_desc") {
                Gcoop.GetEl("Htaxpay_desc").value = newValue;
            }
            objDwTax.AcceptText();
            Gcoop.GetEl("HColumName").value = columnName;
            postRefresh();
        }

        function Validate() {
            objDwMain.AcceptText();
            objDwItem.AcceptText();
            objDwTax.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function MenubarNew() {
            if (confirm("ยืนยันการลบข้อมูล ??")) {
                window.location = Gcoop.GetUrl() + "Applications/app_finance/w_sheet_finslip_spc.aspx";
            }
        }

        function SetId(account_no, dateon_chq, bank_code, bank_branch) {
            objDwMain.SetItem(1, "account_no", account_no);
            objDwMain.SetItem(1, "dateon_chq", dateon_chq);
            objDwMain.SetItem(1, "bank_code", bank_code);
            objDwMain.SetItem(1, "bank_branch", bank_branch);
            objDwMain.AcceptText();
        }

        function GetFinSlipFromDlg(SlipNo, MemberNo) {
            Gcoop.GetEl("HdSlipNo").value = SlipNo;
            Gcoop.GetEl("HdMemberNo").value = MemberNo;
            Gcoop.GetEl("HDChkDeptFEE").value = "1";
            objDwMain.SetItem(1, "pay_recv_status", 1);
            Gcoop.GetEl("HStatus").value = "1";
            postInitMember();
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("Hslipno").value != "") {
//                if (Gcoop.GetEl("Hprintslip").value == "true") {
//                        if (confirm("ยืนยันการพิมพ์ใบสำคัญจ่าย / ใบเสร็จ")) {
//                            Gcoop.GetEl("Hprintslip").value = "false";
//                            postPrintSlip();
//                        }
//                }
//                    else {
//                        Gcoop.GetEl("Hprintslip").value = "false";
//                        if (Gcoop.GetEl("HClear").value == "true") {
//                            Gcoop.GetEl("HClear").value = "false";
//                            postInitBegin();
//                        }
//                    }
//                    Gcoop.GetEl("Hprintslip").value = "false";
//                }

//                if (Gcoop.GetEl("HprintTax").value == "true") {
//                    if (confirm("ยืนยันการพิมพ์ใบรับรองภาษี")) {
//                        Gcoop.GetEl("HprintTax").value = "false";
//                        postPrintTax();
//                    }
//                    Gcoop.GetEl("HprintTax").value = "false";
//                }
            }

            for (var i = 1; i <= objDwItem.RowCount(); i++) {
                objDwItem.SelectRow(i, false);
            }
            var rowNumber = Gcoop.GetEl("HdDetailRow").value;
            objDwItem.SelectRow(rowNumber, true);
            objDwItem.SetRow(rowNumber);

            if (Gcoop.GetEl("Hslipno").value != "" && Gcoop.GetEl("HdCoopid").value == "003001") {
                if (confirm("พิมพ์ใบเสร็จ?")) {
                    postPrintRecPea();
                } else {
                    Gcoop.GetEl("Hslipno").value = "";
                    //Gcoop.GetEl("HStatus").value = "";
                }
            }

            if (Gcoop.GetEl("Hslipno").value != "" && Gcoop.GetEl("HdCoopid").value == "008001" && Gcoop.GetEl("HStatus").value=="1") {
                if (confirm("พิมพ์ใบเสร็จ?")) {
                    postPrintRecPea();
                } else {
                    Gcoop.GetEl("Hslipno").value = "";
                    //Gcoop.GetEl("HStatus").value = "";
                }
            }
        }

        function OnClickInsertRow() {
            var memberno = objDwMain.GetItem(1, "member_no");
            postInsertRow();
        }

        function OnClickDeleteRow() {
            if (objDwItem.RowCount() > 0) {
                var currentRow = Gcoop.GetEl("HdDetailRow").value;
                var confirmText = "ยืนยันการลบแถวที่ " + currentRow;
                if (confirm(confirmText)) {
                    postDeleteRow();
                }
            }
            else {
                alert("ยังไม่มีการเพิ่มแถวสำหรับรายการ");
            }
        }

        function OnPostPrint() {
            var memberno = objDwMain.GetItem(1, "member_no");
            PostPrint();
        }

        //ประกาศ func ready ของ jquery
        $(function () {
            var COOP_ID = ($('#ctl00_TopBarControl1_LbUsername').text()).toString().split('@');

            if ($('select[name="cash_type_0"]').val() == "CHQ" && COOP_ID[1] == '008001') {
                //SetDisable('select[name="tofrom_accid_0"]', true);
                SetCSSBGColor('select[name="tofrom_accid_0"]', 'gold');
            }
            else {
                SetDisable('select[name="tofrom_accid_0"]', false);
                SetCSSBGColor('select[name="tofrom_accid_0"]', '');
            }

            $('body').keyup(function (e) {
                if (e.which == 114) {
                    Gcoop.OpenDlg('675', '400', 'w_dlg_finconfrim.aspx');
                }
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <input type="button" value="พิมพ์ใบเสร็จ" style="width: 80px" onclick="OnPostPrint()" />
    <%--<input type="button" value="พิมพ์ใบสำคัญจ่าย" style="width: 100px" onclick="OnPostPrint()" />--%>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_fin_slipspc"
        LibraryList="~/DataWindow/app_finance/finslip_spc.pbl" AutoRestoreContext="false"
        ClientScriptable="true" ClientEventButtonClicked="DwMainButtonOnClicked" AutoRestoreDataCache="True"
        ClientEventItemChanged="DwMainItemChanged" AutoSaveDataCacheAfterRetrieve="True"
        Width="720px" ClientEventClicked="DwMainClick">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="DwTax" runat="server" DataWindowObject="d_fin_taxdetail"
        LibraryList="~/DataWindow/app_finance/finslip_spc.pbl" ClientScriptable="true"
        Width="720px" ClientEventItemChanged="DwTaxItemChange" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
    </dw:WebDataWindowControl>
    <hr />
    <asp:Label ID="lb_slipdet" runat="server" Text="รายการ" ForeColor="#000099" Font-Strikeout="False"
        Font-Bold="True" Font-Underline="True" Font-Size="Large"></asp:Label>
    &nbsp; <span onclick="OnClickInsertRow()" style="cursor: pointer;">
        <asp:Label ID="LbInsert2" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="#006600" /></span> &nbsp;&nbsp;
    <span onclick="OnClickDeleteRow()" style="cursor: pointer;">
        <asp:Label ID="LbDel2" runat="server" Text="ลบแถว" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="Red" /></span>
    <dw:WebDataWindowControl ID="DwItem" runat="server" DataWindowObject="d_fin_slipspc_det"
        LibraryList="~/DataWindow/App_finance/finslip_spc.pbl" AutoRestoreContext="false"
        VerticalScrollBar="Auto" ClientScriptable="true" AutoRestoreDataCache="True"
        ClientEventButtonClicked="DwItemButtonClicked" AutoSaveDataCacheAfterRetrieve="True"
        RowsPerPage="10" ClientEventItemChanged="DwItemChanged" ClientFormatting="True"
        ClientEventClicked="DwItemClick">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HfRow" runat="server" />
    <asp:HiddenField ID="Hmembgroup" runat="server" />
    <asp:HiddenField ID="Haccrow" runat="server" />
    <asp:HiddenField ID="HVatFlag" runat="server" />
    <asp:HiddenField ID="HtaxAmt" runat="server" />
    <asp:HiddenField ID="HStatus" Value="9" runat="server" />
    <asp:HiddenField ID="HTaxFlag" runat="server" />
    <asp:HiddenField ID="HColumName" runat="server" />
    <asp:HiddenField ID="HDesc" runat="server" />
    <asp:HiddenField ID="Hprintslip" runat="server" Value="false" />
    <asp:HiddenField ID="Hslipno" runat="server" />
    <asp:HiddenField ID="Htaxpay_id" runat="server" />
    <asp:HiddenField ID="Htaxpay_addr" runat="server" />
    <asp:HiddenField ID="Htaxpay_desc" runat="server" />
    <asp:HiddenField ID="HprintTax" runat="server" />
    <asp:HiddenField ID="HdDetailRow" runat="server" />
    <asp:HiddenField ID="HClear" Value="false" runat="server" />
    <asp:HiddenField ID="HCashtype" Value="CSH" runat="server" />
    <asp:HiddenField ID="HAccid" runat="server" />
    <asp:HiddenField ID="Hmember_flag" Value="1" runat="server" />
    <asp:HiddenField ID="HdSlipNo" runat="server" />
    <asp:HiddenField ID="HdMemberNo" runat="server" />
    <asp:HiddenField ID="HdMembGrp" runat="server" />
    <asp:HiddenField ID="HDChkDeptFEE" Value="0" runat="server" />    
    <asp:HiddenField ID="HdCoopid" runat="server" />
</asp:Content>
