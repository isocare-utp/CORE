<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_divsrv_req_methpay.aspx.cs" Inherits="Saving.Applications.Divavg.w_sheet_divsrv_req_methpay" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postRefresh%>
    <%=postInit %>
    <%=postRefresh%>
    <%=postInsertRow %>
    <%=postInitMemberNo%>
    <%=postSetMoneytype %>
    <%=postSetDeptAccountNo %>
    <%=postSumReq %>>
    <script type="text/JavaScript">
        //Function Main
        //=============================================================
        function OnDwMainClick(s, r, c) {
            if (c == "sequest_flag") {
                Gcoop.CheckDw(s, r, c, "sequest_flag", 1, 0);
                postRefresh();
            }
        }
        function SetDeptAccount(deptcoop_id, deptaccount_no) {
            Gcoop.GetEl("Hdacccoop_id").value = deptcoop_id;
            Gcoop.GetEl("Hdacountno").value = deptaccount_no;
            postSetDeptAccountNo();
        }
//        function GetMemberNoFromDialog(member_no) {
//            Gcoop.GetEl("Hdmember_no").value = member_no;
//            postInitMemberNo();
//                }
        function GetValueFromDlg(member_no) {
            Gcoop.GetEl("Hdmember_no").value = member_no;
            postInitMemberNo();
                }
        

        function GetDlgBankAndBranch(bankCode, bankDesc, branchCode, branchDesc, expense_bank_type, expense_accid) {
            var row = Gcoop.GetEl("Hdrow").value;
            objDw_detail.SetItem(row, "expense_bank", bankCode);
            objDw_detail.SetItem(row, "expense_branch", branchCode);
            objDw_detail.SetItem(row, "expense_accid", expense_accid);
            objDw_detail.SetItem(row, "expense_bank_typ", expense_bank_type);
            objDw_detail.AcceptText();
            postRefresh();
        }

        function OnDwMainItemChange(s, r, c, v) {
            if (c == "member_no") {
                objDw_main.SetItem(1, "member_no", v);
                objDw_main.AcceptText();
                postInit();
            }
            else if (c == "methreq_tdate") {
                s.SetItem(1, "methreq_tdate", v);
                s.AcceptText();
                s.SetItem(1, "methreq_date", Gcoop.ToEngDate(v));
                s.AcceptText();
            }
            else if (c == "div_year") {
                objDw_main.SetItem(1, "div_year", v);
                objDw_main.AcceptText();
                Gcoop.GetEl("Hddiv_year").value = v;
            }
        }

        function OnDwMainButtonClick(s, r, b) {
            if (b == "b_search_memno") {
//                Gcoop.OpenIFrame("800", "500", "w_dlg_divsrv_search_mem.aspx", "");
                Gcoop.OpenIFrame2("800", "500", "w_dlg_divsrv_member_search.aspx", "");
            }
        }


        //Function Detail
        //=============================================================
        function AddRow() {
            postInsertRow();
        }

        function OnDwDetailItemChange(s, r, c, v) {
            Gcoop.GetEl("Hdrow").value = r + "";
            if (c == "paytype_code") {
                objDw_detail.SetItem(r, "paytype_code", v);
                objDw_detail.AcceptText();
                postRefresh();
            }
            else if (c == "methpaytype_code") {
                objDw_detail.SetItem(r, "methpaytype_code", v);
                objDw_detail.AcceptText();
                postSetMoneytype();
            }
            else if (c == "pay_amt") {
                objDw_detail.SetItem(r, "pay_amt", v);
                objDw_detail.AcceptText();

                var paytype_code = objDw_detail.GetItem(r, "paytype_code");
                if (paytype_code == "PEC") {
                    var pay_amt = objDw_detail.GetItem(r, "pay_amt");
                    if (pay_amt > 100) {
                        alert("ยอดเปอร์เซนต์ มากกว่า 100% กรุณากรอกข้อมูลใหม่");
                    }
                    else {
                        objDw_detail.SetItem(r, "pay_amt", v);
                        objDw_detail.AcceptText();
                    }
                }
                postSumReq();
            }
        }

        function OnDwDetailButtonClick(s, r, b) {
            if (b == "b_search_mthpaytype") {
                Gcoop.GetEl("Hdrow").value = r + "";
                var methpaytype_code = objDw_detail.GetItem(r, "methpaytype_code");
                var moneytype_code = objDw_detail.GetItem(r, "moneytype_code");

                if (methpaytype_code == "LON") {
                    Gcoop.OpenIFrame("300", "300", "w_dlg_divsrv_search_loan.aspx", "");
                }
                else if (methpaytype_code == "DEP") {
                    var member_no = objDw_main.GetItem(1, "member_no");
                    Gcoop.OpenIFrame("800", "300", "w_dlg_divsrv_search_dept.aspx", "?member_no=" + member_no);
                }
                else if (methpaytype_code == "CBT" || moneytype_code == "CHQ") {
                    Gcoop.OpenIFrame("860", "620", "w_dlg_bank_and_branch.aspx", "?sheetRow=" + r + "&moneytype_code=" + moneytype_code);
                }
                else if (methpaytype_code == "SHR") {
                    Gcoop.OpenIFrame("500", "300", "w_dlg_divsrv_search_share.aspx", "");
                }
            } 
            else if (b == "b_del") {
                objDw_detail.DeleteRow(r);
            }
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
        }

        //Function Default
        //=============================================================
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function MenubarOpen() {

        }

        function MenubarNew() {
            postNewClear();
        }

        


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width: 100%;">
            <tr>
                <td>
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                        DataWindowObject="d_divsrv_req_methpay_main" LibraryList="~/DataWindow/divavg/divsrv_req_methpay.pbl"
                        ClientEventItemChanged="OnDwMainItemChange" ClientEventButtonClicked="OnDwMainButtonClick">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <span class="linkSpan" onclick="AddRow()" style="font-family: Tahoma; font-size: small;
                        float: left; color: #3333CC;">เพิ่มแถว </span>
                </td>
            </tr>
            <tr>
                <td>
                    <dw:WebDataWindowControl ID="Dw_detail" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" DataWindowObject="d_divsrv_req_methpay_detail" LibraryList="~/DataWindow/divavg/divsrv_req_methpay.pbl"
                        ClientEventButtonClicked="OnDwDetailButtonClick" ClientEventItemChanged="OnDwDetailItemChange"
                        ClientEventClicked="OnDwMainClick">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="Hdrow" runat="server" />
                    <asp:HiddenField ID="hdbank" runat="server" />
                    <asp:HiddenField ID="hdbranch" runat="server" />
                    <asp:HiddenField ID="hdaccid" runat="server" />
                    <asp:HiddenField ID="Hdmember_no" runat="server" />
                    <asp:HiddenField ID="Hdacountno" runat="server" />
                    <asp:HiddenField ID="Hddiv_year" runat="server" />
                    <asp:HiddenField ID="Hdacccoop_id" runat="server" />
                     <asp:HiddenField ID="HdIsPostBack" runat="server" Value="true" />
                </td>
            </tr>
        </table>
        <br />
    </p>
</asp:Content>
