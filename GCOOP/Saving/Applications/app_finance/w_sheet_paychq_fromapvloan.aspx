<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_paychq_fromapvloan.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_paychq_fromapvloan" %>

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
                window.location = Gcoop.GetUrl() + "Applications/app_finance/w_sheet_paychq_fromapvloan.aspx";
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
                Gcoop.OpenDlg(455, 200, "w_dlg_bank_list.aspx", "?bank=" + bank + "&bankbranch=" + bankbranch);
            }
        }

        function GetAccid(accid, accname) {
            objDwBank.SetItem(1, "as_fromaccno", accid);
        }

        function DwChqListItemChange(sender, rowNumber, columnName, newValue) {
            objDwChqList.SetItem(rowNumber, columnName, newValue);
            if (columnName == "ai_chqflag") {
                Gcoop.GetEl("HdRow").value = rowNumber;
                Gcoop.GetEl("HdCheck").value = newValue;
                objDwChqList.AcceptText();
                //                postCheckList();
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

            if (columnName == "start_tdate") {
                //alert(newValue + "");

                sender.SetItem(rowNumber, "start_tdate", newValue);
                sender.AcceptText();
                sender.SetItem(rowNumber, "start_date", Gcoop.ToEngDate(newValue));
                sender.AcceptText();
                //  postSearch();

            }
            objDwDateSearch.AcceptText();
        }

        function DwChqListClick(sender, rowNumber, objectName) {
            if (objectName == "ai_chqflag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "ai_chqflag", 1, 0);
                var flag = objDwChqList.GetItem(rowNumber, "ai_chqflag");
                Gcoop.GetEl("HdRow").value = rowNumber;
                Gcoop.GetEl("HdCheck").value = flag;
                objDwChqList.AcceptText();
                postCheckList();
            }
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td>
                เลือกเครื่องพิมพ์ :
                <asp:DropDownList ID="DdPrintSetProfile" runat="server" Width="200px">
                    <asp:ListItem Text="printfin-23" Value="printfin-23" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="printfin-22" Value="printfin-22"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="33%" valign="top">
                <dw:WebDataWindowControl ID="DwCond" runat="server" DataWindowObject="d_conditionprint_cheque"
                    LibraryList="~/DataWindow/App_finance/paychq_fromapvloan.pbl" ClientScriptable="True"
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
                    LibraryList="~/DataWindow/App_finance/paychq_fromapvloan.pbl" ClientScriptable="True"
                    AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true"
                    ClientEventButtonClicked="DwBankButtonClick" ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
            <td width="33%" valign="top">
                <dw:WebDataWindowControl ID="DwType" runat="server" DataWindowObject="d_chqprint_chqtype"
                    LibraryList="~/DataWindow/App_finance/paychq_fromapvloan.pbl" ClientScriptable="True"
                    AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <hr />
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwDateSearch" runat="server" DataWindowObject="d_fn_onedate_bank_chq"
                    LibraryList="~/DataWindow/App_finance/paychq_fromapvloan.pbl" ClientScriptable="True"
                    AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true"
                    ClientEventItemChanged="DwDateSearchItemChange" ClientEventButtonClicked="DwDateSearchButtonClick"
                    ClientEventClicked="DwDateSearchClick" ClientFormatting="True">
                </dw:WebDataWindowControl>
                <asp:Panel runat="server" ID="Panel1" ScrollBars="Auto" Width="700px" Height="400px">
                    <dw:WebDataWindowControl ID="DwChqList" runat="server" DataWindowObject="d_chequelist_apvloan_cbt"
                        LibraryList="~/DataWindow/App_finance/paychq_fromapvloan.pbl" ClientScriptable="True"
                        AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true"
                        ClientEventItemChanged="DwChqListItemChange" ClientEventClicked="DwChqListClick"
                        RowsPerPage="10">
                        <PageNavigationBarSettings Position="Bottom" Visible="True" NavigatorType="Numeric">
                            <BarStyle HorizontalAlign="Center" />
                            <NumericNavigator FirstLastVisible="True" />
                        </PageNavigationBarSettings>
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdRow" runat="server" />
    <asp:HiddenField ID="HdCheck" runat="server" />
</asp:Content>
