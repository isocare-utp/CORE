<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_mbshr_cremation_detail.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_cremation_detail_ctrl.ws_mbshr_cremation_detail" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register src="DsSum.ascx" tagname="DsSum" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;
       

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
            if (c == "member_no") {
                //alert(r);return;
                Gcoop.GetEl("HdRow").value = r;
                SetRefMember();
            }
            if (c == "cmttype_code") {
                var cmttype_code = dsList.GetItem(r, "cmttype_code");
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

        }


        function GetValueFromDlg(memberno) {
              dsMain.SetItem(0, "member_no", memberno);
            PostMember();
        }
        function GetMembNoFromDlg(memberno) {
            dsList.SetItem(Gcoop.GetEl("HdRow").value, memberno, "member_no");

            SetRefMember();

        }
        function OnClickNewRow() {
            PostNewRow();
        }
        //ประกาศฟังก์ชันสำหรับ event Clicked
        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r);
                if (confirm("ลบข้อมูลแถวที่ " + (r + 1) + " ?") == true) {
                    PostDelRow();
                }
            }
        }

        function Setfocus() {
            dsMain.Focus(0, "member_no");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <div style="text-align: right;">
        <span class="NewRowLink" onclick="OnClickNewRow()" style="padding-left: 30px;">เพิ่มแถว</span>
    </div>
    <div style="text-align: left;">
        <span style="padding-left: 30px;">รายละเอียด</span>
    </div>
    <uc2:DsList ID="dsList" runat="server" />
    <br><br>
    <div>

        <uc3:DsSum ID="dsSum" runat="server" />

     </div>
    <asp:HiddenField ID="HdRow" runat="server" />
    <asp:HiddenField ID="HdRow_dlg" runat="server" />
</asp:Content>
