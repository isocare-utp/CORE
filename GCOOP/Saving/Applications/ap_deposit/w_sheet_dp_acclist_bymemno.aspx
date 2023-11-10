<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_acclist_bymemno.aspx.cs" 
Inherits="Saving.Applications.ap_deposit.w_sheet_dp_acclist_bymemno" Title="Untitled Page"  %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=postSelectFlag%>
<%=postShowMember%>
 <%=postShowDetail %>
    <script type="text/javascript">

        function DwMainItemChanged(s, r, c, v) {
            s.SetItem(r, c, v);
            s.AcceptText();
            switch (c) {
                case "select_flag":
                    postSelectFlag();
                    break;
                case "member_no":
                    Gcoop.Trim(v);
                    Gcoop.GetEl("HdMembno").value = v;
                    postShowMember();
                    break;
            }
        }
        function Validate() {
            if (confirm("ต้องการบันทึกข้อมูลใช่หรือไม่")) {
                return true;
            }
            else {
                return false;}
        }
        function Dw_listClick(s, r, c) {
            var deptaccount_no = objDwAccdeptList.GetItem(r, "deptaccount_no");
            Gcoop.GetEl("Hd_deptaccountno").value = deptaccount_no;
            Gcoop.GetEl("Hd_row").value = r + "";
            postShowDetail();
        }
        function showTabPage(tab) {
            var i = 1;
            var tabamount = 4;
            for (i = 1; i <= tabamount; i++) {
                document.getElementById("tab" + i).style.visibility = "hidden";
                document.getElementById("stab_" + i).style.backgroundColor = "rgb(200,235,255)";
                if (i == tab) {
                    document.getElementById("tab" + i).style.visibility = "visible";
                    document.getElementById("stab_" + i).style.backgroundColor = "rgb(211,213,255)";

                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dp_edit_member_new"
            LibraryList="~/DataWindow/ap_deposit/dp_acclist_bymemno.pbl" ClientScriptable="True" ClientEvents="true"
            ClientEventItemChanged="DwMainItemChanged" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True">
        </dw:WebDataWindowControl>
        <table style="width: 100%;  margin-top: 5px">
            <tr align="center" class="dwtab">
                <td style="background-color: rgb(200,235,255);" id="acclist" align="center" width="13%">
                    รายการบัญชี
                </td>
                <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab_1" width="18%"
                    onclick="showTabPage(1);">
                    ข้อมูลบัญชี
                </td>
                <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab_2" width="18%"
                    onclick="showTabPage(2);">
                    รายการเคลื่อนไหว
                </td>
                <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab_3" width="18%"
                    onclick="showTabPage(3);">
                    ต้นเงินฝาก
                </td>
                
               
            </tr>
            <tr>
                <td valign="top">
                    <dw:WebDataWindowControl ID="DwAccdeptList" runat="server" DataWindowObject="d_dp_accdept_list"
                            LibraryList="~/DataWindow/ap_deposit/dp_acclist_bymemno.pbl" ClientScriptable="True" ClientEvents="true"
                            ClientEventItemChanged="DwAccdeptListItemChanged" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientEventClicked="Dw_listClick">
                     </dw:WebDataWindowControl>
                </td>
                <td colspan="4" valign="top">
                <div id="tab1" style="visibility: visible; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_detail1" runat="server" DataWindowObject="d_dp_acclist_master"
                        LibraryList="~/DataWindow/ap_deposit/dp_acclist_bymemno.pbl" ClientScriptable="True" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientEventItemChanged="Dw_detail1ItemChange">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab2" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_detail2" runat="server" DataWindowObject="d_dp_acclist_item"
                        LibraryList="~/DataWindow/ap_deposit/dp_acclist_bymemno.pbl" ClientScriptable="True" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
                    </dw:WebDataWindowControl>
                    </div>
                <div id="tab3" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_detail3" runat="server" DataWindowObject="d_dp_acclist_fixed"
                        LibraryList="~/DataWindow/ap_deposit/dp_acclist_bymemno.pbl" ClientScriptable="True" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
                    </dw:WebDataWindowControl>
                    </div>
                  
                </td>
                <td colspan="5" valign="top">
                </td> 
             </tr>  
        </table>
        <asp:HiddenField ID="HdMembno" runat="server" />
        <asp:HiddenField ID="Hd_deptaccountno" runat="server" />
        <asp:HiddenField ID="Hd_row" runat="server" />
</asp:Content>
