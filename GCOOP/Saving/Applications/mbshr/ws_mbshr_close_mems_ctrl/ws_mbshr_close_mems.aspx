<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_mbshr_close_mems.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_close_mems_ctrl.ws_mbshr_close_mems" %>

<%@ Register Src="DsCriteria.ascx" TagName="DsCriteria" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsShare.ascx" TagName="DsShare" TagPrefix="uc3" %>
<%@ Register Src="DsLoan.ascx" TagName="DsLoan" TagPrefix="uc4" %>
<%@ Register Src="DsCollwho.ascx" TagName="DsCollwho" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsCriteria = new DataSourceTool;
        var dsList = new DataSourceTool;
        var dsShare = new DataSourceTool;
        var dsLoan = new DataSourceTool;
        var dsCollwho = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDsListClicked(s, r, c) {
            if (c == "running_number" || c == "member_no" || c == "cp_name" || c == "cp_groupdesc" || c == "resign_date" || c == "resigncause_desc") {
                dsList.SetRowFocus(r);
                PostRetriveDetail();
            }
        }

        $(function () {
            $("#ctl00_ContentPlace_dsCriteria_FormView1_close_type").change(function () {
                Gcoop.GetEl("Hdclose_type").value = $("#ctl00_ContentPlace_dsCriteria_FormView1_close_type").val();
                PostRetrieveList();
            });
        });

        $(function () {
            $("#ctl00_ContentPlace_dsList_cb_all").click(function () {
                var chk = $('#ctl00_ContentPlace_dsList_cb_all').is(':checked');

                if (chk) {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "operate_flag", 1);
                    }
                } else {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "operate_flag", 0);
                    }
                }
            });
        });

        function OnDsCriteriaClicked(s, r, c) {
            if (c == "b_search") {
                var member_no_search = formatmember(dsCriteria.GetItem(r, "member_no"));                

                var index_old = $window_row_color;
                $window_row_color = -1;
                
                if (member_no_search != "") {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        var member_no_dslist = getObjString(dsList, i, "member_no");
                        if (member_no_search == member_no_dslist) {
                            $window_row_color = i;
                        }
                    }
                }
                                
                var index_new = $window_row_color;

                if (index_new != index_old) {
                    // clear color เก่า
                    if (index_old != -1) {
                        $("#ctl00_ContentPlace_dsList_Panel1 tr").eq(index_old).children().css('background', 'white')
                        $("#ctl00_ContentPlace_dsList_Panel1 tr").eq(index_old).children().find('input:text').css('background', 'white');
                    }
                    dsList.SetRowFocus(index_new);
                    $("#ctl00_ContentPlace_dsList_Panel1 tr").eq(index_new).children().css('background', '#6699FF')
                    $("#ctl00_ContentPlace_dsList_Panel1 tr").eq(index_new).children().find('input:text').css('background', '#6699FF');
                    $window_row_color = index_new;
                }
            }
        }

        function formatmember(str) {
            var count = str.length;
            var strres = str;
            if (count < 8) {
                var count2 = 8 - count;

                for (var i = 0; i < count2; i++) {
                    strres = '0' + strres;
                }

            } else if (count > 8) {
                alert('รูปแบบทะเบียนไม่ถูก');
            }
            return strres;
        }

        function SheetLoadComplete() {
            $window_row_color = -1;

            $('.DataSourceRepeater').find('input,select,button').attr('readOnly', true);
            $('.DataSourceRepeater').find('select').attr('disabled', true);
            var close_type = $("#ctl00_ContentPlace_dsCriteria_FormView1_close_type").val();
            if (close_type == 1) {
                document.getElementById("ctl00_ContentPlace_dsList_cb_all").disabled = true;
                //$('.ctl00_ContentPlace_dsList_Repeater1_ctl00_operate_flag').find('input,select,button').attr('disabled', true);
            }
        }

        $(function () {
            $("#tabs").tabs();
        });
    </script>
    <style type="text/css">
        .ui-tabs
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        #tabs
        {
            width: 760px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hdclose_type" runat="server" />
    <br />
    <uc1:DsCriteria ID="dsCriteria" runat="server" />
    <br />
    <uc2:DsList ID="dsList" runat="server" />
    <br />
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">รายการหุ้น</a></li>
            <li><a href="#tabs-2">เงินกู้</a></li>
            <li><a href="#tabs-3">ค้ำประกันสมาชิก</a></li>
        </ul>
        <div id="tabs-1">
            <uc3:DsShare ID="dsShare" runat="server" />
        </div>
        <div id="tabs-2">
            <uc4:DsLoan ID="dsLoan" runat="server" />
        </div>
        <div id="tabs-3">
            <uc5:DsCollwho ID="dsCollwho" runat="server" />
        </div>
    </div>
    <br />
</asp:Content>
