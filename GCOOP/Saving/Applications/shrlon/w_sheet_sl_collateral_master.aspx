<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_collateral_master.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_collateral_master" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=jsPostMember%>
    <%=PostMember1%>
    <%=jsPostCollmast%>
    <%=newClear %>
    <%=jsRefresh %>
    <%=postInsertRow %>
    <%=jsgetmember%>
    <%=jsDeleteRow %>
    <%=jsprintColl%>
    <%=jspostPorvince%>
    <script type="text/javascript">
        var fromDlg;
        function Validate() {
            objdw_head.AcceptText();
            objdw_detail.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function ItemChangedMain(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "00000000"));
                objdw_main.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objdw_main.GetItem(rowNumber, "member_no");
                jsPostMember();
            }

        }
        function selectRow(s, r, c) {
            var collmast_no = objdw_list.GetItem(r, "collmast_no");
            Gcoop.GetEl("HfSlipNo").value = Gcoop.Trim(collmast_no);
            Gcoop.GetEl("Hmastno").value = collmast_no;
            jsPostCollmast();
        }

        function CnvNumber(num) {
            if (IsNum(num)) {
                return parseFloat(num);
            }
            return 0;
        }
        function MenubarNew() {

            newClear();

        }
        function MenubarOpen() {
            Gcoop.OpenDlg('580', '590', 'w_dlg_sl_collmaster_search_mb_.aspx', '');
        }
        function GetValuecollmaster_search_mb(memco_no) {
            objdw_main.SetItem(1, "member_no", memco_no);
            objdw_main.AcceptText();
            Gcoop.GetEl("Hfmember_no").value = memco_no;
            jsPostMember();
        }
        function dw_main_search(s, r, c) {
            if (c == "b_main") {
                fromDlg = "b_main";
                Gcoop.OpenDlg('580', '590', 'w_dlg_member_search.aspx', '');
            }
        }
        function dw_detail_search1(s, r, c) {
            if (c == "b_main1") {
                fromDlg = "b_main1";
                Gcoop.OpenDlg('580', '590', 'w_dlg_member_search.aspx', '');
            }
        }
        function GetMemDetFromDlg(memberno, prename_desc, memb_name, memb_surname, card_person) {
            if (fromDlg == "b_main") {
                objdw_main.SetItem(1, "member_no", memberno);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = memberno;
                jsPostMember();

            }
            else if (fromDlg == "b_main1") {
                var name = prename_desc + memb_name + " " + memb_surname;
                objdw_detail_01.SetItem(1, "mrtg_memberno", memberno);
                objdw_detail_01.SetItem(1, "mrtg_name", name);
                objdw_detail_01.SetItem(1, "mrtg_personid", card_person);
                Gcoop.GetEl("Hmemnodetail").value = memberno;
                Gcoop.GetEl("Hname").value = name;
                Gcoop.GetEl("Hmemnodetail").value = card_person;




            }

        }
        function dw_head_Click(s, r, c) {
            if (c == "b_landsideno") {
                Gcoop.OpenDlg('580', '590', 'w_dlg_sl_collmaster_search.aspx', '');
                Gcoop.GetEl("Hhead").value = Gcoop.Trim(c);
            }
        }
        function GetValueFromDlgCollmast(collmast_refno) {

            objdw_head.SetItem(1, "landside_no", collmast_refno);
            objdw_head.AcceptText();

        }
        function dw_detail_search(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenDlg('580', '590', 'w_dlg_search_mb.aspx', '');

            }
            else if (c == "b_delete") {

                Gcoop.GetEl("HDeleteRow").value = r + "";
                objdw_detail.SetItem(r, c);
                objdw_detail.AcceptText();
                jsDeleteRow();


            } return 0;
        }

        function GetMemSeachMb(memberno) {
            var j = 1;
            for (j = 1; j <= objdw_detail.RowCount(); j++) {
                Gcoop.GetEl("Hmemco_no").value = memberno;
                jsgetmember();
            }
        }
        function Changedetail(sender, rowNumber, columnName, newValue) {

            objdw_detail.SetItem(rowNumber, columnName, newValue);
            objdw_detail.AcceptText();
            if (columnName == "memco_no") {
//                dw_detail.SetItem(rowNumber, columnName, newValue);
//                dw_detail.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = newValue);
                jsgetmember();
            }

        }



        function ListClick(s, r, c) {
            Gcoop.CheckDw(s, r, c, "redeem_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "blindland_flag", 1, 0);
            if (c == "redeem_flag") {
                jsRefresh();
            }
            else if (c == "blindland_flag") {
                jsRefresh();
            }
        }

        function InsertRow() {

            postInsertRow();

        }

        function InsertRowdetail() {
            objdw_detail.InsertRow(objdw_detail.RowCount() + 1);
        }
        function HeadChange(sender, rowNumber, columnName, newValue) {
            if (columnName == "landestimate_amt" || columnName == "houseestimate_amt") {
                var landestimate_amt = 0;
                var houseestimate_amt = 0;
                var estimate_price = 0;
                objdw_head.SetItem(rowNumber, columnName, newValue);
                objdw_head.AcceptText();
                landestimate_amt = Gcoop.ParseFloat(objdw_head.GetItem(1, "landestimate_amt"));
                houseestimate_amt = Gcoop.ParseFloat(objdw_head.GetItem(1, "houseestimate_amt"));
                estimate_price = landestimate_amt + houseestimate_amt;
                objdw_head.SetItem(1, "estimate_price", estimate_price);
                objdw_head.AcceptText();
            }
        }
        function showTabPage(tab) {
            var i = 1;
            var tabamount = 6;
            for (i = 1; i <= tabamount; i++) {
                document.getElementById("tab" + i).style.visibility = "hidden";
                document.getElementById("stab_" + i).style.backgroundColor = "rgb(200,235,255)";
                if (i == tab) {
                    document.getElementById("tab" + i).style.visibility = "visible";
                    document.getElementById("stab_" + i).style.backgroundColor = "rgb(211,213,255)";
                    Gcoop.GetEl("HiddenFieldTab").value = i + "";
                }
            }
        }
        function SheetLoadComplete() {
            var CurTab = Gcoop.ParseInt(Gcoop.GetEl("HiddenFieldTab").value);

            if (isNaN(CurTab)) {
                CurTab = 1;
            }
            showTabPage(CurTab);
        }
        function Changedetail_mrtg1(sender, rowNumber, columnName, newValue) {
            if (columnName == "pos_province") {
                Gcoop.GetEl("Hf_province").value = columnName;
                objdw_collddetail.SetItem(rowNumber, columnName, newValue);
                objdw_collddetail.AcceptText();
                jspostPorvince();
            }
        }

        function Changedetail_mrtg2(sender, rowNumber, columnName, newValue) {

            if (columnName == "coll_printset") {
                Gcoop.GetEl("Hf_province").value = columnName;
                objdw_detail_01.SetItem(rowNumber, columnName, newValue);
                objdw_detail_01.AcceptText();
                jspostPorvince();
            }
            else if (columnName == "autrz_province") {
                Gcoop.GetEl("Hf_province").value = columnName;
                objdw_detail_01.SetItem(rowNumber, columnName, newValue);
                objdw_detail_01.AcceptText();
                jspostPorvince();
            }
            else if (columnName == "mrtg_memberno") {
                objdw_detail_01.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "00000000"));
                objdw_detail_01.AcceptText();
                Gcoop.GetEl("Hmemno").value = objdw_detail_01.GetItem(rowNumber, "mrtg_memberno");
                PostMember1();
            }
            else if (columnName == "mrtg_province") {
                Gcoop.GetEl("Hf_province").value = columnName;
                objdw_detail_01.SetItem(rowNumber, columnName, newValue);
                objdw_detail_01.AcceptText();
                jspostPorvince();
            }
        }
        function Changedetail_mrtg4(sender, rowNumber, columnName, newValue) {
            //alert(columnName);
            if (columnName == "coll_printset") {
                objdw_printset.SetItem(rowNumber, "coll_printset", newValue);
                objdw_printset.AcceptText();
                //alert(newValue);
                Gcoop.GetEl("Hprintset").value = objdw_printset.GetItem(rowNumber, "coll_printset");
            }
        }

        function OnDwClickPrint(s, r, c) {
            if (c == "b_print") {
                jsprintColl();
            }
        }
        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="Hdatail" runat="server" />
    <asp:HiddenField ID="Hmemco_no" runat="server" />
    <asp:HiddenField ID="Hmem_name" runat="server" />
    <asp:HiddenField ID="HfSlipNo" runat="server" />
    <asp:HiddenField ID="Hhead" runat="server" />
    <asp:HiddenField ID="HDeleteRow" runat="server" />
    <asp:HiddenField ID="Hf_province" runat="server" />
    <asp:HiddenField ID="HiddenFieldTab" runat="server" />
    <asp:HiddenField ID="Hprintset" runat="server" />
    <asp:HiddenField ID="Hmastno" runat="server" />
    <asp:HiddenField ID="Hmemno" runat="server" Value="" />
    <asp:HiddenField ID="Hname" runat="server" Value="" />
    <asp:HiddenField ID="Hmemnodetail" runat="server" Value="" />
    <asp:HiddenField ID="Hcard" runat="server" Value="" />
    <table style="width: 100%;">
        <tr>
            <td colspan="2" valign="top"> 
                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_loanrv_info_memdetail"
                    LibraryList="~/DataWindow/shrlon/sl_collateral_master.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    Width="720px" ClientEventItemChanged="ItemChangedMain" ClientEventButtonClicked="dw_main_search">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td style="background-color: rgb(200,235,255);" id="Td1" align="center" width="10%">
                รายการหลักทรัพย์
            </td>
            <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab_1" onclick="showTabPage(1);"
                align="center" width="12%" height="15%">
                รายละเอียด1
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_2" onclick="showTabPage(2);"
                align="center" width="12%">
                รายละเอียด2
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_3" onclick="showTabPage(3);"
                align="center" width="12%">
                พิมพ์จำนอง1
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_4" onclick="showTabPage(4);"
                align="center" width="12%">
                พิมพ์จำนอง2
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_5" onclick="showTabPage(5);"
                align="center" width="12%">
                พิมพ์เอกสาร
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_6" onclick="showTabPage(6);"
                align="center" width="12%">
                ทบทวนราคา
            </td>
        </tr>
        <tr>
            <td valign="top">
                <dw:WebDataWindowControl ID="dw_list" runat="server" DataWindowObject="d_loansrv_lncollmast_list"
                    LibraryList="~/DataWindow/shrlon/sl_collateral_master.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="True" ClientEventClicked="selectRow">
                </dw:WebDataWindowControl>
            </td>
            <td colspan="4" valign="top">
                <div id="tab1" style="visibility: visible; position: absolute;">
                    <a href="#" onclick="InsertRow()">
                        <asp:Label ID="Label1" runat="server" Text="เพิ่มหลักทรัพย์" CssClass="linkInsertRow"></asp:Label>
                    </a>
                    <dw:WebDataWindowControl ID="dw_head" runat="server" DataWindowObject="d_loansrv_lncollmast_detail"
                        LibraryList="~/DataWindow/shrlon/sl_collateral_master.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientFormatting="True" ClientEventClicked="ListClick" ClientEventButtonClicked="dw_head_Click"
                        ClientEventItemChanged="HeadChange">
                    </dw:WebDataWindowControl>
                    <a href="#" onclick="InsertRowdetail()">
                        <asp:Label ID="Label2" runat="server" Text="เพิ่มผู้กู้" CssClass="linkInsertRow"></asp:Label>
                    </a>
                    <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_loansrvl_lncollmast_memco"
                        LibraryList="~/DataWindow/shrlon/sl_collateral_master.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientFormatting="True" ClientEventButtonClicked="dw_detail_search" ClientEventItemChanged="Changedetail">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab2" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_collddetail" runat="server" DataWindowObject="d_loansrv_lncollmast_mrtg1"
                        LibraryList="~/DataWindow/shrlon/sl_collateral_master.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientFormatting="True" ClientEventButtonClicked="dw_detail_search" ClientEventItemChanged="Changedetail_mrtg1">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab3" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_detail_01" runat="server" DataWindowObject="d_loansrv_lncollmast_mrtg2"
                        LibraryList="~/DataWindow/shrlon/sl_collateral_master.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientFormatting="True" ClientEventButtonClicked="dw_detail_search1" ClientEventItemChanged="Changedetail_mrtg2">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab4" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_detail_02" runat="server" DataWindowObject="d_loansrv_lncollmast_mrtg3"
                        LibraryList="~/DataWindow/shrlon/sl_collateral_master.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientFormatting="True" ClientEventButtonClicked="dw_detail_search" ClientEventItemChanged="Changedetail_mrtg3">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab5" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_printset" runat="server" DataWindowObject="d_sl_collprint_list"
                        LibraryList="~/DataWindow/shrlon/sl_collateral_master.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientFormatting="True" ClientEventButtonClicked="OnDwClickPrint" ClientEventItemChanged="Changedetail_mrtg4">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab6" style="visibility: hidden;  position: absolute;">
                    <dw:WebDataWindowControl ID="dw_review" runat="server" DataWindowObject="d_loansrv_lncollmast_review"
                        LibraryList="~/DataWindow/shrlon/sl_collateral_master.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientFormatting="True" ClientEventButtonClicked="dw_detail_search" ClientEventItemChanged="Changedetail_mrtg5">
                    </dw:WebDataWindowControl>
                
                </div>
                <br />
                <asp:HiddenField ID="HdOpenIFrame" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
