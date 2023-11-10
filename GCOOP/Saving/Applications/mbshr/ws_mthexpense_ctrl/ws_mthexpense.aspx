<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_mthexpense.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mthexpense_ctrl.ws_mthexpense" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsSum.ascx" TagName="DsSum" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;
        var dsSum = new DataSourceTool;

        function Validate() {
            var MTHEXPENSE_AMT = dsMain.GetItem(0, "MTHEXPENSE_AMT");
            if (MTHEXPENSE_AMT == 0) {
                alert("กรุณากรอกจำนวนเงิน");
                return false;
            } else {
                return true;
            }
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenIFrame2("630", "610", "w_dlg_sl_member_search_lite.aspx", "");
            }
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMember();
            }
        }

        function OnDsListItemChanged(s, r, c, v) {
            PostCalSum();
        }




        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r);
                if (confirm("ลบข้อมูลแถวที่ " + (r + 1) + " ?") == true) {
                    PostDelRow();
                }
            }


        }
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
            //            for (var i = 0; i < dsList.GetRowCount() - 1; i++) {
            //                var chk_code = dsList.GetItem(i, "ASSISTTYPE_CODE");
            //    var chk_desc = dsList.GetItem(i, "ASSISTTYPE_GROUPDESC");
            //                if (chk_code == "") {
            //                    alert("กรุณากรอกรหัสประเภทสวัสดิการ");
            //                    dsList.SetItem(i, "ASSISTTYPE_CODE", "");
            //                    break;
            //                } else if (chk_desc = "") {
            //                    alert("กรุณากรอกคำอธิบายสวัสดิการ");
            //        dsList.SetItem(i, "ASSISTTYPE_GROUPDESC", "");
            //                    break;
            //                }
            //            }                                        
        }

        function GetValueFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno);
            PostMember();
        }
        function OnClickNewRow() {
            PostNewRow();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <div style="text-align: right;">
        <span class="NewRowLink" onclick="OnClickNewRow()" style="padding-left: 30px;">เพิ่มแถว</span>
    </div>
    <uc2:DsList ID="dsList" runat="server" />
    <uc3:DsSum ID="dsSum" runat="server" />
</asp:Content>
