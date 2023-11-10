<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loanrequest_search_emer.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_sl_loanrequest_search_emer" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาใบคำขอกู้ฉุกเฉิน</title>
    <%=searchLoanrequest%>
    <%=setDocNo %>
    <%=refresh  %>

    <script type="text/javascript">
     function OnDwCriteriaClick(sender, rowNumber, buttonName){
      if( buttonName == "b_search")
      {
        searchLoanrequest();
      }  
     }
    function selectRow(sender, rowNumber, objectName) {
      if(objectName!= "datawindow"){
         Gcoop.GetEl("HfRow").value = rowNumber;
         setDocNo();
       }
     }
     function ItemChangeDWCriteria(sender, rowNumber, columnName, newValue){
      if(columnName =="member_no"){
        var memberNoT = Gcoop.Trim(newValue);
        var memberNo = Gcoop.StringFormat(memberNoT, "000000");
        objdw_criteria.SetItem(1, "member_no", memberNo);
        objdw_criteria.AcceptText();
        refresh();
      }
     }
     function DialogLoadComplete(){
         var docNo = Gcoop.GetEl("HfDocNo").value;
         if ((docNo != null ) && (docNo != "" ))
         {
//            try{
//                parent.GetValueLoanRequest(docNo);
//            }catch(Err){
//                alert("Error Dlg");
//                //window.close();
//                parent.RemoveIFrame();
//            }
            window.opener.GetValueLoanRequest(docNo);
            window.close();
         }
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_criteria" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    DataWindowObject="d_sl_loanrequest_search_criteria_emer" LibraryList="~/DataWindow/shrlon/sl_loan_requestment.pbl"
                    ClientFormatting="True" ClientEventItemChanged="ItemChangeDWCriteria" ClientEventButtonClicking="OnDwCriteriaClick">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="HfSearch" runat="server" />
                <dw:WebDataWindowControl ID="dw_detail" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientEventClicked="selectRow" DataWindowObject="d_sl_loanrequest_search_list"
                    LibraryList="~/DataWindow/shrlon/sl_loan_requestment.pbl" RowsPerPage="17" ClientFormatting="False"
                    Style="top: 0px; left: 0px">
                    <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
                        <BarStyle HorizontalAlign="Center" />
                        <NumericNavigator FirstLastVisible="True" />
                    </PageNavigationBarSettings>
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HfDocNo" runat="server" />
    <asp:HiddenField ID="HfRow" runat="server" />
    </form>
</body>
</html>
