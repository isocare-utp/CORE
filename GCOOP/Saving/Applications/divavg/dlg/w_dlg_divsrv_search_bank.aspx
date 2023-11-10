<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_divsrv_search_bank.aspx.cs"
    Inherits="Saving.Applications.divavg.dlg.w_dlg_divsrv_search_bank" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 16px;
        }
    </style>
    <%=initJavaScript %>
    <%=postNewClear %>
    <%=postSearchBankBranch%>
    <%=postFilterBranch%>
    <%=postSetBranch%>
    <script type="text/javascript">

        function OnDwMainItemChange(s, r, c, v) {
            //            var expense_bank = objDw_main.GetItem(1, "expense_bank");
            //            var expense_branch = objDw_main.GetItem(1, "expense_branch");

            if (c == "bank_name") {
                Gcoop.GetEl("Hdbank").value = v;
                objDw_main.SetItem(1, "bank_name", v);
                objDw_main.AcceptText();
                Gcoop.GetEl("HdChange").value = c;
                postSearchBankBranch();
            }
            else if (c == "branch_name") {
                Gcoop.GetEl("Hdbranch").value = v;
                objDw_main.SetItem(1, "branch_name", v);
                objDw_main.AcceptText();
                Gcoop.GetEl("HdChange").value = c;
                postSearchBankBranch();
            }
           
        }

        function OnDwMainButtonClick(s, r, b) {
            if (b == "b_ok") {
                var expense_bank = objDw_main.GetItem(1, "expense_bank");
                var expense_branch = objDw_main.GetItem(1, "expense_branch");
                var expense_accid = objDw_main.GetItem(1, "expense_accid");
                var expense_bank_typ = objDw_main.GetItem(1, "expense_bank_typ");
                parent.SetExpense(expense_bank, expense_branch, expense_accid, expense_bank_typ)
                parent.RemoveIFrame();
            }
            else if (b == "b_cancel") {
                postNewClear();
            }
            else if (b == "b_search") {

                postSearchBankBranch();
            }
        }

        function OnDwBankClick(s, r, c) {
            var bank_code = objDw_bank.GetItem(r, "bank_code");
            Gcoop.GetEl("Hdexpensebank").value = bank_code;
            Gcoop.GetEl("HdRowBank").value = r + "";
            postFilterBranch();
        }

        function OnDwBranchClick(s, r, c) {
            var branch_id = objDw_branch.GetItem(r, "branch_id");
            Gcoop.GetEl("Hdexpensebranch").value = branch_id;
            Gcoop.GetEl("HdRowBranch").value = r + "";
            postSetBranch();
        }

       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width: 100%;">
            <tr>
                <td colspan="4">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                        DataWindowObject="d_divsrv_search_bank" LibraryList="~/DataWindow/divavg/divsrv_req_methpay.pbl"
                        Style="top: 0px; left: 0px" ClientEventButtonClicked="OnDwMainButtonClick" ClientEventItemChanged="OnDwMainItemChange">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="Panel2" runat="server" Height="300px" ScrollBars="Auto" Width="370px"
                        BorderStyle="Ridge">
                        <dw:WebDataWindowControl ID="Dw_bank" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                            ClientEventClicked="OnDwBankClick" DataWindowObject="d_dlg_divsrv_search_bank"
                            LibraryList="~/DataWindow/divavg/divsrv_req_methpay.pbl" Style="top: 6px; left: 0px">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td colspan="2">
                    <asp:Panel ID="Panel3" runat="server" Height="300px" ScrollBars="Auto" Width="370px"
                        BorderStyle="Ridge">
                        <dw:WebDataWindowControl ID="Dw_branch" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientEventClicked="OnDwBranchClick" ClientScriptable="True" DataWindowObject="d_dlg_divsrv_search_branch"
                            LibraryList="~/DataWindow/divavg/divsrv_req_methpay.pbl" Style="top: 0px; left: -1px">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;<asp:HiddenField ID="hidden_search" runat="server" />
                    <asp:HiddenField ID="Hdbank" runat="server" />
                    <asp:HiddenField ID="Hdbranch" runat="server" />
                    <asp:HiddenField ID="Hdexpensebank" runat="server" />
                    <asp:HiddenField ID="Hdexpensebranch" runat="server" />
                    <asp:HiddenField ID="HdChange" runat="server" />
                    <asp:HiddenField ID="HdRowBank" runat="server" />
                    <asp:HiddenField ID="HdRowBranch" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
