<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loanrequest_search.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_loanrequest_search" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาใบคำขอกู้</title>
    <%=searchLoanrequest%>
    <%=setDocNo%>
    <%=refresh%>

    <script type="text/javascript">
        function OnDwCriteriaClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_search") {
                searchLoanrequest();
            }
        }

       /* function selectRow(sender, rowNumber, objectName) {
            if (objectName != "datawindow") {
                Gcoop.GetEl("HfRow").value = rowNumber;
                setDocNo();
            }
        }*/

        function ItemChangeDWCriteria(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                var memberNoT = "00000000" + Gcoop.Trim(newValue);
                var memberNo = memberNoT.substring(memberNoT.length - 8, memberNoT.length); // Gcoop.StringFormat(memberNoT, "00000000");
                objdw_criteria.SetItem(1, "member_no", memberNo);
                objdw_criteria.AcceptText();
                searchLoanrequest();
            }
        }

       /* function DialogLoadComplete() {
            var docNo = Gcoop.GetEl("HfDocNo").value;
            var coop_id = Gcoop.GetEl("HdcoopId").value;
            if ((docNo != null) && (docNo != "") && (coop_id != null) && (coop_id != "")) {
                window.opener.GetValueLoanRequest(docNo, coop_id);
                window.close();
            }
        }*/

        function selectRow(sender, rowNumber, objectName) {

            if (objectName != "datawindow") {
                Gcoop.GetEl("HfRow").value = rowNumber;
                setDocNo();
                var docNo = Gcoop.GetEl("HfDocNo").value;
                var coop_id = Gcoop.GetEl("HdcoopId").value;

                if ((docNo != null) && (docNo != "") && (coop_id != null) && (coop_id != "")) {

                    //alert(docNo);
                    parent.GetValueLoanRequest(docNo, coop_id);
                    parent.RemoveIFrame();
                    //window.opener.GetValueLoanRequest(docNo, coop_id);
                }

                //window.close();
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
                    DataWindowObject="d_sl_loanrequest_search_criteria" LibraryList="~/DataWindow/shrlon/sl_loan_requestment_cen.pbl"
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
                    LibraryList="~/DataWindow/shrlon/sl_loan_requestment_cen.pbl" RowsPerPage="17"
                    ClientFormatting="True" Style="top: 0px; left: 0px">
                    <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
                        <BarStyle HorizontalAlign="Center" />
                        <NumericNavigator FirstLastVisible="True" />
                    </PageNavigationBarSettings>
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HfDocNo" runat="server" />
    <asp:HiddenField ID="HdcoopId" runat="server" />
    <asp:HiddenField ID="HfRow" runat="server" />
    </form>
</body>
</html>
