<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_paychq_apvloan_cbt.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_paychq_apvloan_cbt" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postBankBranch %>
    <%=postSumChqList %>
    <%=postGetChqBook%>
    <%=postcheckAll %>
    <%=postInit %>
    <%=postSearch %>
    <%=postCheckBankAll %>
    <%=postGetChqNo%>
    <%=postSumValue%>
    <script type="text/javascript">

        function Validate() {
            objDwMain.AcceptText();
            objDwDetail.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function MenubarNew() {
            if (confirm("ยืนยันการลบข้อมูล ??")) {
                window.location = Gcoop.GetUrl() + "Applications/app_finance/w_sheet_paychq.aspx";
            }
        }

        function DwMainButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_1") {
                var bank = objDwMain.GetItem(rowNumber, "frombank");
                var bankbranch = objDwMain.GetItem(rowNumber, "frombranch");
                Gcoop.OpenDlg(455, 200, "w_dlg_bank_list.aspx", "?bank=" + bank + "&bankbranch=" + bankbranch);
            }
        }

        function DwDateSearchButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_search") {
                objDwDateSearch.AcceptText();
                postSearch();
            }
        }

        function DwMainItemChange(sender, rowNumber, columnName, newValue) {
            objDwMain.SetItem(rowNumber, columnName, newValue);
            if (columnName == "bank_code" || columnName == "frombank") {
                objDwMain.AcceptText();
                postBankBranch();
            }
            else if (columnName == "bank_branch") {
                objDwMain.AcceptText();
                postGetChqBook();
            }
            else if (columnName == "cheque_bookno") {
                objDwMain.AcceptText();
                postGetChqNo();
                postInit();
            }
        }

        function DwDetailItemChange(sender, rowNumber, columnName, newValue) {
            objDwDetail.SetItem(rowNumber, columnName, newValue);
            if (columnName == "ai_chqflag") {
                objDwDetail.AcceptText();
                //                postSumChqList();
            }
        }

        function DwDateSearchItemChange(sender, rowNumber, columnName, newValue) {

            if (columnName == "start_tdate") {
                sender.SetItem(rowNumber, "start_tdate", newValue);
                sender.AcceptText();
                sender.SetItem(rowNumber, "start_date", Gcoop.ToEngDate(newValue));
                sender.AcceptText();
                //  postSearch();

            }

        }

        function DwDwtailClick(sender, rowNumber, objectName) {
            if (objectName == "ai_chqflag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "ai_chqflag", 1, 0);
                objDwDetail.AcceptText();
                //                postSumChqList();
            }
        }

        function DwDateSearchClick(sender, rowNumber, objectName) {
            if (objectName == "bank_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "bank_flag", 1, 0);
                objDwDateSearch.AcceptText();
                postCheckBankAll();
            }
        }
        function b_sum_onclick() {
            postSumValue();
        }

        function ClickCheckAll() {
            if (objDwDetail.RowCount() > 0) {
                postcheckAll();
            }
        }

        function GetAccid(accid, accname) {
            objDwMain.SetItem(1, "fromaccount_no", accid);
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table width="100%" border="0">
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
            <td align="center">
                <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_pay_cheque"
                    LibraryList="~/DataWindow/App_finance/paychq_apvloan_cbt.pbl" ClientEventItemChanged="DwMainItemChange"
                    AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    ClientScriptable="True" ClientEventButtonClicked="DwMainButtonClick" ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr align="left">
            <td>
                <dw:WebDataWindowControl ID="DwDateSearch" runat="server" DataWindowObject="d_fn_onedate_bank_chq"
                    LibraryList="~/DataWindow/App_finance/paychq_apvloan_cbt.pbl" ClientScriptable="True"
                    AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true"
                    ClientFormatting="True" ClientEventItemChanged="DwDateSearchItemChange" ClientEventButtonClicked="DwDateSearchButtonClick"
                    ClientEventClicked="DwDateSearchClick">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="CheckAll" runat="server" Text="เลือกทั้งหมด" onclick="ClickCheckAll()" />
                &nbsp&nbsp&nbsp
                <asp:Label ID="lb_showcount" runat="server" Text="จำนวนที่เลือก : "></asp:Label>
                <input style="height: 23px; width: 100px; margin-left: 350px;" id="Button1" type="button"
                    value="รวมยอด" onclick="b_sum_onclick()" />
                <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="395px">
                    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_chequelist_apvloan_cbt"
                        LibraryList="~/DataWindow/App_finance/paychq_apvloan_cbt.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientEventItemChanged="DwDetailItemChange" ClientFormatting="True" ClientEventClicked="DwDwtailClick"
                        RowsPerPage="10">
                        <PageNavigationBarSettings Position="bottom" Visible="True" NavigatorType="Numeric">
                            <BarStyle HorizontalAlign="Center" />
                            <NumericNavigator FirstLastVisible="True" />
                        </PageNavigationBarSettings>
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
