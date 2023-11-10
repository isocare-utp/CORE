<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_mbshr_adt_mbhistory.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_adt_mbhistory_ctrl.ws_mbshr_adt_mbhistory" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;
        var dsDetail = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c) {

            if (c == "btn_search") {
                PostSearch();
            }
        }

        function OnDsListClicked(s, r, c) {
            // alert(r);
            if (r >= 0) {
                //var doc_no = dsList.GetItem(r, "modtbdoc_no");
                //dsDetail.SetItem(0, "doc_no", doc_no);
                Gcoop.GetEl("HdCheckRow").value = r;
                PostDetail();
            }
        }
        function OnDsDetailClicked(s, r, c) {

            if (r >= 0) {
                var modtb_code = dsDetail.GetItem(r, "modtb_code");
                var clm_name = dsDetail.GetItem(r, "clm_name");
                var clmold_desc = dsDetail.GetItem(r, "clmold_desc");
                var clmnew_desc = dsDetail.GetItem(r, "clmnew_desc");

                if (c == "btn_detail") {
                    Gcoop.OpenDlg("570px", "170px", "w_dlg_mbshr_mbhistory_ctrl/w_dlg_mbshr_mbhistory.aspx?&modtb_code=" + modtb_code + "&clm_name=" + clm_name + "&clmold_desc=" + clmold_desc + "&clmnew_desc=" + clmnew_desc, "");
                }
            }
        }


        function OnDsMainItemChanged(s, r, c, v) {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <span class="TitleSpan">ระบุเงื่อนไขการค้นหา</span>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc2:DsList ID="dsList" runat="server" />
    <br />
    <uc3:DsDetail ID="dsDetail" runat="server" />
    <asp:HiddenField ID="HdCheckRow" runat="server" />
</asp:Content>
