<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_convert_txt2db.aspx.cs" Inherits="Saving.Applications.shrlon.ws_convert_txt2db_ctrl.ws_convert_txt2db" %>

<%@ Register Src="DsHeader.ascx" TagName="DsHeader" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<%@ Register Src="DsTailer.ascx" TagName="DsTailer" TagPrefix="uc3" %>
<%@ Register Src="DsDetailFail.ascx" TagName="DsDetailFail" TagPrefix="uc4" %>
<%@ Register Src="DsDetailFinish.ascx" TagName="DsDetailFinish" TagPrefix="uc5" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc6" %>
<%@ Register Src="DsDetailEtc.ascx" TagName="DsDetailEtc" TagPrefix="uc7" %>
<%@ Register Src="DsDetailShare.ascx" TagName="DsDetailShare" TagPrefix="uc8" %>
<%@ Register Src="DsDetailLoan.ascx" TagName="DsDetailLoan" TagPrefix="uc9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .FiletxtCSS
        {
            position: absolute;
            padding-left: 21ex;
            padding-top: 0.5ex;
        }
        .txtMemberFail, .txtFinish
        {
            font-size: x-large;
        }
        #ctl00_ContentPlace_dsHeader_FormView1_ls_bank, #ctl00_ContentPlace_dsHeader_FormView1_ls_effdate, #ctl00_ContentPlace_dsHeader_FormView1_ls_company, #ctl00_ContentPlace_dsTailer_FormView1_sum_row, #ctl00_ContentPlace_dsTailer_FormView1_sum_amt, #ctl00_ContentPlace_txtInput
        {
            background-color: #FFFFAE;
        }
        #ctl00_ContentPlace_txtInput
        {
            background-color: #E6E6E6;
        }
    </style>
    <%=postDatatxt%>
    <%=Onclickbpost%>
    <%=Onclickbnotpost%>
    <%=Onclickpostagain%>
    <script type="text/javascript">
        dsMain = new DataSourceTool();
        dsHeader = new DataSourceTool();
        dsTailer = new DataSourceTool();

        var dsMain = new DataSourceTool;
        var dsDetailShare = new DataSourceTool;
        var dsDetailLoan = new DataSourceTool;
        var dsDetailEtc = new DataSourceTool;

        //ทำงานเหมือน load begin
        $(function () {
            $('.td_hiddenF, .DetailFail_H, .Detail_F, .b_postagin').hide();
            //$('input[type="checkbox"]').prop('disabled', true);
            $('.td_rowFinish input[type="checkbox"]').prop('disabled', true);
            $('.MemberFail').text("");
            $('.txtFinish, .txtMemberFinish, .txtMemberFail').hide();
            $('.Detail_H').show();

            if (dsHeader.GetItem(0, "ls_filename")) {
                $('.txtFileName').text(dsHeader.GetItem(0, "ls_filename"));
            }

            $('.HidenPanel').hide()

            //ถ้า R ถูก click
            $('.Detail_H th:contains("R")').click(function () {
                for (var RR = 0; RR < dsDetail.GetRowCount(); RR++) {
                    dsDetail.SetItem(RR, "reject_status", 1);
                }
            });

            //ถ้า R ถูก doubleclick
            $('.Detail_H th:contains("R")').dblclick(function () {
                for (var RR = 0; RR < dsDetail.GetRowCount(); RR++) {
                    dsDetail.SetItem(RR, "reject_status", 0);
                }
            });

            $('.Detail_H th:contains("R")').css("cursor", "pointer");

            //นับแถวและตรวจสอบว่า row ไหนผิดพลาด
            var n = 1, nn = 1, nnn = 1, m = 0;
            $('.td_row').each(function () {
                $(this).find('.num_row').val(n)
                n++

                if ($(this).find('.chk_member_c').val() == "T") {
                    $(this).find('.ls_cusname_').css('background-color', '')
                }
                else if ($(this).find('.chk_member_c').val() == "R") {
                    $(this).find('.ls_cusname_').css('background-color', 'red')
                }
                else {
                    $(this).find('.ls_cusname_').css('background-color', 'red')
                    m += 1;
                }
                $('.MemberFail').text(m)
            });
            chk_err();

            //เมื่อมีการ upoload file  
            $(".Filetxt").change(function () {
                postDatatxt();
            });

            //เมื่อกด ผ่านรายการ
            $('.b_post').click(function () {
                Gcoop.OpenIFrame3("500", "400", "dlg_sl_bankbranch.aspx", "");
            });

            //เมื่อกด ดึงข้อมูลที่ไม่ผ่าน
            $('.b_notpost').click(function () {
                Onclickbnotpost();
            });

            //เมื่อกด ผ่านรายการอีกครั้ง
            $('.b_postagin').click(function () {
                Onclickpostagain();
            });

            //นับแถวและตรวจสอบว่า row ไหนผิดพลาด(DetailFail)
            $('.td_rowFail').each(function () {
                $(this).find('.num_rowFail').val(nn)
                nn++

                if ($(this).find('.chk_member_cFail').val() == "T") {
                    $(this).find('.ls_cusname_Fail').css('background-color', '')
                }
                else if ($(this).find('.chk_member_cFail').val() == "R") {
                    $(this).find('.ls_cusname_Fail').css('background-color', 'red')
                }
                else {
                    $(this).find('.ls_cusname_Fail').css('background-color', 'red')
                }
            });

            //นับแถวและตรวจสอบว่า row ไหนบันทึกไปแล้ว(DetailFinish)
            $('.td_rowFinish').each(function () {
                $(this).find('.num_rowFinish').val(nnn)
                nnn++

                if ($(this).find('.chk_member_cFinish').val() == "T") {
                    $(this).find('.ls_cusname_Finish').css('background-color', '')
                }
                else if ($(this).find('.chk_member_cFinish').val() == "R") {
                    $(this).find('.ls_cusname_Finish').css('background-color', 'red')
                }
                else {
                    $(this).find('.ls_cusname_Finish').css('background-color', 'red')
                }
            });
        });

        function chk_err() {
            //ตรวจสอบว่าผิดพลาดจำนวนกี่ row 
            var x = $('#ctl00_ContentPlace_dsTailer_FormView1_sum_row').val()
            if (x == "0") {
                //ปิดไม่ให้ปุ่มสามารถใช้งานได้
                SetDisable("#ctl00_ContentPlace_dsTailer_FormView1_b_notpost", true)
                SetDisable("#ctl00_ContentPlace_dsTailer_FormView1_b_post", true)
            } else {
                //เปิดใช้งานปุ่ม
                SetDisable("#ctl00_ContentPlace_dsTailer_FormView1_b_notpost", false)
                SetDisable("#ctl00_ContentPlace_dsTailer_FormView1_b_post", false)
            }

            //เมื่อมีข้อมูลผิดพลาดให้แสดงข้อความ
            if ($('.MemberFail').text() == 0) {
                //$('.txtMemberFail').hide()
                SetDisable("#ctl00_ContentPlace_dsTailer_FormView1_b_notpost", true)
            } else {
                //$('.txtMemberFail').show()
                SetDisable("#ctl00_ContentPlace_dsTailer_FormView1_b_notpost", false)
                //                alert("เกิดข้อผิดพลาดในการทำรายการโปรดตรวจสอบข้อมูลอีกครั้ง")
            }
        }

        //เอาไว้เช็คว่า row ไหนที่ไม่ถูกต้อง 
        function chk_errAlerttxt() {
            var n = 1, m = 0;
            $('.td_row').each(function () {
                if ($(this).find('.chk_member_c').val() == "T") {
                    $(this).find('.ls_cusname_').css('background-color', '')
                }
                else if ($(this).find('.chk_member_c').val() == "R") {
                    $(this).find('.ls_cusname_').css('background-color', '')
                }
                else {
                    $(this).find('.ls_cusname_').css('background-color', 'red')
                    m += 1;
                }
            });

            //ปิดไม่ให้ปุ่มสามารถใช้งานได้
            SetDisable("#ctl00_ContentPlace_dsTailer_FormView1_b_post", true)
        }

        //เอาไว้กำหนดว่า row ไหนจะแสดงสีแดง
        function chk_chgRGB(Element) {
            var n = 1, m = 0;
            $('.td_row').each(function () {
                if ($(this).find('.chk_member_c').val() == "T") {
                    $(this).find(Element).css('background-color', '')
                }
                else if ($(this).find('.chk_member_c').val() == "R") {
                    $(this).find(Element).css('background-color', '')
                }
                else {
                    $(this).find(Element).css('background-color', 'red')
                    m += 1;
                }
            });

            SetDisable("#ctl00_ContentPlace_dsTailer_FormView1_b_notpost", false)
            $('.MemberFail').text(0)
        }

        function disableBT(Element) {
            SetDisable("#ctl00_ContentPlace_dsTailer_FormView1_b_notpost", Element)
            SetDisable("#ctl00_ContentPlace_dsTailer_FormView1_b_post", Element)
        }

        function RecBankCode(bank_code) {
            dsTailer.SetItem(0, "ls_tofromaccid", bank_code);
            Onclickbpost();
        }

        function SheetLoadComplete() {
            $('.Detail_H th:contains("R")').click();
        } 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div class="txtMemberFail" style="color: Red; padding-bottom: 1ex;">
        <center>
            <span>ทะเบียนสมาชิกจำนวน</span>&nbsp<span class="MemberFail"></span>&nbsp<span>รายการไม่มีในระบบ</span>
        </center>
    </div>
    <div class="txtFinish" style="color: Green; padding-bottom: 1ex;">
        <center>
            <span>ทำรายการเรียบร้อย</span>
        </center>
    </div>
    <div>
        <span class="txtFileName"></span>
    </div>
    <div class="FiletxtCSS">
        <asp:FileUpload ID="txtInput" class="Filetxt" runat="server" /></div>
    <uc1:DsHeader ID="dsHeader" runat="server" />
    <br />
    <uc2:DsDetail ID="dsDetail" runat="server" />
    <uc4:DsDetailFail ID="dsDetailFail" runat="server" />
    <br />
    <uc3:DsTailer ID="dsTailer" runat="server" />
    <br />
    <div class="txtMemberFinish" style="color: Green; padding-bottom: 1ex;">
        <center>
            <span>ข้อมูลที่ผ่านรายการไปแล้ว</span>
        </center>
    </div>
    <uc5:DsDetailFinish ID="dsDetailFinish" runat="server" />
    <div class="HidenPanel">
        <uc6:DsMain ID="dsMain" runat="server" />
        <uc7:DsDetailEtc ID="dsDetailEtc" runat="server" />
        <uc8:DsDetailShare ID="dsDetailShare" runat="server" />
        <uc9:DsDetailLoan ID="dsDetailLoan" runat="server" />
    </div>
</asp:Content>
