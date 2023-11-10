<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_loancredit.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loancredit" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=jsPostMember%>
    <%=jsPostLnrcvList%>>
    <%=jsPostCancelList%>>
    <%=jsGetexpensememno%>
    <%=jsExpensebankbrRetrieve%>
    <%=jsExpenseCode%>
    <%=jsExpenseBank%>
    <script type="text/javascript">
        // save
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        // ItemChange dw_main 
        function ItemChangedMain(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objdw_main.GetItem(rowNumber, "member_no");
                jsPostMember();
            }

        }
        function ListClick(s, r, c) {
            // Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0); 
            Gcoop.GetEl("HfContNo").value = r;
            //Gcoop.GetEl("HfContNo").value = objdw_list.GetItem(r, "contcredit_no");
            jsPostLnrcvList();

        }
        //        function OnClickAdd(sender, rowNumber, objectName) {
        //            var memno = objdw_main.GetItem(rowNumber, "member_no");
        //            if (memno == "" && memno != null && memno != '-1') {
        //                Gcoop.OpenIFrame("650", "560", "w_dlg_sl_contcredit_new.aspx", "?memno=" + memno);
        //            }

        //        }
        //เรียกหน้าจอใหม่
        function MenubarNew() {
            newClear();
        }

        function OnClickAdd(sender, rowNumber, columnName) {
            if (columnName == "addcont") {
                memno = objdw_main.GetItem(rowNumber, "member_no");
                if (rowNumber != 0) {
                    var lnstatus = Gcoop.GetEl("HflnStatus").value;

                    if (lnstatus == 0) {
                        Gcoop.OpenIFrame("580", "600", "w_dlg_sl_contcredit_new.aspx", "?memno=" + memno);
                    } else {
                        alert("สมาชิกทะเบียน " + memno + " นี้ งดกู้เงินทุกประเภท");
                    }
                }
            }

        }
        //        btn_cancel

        function BtnDwListDetClick(s, r, c) {
            if (c == "btn_cancel") {
                jsPostCancelList();
            }
            else if (c == "b_retre") {
            jsGetexpensememno();
            }
    }
    function ItemDwMainChanged(sender, rowNumber, columnName, newValue) {
        objdw_listDet.SetItem(rowNumber, columnName, newValue);
        objdw_listDet.AcceptText();
       
        if (columnName == "loanrcv_code") {
            objdw_listDet.SetItem(rowNumber, columnName, newValue);
            objdw_listDet.AcceptText();

            jsExpenseCode();

        } else if (columnName == "loanrcv_bank") {

          
            objdw_listDet.SetItem(rowNumber, columnName, newValue);
            objdw_listDet.AcceptText();
            jsExpenseBank();
        } else if (columnName == "loanrcv_bank_1") {


            objdw_listDet.SetItem(rowNumber, columnName, newValue);
            objdw_listDet.AcceptText();
            jsExpenseBank();



        } else if (columnName == "loanrcv_branch") {

            objdw_listDet.SetItem(rowNumber, columnName, newValue);
            objdw_listDet.AcceptText();
           // jsExpensebankbrRetrieve();
        }
        else if (columnName == "loanrcv_branch_1") {

            objdw_listDet.SetItem(rowNumber, "loanrcv_branch", newValue);
            objdw_listDet.AcceptText();

        }
    }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="2" valign="top">
                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_sl_contcredit_det_member1"
                    LibraryList="~/DataWindow/Shrlon/sl_loancredit.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    Width="720px" ClientEventItemChanged="ItemChangedMain" ClientEventButtonClicked="OnClickAdd">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <dw:WebDataWindowControl ID="dw_list" runat="server" ClientEventClicked="ListClick"
                    DataWindowObject="d_sl_contcredit_det_contlist1" LibraryList="~/DataWindow/Shrlon/sl_loancredit.pbl"
                    AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    ClientScriptable="True" ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
            <td rowspan="2" valign="top">
                <dw:WebDataWindowControl ID="dw_listDet" runat="server" DataWindowObject="d_sl_contcredit_det_contdet1"
                    LibraryList="~/DataWindow/Shrlon/sl_loancredit.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="True" ClientEventClicked="DwListDetClick" ClientEventButtonClicked="BtnDwListDetClick"
                    ClientEventItemChanged="ItemDwMainChanged">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td valign="top">
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="HfContNo" runat="server" />
    <asp:HiddenField ID="HflnStatus" runat="server" />
</asp:Content>
