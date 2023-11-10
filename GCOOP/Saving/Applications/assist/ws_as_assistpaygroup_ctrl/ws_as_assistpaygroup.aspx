<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_as_assistpaygroup.aspx.cs" Inherits="Saving.Applications.assist.ws_as_assistpaygroup_ctrl.ws_as_assistpaygroup" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsSMain.ascx" TagName="DsSMain" TagPrefix="uc3" %>
<%@ Register Src="DsSPayto.ascx" TagName="DsSPayto" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;
        var dsSMain = new DataSourceTool;
        var dsSPayto = new DataSourceTool;

        function Validate() {
           // return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "assisttype_code") {
                InitAsssistpaytype();
            } else if (c == "select_check") {
                if (v == 0) {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "choose_flag", v);
                        dsList.GetElement(i, "cp_name").style.background = "#FFFFFF";
                        dsList.GetElement(i, "assisttype_desc").style.background = "#FFFFFF";
                        dsList.GetElement(i, "approve_date").style.background = "#FFFFFF";
                        dsList.GetElement(i, "approve_amt").style.background = "#FFFFFF";
                    }
                } else {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "choose_flag", v);
                        dsList.GetElement(i, "cp_name").style.background = "#FFFF99";
                        dsList.GetElement(i, "assisttype_desc").style.background = "#FFFF99";
                        dsList.GetElement(i, "approve_date").style.background = "#FFFF99";
                        dsList.GetElement(i, "approve_amt").style.background = "#FFFF99";
                    }
                }
            }
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_clear") {
                dsMain.SetItem(0, "member_no", "");
                dsMain.SetItem(0, "assisttype_code", "");
                PostSearch();
            } else if (c == "b_search") {
                PostSearch();
            } else if (c == "b_searchmem") {
                Gcoop.OpenIFrame2(650, 600, 'wd_as_member_search.aspx', '')
            } else if (c == "b_pay") {
                var assist = "";
                var word = "";
                var assist_pay = "";

                for (var i = 0; i < dsList.GetRowCount(); i++) {
                    if (dsList.GetItem(i, "choose_flag") == 1) {
                        assist = dsList.GetItem(i, "asscontract_no");
                        word += "," + assist;
                    }
                }
                if (word != "") {
                    word = word.substring(1, word.length);
                    //Gcoop.GetEl("Hd_assist").value = word;
                    JsSavedata();
                 //   Gcoop.OpenIFrame2("800", "550", "wd_as_assistpay.aspx", "?assists=" + word);

                } else { alert('กรุณาเลือกใบคำร้อง!!!'); }
            }
        }

        function GetMembNoFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno.trim());
        }
        function GetRetrivedata() {
            PostSearch();
        }
        function OnDsDsListItemChanged(s, r, c, v) {
            if (c == "choose_flag") {
                if (v == 0) {
                    dsMain.SetItem(0, "select_check", 0);
                    dsList.SetItem(r, "choose_flag", v);
                    dsList.GetElement(r, "asscontract_no").style.background = "#FFFFFF";
                    dsList.GetElement(r, "assistpay_code").style.background = "#FFFFFF";
                    dsList.GetElement(r, "assisttype_desc").style.background = "#FFFFFF";
                    dsList.GetElement(r, "full_name").style.background = "#FFFFFF";
                    dsList.GetElement(r, "approve_amt").style.background = "#FFFFFF";
                } else {
                    dsList.SetItem(r, "choose_flag", v);
                    dsList.GetElement(r, "asscontract_no").style.background = "#FFFF99";
                    dsList.GetElement(r, "assistpay_code").style.background = "#FFFF99";
                    dsList.GetElement(r, "assisttype_desc").style.background = "#FFFF99";
                    dsList.GetElement(r, "full_name").style.background = "#FFFF99";
                    dsList.GetElement(r, "approve_amt").style.background = "#FFFF99";
                }
            } else if (c == "req_status") {
                if (v != 8) {
                    dsList.SetItem(r, "choose_flag", 1);
                    dsList.GetElement(r, "asscontract_no").style.background = "#FFFF99";
                    dsList.GetElement(r, "assistpay_code").style.background = "#FFFF99";
                    dsList.GetElement(r, "assisttype_desc").style.background = "#FFFF99";
                    dsList.GetElement(r, "full_name").style.background = "#FFFF99";
                    dsList.GetElement(r, "approve_amt").style.background = "#FFFF99";
                } else {
                    dsList.SetItem(r, "choose_flag", 0);
                    dsList.GetElement(r, "asscontract_no").style.background = "#FFFFFF";
                    dsList.GetElement(r, "assistpay_code").style.background = "#FFFFFF";
                    dsList.GetElement(r, "assisttype_desc").style.background = "#FFFFFF";
                    dsList.GetElement(r, "full_name").style.background = "#FFFFFF";
                    dsList.GetElement(r, "approve_amt").style.background = "#FFFFFF";
                }
            }

        }

        function OnDsListClicked(s, r, c) {
        }

        function SheetLoadComplete() {
            for (var i = 0; i < dsList.GetRowCount(); i++) {
                if (dsList.GetItem(i, "choose_flag") == 1) {
                    dsList.GetElement(i, "asscontract_no").style.background = "#FFFF99";
                    dsList.GetElement(i, "assistpay_code").style.background = "#FFFF99";
                    dsList.GetElement(i, "assisttype_desc").style.background = "#FFFF99";
                    dsList.GetElement(i, "full_name").style.background = "#FFFF99";
                    dsList.GetElement(i, "approve_amt").style.background = "#FFFF99";
                    dsList.GetElement(i, "req_status").style.background = "#FFFF99";
                }
            }
        }

        function GetShowData() {
            alert('บันทึกข้อมูลสำเร็จ');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
    <uc3:DsSMain ID="dsSMain" runat="server" Visible="false"  />
    <uc4:DsSPayto ID="dsSPayto" runat="server" Visible="false"  />
    <asp:HiddenField ID="Hd_assist" runat="server" />
    <asp:HiddenField ID="HdIndex" runat="server" />
</asp:Content>

