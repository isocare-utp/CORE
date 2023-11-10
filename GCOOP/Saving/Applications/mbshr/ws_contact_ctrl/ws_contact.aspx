<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_contact.aspx.cs" Inherits="Saving.Applications.mbshr.ws_contact_ctrl.ws_contact" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;
        /*function chkNumber(ele) {
            var vchar = String.fromCharCode(event.keyCode);
            if ((vchar = '0' ) && (vchar != '.')) return false;
            ele.onKeyPress = vchar;
        }*/
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

        function OnDsListClicked(s, r, c) {
            if (c == "m_search") {
                Gcoop.GetEl("HdRow_dlg").value = r;
                Gcoop.OpenIFrame2("630", "610", "w_dlg_mbshr_mem_search.aspx", "");
            }
        }
        function OnDsListItemChanged(s, r, c, v) {//salert(c);
            if (c == "refmember_no") {
                //alert(r);return;
                Gcoop.GetEl("HdRow").value = r;
                SetRefMember();
            }
            if (c == "refmember_flag") {
                var refmember_flag = dsList.GetItem(r, "refmember_flag");               
                if (refmember_flag == "0") {             
                    dsList.GetElement(r, 'refmember_no').disabled = true;
                    dsList.GetElement(r, 'refmember_no').style.background = '#EEEEEE';
                } else if (refmember_flag == "1") {
                     dsList.GetElement(r, 'refmember_no').disabled = false;
                    dsList.GetElement(r, 'refmember_no').style.background = '#FFFFFF';
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
            //alert("ddddddddddeeee");
            dsMain.SetItem(0, "member_no", memberno);
            PostMember();
        }
        function GetMembNoFromDlg(memberno) {
            //alert("dddddddddd");
            dsList.SetItem(Gcoop.GetEl("HdRow").value, "refmember_no", memberno);
            SetRefMember();
        }
        function OnClickNewRow() {
            PostNewRow();
        }
        function Setfocus() {
            dsMain.Focus(0, "member_no");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <div style="text-align: left;">
        <span style="padding-left: 30px;">ข้อมูลผู้ติดตามหนี้</span>
    </div>
    <uc2:DsList ID="dsList" runat="server" />
    <asp:HiddenField ID="HdRow" runat="server" />
    <asp:HiddenField ID="HdRow_dlg" runat="server" />
</asp:Content>
