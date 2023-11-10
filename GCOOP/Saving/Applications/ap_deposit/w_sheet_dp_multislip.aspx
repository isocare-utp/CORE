<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_multislip.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_multislip" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postDeptAccountNo%>
    <%=postInsertRowCash%>
    <%=postDeleteRowCash%>
    <%=postInsertRowCheque%>
    <%=postDeleteRowCheque%>
    <%=postBankCode%>
    <%=postBankBranchCode%>
    <%=postChequeType%>
    <%=postPost%>
    <%=newClear%>
    <style type="text/css">
        .test
        {
            background: #EBCBC7;
            width: 99%;
            height: 66px;
            text-align: center;
            color: #990000;
            font-weight: bold;
            vertical-align: middle;
            border: 1px solid BB0000;
        }
        .linkInsertRow
        {
            font-family: Tahoma, Sans-Serif;
            font-weight: bold;
            font-size: 12px;
            cursor: pointer;
            color: #005599;
            text-align: left;
            text-decoration: underline;
        }
        .newStyle1
        {
            font-family: Tahoma;
            font-size: small;
            text-decoration: underline;
            color: #FF0000;
            font-weight: bold;
        }
        .newStyle2
        {
            font-family: Tahoma;
            font-size: small;
            color: #FF3300;
        }
        .newStyle3
        {
            font-family: Tahoma;
            font-size: small;
            font-weight: bold;
        }
        .style1
        {
            font-size: small;
            font-weight: bold;
            text-align: left;
        }
        .newStyle4
        {
            font-family: Tahoma;
            font-size: x-small;
        }
        .style2
        {
            font-family: Tahoma;
            font-size: small;
        }
        .style3
        {
            text-align: left;
        }
        .blueBorder
        {
            border: 1px solid #99CCFF;
            vertical-align: top;
            height: 100px;
        }
    </style>

    <script type="text/javascript">
    function CnvNumber(num) {
        if (IsNum(num)) {
            return parseFloat(num);
        }
        return 0;
    }

    function GetDlgBankAndBranch(sheetRow, bankCode, bankDesc, branchCode, branchDesc) {
        sheetRow = Gcoop.ParseInt(sheetRow);
        objDwCheque.SetItem(sheetRow, "bank_code", bankCode);
        objDwCheque.AcceptText();

        objDwCheque.SetItem(sheetRow, "bank_name", bankDesc);
        objDwCheque.AcceptText();

        objDwCheque.SetItem(sheetRow, "branch_code", branchCode);
        objDwCheque.AcceptText();

        objDwCheque.SetItem(sheetRow, "branch_name", branchDesc);
        objDwCheque.AcceptText();
    }
    
    function MenubarNew() {
        if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
            Gcoop.SetLastFocus("deptaccount_no_0");
            newClear();
        }
    }
    
    function MenubarOpen() {
        Gcoop.OpenDlg(610, 550, "w_dlg_dp_account_search.aspx", "");
    }

    function NewAccountNo(accNo) {
        objDwMain.SetItem(1, "deptaccount_no", Gcoop.Trim(accNo));
        objDwMain.AcceptText();
        postNewAccount();
    }
    
    function OnClickDeleteRowCash(){
        if(objDwCash.RowCount() > 0){
            var currentRow = Gcoop.GetEl("HdDwCashRow").value;
            if(confirm("ยืนยันการลบแถวที่ " + currentRow )){
                postDeleteRowCash();
            }
        } else {
            alert("ยังไม่มีการเพิ่มแถวสำหรับรายการเงินสด");
        }
    }
    
    function OnClickDeleteRowCheque(){
        if(objDwCheque.RowCount() > 0){
            var currentRow = Gcoop.GetEl("HdDwChequeRow").value;
            var checkNo = "";
            try{
                checkNo = Gcoop.Trim(objDwCheque.GetItem(currentRow, "cheque_no"));
            }catch(err){ }
            var confirmText = checkNo == "" || checkNo == null ? "ยืนยันการลบแถวที่ " + currentRow : "ยืนยันการลบเช็คเลขที่ " + checkNo;
            if(confirm(confirmText)){
                postDeleteRowCheque();
            }
        } else {
            alert("ยังไม่มีการเพิ่มแถวสำหรับรายการเช็ค");
        }
    }
    
    function OnClickInsertRowCash() {
        var member_no = objDwMain.GetItem(1, "member_no");
        if (member_no == "" || member_no == null) {
            alert("กรุณาเลือกรายกาบัญชี");
            return;
        }
        postInsertRowCash();
    }

    function OnClickInsertRowCheque() {
        var member_no = objDwMain.GetItem(1, "member_no");
        if (member_no == "" || member_no == null) {
            alert("กรุณากรอกเลขบัญชีก่อน");
            return;
        }
        postInsertRowCheque();
    }
    
    function OnClickTextBox1(){
        Gcoop.SetLastFocus("clt00$ContentPlace$TextBox1");
        Gcoop.Focus("clt00$ContentPlace$TextBox1");
    }
    
    function OnDwCashButtonClicked(sender, row, name) {
        if (name == "b_delete") {
            Gcoop.GetEl("HdDwCashRow").value = row + "";
            if (confirm("ยืนยันการลบรายการแถวที่ " + row)) {
                postDeleteRowCash();
            }
        } return 0;
    }
    
    function OnDwCashClicked(s, r, c){
        if(r > 0){
            Gcoop.GetEl("HdDwCashRow").value = r + "";
        }
    }
    
    function OnDwCashItemChanged(s, r, c, v){
        if(r > 0){
            Gcoop.GetEl("HdDwCashRow").value = r + "";
            s.SetItem(r, c, v);
            s.AcceptText();
            postPost();
        }
    }
    
    function OnDwChequeButtonClicked(sender, row, name) {
        if (name == "b_delete") {
            var detail = objDwCheque.GetItem(row, "product_name");
            if (confirm("คุณต้องการลบรายการ " + row + " ใช่หรือไม่?")) {
                objDwCheque.SetItem(row, name);
                objDwCheque.AcceptText();
                objDwCheque.DeleteRow(row);
            }
        }
        return 0;
    }

    function OnDwChequeClick(s, r, c) {
        Gcoop.CheckDw(s, r, c, "late_flag", 1, 0);
        if(r > 0){
            Gcoop.GetEl("HdDwChequeRow").value = r + "";
        }
        if (c == "bank_name" || c == "branch_name") {
            var bankCode = "";
            try {
                bankCode = s.GetItem(r, "bank_code");
                if (bankCode == null) throw "";
            } catch (Err) { bankCode = ""; }
            Gcoop.OpenDlg(860, 600, "w_dlg_bank_and_branch.aspx", "?sheetRow=" + r + "&bankCodeQuery=" + bankCode);
        } else if (c == "late_flag"){
            var v = s.GetItem(r, c);
            var dayPass = Gcoop.ParseInt(Gcoop.GetEl("HdDayPassCheq").value);
            if(v == 1){
                s.SetItem(r, "day_float", dayPass + 1);
                s.AcceptText();
            } else {
                s.SetItem(r, "day_float", dayPass);
                s.AcceptText();
            }
        }
    }
    
    function OnDwChequeItemChanged(s, r, c, v) {
        if(r > 0){
            Gcoop.GetEl("HdDwChequeRow").value = r + "";
        }
        if (c == "cheque_no") {
            s.SetItem(r, c, v);
            s.AcceptText();
            postPost();
        } else if (c == "bank_code"){
            s.SetItem(r, c, v);
            s.SetItem(r, "bank_name", "");
            s.SetItem(r, "branch_code", "");
            s.SetItem(r, "branch_name", "");
            s.AcceptText();
            postBankCode();
        } else if (c == "branch_code"){
            var bankName = "";
            try{
                bankName = Gcoop.Trim(s.GetItem(r, "bank_name"));
            }catch(err){ bankName = ""; }
            if(bankName != ""){
                s.SetItem(r, c, v);
                s.SetItem(r, "branch_name", "");
                s.AcceptText();
                postBankBranchCode();
            } else {
                alert("กรุณากรอกเลขที่ธนาคารก่อน!");
            }
        } else if (c == "cheque_amt"){
            s.SetItem(r, c, v);
            s.AcceptText();
            postPost();
        }
    }
    
    function OnDwMainItemChanged(s, r, c, v) {
        if (c == "deptaccount_no") {
            objDwMain.SetItem(r, c, v);
            objDwMain.AcceptText();
            postDeptAccountNo();
        }
        return 0;
    }
    
    function SheetLoadComplete(){
        if(Gcoop.GetEl("HdIsPostBack").value == "false"){
            Gcoop.Focus("deptaccount_no_0");
        }
        if(Gcoop.GetEl("HdIsInsertCash").value == "true"){
            try{
                Gcoop.Focus("slip_amt_" + (objDwCash.RowCount() - 1));
            }catch(err){}
        }
        if(Gcoop.GetEl("HdIsInsertCheque").value == "true"){
            try{
                Gcoop.Focus("cheque_no_" + (objDwCheque.RowCount() - 1));
            }catch(err){}
        }
    }
    
    function Validate() {
        Gcoop.SetLastFocus("deptaccount_no_0");
        return confirm("ยืนยันการบันทึกข้อมูล");
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div>
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dept_cancle_master"
            LibraryList="~/DataWindow/ap_deposit/dp_multislip.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="OnDwMainItemChanged"
            ClientScriptable="True" TabIndex="1">
        </dw:WebDataWindowControl>
    </div>
    <table style="width: 100%;" cellspacing="2" cellpadding="1">
        <tr>
            <td colspan="2" class="style1">
                <asp:Label ID="LbGain1" runat="server" Text="รายการเงินสด" Font-Bold="True" Font-Names="Tahoma"
                    Font-Size="14px" ForeColor="#0099CC" Font-Overline="False" Font-Underline="True" />
                &nbsp; <span onclick="OnClickInsertRowCash()" style="cursor: pointer;">
                    <asp:Label ID="Label6" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="#006600" /></span> &nbsp;&nbsp;
                <span onclick="OnClickDeleteRowCash()" style="cursor: pointer;">
                    <asp:Label ID="Label8" runat="server" Text="ลบแถว" Font-Bold="False" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="Red" /></span>
            </td>
            <td class="style3">
                <span class="style1">การทำรายการ </span>
            </td>
        </tr>
        <tr>
            <td class="blueBorder" width="320">
                <dw:WebDataWindowControl ID="DwCash" runat="server" DataWindowObject="d_dp_deposit_multicash"
                    LibraryList="~/DataWindow/ap_deposit/dp_multislip.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientEventButtonClicked="OnDwCashButtonClicked" ClientFormatting="True" ClientEventClicked="OnDwCashClicked"
                    ClientEventItemChanged="OnDwCashItemChanged" TabIndex="500">
                </dw:WebDataWindowControl>
            </td>
            <td class="blueBorder" width="220">
                <asp:Panel ID="Panel4" runat="server" Width="220px" Height="100px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="หมายเหตุ" CssClass="newStyle1"></asp:Label><br />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label4" runat="server" Text="รายการแต่ละรายการจะแยกเป็น" CssClass="newStyle2"></asp:Label><br />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label5" runat="server" Text="รายการต่าง ๆ ไป ไม่มีการรวมรายการ" CssClass="newStyle2"></asp:Label><br />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="ลงในรายการเคลื่อนไหว" CssClass="newStyle2"></asp:Label><br />
                </asp:Panel>
            </td>
            <td class="blueBorder" width="210">
                <asp:Panel ID="Panel5" runat="server" Width="210px" Height="100px">
                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="RadioButton1" runat="server" Text="มีสมุด" CssClass="style2"
                        Checked="True" GroupName="isCheckBook" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:RadioButton ID="RadioButton2" runat="server" Text="ไม่มีสมุด" CssClass="style2"
                        GroupName="isCheckBook" />
                    <br />
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="ยอดในสมุด" CssClass="style2"></asp:Label>
                    &nbsp; <span onclick="OnClickTextBox1()">
                        <asp:TextBox ID="TextBox1" runat="server" Height="25px" Width="130px"
                            OnTextChanged="TextBox1_TextChanged">0.00</asp:TextBox>
                    </span>
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center" class="style1">
                <asp:Label ID="Label7" runat="server" Text="รายการเช็ค" Font-Bold="True" Font-Names="Tahoma"
                    Font-Size="14px" ForeColor="#0099CC" Font-Overline="False" Font-Underline="True" />
                &nbsp; <span onclick="OnClickInsertRowCheque()" style="cursor: pointer;">
                    <asp:Label ID="LbInsert2" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="#006600" /></span> &nbsp;&nbsp;
                <span onclick="OnClickDeleteRowCheque()" style="cursor: pointer;">
                    <asp:Label ID="LbDel2" runat="server" Text="ลบแถว" Font-Bold="False" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="Red" /></span>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="DwCheque" runat="server" DataWindowObject="d_cheque_operate_multiexternal"
                    LibraryList="~/DataWindow/ap_deposit/dp_multislip.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="OnDwChequeItemChanged"
                    ClientEventItemError="OnItemChequeError" ClientScriptable="True" ClientEventButtonClicked="OnDwChequeButtonClicked"
                    ClientEventClicked="OnDwChequeClick" ClientFormatting="True" TabIndex="800">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdDwChequeRow" runat="server" />
    <asp:HiddenField ID="HdDwCashRow" runat="server" />
    <asp:HiddenField ID="HdDeptAccountNo" runat="server" />
    <asp:HiddenField ID="HdDayPassCheq" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" />
    <asp:HiddenField ID="HdIsInsertCash" runat="server" />
    <asp:HiddenField ID="HdIsInsertCheque" runat="server" />
</asp:Content>
