<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_finslip_spc.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_finslip_spc_ctrl.w_sheet_finslip_spc" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DwTax.ascx" TagName="DwTax" TagPrefix="uc2" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dwTax = new DataSourceTool();
        var dsList = new DataSourceTool();
        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdShowDisplay").value = "true") {
                document.getElementById('F_dsShow').style.display = 'block';
            } else {
                document.getElementById('F_dsShow').style.display = 'none';
            }
            var payrec_status = dsMain.GetItem(0, "pay_recv_status");
            if (payrec_status == "1") {
                document.getElementById("headbox").style.backgroundColor = "#99d6ff";
                document.getElementById("headbox").innerHTML = "รายการรับ";
            } else if (payrec_status == "0") {
                document.getElementById("headbox").style.backgroundColor = "#ff99cc";
                document.getElementById("headbox").innerHTML = "รายการจ่าย";
            } else if (payrec_status == "9") {
                document.getElementById("headbox").style.backgroundColor = "#ff0000";
                document.getElementById("headbox").style.color = "#800000";
                document.getElementById("headbox").innerHTML = "กรุณา เลือกประเภท การทำรายการ";
            }
            if (parseFloat(dsMain.GetItem(0, "item_amtnet")) > 0) {
                dsMain.GetElement(0, "tax_flag").disabled = false;
                dsMain.GetElement(0, "vat_flag").disabled = false;
                dsMain.GetElement(0, "payment_desc").disabled = false;
            } else {
                dsMain.GetElement(0, "tax_flag").disabled = true;
                dsMain.GetElement(0, "vat_flag").disabled = true;
                dsMain.GetElement(0, "payment_desc").disabled = true;
            }

        }
        function MenubarOpen() {
            Gcoop.OpenIFrame2(750, 550, 'w_dlg_finconfrim.aspx', '')
        }
        function OnDsMainClicked(s, r, c) {
            var payrec_status = dsMain.GetItem(0, "pay_recv_status");
            Gcoop.GetEl("HdPayrec").value = payrec_status;
            if (c != "pay_recv_status") {
                if (payrec_status == "9") {
                    alert("กรุณาเลือกประเภทรายการรับ/จ่าย!!"); return false;
                }
                if (c == "b_member") {
                    var type_member = dsMain.GetItem(0, "member_flag");
                    if (type_member == "1") {
                        Gcoop.OpenIFrame2(700, 550, 'ws_dlg_fin_member_search.aspx', '');
                    } else if (type_member == "0") {
                        Gcoop.OpenIFrame2(500, 550, 'ws_dlg_fin_extmember_search.aspx', '');
                    }
                } else if (c == "b_accid") {
                    Gcoop.GetEl("HdDlgAccid").value = 1;
                    Gcoop.OpenIFrame2(490, 550, 'ws_dlg_fin_accid.aspx', '');
                }
            }
        }
        // รับค่าจากการค้นหาสมาชิก
        function GetMemNoFromDlg(memberNo) {
            dsMain.SetItem(0, "member_no", memberNo)
            PostInitMember();
        }
        function GetContackFromDlg(contack) {
            dsMain.SetItem(0, "member_no", contack)
            PostInitMember();
        }
        function GetAccidFromDlg(accid) {
            var chk_colum = Gcoop.GetEl("HdDlgAccid").value;
            if (chk_colum == "1") {
                dsMain.SetItem(0, "tofrom_accid", accid);
                Gcoop.GetEl("HdDlgAccid").value = "0";
            } else if (chk_colum == "2") {
                var rownum = Gcoop.GetEl("HdDetailRow").value;
                dsList.SetItem(rownum, "slipitemtype_code", accid)
                Gcoop.GetEl("HdDlgAccid").value = "0";
                PostInitSlipdesc();
            }

        }
        function GetSlipNoFromDlg(SlipNo, coopid) {
            Gcoop.GetEl("HdDlg").value = 1;
            Gcoop.GetEl("HdSlipno").value = SlipNo;
            Gcoop.GetEl("HdCoopid").value = coopid;
            PostInitMember();
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "pay_recv_status") {
                PostInitPayRecv();
            } else if (c == "cash_type") {
                PostInitAccid();
            } else if (c == "member_no") {
                PostInitMember();
            } else if (c == "vat_flag") {
                PostCalVat();
            } else if (c == "tax_flag") {
                PostCalVat();
            } else if (c == "tax_code") {
                PostCalVat();
            } else if (c == "bank_code") {
                PostBankBranch();
            } else if (c == "member_flag") {
                dsMain.SetItem(0, "member_no", "");
                dsMain.SetItem(0, "nonmember_detail", "");
            }
        }

        function OnDsListClicked(s, r, c) {
            var payrec_status = dsMain.GetItem(0, "pay_recv_status");
            if (payrec_status == "9") {
                alert("กรุณาเลือกประเภทรายการรับ/จ่าย!!"); return false;
            }
            //set at row
            Gcoop.GetEl("HdDetailRow").value = r + "";
            if (c == "b_sliptypecode") {
                Gcoop.GetEl("HdDlgAccid").value = 2;
                Gcoop.OpenIFrame2(490, 550, 'ws_dlg_fin_accid.aspx', '');
            }
        }
        function GetDataFromDlg(remain, cashdetail) {
            var rownum = Gcoop.GetEl("HdDetailRow").value;
            dsList.SetItem(rownum, "itempay_amt", remain);
            //sum ยอดรวม
            var amount = 0;
            for (var i = 0; i < dsList.GetRowCount(); i++) {
                amount += parseFloat(dsList.GetItem(i, "itempay_amt"));
            }
            dsMain.SetItem(0, "item_amtnet", amount);
            dsList.SetItem(rownum, "cash_detail", cashdetail);
            if (parseFloat(amount) > 0) {
                dsMain.GetElement(0, "tax_flag").disabled = false;
                dsMain.GetElement(0, "vat_flag").disabled = false;
            } else {
                dsMain.GetElement(0, "tax_flag").disabled = true;
                dsMain.GetElement(0, "vat_flag").disabled = true;
            }
        }
        function OnDsDsListItemChanged(s, r, c, v) {
            if (c == "slipitemtype_code") {
                PostInitSlipdesc();
            } else if (c == "itempay_amt") {
                var amount = 0;
                for (var i = 0; i < dsList.GetRowCount(); i++) {
                    var operate_flag = dsList.GetItem(i, "operate_flag");
                    if (operate_flag == "-1") {
                        amount -= parseFloat(dsList.GetItem(i, "itempay_amt"));
                    }
                    else if (operate_flag == "1") {
                        amount += parseFloat(dsList.GetItem(i, "itempay_amt"));
                    }
                    else {
                        amount += parseFloat(dsList.GetItem(i, "itempay_amt"));
                    }

                }
                if (parseFloat(amount) > 0) {
                    dsMain.GetElement(0, "tax_flag").disabled = false;
                    dsMain.GetElement(0, "vat_flag").disabled = false;
                } else {
                    dsMain.GetElement(0, "tax_flag").disabled = true;
                    dsMain.GetElement(0, "vat_flag").disabled = true;
                }
                PostCalVat();
            }
        }
        function OnClickInsertRow() {
            PostInsertRow();
        }
        function OnClickDeleteRow() {
            if (dsList.GetRowCount() > 0) {
                var currentRow = Gcoop.GetEl("HdDetailRow").value;
                currentRow = parseInt(currentRow) + 1;
                var confirmText = "ยืนยันการลบแถวที่ " + currentRow;
                if (confirm(confirmText)) {
                    PostDeleteRow();
                }
            }
            else {
                alert("ยังไม่มีการเพิ่มแถวสำหรับรายการ");
            }
        }
        function OnClickPrint() {
            PostPrint();
        }
        function Validate() {
            var alertstr = "", nonmember_detail = "", cash_type = "", payment_desc = "";
            var recvpay_status = dsMain.GetItem(0, "pay_recv_status");
            var item_amtnet = dsMain.GetItem(0, "item_amtnet");
            if (recvpay_status == 9) {
                alertstr += "- กรุณาเลือกประเภทรายการรับ/จ่าย\n";
            }
            if (dsMain.GetItem(0, "nonmember_detail") == null) {
                alertstr += "- กรุณาระบุชื่อผู้ติดต่อ\n";
            }
            if (dsMain.GetItem(0, "member_flag") == "0" || dsMain.GetItem(0, "member_flag") == "1") {
                if (dsMain.GetItem(0, "member_no") == null) {
                    alertstr += "- กรุณากรอกรหัสสมาชิก หรือ รหัสบุคคลภายนอก\n";
                }
            }
            if (dsMain.GetItem(0, "payment_desc") == null) {
                alertstr += "- กรุณากรอกรายละเอียดการทำรายการ\n";
            }
            //            if (parseFloat(dsMain.GetItem(0, "item_amtnet")) <= 0) {
            //                alertstr += "- กรุณาระบุจำนวนเงินทำรายการ\n";
            //            }
            var amount = 0;
            for (var i = 0; i < dsList.GetRowCount(); i++) {
                var operate_flag = dsList.GetItem(i, "operate_flag");
                if (operate_flag == "-1") {
                    amount -= parseFloat(dsList.GetItem(i, "itempay_amt"));
                }
                else if (operate_flag == "1") {
                    amount += parseFloat(dsList.GetItem(i, "itempay_amt"));
                }
                else {
                    amount += parseFloat(dsList.GetItem(i, "itempay_amt"));
                }
            }
            amount = amount + dsMain.GetItem(0, "vat_amt") - dsMain.GetItem(0, "tax_amt");
            //ปัด 2 ตำแหน่ง
            amount = amount.toFixed(2);
            var item_amtnet = parseFloat(dsMain.GetItem(0, "item_amtnet"));
            item_amtnet = item_amtnet.toFixed(2);
            //            if (parseFloat(item_amtnet) != parseFloat(amount)) {
            //                alertstr += "- จำนวนเงินทำรายการกับรายละเอียดไม่ตรงกัน\n";
            //            }

            if (recvpay_status == 1 && cash_type == "CHQ") {
                if (dsMain.GetItem(0, "bank_code") == null) {
                    alertstr += "- กรุณากรอกรายละเอียดธนาคาร\n";
                }
                if (dsMain.GetItem(0, "bank_branch") == null) {
                    alertstr += "- กรุณากรอกรายละเอียดสาขาธนาคาร\n";
                }
                if (dsMain.GetItem(0, "account_no") == null) {
                    alertstr += "- กรุณากรอกรายละเอียดเลขที่เช็ค\n";
                }
                //                if (dateon_chq null ) {
                //                    alertstr += "- กรุณากรอกรายละเอียดวันที่หน้าเช็ค\n";
                //                }
            }
            //dsList
            var from_system = dsMain.GetItem(0, "from_system");
            if (from_system != "SHL" || from_system != "LON" || from_system != "INV" || from_system != "DEP") {
                for (var i = 0; i < dsList.GetRowCount(); i++) {
                    if (parseFloat(dsList.GetItem(i, "itempay_amt")) <= 0 || dsList.GetItem(i, "slipitemtype_code") == null || dsList.GetItem(i, "slipitem_desc") == null) {
                        alertstr += "- รายละเอียดรายการ กรอกรายละเอียดไม่ครบถ้วน กรุณาตรวจสอบ\n";
                    }
                }
            }
            if (alertstr == "") {
                return confirm("ยืนยันการบันทึกข้อมูล");
            } else {
                alert(alertstr);
                return false;
            }
            //            if (Gcoop.GetEl("HdPayrec").value == "1") {
            //                if (Gcoop.GetEl("HdPrintReceipt").value == "true") {
            //                    if (confirm("ยืนยันการพิมพ์ใบเสร็จ")) {
            //                        Gcoop.GetEl("HdPrintReceipt").value = "false";
            //                        PostPrint();
            //                    }
            //                }
            //            } 
        } 
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div id="F_dsShow" style="display: none;">
        <input type="button" value="พิมพ์ใบเสร็จ" style="width: 100px;" onclick="OnClickPrint()" />
        <div id="headbox" style="border: 1px solid #000000; text-align: center; height: 32px;
            width: 96%; font-weight: bolder; font-size: 25px;">
            กรุณา เลือกประเภท การทำรายการ</div>
        <uc1:DsMain ID="dsMain" runat="server" />
        <br />
        <span id="F_dwTax" style="display: none">
            <uc2:DwTax ID="dwTax" runat="server" />
        </span>
        <br />
        <asp:Label ID="lb_slipdet" runat="server" Text="รายการ" ForeColor="#000099" Font-Strikeout="False"
            Font-Bold="True" Font-Underline="True" Font-Size="Large"></asp:Label>
        &nbsp; <span onclick="OnClickInsertRow()" style="cursor: pointer;">
            <asp:Label ID="LbInsert2" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
                Font-Size="14px" Font-Underline="True" ForeColor="#006600" /></span> &nbsp;&nbsp;
        <span onclick="OnClickDeleteRow()" style="cursor: pointer;">
            <asp:Label ID="LbDel2" runat="server" Text="ลบแถว" Font-Bold="False" Font-Names="Tahoma"
                Font-Size="14px" Font-Underline="True" ForeColor="Red" /></span>
        <br />
        <uc3:DsList ID="dsList" runat="server" />
    </div>
    <%--<br />
    <input type="button" value="พิมพ์" style="width:100px;" onclick="OnClickPrint()" />--%>
    <asp:HiddenField ID="HdDetailRow" runat="server" />
    <asp:HiddenField ID="HdDlg" Value="0" runat="server" />
    <asp:HiddenField ID="HdSlipno" runat="server" />
    <asp:HiddenField ID="HdSlip_no" runat="server" />
    <asp:HiddenField ID="HdCoopid" runat="server" />
    <asp:HiddenField ID="HdPayrec" runat="server" />
    <asp:HiddenField ID="HdDlgAccid" Value="0" runat="server" />
    <asp:HiddenField ID="HdPrintReceipt" Value="flase" runat="server" />
    <asp:HiddenField ID="HdShowDisplay" Value="flase" runat="server" />
</asp:Content>
