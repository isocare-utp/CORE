<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_as_cancelrequest.aspx.cs" Inherits="Saving.Applications.assist.ws_as_cancelrequest_ctrl.ws_as_cancelrequest" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "assisttype_code") {
                dsMain.SetItem(0, "assist_code", v);
            } else if (c == "assist_code") {
                dsMain.SetItem(0, "assisttype_code", v);
            } else if (c == "select_check") {
                if (v == 0) {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "choose_flag", v);
                        dsList.GetElement(i, "assist_docno").style.background = "#FFFFFF";
                        dsList.GetElement(i, "assistpay_code").style.background = "#FFFFFF";
                        dsList.GetElement(i, "full_name").style.background = "#FFFFFF";
                        dsList.GetElement(i, "assistnet_amt").style.background = "#FFFFFF";
                    }
                } else {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "choose_flag", v);
                        dsList.GetElement(i, "assist_docno").style.background = "#FFFF99";
                        dsList.GetElement(i, "assistpay_code").style.background = "#FFFF99";
                        dsList.GetElement(i, "full_name").style.background = "#FFFF99";
                        dsList.GetElement(i, "assistnet_amt").style.background = "#FFFF99";
                    }
                }
            }
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_clear") {
                dsMain.SetItem(0, "member_no", "");
                dsMain.SetItem(0, "assist_code", "");
                dsMain.SetItem(0, "assisttype_code", "");
                PostSearch();
            } else if (c == "b_search") {
                PostSearch();
            } else if (c == "b_searchmem") {
                Gcoop.OpenIFrame2(650, 600, 'wd_as_member_search.aspx', '')
            }
        }

        function GetMembNoFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno.trim());
        }

        function OnDsDsListItemChanged(s, r, c, v) {
            if (c == "choose_flag") {
                if (v == 0) {
                    dsMain.SetItem(0, "select_check", 0);
                    dsList.SetItem(r, "choose_flag", v);
                    dsList.GetElement(r, "assist_docno").style.background = "#FFFFFF";
                    dsList.GetElement(r, "assistpay_code").style.background = "#FFFFFF";
                    dsList.GetElement(r, "full_name").style.background = "#FFFFFF";
                    dsList.GetElement(r, "assistnet_amt").style.background = "#FFFFFF";
                } else {
                    dsList.SetItem(r, "choose_flag", v);
                    dsList.GetElement(r, "assist_docno").style.background = "#FFFF99";
                    dsList.GetElement(r, "assistpay_code").style.background = "#FFFF99";
                    dsList.GetElement(r, "full_name").style.background = "#FFFF99";
                    dsList.GetElement(r, "assistnet_amt").style.background = "#FFFF99";
                }
            }

        }

        function OnDsListClicked(s, r, c) {
        }

        function SheetLoadComplete() {
            for (var i = 0; i < dsList.GetRowCount(); i++) {
                if (dsList.GetItem(i, "choose_flag") == 1) {
                    dsList.GetElement(i, "assist_docno").style.background = "#FFFF99";
                    dsList.GetElement(i, "assistpay_code").style.background = "#FFFF99";
                    dsList.GetElement(i, "full_name").style.background = "#FFFF99";
                    dsList.GetElement(i, "assistnet_amt").style.background = "#FFFF99";
                }
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
