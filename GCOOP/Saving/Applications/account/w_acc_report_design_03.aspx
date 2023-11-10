<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_report_design_03.aspx.cs"
    Inherits="Saving.Applications.account.w_acc_report_design_03" Title="Untitled Page" %>

<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript%>
<%=postDeleteRow %>
<%=postNewClear%>

<script type="text/javascript" >
       function OnDwMainClick(s,r,c)
     {
        if(c== "compare_b1_b3"){
        Gcoop.CheckDw(s, r, c, "compare_b1_b3", 1, 0);
        }else if(c=="show_remark"){
         Gcoop.CheckDw(s, r, c, "show_remark", 1, 0);
        }else if(c=="percent_status"){
        Gcoop.CheckDw(s, r, c, "percent_status", 1, 0);
        }   
     }
     
     function OnDwShowClick(s,r,c)
     {
         Gcoop.CheckDw(s, r, c, "show_status", 1, 0);
         Gcoop.CheckDw(s, r, c, "show_det_status1", 1, 0);
         Gcoop.CheckDw(s, r, c, "show_det_status3", 1, 0);
         Gcoop.CheckDw(s, r, c, "up_line", 1, 0);    
     }
     
    
     
     function OnDwShowButtonClick(s,r,c){
        if(c=="b_del")
        {
            if(confirm("ยืนยันการลบแถวข้อมูล ?"))
            {
                Gcoop.GetEl("HdRowDelete").value = r +"";
                postDeleteRow();
            }
        }
        return 0;
     }
     
     function MenubarNew(){
        if(confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")){
            postNewClear();
        }
    }
      
    function Validate()
    { 
        return confirm("ยืนยันการบันทึกข้อมูล");
    }

    
</script>  

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table style="width: 100%;">
        <tr>
            <td colspan="6" valign="top">
                <asp:Panel ID="Panel1" runat="server">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" 
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                        ClientScriptable="True" DataWindowObject="d_acc_report_design_master" 
                        LibraryList="~/DataWindow/account/acc_report_design.pbl" 
                        ClientEventClicked="OnDwMainClick">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr > 
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td width="200" height="20">
                <asp:DropDownList ID="Showformula" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="Showformula_SelectedIndexChanged" BackColor="#CCCCCC">
                    <asp:ListItem Value="2">ซ่อนสูตร</asp:ListItem>
                    <asp:ListItem Value="1">แสดงสูตร</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="200" height="20">
                <asp:DropDownList ID="Showselect" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="Showselect_SelectedIndexChanged" BackColor="#CCCCCC">
                    <asp:ListItem Value="1">แสดงทั้งหมด</asp:ListItem>
                    <asp:ListItem Value="2">แสดงเฉพาะส่วนที่เลือกแสดงผล</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="500" height="20">
            </td>
            </tr>
            <tr>
         <%--   <td colspan="3" width="500" height="20">
                <%--<asp:Button ID="B_sortseq" runat="server" Text="จัดเลขที่ลำดับใหม่" 
                    onclick="B_sortseq_Click" />--%>
       <%--         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="B_process" runat="server" Text="จัดทำงบกระแสเงินสด" 
                    onclick="B_process_Click" />                
            </td> --%>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Panel ID="Panel2" runat="server" Height="498px" ScrollBars="Auto" 
                    Width="750px" BorderStyle="Ridge">
                    <dw:WebDataWindowControl ID="Dw_show" runat="server" 
                        DataWindowObject="d_acc_report_design_detail" 
                        LibraryList="~/DataWindow/account/acc_report_design.pbl" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" 
                        ClientEventClicked="OnDwShowClick" 
                        ClientEventButtonClicked="OnDwShowButtonClick">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <br />
                <%--<asp:Button 
--%><%--                    ID="B_add" runat="server" Text="เพิ่มแถว" onclick="B_add_Click" 
                    UseSubmitBehavior="False" Width="70px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Button ID="B_insert" runat="server" 
                    Text="แทรกแถว" onclick="B_insert_Click" UseSubmitBehavior="False" 
                    Width="70px" />--%>
                <asp:HiddenField ID="HdRowDelete" runat="server" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:Button ID="B_backprocess" runat="server" Text="ปรับเงื่อนไขใหม่" onclick="B_backprocess_Click" Visible="False" />
    <table style="width: 100%;">
        <tr>
            <td valign="top" align="right">
                <dw:WebDataWindowControl ID="dw_rpt" runat="server" AutoRestoreContext="False" 
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                    ClientScriptable="True" DataWindowObject="d_acc_pl" 
                    LibraryList="~/DataWindow/account/acc_report_design.pbl" 
                    BorderColor="#99CCFF" BorderStyle="Double" Visible="False">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
    <tr align =left ><font color ="blue"><b>สูตรงบกระแสเงินสดที่ใช้ในการคำนวณ </b></font> 
            <td><font color ="red" size="2">ADD : นำเลขกลุ่มผลรวมมาบวกกัน</font></td>
            <td><font color ="red" size="2">SUB : นำเลขกลุ่มผลรวมมาลบกัน</font></td>
            <td><font color ="red" size="2">CON : ค่าคงที่</font></td>
            <td><font color ="red" size="2">CSH : เงินสด</font></td>
    </tr>
    <tr>
            <td><font color ="red" size="2">OLW : ยอดยกมา - ยอดคงเหลือ</font></td>
            <td><font color ="red" size="2">OLD : ยอดคงเหลือ – ยอดยกมา</font></td>
            <td><font color ="red" size="2">RPD : ยอดระหว่างเดือน Dr – Cr</font></td>
            <td><font color ="red" size="2">RPC : ยอดระหว่างเดือน Cr – Dr</font></td>
    </tr>
    <tr>
            <td><font color ="red" size="2">PAD : เอายอดระหว่างเดือนฝั่ง Dr</font></td>
            <td><font color ="red" size="2">PAC : เอายอดระหว่างเดือนฝั่ง Cr</font></td>
            <td><font color ="red" size="2">OLF : ยอดคงเหลือ</font></td>
            <td><font color ="red" size="2">OLB : ยอดยกมา</font></td>
    </tr>
    </table>
    <asp:HiddenField ID="HdSheetTypeCode" runat="server" />
    <asp:HiddenField ID="HdSheetHeadName" runat="server" />
    <asp:HiddenField ID="HdSheetHeadCol1" runat="server" />
    <asp:HiddenField ID="HdSheetHeadCol2" runat="server" />
</asp:Content>
