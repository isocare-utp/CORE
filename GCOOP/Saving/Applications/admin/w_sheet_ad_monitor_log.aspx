<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_ad_monitor_log.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_monitor_log"
    Culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=ajaxUrl%>
    <%=postSelectName%>
    <style type="text/css">
        .Tb001
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        .Tb001 td
        {
            border: 1px solid #000000;
            border-spacing: 0px;
            height: 22px;
        }
        .Tb001 input
        {
            border: none;
            width: 100%;
            font-family: Tahoma;
            font-size: 11px;
        }
    </style>
    <script type="text/javascript">
        var monitorUrl = '<%=monitorUrl%>';
        var continueTime = 0;
        var maxDate = "2000-01-01_00:00:00";
        var loopInTime = 0;
        var loopTime = 0;
        var lang = "th";

        function SheetLoadComplete() {
        }

        function Validate() {
            return false;
        }

        function RunAjax() {
            Ajax.toGet(monitorUrl + "?hit_date=" + maxDate + "&lang=" + lang, ContinueAjax, "");
        }

        function Start(ln) {
            lang = ln;
            document.getElementById("startTH").disabled = true;
            document.getElementById("startEN").disabled = true;
            continueTime = 3000;
            RunAjax();
        }

        function Pause() {
            continueTime = 0;
        }

        function MessageAlert(objMessage) {
            var mess = objMessage.value;
            mess = mess.replace(/^\s+|\s+$/g, '');
            if (mess != "") {
                alert(mess);
            }
        }

        function ContinueAjax(text) {
            var cut = "Ɣ";
            var result = "";
            try {
                var textArr = text.split(cut);
                maxDate = Gcoop.Trim(textArr[0]);
                var textData = Gcoop.Trim(textArr[1]);
                try {
                    if (textData.indexOf("<style>") >= 0) {
                        result = textData;
                    }
                } catch (err) { }
            } catch (err) {
            }
            if (result != "") {
                loopInTime++;
                document.getElementById("divForAjax").innerHTML = result;
            }
            loopTime++;
            document.getElementById("divLoopTime").innerHTML = " .... loop " + loopInTime + "/" + loopTime;
            if (continueTime > 0) {
                setTimeout("RunAjax()", continueTime);
            } else {
                maxDate = "2000-01-01_00:00:00";
                document.getElementById("startTH").disabled = false;
                document.getElementById("startEN").disabled = false;
            }
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame(730, 1500, "w_dlg_ad_monitor_select.aspx", "");
        }

        function UsingSelectName(selectName) {
            Pause();
            Gcoop.GetEl("HdSelectName").value = selectName;
            postSelectName();
        }

        function AlertText(primary_id) {
            alert(Gcoop.Trim(document.getElementById(primary_id).innerHTML));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <input id="startTH" type="button" style="width: 60px;" value="Run TH" onclick="Start('th')" />
    &nbsp;
    <input id="startEN" type="button" style="width: 60px;" value="Run EN" onclick="Start('en')" />
    &nbsp;
    <input type="button" style="width: 60px;" value="Pause" onclick="Pause()" />
    <span id="divLoopTime"><asp:Label ID="LbLoopTime" runat="server" Text=""></asp:Label></span>
    <br />
    <div id="divForAjax">
        <br />
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <table width="750" class="Tb001">
                    <tr align="center" bgcolor="#D3E7FF">
                        <td width="6%">
                            Log
                        </td>
                        <td width="11%">
                            WebServer
                        </td>
                        <td width="11%">
                            Client
                        </td>
                        <td width="5%">
                            User
                        </td>
                        <td width="5%">
                            Coop
                        </td>
                        <td width="6%">
                            App
                        </td>
                        <td width="15%">
                            CurrentPage
                        </td>
                        <td width="8%">
                            JsPostback
                        </td>
                        <td width="2%">
                            M+T
                        </td>
                        <td width="15%">
                            ServerMessage
                        </td>
                        <td width="3%">
                            Load
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <div id="<%#Eval("primary_id")%>" style="visibility: hidden; position: absolute;">
                            <%#GetAlertText(Eval("primary_id"))%>
                        </div>
                        <input type="text" value="<%#Eval("hit_time")%>" readonly="readonly" onclick="AlertText('<%#Eval("primary_id")%>')"
                            style="cursor: pointer" />
                    </td>
                    <td>
                        <input type="text" value="<%#Eval("server_ip")%>" readonly="readonly" onclick="AlertText('<%#Eval("primary_id")%>')"
                            style="cursor: pointer" />
                    </td>
                    <td>
                        <input type="text" value="<%#Eval("client_ip")%>" readonly="readonly" onclick="AlertText('<%#Eval("primary_id")%>')"
                            style="cursor: pointer" />
                    </td>
                    <td>
                        <input type="text" value="<%#Eval("username")%>" readonly="readonly" onclick="AlertText('<%#Eval("primary_id")%>')"
                            style="cursor: pointer" />
                    </td>
                    <td>
                        <input type="text" value="<%#Eval("coop_id")%>" readonly="readonly" onclick="AlertText('<%#Eval("primary_id")%>')"
                            style="cursor: pointer" />
                    </td>
                    <td>
                        <input type="text" value="<%#Eval("application")%>" readonly="readonly" onclick="AlertText('<%#Eval("primary_id")%>')"
                            style="cursor: pointer" />
                    </td>
                    <td>
                        <input type="text" value="<%#Eval("curr_page")%>" readonly="readonly" onclick="AlertText('<%#Eval("primary_id")%>')"
                            style="cursor: pointer" />
                    </td>
                    <td>
                        <input type="text" value="<%#Eval("jspostback")%>" readonly="readonly" onclick="AlertText('<%#Eval("primary_id")%>')"
                            style="cursor: pointer" />
                    </td>
                    <td>
                        <input type="text" value="<%#Eval("methode_type")%>" readonly="readonly" onclick="AlertText('<%#Eval("primary_id")%>')"
                            style="cursor: pointer" />
                    </td>
                    <td>
                        <input type="text" value="<%#Eval("server_message")%>" readonly="readonly" onclick="AlertText('<%#Eval("primary_id")%>')"
                            style="cursor: pointer" />
                    </td>
                    <td>
                        <input type="text" value="<%#Eval("load_time")%>" readonly="readonly" onclick="AlertText('<%#Eval("primary_id")%>')"
                            style="cursor: pointer" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <asp:HiddenField ID="HdSelectName" Value="" runat="server" />
</asp:Content>
