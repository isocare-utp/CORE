<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_popup_loanreceive.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_popup_loanreceive" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
       
 
    <%=openDialogDetail%>
    <%=initListLnRcv%>
    <%=searchMemberNo  %>
    
    <script type="text/javascript">

        function selectRow(sender, rowNumber, objectName) {
            var member_no = objDwlist.GetItem(rowNumber, "member_no");
            window.opener.GetValueFromDlg(member_no);
            window.close();
        }
    function OpenLoanReceive() {
        if(CheckSelected()){
            objDwlist.AcceptText();
            openDialogDetail();
        }else{
            return;
        } 
    }
    
    //ตรวจสอบ ว่ามี เช็กถูกหรือเปล่า
    function CheckSelected(){
        var allRow = objDwlist.RowCount();
        var i=0;
        var haveChecked = false;
        
        for(i;i<allRow;i++){
            var indexRow = i+1;
            var check = objDwlist.GetItem(indexRow, "operate_flag");
            if(check==1){
                haveChecked=true;
            }
        }
        if(haveChecked==true){
            return true;
        }else{
            return false;
        }
        
    }
    function SearchMember()
    {
        
        var memberNOS =Gcoop.GetEl( "memberNo").value  ;
        Gcoop.GetEl("HfMemberNo"). value = Gcoop.StringFormat(memberNOS, "000000");
        searchMemberNo();
    }
    function RefreshByDlg(){
        initListLnRcv();
    }
    function OnDwListItemClicked(sender, rowNumber, objectName){
        Gcoop.CheckDw(sender, rowNumber, objectName, "operate_flag", 1, 0);        
    }
    
    </script>
</head>
<body onload="SaveStatus();">
    <form id="form1" runat="server">
   <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HfPageCommand" runat="server" />
    <asp:HiddenField ID="HfMemberNo" runat="server" />
    <table style="width: 100%;" border="0">
        <tr>
            <td valign="top" align="center" style="padding-top: 10px; padding-bottom: 10px;">
                ระบุ คำค้นหา&nbsp;<input id="memberNo" type="text" />&nbsp;<input id="b_goto" style="width:60px; height:25px;" type="button" value="ค้นหา" onclick="SearchMember()" />
<%--                <dw:WebDataWindowControl ID="DwGoto" runat="server" DataWindowObject="d_sl_lnrcv_contnogoto"
                    LibraryList="~/DataWindow/shrlon/sl_loan_receive.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>///////
            </td>
            <td valign="top" align="center" style="padding-bottom: 10px;">
                <input id="b_paid" style="width: 200px; cursor:pointer; height: 40px; color: White; font-size: medium;
                    font-weight: bold; background-color: Green;" type="button" onclick="OpenLoanReceive()"
                    value="จ่ายเงินกู้"/>--%>
            </td>
        </tr>
        <tr>
            <td valign="top"colspan="2">
                <table style="border:solid 1px black;background-color: rgb(211,231,255)"  width="100%">
                    <tr>
                    <td style="border-right:solid 1px black; width:23px; text-align:center;">&nbsp;</td>
                    <td style="border-right:solid 1px black; width:45px; text-align:center;">จ่ายเงินกู้จาก</td>
                    <td style="border-right:solid 1px black; width:98px; text-align:center;">เลขที่สัญญา/เลขใบคำขอ</td>
                    <td style="border-right:solid 1px black; width:20px; text-align:center;">ประเภทเงินกู้</td>
                    <td style="border-right:solid 1px black; width:50px; text-align:center;">เลขสมาชิก</td>
                    <td style="border-right:solid 1px black; text-align:center;">ชื่อ-ชื่อสกุล</td>
                    <td style="border-right:solid 1px black; width:60px; text-align:center;">สังกัด</td>
                    <td style="width:120px; text-align:center;">ยอดรอจ่าย</td>
                    </tr>
                </table>
                <asp:Panel ID="PnDwList" runat="server" Height="500px" ScrollBars="Vertical">                
                <dw:WebDataWindowControl ID="Dwlist" runat="server" DataWindowObject="d_sl_lnrcv_contnolist"
                    LibraryList="~/DataWindow/shrlon/sl_loan_receive.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" ClientFormatting="True" 
                    ClientScriptable="True" Height="430px" 
                    ClientEventClicked="selectRow" RowsPerPage="15">
                     <PageNavigationBarSettings Position="Bottom" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
                </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
