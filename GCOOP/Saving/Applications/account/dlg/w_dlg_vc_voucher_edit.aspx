<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_vc_voucher_edit.aspx.cs"
    Inherits="Saving.Applications.account.dlg.w_dlg_vc_voucher_edit" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        #B_ok
        {
            width: 70px;
        }
        .objDw-main3C86
        { ;background-color:transparent;OVERFLOW:hidden}
        .objDw-main3C89
        { ;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:right;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-main3C8A
        { ;background-color:#e6e6e6;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-main3C8D
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-main3C92
        { ;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
        .objDw-search3CAA
        { ;background-color:transparent;OVERFLOW:hidden}
        .objDw-search3CAB
        { ;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:12pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
        .objDw-search3CAD
        { ;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:right;WORD-BREAK:break-all;BORDER-STYLE:none}
        .objDw-search3CAF
        { ;background-color:transparent;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-search3CB0
        { ;background-color:transparent;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-search3CB1
        { ;COLOR:#000000;FONT:12pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:center}
        .objDw-accid3CB3
        { ;background-color:#ffffff}
        .objDw-accid3CB4
        { ;background-color:transparent;BORDER-STYLE:outset}
        .objDw-accid3CB6
        { ;background-color:#d3e7ff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:none}
        .objDw-accid3CBA
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:none}
        .objDw-accid3CBB
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:none}
        .objDw-detail3C6B
        { ;background-color:transparent;OVERFLOW:hidden}
        .objDw-detail3C6C
        { ;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C71
        { ;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:12pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
        .objDw-detail3C76
        { ;background-color:#e6e6e6;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C77
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C78
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C7A
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:right;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C7B
        { ;background-color:#d4d0c8;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:right;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C7C
        { ;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center}
        .objDw-detail3C7D
        { ;background-color:#e6e6e6;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C80
        { ;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:right;WORD-BREAK:break-all;BORDER-STYLE:none}
        .objDw-detail3C81
        { ;background-color:#ffffff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:right;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C83
        { ;background-color:#ffffff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:right;WORD-BREAK:break-all;BORDER-STYLE:none}
        #B_add
        {
            width: 100px;
        }
        #B_insert
        {
            width: 100px;
        }
        #B_save
        {
            width: 100px;
        }
        #B_refresh
        {
            width: 100px;
        }
        #B_cancel
        {
            width: 100px;
        }
        *
        {
            padding: 0;
            margin: 0 0 0 2px;
        }
        .linkSpan
        {
            float: right;
            cursor: pointer;
        }
        .style9
        {
            width: 391px;
        }
        .style10
        {
            width: 214px;
        }
    </style>
</head>
<%=postAccountID%>
<%=postInsertDwDetail%>
<%=postDropdownAccid%>
<%=postInsertAfterDwDetail%>
<%=postAccountSide%>
<%=postVoucherType%>
<%=postVoucherEdit%>
<%=postRefresh%>
<%=postAddnewUpdateVoucher%>
<%=postDeleteRowDetail %>
<%=postKeyAccId %>
<%=postKeyAccSearch%>
<%=postDwAccidClick %>
<%=postDrCr%>
<%=postNew%>
<%=jsPostTempate%>
<%=postGetTemplate%>


<%--<%=postSetitemCashType%>--%>
<script type="text/javascript">

 // ฟังก์ชันการเพิ่มแถวข้อมูล
    function Insert_Dw_detail() {
        Gcoop.GetEl("HdDetailRow").value = (objDw_detail.RowCount() + 1) + ""; // focus
         postInsertDwDetail();
     }

     function OnTemplateClick() {
         postGetTemplate();
     }
     
     //ฟังก์ชันการลบแถวทีละแถว
     function OnDwSearchClick(s, r, c){
        if (c=="b_search") { 
            var account_id = objDw_search.GetItem(1,"account_id");
            //Gcoop.GetEl("Hd")
            postKeyAccSearch();   
        }
    }

//    function DwMainItemChange(s, r, c, v) {
//        s.SetItem(r, c, v);
//        s.AcceptText();
//        postSetitemCashType();
//    }

    function OnItemChange(s, r, c, v) {
        //if (c == "account_id") {
            objDw_search.SetItem(r, c, v);
            objDw_search.AcceptText();
//            Gcoop.GetEl("Hd_accid").value = r + "";
//            Gcoop.GetEl("Hd_accid").value = v;
            postKeyAccSearch();
//        }
        }

    function OnDwAccidClick(s, r, c){
        if (c=="account_id" || c== "account_name") { 
            var account_id = "";
            var account_name = "";
            account_id = objDw_accid.GetItem(r,"account_id");
            account_name = objDw_accid.GetItem(r,"account_name");
            Gcoop.GetEl("HdAccid").value = account_id;
            Gcoop.GetEl("HdAccname").value = account_name;
            Gcoop.GetEl("HdDwAccidRow").value = r + "";
            
            //if (confirm ("ต้องการเพิ่มรหัสบัญชี : " + account_id + "ใช่หรือไม่ ?")) 
            //{
                postDwAccidClick();
                //}
        }
    }

     //ฟังก์ชันการลบแถวทีละแถว
     function OnDwListClick(s, r, c){
        if (c=="b_del") {
           if (confirm ("ยืนยันการลบแถวข้อมูล")){
                var row = r;
                Gcoop.GetEl("HdDetailRow").value = row+"";
                postDeleteRowDetail();
            }
        }
      }
    
     //ฟังก์ชันการแทรกแถวข้อมูล
     function InsertAfter_Dw_detail() {
        postInsertAfterDwDetail();
    }
    //ฟังก์ชั่นการรีเฟรชข้อมูล
    function OnRefreshClick() {
//        postRefresh();
    }
     
     //ฟังก์ชันการส่ง currentrow ให้ Hiden Field
     function Dw_detailItemFocusChanged(s,r,c) {
        Gcoop.GetEl("HdCurrentrow").value = r + "";
     }
//     
     //ฟังก์ชันที่เปลี่ยน dropdownlist แล้วให้ชื่อ account_id ขึ้นอัตโนมัติ
     function Dw_detailItemChange(s,r,c,v) {
        if (c == "account_id") {
            objDw_detail.SetItem(r,c,v);
            objDw_detail.AcceptText();
            Gcoop.GetEl("HdDetailRow").value = r + "";   // ใช้กับ focus
            Gcoop.GetEl("Hd_accid").value = r +"";
            postKeyAccId();
            
//        }else if (c == "account_side") {
//            objDw_detail.SetItem(r,c,v);
//            objDw_detail.AcceptText();
//            postAccountSide();
        }else if (c == "dr_amt"){
            objDw_detail.SetItem(r,c,v);
            objDw_detail.AcceptText();
            Gcoop.GetEl("HdDetailRow").value = r + "";    // ใช้กับ focus
            Gcoop.GetEl("HdCurrentrow").value = r + "";
            if (v < 0) {
                confirm("ยอด Dr มีค่าติดลบ กรุณาตรวจสอบ");
                postDrCr();
            }
                 objDw_detail.SetItem(r, "account_side", "DR");
            postAccountID();
                        
        }else if(c == "cr_amt"){
            objDw_detail.SetItem(r,c,v);
            objDw_detail.AcceptText();
            Gcoop.GetEl("HdDetailRow").value = r + "";    // ใช้กับ focus
            Gcoop.GetEl("HdCurrentrow").value = r + "";
            if (v < 0) {
                confirm("ยอด Cr มีค่าติดลบ กรุณาตรวจสอบ");
                postDrCr();
            }
            objDw_detail.SetItem(r, "account_side", "CR");
            postAccountID();
        }
     }
     
//  

     //ฟังก์ชันในการปิด dialog
     function OnCloseDialog() {
         if (confirm("ยืนยันการออกจากหน้าจอ ")) {

             window.opener.postVoucherDate();
             window.close()
             //parent.RemoveIFrame();
             postRefresh();
        }
   }

   //ฟังก์ชันปัดเศษทศนิยม num = เลขที่ต้องการปัด, dec = จำนวนทศนิยมที่ต้องการ
   function fncToFixed(num, dec) {
       if (typeof (pre) != 'undefined' && pre != null) { var decimals = dec; } else { var decimals = 2; }

       num *= Math.pow(10, decimals);
       num = (Math.round(num, decimals) + (((num - Math.round(num, decimals)) >= 0.4) ? 1 : 0)) / Math.pow(10, decimals);
       return num.toFixed(decimals);
   }
     
     //ฟังก์ชันในการกดปุ่ม ตกลง
//     function OnOkClick() {
//       if (confirm ("ยืนยันการบันทึกข้อมูล ")){
//           postAddnewUpdateVoucher();
////           postRefresh();
//        }
     //     }
     function OnOkClick() {
         var rowcount = objDw_detail.RowCount();
         var dr = 0;
         var cr = 0;
         for (var i = 1; i <= rowcount; i++) {
             dr += objDw_detail.GetItem(i, "dr_amt");
             cr += objDw_detail.GetItem(i, "cr_amt");
         }
         dr = fncToFixed(dr, 2);
         cr = fncToFixed(cr, 2);
         if (rowcount > 0) {
             if (dr == cr) {
                 if (confirm("ยืนยันการบันทึกข้อมูล")) {
                     postAddnewUpdateVoucher();
                     postNew();
                 }
             }
             else {
                 confirm("ยอด Dr และ Cr ไม่เท่ากัน กรุณาตรวจสอบ");
             }
         }
         else {
             confirm("กรุณาเพิ่มรายการก่อนบันทึก");
         }
     }
     
     //ฟังก์ชันการตรวจสอบให้ตกลงแล้วให้ปิดหน้าจอแล้ว refresh ค่า
     function DialogLoadComplete() {
         if (Gcoop.GetEl("HdOpenIFrame").value == "True") {
             Gcoop.GetEl("HdOpenIFrame").value = "False";
             //Gcoop.OpenIFrame("575", "455", "w_dlg_search_accmaster.aspx", "");
             Gcoop.OpenDlgIn("475", "400", "w_dlg_template.aspx", "");
         }
        var checkfinish = Gcoop.GetEl("HdIsFinished").value;
//        var CheckRefresh = Gcoop.GetEl("HdRefresh").value;
        if (checkfinish == "true") { 
//            window.parent.postVoucherDate();
//                      parent.RemoveIFrame();
//            postRefresh();
            postNew();
     }else if (CheckRefresh == "true") {
            postRefresh();
        }
    }

    function GetTemplate(vcauto_code, vcauto_desc, vcauto_type) {
        Gcoop.GetEl("Hdtemp_code").value = vcauto_code;
        Gcoop.GetEl("Hdtemp_desc").value = vcauto_desc;
        Gcoop.GetEl("Hdtemp_type").value = vcauto_type;
        jsPostTempate();
    }

    function SheetLoadComplete() {
        SetFocusDWListClick("Dw_detail");
        SetFocusDWAccidClick("Dw_accid");
    }
    function SetFocusDWListClick(Dwobj) {
        //var rowcount = objDw_detail.RowCount() - 1;
        var idx = Number(Gcoop.GetEl("HdDetailRow").value) - 1;
        //alert(idx);
        if (idx < 1) {
            idx = 0;
        }
        var sel = "#obj" + Dwobj + "_datawindow input[name='account_id_" + idx + "']";
        $(sel).focus();
    }

    function SetFocusDWAccidClick(Dwobj) {
        //var rowcount = objDw_detail.RowCount() - 1;
        var idx = Number(Gcoop.GetEl("HdDwAccidRow").value) - 1;
        //alert(idx);
        if (idx < 1) {
            idx = 0;
        }
        var sel = "#obj" + Dwobj + "_datawindow input[name='account_id_" + idx + "']";
        $(sel).focus();
    }

</script>
<body>
    <form id="form1" runat="server">
    <div style="font-size: small; font-family: Tahoma">
        <asp:Literal ID="LtVoucerMessage" runat="server"></asp:Literal>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width: 100%;">
            <tr>
                <td colspan="3" valign="top">
                    <b>รายละเอียด</b>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <asp:Panel ID="Panel6" runat="server" BorderStyle="Ridge" Height="53px" Width="938px">
                        <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_vc_vcupdate_main"
                            LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientEventItemChanged="DwMainItemChange" ClientScriptable="True" Style="top: 0px; 
                            left: 0px; width: 715px">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td valign="top" class="style10">
                    <b>ค้นหาข้อมูล รหัสบัญชี/ชื่อบัญชี</b>
                </td>
                <td colspan="2" valign="top">
                    <b>รายการ</b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    <input id="Button1" type="button" value="Template" onclick="OnTemplateClick()" />
                </td>
            </tr>
            <tr>
                <td valign="top" class="style10">
                    <asp:Panel ID="Panel7" runat="server" BorderStyle="Ridge" Height="52px" Width="200px">
                        <dw:WebDataWindowControl ID="Dw_search" runat="server" DataWindowObject="d_vc_accmaster_search"
                            LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" Style="top: 0px; left: 0px;
                            width: 269px" AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                            ClientEventClicked="OnDwSearchClick" ClientScriptable="True" ClientEventItemChanged="OnItemChange">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td colspan="2" valign="top" rowspan="2">
                    <asp:Panel ID="Panel9" runat="server" BorderStyle="Ridge" Height="315px" ScrollBars="Vertical"
                        Width="720px">
                        <dw:WebDataWindowControl ID="Dw_detail" runat="server" DataWindowObject="d_vc_vcupdate_detail"
                            LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" Style="top: 0px; left: 1px;
                            height: 75px; width: 728px;" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientEventClicked="OnDwListClick"
                            ClientEventItemChanged="Dw_detailItemChange" ClientEventItemFocusChanged="Dw_detailItemFocusChanged">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td valign="top" class="style10">
                    <asp:Panel ID="Panel8" runat="server" BorderStyle="Ridge" Height="250px" ScrollBars="Vertical"
                        Width="200px">
                        <dw:WebDataWindowControl ID="Dw_accid" runat="server" DataWindowObject="dddw_vc_accmaster_key"
                            LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            ClientEventClicked="OnDwAccidClick" Style="top: 0px; left: 0px; width: 329px;
                            height: 47px;">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td valign="top" class="style10">
                </td>
                <td valign="top" class="style9">
                    &nbsp;<input id="B_add" type="button" value="เพิ่มแถว" onclick="Insert_Dw_detail()" />&nbsp;<input
                        id="B_insert" type="button" value="แทรกแถว" onclick="InsertAfter_Dw_detail()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td valign="top">
                    <%--<input id="B_refresh" type="button" value="รีเฟรช" onclick="OnRefreshClick()" />--%>
                    <input id="B_save" type="button" value="ตกลง" onclick="OnOkClick()" />
                    <input id="B_cancel" type="button" value="ยกเลิก" onclick="OnCloseDialog()" />
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:HiddenField ID="HdAccname" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HdRefresh" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HdCurrentrow" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HdIsFinished" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="HdAccid" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HSqlTemp" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="Hd_accid" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HdDetailRow" runat="server" />
                    <asp:HiddenField ID="vcno" runat="server" />
                    <asp:HiddenField ID="Hdtemp_code" runat="server" />
                    <asp:HiddenField ID="Hdtemp_desc" runat="server" />
                    <asp:HiddenField ID="Hdtemp_type" runat="server" />
                    <asp:HiddenField ID="HdOpenIFrame" runat="server" />
                    <asp:HiddenField ID="Hdrow" runat="server" />
                    <asp:HiddenField ID="HdDwAccidRow" runat="server" />
                    
                    
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
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
        </table>
        <br />
    </div>
    </form>
</body>
</html>
