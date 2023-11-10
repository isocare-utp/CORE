<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mb_constant_memgruop.aspx.cs" Inherits="Saving.Applications.mbshr.w_sheet_mb_constant_memgruop_ctrl.w_sheet_mb_constant_memgruop" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<%@ Register Src="DsMainDetail.ascx" TagName="DsMainDetail" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMainDetail = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }

        //        function OnDsMainClicked(s, r, c) {
        //            if (c == "b_search") {
        //                Gcoop.OpenDlg(600, 500, "", "");
        //            }
        //        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "membgroup_codet") {
                //postMemberGroup();
                postGroupCode();
            }
        }

        function OnDsDetailItemChanged(s, r, c, v) {
            if (c == "addr_tambol") {
                postTambol();
            } else if (c == "addr_amphur") {
                postAmphur();
            } else if (c == "addr_province") {
                postProvince();
            }
        }

        function OnDsMainDetailClicked(s, r, c) {
            if (c == "ls_mbg_code" || c == "ls_mbg_name") {
                Gcoop.GetEl("HdRow").value =  dsMainDetail.GetItem(r, "ls_mbg_code");
                postMemberGroup();
            }
        }

        $(function () {
            var n = 1;

            $('.td_row').each(function () {
                $(this).find('.num_row').val(n)
                n++
            });

            SetCSSBGColor('#ctl00_ContentPlace_dsDetail_FormView1_membgroup_code', 'gold');
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc3:DsMainDetail ID="dsMainDetail" runat="server" />
    <br />
    <uc2:DsDetail ID="dsDetail" runat="server" />

    <asp:HiddenField ID="HdRow" runat="server" />
</asp:Content>
