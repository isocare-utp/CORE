<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_atm_newcard.aspx.cs" Inherits="Saving.Applications.atm.w_sheet_atm_newcard" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostMemberNo%>
    <%=jsRetrieve%>
    <%=jsPostBack%>
    <script type="text/JavaScript">
        function Validate() {
            return confirm("ยืนยันข้อมูล เลขทะเบียน: " + objDwMain.GetItem(1, "member_no") + " สาขา: " + objDwMain.GetItem(1, "coop_id"));
        }

        function OnDwMainButtonClicked(s, r, c) {
            switch (c) {
                case "b_1":
                    //Gcoop.OpenDlg(530, 500, "w_dlg_dp_member_search.aspx", "?coopid=001001");
                    break;
            }
        }
        function GetMemberNoFromDlg(Coopid, memberNo) {
            objDwMain.SetItem(1, "member_no", memberNo);
            objDwMain.AcceptText();
            jsPostMemberNo();
        }

        function OnDwMainItemChanged(s, r, c, v) {
            s.SetItem(r, c, v);
            s.AcceptText();
            switch (c) {
                case "member_no":
                    jsPostMemberNo();
                    break;
                case "docetc_flag":
                    if (v == 0) s.SetItem(r, "docetc_desc", "");
                    jsPostBack();
                    break;
                case "atmitemtype_code":
                    if (v == "AND") s.SetItem(r, "atmcard_amt", 50);
                    else if (v == "ANM") s.SetItem(r, "atmcard_amt", 100);
                    break;
            }
        }

        function SheetLoadComplete() {
            Gcoop.Focus("member_no_0");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_atm_newcard"
        LibraryList="~/DataWindow/atm/dp_atm_newcard.pbl" ClientScriptable="True" ClientEvents="true"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        AutoRestoreContext="False" ClientEventItemChanged="OnDwMainItemChanged" ClientEventButtonClicked="OnDwMainButtonClicked">
    </dw:WebDataWindowControl>
    <table style="text-align: left">
        <tr>
            <td align="left" style="width: 100%;">
                <asp:Literal ID="Literal1" runat="server"><font color="red">หมายเหตุ</font><br />
    - กรณีบัตรชำรุด ให้พิมพ์บัตรเก่าได้เลย และให้ใช้รหัสเก่าได้ทันที<br />
    - กรณีบัตรหาย จะต้องพิมพ์รหัส และบัตรใหม่ทั้งหมด เหมือนเปิดบัตรใหม่</asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
