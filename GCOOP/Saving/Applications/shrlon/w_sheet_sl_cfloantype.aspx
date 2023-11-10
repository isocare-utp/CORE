<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_cfloantype.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_cfloantype" Title="กำหนดค่าคงที่ระบบสินเชื่อ" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=getLoanConfig%>
    <%=itemChangedReload%>
    <%=getDelete%>
    <%=jssetmbtype%>
    <style type="text/css">
        .style4
        {
            width: 172px;
        }
    </style>
    <script type="text/javascript">

        //บันทึกสร้างเงินกู้ประเภทใหม่
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }

        //click Tab
        function showTabPage2(tab) {
            var i = 1;
            var tabamount = 7;
            for (i = 1; i <= tabamount; i++) {
                document.getElementById("tab_" + i).style.visibility = "hidden";
                document.getElementById("stab_" + i).style.backgroundColor = "rgb(200,235,255)";
                if (i == tab) {
                    document.getElementById("tab_" + i).style.visibility = "visible";
                    document.getElementById("stab_" + i).style.backgroundColor = "rgb(211,213,255)";
                    Gcoop.GetEl("HiddenFieldTab").value = i + "";
                }
            }
        }

        //cick รายการเงินกู้แล้ว Retrieve ข้อมูลแต่ละแท๊บ
        function OnListClick(sender, rowNumber, objectName) {
            try {
                objdw_data_list.SelectRow(Gcoop.GetEl("HiddenFieldRow").value, false);
                var loantype_code = objdw_data_list.GetItem(rowNumber, "loantype_code");
                var loantype_desc = objdw_data_list.GetItem(rowNumber, "loantype_desc");
                //alert("เลือกรายการ " + loantype_code + " : " + loantype_desc);
            } catch (ex) {
                alert("ErrorJS :: Can't getItem " + objectName);
            }
            Gcoop.GetEl("HiddenField1").value = loantype_code;
            Gcoop.GetEl("HiddenFieldRow").value = rowNumber;
            objdw_data_list.SelectRow(rowNumber, true);
            getLoanConfig();
            return 0;
        }

        //   function OnListmbtype(sender, rowNumber, objectName) { }


        function MenubarNew() {
            Gcoop.OpenDlg('350', '150', 'w_dlg_sl_cfloantype_newtype.aspx', '');
        }

        //ส้รางรายการเงินกู้ประเภทใหม่เสร็จแล้วทำการRetrieve
        function NewConfigCode(id) {
            Gcoop.GetEl("HiddenField1").value = id;
            getLoanConfig();
        }

        function ItemChanegdmbtype(s, r, c, v) {
            if (c == "membtype_code") {

                objdw_mbtype.SetItem(r, c, v);
                objdw_mbtype.AcceptText();
                jssetmbtype();
            }

        }
        //item changed แล้ว Refresh หน้าใหม่เพื่อมีผลกับ field อื่นๆ
        function ItemChangedNewReload(s, r, c, v) {
            if (c == "od_flag" || c == "salarybal_status" || c == "counttimecont_type" || c == "reqround_type" || c == "payround_type" || c == "document_code") {
                Gcoop.GetEl("HiddenFieldTab").value = 1;
                objdw_data_1.SetItem(r, c, v);
                objdw_data_1.AcceptText();
                itemChangedReload();
            } if (c == "loanright_type") {
                Gcoop.GetEl("HiddenFieldTab").value = 2;
                objdw_data_21.SetItem(r, c, v);
                objdw_data_21.AcceptText();
                itemChangedReload();
            } if (c == "interest_method" || c == "contint_type" || c == "inttabfix_code" || c == "inttabrate_code") {
                Gcoop.GetEl("HiddenFieldTab").value = 3;
                objdw_data_31.SetItem(r, c, v);
                objdw_data_31.AcceptText();
                itemChangedReload();
            } if (c == "intrate_type") {
                Gcoop.GetEl("HiddenFieldTab").value = 3;
                objdw_data_32.SetItem(r, c, v);
                objdw_data_32.AcceptText();
                itemChangedReload();
            } if (c == "grtneed_flag" || c == "usemangrt_status") {
                Gcoop.GetEl("HiddenFieldTab").value = 4;
                objdw_data_41.SetItem(r, c, v);
                objdw_data_41.AcceptText();
                itemChangedReload();
            } if (c == "useman_type") {
                Gcoop.GetEl("HiddenFieldTab").value = 4;
                objdw_data_42.SetItem(r, c, v);
                objdw_data_42.AcceptText();
                itemChangedReload();
            } if (c == "loancolltype_code" || c == "collmasttype_code") {
                Gcoop.GetEl("HiddenFieldTab").value = 4;
                objdw_data_43.SetItem(r, c, v);
                objdw_data_43.AcceptText();
                itemChangedReload();
            } if (c == "loantype_clear_1") {
                Gcoop.GetEl("HiddenFieldTab").value = 5;
                objdw_data_52.SetItem(r, c, v);
                objdw_data_52.AcceptText();
                itemChangedReload();
            } if (c == "loantype_down_1") {
                Gcoop.GetEl("HiddenFieldTab").value = 7;
                objdw_data_72.SetItem(r, c, v);
                objdw_data_72.AcceptText();
                itemChangedReload();
            }
        }

        //เพิ่มแถว
        function OnInsert(dwname) {
            var dwname = Gcoop.ParseInt(dwname);
            if (dwname == 22) {
                objdw_data_22.InsertRow(objdw_data_22.RowCount() + 1);
                objdw_data_22.SetItem(objdw_data_22.RowCount() + 1, "coop_id", "001001");
                objdw_data_22.AcceptText();
            } if (dwname == 23) {
                objdw_data_23.InsertRow(objdw_data_23.RowCount() + 1);
                objdw_data_23.SetItem(objdw_data_23.RowCount(), "coop_id", "001001");
            } if (dwname == 32) {
                objdw_data_32.InsertRow(objdw_data_32.RowCount() + 1);
                objdw_data_32.SetItem(objdw_data_32.RowCount(), "coop_id", "001001");
            } if (dwname == 42) {
                objdw_data_42.InsertRow(objdw_data_42.RowCount() + 1);
                objdw_data_42.SetItem(objdw_data_42.RowCount(), "coop_id", "001001");
            } if (dwname == 43) {
                objdw_data_43.InsertRow(objdw_data_43.RowCount() + 1);
                objdw_data_43.SetItem(objdw_data_43.RowCount(), "coop_id", "001001");
            } if (dwname == 52) {
                objdw_data_52.InsertRow(objdw_data_52.RowCount() + 1);
                objdw_data_52.SetItem(objdw_data_52.RowCount(), "coop_id", "001001");
            } if (dwname == 53) {
                objdw_data_53.InsertRow(objdw_data_53.RowCount() + 1);
                objdw_data_53.SetItem(objdw_data_53.RowCount(), "coop_id", "001001");
            } if (dwname == 62) {
                objdw_data_62.InsertRow(objdw_data_62.RowCount() + 1);
                objdw_data_62.SetItem(objdw_data_62.RowCount(), "coop_id", "001001");
            } if (dwname == 71) {
                objdw_data_71.InsertRow(objdw_data_71.RowCount() + 1);
                objdw_data_71.SetItem(objdw_data_71.RowCount(), "coop_id", "001001");
            } if (dwname == 72) {
                objdw_data_72.InsertRow(objdw_data_72.RowCount() + 1);
                objdw_data_72.SetItem(objdw_data_72.RowCount(), "coop_id", "001001");
            }
            dwname = null;
        }

        function OnButtonClick42(sender, row, name) {
            if (name == "b_delete") {
                if (confirm("คุณต้องการลบรายการแถว " + row + " ใช่หรือไม่?")) {
                    objdw_data_42.DeleteRow(row);
                }
            }
        }
        function OnButtonClick43(sender, row, name) {
            if (name == "b_delete") {
                if (confirm("คุณต้องการลบรายการแถว " + row + " ใช่หรือไม่?")) {
                    objdw_data_43.DeleteRow(row);
                }
            }
        }
        function OnButtonClick52(sender, row, name) {
            if (name == "b_delete") {
                if (confirm("คุณต้องการลบรายการแถว " + row + " ใช่หรือไม่?")) {
                    objdw_data_52.DeleteRow(row);
                }
            }
        }
        function OnButtonClick53(sender, row, name) {
            if (name == "b_delete") {
                if (confirm("คุณต้องการลบรายการแถว " + row + " ใช่หรือไม่?")) {
                    objdw_data_53.DeleteRow(row);
                }
            }
        }
        function OnButtonClick62(sender, row, name) {
            if (name == "b_delete") {
                if (confirm("คุณต้องการลบรายการแถว " + row + " ใช่หรือไม่?")) {
                    objdw_data_62.DeleteRow(row);
                }
            }
        }
        function OnButtonClick72(sender, row, name) {
            if (name == "b_delete") {
                if (confirm("คุณต้องการลบรายการแถว " + row + " ใช่หรือไม่?")) {
                    objdw_data_72.DeleteRow(row);
                }
            }
        }
        function OnButtonClick32(sender, row, name) {
            if (name == "b_delete") {
                if (confirm("คุณต้องการลบรายการแถว " + row + " ใช่หรือไม่?")) {
                    objdw_data_32.DeleteRow(row);
                }
            }
        }
        function OnButtonClick23(sender, row, name) {
            if (name == "b_delete") {
                if (confirm("คุณต้องการลบรายการแถว " + row + " ใช่หรือไม่?")) {
                    objdw_data_23.DeleteRow(row);
                }
            }
        }
        function OnButtonClick22(sender, row, name) {
            if (name == "b_delete") {
                if (confirm("คุณต้องการลบรายการแถว " + row + " ใช่หรือไม่?")) {
                    objdw_data_22.DeleteRow(row);
                }
            }
        }
        function OnButtonClick(sender, row, name) {
            if (name == "b_delete") {
                if (confirm("คุณต้องการลบรายการแถว " + row + " ใช่หรือไม่?")) {

                }
            } else if (name == "b_searchdoc") {
                //เปิด dlg หาเลขสัญญา
                Gcoop.OpenDlg('450', '400', 'w_dlg_sl_cfloantype_contnodlg.aspx', '');
            }
            return 0;
        }

        //รับค่าเลขรหัสสัญญาอ้างอิงจาก dlg
        function GetContNo(contno) {
            var str_temp = window.location.toString();
            var str_arr = str_temp.split("?", 2);
            objdw_data_1.SetItem(1, "document_code", contno);
        }
        function SheetLoadComplete() {
            var CurTab = Gcoop.ParseInt(Gcoop.GetEl("HiddenFieldTab").value);
            if (isNaN(CurTab)) {
                CurTab = 1;
            }
            showTabPage2(CurTab);

            var rr = Gcoop.GetEl("HiddenFieldRow").value;
            objdw_data_list.SelectRow(rr, true);
        }
        function OnDelete() {
            var idde = Gcoop.GetEl("HiddenField1").value;
            if (confirm("คุณต้องการลบรายการ " + idde + " ใช่หรือไม่?")) {
                getDelete();
            }
        }
        function OnClickCheckbox(s, r, c) {
            //tab1
            if (c == "salarybal_status") {
                Gcoop.CheckDw(s, r, c, "salarybal_status", 1, 0);
                itemChangedReload();
            } else if (c == "od_flag") {
                Gcoop.CheckDw(s, r, c, "od_flag", 1, 0);
                itemChangedReload();
            }
            //tab21 
            else if (c == "lngrpcutright_flag") {
                Gcoop.CheckDw(s, r, c, "lngrpcutright_flag", 1, 0);
            } else if (c == "lngrpkeepsum_flag") {
                Gcoop.CheckDw(s, r, c, "lngrpkeepsum_flag", 1, 0);
            } else if (c == "notmoreshare_flag") {
                Gcoop.CheckDw(s, r, c, "notmoreshare_flag", 1, 0);
            } else if (c == "showright_flag") {
                Gcoop.CheckDw(s, r, c, "showright_flag", 1, 0);
            }

            //tab22
            else if (c == "share_flag") {
                Gcoop.CheckDw(s, r, c, "share_flag", 1, 0);
            } else if (c == "deposit_flag") {
                Gcoop.CheckDw(s, r, c, "deposit_flag", 1, 0);
            } else if (c == "collmast_flag") {
                Gcoop.CheckDw(s, r, c, "collmast_flag", 1, 0);
            }
            //tab41
            else if (c == "grtneed_flag") {
                Gcoop.CheckDw(s, r, c, "grtneed_flag", 1, 0);
                itemChangedReload();
            } else if (c == "usemangrt_status") {
                Gcoop.CheckDw(s, r, c, "usemangrt_status", 1, 0);
                itemChangedReload();
            } else if (c == "chklockshare_flag") {
                Gcoop.CheckDw(s, r, c, "chklockshare_flag", 1, 0);
            } else if (c == "retrycollchk_flag") {
                Gcoop.CheckDw(s, r, c, "retrycollchk_flag", 1, 0);
            } else if (c == "lockshare_flag") {
                Gcoop.CheckDw(s, r, c, "lockshare_flag", 1, 0);
            }
            //tab42
            else if (c == "useshare_flag") {
                Gcoop.CheckDw(s, r, c, "useshare_flag", 1, 0);
            }

            //tab51
            else if (c == "clcfstrcvonly_flag") {
                Gcoop.CheckDw(s, r, c, "clcfstrcvonly_flag", 1, 0);
            } else if (c == "clccclworksht_flag") {
                Gcoop.CheckDw(s, r, c, "clccclworksht_flag", 1, 0);
            }
            //tab52
            else if (c == "chkcontcredit_flag") {
                Gcoop.CheckDw(s, r, c, "chkcontcredit_flag", 1, 0);
            }
            //tab61
            else if (c == "dropprncpay_flag") {
                Gcoop.CheckDw(s, r, c, "dropprncpay_flag", 1, 0);
            }

        }

        function ChangeDropDown(objDd) {
            //alert(Gcoop.GetUrl() + "Applications/" + Gcoop.GetApplication() + "/" + Gcoop.GetCurrentPage() + "?app=" + Gcoop.GetApplication() + "&exdd=" + objDd.value);
            window.location = '<%=linkForeDd%>' + "&exdd=" + objDd.value;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:DropDownList ID="DdMembType" runat="server" onchange="ChangeDropDown(this)">
    </asp:DropDownList>
    <dw:WebDataWindowControl ID="dw_mbtype" runat="server" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        DataWindowObject="d_mbucfmemtype" LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl"
        Width="580px" ClientEventItemChanged="ItemChanegdmbtype" TabIndex="1" Visible="false">
    </dw:WebDataWindowControl>
    <table style="width: 100%;">
        <tr>
            <td class="style4" valign="top">
                <div>
                    <dw:WebDataWindowControl ID="dw_data_list" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        DataWindowObject="d_sl_cfloantype" LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl"
                        Width="180px" Height="450px" ClientEventClicked="OnListClick" TabIndex="1">
                    </dw:WebDataWindowControl>
                    <span class="linkSpan" onclick="OnDelete()" style="font-size: small; color: #808080;
                        float: left">ลบ</span>
                </div>
            </td>
            <td valign="top">
                <table style="width: 100%; border: solid 1px; margin-top: 5px">
                    <tr align="center" class="dwtab">
                        <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab_1" width="14.286%"
                            onclick="showTabPage2(1);">
                            ทั่วไป
                        </td>
                        <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_2" width="14.286%"
                            onclick="showTabPage2(2);">
                            วงเงินกู้
                        </td>
                        <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_3" width="14.286%"
                            onclick="showTabPage2(3);">
                            ดอกเบี้ย
                        </td>
                        <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_4" width="14.286%"
                            onclick="showTabPage2(4);">
                            หลักประกัน
                        </td>
                        <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_5" width="14.286%"
                            onclick="showTabPage2(5);">
                            หักชำระ
                        </td>
                        <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_6" width="14.286%"
                            onclick="showTabPage2(6);">
                            งวดชำระ
                        </td>
                        <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_7" width="14.286%"
                            onclick="showTabPage2(7);">
                            จำกัดการกู้
                        </td>
                    </tr>
                </table>
                &nbsp;<table class="dwcontent" style="width: 100%; margin-top: 2px">
                    <tr>
                        <td style="height: 200px;" valign="top">
                            <div id="tab_1" style="visibility: visible; position: absolute;">
                                <dw:WebDataWindowControl ID="dw_data_1" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_general"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick" ClientEventClicked="OnClickCheckbox"
                                    TabIndex="100">
                                </dw:WebDataWindowControl>
                            </div>
                            <div id="tab_2" style="visibility: hidden; position: absolute;">
                                <dw:WebDataWindowControl ID="dw_data_21" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_rightdet"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick" ClientEventClicked="OnClickCheckbox"
                                    TabIndex="500">
                                </dw:WebDataWindowControl>
                                <br />
                                <dw:WebDataWindowControl ID="dw_data_22" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_rightcollmast"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick22" ClientEventClicked="OnClickCheckbox"
                                    TabIndex="1000">
                                </dw:WebDataWindowControl>
                                <span class="linkSpan" onclick="OnInsert(22)" style="font-size: small; color: #808080;
                                    float: left">เพิ่มแถว</span>
                                <br />
                                <dw:WebDataWindowControl ID="dw_data_23" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_rightcustom"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick23" TabIndex="1500">
                                </dw:WebDataWindowControl>
                                <span class="linkSpan" onclick="OnInsert(23)" style="font-size: small; color: #808080;
                                    float: left">เพิ่มแถว</span>
                            </div>
                            <div id="tab_3" style="visibility: hidden; position: absolute;">
                                <dw:WebDataWindowControl ID="dw_data_31" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_intdetail"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick" TabIndex="2000">
                                </dw:WebDataWindowControl>
                                <br />
                                <dw:WebDataWindowControl ID="dw_data_32" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_intspc"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick32" TabIndex="2500">
                                </dw:WebDataWindowControl>
                                <span class="linkSpan" onclick="OnInsert(32)" style="font-size: small; color: #808080;
                                    float: left">เพิ่มแถว</span>
                            </div>
                            <div id="tab_4" style="visibility: hidden; position: absolute;">
                                <dw:WebDataWindowControl ID="dw_data_41" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_colldet"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick" ClientEventClicked="OnClickCheckbox"
                                    TabIndex="3000">
                                </dw:WebDataWindowControl>
                                <br />
                                <dw:WebDataWindowControl ID="dw_data_42" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_collreqgrt"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick42" ClientEventClicked="OnClickCheckbox"
                                    TabIndex="3500">
                                </dw:WebDataWindowControl>
                                <span class="linkSpan" onclick="OnInsert(42)" style="font-size: small; color: #808080;
                                    float: left">เพิ่มแถว</span>
                                <br />
                                <dw:WebDataWindowControl ID="dw_data_43" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_collcanuse"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick43" TabIndex="4000">
                                </dw:WebDataWindowControl>
                                <span class="linkSpan" onclick="OnInsert(43)" style="font-size: small; color: #808080;
                                    float: left">เพิ่มแถว</span>
                            </div>
                            <div id="tab_5" style="visibility: hidden; position: absolute;">
                                <dw:WebDataWindowControl ID="dw_data_51" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_cleardet"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick" ClientEventClicked="OnClickCheckbox"
                                    TabIndex="4500">
                                </dw:WebDataWindowControl>
                                <br />
                                <dw:WebDataWindowControl ID="dw_data_52" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_clearlist"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick52" ClientEventClicked="OnClickCheckbox"
                                    TabIndex="5000">
                                </dw:WebDataWindowControl>
                                <span class="linkSpan" onclick="OnInsert(52)" style="font-size: small; color: #808080;
                                    float: left">เพิ่มแถว</span>
                                <br />
                                <dw:WebDataWindowControl ID="dw_data_53" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_clearbuyshr"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick53" TabIndex="5500">
                                </dw:WebDataWindowControl>
                                <span class="linkSpan" onclick="OnInsert(53)" style="font-size: small; color: #808080;
                                    float: left">เพิ่มแถว</span>
                            </div>
                            <div id="tab_6" style="visibility: hidden; position: absolute;">
                                <dw:WebDataWindowControl ID="dw_data_61" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_paymentdet"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick" ClientEventClicked="OnClickCheckbox"
                                    TabIndex="6000">
                                </dw:WebDataWindowControl>
                                <br />
                                <dw:WebDataWindowControl ID="dw_data_62" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_paymentlist"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    Width="580px" Height="150px" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                                    ClientEventItemChanged="ItemChangedNewReload" ClientEventButtonClicked="OnButtonClick62"
                                    TabIndex="6500">
                                </dw:WebDataWindowControl>
                                <span class="linkSpan" onclick="OnInsert(62)" style="font-size: small; color: #808080;
                                    float: left">เพิ่มแถว</span>
                            </div>
                            <br />
                            <div id="tab_7" style="visibility: hidden; position: absolute;">
                                <dw:WebDataWindowControl ID="dw_data_71" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_dropln"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick" TabIndex="7000">
                                </dw:WebDataWindowControl>
                                <span class="linkSpan" onclick="OnInsert(71)" style="font-size: small; color: #808080;
                                    float: left">เพิ่มแถว</span>
                                <br />
                                <dw:WebDataWindowControl ID="dw_data_72" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cfloantype_permdown"
                                    LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="ItemChangedNewReload"
                                    ClientEventButtonClicked="OnButtonClick72" TabIndex="7500">
                                </dw:WebDataWindowControl>
                                <span class="linkSpan" onclick="OnInsert(72)" style="font-size: small; color: #808080;
                                    float: left">เพิ่มแถว</span>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenFieldTab" runat="server" />
    <asp:HiddenField ID="HiddenFieldRow" runat="server" />
</asp:Content>
