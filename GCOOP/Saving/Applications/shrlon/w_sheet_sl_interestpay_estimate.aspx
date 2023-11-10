<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_interestpay_estimate.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_interestpay_estimate" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=initLnRcv%>
    <%=initSlipCalInt%>
    <%=getPeriodCont%>
    <%=calculateAmt%>
    <%=downloadPDF%>
    <style type="text/css">
        .style1
        {
            color: #009999;
            padding-left: 10px;
            font-family: Tahoma, 'Times New Roman' , Times;
            font-weight: bold;
            font-size: 15px;
            text-decoration: underline;
            padding-left: 10px;
        }
        .style2
        {
            color: #9900CC;
            text-decoration: underline;
            padding-left: 10px;
            font-family: Tahoma, 'Times New Roman' , Times;
            font-weight: bold;
            font-size: 15px;
        }
        .style3
        {
            color: #999966;
            text-decoration: underline;
            padding-left: 10px;
            font-family: Tahoma, 'Times New Roman' , Times;
            font-weight: bold;
            font-size: 15px;
        }
        .style4
        {
            font-family: Tahoma, 'Times New Roman' , Times;
            font-weight: bold;
            font-size: 15px;
            color: #0066BB;
            text-decoration: underline;
            padding-left: 10px;
        }
    </style>

    <script type="text/javascript">
    function GetValueFromDlg(strvalue) {
        objdw_main.SetItem(1, "member_no", Gcoop.StringFormat(strvalue,"00000000"));
        objdw_main.AcceptText();
        initLnRcv();
    }
    function GetValueFromIFrame(strvalue,strdesc) {
            objdw_main.SetItem(1,"tofrom_accid",strvalue);
            //objdw_main.Modify("accid_text.text="+strvalue);
            objdw_main.AcceptText();
            Gcoop.GetEl("HfAccDesc").value = strdesc;
            setAccDesc();
    }
    function IsInit(){
        try{
            var haveMem = objdw_main.GetItem(1, "member_no");
            if(haveMem != ""){
                return true;
            }
        }catch(err){
            return false;
        }
    }

    function IsLoanChecked(){
        try{
            var row = objdw_loan.RowCount();
            for(var i=0; i <= row; i++){
                var isCheck = objdw_loan.GetItem(i, "operate_flag");
                if(isCheck == "1"){
                    return true;
                    break;
                }
            }
            return false;
        }catch(err){alert(err);}
    }

    function LoanUnChecked(sender, rowNumber, columnName, newValue){
        objdw_loan.SetItem(rowNumber,"interest_payamt",0);
        objdw_loan.SetItem(rowNumber,"principal_payamt",0);
        objdw_loan.SetItem(rowNumber,"item_payamt",0);
        objdw_loan.SetItem(rowNumber,"item_balance",objdw_loan.GetItem(rowNumber,"bfshrcont_balamt"));
    }
    
    function OnDwLoanClicked(s, r, c){
    if(c!="datawindow"){
        Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
        Gcoop.CheckDw(s, r, c, "periodcount_flag", 1, 0);
        var v = s.GetItem(r, c);
        if(c == "periodcount_flag"){
            var pe = s.GetItem(r, "period");
            if(v == 1){
                s.SetItem(r, "period", pe + 1);
            }else{
                s.SetItem(r, "period", pe - 1);
            }
            s.AcceptText();
        }else if(c == "operate_flag"){
            Gcoop.SetLastFocus("principal_payamt");
            if(v == 0){
                //objdw_loan.SetItem(r, "operate_flag", 0);
                objdw_loan.SetItem(r, "item_payamt", 0);
                objdw_loan.SetItem(r, "principal_payamt", 0);
                objdw_loan.SetItem(r, "interest_payamt", 0);
                
                if(objdw_loan.GetItem(r, "periodcount_flag") == 1){
                    objdw_loan.SetItem(r, "periodcount_flag", 0);
                    objdw_loan.SetItem(r, "period", objdw_loan.GetItem(r, "period") - 1);
                }
                objdw_loan.AcceptText();
                calculateAmt();
            }
        }
        }
    }
    
    function OnDwLoanItemChanged(sender, rowNumber, columnName, newValue){
        //Set ค่า ให้ HiddenField ว่า กดจาก Row ไหน
        Gcoop.GetEl("HfRowSelected").value = rowNumber;
        //get ค่า จาก operate_flag ที่ คลิก
        var operateflagValue = objdw_loan.GetItem(rowNumber, "operate_flag");
        var bfpayspec_method = objdw_loan.GetItem(rowNumber, "bfpayspec_method");
        //ถ้ากรอกจำนวนเงิน
        if(columnName == "item_payamt" && bfpayspec_method =="1"){
            //มีการเช็คถูก
            if(operateflagValue!=0){
                objdw_loan.SetItem(rowNumber,"item_payamt",newValue);
                objdw_loan.AcceptText();
                objdw_main.AcceptText();
                calculateAmt();
            }
        }else if(columnName=="principal_payamt" && bfpayspec_method=="2"){
            if(newValue > 0){
                objdw_loan.SetItem(rowNumber, "operate_flag", 1);
                objdw_loan.SetItem(rowNumber, "item_payamt", newValue);
                objdw_loan.SetItem(rowNumber, "principal_payamt", newValue);
                objdw_loan.AcceptText();
            }else{
                objdw_loan.SetItem(rowNumber, "operate_flag", 0);
                objdw_loan.SetItem(rowNumber, "item_payamt", 0);
                objdw_loan.SetItem(rowNumber, "principal_payamt", 0);
                objdw_loan.SetItem(rowNumber, "interest_payamt", 0);
                
                if(objdw_loan.GetItem(rowNumber, "periodcount_flag") == 1){
                    objdw_loan.SetItem(rowNumber, "periodcount_flag", 0);
                    objdw_loan.SetItem(rowNumber, "period", objdw_loan.GetItem(rowNumber, "period") - 1);
                }
                objdw_loan.AcceptText();
            }
            calculateAmt();
        }
    }
    
    function OnDwMainButtonClicked(sender, rowNumber, buttonName){
        if(buttonName == "b_search"){
            //Gcoop.OpenDlg('570','590','w_dlg_sl_member_search.aspx','');
            OpenDlgSearch();
        }else if(buttonName == "b_accset"){
            var memno = objdw_main.GetItem(1,"member_no");
            if(memno!= null){
                var mtype = objdw_main.GetItem(1,"moneytype_code");
                Gcoop.OpenIFrame('365','300','w_dlg_bankaccount.aspx','?mtype='+mtype,null,'180');
            }
        }
    }
    function MenubarOpen(){
        OpenDlgSearch();
    }
    
    function OpenDlgSearch(){
        Gcoop.OpenIFrame('570','600','w_dlg_sl_member_search.aspx','');
    }

    function OnDwMainItemChanged(sender, rowNumber, columnName, newValue){
        if(columnName == "member_no"){
            objdw_main.SetItem (1, "member_no", Gcoop.StringFormat(Gcoop.Trim(newValue), "00000000"));
            objdw_main.AcceptText();
            if(Gcoop.Trim(newValue) != ""){
                initLnRcv();
            }
            return 0;
        }else if(columnName == "operate_tdate"){
            var memNo = Gcoop.Trim(objdw_main.GetItem(1, "member_no"));
            if(memNo != ""){
                objdw_main.SetItem( 1, "operate_tdate", newValue );
                objdw_main.AcceptText();
                objdw_main.SetItem( 1, "operate_date", Gcoop.ToEngDate(newValue));
                objdw_main.AcceptText();
                objdw_loan.AcceptText();
                initSlipCalInt();
            }
            return 0;
        }else if(columnName=="moneytype_code"){
            objdw_main.SetItem (rowNumber, columnName, newValue);
            objdw_main.AcceptText();
        }
    }

    function Validate(){
        var isCalInt = Gcoop.GetEl("HfisCalInt").value;
        var haveLoan = IsLoanChecked();
        var haveMem = IsInit();
            if(haveMem){
                return true;
            }else{
                alert("กรุณากรอกเลขทะเบียนสมาชิก"); 
                return false;
            }
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table width="98%">
        <tr>
            <td align="left">
                <asp:Label ID="Label1" runat="server" Text="การรับชำระจากสมาชิก" CssClass="style4"></asp:Label>
            </td>
            <td align="right">
                <input id="pdfbutton" type="button" value="ดาวน์โหลดเป็น PDF+" onclick="downloadPDF()" />
            </td>
        </tr>
    </table>
    <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_sl_slip_operate_main"
        LibraryList="~/DataWindow/shrlon/sl_interestpay_estimate.pbl" ClientScriptable="True"
        ClientEventButtonClicked="OnDwMainButtonClicked" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="OnDwMainItemChanged"
        ClientFormatting="True" TabIndex="1">
    </dw:WebDataWindowControl>
    <br />
    <span class="style2">รายการหนี้</span>
    <dw:WebDataWindowControl ID="dw_loan" runat="server" DataWindowObject="d_sl_slip_operate_loan"
        LibraryList="~/DataWindow/shrlon/sl_interestpay_estimate.pbl" ClientScriptable="True"
        ClientEventItemChanged="OnDwLoanItemChanged" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientEventClicked="OnDwLoanClicked"
        TabIndex="500">
    </dw:WebDataWindowControl>
    <br />
    <dw:WebDataWindowControl ID="dw_pdf" runat="server" DataWindowObject="d_slippayin_invoice"
        Width="400px" LibraryList="~/DataWindow/shrlon/sl_interestpay_estimate.pbl">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HfRowSelected" runat="server" />
    <asp:HiddenField ID="HfisCalInt" runat="server" />
</asp:Content>
