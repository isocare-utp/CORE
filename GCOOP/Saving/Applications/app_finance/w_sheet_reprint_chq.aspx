<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_reprint_chq.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_reprint_chq" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postBankBranch %>
    <%=postChqBook %>
    <%=postChqlistRetrieve %>
    <%=postPrint %>
    <script type="text/javascript">

        function Validate() {
            objDwCon.AcceptText();
            objDwChqSize.AcceptText();
            objDwChqList.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function DwConItemChange(sender, rowNumber, columnName, newValue) {
            objDwCon.SetItem(rowNumber, columnName, newValue);
            if (columnName == "as_bank") {
                objDwCon.AcceptText();
                postBankBranch();
            }
            else if (columnName == "as_bankbranch") {
                objDwCon.AcceptText();
                postChqBook();
            } else if (columnName == "adtm_tdate") {
                objDwCon.SetItem(rowNumber, "adtm_date", Gcoop.ToEngDate(newValue));
                objDwCon.AcceptText();
                alert(objDwCon.GetItem(rowNumber, "adtm_date") + "" + columnNames);

            }
            objDwCon.AcceptText();

        }

        function DwChqListClick(sender, rowNumber, objectName) {
            if (objectName == "ai_check") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "ai_check", 1, 0);
                objDwChqList.AcceptText();
            }
        }

        function DwConButtonClick(sender, rowNumber, buttonName) {
            objDwCon.AcceptText();
            postChqlistRetrieve();
        }

        function b_print_onclick() {
            postPrint();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table border="0" width="100%">
        <tr>
            <td colspan="2">
                เลือกเครื่องพิมพ์ :
                <asp:DropDownList ID="DdPrintSetProfile" runat="server" Width="200px">
                    <asp:ListItem Text="printfin-23" Value="printfin-23" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="printfin-22" Value="printfin-22"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="10%">
                <dw:WebDataWindowControl ID="DwChqSize" runat="server" DataWindowObject="d_chqprint_chqsize"
                    LibraryList="~/DataWindow/App_finance/reprint.pbl" ClientScriptable="True" AutoRestoreContext="false"
                    AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true">
                </dw:WebDataWindowControl>
            </td>
            <td width="40%">
                <dw:WebDataWindowControl ID="DwCon" runat="server" DataWindowObject="d_conditionprint_chequefromchq"
                    LibraryList="~/DataWindow/App_finance/reprint.pbl" ClientScriptable="True" AutoRestoreContext="false"
                    AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true" ClientEventItemChanged="DwConItemChange"
                    ClientEventButtonClicked="DwConButtonClick">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr align="right">
            <td align="right" colspan="2">
                <input style="height: 30px; width: 100px" id="Button2" type="button" value="สั่งพิมพ์"
                    onclick="b_print_onclick()" />
            </td>
        </tr>
    </table>
    <hr />
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwChqList" runat="server" DataWindowObject="d_chequelist_fromcheque"
                    LibraryList="~/DataWindow/App_finance/reprint.pbl" ClientScriptable="True" AutoRestoreContext="false"
                    AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true" ClientEventClicked="DwChqListClick">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
</asp:Content>
