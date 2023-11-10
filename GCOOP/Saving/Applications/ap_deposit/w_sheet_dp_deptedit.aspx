<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_deptedit.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_deptedit" Title="Untitled Page"
    ValidateRequest="false" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postAccountNo%>
    <%=CheckCoop%>
    <%=postNew%>
    <%=setCoopname %>
    <script type="text/javascript">
        function MenubarOpen() {
            Gcoop.OpenIFrame(900, 600, "w_dlg_dp_account_search.aspx", "");
        }

        function OnDwMainItemChanged(s, r, c, v) {
            if (c == "deptaccount_no") {
                var coopcontrol = Gcoop.GetEl("HfCoopcontrol").value;
                NewAccountNo(coopcontrol, v);
                return 0;
            }
        }

        function NewAccountNo(coopid, accNo) {
            Gcoop.GetEl("HfCoopid").value = coopid + "";
            objDwMain.SetItem(1, "deptaccount_no", Gcoop.Trim(accNo));
            objDwMain.AcceptText();
            setCoopname();
        }

        function OnDwTab1Click(s, r, c) {
            if (c == "datawindow")
                return 0;
        }

        function OnDwListCoopClick(s, r, c) {
            if (c == "cross_coopflag") {
                Gcoop.CheckDw(s, r, c, "cross_coopflag", 1, 0);
                CheckCoop();
            }
        }

        function OnDwListCoopItemChanged(s, r, c, v) {
            if (c == "dddwcoopname") {
                s.SetItem(r, c, v);
                s.AcceptText();
                var coopid = s.GetItem(r, "dddwcoopname");
                Gcoop.GetEl("HfCoopid").value = coopid + "";
            }
        }

        function ShowTabPage2(tab) {
            var i = 1;
            var tabamount = 5;
            for (i = 1; i <= tabamount; i++) {
                if (i == tab) {
                    document.getElementById("tab_" + i).style.visibility = "visible";
                    document.getElementById("stab_" + i).className = "tabTypeTdSelected";
                } else {
                    document.getElementById("tab_" + i).style.visibility = "hidden";
                    document.getElementById("stab_" + i).className = "tabTypeTdDefault";
                }
            }
        }

        function SheetLoadComplete() {
            Gcoop.SetLastFocus("deptaccount_no_0");
            Gcoop.Focus();
        }

        function Validate() {
            alert("ไม่มีคำสั่งการบันทึกข้อมูล");
            return false;
        }
    </script>
    <style type="text/css">
        .tabTypeDefault
        {
            width: 100%;
            border-spacing: 2px;
        }
        .tabTypeTdDefault
        {
            width: 20%;
            height: 45px;
            font-family: Tahoma, Sans-Serif, Times;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            vertical-align: middle;
            color: #777777;
            border: solid 1px #55A9CD;
            background-color: rgb(200,235,255);
            cursor: pointer;
        }
        .tabTypeTdSelected
        {
            width: 20%;
            height: 45px;
            font-family: Tahoma, Sans-Serif, Times;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            vertical-align: middle;
            color: #660066;
            border: solid 1px #77CBEF;
            background-color: #76EFFF;
            cursor: pointer;
            text-decoration: underline;
        }
        .tabTypeTdDefault:hover
        {
            color: #882288;
            border: solid 1px #77CBEF;
            background-color: #98FFFF;
        }
        .tabTypeTdSelected:hover
        {
            color: #882288;
            border: solid 1px #77CBEF;
            background-color: #98FFFF;
        }
        .tabTableDetail
        {
            width: 99%;
        }
        .tabTableDetail td
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server" Text=""></asp:Literal>
    <dw:WebDataWindowControl ID="DwListCoop" runat="server" DataWindowObject="d_dp_dept_cooplist"
        LibraryList="~/DataWindow/ap_deposit/cm_constant_config.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventItemChanged="OnDwListCoopItemChanged" ClientFormatting="True" TabIndex="1"
        ClientEventClicked="OnDwListCoopClick">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_dept_edit_master"
        LibraryList="~/DataWindow/ap_deposit/dp_deptedit.pbl" ClientEventItemChanged="OnDwMainItemChanged"
        ClientEventClicked="OnDwMainClick" ClientFormatting="True">
    </dw:WebDataWindowControl>
    <table class="tabTypeDefault">
        <tr>
            <td class="tabTypeTdSelected" id="stab_1" onclick="ShowTabPage2(1);">
                รายการเคลื่อนไหว
            </td>
            <td class="tabTypeTdDefault" id="stab_2" onclick="ShowTabPage2(2);">
                ต้นเงินฝาก
            </td>
            <td class="tabTypeTdDefault" id="stab_3" onclick="ShowTabPage2(3);">
                ผู้ฝากร่วม
            </td>
            <td class="tabTypeTdDefault" id="stab_4" onclick="ShowTabPage2(4);">
                ตัวอย่างลายมือ
            </td>
            <%-- <td class="tabTypeTdDefault" id="stab_5" onclick="ShowTabPage2(5);">
                คิดดอกเบี้ย
            </td>--%>
        </tr>
    </table>
    <table class="tabTableDetail">
        <tr>
            <td style="height: 200px;" valign="top">
                <div id="tab_1" style="visibility: visible; position: absolute;">
                    <asp:Panel ID="Panel1" Width="758px" runat="server" ScrollBars="Horizontal">
                        <dw:WebDataWindowControl ID="DwTab1" runat="server" DataWindowObject="d_dp_dept_edit_item1"
                            LibraryList="~/DataWindow/ap_deposit/dp_deptedit.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" RowsPerPage="10"
                            ClientEventClicked="OnDwTab1Click" ClientScriptable="True" ClientFormatting="True">
                            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo">
                            </PageNavigationBarSettings>
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </div>
                <div id="tab_2" style="visibility: hidden; position: absolute;">
                    <asp:Panel ID="Panel2" Width="758px" runat="server" ScrollBars="Horizontal">
                        <dw:WebDataWindowControl ID="DwTab2" runat="server" DataWindowObject="d_dp_dept_edit_fixed"
                            LibraryList="~/DataWindow/ap_deposit/dp_deptedit.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" RowsPerPage="10"
                            ClientFormatting="True" ClientScriptable="True">
                            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo">
                            </PageNavigationBarSettings>
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </div>
                <div id="tab_3" style="visibility: hidden; position: absolute;">
                    <asp:Panel ID="Panel3" Width="758px" runat="server">
                        <dw:WebDataWindowControl ID="DwTab3" runat="server" DataWindowObject="d_dp_dept_edit_codept"
                            LibraryList="~/DataWindow/ap_deposit/dp_deptedit.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientScriptable="True">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </div>
                <div id="tab_4" style="visibility: hidden; position: absolute;">
                    <asp:Panel ID="Panel4" Width="758px" runat="server">
                        <table>
                            <tr>
                                <td valign="top">
                                    <dw:WebDataWindowControl ID="DwTab4" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                                        AutoSaveDataCacheAfterRetrieve="True" DataWindowObject="d_dept_edit_pics" LibraryList="~/DataWindow/ap_deposit/dp_deptedit.pbl"
                                        ClientFormatting="True" ClientScriptable="True">
                                    </dw:WebDataWindowControl>
                                </td>
                            </tr>
                        </table>
                        <asp:Image ID="Img_dp_signature" runat="server" Height="300px" Width="400px" /><br />
                        <asp:Image ID="Img_dp_signature2" runat="server" Height="300px" Width="400px" />

                    </asp:Panel>
                </div>
                <div id="tab_5" style="visibility: hidden; position: absolute;">
                    &nbsp;
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HSelect" runat="server" Value="01" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:HiddenField ID="HdRowsel" runat="server" Value="false" />
    <asp:HiddenField ID="HfCoopid" runat="server" />
    <asp:HiddenField ID="HfCoopcontrol" runat="server" />
</asp:Content>
