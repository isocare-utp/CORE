<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_collateral_check.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_collateral_check" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript  %>
    <%=refresh  %>
    <%=openNew  %>
    <%=jsPostCheckColl %>
    <script type="text/javascript">
        function Validate() {
            alert("ไม่สามารถบันทึกข้อมูลได้");
        }
        function ItemChangeDwMain(sender, rowNumber, columnName, newValue) {
            var collTypeCode = objdw_main.GetItem(1, "colltype_code");

            if (columnName == "colltype_code") {
                objdw_main.SetItem(1, "colltype_code", newValue);
                jsPostCheckColl();
            } else if (columnName == "collateral_no") {

                var collNoT = Gcoop.Trim(newValue);
                Gcoop.GetEl("Hdrefcoll_no").value = collNoT;
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();

                jsPostCheckColl();
            }

            //            else if ((columnName == "collateral_no")||(collTypeCode == "03")){
            //                objdw_main.SetItem( 1, "collateral_no", newValue );
            //                objdw_main.AcceptText();
            //                jsPostCheckColl();
            //            }else if ((columnName == "collateral_no")||(collTypeCode == "04")){
            //               objdw_main.SetItem( 1, "collateral_no", newValue );
            //                objdw_main.AcceptText();
            //                jsPostCheckColl();
            //            }
        }
        function MenubarNew() {
            openNew();
        }
        function OnDwMainButtonClicked(sender, rowNumber, buttonName) {
            var collTypeCode = objdw_main.GetItem(1, "colltype_code");
            Gcoop.GetEl("HdReturn").value = "1";
            if ((buttonName == "b_search") && (collTypeCode == '01')) {
                Gcoop.GetEl("HdColumnName").value = buttonName;
                Gcoop.OpenDlg('600', '600', 'w_dlg_sl_member_search.aspx', '');
            } else if ((buttonName == "b_search") && (collTypeCode == '03')) {
                Gcoop.OpenDlg('600', '450', 'w_dlg_dp_account_search.aspx', '');
            }
        }
        function GetValueFromDlg(memberno) {
            var colunmName = Gcoop.GetEl("HdColumnName").value;
            //alert(memberno);
            if (colunmName == "b_search") {
                Gcoop.GetEl("Hdrefcoll_no").value = memberno;
                objdw_main.SetItem(1, "collateral_no", memberno);
                objdw_main.AcceptText();
                jsPostCheckColl();
            }
        }
        function NewAccountNo(dept_no) {
            objdw_coll.SetItem(1, "collateral_no", dept_no);
            objdw_coll.AcceptText();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="2">
                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_sl_collcheck_main"
                    LibraryList="~/DataWindow/Shrlon/sl_collateral_check.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientEventItemChanged="ItemChangeDwMain" ClientEventButtonClicked="OnDwMainButtonClicked">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td rowspan="2" valign="top">
                <%--สำหรับใส่ DW ต่างๆตามประเภทคนค้ำ--%>
                <dw:WebDataWindowControl ID="dw_memdet1" runat="server" DataWindowObject="d_sl_collcheck_detail_memdet"
                    LibraryList="~/DataWindow/Shrlon/sl_collateral_check.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
                <dw:WebDataWindowControl ID="dw_memcoll1" runat="server" DataWindowObject="d_sl_coll_list_wa"
                    LibraryList="~/DataWindow/Shrlon/sl_collateral_check.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
                <dw:WebDataWindowControl ID="dw_sharedet2" runat="server" DataWindowObject="d_sl_collcheck_detail_sharedet"
                    LibraryList="~/DataWindow/Shrlon/sl_collateral_check.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
                <dw:WebDataWindowControl ID="dw_deptdet3" runat="server" DataWindowObject="d_sl_collcheck_detail_deptdet"
                    LibraryList="~/DataWindow/Shrlon/sl_collateral_check.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
                <dw:WebDataWindowControl ID="dw_collmast4" runat="server" DataWindowObject="d_sl_collcheck_detail_collmast"
                    LibraryList="~/DataWindow/Shrlon/sl_collateral_check.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
                <dw:WebDataWindowControl ID="dw_collmemco4" runat="server" DataWindowObject="d_sl_collcheck_detail_collmemco"
                    LibraryList="~/DataWindow/Shrlon/sl_collateral_check.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="Label2" runat="server" Text="รายการค้ำประกันสัญญา"></asp:Label>
                <dw:WebDataWindowControl ID="dw_collwho" runat="server" DataWindowObject="d_sl_collcheck_det_contno_wa"
                    LibraryList="~/DataWindow/Shrlon/sl_collateral_check.pbl" Width="530px" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                    Height="200px">
                </dw:WebDataWindowControl>
                <br />
                <asp:Label ID="Label3" runat="server" Text="รายการค้ำประกันใบคำขอ"></asp:Label>
                <dw:WebDataWindowControl ID="dw_collwholnreq" runat="server" DataWindowObject="d_sl_collcheck_det_reqloan_wa"
                    LibraryList="~/DataWindow/Shrlon/sl_collateral_check.pbl" ClientScriptable="True"
                    AutoRestoreContext="False" AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True"
                    ClientFormatting="True" Width="530px" Height="200px">
                </dw:WebDataWindowControl>
                <br />
            </td>
        </tr>
        <asp:HiddenField ID="HdReturn" runat="server" />
        <asp:HiddenField ID="HdColumnName" runat="server" />
        <asp:HiddenField ID="Hdrefcoll_no" runat="server" />
    </table>
</asp:Content>
