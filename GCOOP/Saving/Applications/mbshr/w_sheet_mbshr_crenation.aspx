<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_mbshr_crenation.aspx.cs" Inherits="Saving.Applications.mbshr.w_sheet_mbshr_crenation" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postNewClear%>
    <%=postMemberNo%>
    <%=postCmAccount%>
    <%=postMember_delete%>
    <%=postMember_delete_2%>

    <script type="text/javascript">
        function Validate() {
            objdw_main.AcceptText(); 
            if (confirm("ยืนยันการบันทึกข้อมูล")) {
                return true;
            }
        }

        function MenubarNew() {
            postNewClear()
        }

        function ItemDwMainChanged(sender, rowNumber, columnName, newValue) {
             if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();               
                postMemberNo();
            }
        }

        function ItemDwListChanged(sender, rowNumber, columnName, newValue) {

//            if (columnName == "cmtaccount_no") {
//                Gcoop.GetEl("Hdlist_row").value = rowNumber;
//                objdw_list.SetItem(rowNumber, columnName, newValue);
//                objdw_list.AcceptText();
//                postCmAccount();
//            }        
        }
        function OnDwListClicked(s, r, c) {
           
            if (c == "b_addrow") {                
                objdw_list.InsertRow(0);
            }
            else if (c == "b_delete") 
            {              
                Gcoop.GetEl("HRow").value = r;
                var isConfirm = confirm("ยืนยันการลบรายการ");
                if (isConfirm) {
                    postMember_delete();
                }
            }
        }
//        //ประกัน
//        function OnDwList2Clicked(s, r, c) {
//           if (c == "b_addrow") {
//                objdw_list2.InsertRow(0);                
//            } else if (c == "b_delete") {

//                Gcoop.GetEl("HRow_2").value = r;
//                var isConfirm = confirm("ยืนยันการลบรายการ");
//                if (isConfirm) {
//                    postMember_delete_2();
//                }
//            }
//        }

        function GetValueFromDlg(member_no) {
            objdw_main.SetItem(1, "member_no", member_no);
            objdw_main.AcceptText();                     
            postMemberNo();
        }

        function MenubarOpen() {
            Gcoop.OpenDlg('650', '590', 'w_dlg_sl_member_search.aspx', '');
        }
               
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>     
        <asp:HiddenField ID="Hfmember_no" runat="server" />
        <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_mbsrv_crenation"
            LibraryList="~/DataWindow/mbshr/mb_req_trnmb.pbl" ClientScriptable="True"
            ClientEventButtonClicked="MenubarOpen" AutoRestoreContext="False" AutoRestoreDataCache="True"
            ClientEventItemChanged="ItemDwMainChanged" ClientEventClicked="OnDwMainClicked"
            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True">
        </dw:WebDataWindowControl>
</p> 
<p>
        <dw:WebDataWindowControl ID="dw_list" runat="server" DataWindowObject="d_mbsrv_insurance_list2"
            LibraryList="~/DataWindow/mbshr/mb_req_trnmb.pbl" ClientScriptable="True"
            AutoRestoreContext="False" AutoRestoreDataCache="True"
            ClientEventItemChanged="ItemDwListChanged" ClientEventClicked="OnDwListClicked"
            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True">
        </dw:WebDataWindowControl>
        <asp:HiddenField ID="Hdlist_row" runat="server" />
        <asp:HiddenField ID="HRow" runat="server" />
        <asp:HiddenField ID="HRow_2" runat="server" />
        <br><br>
        </p>
        <dw:WebDataWindowControl ID="dw_list2" runat="server" DataWindowObject="d_mbsrv_fund_list"
            LibraryList="~/DataWindow/mbshr/mb_req_trnmb.pbl" ClientScriptable="True"
            AutoRestoreContext="False" AutoRestoreDataCache="True"
            ClientEventItemChanged="ItemDwList2Changed" ClientEventClicked="OnDwList2Clicked"
            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True">
        </dw:WebDataWindowControl>
        
        <asp:HiddenField ID="HiddenField2" runat="server" />
</asp:Content>
