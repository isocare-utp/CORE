<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_mbshr_apv_chgmthshr.aspx.cs"
    Inherits="Saving.Applications.mbshr.w_sheet_mbshr_apv_chgmthshr" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postInit%>
    <%=postSetStatus%>
    <%=postRequestStatus%>
    <script type="text/javascript">
        function Validate() {
            objDw_option.AcceptText();
            objDw_list.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDwOptionButtonClick(s, r, b) {
            if (b == "b_sch") {
                objDw_option.AcceptText();
                postInit();
            }
        }

        function OnDwListButtonClick(s, r, b) {
            if (objDw_list.RowCount() > 0) {
                if (b == "b_wait" || b == "b_apv" || b == "b_noapv") {
                    Gcoop.GetEl("Hdbutton").value = b;
                    postSetStatus();
                }
            }
        }

        function OnDwListItemChange(s, r, c, v) {
            if (c == "shrpayadj_status") {
                objDw_list.SetItem(r, "shrpayadj_status", v);
                objDw_list.AcceptText();
                Gcoop.GetEl("HdRow").value = r + "";
                postRequestStatus();
            }
            else if (c == "approve_tdate") {
                objDw_list.SetItem(1, "approve_tdate", v);
                objDw_list.AcceptText();
                objDw_list.SetItem(1, "approve_date", Gcoop.ToEngDate(v));
                objDw_list.AcceptText();
            }            
        }

        function OnDwOptionItemChange(s, r, c, v) {
            if (c == "docreq_stdate") {
                objDw_list.SetItem(1, "docreq_stdate", v);
                objDw_list.AcceptText();
                objDw_list.SetItem(1, "docreq_sdate", Gcoop.ToEngDate(v));
                objDw_list.AcceptText();
            }
            else if (c == "docreq_etdate") {
                objDw_list.SetItem(1, "docreq_etdate", v);
                objDw_list.AcceptText();
                objDw_list.SetItem(1, "docreq_edate", Gcoop.ToEngDate(v));
                objDw_list.AcceptText();
            }
            if (c == "member_sno") {
                objDw_option.SetItem(r, "member_eno", v);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr>
                <td>
                    <dw:WebDataWindowControl ID="Dw_option" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" DataWindowObject="d_mbsrv_list_apvchgmthshr_opt" LibraryList="~/DataWindow/mbshr/mb_apvchgmthshr.pbl"
                        Style="top: 0px; left: 0px" ClientEventButtonClicked="OnDwOptionButtonClick"  ClientEventItemChanged="OnDwOptionItemChange">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <strong>รายละเอียดอนุมัติเปลี่ยนแปลงค่าหุ้น</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server" Height="400px">
                        <dw:WebDataWindowControl ID="Dw_list" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                            DataWindowObject="d_mbsrv_list_apvchgmthshr" LibraryList="~/DataWindow/mbshr/mb_apvchgmthshr.pbl"
                            Style="top: 0px; left: 0px" ClientEventButtonClicked="OnDwListButtonClick" ClientEventItemChanged="OnDwListItemChange">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:HiddenField ID="Hdbutton" runat="server" />
                    <asp:HiddenField ID="HdRow" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
