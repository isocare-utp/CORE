<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_collmaster_search_req.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_collmaster_search_req" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาทะเบียนหลักทรัพย์</title>
    <%=collmastersSearch%>
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            var collmast_no = objdw_list.GetItem(rowNumber, "collmast_no");
            var collmast_desc = objdw_list.GetItem(rowNumber, "collmast_desc");
            var collmast_refno = objdw_list.GetItem(rowNumber, "collmast_refno");
            var mortgage_price = Gcoop.ParseFloat(objdw_list.GetItem(rowNumber, "mortgage_price"));
            // var collmast_price = Gcoop.ParseFloat(objdw_list.GetItem(rowNumber, "collmast_price"));
            var base_percent = Gcoop.ParseFloat(objdw_list.GetItem(rowNumber, "coll_percent"));
            //            if (collmast_price == null || collmast_price == 0) {
            //                collmast_price = Gcoop.ParseFloat(objdw_list.GetItem(rowNumber, "mortgage_price"));
            //                collmast_price = collmast_price * base_percent;
            //            }
            collmast_desc = collmast_refno + ' ' + collmast_desc;
            
            try {
                window.opener.GetValueFromDlgCollmast(collmast_no, collmast_desc, mortgage_price, base_percent);
                window.close();
            } catch (err) {
                parent.GetValueFromDlgCollmast(collmast_no, collmast_desc, mortgage_price, base_percent);
                parent.RemoveIFrame();
            }
        }
        function OnDwDataLnContSrc(sender, rowNumber, objectName) {
            collmastersSearch();
            dw_master.AcceptText();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <%--  รายการค้นหา--%>
    <table style="width: 530px;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_master" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    DataWindowObject="d_sl_collmaster_criteria" Visible="false" LibraryList="~/DataWindow/shrlon/sl_loan_requestment_cen.pbl"
                    ClientEventButtonClicked="OnDwDataLnContSrc">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <%--รายละเอียด--%>
    <asp:HiddenField ID="hidden_search" runat="server" />
    <asp:HiddenField ID="HdLoantype_code" runat="server" />
    <dw:WebDataWindowControl ID="dw_list" runat="server" ClientEventClicked="selectRow"
        DataWindowObject="d_sl_collmaster_list" LibraryList="~/DataWindow/shrlon/sl_loan_requestment_cen.pbl"
        RowsPerPage="14" ClientScriptable="True">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    </form>
</body>
</html>
