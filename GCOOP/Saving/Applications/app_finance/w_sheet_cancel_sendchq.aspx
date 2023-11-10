<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_cancel_sendchq.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_cancel_sendchq" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postRetrieve %>
    <%=postSumSelect %>

    <script type="text/javascript">

        function Validate() {
            try {
                objDwCancelList.AcceptText();
                objDwSendChqAcc.AcceptText();
            }
            catch (errs) {
                alert(errs);
            }
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function RetrieveClick() {
            objDwSendChqAcc.AcceptText();
            postRetrieve();
        }

        function DwCancelListClick(sender, rowNumber, objectName) {
            if (objectName == "select_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "select_flag", 1, 0);
                objDwCancelList.AcceptText();
                postSumSelect();
            }
        }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table width="100%" border="0">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwDate" runat="server" DataWindowObject="d_senchq_date_head"
                    LibraryList="~/DataWindow/App_finance/sendchq.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
            </td>
            <td valign="bottom">
                <dw:WebDataWindowControl ID="DwSendChqAcc" runat="server" DataWindowObject="d_senchq_acc_head"
                    LibraryList="~/DataWindow/App_finance/sendchq.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
            </td>
            <td>
                <input style="width: 60px; height: 30px;" id="cb_retrieve" type="button" value="ดึงข้อมูล"
                    onclick="RetrieveClick()" />
            </td>
        </tr>
    </table>
    <br />
    <dw:WebDataWindowControl ID="DwCancelList" runat="server" DataWindowObject="d_cancel_senchq_list"
        LibraryList="~/DataWindow/App_finance/sendchq.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventClicked="DwCancelListClick">
    </dw:WebDataWindowControl>
</asp:Content>
