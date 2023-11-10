<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_reqgain_true.aspx.cs" Inherits="Saving.Applications.mbshr.ws_sl_reqgain_true_ctrl.ws_sl_reqgain_true" %>

<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc1" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsDetail = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenIFrame(650, 470, "w_dlg_mbshr_mbgain_history_ctrl/w_dlg_mbshr_mbgain_history.aspx", "");
            }
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                postMemberno();
            } else if (c == "gaincond_type") {
                if (v == 99) {
                    dsMain.GetElement(0, "gaincond_desc").readOnly = false;
                    dsMain.GetElement(0, "gaincond_desc").style.background = "#FFFFFF";

                } else {
                    dsMain.GetElement(0, "gaincond_desc").readOnly = true;
                    dsMain.SetItem(0, "gaincond_desc", "")
                    dsMain.GetElement(0, "gaincond_desc").style.background = "#CCCCCC";
                }

                for (var i = 0; i < dsDetail.GetRowCount(); i++) {
                    dsDetail.SetItem(i, "gaincond_type", v);
                    dsDetail.SetItem(i, "gaincond_desc", dsMain.GetItem(0, "gaincond_desc"));
                }
            } else if (c == "write_at" || c == "gaincond_date") {
                for (var i = 0; i < dsDetail.GetRowCount(); i++) {
                    dsDetail.SetItem(i, "write_at", dsMain.GetItem(0, "write_at"));
                    dsDetail.SetItem(i, "write_date", dsMain.GetItem(0, "gaincond_date"));
                }
            }
        }

        function OnDsDetailClicked(s, r, c) {
            if (c == "b_del") {
                dsDetail.SetRowFocus(r);
                PostDeleteRow();
            }
        }

        function OnDsDetailItemChanged(s, r, c, v) {
        }

        function SheetLoadComplete() {
            var type = dsMain.GetItem(0, "gaincond_type");
            if (type == 99) {
                dsMain.GetElement(0, "gaincond_desc").readOnly = false;
                dsMain.GetElement(0, "gaincond_desc").style.background = "#FFFFFF";
            } else {
                dsMain.GetElement(0, "gaincond_desc").readOnly = true;
                dsMain.GetElement(0, "gaincond_desc").style.background = "#CCCCCC";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc2:DsMain ID="dsMain" runat="server" />
    <br />
    <div align="right">
        <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว </span>
    </div>
    <uc1:DsDetail ID="dsDetail" runat="server" />
</asp:Content>
