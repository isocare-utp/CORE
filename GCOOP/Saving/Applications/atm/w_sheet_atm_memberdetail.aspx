<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_atm_memberdetail.aspx.cs" Inherits="Saving.Applications.atm.w_sheet_atm_memberdetail" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostBack%>
    <%=jsPostMemberNo%>
    <%=jsRetrieve%>
    <script type="text/JavaScript">
        function showTabPage(tab) {
            var i = 1;
            var tabamount = 4;
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

        function OnDwDetailItemChanged(s, r, c, v) {
            s.SetItem(r, c, v);
            s.AcceptText();
            switch (c) {
                case "member_no":
                    jsPostMemberNo();
                    break;
                case "atmcard_id":
                    jsRetrieve();
                    break;
            }
        }

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_atm_member_detail"
        LibraryList="~/DataWindow/atm/dp_atm_memberdetail.pbl" ClientScriptable="True"
        ClientEvents="true" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" AutoRestoreContext="False" ClientEventItemChanged="OnDwDetailItemChanged"
        ClientEventButtonClicked="OnDwDetailButtonClicked">
    </dw:WebDataWindowControl>
    <table style="width: 100%; border: solid 1px; margin-top: 5px">
        <tr align="center" class="dwtab">
            <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab_4" width="25%"
                onclick="showTabPage(4);">
                ข้อมูลทางการเงิน
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_1" width="25%"
                onclick="showTabPage(1);">
                ข้อมูลการใช้งาน (ERROR)
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_2" width="25%"
                onclick="showTabPage(2);">
                ข้อมูลการจัดการ
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_3" width="25%"
                onclick="showTabPage(3);">
                ข้อมูลการทำรายการ
            </td>
        </tr>
    </table>
    <table style="width: 100%; border: solid 1px; margin-top: 2px" class="dwcontent">
        <tr>
            <td style="height: 250px;" valign="top">
                <div id="tab_1" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="DwErrorlog" runat="server" DataWindowObject="d_atm_member_atmerr_log"
                        LibraryList="~/DataWindow/atm/dp_atm_memberdetail.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientEventItemChanged="DwDet1ItemChanged" ClientFormatting="True" TabIndex="1000"
                        ClientEventButtonClicked="DwDet1ButtonClicked" Width="742px" Height="250px">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_2" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="DwMemberlog" runat="server" DataWindowObject="d_atm_member_atmmember_log"
                        LibraryList="~/DataWindow/atm/dp_atm_memberdetail.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientEventItemChanged="DwTypeItemChanged" ClientFormatting="True" TabIndex="500"
                        ClientEventButtonClicked="DwTypeButtonClicked" Width="742px" Height="250px">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_3" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="DwWithdraw" runat="server" DataWindowObject="d_atm_withdraw_log"
                        LibraryList="~/DataWindow/atm/dp_atm_memberdetail.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientEventItemChanged="DwDetailItemChanged" ClientFormatting="True" TabIndex="1000"
                        ClientEventButtonClicked="DwDetailButtonClicked" Width="742px" Height="250px">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_4" style="visibility: visible; position: absolute;">
                    <table>
                        <tr>
                            <td>
                                <dw:WebDataWindowControl ID="DwDept" runat="server" DataWindowObject="d_atm_member_dept"
                                    LibraryList="~/DataWindow/atm/dp_atm_memberdetail.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                                    ClientFormatting="True" TabIndex="1000" Width="300px" Height="250px">
                                </dw:WebDataWindowControl>
                            </td>
                            <td>
                                <dw:WebDataWindowControl ID="DwLoan" runat="server" DataWindowObject="d_atm_member_loan"
                                    LibraryList="~/DataWindow/atm/dp_atm_memberdetail.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                                    ClientFormatting="True" TabIndex="1000" Width="442px" Height="250px">
                                </dw:WebDataWindowControl>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenFieldTab" runat="server" />
</asp:Content>
