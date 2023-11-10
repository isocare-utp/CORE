<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_paychq_fromslip.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_paychq_fromslip" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postBankBranch %>
    <%=postChqBook%>
    <%=postCheckList %>
    <%=postInit %>
    <%=postProtect %>
    <%=postSearch %>
    <%=postCheckAll %>
    <script type="text/javascript">

        function Validate() {
            objDwCond.AcceptText();
            objDwBank.AcceptText();
            objDwType.AcceptText();
            objDwChqList.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function MenubarNew() {
            if (confirm("ยืนยันการลบข้อมูล ??")) {
                window.location = Gcoop.GetUrl() + "Applications/app_finance/w_sheet_paychq_fromslip.aspx";
            }
        }

        function DwMainItemChange(sender, rowNumber, columnName, newValue) {
            objDwCond.SetItem(rowNumber, columnName, newValue);
            if (columnName == "as_bank") {
                objDwCond.AcceptText();
                postBankBranch();
            }
            else if (columnName == "as_bankbranch") {
                objDwCond.AcceptText();
                postChqBook();
            }
            else if (columnName == "as_chqbookno") {
                objDwCond.AcceptText();
                postInit();
            }
            else if (columnName == "as_printtype") {
                objDwCond.AcceptText();
                postProtect();
            }
        }

        function DwBankButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_1") {
                var bank = objDwCond.GetItem(rowNumber, "as_bank");
                var bankbranch = objDwCond.GetItem(rowNumber, "as_bankbranch");
                Gcoop.OpenIFrameExtend(455, 200, "w_dlg_bank_list.aspx", "?bank=" + bank + "&bankbranch=" + bankbranch);
            }
        }

        function GetAccid(accid, accname) {
            objDwBank.SetItem(1, "as_fromaccno", accid);
        }

        function DwChqListItemChange(sender, rowNumber, columnName, newValue) {
            objDwChqList.SetItem(rowNumber, columnName, newValue);
            if (columnName == "ai_selected") {
                Gcoop.GetEl("HdRow").value = rowNumber;
                Gcoop.GetEl("HdCheck").value = newValue;
                objDwChqList.AcceptText();
                postCheckList();
            }
        }

        function DwDateSearchButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_search") {
                objDwDateSearch.AcceptText();
                postSearch();
            }
        }

        function DwDateSearchClick(sender, rowNumber, objectName) {
            if (objectName == "all_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "all_flag", 1, 0);
                objDwDateSearch.AcceptText();
                postCheckAll();
            }
        }

        function DwDateSearchItemChange(sender, rowNumber, columnName, newValue) {
            objDwDateSearch.AcceptText();
        }

        function DwChqListClick(sender, rowNumber, objectName) {
            if (objectName == "ai_selected") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "ai_selected", 1, 0);
                var flag = objDwChqList.GetItem(rowNumber, "ai_selected");
                Gcoop.GetEl("HdRow").value = rowNumber;
                Gcoop.GetEl("HdCheck").value = flag;
                objDwChqList.AcceptText();
                postCheckList();
            }
        }

        $(function () {
            // AutoSlash('input[name="start_tdate_0"]');

        });
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td width="33%" valign="top">
                <dw:WebDataWindowControl ID="DwCond" runat="server" DataWindowObject="d_conditionprint_cheque"
                    LibraryList="~/DataWindow/App_finance/paychq_fromslip.pbl" ClientScriptable="True"
                    AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true"
                    ClientEventItemChanged="DwMainItemChange">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td width="25%" valign="top">
                <dw:WebDataWindowControl ID="DwBank" runat="server" DataWindowObject="d_chqprint_cutfrom"
                    LibraryList="~/DataWindow/App_finance/paychq_fromslip.pbl" ClientScriptable="True"
                    AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true"
                    ClientEventButtonClicked="DwBankButtonClick" ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
            <td width="33%" valign="top">
                <dw:WebDataWindowControl ID="DwType" runat="server" DataWindowObject="d_chqprint_chqtype"
                    LibraryList="~/DataWindow/App_finance/paychq_fromslip.pbl" ClientScriptable="True"
                    AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <hr />
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwDateSearch" runat="server" DataWindowObject="d_fn_onedate_chq"
                    LibraryList="~/DataWindow/App_finance/paychq_fromslip.pbl" ClientScriptable="True"
                    AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true"
                    ClientEventItemChanged="DwDateSearchItemChange" ClientEventButtonClicked="DwDateSearchButtonClick"
                    ClientEventClicked="DwDateSearchClick">
                </dw:WebDataWindowControl>
                <dw:WebDataWindowControl ID="DwChqList" runat="server" DataWindowObject="d_chequelist_fromslip"
                    LibraryList="~/DataWindow/App_finance/paychq_fromslip.pbl" ClientScriptable="True"
                    AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true"
                    ClientEventItemChanged="DwChqListItemChange" ClientEventClicked="DwChqListClick"
                    ClientFormatting="True" RowsPerPage="10">
                    <PageNavigationBarSettings Position="Bottom" Visible="True" NavigatorType="Numeric">
                        <BarStyle HorizontalAlign="Center" />
                        <NumericNavigator FirstLastVisible="True" />
                    </PageNavigationBarSettings>
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdRow" runat="server" />
    <asp:HiddenField ID="HdCheck" runat="server" />
</asp:Content>
