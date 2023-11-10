<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_mb_mbucfmemgrp.aspx.cs" Inherits="Saving.Applications.mbshr_const.ws_mb_mbucfmemgrp_ctrl.ws_mb_mbucfmemgrp" %>

<%@ Register Src="DsSearch.ascx" TagName="DsSearch" TagPrefix="uc2" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();
        var dsSearch = new DataSourceTool();
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function OnDsSearchClicked(s, r, c) {

            if (c == "b_add") {
                var ls_membgroup_code = "";

                Gcoop.OpenIFrame3Extend("630", "450", "w_dlg_sl_searchmembgroup.aspx", "?ls_membgroup_code=" + ls_membgroup_code);

            } else if (c == "b_search") {

                var index_old = $window_row_color;
                $window_row_color = -1;
                var index_new = findindex($window_row_color);

                if (index_new != index_old) {
                    // clear color เก่า
                    if (index_old != -1) {
                        $("#ctl00_ContentPlace_dsList_Panel2 tr").eq(index_old).children().css('background', 'white')
                        $("#ctl00_ContentPlace_dsList_Panel2 tr").eq(index_old).children().find('input:text').css('background', 'white');
                    }
                    $("#ctl00_ContentPlace_dsList_Panel2 tr").eq(index_new).children().css('background', '#6699FF')
                    $("#ctl00_ContentPlace_dsList_Panel2 tr").eq(index_new).children().find('input:text').css('background', '#6699FF');
                    $window_row_color = index_new;
                }
            } else if (c == "b_next") {
                var index_old = $window_row_color;
                var index_new = findindex(index_old);

                if (index_new != index_old) {
                    // clear color เก่า
                    if (index_old != -1) {
                        $("#ctl00_ContentPlace_dsList_Panel2 tr").eq(index_old).children().css('background', 'white')
                        $("#ctl00_ContentPlace_dsList_Panel2 tr").eq(index_old).children().find('input:text').css('background', 'white');
                    }
                    $("#ctl00_ContentPlace_dsList_Panel2 tr").eq(index_new).children().css('background', '#6699FF')
                    $("#ctl00_ContentPlace_dsList_Panel2 tr").eq(index_new).children().find('input:text').css('background', '#6699FF');
                    $window_row_color = index_new;
                }
            }
        }

        function RefreshSheet(ls_membgroup_control) {
            dsSearch.SetItem(0, "cp_groupcontrol", ls_membgroup_control);
        }

        function findindex(indexold2) {

            var membgroup_code = getObjString(dsSearch, 0, "membgroup_code");
            var membgroup_desc = getObjString(dsSearch, 0, "membgroup_desc");
            var countdsList = dsList.GetRowCount();

            if (membgroup_code != "" && membgroup_desc == "") {
                for (var i = 0; i < countdsList; i++) {
                    var membgroup_code_dslist = getObjString(dsList, i, "membgroup_code");
                    if (membgroup_code == membgroup_code_dslist) {
                        return i;
                    }
                }
            } else if (membgroup_desc != "" && membgroup_code == "") {
                var indexold
                if (indexold2 == countdsList) {
                    indexold = 0;
                } else {
                    indexold = indexold2 + 1;
                }

                for (var i = indexold; i < countdsList; i++) {
                    var membgroup_desc_dslist = getObjString(dsList, i, "membgroup_desc");
                    var search = membgroup_desc_dslist.search(membgroup_desc);
                    if (search >= 0) {
                        return i;
                    }
                }
            } else if (membgroup_code != "" && membgroup_desc != "") {
                for (var i = 0; i < countdsList; i++) {
                    var membgroup_code_dslist = getObjString(dsList, i, "membgroup_code");
                    var membgroup_desc_dslist = getObjString(dsList, i, "membgroup_desc");
                    var search = membgroup_desc_dslist.search(membgroup_desc);
                    if (membgroup_code == membgroup_code_dslist /*&& search >= 0*/) {
                        return i;
                    }
                }
            }
            return indexold2;
        }

        function OnDsMainItemChanged(s, r, c) {

            if (c == "membgroup_code") {

                var ls_membgroup_code = dsMain.GetItem(0, "membgroup_code");
                for (var i = 0; i < dsList.GetRowCount(); i++) {
                    var ls_membgroup_code_list = dsList.GetItem(i, "membgroup_code");
                    if (ls_membgroup_code == ls_membgroup_code_list) {
                        alert("รหัสสังกัดซ้ำ");
                        dsMain.SetItem(0, "membgroup_code", "");
                    }
                }
            }
        }

        function OnDsSearchItemChanged(s, r, c) {
            if (c == "cp_groupcontrol") {
                PostGroupControl();
            }
        }

        function GetIMembgroup(membgroup_code) {
            Gcoop.RemoveIFrame();
            Gcoop.GetEl("HdMembgroup").value = membgroup_code;
            dsMain.SetItem(0, "membgroup_code", membgroup_code);
            //            Gcoop.GetEl("HdSlipStatus").value = 1;
            PostMembGroup();
        }
        function OnDsListClicked(s, r, c, v) {

            if (c == "b_detail") {

                var ls_membgroup_code = dsList.GetItem(r, "membgroup_code");

                Gcoop.OpenIFrame3("630", "450", "w_dlg_sl_searchmembgroup.aspx", "?ls_membgroup_code=" + ls_membgroup_code);
                // Gcoop.OpenIFrame3("830", "650", "w_dlg_sl_searchmembgroup.aspx");
            } else if (c == "b_delete") {

                Validate();
                dsList.SetRowFocus(r);
                PostDeleteRow();
            }
        }

        function SheetLoadComplete() {
            $window_row_color = -1;
            //clear color
            var countdsList = dsList.GetRowCount();
            for (var i = 0; i < countdsList; i++) {
                $("#ctl00_ContentPlace_dsList_Panel2 tr").eq(i).children().css('background', 'white')
                $("#ctl00_ContentPlace_dsList_Panel2 tr").eq(i).children().find('input:text').css('background', 'white');
            }
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table align="center">
        <tr>
            <td>
                <uc2:DsSearch ID="dsSearch" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <uc3:DsList ID="dsList" runat="server" />
            </td>
        </tr>
        <%-- <tr>
            <td>
                <uc1:DsMain ID="dsMain" runat="server" />
                <br />
            </td>
        </tr>--%>
    </table>
    <asp:HiddenField ID="HdMembgroup" runat="server" />
</asp:Content>
