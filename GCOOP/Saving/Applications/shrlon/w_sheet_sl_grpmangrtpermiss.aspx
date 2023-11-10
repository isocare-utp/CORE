<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_grpmangrtpermiss.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_grpmangrtpermiss" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=newRecord %>
    <%=getDetail%>
    <%=deleteRecord%>
    <style type="text/css">
        .style3
        {
            width: 198px;
        }
        .style4
        {
            width: 47px;
        }
    </style>

    <script type="text/javascript">
        function Validate(){
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }
        function OnButtonClickedDetail2(s, row, oName){
            if(oName == "b_delete" ){
                if(confirm("คุณต้องการลบรายการ "+ row +" ใช่หรือไม่?")){
                    objDwDetail2.DeleteRow(row);
                }
            }
            return 0;
        }
         function MenubarNew(){
            if(confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")){
                newRecord();
            }
        }
        function OnListClick(sender, rowNumber, objectName) {
        try{
            objDwList.SelectRow(Gcoop.GetEl("HiddenFieldIdRow").value, false);
            var mangrtpermgrp_code = objDwList.GetItem(rowNumber, "mangrtpermgrp_code");
            var mangrtpermgrp_desc = objDwList.GetItem(rowNumber, "mangrtpermgrp_desc");
            Gcoop.GetEl("HiddenFieldID").value = mangrtpermgrp_code;
            Gcoop.GetEl("HiddenFieldIdRow").value =  rowNumber.toString();
            objDwList.SelectRow(rowNumber, true);
            getDetail();    //retreive data
            }catch(ex){
                alert("error on click");
            }
            return 0;
        }
        function OnAddRowDet(){
            objDwDetail2.InsertRow(objDwDetail2.RowCount() + 1);
        }
        function OnDelete(){
            var mangrtpermgrp_code = objDwDetail1.GetItem(1, "mangrtpermgrp_code");
            var mangrtpermgrp_desc = objDwDetail1.GetItem(1, "mangrtpermgrp_desc");
            if(confirm("คุณต้องการลบรายการ "+ mangrtpermgrp_code + " : " + mangrtpermgrp_desc +" ใช่หรือไม่?")){
                    deleteRecord();
                }
            
        }
        function SheetLoadComplete(){
            var rr =  Gcoop.GetEl("HiddenFieldIdRow").value; 
            objdw_list.SelectRow(rr, true);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td class="style3" valign="top">
                &nbsp;
                <asp:Label ID="Label2" runat="server" Text="กลุ่มการค้ำประกัน" Font-Bold="True" Font-Names="tahoma"
                    Font-Size="12px" ForeColor="#003399"></asp:Label>
                <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_sl_grpmangrtlist"
                    LibraryList="~/DataWindow/shrlon/sl_grpmangrtpermiss.pbl" AutoSaveDataCacheAfterRetrieve="True"
                    AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
                    ClientEventClicked="OnListClick" TabIndex="1">
                </dw:WebDataWindowControl>
                    <span class="linkSpan" onclick="OnDelete()" style="font-family: Tahoma; font-size: small;
                    float: left; color: #808080;">ลบ&nbsp;&nbsp;&nbsp; </span>
                &nbsp;<br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
            <td valign="top">
                &nbsp;
                <asp:Label ID="Label3" runat="server" Text="รายละเอียด" Font-Bold="True" Font-Names="tahoma"
                    Font-Size="12px" ForeColor="#003399"></asp:Label>
                <dw:WebDataWindowControl ID="DwDetail1" runat="server" HorizontalScrollBar="Auto" TabIndex="500"
                    AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True" AutoRestoreContext="False"
                    ClientScriptable="true" DataWindowObject="d_sl_grpmangrtdetail" LibraryList="~/DataWindow/shrlon/sl_grpmangrtpermiss.pbl">
                </dw:WebDataWindowControl>
                <br />
                <asp:Label ID="Label4" runat="server" Text="วงเงินการค้ำประกัน" Font-Bold="True"
                    Font-Names="tahoma" Font-Size="12px" Width="200px" ForeColor="#003399"></asp:Label>
                <br />
                <dw:WebDataWindowControl ID="DwDetail2" runat="server" AutoSaveDataCacheAfterRetrieve="True"
                    AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
                    DataWindowObject="d_sl_grpmangrtpermiss" LibraryList="~/DataWindow/shrlon/sl_grpmangrtpermiss.pbl"
                    ClientEventButtonClicked="OnButtonClickedDetail2" TabIndex="1000">
                </dw:WebDataWindowControl>
                <span class="linkSpan" onclick="OnAddRowDet()"  style="font-family: Tahoma; font-size: small;
                    float: right; color: #808080;">เพิ่มแถว</span>
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;
            </td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenFieldID" runat="server" />
    <asp:HiddenField ID="HiddenFieldIdRow" runat="server" />
</asp:Content>
