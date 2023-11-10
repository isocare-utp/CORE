<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_sharetype_detail.aspx.cs"
    Inherits="Saving.Applications.mbshr.w_sheet_sl_sharetype_detail" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=itemChangedReload%>
    <%=insertRowTab2%>
    <%=ShareTypeChange%>
    <%=shareRateChange %>
    <script type="text/javascript">
        function Validate(){
            objdw_data1.AcceptText();
            objdw_data2.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }
        
        //click Tab
        function showTabPage2(tab) {
            var i = 1;
            var tabamount = 2;
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
//        function ItemChangedNewReload(s,r,c,v){
//            
//            if(c == "chgcount_type"){
//            objdw_data1.SetItem(r, c, v);
//            objdw_data1.AcceptText();
//                itemChangedReload();
//            }
//            //item change not reload in javaScript
//            
//            
        //        }

        function OnDwItemChange(s, r, c, v) {

            if (c == "chgcount_type") {
                objdw_data1.SetItem(r, c, v);
                objdw_data1.AcceptText();
                itemChangedReload();
            }
            if (c == "sharegroup_code") {
                objdw_data1.SetItem(r, c, v);
                objdw_data1.AcceptText();
                ShareTypeChange();
            }

        }
        function SheetLoadComplete(){
            var CurTab = Gcoop.ParseInt( Gcoop.GetEl("HiddenFieldTab").value);
            if(isNaN( CurTab)){
                CurTab = 1;
            }
            showTabPage2(CurTab);
        }
       
        
         function OnButtonClick(sender, row, name){
            if(name == "b_delete"){
                if(confirm("คุณต้องการลบรายการแถว "+ row +" ใช่หรือไม่?")){
                    objdw_data2.DeleteRow(row);
                    //alert("อย่าลืมบันทึกรายการ");
                }
            }else if(name == "b_search"){
                //เปิด dlg หาข้อมูล
                Gcoop.OpenIFrame('400', '325', 'w_dlg_sl_sharetype_search.aspx', '');
            }
            return 0;
        }
        function NewShareType(sharecode, sharedesc){
            objdw_main.SetItem(1, "sharetype_code", sharecode);
            objdw_main.SetItem(1, "sharetype_desc", sharedesc);
            Gcoop.GetEl("HiddenField1").value = sharecode.toString();
            
        }
         //รับค่ารายการเงินกู้จากค้นหา
        function GetShareType(sharecode, sharedesc){
            var str_temp = window.location.toString();
            var str_arr = str_temp.split("?", 2); 
            objdw_main.SetItem(1, "sharetype_code", sharecode);
            objdw_main.SetItem(1, "sharetype_desc", sharedesc);
            Gcoop.GetEl("HiddenField1").value = sharecode.toString();
        }
        function OnInsertRowTab2(){
            insertRowTab2();           
        }
        function OnItemChangedTab2(s, r, c, v){
            objdw_data2.SetItem(r, c, v);
            objdw_data2.AcceptText();
            if(c == "sharemonth_amt"){
                var shval = objdw_data1.GetItem(1, "share_value");
                var result = shval*v; 
                objdw_data2.SetItem(r, "c_sharemonth_value", result);
                objdw_data2.SetItem(r, "share_value", shval);
                objdw_data2.AcceptText();
            }else if(c == "maxsharemonth_amt"){
                var shval = objdw_data1.GetItem(1, "share_value");
                var result = shval*v; 
                objdw_data2.SetItem(r, "c_maxshareamt", result);
                objdw_data2.SetItem(r, "share_value", shval);
                objdw_data2.AcceptText();
            }
        }

        function OnDwMembtypeItemChange(s, r, c, v) {

            objdw_membtype.AcceptText();
            objdw_membtype.SetItem(r, c, v);
            if (c == "member_type") {
                shareRateChange();
            }
        }
    </script>

    <style type="text/css">
        table.t
        {
            border-width: medium;
            border-spacing: 2px;
            border-style: ridge;
            border-color: gray;
            border-collapse: separate;
            background-color: white;
        }
        table.t th
        {
            border-width: 1px;
            padding: 1px;
            border-style: none;
            border-color: gray;
            background-color: white;
            -moz-border-radius: 0px 0px 0px 0px;
        }
        table.t td
        {
            border-width: 1px;
            padding: 1px;
            border-style: none;
            border-color: gray;
            background-color: white;
            -moz-border-radius: 0px 0px 0px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td class="style4" valign="top">
                <div>
                    &nbsp;</div>
            </td>
            <td valign="top">
                <div id="Div1" style="visibility: visible; position: absolute;">
                </div>
                <table style="width: 50%; border: solid 1px; margin-top: 5px">
                    <tr align="center" class="dwtab">
                        <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab_1" width="50%"
                            onclick="showTabPage2(1);">
                            รายละเอียด
                        </td>
                        <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_2" width="50%"
                            onclick="showTabPage2(2);">
                            การส่งหุ้นขั้นต่ำรายเดือน
                        </td>
                    </tr>
                </table>
                &nbsp;<table class="dwcontent" style="width: 100%; margin-top: 2px">
                    <tr>
                        <td style="height: 200px;" valign="top">
                            <div id="tab_1" style="visibility: visible; position: absolute;">
                                <dw:WebDataWindowControl ID="dw_data1" runat="server" ClientScriptable="True" DataWindowObject="d_sl_sharetype_detail"
                                    LibraryList="~/DataWindow/mbshr/sl_sharetype_detail.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="OnDwItemChange"
                                    TabIndex="100">
                                </dw:WebDataWindowControl>
                            </div>
                            <div id="tab_2" style="visibility: hidden; position: absolute;">
                             <dw:WebDataWindowControl ID="dw_membtype" runat="server" ClientScriptable="True" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                                    ClientEventItemChanged="OnDwMembtypeItemChange" ClientFormatting="True" DataWindowObject="d_sl_membtype"
                                    LibraryList="~/DataWindow/mbshr/sl_sharetype_detail.pbl" 
                                    TabIndex="200" Height="50px" Width="740px">
                                </dw:WebDataWindowControl>
                            <span class="linkSpan" style="font-family: tahoma; font-size: small; color: #808080;
                                    float: left;" onclick="OnInsertRowTab2()">เพิ่มแถว</span>
                                <dw:WebDataWindowControl ID="dw_data2" runat="server" ClientScriptable="True" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="OnButtonClick"
                                    ClientEventItemChanged="OnItemChangedTab2" ClientFormatting="True" DataWindowObject="d_sl_sharetype_mthrate"
                                    LibraryList="~/DataWindow/mbshr/sl_sharetype_detail.pbl" 
                                    TabIndex="1000" Height="500px" Width="740px">
                                </dw:WebDataWindowControl>
                                
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenFieldTab" runat="server" />
    <asp:HiddenField ID="HdUser" runat="server" />
    <asp:HiddenField ID="HdCheckRow" Value = "0" runat="server" />
</asp:Content>
