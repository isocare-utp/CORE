<%@ Page Title="Untitled Page" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_reclint.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_reclint" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postNewAccount%>
    <%=Pcalculate%>
    <%=newClear%>
    <script type="text/javascript">
        //        function cal() {
        //            objDwMain.SetItem(1, "deptpassbook_no", newBookNo);
        //        }

        // เขียน function ให้หน้า .cs เรียกใช้
        function OnDwMainItemChanged(sender, rownumber, columnname, newvalue) {
            sender.SetItem(rownumber, columnname, newvalue);
            sender.AcceptText();
            if (columnname == "deptaccount_no") {
                //alert(sender + rownumber + columnname + newvalue);
                postNewAccount();
            }
            else if (columnname == "operate_tdate") {
                sender.SetItem(rownumber, "operate_date", Gcoop.ToEngDate(newvalue));
                sender.AcceptText();
            }
        }
        function OnDwMainItemClicked(sender, rownumber, columnname, newvalue) {
            sender.AcceptText();
            if (columnname == "calculate") {

                Pcalculate();
            }
        }

        function MenubarNew() {
            if (confirm("ยืนยันการล้างหน้าจอ")) {
                newClear();
            }
        }


        function NewAccountNo(coopId, accNo) {

            DwMain.SetItem(1, "deptaccount_no", Gcoop.Trim(accNo));
            DwMain.AcceptText();
            Gcoop.GetEl("HdnewAcc").value = accNo;
            postNewAccount();
        }
        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dept_head_int_recal"
                    LibraryList="~/DataWindow/ap_deposit/dp_recalint.pbl" ClientEventItemChanged="OnDwMainItemChanged"
                    ClientEventButtonClicked="OnDwMainItemClicked">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwDetail" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" DataWindowObject="d_dp_cm_stm_recalint_loadbyseq_test"
                    LibraryList="~/DataWindow/ap_deposit/dp_recalint.pbl" RowsPerPage="10" ClientScriptable="True"
                    ClientFormatting="True" ClientEventClicked="OnDwDetailClicked">
                    <PageNavigationBarSettings Position="Bottom" Visible="True" NavigatorType="Numeric">
                        <BarStyle HorizontalAlign="Center" />
                        <NumericNavigator FirstLastVisible="True" />
                    </PageNavigationBarSettings>
                </dw:WebDataWindowControl>
                <%--
                     <PageNavigationBarSettings Position="Bottom" Visible="True" NavigatorType="Numeric">
                        <BarStyle HorizontalAlign="Center" />
                        <NumericNavigator FirstLastVisible="True" />
                     </PageNavigationBarSettings>--%>
            </td>
        </tr>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwDetailBouns" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" DataWindowObject="d_dp_showbouns"
                    LibraryList="~/DataWindow/ap_deposit/dp_recalint.pbl" RowsPerPage="10" ClientScriptable="True"
                    ClientFormatting="True" ClientEventClicked="OnDwDetailClicked">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td height="10">
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="operate_tdate" runat="server" />
    <asp:HiddenField ID="deptaccount_no" runat="server" />
    <asp:HiddenField ID="deptaccount_name" runat="server" />
    <asp:HiddenField ID="seq_no" runat="server" />
    <asp:HiddenField ID="deptitemtype_code" runat="server" />
    <asp:HiddenField ID="compute_2" runat="server" />
    <asp:HiddenField ID="eptitem_amt" runat="server" />
    <asp:HiddenField ID="prncbal" Value="" runat="server" />
    <asp:HiddenField ID="int_amt" runat="server" />
    <asp:HiddenField ID="accuint_amt" runat="server" />
    <asp:HiddenField ID="compute_3" Value="" runat="server" />
    <asp:HiddenField ID="compute_4" runat="server" />
    <asp:HiddenField ID="HdClickedSeqNo" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" />
    <asp:HiddenField ID="HdnewAcc" runat="server" />
</asp:Content>
