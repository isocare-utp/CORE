<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_kp_req_expense.aspx.cs"
    Inherits="Saving.Applications.keeping.w_sheet_kp_req_expense" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostMember%>
    <%=initJavaScript%>
    <%=newClear%>
    <%=jsRetreiveDwdetail %>
    <%=jsChangeEnpenseBank %>
    <%=jsAddBtn %>
    <%=jsRefresh %>
    
    <script type="text/javascript">
        function Validate() {
            objdw_main.AcceptText();
            objdw_detail.AcceptText();
            return true; // confirm("ยืนยันการบันทึกข้อมูล");
        }
        function MenubarNew() {
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            newclear();
        }
        function MenubarOpen() {
            Gcoop.OpenDlg('580', '590', 'w_dlg_kp_member_search.aspx', '');
        }

        function Click_search(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenDlg('580', '590', 'w_dlg_kp_member_search.aspx', '');

            }

        }
        function GetValueFromDlg(memberno) {
            alert(memberno);
            objdw_main.SetItem(1, "member_no", memberno);
            objdw_main.AcceptText();
            Gcoop.GetEl("Hfmember_no").value = memberno;
            jsPostMember();
        }

        function GetBankFromDlg(bank_code) {
            objdw_detail.SetItem(Gcoop.GetEl("HiddenRowexpenseBank").value, "expense_bank", bank_code);
            objdw_detail.AcceptText();

        }
        function GetBankBranchFromDlg(branch_id) {
            objdw_detail.SetItem(Gcoop.GetEl("HiddenRowexpenseBankBranch").value, "expense_branch", branch_id);
            objdw_detail.AcceptText();
        }

        function GetDeptNoFromDlg(deptaccount_no) {
            objdw_detail.SetItem(Gcoop.GetEl("HiddenRowexpenseaccid").value, "expense_accid", deptaccount_no);
            objdw_detail.AcceptText();
        }
        function itemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "00000000"));
                objdw_main.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objdw_main.GetItem(rowNumber, "member_no");
                jsPostMember();
            }

            return 0;

        }

        function OnClickAddRow() {

            jsAddBtn();
        }

        function OnDwdetailButtonClicked(sender, rowNumber, columnName) {


            if (columnName == "btn_del") {

                if (confirm("คุณต้องการลบรายการแถว " + rowNumber + " ใช่หรือไม่?")) {
                    objdw_detail.DeleteRow(rowNumber);
                    //alert("อย่าลืมบันทึกรายการ");
                }

            }
            if (columnName == "btn_add") {
                Gcoop.GetEl("HiddenRowdetail").value = rowNumber;
                jsAddBtn();
            }

            if (columnName == "b_search_expbank") {
                Gcoop.OpenDlg('580', '590', 'w_dlg_kp_bank_search.aspx', '');
                Gcoop.GetEl("HiddenRowexpenseBank").value = rowNumber;
            }
            if (columnName == "b_search_expbranch") {
                var bank_code = objdw_detail.GetItem(rowNumber, "expense_bank");
                Gcoop.OpenDlg(580, 590, "w_dlg_kp_bankbranch_search.aspx", "?bank_code=" + bank_code + "");
                Gcoop.GetEl("HiddenRowexpenseBankBranch").value = rowNumber;
                Gcoop.GetEl("HiddenBankcode").value = bank_code;
            }
            if (columnName == "b_search_deptno") {
                var member_no = Gcoop.GetEl("Hfmember_no").value;
                Gcoop.OpenDlg(580, 590, "w_dlg_kp_deptaccount_search.aspx", "?member_no=" + member_no + "");
                Gcoop.GetEl("HiddenRowexpenseaccid").value = rowNumber;

            }
            return 0;
        }


        function SheetLoadComplete() {

            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
        }


        function OnDwdetailItemChange(sender, rowNumber, columnName, newValue) {
            if (columnName == "chg_status") {
                objdw_detail.SetItem(rowNumber, columnName, newValue);
                objdw_detail.AcceptText();
                Gcoop.GetEl("HiddenRowdetail").value = rowNumber;
                jsRefresh();
            }

            if (columnName == "moneytype_code") {
                objdw_detail.SetItem(rowNumber, columnName, newValue);
                objdw_detail.AcceptText();
                Gcoop.GetEl("HiddenRowdetail").value = rowNumber;
                jsRefresh();
            }
            if (columnName == "monthlycut_type") {
                objdw_detail.SetItem(rowNumber, columnName, newValue);
                objdw_detail.AcceptText();
                Gcoop.GetEl("HiddenRowdetail").value = rowNumber;
                jsRefresh();
            }
            if (columnName == "expense_bank") {
                objdw_detail.SetItem(rowNumber, columnName, newValue);
                objdw_detail.AcceptText();
                Gcoop.GetEl("HiddenRowdetail").value = rowNumber;
                //jsRefresh();
                //jsSearchBranch();
                jsChangeEnpenseBank();
            }
            return 0;

        }

       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="Hidmembsection" runat="server" />
    <asp:HiddenField ID="Hidgroup" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:HiddenField ID="HiddenRowlist" runat="server" Value="false" />
    <asp:HiddenField ID="HiddenRowdetail" runat="server" Value="false" />
    <asp:HiddenField ID="HiddenRowexpenseBank" runat="server" Value="false" />
    <asp:HiddenField ID="HiddenRowexpenseBankBranch" runat="server" Value="false" />
    <asp:HiddenField ID="HiddenRowexpenseaccid" runat="server" Value="false" />
    <asp:HiddenField ID="HiddenBankcode" runat="server" Value="false" />
    <asp:TextBox ID="TextDwmain" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="Textdwhistory" runat="server" Visible="False"></asp:TextBox>
    <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_kp_req_expense_main"
        LibraryList="~/DataWindow/keeping/kp_req_expense.pbl" ClientScriptable="True"
        ClientEventItemChanged="itemChanged" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientEventButtonClicked="Click_search"
        ClientEventClicked="checkMain" TabIndex="1">
    </dw:WebDataWindowControl>
    <table style="width: 100%;">
        <tr>
            <td align="left">
                <span style="cursor: pointer" onclick="OnClickAddRow();">เพิ่มแถว </span>
            </td>
            <td align="right">
            </td>
        </tr>
    </table>
    <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_kp_req_expense_detail"
        LibraryList="~/DataWindow/keeping/kp_req_expense.pbl" ClientScriptable="True"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" TabIndex="600" ClientEventButtonClicked="OnDwdetailButtonClicked"
        Height="200" Width="700" ClientEventItemChanged="OnDwdetailItemChange">
    </dw:WebDataWindowControl>
</asp:Content>
