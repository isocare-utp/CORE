<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_bg_budgetsetamt.aspx.cs"
    Inherits="Saving.Applications.account.w_sheet_bg_budgetsetamt" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=RetrieveList%>
    <%=jsPostInsertRow%>
    <%=jsPostDeleteRow%>
    <%=jsPostGetAccount%>
    <%=jsPostGetAllBudget%>
    <%=jsPostYear%>
    <%=jsPostBGType%>
    <%=jsPostInsertBfRow%>
    <%=jsPostMoveUp%>
    <%=jsPostMoveDown%>
    <%=jsSetBGDetail%>>
    <script type="text/javascript">
        function OnDwListClicked(sender, row, col) {
            Gcoop.GetEl("HdrowSelect").value = row + "";
        }

        function Validate() {
            var isconfirm = confirm("ยืนยันการบันทึกข้อมูล ?");

            if (!isconfirm) {
                return false;
            }

            var rowcount = objDwList.RowCount();
            var alert = "";

            for (var i = 1; i <= rowcount; i++) {
                var accbudgetgroup_typ = objDwList.GetItem(i, "accbudgetgroup_typ");
                var budget_detail = objDwList.GetItem(i, "budget_detail");
                var account_budget = objDwList.GetItem(i, "account_budget");
                var account_id_list = objDwList.GetItem(i, "account_id_list");
                var budget_type = objDwList.GetItem(i, "budget_type");

                if (!(accbudgetgroup_typ != "" && accbudgetgroup_typ != null && budget_detail != "" && budget_detail != null)) {
                    alert = "กรุณาระบุข้อมูลให้ครบถ้วน";
                    break;
                }
            }

            if (alert != "") {
                confirm(alert);
                return false;
            }
            else {
                return true;
            }
        }

        function OnDwListItemChanged(sender, rowNumber, columnName, newValue) {
            sender.SetItem(rowNumber, columnName, newValue);
            sender.AcceptText();
            if (columnName == "account_budget") {
                var setAmt = objDwList.GetItem(rowNumber, columnName);
                if (setAmt < 0) {
                    alert("จำนวนเงินควรมีค่ามากกว่า 0");
                    objDwList.SetItem(rowNumber, columnName, 0);
                }
                initJavaScript();
            }
            else if (columnName == "budget_type") {
                Gcoop.GetEl("Hdrow").value = rowNumber + "";
                jsPostBGType();
            }
            else if (columnName == "accbudgetgroup_typ") {
                Gcoop.GetEl("Hdrow").value = rowNumber + "";
                jsSetBGDetail();
            }
        }

        function OnDwMainButtonClicked(sender, rowNumber, buttonName) {
            if (buttonName == "cb_ok") {
                var bgYear = 0;
                try {
                    bgYear = objDwMain.GetItem(1, "budgetyear");
                } catch (err) { bgYear = 0; }
                if (bgYear == 0 || bgYear == null) {
                    alert("กรุณาเลือกปีงบประมาณก่อน");
                }
                else {
                    Gcoop.GetEl("HdRetrive").value = "True";
                    RetrieveList();
                }
            }
        }

        function OnDwmainInsertBfRow() {
            if (Gcoop.GetEl("HdRetrive").value == "True") {
                jsPostInsertBfRow();
            }
        }
        function OnDwmainInsertRow() {
            if (Gcoop.GetEl("HdRetrive").value == "True") {
                jsPostInsertRow();
            }
        }

        function OnDwListButtonClick(sender, row, bName) {
            if (bName == "b_del") {
                var budget_detail = objDwList.GetItem(row, "budget_detail");
                var accbudgetgroup_typ = objDwList.GetItem(row, "accbudgetgroup_typ");

                if (accbudgetgroup_typ == "" || accbudgetgroup_typ == null) {
                    Gcoop.GetEl("Hdrow").value = row + "";
                    jsPostDeleteRow();
                } else {
                    var isConfirm = confirm("ต้องการลบข้อมูลงบประมาณแถวที่ [" + row + "] " + budget_detail + " ใช่หรือไม่ ?");
                    if (isConfirm) {
                        Gcoop.GetEl("Hdrow").value = row + "";
                        jsPostDeleteRow();
                    }
                }
            }
            else if (bName == "b_account") {
                Gcoop.GetEl("Hdrow").value = row + "";
                var budget_type = objDwList.GetItem(row, "budget_type");
                if (budget_type == "D") {
                    var account_id_list = objDwList.GetItem(row, "account_id_list");
                    if (account_id_list == null) {
                        account_id_list = "";
                    }
                    Gcoop.OpenIFrame(950, 550, "w_dlg_select_account.aspx", "?acc_list=" + account_id_list);
                }
            }
            else if (bName == "b_up") {
                if (row > 1) {
                    Gcoop.GetEl("Hdrow").value = row + "";
                    jsPostMoveUp();
                }
            }
            else if (bName == "b_down") {
                if (row < sender.RowCount()) {
                    Gcoop.GetEl("Hdrow").value = row + "";
                    jsPostMoveDown();
                }
            }
            return 0;
        }

        function GetAccount(acc_id) {
            Gcoop.GetEl("Hdacclist").value = acc_id;
            jsPostGetAccount();
        }

        function OnGetAllBudget() {
            if (Gcoop.GetEl("HdRetrive").value == "True") {
                jsPostGetAllBudget();
            }
        }

        function OnCopyBudget() {
            if (Gcoop.GetEl("HdRetrive").value == "True") {
                Gcoop.OpenIFrame(370, 50, "w_dlg_getyear.aspx", "");
            }
        }

        function GetYear(year) {
            Gcoop.GetEl("Hdyear").value = year + "";
            jsPostYear();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table width="100%">
        <tr>
            <td align="left">
                <asp:Label ID="Label1" runat="server" Text="ตั้งค่างบประมาณ" Font-Bold="True" Font-Names="Tahoma"
                    Font-Size="16px" Font-Underline="True" />
            </td>
        </tr>
    </table>
    <br />
    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_bg_budgetsetamt_main"
        LibraryList="~/DataWindow/account/budget.pbl" ClientFormatting="True" ClientEventButtonClicked="OnDwMainButtonClicked">
    </dw:WebDataWindowControl>
    <br />
    <asp:Label ID="Label3" runat="server" Text="รายละเอียดรายการ" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="14px" Font-Underline="True" /><br />
    <br />
    <span onclick="OnDwmainInsertRow()" style="cursor: pointer;">
        <asp:Label ID="LbInsert2" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="Blue" />
    </span><span onclick="OnDwmainInsertBfRow()" style="cursor: pointer; margin-left: 5%;">
        <asp:Label ID="Label2" runat="server" Text="แทรกแถว" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="Blue" />
    </span><span onclick="OnGetAllBudget()" style="cursor: pointer; margin-left: 44%;">
        <asp:Label ID="Label5" runat="server" Text="ดึงงบประมาณทั้งหมด" Font-Bold="False"
            Font-Names="Tahoma" Font-Size="14px" Font-Underline="True" ForeColor="Blue" Font-Strikeout="False" />
    </span><span onclick="OnCopyBudget()" style="cursor: pointer; margin-left: 5%;">
        <asp:Label ID="Label4" runat="server" Text="คัดลอกงบประมาณ" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="Blue" Font-Strikeout="False" />
    </span>
    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" ScrollBars="Auto" Height="360px"
        Width="750px">
        <dw:WebDataWindowControl ID="DwList" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_acc_set_budget"
            LibraryList="~/DataWindow/account/budget.pbl" ClientFormatting="True" ClientEventItemChanged="OnDwListItemChanged"
            Height="360px" Width="750px" ClientEventButtonClicked="OnDwListButtonClick" ClientEventClicked="OnDwListClicked">
        </dw:WebDataWindowControl>
    </asp:Panel>
    <asp:HiddenField ID="HdRetrive" runat="server" />
    <asp:HiddenField ID="Hdrow" runat="server" />
    <asp:HiddenField ID="Hdacclist" runat="server" />
    <asp:HiddenField ID="Hdyear" runat="server" />
    <asp:HiddenField ID="HdrowSelect" runat="server" Value="1" />
</asp:Content>
