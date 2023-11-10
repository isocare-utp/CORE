<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_mbshr_req_chgmthshr.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_req_chgmthshr_ctrl.ws_mbshr_req_chgmthshr" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame('580', '590', 'w_dlg_sl_member_chgshr_search.aspx', '');
        }

        function GetDocNoFromDlg(docno) {
            Gcoop.GetEl("HdDocno").value = docno;
            PostGetFromDlg();
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }
            else if (c == "salary_id") {
                PostSalary();
            }
            else if (c == "new_periodvalue") {
                var oldValue = dsMain.GetItem(r, "old_periodvalue");
                PostChanegValue();
            }
            else if (c == "new_paystatus") {
                PostCheckStopShare();
            }
//            if (c == "new_periodvalue") {

//                var sarary = dsMain.GetItem(r, "salary_amount");
//                var share = dsMain.GetItem(r, "new_periodvalue");
//                if(sarary > 30000 && sarary <= 35000 && share > 900 && share < 1800)
//            }
//            alert("test");

        }

        function numberWithCommas(x, fixed) {
            x = x.toFixed(fixed);
            x = x.toString();
            var pattern = /(-?\d+)(\d{3})/;
            while (pattern.test(x))
                x = x.replace(pattern, "$1,$2");
            return x;
        }

        function SheetLoadComplete() {
            DisabledTableFormView1(dsMain,'chgmthpay_satus', 'dsMain', 'textarea');
        }


        function DisabledTableFormView1(s, col , nameDw, findname) {
            var chk = s.GetItem(1, col);
            chk = chk.toString();
            if (findname == null || findname == '') {
                findname = '';
            } else {
                findname = ',' + findname;
            }
            var status;
            if (chk) {
                status = false;
            } else {
                status = true;
            }
            $('#ctl00_ContentPlace_' + nameDw + '_FormView1').find('input,select,button' + findname).attr('disabled', status)
        }

        //เมื่อกด ผ่านรายการ
        $(function () {
            $('#ctl00_ContentPlace_dsMain_FormView1_bt_search').click(function () {
                Gcoop.OpenIFrame("600", "590", "w_dlg_sl_member_search.aspx", "")
            });
        });

        function GetValueFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno);
            PostMemberNo();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HdMemcoopid" runat="server" Value="false" />
    <asp:HiddenField ID="HdDocno" runat="server" Value="false" />
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
