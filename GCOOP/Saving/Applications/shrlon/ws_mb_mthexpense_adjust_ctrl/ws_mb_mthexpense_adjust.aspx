<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_mb_mthexpense_adjust.aspx.cs" Inherits="Saving.Applications.shrlon.ws_mb_mthexpense_adjust_ctrl.ws_mb_mthexpense_adjust" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsPlus.ascx" TagName="DsPlus" TagPrefix="uc2" %>
<%@ Register Src="DsMinus.ascx" TagName="DsMinus" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsPlus = new DataSourceTool();
        var dsMinus = new DataSourceTool();

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }
        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_memsearch") {
                Gcoop.OpenDlg("630", "720", "w_dlg_member_search.aspx", "")
            }

        }
        function GetMemDetFromDlg(memberno, prename_desc, memb_name, memb_surname, card_person) {

            dsMain.SetItem(0, "member_no", memberno);
            PostMemberNo();

        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }
        }
        function OnDsPlusClicked(s, r, c, v) {
            if (c == "b_delete") {
                dsPlus.SetRowFocus(r);
                PostDeleteRowPlus();
            }

        }
        function OnDsMinusClicked(s, r, c, v) {
            if (c == "b_delete") {
                dsMinus.SetRowFocus(r);
                PostDeleteRowMinus();
            }

        }
        function OnDsPlusItemChanged(s, r, c, v) {
            if (c == "mthexpense_amt") {
                //PostSumPlus();

                var row = dsPlus.GetRowCount();

                var mthexpense_amt = 0;
                for (var i = 0; i < row; i++) {
                    mthexpense_amt +=Number( dsPlus.GetItem(i, "mthexpense_amt"));
                }
                $("#ctl00_ContentPlace_dsPlus_cpsum_mthexpense_amt").val(addCommas( mthexpense_amt,2));
            }
        }
        function OnDsMinusItemChanged(s, r, c, v) {
            if (c == "mthexpense_amt") {
                //PostSumMinus();
                var row = dsMinus.GetRowCount();

                var mthexpense_amt = 0;
                for (var i = 0; i < row; i++) {
                    mthexpense_amt += Number(dsMinus.GetItem(i, "mthexpense_amt"));
                }
                $("#ctl00_ContentPlace_dsMinus_cpsum_mthexpense_amt2").val(addCommas(mthexpense_amt, 2));
            }
        }
        function SheetLoadComplete() { }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <u><b>รายได้/รายการหัก รายเดือนอื่นๆ</b></u>
    <uc1:DsMain ID="dsMain" runat="server" />
    <table>
        <tr>
            <td>
                <u><b>รายได้อื่นๆ</b></u>
                <div align="right">
                    <span class="NewRowLink" onclick="PostInsertRowPlus()">เพิ่มแถว</span>
                </div>
            </td>
            <td>
                <u><b>รายการหัก</b></u>
                <div align="right">
                    <span class="NewRowLink" onclick="PostInsertRowMinus()">เพิ่มแถว</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <uc2:DsPlus ID="dsPlus" runat="server" />
            </td>
            <td>
                <uc3:DsMinus ID="dsMinus" runat="server" />
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
