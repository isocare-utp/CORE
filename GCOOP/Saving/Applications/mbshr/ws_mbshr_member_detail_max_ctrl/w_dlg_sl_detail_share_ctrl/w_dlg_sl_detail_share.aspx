<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_sl_detail_share.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl.w_dlg_sl_detail_share_ctrl.w_dlg_sl_detail_share" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsStatement.ascx" TagName="DsStatement" TagPrefix="uc2" %>
<%@ Register Src="DsPayment.ascx" TagName="DsPayment" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    var dsStatement = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }


        function DialogLoadComplete() {

            $(function () {
                //            $("#tabs").tabs();

                var tabIndex = Gcoop.ParseInt($("#<%=hdTabIndex.ClientID%>").val());
                $("#tabs").tabs({
                    active: tabIndex,
                    activate: function (event, ui) {
                        $("#<%=hdTabIndex.ClientID%>").val(ui.newTab.index() + "");
                    }
                });
            });

            for (var i = 0; i < dsStatement.GetRowCount();i++ ) {
                var sign_flag = dsStatement.GetItem(i, "sign_flag");
                if (sign_flag < "0") {
                    dsStatement.GetElement(i, "running_number").style.color = "#FF0000";
                    dsStatement.GetElement(i, "operate_date").style.color = "#FF0000";
                    dsStatement.GetElement(i, "slip_date").style.color = "#FF0000";
                    dsStatement.GetElement(i, "ref_docno").style.color = "#FF0000";
                    dsStatement.GetElement(i, "shritemtype_code").style.color = "#FF0000";
                    dsStatement.GetElement(i, "period").style.color = "#FF0000";
                    dsStatement.GetElement(i, "cp_sign_flag_in").style.color = "#FF0000";
                    dsStatement.GetElement(i, "cp_sign_flag_out").style.color = "#FF0000";
                    dsStatement.GetElement(i, "cp_sharestk").style.color = "#FF0000";
                }
            }
        }
    </script>
    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table width="100%">
        <tr>
            <td align="center">
                <uc1:DsMain ID="dsMain" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="tabs">
                    <ul>
                        <li><a href="#tabs-1">Statement</a></li>
                        <li><a href="#tabs-2">การส่งงวด</a></li>
                    </ul>
                    <div id="tabs-1">
                        <uc2:DsStatement ID="dsStatement" runat="server" />
                    </div>
                    <div id="tabs-2">
                        <uc3:DsPayment ID="dsPayment" runat="server" />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table style="width:90%; font-size: x-small">
                    <tr>
                        <td>
                            B/F : ยกยอดมาต้นปี
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            SPX : ซื้อหุ้นพิเศษ
                        </td>
                        <td>
                            RPX : ยกเลิกซื้อหุ้นพิเศษ
                        </td>
                        <td>
                            SPM : ส่งหุ้นประจำเดือน
                        </td>
                        <td>
                            RPM : ยกเลิกส่งหุ้นประจำเดือน
                        </td>
                    </tr>
                    <tr>
                        <td>
                            STL : โอนหุ้นชำระหนี้
                        </td>
                        <td>
                            RTL : ยกเลิกโอนหุ้นชำระหนี้
                        </td>
                        <td>
                            SWD : ถอนหุ้น
                        </td>
                        <td>
                            RWD : ยกเลิกถอนหุ้น
                        </td>
                    </tr>
                    <tr>
                        <td>
                            SSR : เก็บเกินจ่ายคืน
                        </td>
                        <td>
                            RSR : ยกเลิกเก็บเกินจ่ายคืน
                        </td>
                        <td>
                            STR : รับโอนหุ้นสมัครใหม่
                        </td>
                        <td>
                            RTR : รับโอนหุ้นสมัครใหม่
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdTabIndex" runat="server" />
    <style type='text/css'>
        .ui-tabs
        {
            font-family: Tahoma;
            font-size: 12px;
            margin-left: 15px;
        }
        #tabs
        {
            width: 755px;
        }
    </style>
</asp:Content>
