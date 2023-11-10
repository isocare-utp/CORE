<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_mb_add_newgroup.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_mb_add_newgroup" Title="Untitled Page" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=deleteRecord%>
    <%=filterDistrict%>
    <%=bSearch%>
    <%=itemChangedReload%>
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
        function OnButtonClicked(s, row, oName){
            if(oName == "b_delete" ){
                var membgroup_code = objDwList.GetItem(row, "membgroup_code");
                var membgroup_desc = objDwList.GetItem(row, "membgroup_desc");
                if(confirm("คุณต้องการลบรายการ "+ membgroup_code + " " + membgroup_desc +" ใช่หรือไม่?")){
                Gcoop.GetEl("HiddenFieldID").value = membgroup_code;
                deleteRecord();
                }
            }
            else if(oName == "b_search"){
                bSearch();
            }
            return 0;
        }
         function MenubarNew(){
            if(confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")){
                objDwList.InsertRow(1);
            }
        }
        function OnClickCheckbox(s,r,c){
            if(c== "keep_flag"){
                Gcoop.CheckDw(s, r, c, "keep_flag", 1, 0);
            }
        }
        function OnSearch(s, r, c, v){
            objDwSearch.SetItem(r, c, v);
            objDwSearch.AcceptText();
            bSearch();
        }
        function OnItemChanged(s, r, c, v){
            if( c=="membgroup_province"){
                objDwList.SetItem(r, c, v);
                objDwList.AcceptText();
                filterDistrict();
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
     <tr>
            <td class="style3">
                &nbsp;
                <dw:WebDataWindowControl ID="DwSearch" runat="server" AutoRestoreContext="False" 
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                    ClientScriptable="True" DataWindowObject="d_mb_group_goto" 
                    LibraryList="~/DataWindow/shrlon/mb_add_newgroup.pbl" ClientEventButtonClicked="OnButtonClicked"
                    TabIndex="1" ClientEventItemChanged="OnSearch">
                    <PageNavigationBarSettings>
                        <BarStyle Font-Size="9px" />
                    </PageNavigationBarSettings>
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td class="style3" valign="top">
                &nbsp;
                <asp:Label ID="Label2" runat="server" Text="รายการ" Font-Bold="True" Font-Names="tahoma"
                    Font-Size="12px"></asp:Label>
                <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_mb_group_master"
                    
                    LibraryList="~/DataWindow/shrlon/mb_add_newgroup.pbl;~/DataWindow/shrlon/cmcomddw.pbl" AutoSaveDataCacheAfterRetrieve="True"
                    AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
                    ClientEventClicked="OnClickCheckbox" 
                    ClientEventItemChanged="OnItemChanged" Height="480px" Width="740px" TabIndex="100" 
                    RowsPerPage="15" ClientEventButtonClicked="OnButtonClicked">
                    <PageNavigationBarSettings Position="TopAndBottom" Visible="True">
                        <BarStyle Font-Size="11px" />
                        <PageStatusInfo TextFormat="หน้า {C} / {T}" Visible="True" />
                    </PageNavigationBarSettings>
                </dw:WebDataWindowControl>
                &nbsp;<br />
              <%--  <span class="linkSpan" onclick="MenubarNew()" 
                    style="font-family: Tahoma; font-size: small; float: left; color: #808080;">
                เพิ่มแถว</span><br />--%>
                <br />
            </td>
        </tr>
       
    </table>
    <asp:HiddenField ID="HiddenFieldID" runat="server" />
    </asp:Content>

