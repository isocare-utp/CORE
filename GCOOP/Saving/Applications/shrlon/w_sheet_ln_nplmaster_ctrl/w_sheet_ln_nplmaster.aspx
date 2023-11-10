<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_ln_nplmaster.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.w_sheet_ln_nplmaster" %>

<%@ Register Src="DsMemb.ascx" TagName="DsMemb" TagPrefix="uc1" %>
<%@ Register Src="DsLoan.ascx" TagName="DsLoan" TagPrefix="uc2" %>
<%@ Register Src="DsNPL.ascx" TagName="DsNPL" TagPrefix="uc3" %>
<%@ Register Src="DsFollow.ascx" TagName="DsFollow" TagPrefix="uc4" %>
<%@ Register Src="DsBoard.ascx" TagName="DsBoard" TagPrefix="uc5" %>
<%@ Register Src="DsResolution.ascx" TagName="DsResolution" TagPrefix="uc6" %>
<%@ Register Src="DsMort.ascx" TagName="DsMort" TagPrefix="uc7" %>
<%@ Register Src="DsColl.ascx" TagName="DsColl" TagPrefix="uc8" %>
<%@ Register Src="DsCollSub.ascx" TagName="DsCollSub" TagPrefix="uc9" %>
<%@ Register Src="DsReport.ascx" TagName="DsReport" TagPrefix="uc10" %>
<%@ Register Src="DsFollowMast.ascx" TagName="DsFollowMast" TagPrefix="uc11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ui-tabs
        {
            font-family: Tahoma;
            font-size: 12px;
        }
    </style>
    <script type="text/javascript">
        $(function () {

        });
    </script>
    <script type="text/javascript">
        var dsMemb = new DataSourceTool();
        var dsLoan = new DataSourceTool();
        var dsNPL = new DataSourceTool();
        var dsFollowMast = new DataSourceTool();
        var dsFollow1 = new DataSourceTool();
        var dsFollow2 = new DataSourceTool();
        var dsBoard = new DataSourceTool();
        var dsResolution = new DataSourceTool();
        var dsMort = new DataSourceTool();
        var dsColl = new DataSourceTool();
        var dsCollSub = new DataSourceTool();
        var dsReport = new DataSourceTool();

        function OnDsMembItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }
        }

        function OnDsMembClicked(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenSearchMemberNo();
            }
        }

        function OnDsLoanItemChanged(s, r, c, v) {
            if (c == "loancontract_no") {
                PostLoanContractNo();
            }
        }

        function OnDsLoanClicked(s, r, c) {
            if (c == "b_search") {
                var member_no = dsMemb.GetItem(0, "member_no");
                if (member_no == null || member_no == "") {
                    alert("กรุณาใส่ทะเบียนสมาชิกก่อน");
                } else {
                    Gcoop.OpenIFrame3(720, 550, "w_dlg_ln_loan_search.aspx", "?cmd=member&member_no=" + dsMemb.GetItem(0, "member_no"));
                }
            }
        }

        function OnDsNPLItemChanged(s, r, c, v) {
            if (c == "margin") {
                dsFollowMast.SetItem(0, "advance_amt", v);
            }
        }

        function OnDsNPLClicked(s, r, c) {
            if (c == "indict_date" || c == "judge_date" || c == "received_date" || c == "loancontract_date" || c == "enforcement_date") {
                datePicker.PickDs(dsNPL, r, c, null);
            } else if (c == "b_getdata") {
                var qstr = "?loancontract_no=" + dsLoan.GetItem(0, "loancontract_no");
                Gcoop.OpenDlg3(350, 350, "w_dlg_npl_retrievedetail.aspx", qstr);
            }
        }

        function OnDsFollowMastItemChanged(s, r, c, v) {
            if (c == "follow_seq") {
                if (v != 999) {
                    PostFollowSeq();
                } else {
                    dsFollowMast.SetItem(0, "description", "");
                }
            }
        }

        function OnDsFollowMasterClicked(s, r, c) {
            if (c == "b_del") {
                if (confirm("ยืนยันการลบข้อมูลการติดตาม")) {
                    PostDeleteFollowSeq();
                }
            }
        }

        function OnDsFollow1ItemChanged(s, r, c, v) {
        }

        function OnDsFollow1Clicked(s, r, c) {
            if (c == "new_row") {
                if (dsFollowMast.GetItem(0, "follow_seq") <= 0) {
                    alert("ไม่สามารถเพิ่มแถวได้");
                } else {
                    PostAddRowFollow1();
                }
            } else if (c == "b_del") {
                dsFollow1.SetRowFocus(r);
                PostDelRowFollow1();
            } else if (c == "follow_date") {
                datePicker.PickDs(dsFollow1, r, c, null);
            }
        }

        function OnDsFollow2ItemChanged(s, r, c, v) {
        }

        function OnDsFollow2Clicked(s, r, c) {
            if (c == "new_row") {
                if (dsFollowMast.GetItem(0, "follow_seq") <= 0) {
                    alert("ไม่สามารถเพิ่มแถวได้");
                } else {
                    PostAddRowFollow2();
                }
            } else if (c == "b_del") {
                dsFollow2.SetRowFocus(r);
                PostDelRowFollow2();
            } else if (c == "follow_date") {
                datePicker.PickDs(dsFollow2, r, c, null);
            }
        }

        function OnDsBoardItemChanged(s, r, c, v) {
        }

        function OnDsBoardClicked(s, r, c) {
            if (c == "new_row") {
                PostAddRowBoard();
            } else if (c == "b_del") {
                dsBoard.SetRowFocus(r);
                PostDelRowBoard();
            } else if (c == "sub_date") {
                datePicker.PickDs(dsBoard, r, c, null);
            }
        }

        function OnDsResolutionItemChanged(s, r, c, v) {
        }

        function OnDsResolutionClicked(s, r, c) {
        }

        function OnDsMortItemChanged(s, r, c, v) {
        }

        function OnDsMortClicked(s, r, c) {
        }

        function OnDsCollItemChanged(s, r, c, v) {
        }

        function OnDsCollClicked(s, r, c) {
            if (r >= 0) {
                OnSelectColl(r);
            }
        }

        function OnDsCollSubItemChanged(s, r, c, v) {
            OnChangeCollDetail();
        }

        function OnDsCollSubClicked(s, r, c, v) {
            if (c == "sale_date" || c == "confiscation_date") {
                datePicker.PickDs(dsCollSub, r, c, OnChangeCollDetail);
            }
        }

        function OnDsReportItemChanged(s, r, c, v) {
        }

        function OnDsReportClicked(s, r, c) {
        }

        function OnSelectColl(row) {
            $("#collDetailText").val(dsColl.GetItem(row, "collmast_desc"));
            dsColl.CopyData(row, 0, dsCollSub);
        }

        function OnChangeCollDetail() {
            var collRow = dsColl.GetRowFocus();
            dsCollSub.CopyData(0, collRow, dsColl);
        }

        function Validate() {
            if (dsFollow1.GetRowCount() > 0 || dsFollow2.GetRowCount() > 0) {
                if (dsFollowMast.GetItem(0, "follow_seq") == 999) {
                    var desc = dsFollowMast.GetItem(0, "description");
                    if (desc == "" || desc == null) {
                        alert("กรุณาใส่ \"ชื่อการติดตาม\" ก่อนบันทึกข้อมูลด้วย");
                        return false;
                    }
                }
            }
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame3(720, 550, "w_dlg_ln_loan_search.aspx", "?cmd=npl");
        }

        function GetValueSearch(loancontract_no) {
            dsLoan.SetItem(0, "loancontract_no", loancontract_no);
            PostLoanContractNo();
        }

        function GetValueSearchMemberNo(member_no) {
            dsMemb.SetItem(0, "member_no", member_no);
            PostMemberNo();
        }

        function SheetLoadComplete() {
            var tabIndex = Gcoop.ParseInt($("#<%=hdTabIndex.ClientID%>").val());
            $("#tabs").tabs({
                active: tabIndex,
                activate: function (event, ui) {
                    $("#<%=hdTabIndex.ClientID%>").val(ui.newTab.index() + "");
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMemb ID="dsMemb" runat="server" />
    <br />
    <hr />
    <uc2:DsLoan ID="dsLoan" runat="server" />
    <br />
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">ข้อมูลคดีความ</a></li>
            <li><a href="#tabs-2">การติดตาม</a></li>
            <li><a href="#tabs-3">การประชุม</a></li>
            <li><a href="#tabs-4">หลักทรัพย์</a></li>
            <li><a href="#tabs-5">รายงานคุณสมบัติ</a></li>
        </ul>
        <div id="tabs-1">
            <uc3:DsNPL ID="dsNPL" runat="server" />
        </div>
        <div id="tabs-2">
            <uc11:DsFollowMast ID="dsFollowMast" runat="server" />
            <br />
            <fieldset>
                <legend style="font-family: Tahoma; font-size: 14px;">จะดำเนินการ</legend>
                <uc4:DsFollow ID="dsFollow1" runat="server" />
            </fieldset>
            <br />
            <fieldset>
                <legend style="font-family: Tahoma; font-size: 14px;">ดำเนินการแล้ว</legend>
                <uc4:DsFollow ID="dsFollow2" runat="server" />
            </fieldset>
        </div>
        <div id="tabs-3">
            <fieldset>
                <legend style="font-family: Tahoma; font-size: 14px;">ประชุมครั้งก่อน</legend>
                <uc5:DsBoard ID="dsBoard" runat="server" />
            </fieldset>
            <br />
            <fieldset>
                <legend style="font-family: Tahoma; font-size: 14px;">มติคณะกรรมการ</legend>
                <uc6:DsResolution ID="dsResolution" runat="server" />
            </fieldset>
        </div>
        <div id="tabs-4">
            <table>
                <tr>
                    <td align="left" valign="top">
                        <fieldset>
                            <uc7:DsMort ID="dsMort" runat="server" />
                        </fieldset>
                        <br />
                        <fieldset>
                            <uc8:DsColl ID="dsColl" runat="server" />
                        </fieldset>
                        <br />
                        <fieldset style="background-color: #E7E7E7;">
                            <legend style="font-family: Tahoma; font-size: 14px;">รายละเอียดหลักประกัน</legend>
                            <textarea id="collDetailText" style="background-color: #E7E7E7; border: none; width: 320px;
                                height: 70px;" readonly="readonly"></textarea>
                        </fieldset>
                    </td>
                    <td width="30">
                    </td>
                    <td align="left" valign="top">
                        <uc9:DsCollSub ID="dsCollSub" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="tabs-5">
            <input type="button" value="ออกรายงานคุณสมบัติ" onclick="PostReport()" />
        </div>
    </div>
    <asp:HiddenField ID="hdTabIndex" Value="0" runat="server" />
</asp:Content>
